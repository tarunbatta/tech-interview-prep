# Instagram System Design

[← Back to System Design](system-design.md)

> **Core Building Blocks Used**: [Chapter 2 — Pull CDN with Origin Shield for Media Delivery](fundamentals/02-edge-load-balancing-and-cdn.md#4-content-delivery-network-cdn) · [Chapter 3 — Redis Timeline Caching & Celebrity Hot-Key Mitigation](fundamentals/03-caching-and-scaling.md#caching-failure-modes--interview-mitigations) · [Chapter 4 — Pre-Signed S3 Uploads, Cassandra Feeds & Social Graph](fundamentals/04-databases-sql-vs-nosql.md#block-storage-vs-file-storage-vs-object-blob-storage) · [Chapter 5 — Snowflake 64-Bit Time-Sortable IDs](fundamentals/05-distributed-systems-and-messaging.md#6-distributed-unique-id-generation-snowflake-uuidv7-ticket-servers)

## Overview

Instagram is a global photo and video-centric social network serving over 500 million Daily Active Users (DAU) and 2 billion Monthly Active Users (MAU). The platform specializes in visual content sharing, ephemeral stories (24-hour expiration), short-form algorithmic video reels, user discovery, and direct messaging.

Designing Instagram requires an architecture optimized for massive media storage, ultra-fast global content delivery via CDNs, extreme read-to-write ratios ($100:1$ to $200:1$), efficient follower graph traversal, and resilient feed generation capable of handling celebrity hot-spots without latency degradation.

---

## Functional Requirements

* **Photo & Video Ingestion**: Users can upload high-resolution photos and short videos with captions, filters, location tags, and hashtags.
* **Social Graph (Follow / Unfollow)**: Users can follow or unfollow other accounts, approve follow requests for private profiles, and view followers/following lists.
* **Home Feed Generation**: Chronological and algorithmically ranked feed displaying posts from followed users, along with sponsored and recommended content.
* **User Profile & Grid**: Display user biography, follower counts, and a reverse-chronological grid of their uploaded media.
* **Stories (Ephemeral Media)**: Users can post 24-hour expiring photos/videos shown in a dedicated top carousel.
* **Social Interactions**: Users can like, comment, bookmark, and share posts.
* **Search & Explore**: Search by username, full-text captions, and explore trending tags and high-engagement media.

---

## Non-Functional Requirements

* **Ultra-Low Feed Latency**: Home feed retrieval must complete within **$< 200\text{ ms}$**; photo upload acknowledgement must take **$< 1\text{ second}$**.
* **High Availability (99.99%)**: The service must prioritize availability over strict consistency for media and feed retrieval.
* **Eventual Consistency**: New posts and likes may take a few seconds to propagate to all followers worldwide.
* **High Durability (100%)**: Once uploaded, user media must never be corrupted or lost (minimum 99.999999999% durability using multi-region cloud object storage).
* **Read-Heavy Traffic**: System must comfortably sustain at least a **$100:1$ read-to-write ratio**.
* **Global Edge Performance**: Images and video segments must be cached at edge CDN points of presence (PoPs) within milliseconds of end users.

---

## Back of the Envelope Estimations

### Scale & Traffic

* **Daily Active Users (DAU)**: $500\text{ Million}$.
* **Photos Uploaded per Day**: $100\text{ Million photos/day}$ (approx. $0.2$ photos/user/day).
* **Average Photo Write QPS**:
  $$\text{Write QPS} = \frac{100,000,000}{86,400} \approx 1,160\text{ writes/sec} \quad (\text{Peak: } \approx 2,500\text{ QPS})$$
* **Feed Read Requests per User**: $20\text{ feed views/day}$.
* **Total Daily Feed Reads**:
  $$500,000,000 \times 20 = 10,000,000,000\text{ feed reads/day}$$
* **Average Feed Read QPS**:
  $$\text{Read QPS} = \frac{10,000,000,000}{86,400} \approx 115,700\text{ QPS} \quad (\text{Peak: } \approx 250,000\text{ QPS})$$

### Storage Requirements

* **Average Compressed Photo Size**: $\approx 2\text{ MB}$ (raw upload is compressed into WebP/AVIF thumbnail formats).
* **Daily Media Storage**:
  $$100,000,000\text{ photos} \times 2\text{ MB} = 200\text{ Terabytes/day}$$
* **Yearly Media Storage**:
  $$200\text{ TB} \times 365 \approx 73\text{ Petabytes/year} \implies \approx 730\text{ PB over 10 years}$$
* **Post Metadata Size**: $\approx 500\text{ bytes}$ per post record.
  $$100,000,000 \times 500\text{ bytes} \approx 50\text{ GB/day} \implies \approx 18.25\text{ TB/year}$$

### Cache & Memory Sizing

* **80/20 Rule**: $20\%$ of posts generate $80\%$ of read traffic.
* Caching the active home feed (top 500 post IDs) for active users ($100\text{M}$ highly active users):
  $$100,000,000 \times 500\text{ post IDs} \times 8\text{ bytes (int64)} \approx 400\text{ GB RAM}$$
  *(Readily handled by a distributed Redis cluster).*

---

## High-Level System Architecture

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 25, 'rankSpacing': 45, 'padding': 10}}}%%
flowchart LR
    Client["Instagram Mobile (iOS / Android)"]
    subgraph EdgeTier["Edge & Ingress"]
        CDN["Global Edge CDN (Cloudflare)"]
        GW["API Gateway / Envoy"]
    end
    subgraph AppBackend["Services & Workers"]
        PostSvc["Post Service"]
        GraphSvc["User Graph Service"]
        FeedSvc["Feed Service"]
        Fanout["Fanout Workers (Push / Pull)"]
        Transcode["Transcode Fleet (WebP)"]
    end
    subgraph DataLayer["Storage, Cache & Bus"]
        S3[("AWS S3 Master Assets")]
        Kafka[["Apache Kafka Bus"]]
        RedisFeed[("Redis Feed Cache (ZSet)")]
        DB[("PostgreSQL / Cassandra")]
    end
    Client e1@-->|"1. Pre-Signed PUT"| S3
    Client e2@-->|"Fetch Media"| CDN
    CDN e3@-->|"Origin Pull"| S3
    Transcode e4@<-->|"WebP Variants"| S3
    Client e5@-->|"2. REST / GraphQL"| GW
    GW e6@-->|"Publish"| PostSvc
    GW e7@-->|"Follow"| GraphSvc
    GW e8@-->|"Fetch Feed"| FeedSvc
    PostSvc e9@-->|"PostCreated"| Kafka
    GraphSvc e10@-->|"FollowEvent"| Kafka
    Fanout e11@-->|"Consume"| Kafka
    Fanout e12@-->|"Push ZSet"| RedisFeed
    Fanout e13@-->|"Persist"| DB
    FeedSvc e14@-->|"Read Feed"| RedisFeed
    FeedSvc e15@-->|"Pull Celebs"| DB

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
    e15@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class Client client
    class CDN,GW edge
    class PostSvc,GraphSvc,FeedSvc service
    class Fanout,Transcode worker
    class RedisFeed cache
    class S3,DB storage
    class Kafka queue

    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style AppBackend fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataLayer fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Core Components & Data Flows

