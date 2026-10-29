# Distributed Key-Value Store System Design (DynamoDB / Cassandra)

[← Back to System Design](system-design.md)

> **Core Building Blocks Used**: [Chapter 3 — Consistent Hashing with Virtual Nodes](fundamentals/03-caching-and-scaling.md#4-consistent-hashing) · [Chapter 4 — LSM-Tree, WAL, MemTable & SSTable Compaction](fundamentals/04-databases-sql-vs-nosql.md#btree-vs-lsm-tree-why-sql-vs-nosql-engines-differ) · [Chapter 5 — CAP/PACELC, Tunable Quorum ($R+W>N$) & Vector Clocks](fundamentals/05-distributed-systems-and-messaging.md#1-cap-theorem--pacelc-theorem) · [Chapter 7 — Bloom Filters, Gossip Protocol & Merkle Trees](fundamentals/07-probabilistic-structures-and-stream-processing.md#2-decentralized-cluster-protocols--conflict-resolution)

## Overview

A **Distributed Key-Value Store** (inspired by the seminal **Amazon Dynamo paper**, **Apache Cassandra**, **ScyllaDB**, and **Amazon DynamoDB**) is a horizontally scalable, fault-tolerant storage engine that maps arbitrary keys to values (`Get(key)`, `Put(key, value)`, `Delete(key)`) with predictable single-digit millisecond latency at petabyte scale.

Designing a distributed key-value store from scratch is the quintessential distributed systems interview question because it synthesizes almost every core primitive in distributed computing:

1. **Data Partitioning & Placement**: Distributing keys evenly across hundreds of storage nodes using **Consistent Hashing with Virtual Nodes (`vnodes`)** so adding or removing a node only moves $O(K/N)$ keys.
2. **High Availability & Tunable Replication ($N, W, R$)**: Replicating every key across $N$ physical racks/AZs and allowing clients to trade consistency vs. latency per request using **Quorum Consensus ($R + W > N$)**, **Sloppy Quorums**, and **Hinted Handoff**.
3. **Conflict Detection & Resolution**: Using **Vector Clocks** (or Hybrid Logical Clocks) to distinguish causally ordered overwrites from concurrent conflicting writes during network partitions.
4. **Decentralized Cluster Membership & Failure Detection**: Eliminating single points of failure (leaderless peer-to-peer ring) via **Gossip Protocol** and the **$\Phi$ (Phi) Accrual Failure Detector**.
5. **Replica Synchronization (Anti-Entropy)**: Healing stale replicas in the background using **Read Repair** (active) and **Merkle Tree cryptographic range comparisons** (passive).
6. **Node-Local Storage Engine Internals**: Achieving extreme write throughput and fast point reads on each node via an **LSM-Tree (`Write-Ahead Log` $\to$ `MemTable` $\to$ `Immutable SSTables` + `Bloom Filters` + `Compaction`)**.

---

## Functional Requirements

* **`Put(key, value, context)`**: Insert or update the value associated with a `key` (or composite `(partition_key, sort_key)`), returning an updated version context (vector clock / sequence number).
* **`Get(key, consistencyLevel)`**: Retrieve the value(s) associated with `key` using a caller-specified consistency mode (`ONE`, `QUORUM`, `ALL`, or `LOCAL_QUORUM`).
* **`Delete(key, context)`**: Logically delete a key by writing a distributed **Tombstone** marker that replicates across nodes before garbage collection.
* **Range Scans on Sort Key (Optional DynamoDB/Cassandra Extension)**: Query ordered items within a single partition key (`Scan(partition_key, sort_key_start, sort_key_end)`).
* **TTL (Time-To-Live) Expiration**: Automatically expire and purge keys after a configurable timestamp.

---

## Non-Functional Requirements

* **Predictable Low Latency**: P99 **Write Latency $< 5\text{ ms}$** and P99 **Read Latency $< 5\text{ ms}$** regardless of total dataset size (whether $100\text{ GB}$ or $5\text{ PB}$).
* **Linear Horizontal Scalability**: Doubling the number of storage nodes doubles both write/read QPS and storage capacity without downtime.
* **Tunable CAP / PACELC Profile**:
  * **AP Mode (`W=1, R=1`)**: Always writable and readable even during multi-node partitions.
  * **Strong Quorum Mode (`N=3, W=2, R=2`, where $R + W > N$)**: Strong read-your-writes consistency across overlapping replica sets.
* **High Durability ($99.999999999\%$)**: Zero data loss once a quorum write succeeds, surviving simultaneous disk crashes or an entire Availability Zone (AZ) outage.

---

## Back of the Envelope Estimations

### Traffic & Throughput

* **Total Stored Keys**: $100\text{ Billion keys}$ across the cluster.
* **Average Value Size**: $1\text{ KB}$ (plus $64\text{ bytes}$ key & metadata).
* **Read QPS**: $1,000,000\text{ reads/sec}$ ($1\text{M QPS}$).
* **Write QPS**: $250,000\text{ writes/sec}$ ($250\text{K QPS}$).

### Storage & Cluster Sizing

* **Raw Unique Data Size**:
  $$100 \times 10^9\text{ keys} \times 1\text{ KB} = \mathbf{100\text{ Terabytes}}$$
* **Replicated Storage ($N = 3$ Replication Factor across 3 AZs)**:
  $$100\text{ TB} \times 3 = \mathbf{300\text{ Terabytes}} \quad (\text{With } 30\% \text{ LSM compaction headroom: } \approx \mathbf{400\text{ TB NVMe SSD}})$$
* **Bloom Filter & Index Memory per Node**:
  * A 10-bits-per-key Bloom filter across $300\text{B total replica keys}$ requires:
    $$300 \times 10^9 \times 10\text{ bits} \approx 375\text{ Gigabytes RAM total across the cluster}$$
* **Cluster Node Count**:
  * Using **100 storage nodes** (each with $64\text{ GB RAM}$ and $4\text{ TB NVMe SSD}$):
  * Each node handles $\sim 7,500\text{ replica writes/sec}$ ($250\text{K} \times 3 / 100$), $\sim 10,000\text{ reads/sec}$, and holds only $3.75\text{ GB}$ of Bloom filters in RAM.

---

## High-Level System Architecture

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 25, 'rankSpacing': 45, 'padding': 10}}}%%
flowchart LR
    Client["Client SDK (Token-Aware)"]
    subgraph Ring["Leaderless Consistent Hashing Ring (N=3 across 3 AZs)"]
        Coord["Coordinator Node (vnode Owner)"]
        RepA["Replica Node A (AZ-1)"]
        RepB["Replica Node B (AZ-2)"]
        RepC["Replica Node C (AZ-3)"]
    end
    subgraph NodeInternals["Storage Node Local LSM Engine"]
        WAL["Write-Ahead Log (Sequential Disk)"]
        Mem["MemTable (In-Memory SkipList)"]
        Bloom["Bloom Filter + Key Index (RAM)"]
        SST[("Immutable SSTables (L0 -> L3 NVMe)")]
    end
    subgraph Background["Background Anti-Entropy & Gossip"]
        Gossip["Gossip Membership & Phi Detector"]
        Merkle["Merkle Tree Anti-Entropy Sync"]
    end

    Client e1@-->|"1. Put/Get(key)"| Coord
    Coord e2@-->|"2a. Local Write/Read"| RepA
    Coord e3@-->|"2b. Async RPC"| RepB
    Coord e4@-->|"2c. Async RPC"| RepC
    RepA e5@-->|"Append"| WAL
    RepA e6@-->|"Upsert"| Mem
    Mem e7@-->|"Flush @ 64MB"| SST
    RepA e8@-->|"Check Key Exists"| Bloom
    Bloom e9@-->|"Read Block"| SST
    RepA e10@<-->|"1s Heartbeat"| Gossip
    RepB e11@<-->|"Compare Hash Trees"| Merkle

    e1@{ animate: true }
    e2@{ animate: true }
    e3@{ animate: true }
    e4@{ animate: true }
    e5@{ animate: true }
    e6@{ animate: true }
    e7@{ animate: true }
    e8@{ animate: true }
    e9@{ animate: true }
    e10@{ animate: true }
    e11@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff

    class Client client
    class Coord edge
    class RepA,RepB,RepC service
    class Mem,Bloom cache
    class WAL,SST storage
    class Gossip,Merkle worker

    style Ring fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style NodeInternals fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
    style Background fill:transparent,stroke:#22d3ee,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Core Components & Data Flows

