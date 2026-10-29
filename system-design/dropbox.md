# Dropbox System Design

[← Back to System Design](system-design.md)

## Overview

Dropbox is a premier cloud storage and file synchronization platform serving over 700 million registered users and syncing billions of files daily. The core engineering mandate of Dropbox is to provide the illusion of an instant, infinite, and globally accessible local hard drive while ensuring data durability (99.999999999% - 11 9s), low sync latency, cross-device consistency, and efficient bandwidth utilization.

Designing Dropbox involves solving critical distributed systems challenges:

1. **Block-Level Chunking & Content-Addressed Storage**: Splitting large files into 4MB immutable chunks identified by their SHA-256 hash.
2. **Bandwidth Optimization & Delta Sync**: Uploading and downloading only modified chunks, saving up to 99% bandwidth on file edits.
3. **Cross-Device Metadata Sync & Conflict Resolution**: Synchronizing file tree hierarchies and resolving simultaneous offline edits gracefully without data loss.
4. **Massive-Scale Storage Tiering**: Multi-exabyte scale storage using custom blob stores (Magic Pocket / S3) combined with sharded metadata databases.

---

## Functional Requirements

* **File Upload & Sync**: Users can upload, edit, rename, move, and delete files and directories from desktop, web, and mobile clients. Changes sync automatically across all linked devices.
* **Block-Level Delta Synchronization**: Only altered blocks (chunks) of a file are transferred over the wire when an update occurs.
* **Resumable Transfers**: Large file transfers pause and resume seamlessly across network disruptions without re-uploading completed chunks.
* **File Versioning & Revision History**: Automatic tracking of file versions up to 30 days (or 180 days for enterprise tiers), supporting full rollback and undelete.
* **File Sharing & Permissions**: Sharing files/folders via public links or direct collaborative invitations (Read-Only vs. Editor permissions).
* **Offline Access & Sync**: Users can modify files while offline; local modifications sync immediately upon reconnection.

---

## Non-Functional Requirements

* **High Data Durability (11 9s)**: $99.999999999\%$ durability for all file blocks using erasure coding and cross-region replication.
* **Fast Synchronization Latency**: Small file edits ($< 10\text{ MB}$) must notify other connected devices within **$< 1\text{ second}$** under normal network conditions.
* **High Availability**: $99.99\%$ availability for metadata reads and file downloads; $99.95\%$ for block uploads.
* **Consistency Guarantee**: **Strong Consistency** for file metadata within a user's account namespace (ACID metadata updates).
* **Bandwidth & Storage Efficiency**: Global block deduplication across files and accounts to prevent redundant storage and transmission.
* **Security & Encryption**: AES-256 encryption at rest; TLS 1.3 in transit; zero-knowledge or managed envelope encryption.

---

## Back of the Envelope Estimations

### Scale & Users

* **Total Registered Users**: $700\text{ Million}$.
* **Daily Active Users (DAU)**: $100\text{ Million}$.
* **Average Devices per User**: $3\text{ devices}$ (e.g., Laptop, Phone, Tablet) $= 300\text{ Million concurrent active sync endpoints}$.
* **Average Files Synced per User / Day**: $10\text{ files/day}$.
* **Total Files Synced per Day**:
  $$100,000,000\text{ DAU} \times 10\text{ files} = 1,000,000,000\text{ sync operations/day}$$
* **Average Sync QPS**:
  $$\text{Sync QPS} = \frac{1,000,000,000}{86,400} \approx 11,570\text{ QPS} \quad (\text{Peak: } \approx 30,000\text{ QPS})$$

### Storage Estimations

* **Average File Size**: $500\text{ KB}$ to $2\text{ MB}$.
* **Chunk Size**: $4\text{ MB}$ fixed-size or content-defined chunking (Rabin fingerprints).
* **Daily Ingested Storage**:
  $$1,000,000,000\text{ updates/day} \times 2\text{ MB} = 2,000\text{ TB} = 2\text{ Petabytes/day}$$
* **Deduplication Savings**: Block-level deduplication saves $\approx 40\%$ storage:
  $$\text{Net New Storage/Day} \approx 1.2\text{ Petabytes/day} \implies \approx 438\text{ Petabytes/year}$$
* **Total Stored Data**: Over $500\text{ Petabytes}$ to $1+\text{ Exabyte}$ total footprint.

### Metadata Footprint

* Average user stores 2,000 files $\implies 700\text{M} \times 2,000 = 1.4\text{ Trillion metadata records}$.
* Record size $\approx 500\text{ bytes}$ (file name, path, block hash list, permissions, version).
* Total Metadata Size:
  $$1.4\text{ Trillion} \times 500\text{ bytes} \approx 700\text{ Terabytes}$$ (sharded across distributed relational engines).