### 1. Photo Upload Pipeline (Direct-to-S3 via Pre-Signed URLs)

Uploading high-resolution media through web app servers creates massive I/O bottlenecks:

1. Client requests a pre-signed upload URL from the **Post Service**.
2. Post Service generates an encrypted, short-lived Amazon S3 pre-signed URL with strict bucket policies.
3. Mobile client uploads binary image bytes directly to Amazon S3 via `HTTP PUT`.
4. S3 fires an `ObjectCreated` event to an **SQS queue**.
5. **Transcoding Workers** consume the event, download the master image, and generate 3 WebP resolutions:
   * Low/Thumbnail: $150 \times 150\text{ px}$ (for profile grid)
   * Medium: $640 \times 640\text{ px}$ (for mobile feed)
   * High: $1080 \times 1080\text{ px}$ (for tablet/desktop)
6. Processed images are saved to the CDN origin bucket, and the Post Service writes the metadata record to the database.

### 2. Hybrid Feed Generation Model

* **Fan-Out on Write (Push)**:
  * For regular users ($< 25,000$ followers): When author uploads a photo, fan-out workers insert `postId` into all followers' **Redis Sorted Sets (ZSets)** using `createdAt` as the score.
  * Fast $O(1)$ read performance when a follower opens their feed.
* **Fan-Out on Read (Pull)**:
  * For celebrities (e.g., Cristiano Ronaldo, Selena Gomez with $> 100\text{M}$ followers): Posts are **never** pushed to millions of follower mailboxes.
  * When a user opens their feed, the Feed Service reads the user's precomputed Redis ZSet and queries the celebrity user timelines in parallel, merging and sorting the top 20 items in memory.

