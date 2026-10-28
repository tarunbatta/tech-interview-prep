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
%%{init: {'flowchart': {'nodeSpacing': 25, 'rankSpacing': 45, 'padding': 10}}}%%
flowchart LR
    Client["Client Apps (FireTV / Mobile / Web)"]
    subgraph EdgeTier["Edge & Multi-CDN"]
        CDNRouter["Dynamic CDN Router"]
        GW["API Gateway (AppSync)"]
    end
    subgraph AppBackend["Application Services"]
        EdgePoP["Edge Video PoPs (HLS / DASH)"]
        EntSvc["Entitlement Service"]
        CatSvc["Catalog Service"]
        XRaySvc["X-Ray Service"]
    end
    subgraph DataLayer["Storage & Databases"]
        S3Master[("AWS S3 (Video & DRM)")]
        DynamoEnt[("DynamoDB (Entitlements)")]
        DynamoCat[("DynamoDB / ES (Catalog)")]
        Neptune[("Amazon Neptune (Graph)")]
    end
    Client e1@-->|"1. Stream Video"| CDNRouter
    CDNRouter e2@-->|"Optimal PoP"| EdgePoP
    EdgePoP e3@-->|"Origin Pull"| S3Master
    Client e4@-->|"2. Control & X-Ray"| GW
    GW e5@-->|"Verify Plan"| EntSvc
    GW e6@-->|"Browse Titles"| CatSvc
    GW e7@-->|"Scene Data"| XRaySvc
    EntSvc e8@-->|"Global Lookup"| DynamoEnt
    CatSvc e9@-->|"Search Query"| DynamoCat
    XRaySvc e10@-->|"Graph Query"| Neptune

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
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff

    class Client client
    class CDNRouter,GW edge
    class EntSvc,CatSvc,XRaySvc service
    class EdgePoP cache
    class S3Master,DynamoEnt,DynamoCat,Neptune storage

    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style AppBackend fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataLayer fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
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
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph ClientTier["Client Devices"]
        Client["Client Apps (FireTV / Mobile / Web)"]
    end
    subgraph EdgeTier["Multi-CDN & API Gateway"]
        CloudFront["Amazon CloudFront (Primary CDN)"]
        Akamai["Akamai CDN (Failover PoP)"]
        Fastly["Fastly CDN (Live Sports)"]
        GW{"API Gateway / AppSync (Auth & GraphQL)"}
    end
    subgraph CoreServices["Core Control Plane Services"]
        EntSvc["Prime Entitlement Svc (Sub-50ms Auth)"]
        XRaySvc["Amazon X-Ray Service (Scene Metadata)"]
        WatchSvc["Playback Sync Svc (Watch History)"]
        DRMSvc["DRM License Svc (Widevine / FairPlay)"]
    end
    subgraph DataAndMediaStorage["Global Data Stores & Media"]
        S3Master[("AWS S3 & MediaConvert (AV1 / HEVC)")]
        RedisEnt[("ElastiCache Redis (Entitlement Cache)")]
        DynamoGlobal[("DynamoDB Global (Member & History)")]
        Neptune[("Amazon Neptune (X-Ray Scene Graph)")]
    end
    Client e1@-->|"HLS / DASH ABR"| CloudFront
    Client e2@-.->|"Failover Reroute"| Akamai
    Client e3@-.->|"Live Sports"| Fastly
    CloudFront e4@-.->|"Origin Fetch"| S3Master
    Akamai e5@-.->|"Origin Fetch"| S3Master
    Fastly e6@-.->|"Origin Fetch"| S3Master
    Client e7@-->|"Playback Session"| GW
    GW e8@-->|"Verify Benefit"| EntSvc
    GW e9@-->|"Fetch Scene JSON"| XRaySvc
    GW e10@-->|"Sync Position"| WatchSvc
    GW e11@-->|"License Keys"| DRMSvc
    EntSvc e12@-->|"Sub-5ms Read"| RedisEnt
    EntSvc e13@-->|"Global Sync"| DynamoGlobal
    XRaySvc e14@-->|"Graph Query"| Neptune
    WatchSvc e15@-->|"Persist State"| DynamoGlobal
    DRMSvc e16@-->|"Key Store"| S3Master

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

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff

    class Client client
    class CloudFront,Akamai,Fastly,GW edge
    class EntSvc,XRaySvc,WatchSvc service
    class DRMSvc worker
    class RedisEnt cache
    class S3Master,DynamoGlobal,Neptune storage

    style ClientTier fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style CoreServices fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataAndMediaStorage fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Additional Notes

### Video References

* [![Amazon Prime System Design Video 1](https://img.youtube.com/vi/v6qvrIY5Tgs/0.jpg)](https://www.youtube.com/watch?v=v6qvrIY5Tgs)
* [![Amazon Prime System Design Video 2](https://img.youtube.com/vi/7hZXBrI2TjY/0.jpg)](https://www.youtube.com/watch?v=7hZXBrI2TjY)
