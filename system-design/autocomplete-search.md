# Autocomplete Search (Typeahead) System Design

[← Back to System Design](system-design.md)

## Overview

An Autocomplete (Typeahead) search system predicts and suggests the top $K$ most relevant, high-frequency search queries in real time as a user types characters into a search bar. The system is a core feature of search engines (Google), e-commerce platforms (Amazon), streaming apps (YouTube, Netflix), and social media platforms (Twitter/X).

The fundamental architectural challenge is delivering responses with **ultra-low latency ($< 20\text{–}30\text{ ms}$)** across hundreds of millions of concurrent users, using specialized prefix data structures (**Trie / Prefix Trees**) with precomputed top-$K$ rankings, memory sharding, and decoupled offline analytics pipelines.

---

## Functional Requirements

* **Real-Time Prefix Suggestions**: Return the top 5 to 10 most relevant search suggestions matching the typed prefix string.
* **Frequency & Trending Ranking**: Rank suggestions by historical query frequency, recency, and real-time trending velocity.
* **Personalization & Context**: Optionally personalize suggestions based on the user's past search history and geographic location.
* **Spam & Content Moderation**: Filter out profanity, hate speech, sensitive personal data, and copyright-infringing terms via dynamic blocklists.

---

## Non-Functional Requirements

* **Ultra-Low Latency**: End-to-end response time must be strictly **$< 20\text{–}30\text{ ms}$** from keypress to UI render.
* **High Availability (99.999%)**: Autocomplete failures must fail gracefully without blocking the main search functionality.
* **Massive Query Throughput**: Handle over **$100,000\text{ queries/second (QPS)}$** at peak globally.
* **Scalable Storage**: Store millions of unique query prefixes in memory with sub-millisecond retrieval.

---

## Back of the Envelope Estimations

### Scale & Traffic

* **Daily Active Users (DAU)**: $100\text{ Million}$.
* **Searches per User per Day**: 10 searches/day $= 1\text{ Billion searches/day}$.
* **Keystrokes per Search**:
  * An average search query has 20 characters (4 words).
  * Assume client debounces keystrokes ($50\text{ ms}$ window), resulting in $\approx 5\text{ autocomplete requests per search}$.
* **Total Autocomplete Requests per Day**:
  $$1,000,000,000\text{ searches} \times 5\text{ requests} = 5,000,000,000\text{ requests/day}$$
* **Average Autocomplete QPS**:
  $$\text{Average QPS} = \frac{5,000,000,000}{86,400} \approx 58,000\text{ QPS} \quad (\text{Peak: } \approx 120,000\text{ QPS})$$

### Memory Sizing for Trie

* **Unique Search Queries Tracked**: $\approx 20\text{ Million queries}$ (after filtering low-frequency queries).
* **Average Query Length**: 20 characters (20 bytes).
* **Precomputed Top-K Cache per Node**:
  * Store top 5 query IDs (each 8 bytes) at each prefix node:
    $$5 \times 8\text{ bytes} = 40\text{ bytes per node}$$
* **Trie Node Overhead**:
  * Pointer map to children + frequency integer $\approx 64\text{ bytes}$.
* **Total Memory for 20M Queries in Trie**:
  $$\approx 20,000,000 \times 100\text{ bytes} \approx 2\text{ Gigabytes}$$
* Even with $10\times$ redundancy, indexes, and sharding overhead, the entire Trie index fits in **$< 25\text{ GB of RAM}$**, enabling 100% in-memory serving!

---

## High-Level System Architecture

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 25, 'rankSpacing': 45, 'padding': 10}}}%%
flowchart LR
    Client["Client Apps (50ms Debounce & Cache)"]
    subgraph EdgeTier["Gateway & Ingestion"]
        GW["API Gateway / Envoy"]
        LogCollector["Search Log Collector"]
    end
    subgraph AppServices["Serving & Aggregation"]
        SuggestSvc["Suggestion Service"]
        Kafka[["Apache Kafka Logs"]]
        Spark["Spark Aggregation"]
    end
    subgraph StorageTier["Trie & Builders"]
        TrieCluster[("In-Memory Trie (Top-5)")]
        TrieBuilder["Trie Builder Fleet"]
    end
    Client e1@-->|"1. GET /suggest"| GW
    GW e2@-->|"Route Query"| SuggestSvc
    SuggestSvc e3@-->|"Lookup Prefix"| TrieCluster
    Client e4@-->|"2. Submit Search"| LogCollector
    LogCollector e5@-->|"Query Event"| Kafka
    Kafka e6@-->|"Stream Logs"| Spark
    Spark e7@-->|"Frequencies"| TrieBuilder
    TrieBuilder e8@-.->|"Swap Snapshot"| TrieCluster

    e1@{ animate: true }
    e2@{ animate: true }
    e3@{ animate: true }
    e4@{ animate: true }
    e5@{ animate: true }
    e6@{ animate: true }
    e7@{ animate: true }
    e8@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class Client client
    class GW,LogCollector edge
    class SuggestSvc service
    class Spark,TrieBuilder worker
    class TrieCluster cache
    class Kafka queue

    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style AppServices fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style StorageTier fill:transparent,stroke:#fb7185,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Core Components & Data Structures