---

## API Endpoints

### 1. Request Pre-Signed Upload URL

```http
POST /api/v1/media/presigned-url
Authorization: Bearer <user_jwt>
Content-Type: application/json
```

```json
// Request
{
  "contentType": "image/jpeg",
  "contentLengthBytes": 4291040,
  "checksumMd5": "c2FtcGxlX21kNV9oYXNoCg=="
}

// Response: 200 OK
{
  "uploadUrl": "https://s3.us-west-2.amazonaws.com/ig-masters/usr_102/med_99182a.jpg?AWSAccessKeyId=...",
  "mediaId": "med_99182a",
  "expiresInSeconds": 300
}
```

### 2. Create Post

```http
POST /api/v1/posts
Authorization: Bearer <user_jwt>
Content-Type: application/json
```

```json
// Request
{
  "mediaId": "med_99182a",
  "caption": "Sunset in Malibu #goldenhour #sunset",
  "location": "Malibu Beach, CA",
  "taggedUserIds": [9912, 10482]
}

// Response: 201 Created
{
  "postId": 1490284729102840,
  "mediaUrls": {
    "thumbnail": "https://cdn.instagram.com/p/med_99182a_150.webp",
    "medium": "https://cdn.instagram.com/p/med_99182a_640.webp",
    "full": "https://cdn.instagram.com/p/med_99182a_1080.webp"
  },
  "caption": "Sunset in Malibu #goldenhour #sunset",
  "createdAt": 1730005200
}
```

### 3. Get User Feed

```http
GET /api/v1/feed?limit=20&cursor=1490284720000000
Authorization: Bearer <user_jwt>
```

```json
// Response: 200 OK
{
  "posts": [
    {
      "postId": 1490284729102840,
      "author": {
        "userId": 102,
        "username": "tarun_photo",
        "avatarUrl": "https://cdn.instagram.com/a/102.webp"
      },
      "mediaUrls": {
        "medium": "https://cdn.instagram.com/p/med_99182a_640.webp"
      },
      "caption": "Sunset in Malibu #goldenhour #sunset",
      "likeCount": 384,
      "commentCount": 18,
      "hasLiked": false,
      "createdAt": 1730005200
    }
  ],
  "nextCursor": "1490284500000000",
  "hasMore": true
}
```

---

## Object Model & Database Schema

### Database Selection & Rationale