### 1. Consistent Hashing with Virtual Nodes & Rack-Aware Preference Lists

* **Hash Ring ($[0, 2^{128}-1]$)**: Keys are hashed via `MurmurHash3(partition_key)` onto a circular keyspace.
* **Virtual Nodes (`vnodes`)**: Instead of assigning a physical machine to a single point on the ring (which causes severe data skew and hotspots when a node joins/leaves), each physical server owns **256 Virtual Nodes (`vnodes`)** scattered uniformly across the ring.
* **Rack / AZ-Aware Preference List ($N=3$)**: To replicate a key $K$, the coordinator walks clockwise from `hash(K)` to select $N=3$ distinct physical nodes, skipping any `vnode` belonging to an Availability Zone already selected so the 3 replicas reside in **3 separate AZs (`AZ-1`, `AZ-2`, `AZ-3`)**.

### 2. Tunable Quorum ($R + W > N$), Read Repair & Hinted Handoff

```mermaid
sequenceDiagram
    autonumber
    actor Client
    participant Coord as "Coordinator (Node A, AZ-1)"
    participant NodeB as "Replica B (AZ-2)"
    participant NodeC as "Replica C (AZ-3 - Stale)"

    Note over Client,NodeC: Write Path (N=3, W=2 Quorum)
    Client->>Coord: PUT(key="user:42", val="v2", clock=[A:1])
    Coord->>Coord: Local WAL + MemTable Write (Clock -> [A:2])
    par Parallel Replica Writes
        Coord->>NodeB: Replicate(key="user:42", val="v2", clock=[A:2])
        Coord->>NodeC: Replicate(key="user:42", val="v2", clock=[A:2])
    end
    NodeB-->>Coord: ACK Write (Quorum W=2 Reached!)
    Coord-->>Client: 200 OK (clock=[A:2], Latency: 2.1ms)
    Note right of NodeC: Network glitch drops write to Node C (still has "v1", [A:1])

    Note over Client,NodeC: Read Path with Active Read Repair (N=3, R=2 Quorum)
    Client->>Coord: GET(key="user:42", R=2)
    par Parallel Quorum Read + Digest
        Coord->>Coord: Local Read -> ("v2", clock=[A:2])
        Coord->>NodeC: Read Digest(key="user:42")
    end
    NodeC-->>Coord: Digest Response -> ("v1", clock=[A:1])
    Coord->>Coord: Compare Clocks: [A:2] > [A:1] (Winner = "v2")
    Coord-->>Client: 200 OK (val="v2", clock=[A:2])
    Coord-)NodeC: Async Read Repair: PUT(key="user:42", val="v2", clock=[A:2])
```