---

## High-Level System Architecture

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 25, 'rankSpacing': 45, 'padding': 10}}}%%
flowchart LR
    Client["Client Devices (Desktop / Mobile / Web)"]
    subgraph EdgeTier["Edge & Ingress"]
        CDN["Global Edge CDN"]
        GW["API Gateway / Load Balancers"]
    end
    subgraph AppServices["Block, Metadata & Sync Services"]
        BlockSvc["Block Server (4MB SHA-256)"]
        MetaSvc["Metadata Service (File Tree)"]
        NotifSvc["Notification Svc (Long-Poll)"]
    end
    subgraph DataLayer["Storage & Sync Stream"]
        S3[("S3 / Magic Pocket (Blocks)")]
        MetaDB[("Sharded Metadata DB")]
        SyncStream[["Redis / Kafka Sync Hub"]]
    end
    Client e1@-->|"1. Upload / Download"| CDN
    CDN e2@-->|"Route Blocks"| BlockSvc
    BlockSvc e3@-->|"SHA-256 Chunks"| S3
    Client e4@-->|"2. Metadata & Sync"| GW
    GW e5@-->|"Commit Version"| MetaSvc
    GW e6@-->|"Subscribe"| NotifSvc
    MetaSvc e7@-->|"ACID Tx"| MetaDB
    MetaSvc e8@-->|"Delta Event"| SyncStream
    NotifSvc e9@-->|"Consume Hint"| SyncStream

    e1@{ animate: true }
    e2@{ animate: true }
    e3@{ animate: true }
    e4@{ animate: true }
    e5@{ animate: true }
    e6@{ animate: true }
    e7@{ animate: true }
    e8@{ animate: true }
    e9@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class Client client
    class CDN,GW edge
    class BlockSvc,MetaSvc service
    class NotifSvc worker
    class S3,MetaDB storage
    class SyncStream queue

    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style AppServices fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataLayer fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Core Components & Synchronization Workflow

### 1. Client Architecture

The local client running on macOS, Windows, or Linux consists of four collaborating threads:

* **Watcher / File Monitor**: Hooks into kernel filesystem events (`inotify` on Linux, `FSEvents` on macOS, `ReadDirectoryChangesW` on Windows) to detect file creations, modifications, moves, and deletions in the synced folder.
* **Chunker**: Splits large files into 4MB chunks. Calculates the cryptographic SHA-256 hash for every chunk.
* **Local Indexer (SQLite DB)**: Tracks local file paths, chunk hash lists, file timestamps, local version numbers, and sync status.
* **Sync Engine**: Communicates with the remote cloud servers. Queries whether chunk hashes already exist in the cloud (deduplication check), uploads missing chunks, and commits atomic metadata transactions.

### 2. Block-Level Deduplication & Delta Upload Flow

```mermaid
sequenceDiagram
    autonumber
    actor User
    participant Client as Desktop Client
    participant SQLite as Local SQLite DB
    participant Meta as Metadata Service
    participant S3 as S3 Block Store
    participant Notify as Notification Service
    participant OtherClient as User's Mobile / Laptop 2
    User->>Client: Edit 50MB File (modifies 4MB in middle)
    Client->>Client: Chunker splits into 13 chunks (4MB each)
    Client->>Client: Calculate SHA-256 for all 13 chunks
    Client->>SQLite: Compare chunk hashes with local DB
    Note over Client: Identifies ONLY Chunk #6 hash changed!
    Client->>Meta: POST /chunks/check-exists ([hash1, hash2... hash13])
    Meta-->>Client: Chunk #6 does not exist; others exist
    Client->>S3: PUT /blocks/{hash6} (Uploads only 4MB)
    S3-->>Client: 200 OK (Upload Complete)
    Client->>Meta: POST /files/commit (FileId, Version+1, [hash1..hash13])
    Meta->>Meta: Update Sharded DB (Atomic Transaction)
    Meta->>Notify: Publish FileUpdatedEvent(userId, fileId, version)
    Notify->>OtherClient: Push Sync Notification (WebSocket / Long-Poll)
    OtherClient->>Meta: GET /files/{fileId}/revisions/latest
    OtherClient->>S3: GET /blocks/{hash6} (Downloads only 4MB)
    OtherClient->>OtherClient: Reassemble local file on disk
```

---

## API Specifications

### 1. Check Chunks Existence (Deduplication)

```http
POST /api/v2/chunks/check-exists
Authorization: Bearer <user_token>
Content-Type: application/json
```