| Workload / Subsystem | Database Choice | Data Model & CAP Profile | Why This Database? (Justification & Trade-offs) |
| :--- | :--- | :--- | :--- |
| **Users, Posts & Media Metadata** | **Sharded PostgreSQL** | Relational (ACID, **CP / PC-EC**) | **Why:** Instagram famously scaled core metadata (`User`, `Post`, `Media`) across thousands of logical PostgreSQL shards using custom 64-bit PL/pgSQL Snowflake-like IDs (41b timestamp + 13b shard ID + 10b sequence). Embedding the logical shard ID directly inside `postId` enables single-hop routing without a central lookup table while preserving ACID guarantees and relational integrity. |
| **User Feed Timelines & Activity Inbox** | **Apache Cassandra** | Wide-Column Store (**AP / PA-EL**) | **Why:** Precomputed user feeds require high-velocity fan-out writes and sequential reverse-chronological reads (`WHERE user_id = ? ORDER BY post_id DESC LIMIT 20`). Cassandra's LSM-Tree storage engine handles massive write amplification across data centers with zero single points of failure and tunable quorum consistency. |
| **Hot Feed Cache, 24h Stories & Counters** | **Redis Cluster (+ Memcached / TAO)** | In-Memory Key-Value / ZSet (**AP / Sub-ms**) | **Why:** Sustaining $250,000\text{ Peak Read QPS}$ with $< 200\text{ ms}$ latency requires serving the active home feed (top 500 `postId`s per active user, $\sim 400\text{ GB}$ RAM) from Redis Sorted Sets (`ZSET`). Ephemeral 24-hour Stories leverage native Redis key TTLs to auto-evict without disk tombstone bloat. |
| **Social Graph (`Follower` Edges)** | **Meta TAO (Memcached + Sharded MySQL) / Cassandra** | Distributed Graph Cache + Persistent Store (**AP / Read-Your-Writes**) | **Why:** Fan-out workers and privacy checks frequently query bidirectional edges (`followers_of(u)`, `following_of(u)`). TAO caches adjacency lists in memory with write-through persistence to sharded MySQL, serving billions of graph lookups per second at microsecond latency. |
| **Photos, Videos & WebP Variants** | **AWS S3 / Meta Haystack + Global Edge CDN** | Immutable Object Storage (**11 9s Durability**) | **Why:** $200\text{ TB/day}$ ($73\text{ PB/year}$) of binary media cannot live in relational or NoSQL tables. S3 stores master and transcoded WebP variants (`150px`, `640px`, `1080px`), while Cloudflare/CloudFront Edge CDNs serve $>95\%$ of media reads directly from local ISP PoPs. |
| **Hashtag & Explore Search** | **Elasticsearch** | Inverted Index (**AP / Near Real-Time**) | **Why:** Indexes captions, hashtags (`#sunset`), and location tags for sub-100ms prefix and relevance search on the Explore page. |

```mermaid
erDiagram
    User ||--o{ Post : "publishes"
    User ||--o{ Follower : "follows_or_followed_by"
    Post ||--|{ Media : "contains_media"
    User {
        bigint userId PK
        varchar_50 username
        varchar_255 email
        varchar_100 fullName
        varchar_255 profilePicUrl
        varchar_150 bio
        boolean isCelebrity
        boolean isPrivate
    }
    Follower {
        bigint followeeId PK,FK
        bigint followerId PK,FK
        varchar_16 status
        timestamp createdAt
    }
    Post {
        bigint postId PK
        bigint userId FK
        varchar_2200 caption
        varchar_100 location
        bigint likeCount
        bigint commentCount
        timestamp createdAt
    }
    Media {
        varchar_64 mediaId PK
        bigint postId FK
        varchar_16 mediaType
        varchar_255 s3Path
        varchar_255 cdnUrl
        int width
        int height
    }
```

---

