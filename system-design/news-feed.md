# News Feed System Design

[← Back to System Design](system-design.md)

> **Core Building Blocks Used**: [Chapter 1 — GraphQL Feed Aggregation & SSE/WebSocket Live Updates](fundamentals/01-networking-and-api-paradigms.md#2-api-paradigms-rest-vs-graphql-vs-grpc) · [Chapter 3 — Hybrid Fanout-on-Write (Push) vs. Fanout-on-Read (Pull) Caching](fundamentals/03-caching-and-scaling.md#1-caching-deep-dive) · [Chapter 4 — Graph Stores (Social Graph) & Wide-Column Timeline Storage](fundamentals/04-databases-sql-vs-nosql.md#6-graph-databases--relation-stores-neo4j-neptune-meta-tao-zanzibar) · [Chapter 9 — 4-Stage Feed Ranking Funnel](fundamentals/09-genai-llm-and-ml-system-design.md#multi-stage-recommendation--ranking-funnel)

## Overview

A News Feed system is a real-time, personalized content aggregation and ranking engine that continuously compiles posts, photos, videos, and status updates from friends, followed pages, and algorithmic recommendation engines into an infinite scrollable stream.

Prominent examples include Facebook News Feed, Instagram Feed, Twitter/X Home Timeline, and LinkedIn Feed. The core engineering challenge is delivering sub-second personalized feeds to hundreds of millions of concurrent users while handling extreme read-to-write asymmetries, massive fan-out write amplification, and complex multi-factor ranking algorithms.

---

## Functional Requirements

* **Feed Publishing**: When a user creates a post (status update, photo, video, link), the post must be distributed to the feeds of all their followers/friends.
* **Feed Generation & Retrieval**: Users can fetch their personalized news feed ordered chronologically or scored by an algorithmic ranking model.
* **Infinite Scroll & Pagination**: Fast, cursor-based pagination supporting continuous feed scrolling without duplicate items or skipped posts.
* **Media Content Rendering**: Display embedded media (images, video previews, link cards, user tags).
* **Social Engagement**: Seamless integration with real-time like counts, comment counts, and shares.

---

## Non-Functional Requirements

* **Ultra-Low Latency**: Feed retrieval must return the top 20 posts in **$< 150–200\text{ ms}$** (P95).
* **High Availability (99.99%)**: The feed is the primary user retention surface. If the ranking pipeline is slow or fails, the system must gracefully fall back to a chronological or cached feed.
* **Eventual Consistency**: New posts may appear in a follower's feed within 2–5 seconds of being published.
* **Scalability**: Handle **$300\text{ Million}$ Daily Active Users (DAU)** with up to $50,000$ feed queries per second at peak.

---

## Back of the Envelope Estimations

### Scale & Traffic

* **Daily Active Users (DAU)**: $300\text{ Million}$.
* **Average Feed Refreshes per User**: $10\text{ times/day}$.
* **Total Daily Feed Reads**:
  $$300,000,000 \times 10 = 3,000,000,000\text{ feed reads/day}$$
* **Average Read QPS**:
  $$\text{Feed Read QPS} = \frac{3,000,000,000}{86,400} \approx 35,000\text{ QPS} \quad (\text{Peak: } \approx 70,000\text{ QPS})$$
* **New Posts Published Daily**:
  * Assume $20\%$ of users publish 1 post per day:
  $$300,000,000 \times 0.20 = 60,000,000\text{ posts/day}$$
* **Average Post Ingestion QPS**:
  $$\text{Post Write QPS} = \frac{60,000,000}{86,400} \approx 700\text{ writes/sec} \quad (\text{Peak: } \approx 1,500\text{ QPS})$$

### Feed Cache Sizing (Redis Cluster)

* **Strategy**: Cache the top 500 post IDs for the top $20\%$ most active users ($60\text{ Million users}$).
* **Memory per User**:
  $$500\text{ post IDs} \times 8\text{ bytes (Int64)} = 4,000\text{ bytes} \approx 4\text{ KB}$$
* **Total Cache RAM Required**:
  $$60,000,000 \times 4\text{ KB} = 240\text{ Gigabytes}$$
  *(Even doubling for replication and metadata overhead, $\approx 500\text{ GB}$ easily fits on a small Redis cluster).*

---

## High-Level System Architecture

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 25, 'rankSpacing': 45, 'padding': 10}}}%%
flowchart LR
    Client["Client Apps (iOS, Android, Web)"]
    subgraph EdgeTier["Edge & Gateway"]
        GW["API Gateway / Load Balancer"]
    end
    subgraph CoreServices["Feed Services & Workers"]
        PostSvc["Post Service"]
        FeedSvc["Feed Service"]
        Fanout["Fanout Workers Engine"]
        Hydrator["Post & Author Hydrator"]
    end
    subgraph DataLayer["Storage, Cache & Bus"]
        Kafka[["Kafka ('new-posts')"]]
        SocialGraph[("Social Graph DB")]
        RedisCache[("Redis Feed Cache (ZSet)")]
        PostDB[("Post Master Database")]
    end
    Client e1@-->|"1. POST /posts & GET /feed"| GW
    GW e2@-->|"Publish"| PostSvc
    GW e3@-->|"Fetch Feed"| FeedSvc
    PostSvc e4@-->|"Emit Event"| Kafka
    PostSvc e5@-->|"Save Post"| PostDB
    Fanout e6@-->|"Consume"| Kafka
    Fanout e7@-->|"Get Followers"| SocialGraph
    Fanout e8@-->|"Push postId"| RedisCache
    FeedSvc e9@-->|"Top Post IDs"| RedisCache
    FeedSvc e10@-->|"Hydrate IDs"| Hydrator
    Hydrator e11@-->|"MGET Objects"| PostDB

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
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class Client client
    class GW edge
    class PostSvc,FeedSvc service
    class Fanout,Hydrator worker
    class RedisCache cache
    class SocialGraph,PostDB storage
    class Kafka queue

    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style CoreServices fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataLayer fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Core Workflows: Fanout Strategies