* **Sloppy Quorum & Hinted Handoff (High Availability Mode)**: If both `Node B` and `Node C` are temporarily unreachable during a network partition and the client requires high write availability, the coordinator writes a **Hinted Handoff** record to a healthy fallback node (`Node D`) tagged with `target=Node C` in a local hints table. Once Gossip detects `Node C` has recovered, `Node D` replays the hinted writes and deletes the hint.

### 3. Node-Local Storage Engine: LSM-Tree + Bloom Filter + SSTable Compaction

Why do distributed KV stores (Cassandra, RocksDB, Bigtable) use **Log-Structured Merge-Trees (LSM)** instead of **B+Trees**? Because B+Trees modify 4KB–16KB disk pages in-place (random I/O), whereas LSM-Trees turn all writes into **100% sequential disk appends**:

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph WritePath["1. Fast Sequential Write Path (< 1ms)"]
        WriteReq["PUT / DELETE(Tombstone)"]
        WAL["Commit Log / WAL (Sequential Append)"]
        MemTable["MemTable (RAM Red-Black / SkipList)"]
    end
    subgraph DiskTiers["2. Immutable On-Disk SSTable Tiers"]
        L0["Level 0 SSTables (Flushed from RAM)"]
        L1["Level 1 SSTables (Non-Overlapping Keys)"]
        L2["Level 2 SSTables (10x Larger)"]
    end
    subgraph ReadPath["3. Read Path Optimizations (RAM)"]
        RowCache["Row / Block Cache"]
        Bloom["Per-SSTable Bloom Filters (10 bits/key)"]
        SparseIdx["Sparse Key Offset Index"]
    end

    WriteReq e1@-->|"1a. fsync Append"| WAL
    WriteReq e2@-->|"1b. O(log N) Insert"| MemTable
    MemTable e3@-->|"Flush @ 64MB"| L0
    L0 e4@-->|"Sorted Merge Compaction"| L1
    L1 e5@-->|"Leveled Compaction"| L2
    Bloom e6@-.->|"Skip 99% of SSTables"| L1
    SparseIdx e7@-.->|"Binary Search Block"| L1
    RowCache e8@-.->|"Hot Key Hit"| MemTable

    e1@{ animate: true }
    e2@{ animate: true }
    e3@{ animate: true }
    e4@{ animate: true }
    e5@{ animate: true }
    e6@{ animate: true }
    e7@{ animate: true }
    e8@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff

    class WriteReq client
    class MemTable,RowCache,Bloom,SparseIdx cache
    class WAL,L0,L1,L2 storage

    style WritePath fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style DiskTiers fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
    style ReadPath fill:transparent,stroke:#fb7185,stroke-width:1.5px,stroke-dasharray: 5 5
