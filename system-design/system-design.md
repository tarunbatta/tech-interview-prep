# System Design Documentation

[← Back to Main README](../README.md)

## Real-World Architectural Retrospectives

* [🌟 **DocuSign Billing & Invoicing Modernization**](project-retrospective.md) — Production architecture retrospective: Migrating legacy Quartz batch systems to event-driven MuleSoft & Core microservices, reducing invoice latency from 7 days to real-time (< 2s), achieving a 90% reduction in reconciliation discrepancies, and an 80% DSO reduction.

## Interview System Design Case Studies (28 Systems)

1. [Airbnb](airbnb.md)
2. [Amazon Prime](amazon-prime.md)
3. [Amazon](amazon.md)
4. [ATM](atm.md)
5. [Autocomplete Search](autocomplete-search.md)
6. [Bitly](bitly.md)
7. [DocuSign](docusign.md)
8. [Dropbox](dropbox.md)
9. [Email Archive](email-archive.md)
10. [Google Docs](google-docs.md)
11. [Google Drive](google-drive.md)
12. [Instagram](instagram.md)
13. [Netflix](netflix.md)
14. [News Feed](news-feed.md)
15. [Notification System](notification-system.md)
16. [Parking Garage](parking-garage.md)
17. [PasteBin](pastebin.md)
18. [Rate Limiter](rate-limiter.md)
19. [Shopify](shopify.md)
20. [Social Media](social-media.md)
21. [Tag Service](tag-service.md)
22. [TicketMaster](ticketmaster.md)
23. [TikTok](tiktok.md)
24. [Twitter](twitter.md)
25. [Uber](uber.md)
26. [Web Crawler](webcrawler.md)
27. [Whatsapp](whatsapp.md)
28. [YouTube](youtube.md)

## Distributed Systems Fundamentals: CAP & PACELC Theorem

In distributed systems, trade-offs between consistency, availability, and latency are inevitable. Understanding these models is essential during system design interviews when choosing databases, replication strategies, and caching layers.

### CAP Theorem Overview

Formulated by Eric Brewer and proved by Gilbert & Lynch, the CAP theorem states that a distributed data store can guarantee at most **two out of three** properties:

| Property | Definition | Interview Reality |
| :--- | :--- | :--- |
| **Consistency (C)** | **Linearizability / Single-Copy Consistency**: Every read receives the most recent write or an error. All nodes see identical data simultaneously. | Note: This refers to *linearizability* in distributed systems, not ACID's "C" (database schema invariants/integrity). |
| **Availability (A)** | Every non-failing node returns a non-error response for every received request (without guaranteeing it contains the most recent write). | Does not mean high uptime percentage (e.g., 99.99%); it means non-blocking responses without explicit error. |
| **Partition Tolerance (P)** | The system continues to operate despite arbitrary dropped, delayed, or duplicated messages across network partitions. | **Non-negotiable** in distributed systems; networks will experience partitions, packet drops, or hardware links going down. |

> [!IMPORTANT]
> **Why "CA" is not a valid choice in distributed systems:**
> Network partitions ($P$) cannot be avoided across distributed networks. Therefore, when a network partition occurs, a system must choose between:
>
> 1. **Cancel the operation / return an error** to preserve consistency $\rightarrow$ **CP**
> 2. **Proceed with the operation** accepting stale or divergent reads/writes $\rightarrow$ **AP**
>
> "CA" only exists in single-node systems (e.g., traditional single-instance RDBMS) where network partitions between data replicas cannot happen.

---

### CP vs. AP Systems Comparison

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 25, 'rankSpacing': 45, 'padding': 10}}}%%
flowchart LR
    P["Network Partition (P) Occurs"]
    subgraph Choices["CAP Trade-Off During Partition"]
        CP["Consistency (CP): Rejects minority writes (Spanner, etcd, ZooKeeper)"]
        AP["Availability (AP): Accepts divergent writes (Cassandra, DynamoDB, Riak)"]
    end
    P e1@-->|"Preserve Linearizability"| CP
    P e2@-->|"Maximize Uptime"| AP

    e1@{ animate: true }
    e2@{ animate: true }

    classDef partition fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef cp fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef ap fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff

    class P partition
    class CP cp
    class AP ap

    style Choices fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