### 1. Fanout-on-Write (Push Model)

* **How it works**: When a user creates a post, the Fanout Service queries their followers list from the Social Graph Service and writes the `postId` directly into every follower's Redis feed cache (Sorted Set).
* **Pros**:
  * Real-time feed retrieval is extremely fast: $O(1)$ lookup from Redis.
* **Cons**:
  * **Celebrity Hotspot / Write Amplification**: An account with 40M followers triggers 40M write operations for a single post.
  * Inactive users waste cache space receiving pre-computed feeds they never read.

### 2. Fanout-on-Read (Pull Model)

* **How it works**: The user's feed is not generated ahead of time. When a user requests their feed, the system queries the latest posts from all people they follow, merges them, and ranks them on the fly.
* **Pros**:
  * No write amplification. Inactive users consume zero system resources.
* **Cons**:
  * High read latency: Querying hundreds of accounts and merge-sorting on every feed load overwhelms the database.

### 3. The Hybrid Fanout Architecture

The production industry standard splits users into two classes:

1. **Regular Users ($< 20,000$ followers)**: Use **Fanout-on-Write**. Their posts are pushed into their followers' Redis caches immediately.
2. **Celebrity Accounts ($> 20,000$ followers)**: Use **Fanout-on-Read**. Their posts are *not* broadcast to millions of follower caches.
3. **Feed Merge on Read**: When a follower fetches their feed:
   * Read the follower's pre-computed Redis cache (posts from regular friends).
   * Fetch the latest posts from the celebrities they follow.
   * Merge the two lists in memory and apply ranking.

---

## API Specifications

### 1. Publish a Post

```http
POST /api/v1/posts
Authorization: Bearer <user_token>
Content-Type: application/json
```

```json
// Request
{
  "content": "Excited to share our new distributed systems architecture!",
  "mediaUrls": ["https://cdn.example.com/images/arch.png"],
  "visibility": "PUBLIC"
}

// Response: 201 Created
{
  "postId": "pst_9812490124",
  "authorId": "usr_44012",
  "createdAt": 1730005200
}
```

### 2. Retrieve News Feed

```http
GET /api/v1/feed?limit=20&cursor=pst_9812490000
Authorization: Bearer <user_token>
```

```json
// Response: 200 OK
{
  "items": [
    {
      "postId": "pst_9812490124",
      "author": {
        "userId": "usr_44012",
        "name": "Tarun Batta",
        "avatarUrl": "https://cdn.example.com/avatars/44012.jpg"
      },
      "content": "Excited to share our new distributed systems architecture!",
      "mediaUrls": ["https://cdn.example.com/images/arch.png"],
      "likeCount": 312,
      "commentCount": 42,
      "createdAt": 1730005200
    }
  ],
  "nextCursor": "pst_9812488500",
  "hasMore": true
}
```

