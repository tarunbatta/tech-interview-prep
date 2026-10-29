# Twitter System Design

[← Back to System Design](system-design.md)

## Overview

Twitter (now X) is a real-time microblogging and social broadcast platform where users post short messages (tweets up to 280 characters), follow other users, interact via retweets, likes, and replies, and discover real-time breaking news through trending topics and full-text search.

The system is famously known for pioneering large-scale real-time timeline fan-out architectures, the **Snowflake** distributed unique ID generation system, real-time search indexing (**Earlybird**), and sliding-window trending topic algorithms.

---

## Functional Requirements

* **Post Tweet**: Users can publish short text updates (up to 280 characters), optionally attaching photos, video clips, mentions (`@username`), and hashtags (`#topic`).
* **Home Timeline**: Users view an aggregated, real-time feed of tweets from accounts they follow.
* **User Timeline**: Users can view their own past tweets and retweets in reverse chronological order.
* **Social Graph**: Users can follow, unfollow, and block other accounts.
* **Retweet & Quote Tweet**: Re-broadcast another user's tweet to one's own followers, optionally adding commentary.
* **Likes & Replies**: Like tweets and participate in threaded reply conversations.
* **Search & Trends**: Search tweets by keyword/hashtag; display real-time trending topics computed across geographic regions.

---

## Non-Functional Requirements

* **Ultra-Low Read Latency**: Home timeline retrieval must return within **$< 100\text{ ms}$** (P95).
* **Extreme Read-Heavy Scalability**: System must support a **$50:1$ to $100:1$ read-to-write ratio**.
* **High Availability (99.99%)**: Timeline reads must remain available; degraded recommendations or stale timelines are preferred over outages.
* **Eventual Consistency**: A new tweet should appear in followers' home timelines within **$< 3–5\text{ seconds}$**.
* **Unique & Monotonically Increasing IDs**: Every tweet must have a time-sortable 64-bit identifier to avoid cross-shard sorting bottlenecks.

---

## Back of the Envelope Estimations

### Scale & Traffic

* **Daily Active Users (DAU)**: $250\text{ Million}$.
* **New Tweets per Day**: $500\text{ Million tweets/day}$ (average 2 tweets/user/day).
* **Average Tweet Write QPS**:
  $$\text{Write QPS} = \frac{500,000,000}{86,400} \approx 5,800\text{ writes/sec} \quad (\text{Peak: } \approx 15,000\text{ QPS})$$
* **Timeline Reads per User**: $20\text{ timeline views/day}$.
* **Total Daily Timeline Views**:
  $$250,000,000 \times 20 = 5,000,000,000\text{ views/day}$$
* **Average Timeline Read QPS**:
  $$\text{Read QPS} = \frac{5,000,000,000}{86,400} \approx 58,000\text{ QPS} \quad (\text{Peak: } \approx 150,000\text{ QPS})$$

### Storage Requirements

* **Tweet Text & Metadata**: $\approx 300\text{ bytes}$ per tweet.
  $$500,000,000 \times 300\text{ bytes} \approx 150\text{ GB/day} \implies \approx 55\text{ TB/year}$$
* **Media Storage**:
  * $20\%$ of tweets include an image ($\approx 1\text{ MB}$).
  * $500\text{M} \times 0.20 \times 1\text{ MB} = 100\text{ TB/day} \implies \approx 36.5\text{ PB/year}$.
* **Redis Timeline Cache**:
  * Cache top 800 tweet IDs for all 250M active users:
  $$250,000,000 \times 800 \times 8\text{ bytes} \approx 1.6\text{ Terabytes of RAM}$$
  *(Distributed across a cluster of Redis / Memcached instances).*

---