### 1. The Trie Data Structure & Optimization

A naive Trie traverses from the root down to the last character of the prefix, then performs a Depth-First Search (DFS) over all descendant nodes to find the top $K$ queries.

* **Why naive DFS fails**: In the worst case, traversing millions of sub-trees on every keystroke takes $O(V + E)$ time, blowing through the 20ms latency budget.
* **Optimization: Precompute Top-$K$ at Every Node**:
  * Each Trie node stores a list of the top 5 most popular full queries passing through that node.
  * Finding the top suggestions becomes an $O(L)$ operation, where $L$ is the prefix length (e.g., $L \le 20$ operations), completely independent of the total number of words in the dictionary!

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    Root(("Root"))
    NodeT["'t' (Top-5: twitter, tesla...)"]
    NodeE["'e' (Top-5: tech, telegram...)"]
    NodeC["'c' (Top-5: tech interview...)"]
    NodeS["'s' (Top-5: system design, sql...)"]
    NodeY["'y' (Top-5: system design, sys...)"]
    NodeSys["'s' (Top-5: system design, sysadmin)"]
    Root e1@-->|"t"| NodeT
    NodeT e2@-->|"e"| NodeE
    NodeE e3@-->|"c"| NodeC
    Root e4@-->|"s"| NodeS
    NodeS e5@-->|"y"| NodeY
    NodeY e6@-->|"s"| NodeSys

    e1@{ animate: true }
    e2@{ animate: true }
    e3@{ animate: true }
    e4@{ animate: true }
    e5@{ animate: true }
    e6@{ animate: true }

    classDef root fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef branchT fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef branchS fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef target fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff

    class Root root
    class NodeT,NodeE,NodeC branchT
    class NodeS,NodeY branchS
    class NodeSys target