```

| Dimension | CP (Consistency + Partition Tolerance) | AP (Availability + Partition Tolerance) |
| :--- | :--- | :--- |
| **Primary Goal** | Data correctness and linearizability. | Maximum availability and minimal downtime. |
| **Partition Behavior** | Nodes on minority partitions refuse writes/reads to prevent stale/split-brain states. | All partitions accept reads/writes; data diverges and syncs later. |
| **Consistency Model** | Strong Consistency (Sequential / Linearizable). | Eventual Consistency / Causal Consistency / Read-Your-Writes. |
| **Conflict Handling** | Prevented up-front using distributed consensus (Raft, Paxos, 2PC). | Resolved asynchronously (CRDTs, Last-Write-Wins, Vector Clocks). |
| **Typical Use Cases** | Banking, payment gateways, ledger balances, distributed locks, inventory reservation. | Social feeds, comments/likes, product catalogs, shopping carts, metrics collection. |

---

### The PACELC Theorem (Modern Extension)

The CAP theorem only describes behavior **during network partitions** ($P$). However, distributed systems run under normal operating conditions most of the time. Daniel Abadi created **PACELC** to describe trade-offs under both states:

$$\text{If } \mathbf{P} \text{ (Partition) } \rightarrow \text{choose between } \mathbf{A} \text{ and } \mathbf{C}; \quad \mathbf{E} \text{lse (Normal Operation) } \rightarrow \text{choose between } \mathbf{L} \text{ (Latency) and } \mathbf{C} \text{ (Consistency)}$$

| Classification | Meaning | Database Examples |
| :--- | :--- | :--- |
| **PA / EL** | During partition: choose **Availability**; Else (normal): choose **Latency** (async replication). | Cassandra, DynamoDB, CouchDB |
| **PC / EC** | During partition: choose **Consistency**; Else (normal): choose **Consistency** (sync replication / consensus). | Google Spanner, Bigtable, HBase, etcd |
| **PA / EC** | During partition: choose **Availability**; Else (normal): choose **Consistency**. | MongoDB (configured with primary writes and replica sync) |
| **PC / EL** | During partition: choose **Consistency**; Else (normal): choose **Latency**. | Megastore, VoltDB / H-Store |

---

### Interview Cheatsheet: Making the Right Trade-Off

When designing a system in an interview:

1. **Clarify Business Requirements First**:
   * *"Does a user need to see their own updates immediately?"* $\rightarrow$ Read-Your-Writes consistency.
   * *"Is double-spending or overselling acceptable?"* $\rightarrow$ Strong Consistency (CP).
   * *"Can users tolerate seeing 10-second-old comments or views?"* $\rightarrow$ Eventual Consistency (AP).
2. **Hybrid Architectural Patterns**:
   * **Dual-path architecture**: Use CP datastores for transactions/checkout/orders and AP datastores for product search, recommendations, and analytics.
   * **Tunable consistency**: Many modern distributed databases (e.g., Cassandra, DynamoDB) allow tuning consistency per operation using quorum read/write levels:
     $$R + W > N$$
     *(Where $N$ = replication factor, $W$ = write ack count, $R$ = read replica count. If $R + W > N$, you guarantee strong consistency; otherwise eventual consistency).*

---

### Database Selection Cheat Sheet (When to Choose Which DB)

| Database Category | Representative Engines | CAP / PACELC | Ideal Access Patterns & Why to Choose | Systems Using It |
| :--- | :--- | :--- | :--- | :--- |
| **Relational (RDBMS / Sharded SQL)** | PostgreSQL, MySQL, Amazon Aurora, Vitess | **CP** (`PC / EC`) | Structured schemas, multi-row ACID transactions, foreign-key integrity, and financial/inventory ledgers where double-spending or double-booking is unacceptable. | [Airbnb](airbnb.md), [Amazon](amazon.md), [DocuSign](docusign.md), [Instagram](instagram.md), [Shopify](shopify.md), [YouTube](youtube.md) |
| **Distributed SQL (NewSQL)** | Google Cloud Spanner, CockroachDB | **CP** (`PC / EC`) | Global multi-region active-active writes requiring external consistency / `SERIALIZABLE` ACID isolation without manual application-level sharding. | [ATM](atm.md), [Google Docs](google-docs.md), [Google Drive](google-drive.md), [TicketMaster](ticketmaster.md), [Uber](uber.md) |
| **Wide-Column / LSM-Tree NoSQL** | Apache Cassandra, ScyllaDB, Google Bigtable | **AP** (`PA / EL`) | Massive append-heavy write velocity (millions of writes/sec), time-series/ordered range scans by `(partition_key, clustering_key)`, and zero single point of failure. | [Google Docs](google-docs.md), [Netflix](netflix.md), [Notification System](notification-system.md), [Social Media](social-media.md), [TikTok](tiktok.md), [Twitter](twitter.md), [Whatsapp](whatsapp.md), [YouTube](youtube.md) |
| **Key-Value / Document NoSQL** | Amazon DynamoDB, MongoDB, RocksDB | **AP** (`PA / EL`) | Predictable single-digit millisecond point lookups (`GET/PUT` by primary key), flexible JSON schemas, and native item TTL expiration at any scale. | [Amazon Prime](amazon-prime.md), [Amazon](amazon.md), [Bitly](bitly.md), [PasteBin](pastebin.md), [Web Crawler](webcrawler.md) |
| **In-Memory Cache & Data Structures** | Redis Cluster, Memcached (EVCache) | **AP / CP-Primary** | Sub-millisecond hot reads, sorted-set timelines (`ZSET`), availability bitmaps, geospatial indexes (`H3`/`GEOADD`), and atomic Lua distributed locks/rate-limiters. | All 28 Systems ([Rate Limiter](rate-limiter.md), [News Feed](news-feed.md), [Autocomplete Search](autocomplete-search.md), [Uber](uber.md), etc.) |
| **Search & Inverted Index** | Elasticsearch, OpenSearch, Apache Lucene (Earlybird) | **AP** (`PA / EL`) | Full-text keyword search, typo tolerance, faceted filtering, boolean Roaring Bitmap intersections, and `geo_point` bounding-box queries. | [Airbnb](airbnb.md), [Amazon](amazon.md), [Email Archive](email-archive.md), [Google Drive](google-drive.md), [Tag Service](tag-service.md), [Twitter](twitter.md) |
| **Graph & Relation Tuple Stores** | Amazon Neptune, Neo4j, Meta TAO, Google Zanzibar | **AP / CP** | Highly connected many-to-many relationships, social follower adjacency lists, scene/actor knowledge graphs, and transitive hierarchical ACL checks. | [Amazon Prime](amazon-prime.md), [Google Drive](google-drive.md), [Instagram](instagram.md), [News Feed](news-feed.md), [Social Media](social-media.md) |
| **Vector Databases** | Milvus, FAISS, Pinecone | **AP** (`PA / EL`) | High-dimensional Approximate Nearest Neighbor (ANN / HNSW) embedding similarity search for real-time ML candidate retrieval. | [TikTok](tiktok.md) |
| **Columnar OLAP & Time-Series** | ClickHouse, Google BigQuery, TimescaleDB | **AP** (Analytical) | High-compression append-only event streams and sub-second `GROUP BY` aggregations over billions of click, telemetry, or billing rows. | [Bitly](bitly.md), [Parking Garage](parking-garage.md), [Rate Limiter](rate-limiter.md), [Shopify](shopify.md) |
| **Object / Blob & WORM Ledger** | AWS S3, Google Colossus, Magic Pocket, Amazon QLDB | **11 9s Durability** | Immutable binary media (photos, HLS/DASH video chunks, PDFs, 4MB blocks) and SEC/court-compliant Write-Once-Read-Many (WORM) cryptographic audit trails. | [ATM](atm.md), [DocuSign](docusign.md), [Dropbox](dropbox.md), [Email Archive](email-archive.md), [Netflix](netflix.md), [YouTube](youtube.md) |

## Easy to remember numbers for back-of-the-envelope calculations in system design interviews

### Powers of two table

```text
Power    Approx Value (Bytes)       Bytes
-----------------------------------------
10                 1 thousand        1 KB
16                65 thousand       64 KB
20                  1 million        1 MB
30                  1 billion        1 GB
32                  4 billion        4 GB
40                 1 trillion        1 TB
50              1 quadrillion        1 PB
```

### Bytes to store different data types in SQL

```text
Data type              Bytes
-----------------------------
ASCII character             1
UNICODE character           2