## System Architecture

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 25, 'rankSpacing': 45, 'padding': 10}}}%%
flowchart LR
    Client["Client App (iOS / Android / Web)"]
    subgraph EdgeTier["Edge & Ingress"]
        Gateway["API Gateway / Envoy"]
    end
    subgraph AppBackend["Application Services & Workers"]
        WriteSvc["Tweet Write Service"]
        TimelineSvc["Timeline Read Service"]
        Fanout["Fanout Workers (Hybrid)"]
    end
    subgraph DataLayer["Storage, Cache & Search"]
        Manhattan[("Manhattan DB (KV)")]
        Kafka[["Kafka Event Bus"]]
        RedisTimeline[("Redis Timeline ZSet")]
        Earlybird[("Earlybird RAM Index")]
    end
    Client e1@-->|"1. POST /tweets"| Gateway
    Client e2@-->|"2. GET /timeline"| Gateway
    Gateway e3@-->|"Write"| WriteSvc
    Gateway e4@-->|"Read"| TimelineSvc
    WriteSvc e5@-->|"Persist"| Manhattan
    WriteSvc e6@-->|"Publish"| Kafka
    Kafka e7@-->|"Consume"| Fanout
    Fanout e8@-->|"Push IDs"| RedisTimeline
    Fanout e9@-->|"Index"| Earlybird
    TimelineSvc e10@-->|"Read ZSet"| RedisTimeline

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

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class Client client
    class Gateway edge
    class WriteSvc,TimelineSvc service
    class Fanout worker
    class RedisTimeline,Earlybird cache
    class Manhattan storage
    class Kafka queue

    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style AppBackend fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataLayer fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Core Components & Deep Dives

### 1. The Timeline Fanout Architecture