## System Design Diagram

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph ClientTier["Clients"]
        Client["Instagram Client (iOS, Android, Web)"]
    end
    subgraph EdgeTier["Edge & API Gateway"]
        CDN["Global Edge CDN (WebP / AVIF)"]
        Gateway{"API Gateway & L7 Load Balancer"}
    end
    subgraph CoreServices["Core Microservices & Workers"]
        PostSvc["Post Service (64-Bit ID & Pre-Sign)"]
        UserFeedSvc["User Feed Svc (Hybrid Push/Pull)"]
        TranscodeWorkers["Media Transcode Fleet (WebP)"]
    end
    subgraph DataAndEventTier["Data Stores, Caches & Event Bus"]
        S3[("AWS S3 (Master & Multi-Res)")]
        PostCache[("Post Cache (Redis / Memcached)")]
        PostDB[("Sharded Post DB (PostgreSQL)")]
        StoriesCache[("Stories Store (Redis 24h TTL)")]
        FeedCache[("Feed Cache (Redis ZSet Top 500)")]
        FeedDB[("User Feed DB (Cassandra)")]
        GraphDB[("Social Graph DB (Tao / Neo4j)")]
        SearchIndex[("Elasticsearch (Explore & Tags)")]
        Kafka[["Apache Kafka Event Bus"]]
    end
    Client e1@-->|"1. API / Feed"| Gateway
    Client e2@-->|"2. Pre-Signed PUT"| S3
    CDN e3@-->|"Origin Pull"| S3
    Client e4@-->|"3. Fetch Media"| CDN
    Gateway e5@-->|"Read / Write"| PostSvc
    Gateway e6@-->|"Feed & Stories"| UserFeedSvc
    TranscodeWorkers e7@<-->|"Resize Variants"| S3
    PostSvc e8@-->|"Write-Through"| PostCache
    PostSvc e9@-->|"Persist Shards"| PostDB
    PostSvc e10@-->|"Publish Event"| Kafka
    Kafka e11@-->|"Index Tags"| SearchIndex
    Kafka e12@-->|"Update Edges"| GraphDB
    UserFeedSvc e13@-->|"Fetch Followers"| GraphDB
    UserFeedSvc e14@-->|"Push (<25K)"| FeedCache
    UserFeedSvc e15@-->|"Persist Timeline"| FeedDB
    UserFeedSvc e16@-->|"Pull (>25K Celeb)"| PostCache
    UserFeedSvc e17@<-->|"24h Stories"| StoriesCache

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
    e15@{ animate: true }
    e16@{ animate: true }
    e17@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class Client client
    class CDN,Gateway edge
    class PostSvc,UserFeedSvc service
    class TranscodeWorkers worker
    class PostCache,StoriesCache,FeedCache cache
    class S3,PostDB,FeedDB,GraphDB,SearchIndex storage
    class Kafka queue

    style ClientTier fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style CoreServices fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataAndEventTier fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Deep Dive & Key Technical Challenges

### 1. Database Sharding: Instagram's 64-bit ID Generation

To scale beyond a single database, Instagram shards its PostgreSQL databases across thousands of logical partitions.

* **Why Sharding by User ID has flaws**: If a user is a celebrity, all their photos and likes hit one shard, causing a **hot shard bottleneck**.
* **Instagram ID Generation Scheme (Snowflake-like)**:
  Instagram IDs are 64-bit integers generated directly inside PostgreSQL using custom PL/pgSQL sequences:
  * **41 bits**: Epoch timestamp in milliseconds (gives $\approx 69\text{ years}$ of unique IDs).
  * **13 bits**: Logical Shard ID ($2^{13} = 8,192\text{ logical shards}$).
  * **10 bits**: Auto-incrementing sequence number within the microsecond ($2^{10} = 1,024\text{ IDs per millisecond per shard}$).

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph ID64["64-Bit Instagram ID Layout"]
        B1["Bits 63..23 (41b): Epoch ms (~69 Yrs)"]
        B2["Bits 22..10 (13b): Shard ID (8,192)"]
        B3["Bits 9..0 (10b): Sequence (1,024/ms)"]
        B1 e1@-->|"Next"| B2
        B2 e2@-->|"Next"| B3
    end

    e1@{ animate: true }
    e2@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff

    class B1 client
    class B2 service
    class B3 storage

    style ID64 fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
```

* **Routing Benefit**: Given any `postId`, the application extracts the 13-bit Shard ID using a simple bitmask (`(postId >> 10) & 0x1FFF`), allowing the system to route the query directly to the responsible database shard without querying a centralized coordinator.

### 2. Ephemeral Content (Stories) Architecture

* **24-Hour Expiration**: Stories are saved with a TTL. Using Redis or DynamoDB with native TTL features ensures that expired stories are automatically dropped without heavy disk I/O.
* **Story Ring Caching**:
  * An in-memory cache maintains a bitset of viewed story IDs per user.
  * When a user opens Instagram, the Story Tray shows unread stories sorted by close friends and interaction frequency.

---

## Additional Notes

### Video References

* [![Instagram System Design Video](https://img.youtube.com/vi/anM-cr6RqDI/0.jpg)](https://www.youtube.com/watch?v=anM-cr6RqDI)