```

1. **Write Path**: Append to on-disk **Write-Ahead Log (WAL)** for crash durability + insert into in-memory **MemTable (`SkipList`)**. Return `200 OK` immediately.
2. **Flush**: When the MemTable reaches $64\text{ MB}$, freeze it and flush its sorted keys sequentially to an immutable **SSTable (Sorted String Table)** on NVMe disk, generating a **Bloom Filter** and **Sparse Index** in the process.
3. **Read Path**: Check `MemTable` first. If not found, check the `Row/Block Cache`. If still a miss, test each SSTable's **in-memory Bloom Filter**; if the Bloom filter returns `false`, skip that SSTable file with **zero disk I/O**. For matching SSTables, binary-search the in-memory `Sparse Index` and read a single $4\text{ KB}$ compressed disk block.

---

## API Endpoints & Wire Protocol

In production (Cassandra CQL Binary Protocol / DynamoDB HTTP-JSON / gRPC), requests use persistent multiplexed TCP connections:

### 1. Put Item (Write with Tunable Consistency & Conditional Version)

```http
PUT /api/v1/tables/user_sessions/items/usr_99812
X-Consistency-Level: QUORUM
Content-Type: application/json
```

```json
// Request
{
  "partitionKey": "usr_99812",
  "sortKey": "sess_2026_10_28",
  "value": {
    "cartItems": ["sku_101", "sku_204"],
    "device": "iOS-18"
  },
  "contextVectorClock": {"nodeA": 4, "nodeB": 2},
  "ttlSeconds": 86400
}