* **User Timeline**: All tweets authored by a specific user. Fast and simple: stored in a key-value store (Twitter's Manhattan / Cassandra) keyed by `user_id`.
* **Home Timeline**: Aggregated feed of tweets from everyone a user follows.
  * Stored as a **Redis Sorted Set (ZSet)** for each active user, containing up to 800 recent tweet IDs.
* **Hybrid Fan-Out Pipeline**:
  * **Regular Users**: When user posts, fan-out workers query the follower graph (FlockDB) and append the `tweet_id` to each follower's Redis timeline list.
  * **Celebrities / High-Follower Accounts ($> 25,000$ followers)**: Do NOT fan-out on write. When a follower queries their home timeline, the Timeline Service performs an on-the-fly multi-get from the followed celebrities' user timelines and merges them with the follower's Redis cache.

### 2. Distributed Unique ID Generation: Twitter Snowflake

Database auto-increment IDs fail in distributed systems because they require a centralized coordination lock. Twitter designed **Snowflake** to generate 64-bit, time-sortable unique IDs across hundreds of distributed machines without coordination.

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph Snowflake_64Bit_ID["Twitter Snowflake 64-Bit ID Structure"]
        Sign["1 Bit: Sign (0)"]
        Timestamp["41 Bits: Timestamp (ms)"]
        Machine["10 Bits: Machine ID (DC+Worker)"]
        Sequence["12 Bits: Sequence (4,096/ms)"]
    end
    Sign e1@-->|"MSB"| Timestamp
    Timestamp e2@-->|"Bits 22..62"| Machine
    Machine e3@-->|"LSB 0..11"| Sequence

    e1@{ animate: true }
    e2@{ animate: true }
    e3@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff

    class Sign client
    class Timestamp edge
    class Machine service
    class Sequence worker

    style Snowflake_64Bit_ID fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
```

* **Sign bit (1 bit)**: Always 0 (keeps integer positive).
* **Timestamp (41 bits)**: Milliseconds since a custom epoch (e.g., Nov 4, 2010). Gives $2^{41} \approx 69\text{ years}$ of unique timestamps.
* **Machine ID (10 bits)**: 5 bits Datacenter ID + 5 bits Worker ID ($2^{10} = 1,024\text{ distinct worker nodes}$).
* **Sequence Number (12 bits)**: Allows $2^{12} = 4,096\text{ unique IDs per millisecond per machine}$.
* **Key Advantage**: IDs are naturally sorted chronologically, allowing pagination queries (`WHERE id < :cursor LIMIT 20`) without secondary index sorting!

### 3. Real-Time Search Indexing (Earlybird)

* Unlike Google Web Search where batch indexes are built over hours, Twitter search must surface breaking news **seconds** after publication.
* **Earlybird Architecture**: Inverted index held entirely in RAM.
  * Incoming tweets flow from Kafka into Earlybird worker memory.
  * Earlybird creates an append-only in-memory posting list without locks.
  * A search query broadcasts to all Earlybird shards; results are scored, merged, and returned in $< 50\text{ ms}$.

### 4. Real-Time Trending Topics

* **Sliding Window Counting**: Streaming processors (Apache Flink / Heron) compute word frequencies over sliding time windows (e.g., 30 minutes).
* **Count-Min Sketch**: Space-efficient probabilistic data structure that tracks frequencies of millions of hashtags with sublinear memory.
* **Z-Score Spike Detection**: Identifies sudden surges in hashtag velocity compared to normal baseline traffic, filtering out bot spam and recurring words.

---

## API Endpoints

### 1. Post a Tweet

```http
POST /api/v1/tweets
Authorization: Bearer <user_token>
Content-Type: application/json
```

```json
// Request
{
  "text": "Excited to launch our distributed timeline engine! #systems #tech",
  "mediaIds": ["med_10284019"]
}

// Response: 201 Created
{
  "tweetId": 1490284729102840832,
  "authorId": 50129,
  "text": "Excited to launch our distributed timeline engine! #systems #tech",
  "createdAt": 1730005200,
  "likeCount": 0,
  "retweetCount": 0
}
```

### 2. Get Home Timeline

```http
GET /api/v1/timeline/home?count=20&max_id=1490284729102840000
Authorization: Bearer <user_token>
```

```json
// Response: 200 OK
{
  "tweets": [
    {
      "tweetId": 1490284729102840832,
      "author": {
        "userId": 50129,
        "screenName": "tarunbatta",
        "verified": true
      },
      "text": "Excited to launch our distributed timeline engine! #systems #tech",
      "likeCount": 420,
      "retweetCount": 85,
      "createdAt": 1730005200
    }
  ],
  "nextCursor": 1490284729102830000
}
```

---

## Object Model & Database Schema

### Database Selection & Rationale

| Workload / Subsystem | Database Choice | Data Model & CAP Profile | Why This Database? (Justification & Trade-offs) |
| :--- | :--- | :--- | :--- |
| **Tweets & User Timelines (`TWEET`, `USER`)** | **Twitter Manhattan / Apache Cassandra** | Distributed Wide-Column Key-Value (**AP / Tunable Consistency**) | **Why:** Ingesting $500\text{M tweets/day}$ ($15,000\text{ Peak Write QPS}$) and serving author timelines requires horizontal write scalability and time-ordered range scans. Keying by `authorId` with 64-bit time-sortable **Snowflake** `tweetId` as the clustering key stores a user's tweets contiguously on disk in chronological order, making `User Timeline` queries and cursor pagination single-partition sequential reads. |
| **Home Timelines (Fanout Cache)** | **Redis Cluster (Sorted Sets / Native Lists)** | In-Memory Key-Value (**AP / Sub-ms**) | **Why:** Serving $150,000\text{ Peak Read QPS}$ for Home Timelines in $< 100\text{ ms}$ is only possible from RAM. Each active user's Home Timeline is a Redis Sorted Set (`ZSET`) capped at 800 `tweetId`s (8-byte Snowflake IDs + sender IDs $\approx 1.6\text{ TB}$ cluster RAM), populated on write for regular accounts ($< 25\text{K}$ followers) and merged on read with celebrity timelines from Manhattan. |
| **Social Graph (`FOLLOWER` Adjacency Lists)** | **FlockDB (Sharded MySQL + Gizzard) / Cassandra** | Distributed Graph Adjacency Store (**AP / Idempotent Set Ops**) | **Why:** Twitter's social graph requires high-throughput set intersections ("who do both Alice and Bob follow?"), paginated adjacency lookups (`followers_of` for fan-out), and out-of-order idempotent follow/unfollow writes—optimized via forward (`followeeId` $\to$ `followerId`) and backward (`followerId` $\to$ `followeeId`) B-Tree/LSM shards rather than deep multi-hop graph traversal engines. |
| **Real-Time Full-Text Search (Tweets)** | **Earlybird (Custom In-Memory Lucene Inverted Index)** | In-Memory Inverted Index (**AP / Sub-Second Ingestion**) | **Why:** Traditional disk-based Lucene/Elasticsearch segment merges take seconds, which is too slow during breaking news. Earlybird holds lock-free append-only posting lists in RAM so a newly published tweet is searchable globally within $< 1\text{ second}$. |

```mermaid
erDiagram
    USER ||--o{ TWEET : "authors"
    USER ||--o{ FOLLOWER : "has_followers"
    USER {
        bigint userId PK
        varchar_50 username
        varchar_100 screenName
        int followerCount
        int followingCount
        boolean isCelebrity
    }
    TWEET {
        bigint tweetId PK
        bigint authorId FK
        varchar_280 text
        bigint inReplyToTweetId FK
        int retweetCount
        int likeCount
        timestamp createdAt
    }
    FOLLOWER {
        bigint followeeId PK,FK
        bigint followerId PK,FK
        timestamp createdAt
    }
```

---

## System Design Diagram

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph ClientTier["Clients"]
        Client["Client App (iOS / Android / Web)"]
    end
    subgraph EdgeTier["Edge & ID Generation"]
        Gateway{"API Gateway / Envoy (Auth & Limit)"}
        Snowflake["Snowflake 64-Bit ID Generator"]
    end
    subgraph CoreServices["Core Microservices & Workers"]
        TweetWriteSvc["Tweet Write Service"]
        TimelineReadSvc["Timeline Read Svc (Hybrid Merge)"]
        FanoutSvc["Fanout Service (Skips >25K)"]
        Earlybird["Earlybird Search (RAM Index)"]
        TrendingSvc["Trending Engine (Flink + Sketch)"]
    end
    subgraph DataAndEventTier["Data Stores, Caches & Event Bus"]
        ManhattanDB[("Manhattan DB (User Timelines)")]
        RedisTimeline[("Redis Timeline ZSet (800 IDs)")]
        FlockDB[("FlockDB Social Graph")]
        Kafka[["Kafka Event Bus (tweets)"]]
    end
    Client e1@-->|"REST / GraphQL"| Gateway
    Gateway e2@-->|"Next 64b ID"| Snowflake
    Gateway e3@-->|"POST /tweets"| TweetWriteSvc
    Gateway e4@-->|"GET /timeline"| TimelineReadSvc
    Gateway e5@-->|"GET /search"| Earlybird
    TweetWriteSvc e6@-->|"Save Tweet"| ManhattanDB
    TweetWriteSvc e7@-->|"Publish Event"| Kafka
    Kafka e8@-->|"Consume"| FanoutSvc
    Kafka e9@-->|"Index RAM"| Earlybird
    Kafka e10@-->|"Count Tags"| TrendingSvc
    FanoutSvc e11@-->|"Query Graph"| FlockDB
    FanoutSvc e12@-->|"Push ZSet"| RedisTimeline
    TimelineReadSvc e13@-->|"1. Read ZSet"| RedisTimeline
    TimelineReadSvc e14@-->|"2. Merge Celebs"| ManhattanDB

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
    class Gateway,Snowflake edge
    class TweetWriteSvc,TimelineReadSvc,Earlybird service
    class FanoutSvc,TrendingSvc worker
    class RedisTimeline cache
    class ManhattanDB,FlockDB storage
    class Kafka queue

    style ClientTier fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style CoreServices fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataAndEventTier fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Additional Notes

### Video References

* [![Twitter System Design Video 1](https://img.youtube.com/vi/Nfa-uUHuFHg/0.jpg)](https://www.youtube.com/watch?v=Nfa-uUHuFHg)
* [![Twitter System Design Video 2](https://img.youtube.com/vi/Fy_Vo45P9xM/0.jpg)](https://www.youtube.com/watch?v=Fy_Vo45P9xM)
* [![Twitter System Design Video 3](https://img.youtube.com/vi/gbysuvl2TZo/0.jpg)](https://www.youtube.com/watch?v=gbysuvl2TZo)