---

## Object Model & Database Schema

### Database Selection & Rationale

| Workload / Subsystem | Database Choice | Data Model & CAP Profile | Why This Database? (Justification & Trade-offs) |
| :--- | :--- | :--- | :--- |
| **Precomputed User Timelines & Celebrity Outboxes** | **Redis Cluster (Sorted Sets / ZSets)** | In-Memory Key-Value (**AP / Sub-ms**) | **Why:** Delivering a personalized feed in $< 150\text{ ms}$ at $70,000\text{ Peak QPS}$ prevents executing expensive SQL `JOIN` queries across hundreds of followed authors on read. Storing only the top 500 `postId` integers (8 bytes each, $\sim 4\text{ KB/user}$) in a Redis `ZSET` keyed by `userId` (scored by timestamp or ML rank) fits 60M active users in $\sim 240\text{ GB}$ RAM and provides $O(\log N)$ insertions and $O(1)$ range fetches. |
| **Post Master Records & Persistent Timelines** | **Sharded PostgreSQL / MySQL or Apache Cassandra** | Sharded Relational / Wide-Column (**CP or Tunable AP**) | **Why:** `Post` records (`postId`, `authorId`, `content`, `mediaUrls`) require durable persistence and fast batch point-lookups (`MGET` by `postId`) or author-timeline scans (`WHERE authorId = ? ORDER BY postId DESC`). Sharding by `authorId` (or time-sortable Snowflake `postId`) scales storage linearly across terabytes of posts. |
| **Social Graph (`FollowEdge`)** | **Meta TAO (Memcached + Sharded MySQL) / Neo4j** | Graph / Adjacency Store (**AP / Read-Your-Writes**) | **Why:** Every post fan-out and hybrid read-merge must rapidly fetch `followers(authorId)` or `followees(userId)`. Storing directed adjacency lists (`followerId` $\leftrightarrow$ `followeeId`) indexed in both directions with an in-memory graph cache (TAO) guarantees single-millisecond neighbor traversal. |
| **Post Hydration & Engagement Counter Cache** | **Memcached / Redis Cluster** | In-Memory Key-Value (**AP / Sub-ms**) | **Why:** Once the ML Ranker selects the top 20 `postId`s, the Hydrator executes a single parallel `MGET` against Memcached/Redis to attach full post payloads, author avatars, and atomic `likeCount`/`commentCount` counters in $< 10\text{ ms}$ without touching disk. |
| **ML Online Feature Store** | **Redis / Feast (backed by Cassandra/DynamoDB)** | Low-Latency Key-Value (**AP**) | **Why:** The Two-Tower/GBDT Ranking Engine needs real-time user-author affinity vectors and post engagement velocity features in $< 15\text{ ms}$ to score 500 candidate posts on the fly. |

```mermaid
erDiagram
    User ||--o{ Post : "publishes"
    User ||--o{ FollowEdge : "follows_or_followed_by"
    User {
        bigint userId PK
        varchar_50 username
        varchar_255 email
        int followerCount
        boolean isCelebrity
    }
    FollowEdge {
        bigint followerId PK,FK
        bigint followeeId PK,FK
        timestamp createdAt
    }
    Post {
        bigint postId PK
        bigint authorId FK
        text content
        json mediaUrls
        int likeCount
        int commentCount
        timestamp createdAt
    }
```

---