```json
// Request
{
  "chunkHashes": [
    "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
    "f7ff9e8b7bb2e09b70935a5d785e0cc5d9d0abf0e423d2da8853b0c2688746b4"
  ]
}

// Response: 200 OK
{
  "existingHashes": [
    "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855"
  ],
  "uploadUrls": {
    "f7ff9e8b7bb2e09b70935a5d785e0cc5d9d0abf0e423d2da8853b0c2688746b4": "https://s3.amazonaws.com/dropbox-blocks/f7ff...?signature=..."
  }
}
```

### 2. Commit File Version

```http
POST /api/v2/files/commit
Authorization: Bearer <user_token>
Content-Type: application/json
```

```json
// Request
{
  "path": "/Documents/Quarterly_Report.pdf",
  "baseVersion": 14,
  "chunkHashes": [
    "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
    "f7ff9e8b7bb2e09b70935a5d785e0cc5d9d0abf0e423d2da8853b0c2688746b4"
  ],
  "sizeBytes": 8388608,
  "updatedAt": "2026-10-27T19:35:00Z"
}

// Response: 200 OK
{
  "fileId": "fil_991823104",
  "version": 15,
  "status": "COMMITTED"
}
```

---

## Object Model & Database Schema

### Database Selection & Rationale

| Workload / Subsystem | Database Choice | Data Model & CAP Profile | Why This Database? (Justification & Trade-offs) |
| :--- | :--- | :--- | :--- |
| **File Tree Metadata, Versions & Shares** | **Sharded MySQL / Amazon Aurora (or Edgestore)** | Relational (ACID, **CP / PC-EC**) | **Why:** File hierarchy updates (`File`), revision commits (`FileVersion`, `FileBlock`), and Optimistic Concurrency Control (`WHERE version = baseVersion`) require strict ACID transactions and linearizable consistency within a user's namespace to prevent silent data corruption or orphaned blocks. Sharded horizontally by `ownerId` / `namespaceId`. |
| **4MB Content-Addressed File Chunks** | **Dropbox Magic Pocket / AWS S3 (+ Glacier Cold Tier)** | Immutable Object Storage (**11 9s Durability**) | **Why:** Storing $1.2+\text{ PB/day}$ of binary file chunks inside a database is cost-prohibitive. Content-addressed blob storage keyed by `SHA-256(chunk)` enables global block-level deduplication and $8+4$ Reed-Solomon erasure coding across racks, while aged file revisions ($> 30\text{ days}$) automatically tier to cold Glacier storage. |
| **Chunk Deduplication Index & Hot Metadata** | **Redis Cluster** | In-Memory Key-Value (**Sub-ms Read**) | **Why:** Before uploading a 4MB chunk, clients call `/chunks/check-exists` with an array of SHA-256 hashes. Caching known `blockHash` keys and active directory trees in Redis answers existence checks in $< 1\text{ ms}$ and offloads $>90\%$ of read QPS from the sharded MySQL metadata tier. |
| **Local Client Sync State** | **Embedded SQLite** | Local Relational (**ACID on Device**) | **Why:** Desktop and mobile sync agents need a lightweight, zero-administration embedded relational store on disk to index local file paths, modification timestamps, and `SHA-256` chunk lists for offline delta comparisons. |

```mermaid
erDiagram
    User ||--o{ File : "owns"
    User ||--o{ FileShare : "shared_with"
    File ||--o{ File : "parent_directory"
    File ||--o{ FileShare : "has_shares"
    File ||--o{ FileVersion : "has_versions"
    FileVersion ||--o{ FileBlock : "composed_of"
    User {
        bigint userId PK
        varchar_255 email
        bigint quotaBytes
        bigint usedBytes
        timestamp createdAt
    }
    File {
        bigint fileId PK
        bigint ownerId FK
        bigint parentDirId FK
        varchar_255 name
        boolean isDirectory
        boolean isDeleted
        int latestVersion
    }
    FileShare {
        bigint shareId PK
        bigint fileId FK
        bigint sharedWithUserId FK
        varchar_32 permission
        varchar_64 token
    }
    FileVersion {
        bigint versionId PK
        bigint fileId FK
        int versionNumber
        bigint totalSize
        timestamp createdAt
    }
    FileBlock {
        bigint versionId PK,FK
        int blockIndex PK
        varchar_64 blockHash
        int blockSize
    }
```

---