Bytes calculation for string of length x
-----------------------------------------
(x*(bytes to store the char)*(8 bits)) / 8 bits


https://dev.mysql.com/doc/refman/8.0/en/storage-requirements.html
```

### Latency numbers every programmer should know in seconds

In a system design discussion, we normally discuss numbers in seconds or using relative comparisons.
I doubt we are expected to know the exact numbers so, I created a cheat sheet that would be easy to remember and includes references to newer technology.

```text
Latency Comparison Numbers
--------------------------
L1 cache can be accessed 14x faster than L2 cache.
Main memory access is 20x slower than L2, 200x slower than L1.

Zippy Compression at 250 MB/sec.
Send data over 1 Gbps network at 100 MB/sec.
Read sequentially from 1 Gbps Ethernet at 100 MB/sec.
Read randomly/sequentially from SSD at ~ 1 GB/sec.
Read sequentially from main memory at 4 GB/sec.
Read sequentially from disk at 30 MB/sec.
Disk seeks at 100 seeks/sec.
Round-trips within a data center at 2,000 trips/sec.
World-wide round trips at 6-7 trips/sec.

1MB sequential read from memory --> 250 micro seconds
1MB sequential read from ssd    --> 4x of memory
1MB sequential read from disk   --> 80x of memory
```

### Handy numbers for requests calculations

```text
Numbers for requests calculations
---------------------------------
2.5 million seconds per month

1 million reqs/month   -->  .4 reqs/sec
2.6 million reqs/month -->   1 reqs/sec
5 million reqs/month   -->   2 reqs/sec
10 million reqs/month  -->   4 reqs/sec
100 million reqs/month -->  40 reqs/sec
1 billion reqs/month   --> 400 reqs/sec
```

## References

* [The System Design Primer (donnemartin)](https://github.com/donnemartin/system-design-primer)
* [Brewer's CAP Theorem (Eric Brewer)](https://www.infoq.com/articles/cap-twelve-years-later-how-the-rules-have-changed/)
* [Consistency Tradeoffs in Modern Distributed Database System Design (Daniel Abadi on PACELC)](https://www.cs.umd.edu/~abadi/papers/abadi-pacelc.pdf)
