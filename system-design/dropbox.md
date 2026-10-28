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
flowchart TD
    Client["Client Devices (Desktop, Mobile, Web)"]
    subgraph BlockTier["Block Storage Tier"]
        CDN["Global Edge CDN (Regional Caching)"]
        BlockSvc["Block Server (4MB Chunking & SHA-256 Deduplication)"]
        S3["S3 / Magic Pocket (Erasure-Coded Block Store)"]
    end
    subgraph MetaTier["Metadata & Sync Tier"]
        GW["API Gateway / Load Balancers"]
        MetaSvc["Metadata Service (File Tree & Namespace)"]
        NotifSvc["Notification Service (Long-Poll / WebSocket)"]
        MetaDB[("Sharded Metadata DB (Edgestore / MySQL)")]
        SyncStream["Redis / Kafka Sync Stream Hub"]
    end
    Client -->|"1. Direct Chunk Upload/Download"| CDN
    CDN -->|"Route Blocks"| BlockSvc
    BlockSvc -->|"Content-Addressed SHA-256 Chunks"| S3
    Client -->|"2. Metadata & Long-Poll Sync"| GW
    GW -->|"Commit & Query Metadata"| MetaSvc
    GW -->|"Subscribe to File Changes"| NotifSvc
    MetaSvc -->|"ACID Transactions"| MetaDB
    MetaSvc -->|"Publish FileUpdatedEvent"| SyncStream
    SyncStream -->|"Fanout Delta Events"| NotifSvc
    NotifSvc -.->|"Push Sync Hint"| Client
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
flowchart TD
    subgraph ClientTier["Client Devices Tier"]
        Watcher["OS File Watcher (inotify / FSEvents / ReadDirectoryChangesW)"]
        Chunker["Chunker & Hasher (4MB Rabin Fingerprints & SHA-256)"]
        LocalDB[("Local SQLite Index DB (Paths, Hashes, Local Versions)")]
        SyncEng["Client Sync Engine (Deduplication & Delta Transfer)"]
        Watcher -->|"File Modified Event"| Chunker
        Chunker -->|"Compare Chunk Hashes"| LocalDB
        LocalDB -->|"Identify Changed Chunks"| SyncEng
    end
    subgraph EdgeTier["Edge & API Gateway Tier"]
        EdgeCDN["Global Edge CDN (Regional Block Caching)"]
        Gateway{"API Gateway (TLS 1.3, OAuth2, Rate Limiting)"}
    end
    subgraph BlockStoragePlane["Content-Addressed Block Storage Plane"]
        BlockSvc["Block Server (Chunking Service, SHA-256 Verification, Pre-Signed S3 URLs)"]
        DedupCache[("Redis Global Chunk Hash Index (SHA-256 Existence Cache)")]
        MagicPocket[("S3 / Magic Pocket Block Store (8+4 Reed-Solomon Erasure Coding, AES-256)")]
        ColdArchive[("Glacier / Cold Storage Tier (Aged Version History)")]
    end
    subgraph MetadataSyncPlane["Metadata & Synchronization Control Plane"]
        SyncSvc["Sync & Metadata Service (File Trees, OCC Version Checks, Conflicted Copy Engine)"]
        MetaCache[("Redis Metadata & Session Cache (Hot Namespace Trees)")]
        MetaDB[("Sharded Metadata DB (Aurora MySQL / Edgestore - Users, Files, Block Manifests)")]
    end
    subgraph NotificationPlane["Real-Time Notification Plane (300M Connections)"]
        KafkaBus["Kafka / Redis Pub-Sub Sync Stream (Partitioned by userId)"]
        NotifSvc["Notification Gateway Cluster (Long-Polling / WebSocket / SSE)"]
    end
    SyncEng -->|"1. Check Chunk Hashes & Commit Metadata"| Gateway
    SyncEng -->|"2. Upload / Download 4MB Blocks"| EdgeCDN
    EdgeCDN -->|"Forward Uncached Blocks"| BlockSvc
    Gateway -->|"POST /chunks/check-exists"| BlockSvc
    BlockSvc -->|"Lookup SHA-256"| DedupCache
    BlockSvc -->|"Multipart PUT / GET Key=SHA-256"| MagicPocket
    MagicPocket -->|"Lifecycle Tiering"| ColdArchive
    Gateway -->|"POST /files/commit (baseVersion)"| SyncSvc
    SyncSvc -->|"Read/Write Hot File Trees"| MetaCache
    SyncSvc -->|"ACID Transaction (OCC Check)"| MetaDB
    SyncSvc -->|"Publish Delta Event"| KafkaBus
    KafkaBus -->|"Route by userId"| NotifSvc
    NotifSvc -.->|"Push FileUpdated Hint"| SyncEng
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