## System Design Diagram

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph ClientTier["Client Agents"]
        Watcher["OS File Watcher & 4MB Chunker"]
        LocalDB[("Local SQLite Index (Chunk Hashes)")]
        SyncEng["Client Sync Engine (Delta Transfer)"]
    end
    subgraph EdgeTier["Edge & API Gateway"]
        EdgeCDN["Global Edge CDN (Regional Blocks)"]
        Gateway{"API Gateway (TLS 1.3 & OAuth2)"}
    end
    subgraph CoreServices["Block, Metadata & Notification"]
        BlockSvc["Block Server (SHA-256 & Pre-Signed)"]
        SyncSvc["Sync & Metadata Svc (OCC & Forking)"]
        NotifSvc["Notification Gateway (Long-Poll/SSE)"]
    end
    subgraph DataAndEventTier["Block Store, Caches & Event Bus"]
        DedupCache[("Redis Hash Index (Dedup Cache)")]
        MagicPocket[("S3 / Magic Pocket (8+4 Erasure)")]
        ColdArchive[("Glacier Cold Store (Aged Versions)")]
        MetaCache[("Redis Cache (Hot File Trees)")]
        MetaDB[("Sharded Metadata DB (Aurora MySQL)")]
        KafkaBus[["Kafka / Redis Pub-Sub (Sync Stream)"]]
    end
    Watcher e1@-->|"File Diff"| SyncEng
    LocalDB e2@-->|"Hash Compare"| SyncEng
    SyncEng e3@-->|"4MB Blocks"| EdgeCDN
    SyncEng e4@-->|"Commit & Dedup"| Gateway
    SyncEng e5@-.->|"Long-Poll / SSE"| NotifSvc
    EdgeCDN e6@-->|"Uncached Blocks"| BlockSvc
    Gateway e7@-->|"Check Hashes"| BlockSvc
    Gateway e8@-->|"POST /commit"| SyncSvc
    BlockSvc e9@-->|"Lookup SHA-256"| DedupCache
    BlockSvc e10@-->|"PUT / GET Blocks"| MagicPocket
    BlockSvc e11@-->|"Tier History"| ColdArchive
    SyncSvc e12@-->|"Hot Trees"| MetaCache
    SyncSvc e13@-->|"OCC Commit"| MetaDB
    SyncSvc e14@-->|"Delta Event"| KafkaBus
    NotifSvc e15@-->|"Subscribe"| KafkaBus

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

    class Watcher,SyncEng client
    class EdgeCDN,Gateway edge
    class BlockSvc,SyncSvc service
    class NotifSvc worker
    class LocalDB,DedupCache,MetaCache cache
    class MagicPocket,ColdArchive,MetaDB storage
    class KafkaBus queue

    style ClientTier fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style CoreServices fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataAndEventTier fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Deep Dive & Key Technical Challenges

### 1. Conflict Resolution (The "Conflicted Copy" Model)

When two users (or the same user on laptop and desktop) edit the same file simultaneously while disconnected or with high latency:

* **Optimistic Concurrency Control (OCC)**:
  * When Client A commits version 15 with `baseVersion = 14`, the metadata service checks if the current version in DB equals 14.
  * If Client B commits first, the database advances to 15.
  * When Client A's request arrives, `baseVersion (14) != currentVersion (15)`. The commit is rejected with HTTP 409 Conflict.
* **Deterministic Forking**:
  * Instead of discarding Client A's changes or silently overwriting Client B's edits, the client automatically saves Client A's file as:
    `"Quarterly_Report (Alice's conflicted copy 2026-10-27).pdf"`.
  * The newly renamed file is committed as a new, independent file with its own chunk list, preserving 100% of user data.

### 2. High-Durability Erasure Coding in Magic Pocket

Storing 3 full replicas of exabyte-scale data triples storage hardware costs.

* **8+4 Reed-Solomon Erasure Coding**:
  * Chunks are divided into 8 data fragments and 4 parity fragments.
  * These 12 fragments are distributed across 12 distinct physical server racks across multiple power zones.
  * Any 4 racks can fail simultaneously without data loss.
  * Storage overhead is reduced from $200\%$ (3x replication) to only $50\%$ (1.5x parity overhead), saving hundreds of millions of dollars.

### 3. Notification Service & Connection Scale

* **300 Million Concurrent Connections**:
  * Maintaining 300 million open HTTP/TCP connections on application servers would exhaust file descriptors and memory.
  * Dedicated Notification Gateway cluster (built in Go / Erlang) terminates lightweight long-polling or HTTP/2 server-sent event (SSE) streams.
  * Uses a distributed Pub/Sub backplane (Kafka + Redis Channels partitioned by `user_id`).
  * Notifications are lightweight hints: `"User X, file tree changed"`. The client responds by issuing a pull to the Metadata Service, preventing the notification payload from creating memory bloat.

---

## Additional Notes

### Video References

* [![Dropbox System Design Video 1](https://img.youtube.com/vi/_UZ1ngy-kOI/0.jpg)](https://www.youtube.com/watch?v=_UZ1ngy-kOI)
* [![Dropbox System Design Video 2](https://img.youtube.com/vi/bIhXn_3jBo0/0.jpg)](https://www.youtube.com/watch?v=bIhXn_3jBo0)
