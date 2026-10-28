# Amazon Prime System Design

[← Back to System Design](system-design.md)

## Overview

Amazon Prime is a global multi-benefit subscription platform encompassing Prime Video on-demand streaming, Prime Music, e-commerce fast delivery benefits, Twitch gaming, and cloud photo storage.

In a system design interview, Amazon Prime is typically explored through the lens of **Prime Video** and its integration with the centralized **Prime Entitlement Service**, featuring real-time scene metadata synchronization (**Amazon X-Ray**), dynamic multi-CDN switching, digital rights management (DRM), and offline media synchronization.

---

## Functional Requirements

* **Unified Entitlement & Access Control**: Validate active Prime subscription status across multiple product verticals (Video, Music, Fast Shipping) with microsecond latency.
* **Prime Video Adaptive Streaming**: Stream high-definition, 4K HDR, and live sports (e.g., Thursday Night Football) using HLS and MPEG-DASH.
* **Amazon X-Ray Synchronization**: Real-time display of on-screen actors, character trivia, and background soundtrack music synchronized with the exact video playback timestamp.
* **Offline Media Downloads**: Encrypted, time-limited video downloads on iOS, Android, and Fire devices.
* **Multi-Profile & Watchlist Sync**: Support up to 6 household profiles with synchronized watch history and bookmark positions.

---

## Non-Functional Requirements

* **Ultra-Low Entitlement Check Latency**: Subscription validation must complete in **$< 50\text{ ms}$** to avoid delaying page or stream loads.
* **Streaming Availability (99.99%)**: Video streaming must remain resilient even during massive concurrent live events.
* **Adaptive Bitrate & Zero Buffering**: Smooth playback across fluctuating cellular and home broadband connections.
* **Multi-CDN Redundancy**: Avoid single-point-of-failure CDN outages by dynamically load-balancing between Amazon CloudFront, Akamai, and Fastly.
* **Strict Content Protection**: Hardware-backed DRM (Widevine Level 1, Apple FairPlay, Microsoft PlayReady).

---

## Back of the Envelope Estimations

### Scale & Concurrency

* **Total Global Prime Subscribers**: $200\text{ Million}$.
* **Prime Video Daily Active Users (DAU)**: $60\text{ Million}$.
* **Average Video Stream Time**: $1.5\text{ hours/day}$ per active user.
* **Daily Video Hours Streamed**:
  $$60,000,000 \times 1.5 = 90,000,000\text{ hours/day}$$
* **Peak Streaming Bandwidth**:
  * Average bitrate of $4\text{ Mbps}$, evening peak 4-hour window delivering $40\%$ of volume:
  $$\text{Peak Bandwidth} = \frac{0.40 \times (90\text{M} \times 3,600\text{ s} \times 4\text{ Mbps})}{4 \times 3,600\text{ seconds}} \approx 36\text{ Terabits per second (Tbps)}$$
* **Live Sports Peak (Thursday Night Football)**:
  * Up to $15\text{ Million concurrent live streams} \times 6\text{ Mbps (1080p60)} \approx 90\text{ Tbps concurrent delivery}$!

---

## High-Level System Architecture

