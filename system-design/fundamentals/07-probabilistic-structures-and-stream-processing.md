# Chapter 7: Probabilistic Data Structures, Gossip Protocols & Stream Processing

[← Back to System Design Hub](../system-design.md) | [← Previous: Chapter 6 — Performance Metrics, Resiliency & Observability](06-resiliency-observability-and-metrics.md) | [Next: Chapter 8 — Security, Multi-Region DR & Deployment Patterns →](08-security-multi-region-and-deployments.md)

---

## Table of Contents

- [1. Probabilistic Data Structures at Scale](#1-probabilistic-data-structures-at-scale)
  - [Overview & Memory Comparison](#overview--memory-comparison)
  - [Bloom Filter & Cuckoo Filter (Set Membership)](#bloom-filter--cuckoo-filter-set-membership)
  - [Count-Min Sketch & The Top-K Heavy Hitters Problem](#count-min-sketch--the-top-k-heavy-hitters-problem)
  - [HyperLogLog (HLL — Cardinality / Unique Counting)](#hyperloglog-hll--cardinality--unique-counting)
  - [Locality-Sensitive Hashing (SimHash & MinHash — Near-Duplicate Detection)](#locality-sensitive-hashing-simhash--minhash--near-duplicate-detection)
- [2. Decentralized Cluster Protocols & Conflict Resolution](#2-decentralized-cluster-protocols--conflict-resolution)
  - [Gossip Protocol (Peer-to-Peer Membership & Failure Detection)](#gossip-protocol-peer-to-peer-membership--failure-detection)
  - [Merkle Trees (Cryptographic Anti-Entropy Sync)](#merkle-trees-cryptographic-anti-entropy-sync)
  - [CRDTs vs. Operational Transformation (OT)](#crdts-vs-operational-transformation-ot)
- [3. Stream Processing & Event-Driven Data Architectures](#3-stream-processing--event-driven-data-architectures)
  - [Batch vs. Micro-Batch vs. True Stream Processing](#batch-vs-micro-batch-vs-true-stream-processing)
  - [Stream Windowing (Tumbling, Sliding & Session Windows)](#stream-windowing-tumbling-sliding--session-windows)
  - [Event Time vs. Processing Time & Watermarks](#event-time-vs-processing-time--watermarks)
  - [CQRS, Event Sourcing & Lambda vs. Kappa Architecture](#cqrs-event-sourcing--lambda-vs-kappa-architecture)

---

## 1. Probabilistic Data Structures at Scale

When dealing with billions of users, URLs, or events, exact data structures (`HashSet`, `HashMap`) require ratusan/hundreds of gigabytes of RAM. **Probabilistic Data Structures** trade a tiny, mathematically bounded error margin (`< 1%`) for **$100\times–10,000\times$ memory reduction**.

### Overview & Memory Comparison

| Problem | Exact Data Structure (for $10^9$ items) | Probabilistic Structure (for $10^9$ items) | Error Trade-Off | Classic Interview Use Cases |
| :--- | :--- | :--- | :--- | :--- |
| **Set Membership** (*"Have we seen this URL/key before?"*) | `HashSet<string>` (`~32–64 GB` RAM) | **Bloom Filter** (`~1.2 GB` RAM at 1% FP rate; ~10 bits/item) | **0% False Negatives**; tunable ~1% **False Positives**. | Skipping LSM-Tree SSTables on disk, preventing Cache Penetration, [Web Crawler](../webcrawler.md) URL deduplication. |
| **Frequency Estimation & Top-K** (*"What are the top 100 trending hashtags?"*) | `HashMap<string, long>` (`~32+ GB` RAM) | **Count-Min Sketch + Min-Heap** (`~few MBs` of fixed RAM) | Never underestimates count; slight **overestimation** possible on hash collisions. | Trending topics ([Twitter](../twitter.md)), viral videos ([TikTok](../tiktok.md)), DDoS heavy-hitter IP detection. |
| **Cardinality Estimation** (*"How many unique users viewed this post?"*) | `HashSet<user_id>` (`~8–16 GB` RAM per post!) | **HyperLogLog (`PFADD` / `PFCOUNT`)** (**`12 KB`** fixed RAM!) | `~0.81%` standard error across up to $2^{64}$ unique elements. | Unique daily active users (DAU), unique ad reach, unique article/video viewers ([YouTube](../youtube.md)). |
| **Near-Duplicate Similarity** (*"Is this web page 95% identical to an existing page?"*) | Pairwise string diff $O(N^2)$ (Impossible at scale) | **SimHash / MinHash** (`64-bit` fingerprint per document) | Approximate Cosine / Jaccard similarity via Hamming distance $\le 3$. | Web crawler deduplication ([Web Crawler](../webcrawler.md)), plagiarism & copyright detection. |

---

### Bloom Filter & Cuckoo Filter (Set Membership)

- **How a Bloom Filter Works**:
  1. Allocates a bit array of $m$ bits (all initialized to `0`) and $k$ independent hash functions ($h_1, h_2, \dots, h_k$).
  2. **Insert(`item`)**: Computes $h_1(\text{item}), \dots, h_k(\text{item}) \pmod m$ and sets those $k$ bit positions to `1`.
  3. **Lookup(`item`)**: Checks the $k$ bit positions:
     - If **any** bit is `0` $\rightarrow$ the item is **100% definitely NOT in the set** (Zero False Negatives).
     - If **all** $k$ bits are `1` $\rightarrow$ the item is **probably in the set** (may be a False Positive if other items happened to flip those same bits).
- **Limitation & Cuckoo Filter Alternative**:
  - You **cannot delete** an item from a standard Bloom Filter (setting a bit back to `0` would break other items sharing that bit).
  - If deletions are required, use a **Counting Bloom Filter** (4-bit counters per slot) or a **Cuckoo Filter** (stores compact fingerprints in a cuckoo hash table, supporting both $O(1)$ lookups and deletions with better space efficiency at `< 3%` false positive rates).

---

### Count-Min Sketch & The Top-K Heavy Hitters Problem

*Classic Interview Prompt*: *"Design a system that computes the Top 100 Trending Hashtags or Top Viewed Songs over the last 1 hour across 500,000 events/sec."*

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 35, 'padding': 10}}}%%
flowchart LR
    Stream["Event Stream (500K/sec)"]
    CMS["Count-Min Sketch (d x w Counter Matrix)"]
    Heap["Size-K Min-Heap (Top 100 Items)"]

    Stream -->|"1. Hash Item to d Rows & Increment"| CMS
    CMS -->|"2. Est. Count = min(row_1..row_d)"| Heap

    classDef stream fill:#0284c7,stroke:#38bdf8,stroke-width:2px,color:#ffffff
    classDef cms fill:#7c3aed,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef heap fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff

    class Stream stream
    class CMS cms
    class Heap heap
```

1. **Count-Min Sketch (CMS)** maintains a fixed 2D array of $d$ rows (hash functions, e.g., $d=5$) by $w$ columns (buckets, e.g., $w=10,000$).
2. **Update(`hashtag`)**: For each row $i \in [1..d]$, increment `matrix[i][hash_i(hashtag) % w]++`.
3. **EstimateCount(`hashtag`)**: Return the **minimum** value across the $d$ rows: $\min_{i} \text{matrix}[i][\text{hash}_i(\text{hashtag}) \bmod w]$. Because hash collisions only *increase* bucket values, taking the minimum across $d$ independent hashes yields a tight upper-bound estimate.
4. **Maintaining Top-K**: Pair the Count-Min Sketch with a **Min-Heap of size $K$** (e.g., $K=100$). Whenever an item's estimated count from the CMS exceeds the minimum element at the root of the Min-Heap, update/insert it into the Min-Heap in $O(\log K)$ time.

---

### HyperLogLog (HLL — Cardinality / Unique Counting)

- **Intuition**: If you flip a fair coin repeatedly and tell someone *"My longest run of consecutive heads at the start of a sequence was 10 heads (`00000000001...`)"*, they can statistically estimate that you flipped the coin roughly $2^{10} = 1,024$ times!
- **How HLL Works**: Hashes each incoming `user_id` into a uniform 64-bit binary number, splits the stream into $m = 16,384$ ($2^{14}$) sub-streams (using the first 14 bits to avoid single-outlier variance), tracks the maximum number of leading zeros seen in each bucket (6 bits per bucket), and computes the **harmonic mean**.
- **Result**: $16,384 \times 6\text{ bits} = \mathbf{12\text{ KB}}$ of RAM (built into Redis via `PFADD` / `PFCOUNT`) counts billions of unique visitors with `0.81%` error.

---

### Locality-Sensitive Hashing (SimHash & MinHash — Near-Duplicate Detection)

Cryptographic hashes (`SHA-256`) change completely if even a single comma changes in a document. **Locality-Sensitive Hashing (LSH)** is designed so that **similar documents produce similar hashes**:

- **SimHash (Google Web Crawler)**: Tokenizes a web page into features/shingles, weights them, and produces a **64-bit fingerprint**. Two web pages are near-duplicates if the **Hamming Distance** (number of differing bits, computed via a single CPU `XOR` + `POPCNT` instruction) between their 64-bit SimHashes is $\le 3$.
- **MinHash**: Estimates the **Jaccard Similarity** ($\frac{|A \cap B|}{|A \cup B|}$) between two sets of shingles in $O(1)$ space.

---

## 2. Decentralized Cluster Protocols & Conflict Resolution

### Gossip Protocol (Peer-to-Peer Membership & Failure Detection)

In leaderless clusters with hundreds of nodes (**Apache Cassandra, Amazon Dynamo, Consul, Redis Cluster**), there is no central coordinator to track which nodes are alive.

- **How Gossip Works**: Every second, each node randomly selects 1–3 peer nodes and exchanges its local **Heartbeat Generation & Version Table** (`Node A: heartbeat=1042, status=UP`).
- **Epidemic Convergence**: Information spreads like a rumor in an epidemic — reaching all $N$ nodes in **$O(\log N)$ rounds** (for 1,000 nodes, any cluster state change propagates everywhere in ~10 hops) with minimal, bounded network bandwidth.
- **Phi Accrual Failure Detection**: Instead of a hard boolean timeout, Cassandra uses the historical inter-arrival distribution of gossip heartbeats to output a continuous suspicion score ($\phi$), adapting automatically to network jitter.

---

### Merkle Trees (Cryptographic Anti-Entropy Sync)

When two Dynamo/Cassandra replicas recover from a network partition, how do they figure out which keys out of 100 million diverged without sending all 100 million keys over the network?

- Each replica builds a **Merkle Tree** (a binary tree where leaves are hashes of individual key-ranges and parent nodes are hashes of their two children).
- **Sync in $O(\log N)$**: Replica A and Replica B first compare their **Root Hash**.
  - If the Root Hashes match $\rightarrow$ the 100 million keys are 100% identical (done in 1 packet!).
  - If they differ $\rightarrow$ they recursively compare left/right child hashes down the tree, pinpointing the exact diverged key range in $\log_2(N)$ steps.

---

### CRDTs vs. Operational Transformation (OT)

When multiple users concurrently edit the same state without central locking (e.g., collaborative document editing in [Google Docs](../google-docs.md), distributed shopping carts, or global like/view counters):

| Dimension | Operational Transformation (OT) | CRDTs (Conflict-Free Replicated Data Types) |
| :--- | :--- | :--- |
| **Core Mechanism** | Transforms incoming index-based operations (`Insert(pos=5, "a")`) against previously executed concurrent operations using a central ordering server. | Data structures mathematically designed so concurrent updates **commute, associate, and are idempotent** — merging in any order always converges to the exact same state without a central server. |
| **Examples** | Google Docs collaborative text editor. | **G-Counter / PN-Counter** (increment/decrement counters), **LWW-Element-Set** (sets), **RGA / Yjs / Automerge** (collaborative text/JSON), Redis Enterprise Active-Active, Riak. |
| **Trade-Off** | Lightweight wire payloads, but transformation functions become complex and require a central serialization server per document session. | Supports true peer-to-peer / offline-first multi-region merging, at the cost of extra per-element tombstone metadata. |

---

## 3. Stream Processing & Event-Driven Data Architectures

### Batch vs. Micro-Batch vs. True Stream Processing

| Processing Model | Representative Engines | Typical Latency | How It Works & Ideal Use Cases |
| :--- | :--- | :--- | :--- |
| **Batch Processing** | Apache Spark, MapReduce, dbt / Snowflake | `Minutes to Hours` | Processes bounded historical datasets stored on S3/HDFS. Ideal for T+1 financial reconciliation ([Payment System](../payment-system.md)), daily billing reports, and ML model training. |
| **Micro-Batch Processing** | Spark Structured Streaming | `1 – 10 Seconds` | Slices an incoming stream into small time-bounded mini-batches (e.g., every 2 seconds) and runs batch jobs over them. High throughput, moderate latency. |
| **True Event-at-a-Time Stream Processing** | **Apache Flink, Kafka Streams** | **`5 – 100 Milliseconds`** | Processes each event immediately as it arrives using stateful in-memory operators (backed by local RocksDB + async S3 checkpoints). Ideal for real-time fraud velocity scoring ([Fraud Detection](../fraud-detection.md)) and live alerting ([Metrics Monitoring](../metrics-monitoring.md)). |

---

### Stream Windowing (Tumbling, Sliding & Session Windows)

To aggregate an unbounded infinite stream (e.g., *"Count ad clicks per minute"* or *"Calculate 5-minute rolling average payment failure rate"*), stream processors group events into **Windows**:

1. **Tumbling Window (Fixed, Non-Overlapping)**: Fixed-size contiguous time buckets (e.g., `12:00–12:01`, `12:01–12:02`). Every event belongs to exactly **1** window. *Used for: per-minute metric rollups.*
2. **Sliding / Hopping Window (Fixed Size, Overlapping)**: A window of length $W$ (e.g., 5 minutes) that slides forward every hop $S$ (e.g., every 10 seconds). An event belongs to $\frac{W}{S}$ windows. *Used for: smooth moving-average velocity features ([Fraud Detection](../fraud-detection.md)).*
3. **Session Window (Dynamic, Activity-Driven)**: Groups a user's events together as long as the gap between consecutive events is less than an **inactivity timeout** (e.g., 30 minutes of silence closes the session window). *Used for: user engagement analytics and clickstream funnel tracking.*

---

### Event Time vs. Processing Time & Watermarks

- **Event Time**: The timestamp when the action actually occurred on the user's mobile device (`12:00:05`).
- **Processing Time**: The timestamp when the Apache Flink server received and processed the event (`12:03:40` — delayed by 3.5 minutes because the user rode into a subway tunnel).
- **Watermarks & Late Events**:
  - If you aggregate by **Event Time**, how does Flink know when all events for the `12:00–12:01` window have arrived so it can emit the final count?
  - A **Watermark($t$)** is a monotonically advancing threshold asserting: *"The stream processor believes no more events with `event_time < t` will arrive"* (e.g., `max_seen_event_time - 30s allowed lateness`).
  - Events arriving *after* the watermark has passed are routed to a **Side Output** to incrementally update the stored aggregate in the database without blocking real-time window emission.

---

### CQRS, Event Sourcing & Lambda vs. Kappa Architecture

1. **CQRS (Command Query Responsibility Segregation)**:
   - Separates the **Write Model (Command)** — normalized ACID PostgreSQL optimized for transactional integrity — from the **Read Model (Query)** — denormalized Elasticsearch / Redis / Cassandra views updated asynchronously via Kafka CDC events.
2. **Event Sourcing**:
   - Instead of overwriting the current state in place (`UPDATE accounts SET balance = 150`), store an **immutable append-only sequence of domain events** (`AccountCreated($0)`, `Deposited($200)`, `Withdrew($50)`).
   - Current state is derived by replaying events (accelerated via periodic **State Snapshots**). Provides a 100% tamper-proof audit log and time-travel debugging ([Payment System](../payment-system.md)).
3. **Lambda Architecture vs. Kappa Architecture**:
   - **Lambda Architecture**: Runs two separate pipelines in parallel — a **Speed Layer** (Flink/Storm for approximate low-latency real-time views) and a **Batch Layer** (Spark/Hadoop recomputing exact historical truth overnight) merged in a Serving Layer. *Downside: requires maintaining duplicate business logic in two different codebases.*
   - **Kappa Architecture**: Eliminates the separate Batch Layer by treating everything as a **single stream processing pipeline (Flink)** over a long-retention replayable log (Kafka + Tiered S3 Storage). When logic changes, simply spin up a parallel Flink job and replay the Kafka log from offset `0`.

---

[← Back to System Design Hub](../system-design.md) | [← Previous: Chapter 6 — Performance Metrics, Resiliency & Observability](06-resiliency-observability-and-metrics.md) | [Next: Chapter 8 — Security, Multi-Region DR & Deployment Patterns →](08-security-multi-region-and-deployments.md)