## System Design Diagram

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph ClientTier["Clients"]
        Client["Client Devices (iOS, Android, Web)"]
    end
    subgraph EdgeTier["Edge & API Gateway"]
        CDN["Global Media CDN (Images & Video)"]
        Gateway{"API Gateway / Load Balancer"}
    end
    subgraph CoreServices["Core Services, Ranking & Workers"]
        PostSvc["Post Service (Validate & Save)"]
        FanoutSvc["Fanout Worker (<20K Push Hybrid)"]
        FeedSvc["Feed Coordinator (Merge Push/Pull)"]
        Ranker["ML Ranking Engine (Two-Tower / GBDT)"]
        Hydrator["Post Hydrator & Diversity Filter"]
    end
    subgraph DataAndEventTier["Data Stores, Caches & Event Bus"]
        KafkaPosts[["Kafka Bus ('new-posts' Stream)"]]
        UserGraph[("Social Graph DB (Followers)")]
        RedisTimeline[("Redis Timeline ZSet (Top 500 IDs)")]
        CelebrityCache[("Celebrity Outbox Cache (Redis)")]
        FeatureStore[("Online Feature Store (Affinity)")]
        PostCache[("Post & Counter Cache (Memcached)")]
        PostMasterDB[("Sharded Post Master DB")]
    end
    Client e1@-->|"1. POST & GET"| Gateway
    Client e2@-->|"Fetch Media"| CDN
    Gateway e3@-->|"Publish Post"| PostSvc
    Gateway e4@-->|"GET /feed"| FeedSvc
    PostSvc e5@-->|"Save Master"| PostMasterDB
    PostSvc e6@-->|"Warm Cache"| PostCache
    PostSvc e7@-->|"Celeb Outbox"| CelebrityCache
    PostSvc e8@-->|"Emit Event"| KafkaPosts
    FanoutSvc e9@-->|"Consume"| KafkaPosts
    FanoutSvc e10@-->|"Get Followers"| UserGraph
    FanoutSvc e11@-->|"Push Regular"| RedisTimeline
    FeedSvc e12@-->|"Read Regular"| RedisTimeline
    FeedSvc e13@-->|"Pull Celebs"| CelebrityCache
    FeedSvc e14@-->|"Rank 500 IDs"| Ranker
    Ranker e15@-->|"Read Features"| FeatureStore
    FeedSvc e16@-->|"Hydrate Top 20"| Hydrator
    Hydrator e17@-->|"Batch MGET"| PostCache
    Hydrator e18@-->|"Cache Miss"| PostMasterDB

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
    e18@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class Client client
    class CDN,Gateway edge
    class PostSvc,FeedSvc,Ranker service
    class FanoutSvc,Hydrator worker
    class RedisTimeline,CelebrityCache,PostCache cache
    class UserGraph,FeatureStore,PostMasterDB storage
    class KafkaPosts queue

    style ClientTier fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style CoreServices fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataAndEventTier fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Deep Dive: Algorithmic Feed Ranking Pipeline

While chronological feeds simply sort by `createdAt DESC`, modern feeds rank content by predicted user engagement.

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    A["Candidate Generation"]
    B["Feature Extraction"]
    C["ML Scoring & Ranking"]
    D["Diversity & Filtering"]
    E["Hydration & Delivery"]
    A e1@-->|"500 IDs"| B
    B e2@-->|"Vectors"| C
    C e3@-->|"Scored"| D
    D e4@-->|"Top 20"| E

    e1@{ animate: true }
    e2@{ animate: true }
    e3@{ animate: true }
    e4@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff

    class A client
    class B edge
    class C service
    class D worker
    class E storage
```

1. **Candidate Generation (Retrieval)**:
   * Pull top 500 recent post IDs from friends and followed pages.
2. **Feature Extraction**:
   * **User Features**: Past engagement history, active times of day, preferred topics.
   * **Author Features**: Affinity score between viewer and author (message frequency, previous likes).
   * **Post Features**: Post age, media type (video vs image), current like/comment velocity.
3. **ML Scoring (Two-Tower Neural Network / GBDT)**:
   $$\text{Score} = w_1 P(\text{click}) + w_2 P(\text{like}) + w_3 P(\text{comment}) + w_4 P(\text{share}) - w_5 P(\text{hide})$$
4. **Diversity & Deduplication Filters**:
   * Prevent showing 5 consecutive posts from the same author.
   * Insert sponsored/promoted posts at fixed intervals (e.g., every 5th post).
5. **Post Hydration**:
   * The scored feed contains only IDs. The hydrator performs a multi-key get (`MGET`) against Memcached/Redis to inject full post text, user avatars, and media URLs in $< 10\text{ ms}$.

---

## Additional Notes

### Video References

* [![News Feed System Design Video 1](https://img.youtube.com/vi/Qj4-GruzyDU/0.jpg)](https://www.youtube.com/watch?v=Qj4-GruzyDU)
* [![News Feed System Design Video 2](https://img.youtube.com/vi/PE7ZwE5Ace4/0.jpg)](https://www.youtube.com/watch?v=PE7ZwE5Ace4)
