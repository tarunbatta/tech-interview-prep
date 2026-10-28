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
flowchart TD
    Client["Client Apps (50ms Debounced Keystroke, Local Browser Cache)"]
    subgraph RealtimePath["Real-Time Query Path"]
        GW["API Gateway / Envoy"]
        SuggestSvc["Suggestion Service"]
        TrieCluster[("In-Memory Trie Cluster (Precomputed Top-5 Nodes)")]
    end
    subgraph DataPipeline["Data Collection Pipeline"]
        LogCollector["Search Log Collector"]
        Kafka[["Apache Kafka Logs"]]
        Spark["Apache Spark Aggregation (Weekly Frequency Counts)"]
        TrieBuilder["Trie Builder Worker Fleet (Serializes Snapshot)"]
    end
    Client -->|"1. GET /suggest?q=sys"| GW
    GW -->|"Route Query"| SuggestSvc
    SuggestSvc -->|"Lookup Prefix"| TrieCluster
    Client -->|"2. Submit Search"| LogCollector
    LogCollector -->|"Append Query Event"| Kafka
    Kafka -->|"Stream Logs"| Spark
    Spark -->|"Aggregated Frequencies"| TrieBuilder
    TrieBuilder -.->|"Swap Snapshot"| TrieCluster
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
flowchart TD
    Root(("Root"))
    NodeT["'t'"]
    NodeE["'e'"]
    NodeC["'c'"]
    NodeS["'s' (Top-5: system design, sql, spotify...)"]
    NodeY["'y' (Top-5: system design, systemctl, sysadmin...)"]
    NodeSys["'s' (Top-5: system design, sysadmin, systemctl)"]
    Root -->|"t"| NodeT
    NodeT -->|"e"| NodeE
    NodeE -->|"c"| NodeC
    Root -->|"s"| NodeS
    NodeS -->|"y"| NodeY
    NodeY -->|"s"| NodeSys
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
flowchart TD
    subgraph ClientAndEdge["Client & Edge Caching Tier"]
        Client["Client Browser / App (50ms Debounced Keystroke & Local LRU Cache)"]
        CDN["Global Edge CDN / PoP (Caches Top Popular Prefixes: 'a', 'b', 'c', 'fac', 'you')"]
    end
    subgraph GatewayTier["Gateway & Routing Tier"]
        GW{"API Gateway / Load Balancer (SSL Termination, Rate Limiting, Prefix Routing)"}
    end
    subgraph RealtimeServing["Real-Time Serving Path (< 20ms Latency)"]
        SuggestSvc["Suggestion Service (Queries In-Memory Shards by Consistent Hash of Prefix)"]
        TrieCache[("In-Memory Trie Cluster (Precomputed Top-K at Each Node, O(L) Traversal)")]
        TrendingCache[("Auxiliary Redis Cache (Fast-Trending Breaking News Overlay)")]
        FilterSvc["Filter & Personalization Svc (Profanity / DMCA Blocklist & Geo/User Context)"]
    end
    subgraph OfflinePipeline["Data Collection & Trie Rebuild Pipeline (Batch & Streaming)"]
        LogSvc["Search Log Service (Appends User Search Queries)"]
        Kafka[["Kafka Logs Topic"]]
        Flink["Apache Flink Stream Processor (5-min Sliding Window Trending Spikes)"]
        Spark["Apache Spark / MapReduce (Weekly Query Frequency Counts & Exponential Decay)"]
        TrieBuilder["Trie Builder Worker (Compiles Static Trie Snapshots)"]
        S3Snap[("AWS S3 Snapshot Store (Immutable Serialized Trie Images)")]
    end
    Client -->|"Fetch Cached Top Prefix"| CDN
    Client -->|"GET /api/v1/search/suggest?q=sys"| GW
    GW -->|"Forward Query"| SuggestSvc
    SuggestSvc -->|"O(L) Prefix Lookup"| TrieCache
    SuggestSvc -->|"Merge Real-Time Spikes"| TrendingCache
    TrieCache -->|"Raw Top-K Candidates"| FilterSvc
    TrendingCache -->|"Trending Candidates"| FilterSvc
    GW -->|"Async Search Event"| LogSvc
    LogSvc -->|"Publish Search Event"| Kafka
    Kafka -->|"Real-Time Stream"| Flink
    Flink -->|"Inject High-Velocity Terms"| TrendingCache
    Kafka -->|"Batch Ingestion"| Spark
    Spark -->|"Time-Decayed Frequencies"| TrieBuilder
    TrieBuilder -->|"Upload Compiled Snapshot"| S3Snap
    S3Snap -.->|"Weekly Zero-Downtime Pointer Swap"| TrieCache
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