```

### 2. Trie Sharding Strategy

To support 120K QPS and horizontal scalability, the Trie is sharded across multiple machines:

* **First-Character Sharding**: Shard by first letter (e.g., Shard 1: `a–c`, Shard 2: `d–f`...).
  * *Flaw*: Causes severe hotspots (`s` and `t` receive $10\times$ more queries than `x` or `z`).
* **Hash-Based Sharding (Consistent Hashing)**:
  * Route by `hash(prefix) % num_shards`.
  * Suggestion Service routes to the corresponding Trie shard node.
  * Read replicas allow linear scaling of read throughput.

### 3. Decoupled Offline Trie Rebuilding

The Trie is **not** updated synchronously when users search. Search queries are logged asynchronously to Kafka:

1. **Analytics Aggregator (Spark)**: Aggregates query frequencies across rolling 7-day windows, applying exponential time decay to older queries.
2. **Weekly Offline Build**: A dedicated worker builds a fresh immutable Trie snapshot and stores it in S3.
3. **Zero-Downtime Swap**: Trie serving servers download the new snapshot into secondary memory and atomically swap pointers.

---

## API Specifications

### 1. Get Suggestions

```http
GET /api/v1/search/suggest?q=sys&limit=5
```

```json
// Response: 200 OK
{
  "prefix": "sys",
  "suggestions": [
    {"query": "system design interview", "score": 982000},
    {"query": "system design primer", "score": 640000},
    {"query": "systemctl commands", "score": 412000},
    {"query": "sysadmin resume", "score": 215000},
    {"query": "systolic blood pressure", "score": 189000}
  ],
  "latencyMs": 8
}
```

---

## Object Model & Database Schema

### Database Selection & Rationale

| Workload / Subsystem | Database Choice | Data Model & CAP Profile | Why This Database? (Justification & Trade-offs) |
| :--- | :--- | :--- | :--- |
| **Real-Time Prefix Serving (Online Path)** | **Custom In-Memory Prefix Trie Cluster (or Redis Cluster)** | In-Memory Tree / Key-Value (**AP / PA-EL**) | **Why:** Disk-based relational (`LIKE 'sys%'`) or standard B-Tree lookups cannot meet a strict $< 20\text{ ms}$ P99 budget at $120,000\text{ QPS}$. Holding the compressed Trie ($\sim 2\text{--}25\text{ GB}$ for 20M unique queries) entirely in RAM with precomputed Top-5 suggestions at every `TRIE_NODE` turns prefix lookup into a deterministic $O(L)$ pointer/hash lookup ($< 1\text{ ms}$) with zero disk I/O. Sharded by `hash(prefix)`. |
| **Fast-Trending Overlay & Blocklists** | **Redis Cluster (Sorted Sets & Sets)** | In-Memory Key-Value (**AP / Sub-ms**) | **Why:** Because the main Trie is rebuilt offline in batches, breaking news spikes (computed by Apache Flink over 5-minute sliding windows) and real-time DMCA/profanity blocklists are stored in Redis Sorted Sets (`ZSET`) and merged with static Trie results at read time in $< 2\text{ ms}$. |
| **Aggregated Query Frequency Store (Offline)** | **Apache Cassandra / Amazon DynamoDB (or Parquet on S3)** | Wide-Column / Analytical Store (**AP**) | **Why:** Spark aggregation jobs roll up billions of raw Kafka search logs into time-decayed frequency scores (`TOP_K_RECORD`) per query string. Cassandra/S3 handles massive parallel batch writes from Spark and feeds the weekly/hourly Trie Builder fleet. |
| **Immutable Trie Snapshots** | **Amazon S3** | Object Storage (**11 9s Durability**) | **Why:** Stores serialized, versioned binary Trie images generated by the Trie Builder fleet so serving nodes can download new snapshots into secondary memory and perform zero-downtime atomic pointer swaps, or recover instantly after a node restart. |

```mermaid
erDiagram
    TRIE_NODE ||--o{ TRIE_NODE : "children"
    TRIE_NODE ||--o{ TOP_K_RECORD : "caches_top_k"
    TRIE_NODE {
        varchar_64 prefixHash PK
        char_1 charKey
        boolean isEndOfWord
        json childrenMap
    }
    TOP_K_RECORD {
        bigint recordId PK
        varchar_64 prefixHash FK
        varchar_255 queryText
        bigint frequencyScore
        varchar_32 category
    }
```

---

## System Design Diagram

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph ClientTier["Clients"]
        Client["Client Browser / App (50ms Debounce)"]
    end
    subgraph EdgeTier["Edge & API Gateway"]
        CDN["Global Edge CDN (Top Prefixes Cache)"]
        GW{"API Gateway / Envoy (Rate Limit & SSL)"}
    end
    subgraph CoreServices["Real-Time Serving & Workers"]
        SuggestSvc["Suggestion Service (Consistent Hash)"]
        FilterSvc["Filter & Context Svc (DMCA & Geo)"]
        LogSvc["Search Log Service (Async Ingestion)"]
        Flink["Apache Flink (5m Trending Window)"]
        Spark["Spark & Trie Builder (Weekly Decay)"]
    end
    subgraph DataAndEventTier["Caches, Event Bus & Snapshots"]
        TrieCache[("In-Memory Trie (O(L) Top-K Nodes)")]
        TrendingCache[("Redis Cache (Breaking News Overlay)")]
        Kafka[["Kafka Logs Topic (Query Events)"]]
        S3Snap[("AWS S3 Store (Trie Snapshots)")]
    end
    Client e1@-->|"Top Prefixes"| CDN
    Client e2@-->|"GET /suggest"| GW
    GW e3@-->|"Lookup Prefix"| SuggestSvc
    GW e4@-->|"Post-Filter"| FilterSvc
    GW e5@-->|"Async Log"| LogSvc
    SuggestSvc e6@-->|"O(L) Read"| TrieCache
    SuggestSvc e7@-->|"Merge Spikes"| TrendingCache
    FilterSvc e8@-->|"Blocklist"| TrendingCache
    LogSvc e9@-->|"Publish Event"| Kafka
    Flink e10@-->|"Consume Stream"| Kafka
    Flink e11@-->|"Inject Spikes"| TrendingCache
    Spark e12@-->|"Batch Read"| Kafka
    Spark e13@-->|"Write Image"| S3Snap
    S3Snap e14@-.->|"Zero-Downtime Swap"| TrieCache

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
    e13@{ animate: true }
    e14@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class Client client
    class CDN,GW edge
    class SuggestSvc,FilterSvc,LogSvc service
    class Flink,Spark worker
    class TrieCache,TrendingCache cache
    class S3Snap storage
    class Kafka queue

    style ClientTier fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style CoreServices fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataAndEventTier fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Deep Dive & Key Technical Challenges

### 1. Client-Side Optimization (Debouncing & Caching)

* **Debouncing**: The client waits $50\text{ ms}$ after a keypress before sending a request. If the user rapidly types "s-y-s-t-e-m", only 1 or 2 network calls are made instead of 6.
* **Browser LRU Cache**: If a user types "sys", deletes "s" to get "sy", and re-types "sys", the client serves results immediately from local JavaScript memory without network round-trips.

### 2. Fast-Trending Spikes (Breaking News)

* Batch Spark processing takes hours to reflect new queries. If an earthquake or celebrity news occurs, users expect instant autocomplete suggestions within minutes.
* **Real-Time Trending Overlay**: An Apache Flink stream processor monitors high-velocity query spikes over 5-minute sliding windows and injects trending terms directly into an auxiliary Redis cache, which merges with the static Trie results.

---

## Additional Notes

### Video References

* [![Autocomplete Search System Design Video](https://img.youtube.com/vi/us0qySiUsGU/0.jpg)](https://www.youtube.com/watch?v=us0qySiUsGU)