```mermaid
flowchart TD
    Client["Client App (FireTV, Phone, Web)"]
    subgraph AmazonBackend["Amazon Backend"]
        GW["API Gateway (AppSync)"]
        EntSvc["Entitlement Service"]
        CatSvc["Catalog Service"]
        XRaySvc["X-Ray Service"]
        DynamoEnt[("Amazon DynamoDB")]
        DynamoCat[("DynamoDB / Elasticsearch")]
        Neptune[("Neptune (Graph DB)")]
    end
    subgraph MultiCDN["Multi-CDN Data Plane"]
        CDNRouter["Dynamic CDN Router (CloudFront, Akamai, Fastly)"]
        EdgePoP["Video Chunks & Manifests (Edge PoPs Worldwide)"]
        S3Master[("Amazon S3 Master Storage (Transcoded Video & DRM)")]
    end
    Client -->|"1. Entitlement API & X-Ray Metadata"| GW
    GW -->|"Verify Subscription"| EntSvc
    GW -->|"Browse Titles"| CatSvc
    GW -->|"Fetch Scene Metadata"| XRaySvc
    EntSvc -->|"Global Lookup"| DynamoEnt
    CatSvc -->|"Search & Metadata"| DynamoCat
    XRaySvc -->|"Graph Traversal"| Neptune
    Client -->|"2. Stream Video (HLS / DASH)"| CDNRouter
    CDNRouter -->|"Route to Optimal PoP"| EdgePoP
    S3Master -->|"Origin Fetch"| EdgePoP
```

---

## Core Components & Deep Dives

### 1. Unified Entitlement Service

* Prime subscription status is authoritative in Amazon's Core Customer Account database.
* To avoid hitting the core database on every video play or e-commerce checkout, the **Entitlement Service** publishes updates to **Amazon DynamoDB Global Tables** replicated worldwide.
* In-memory caches (ElastiCache Redis) provide sub-5ms entitlement checks (`hasActiveBenefit(userId, 'PRIME_VIDEO')`).

### 2. Amazon X-Ray: Time-Synchronized Metadata Engine

* **How it works**:
  * During studio ingestion, computer vision facial recognition and audio fingerprinting scan each video frame to detect actors, characters, and background music tracks.
  * Graph relationships (Actor $\to$ Character $\to$ Filmography) are stored in **Amazon Neptune** (Graph Database).
  * The client receives a lightweight JSON metadata track aligned with video timestamps:

  ```json
  {
    "sceneStartMs": 142000,
    "sceneEndMs": 185000,
    "actors": [
      {"name": "Karl Urban", "character": "Billy Butcher", "photoUrl": "..."}
    ],
    "music": {
      "trackTitle": "London Calling",
      "artist": "The Clash",
      "amazonMusicUrl": "..."
    }
  }
  ```

  * As the video player timeline advances, the client UI updates on-screen actor overlays seamlessly without backend network calls.

### 3. Dynamic Multi-CDN Switching

* Relying on a single CDN for high-profile events (e.g., Premier League or NFL live games) presents catastrophic outage risk.
* **Client-Side Real-Time CDN Rerouting**:
  * The video player continuously measures chunk download latency, TCP connection errors, and throughput across multiple CDNs (CloudFront, Akamai, Fastly).
  * If CloudFront experiences packet loss or throttling in a specific ISP region, the player dynamically switches to requesting subsequent chunks from Akamai without stuttering or video restart.

---

## API Endpoints

### 1. Verify Entitlement & Fetch Stream Manifest

```http
POST /api/v1/video/playback/session
Authorization: Bearer <auth_token>
Content-Type: application/json
```

```json
// Request
{
  "titleId": "amzn1.dv.gti.89b1248a",
  "profileId": "prof_1028",
  "clientInfo": {
    "deviceType": "FIRE_TV_4K",
    "drmScheme": "PLAYREADY"
  }
}

// Response: 200 OK
{
  "playbackUrl": "https://d12345.cloudfront.net/manifests/89b1248a.mpd",
  "backupUrl": "https://akamai.primevideo.com/manifests/89b1248a.mpd",
  "xrayMetadataUrl": "https://xray.amazon.com/scenes/89b1248a.json",
  "drmLicenseUrl": "https://drm.amazon.com/license",
  "resumePositionMs": 420100
}
```

---

## Object Model & Database Schema