// Response: 200 OK
{
  "status": "STORED",
  "newVectorClock": {"nodeA": 5, "nodeB": 2},
  "replicasAcked": 2
}
```

### 2. Get Item (Quorum Read with Conflict Siblings)

```http
GET /api/v1/tables/user_sessions/items/usr_99812?sortKey=sess_2026_10_28
X-Consistency-Level: QUORUM
```

```json
// Response: 200 OK (Single Resolved Version or Divergent Siblings)
{
  "partitionKey": "usr_99812",
  "sortKey": "sess_2026_10_28",
  "versions": [
    {
      "value": {"cartItems": ["sku_101", "sku_204"], "device": "iOS-18"},
      "vectorClock": {"nodeA": 5, "nodeB": 2},
      "lastModifiedEpochMs": 1793246015000
    }
  ]
}
```

---

## Object Model & On-Disk Data Structures

### Subsystem Architecture & Trade-Off Matrix

| Subsystem | Design Choice | Alternative Considered | Why This Choice Wins at Scale |
| :--- | :--- | :--- | :--- |
| **Cluster Topology** | **Leaderless Peer-to-Peer Ring (Dynamo / Cassandra)** | Single-Leader per Shard (Raft / Spanner / HBase) | Eliminates leader failover stalls (typically 5–15 seconds). Any replica in the preference list can coordinate writes immediately (`AP`). |
| **Storage Engine** | **LSM-Tree (`WAL` + `MemTable` + `SSTable`)** | B+Tree (InnoDB / PostgreSQL) | Sustains $5\text{--}10\times$ higher write throughput with lower write amplification on NVMe SSDs by turning random key updates into sequential flushes. |
| **Conflict Resolution** | **LWW (HLC Timestamp) or Vector Clocks / CRDTs** | Distributed 2PC Locks | Avoids cross-node coordination locks on the hot path. Vector clocks / CRDTs preserve concurrent updates (e.g., merging two shopping carts), while LWW simplifies scalar fields. |
| **Anti-Entropy Repair** | **Merkle Trees (Cryptographic Hash Trees) per Virtual Node** | Full Key-Range Dump Comparison | Two replicas compare the root hashes of a key range in $O(1)$ bytes and traverse $O(\log K)$ tree depth to pinpoint exact out-of-sync keys without transferring gigabytes over the network. |

```mermaid
erDiagram
    VIRTUAL_NODE ||--o{ SSTABLE_FILE : "stores on disk"
    SSTABLE_FILE ||--|{ DATA_BLOCK : "contains sorted"
    SSTABLE_FILE ||--|| BLOOM_FILTER : "indexed by"
    SSTABLE_FILE ||--|| MERKLE_TREE : "verified by"

    VIRTUAL_NODE {
        int vnodeId PK
        bigint tokenRangeStart
        bigint tokenRangeEnd
        varchar_64 physicalNodeId
        varchar_16 availabilityZone
    }
    SSTABLE_FILE {
        uuid sstableId PK
        int vnodeId FK
        int compactionLevel
        bigint minKeyHash
        bigint maxKeyHash
        bigint fileSizeBytes
    }
    DATA_BLOCK {
        bytes partitionKey
        bytes sortKey
        bytes valuePayload
        jsonb vectorClock
        bigint hlcTimestamp
        boolean isTombstone
        int ttlEpochSeconds
    }
    BLOOM_FILTER {
        uuid sstableId PK
        int numHashFunctions
        bytes bitArrayMemory
    }
    MERKLE_TREE {
        int vnodeId PK
        bytes rootSha256Hash
        bytes leafRangeHashes
    }
```

---

## System Design Diagram

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph ClientTier["Smart Client SDK"]
        App["Application + Token-Aware Driver"]
    end
    subgraph RingTier["Consistent Hashing Ring (256 vnodes/node)"]
        Coord{"Coordinator Node (Hash Router)"}
        Replica1["Replica 1 (AZ-a)"]
        Replica2["Replica 2 (AZ-b)"]
        Replica3["Replica 3 (AZ-c)"]
        HintNode["Fallback Node (Hinted Handoff)"]
    end
    subgraph LocalStorage["Local LSM-Tree Storage Engine"]
        CommitLog["Commit Log (WAL)"]
        SkipList["MemTable (RAM SkipList)"]
        BloomRAM[("Bloom Filters & Sparse Index (RAM)")]
        SSTables[("Leveled SSTables (NVMe SSD)")]
    end
    subgraph ClusterProtocols["Decentralized Background Protocols"]
        GossipProto["Gossip Membership & Phi Accrual"]
        MerkleRepair["Merkle Tree Anti-Entropy & Read Repair"]
    end

    App e1@-->|"Murmur3(key) Direct Route"| Coord
    Coord e2@-->|"Quorum Write/Read"| Replica1
    Coord e3@-->|"Quorum Write/Read"| Replica2
    Coord e4@-->|"Quorum Write/Read"| Replica3
    Coord e5@-.->|"If AZ-c Down: Store Hint"| HintNode
    Replica1 e6@-->|"1. Append"| CommitLog
    Replica1 e7@-->|"2. Upsert"| SkipList
    SkipList e8@-->|"3. Flush & Compact"| SSTables
    Replica1 e9@-->|"4. Filter SSTables"| BloomRAM
    BloomRAM e10@-->|"5. Offset Seek"| SSTables
    Replica1 e11@<-->|"Every 1s UDP Gossip"| GossipProto
    Replica2 e12@<-->|"Sync Divergent Leaf Ranges"| MerkleRepair

    e1@{ animate: true }
    e2@{ animate: true }
    e3@{ animate: true }
    e4@{ animate: true }
    e5@{ animate: true }
    e6@{ animate: true }
    e7@{ animate: true }
    e8@{ animate: true }
    e9@{ animate: true }
    e10@{ animate: true }
    e11@{ animate: true }
    e12@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff

    class App client
    class Coord edge
    class Replica1,Replica2,Replica3,HintNode service
    class SkipList,BloomRAM cache
    class CommitLog,SSTables storage
    class GossipProto,MerkleRepair worker

    style ClientTier fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style RingTier fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style LocalStorage fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
    style ClusterProtocols fill:transparent,stroke:#22d3ee,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Deep Dive & Key Technical Challenges

### 1. Why Deletes Require Tombstones (& The `gc_grace_seconds` Zombie Data Bug)

Because SSTables on disk are **strictly immutable**, `Delete(key)` cannot erase a row in-place from older SSTable files:

* **How Tombstones Work**: A delete writes a new record `(key, value=null, is_tombstone=true, timestamp=T_del)` to the MemTable. During subsequent reads, the tombstone shadows older versions in lower SSTables.
* **The Zombie Resurrection Bug**: Suppose Node C is offline for 3 days. On Day 1, a key is deleted on Nodes A and B, and if Nodes A and B immediately garbage-collected the tombstone during compaction on Day 2, when Node C comes back online on Day 3 and runs Merkle-tree repair, Nodes A and B would think they *never heard of the key* and re-replicate Node C's deleted data!
* **The Solution (`gc_grace_seconds`)**: Tombstones are retained for a mandatory grace period (e.g., **10 days**) before compaction physically purges them, and any node offline longer than `gc_grace_seconds` must have its local disk wiped and re-bootstrapped from healthy replicas.

### 2. Compaction Strategies: Size-Tiered (STCS) vs. Leveled (LCS) vs. Time-Window (TWCS)

| Strategy | How It Works | Read Amplification | Write Amplification | Disk Space Overhead | Best Workload |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Size-Tiered (STCS)** | Merges 4 SSTables of similar size into 1 larger SSTable. | High (key may exist in many SSTables) | **Low** | **High ($50\%$ free disk required)** | Write-heavy append workloads. |
| **Leveled (LCS / RocksDB)** | Organizes SSTables into exponentially sized levels ($L_1 = 100\text{ MB}, L_2 = 1\text{ GB}, L_3 = 10\text{ GB}$) with disjoint key ranges per level. | **Low ($90\%$ of reads touch $\le 1$ SSTable)** | High | **Low ($\sim 10\%$ free disk)** | **Point-read latency-sensitive KV workloads**. |
| **Time-Window (TWCS)** | Groups SSTables into discrete time buckets (1 hour, 1 day) and drops expired SSTable files whole via `unlink()`. | Low for time-range queries | **Lowest** | Low | Time-series logs & expiring session keys. |

### 3. Handling Hot Keys (Celebrity / Flash-Sale Partitions)

Consistent hashing distributes *distinct keys* evenly, but if 500,000 requests/sec target the **same single key** (`product:iphone_launch`), all requests hit the same 3 replicas:

* **Client-Side / Edge L1 Read Cache**: Token-aware SDKs cache hot immutable/semi-static keys locally for 100–500ms.
* **Key Salting / Write Sharding**: For high-velocity counters or append partitions, append a deterministic shard suffix `key + "#" + (random(0..19))` to spread writes across 20 virtual nodes on the ring, aggregating the 20 sub-keys on read (scatter-gather).

---

## Additional Notes

### Seminal Papers & Video References

* [Amazon Dynamo Paper (DeCandia et al., SOSP 2007)](https://www.allthingsdistributed.com/files/amazon-dynamo-sosp2007.pdf)
* [![Design a Distributed Key-Value Store](https://img.youtube.com/vi/rnZmdmlR-2M/0.jpg)](https://www.youtube.com/watch?v=rnZmdmlR-2M)