```mermaid
erDiagram
    PRIME_MEMBER ||--o{ WATCH_HISTORY : "records"
    TITLE_METADATA ||--o{ WATCH_HISTORY : "tracked_in"
    TITLE_METADATA ||--o{ XRAY_SCENE : "contains"
    PRIME_MEMBER {
        uuid customerId PK
        varchar_32 status
        varchar_32 planType
        date renewalDate
        boolean autoRenew
    }
    WATCH_HISTORY {
        uuid historyId PK
        uuid customerId FK
        varchar_64 titleId FK
        int resumePositionMs
        timestamp lastWatchedAt
    }
    TITLE_METADATA {
        varchar_64 titleId PK
        varchar_255 title
        text synopsis
        boolean is4K
        varchar_16 maturityRating
        int releaseYear
    }
    XRAY_SCENE {
        uuid sceneId PK
        varchar_64 titleId FK
        int startOffsetMs
        int endOffsetMs
        uuid_array actorIds
        uuid songId
    }
```

---

## System Design Diagram

```mermaid
flowchart TD
    subgraph ClientTier["Client Devices"]
        Client["Client Devices (Fire TV, Smart TV, Mobile, Web: Multi-CDN Switcher & X-Ray Overlay)"]
    end
    subgraph CDNTier["Multi-CDN Data Plane"]
        CloudFront["Amazon CloudFront (Primary Edge CDN)"]
        Akamai["Akamai CDN (Secondary Failover)"]
        Fastly["Fastly CDN (Live Events / Thursday Night Football)"]
    end
    subgraph ControlPlane["API Gateway & Control Plane Services"]
        GW{"API Gateway / AppSync (GraphQL / REST, Auth, Entitlement Routing)"}
        EntSvc["Prime Entitlement Service (Sub-50ms Validation: Video, Music, Shipping)"]
        XRaySvc["Amazon X-Ray Service (Time-Synchronized Scene Engine: Actor, Trivia & Music)"]
        WatchSvc["Playback History & Sync Service (Multi-Profile Bookmark Resume Timestamp)"]
        DRMSvc["DRM License Service (Widevine L1, FairPlay, PlayReady)"]
    end
    subgraph DataAndMediaStorage["Global Data Stores & Media Pipeline"]
        RedisEnt[("ElastiCache Redis (Sub-5ms Entitlement Cache)")]
        DynamoGlobal[("DynamoDB Global Tables (Prime Membership Records Replicated Worldwide)")]
        Neptune[("Amazon Neptune Graph DB (Actor -> Character -> Filmography & Soundtrack)")]
        S3Master[("AWS S3 Master Video Bucket & MediaConvert GPU Pipeline (H.264/HEVC/AV1)")]
    end
    Client -->|"Stream Chunks (HLS / MPEG-DASH ABR)"| CloudFront
    Client -.->|"Dynamic Client Reroute on Packet Loss"| Akamai
    Client -.->|"Live Sports Stream"| Fastly
    Client -->|"Control / Playback Session / Metadata"| GW
    GW -->|"Verify Benefit"| EntSvc
    EntSvc -->|"Read Cache"| RedisEnt
    EntSvc -->|"Fallback / Sync"| DynamoGlobal
    GW -->|"Fetch Scene JSON Track"| XRaySvc
    XRaySvc -->|"Query Graph Relationships"| Neptune
    GW -->|"Sync Watch Position"| WatchSvc
    GW -->|"Acquire Decryption Keys"| DRMSvc
    S3Master -.->|"Origin Fetch to Edge PoPs"| CloudFront
    S3Master -.->|"Origin Fetch to Edge PoPs"| Akamai
    S3Master -.->|"Origin Fetch to Edge PoPs"| Fastly
```

---

## Additional Notes

### Video References

* [![Amazon Prime System Design Video 1](https://img.youtube.com/vi/v6qvrIY5Tgs/0.jpg)](https://www.youtube.com/watch?v=v6qvrIY5Tgs)
* [![Amazon Prime System Design Video 2](https://img.youtube.com/vi/7hZXBrI2TjY/0.jpg)](https://www.youtube.com/watch?v=7hZXBrI2TjY)
