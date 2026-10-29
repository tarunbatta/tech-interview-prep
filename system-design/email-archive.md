# Email Archive System Design

[← Back to System Design](system-design.md)

> **Core Building Blocks Used**: [Chapter 4 — Inverted Index (Elasticsearch), Columnar OLAP & S3 Glacier Cold Tiers](fundamentals/04-databases-sql-vs-nosql.md#full-text-search--inverted-index-elasticsearch--lucene) · [Chapter 5 — Kafka Ingestion Buffering & Sharding](fundamentals/05-distributed-systems-and-messaging.md#8-asynchronous-messaging-message-queues-vs-pubsub-event-streams) · [Chapter 7 — SHA-256 Attachment Deduplication & Bloom Filters](fundamentals/07-probabilistic-structures-and-stream-processing.md#bloom-filter--cuckoo-filter-set-membership) · [Chapter 8 — KMS Envelope Encryption & Legal Hold Compliance](fundamentals/08-security-multi-region-and-deployments.md#envelope-encryption-kms-master-key--data-encryption-keys)

## Overview

An enterprise email archiving system is engineered to capture, index, preserve, and retrieve high volumes of corporate email communications for regulatory compliance (SEC Rule 17a-4, FINRA, HIPAA, GDPR) and corporate legal proceedings (eDiscovery). The system must guarantee immutability (Write Once, Read Many - WORM), protect against intentional or accidental deletion, enforce automated retention schedules, and allow legal compliance officers to execute complex boolean searches across petabytes of historical correspondence.

Designing an enterprise email archiving platform requires solving critical systems challenges:

1. **Immutable WORM Compliance**: Ensuring emails and attachments cannot be altered or deleted prior to retention expiry, even by root/administrator accounts.
2. **eDiscovery Full-Text Search Across Petabytes**: Indexing millions of raw MIME messages and document attachments (PDFs, spreadsheets, slides) with sub-second keyword search.
3. **Content-Addressed Attachment Deduplication**: Preventing storage explosion when company-wide broadcast emails are sent to 50,000 employees with a 10MB attachment.
4. **Litigation / Legal Hold State Machine**: Overriding standard retention schedules instantaneously when a legal hold is placed on specific custodians or keywords.

---

## Functional Requirements

* **Automated Ingestion (Journaling)**: Ingest all inbound, outbound, and internal emails in real time via SMTP journaling feeds from Microsoft Exchange, Office 365, and Google Workspace.
* **MIME Parsing & Attachment Extraction**: Parse RFC 822 / MIME emails, extract metadata headers (From, To, Cc, Bcc, Subject, Date, Message-ID), and isolate file attachments.
* **Global Attachment Deduplication**: Store single copies of identical attachments referenced by cryptographic hash (SHA-256).
* **eDiscovery Search**: Compliance officers can search across millions of mailboxes using boolean queries, date windows, custodians, and extracted attachment text.
* **Legal / Litigation Hold**: Place immediate, indefinite holds on specific users or topics, suspending automated deletion policies.
* **Export & Evidentiary Chain of Custody**: Export search results to EDRM XML or PST formats with cryptographic verification hashes for court presentation.

---

## Non-Functional Requirements

* **Regulatory Immutability (WORM)**: Support SEC Rule 17a-4 compliance using hardware or cloud Object Lock (compliance mode).
* **High Durability (11 9s)**: $99.999999999\%$ durability across multi-region object storage tiers.
* **High Ingestion Throughput**: Capable of ingesting over **$5,000\text{ emails/second}$** during corporate peak hours without dropping messages.
* **Audit Trail Completeness**: $100\%$ tamper-evident logging of every search query and document access made by compliance officers.
* **Storage Cost Optimization**: Automated lifecycle tiering from high-performance SSD to cold Glacier Deep Archive.

---

## Back of the Envelope Estimations

### Scale & Traffic

* **Enterprise Mailboxes**: $100,000\text{ employees}$.
* **Average Emails per Employee / Day**: $50\text{ emails/day}$.
* **Daily Ingested Emails**:
  $$100,000 \times 50 = 5,000,000\text{ emails/day}$$
* **Average Ingestion QPS**:
  $$\text{Ingestion QPS} = \frac{5,000,000}{86,400} \approx 58\text{ emails/sec} \quad (\text{Peak: } \approx 250\text{ QPS})$$

### Storage Calculations

* **Average Email Size (Headers + Body)**: $50\text{ KB}$.
* **Average Attachment Size (Weighted)**: $150\text{ KB}$ per email.
* **Daily Storage Ingestion**:
  $$5,000,000 \times 200\text{ KB} = 1,000\text{ GB} = 1\text{ Terabyte/day}$$
* **Deduplication Savings**: Deduplicating attachments saves $\approx 40\%$ storage $\implies 600\text{ GB/day net}$.
* **Annual Storage Footprint**:
  $$600\text{ GB} \times 365 \approx 219\text{ Terabytes/year}$$
* **7-Year Regulatory Retention Total**: $\approx 1.5\text{ Petabytes}$ total archive storage.

---

## High-Level System Architecture

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 25, 'rankSpacing': 45, 'padding': 10}}}%%
flowchart LR
    MailSrc["Corporate Mail (Exchange / O365)"]
    subgraph EdgeTier["Edge & Ingestion"]
        IngestGW["Journal Gateway (TLS / DKIM)"]
        Kafka[["Kafka MIME Stream"]]
    end
    subgraph Workers["Parser & Dedup Workers"]
        MIMEParser["MIME Header & Body Parser"]
        Dedup["SHA-256 Attachment Dedup"]
    end
    subgraph DataLayer["Storage & Indexing"]
        SearchIdx[("Elasticsearch Index")]
        RelDB[("Aurora PostgreSQL")]
        WORM[("S3 Object Lock (WORM)")]
    end
    MailSrc e1@-->|"SMTP Journal"| IngestGW
    IngestGW e2@-->|"Enqueue RFC 822"| Kafka
    Kafka e3@-->|"Consume"| MIMEParser
    Kafka e4@-->|"Extract Blobs"| Dedup
    MIMEParser e5@-->|"Inverted Index"| SearchIdx
    MIMEParser e6@-->|"Save Metadata"| RelDB
    Dedup e7@-->|"Write Blobs"| WORM

    e1@{ animate: true }
    e2@{ animate: true }
    e3@{ animate: true }
    e4@{ animate: true }
    e5@{ animate: true }
    e6@{ animate: true }
    e7@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class MailSrc client
    class IngestGW edge
    class Kafka queue
    class MIMEParser,Dedup worker
    class SearchIdx,RelDB,WORM storage

    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style Workers fill:transparent,stroke:#22d3ee,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataLayer fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Tiered WORM Storage Strategy

To meet strict legal compliance without exploding cloud storage costs:

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    Ingest["Email Ingested"]
    Hot["Hot: S3 Standard + ES (0-90d)"]
    Warm["Warm: Glacier Instant (91-365d)"]
    Cold["Cold: Deep Archive (1-7+ Yrs)"]
    Purge["Cryptographic Purge"]
    Ingest e1@-->|"Write"| Hot
    Hot e2@-->|"Day 91 Rule"| Warm
    Warm e3@-->|"Year 1 Rule"| Cold
    Cold e4@-->|"Expiry (No Hold)"| Purge

    e1@{ animate: true }
    e2@{ animate: true }
    e3@{ animate: true }
    e4@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff

    class Ingest client
    class Hot service
    class Warm worker
    class Cold storage
    class Purge cache
```

* **AWS S3 Object Lock in Compliance Mode**: Neither the AWS root user nor IAM administrators can delete or overwrite objects during the retention duration.
* **Legal Hold Override**: If a legal hold is applied, the S3 Object Lock `LegalHold: ON` flag is toggled, preventing deletion indefinitely regardless of retention expiration.

---

## API Specifications

### 1. Ingest Email (Internal MTA Journal Receiver)

```http
POST /api/v1/archive/ingest
Authorization: Bearer <mta_service_token>
Content-Type: message/rfc822
```

```http
// Response: 202 Accepted
{
  "messageId": "msg_9918231a-4bc1-49fa",
  "status": "QUEUED_FOR_PARSING"
}
```

### 2. Search Archived Emails (eDiscovery)

```http
POST /api/v1/ediscovery/search
Authorization: Bearer <compliance_officer_token>
Content-Type: application/json
```

```json
// Request
{
  "query": "project titan AND confidential",
  "custodians": ["ceo@company.com", "cfo@company.com"],
  "startDate": "2024-01-01T00:00:00Z",
  "endDate": "2026-10-27T00:00:00Z",
  "hasAttachments": true,
  "page": 1,
  "pageSize": 50
}

// Response: 200 OK
{
  "totalMatches": 182,
  "results": [
    {
      "archiveId": "arc_4410291",
      "subject": "RE: Project Titan Financial Forecast",
      "sender": "cfo@company.com",
      "recipients": ["ceo@company.com"],
      "sentDate": "2025-06-14T14:22:10Z",
      "hasLegalHold": true,
      "attachmentCount": 1
    }
  ]
}
```

### 3. Apply Legal Hold

```http
POST /api/v1/legal-holds
Authorization: Bearer <legal_officer_token>
Content-Type: application/json
```

```json
// Request
{
  "caseName": "SEC Investigation 2026-B",
  "matterId": "MATTER_88192",
  "custodianEmails": ["trader1@company.com", "trader2@company.com"],
  "reason": "Regulatory document subpoena",
  "targetKeywords": ["short position", "margin loan"]
}

// Response: 201 Created
{
  "holdId": "hold_99102",
  "affectedMessagesCount": 14250,
  "status": "ACTIVE_HOLD_ENFORCED"
}
```

---

## Object Model & Database Schema

### Database Selection & Rationale

| Workload / Subsystem | Database Choice | Data Model & CAP Profile | Why This Database? (Justification & Trade-offs) |
| :--- | :--- | :--- | :--- |
| **Custodians, Legal Holds & Email Headers** | **Amazon Aurora PostgreSQL** | Relational (ACID, **CP / PC-EC**) | **Why:** Placing a litigation hold (`LegalHold`) on a custodian (`Custodian`) or matter must atomically lock all associated `ArchivedEmail` rows (`isHeld = TRUE`) so background retention daemons cannot purge subpoenaed evidence. Relational ACID transactions and foreign key constraints guarantee zero race conditions between legal holds and expiration jobs. |
| **eDiscovery Full-Text & Attachment Search** | **Elasticsearch / OpenSearch** | Inverted Index (**AP / Near Real-Time**) | **Why:** Compliance officers run complex boolean queries (`"project titan" AND confidential`), proximity searches, and date-range filters across billions of email bodies and Apache Tika-extracted PDF/Office attachments. Relational `LIKE` or basic full-text indexes cannot deliver sub-second search across hundreds of terabytes of tokenized text. |
| **Immutable Raw MIME & Deduplicated Attachments** | **AWS S3 Object Lock (Compliance WORM) + S3 Glacier** | Immutable Object Storage (**11 9s Durability**) | **Why:** SEC Rule 17a-4 and FINRA require Write-Once-Read-Many (WORM) immutability where neither root admins nor attackers can alter or delete archived emails before `retentionExpiry`. S3 Object Lock enforces hardware/cloud-level WORM compliance and automatically tiers aged archives ($91+\text{ days}$) to Glacier Deep Archive. |
| **Content-Addressed Attachment Deduplication** | **Redis Cluster** | In-Memory Key-Value (**Sub-ms Lookup**) | **Why:** When a 15MB company-wide attachment is journaled across 100,000 mailboxes, ingestion workers check `EXISTS att:<sha256>` in Redis in $< 1\text{ ms}$ to store a single S3 blob and link 100,000 lightweight `Attachment` metadata rows. |
| **Compliance Access Audit Log** | **Amazon QLDB (Quantum Ledger DB)** | Cryptographic Append-Only Ledger (**CP / Immutable**) | **Why:** Every search query, preview, and EDRM/PST export performed by a compliance officer must be recorded in a tamper-evident cryptographic ledger to preserve evidentiary chain of custody in court. |

```mermaid
erDiagram
    Custodian ||--o{ ArchivedEmail : "sends_or_receives"
    Custodian ||--o{ LegalHold : "subject_to"
    ArchivedEmail ||--o{ Attachment : "contains"
    LegalHold ||--o{ ArchivedEmail : "protects"
    Custodian {
        int custodianId PK
        varchar_255 email
        varchar_100 department
        boolean isUnderHold
        timestamp createdAt
    }
    LegalHold {
        uuid holdId PK
        varchar_50 matterId
        varchar_255 caseName
        varchar_255 initiatedBy
        timestamp createdAt
        boolean isActive
    }
    ArchivedEmail {
        uuid emailId PK
        varchar_255 messageIdHeader
        int senderId FK
        varchar_500 subject
        timestamp sentAt
        varchar_500 rawS3Locator
        date retentionExpiry
        boolean isHeld
    }
    Attachment {
        uuid attachmentId PK
        uuid emailId FK
        binary_32 sha256Hash
        varchar_255 fileName
        bigint sizeBytes
        varchar_500 s3BlobUri
    }
```

---

## System Design Diagram

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph ClientTier["Sources & Compliance Users"]
        MailSrc["Corporate Mail (Exchange / O365)"]
        Auditors["Compliance Officers (eDiscovery UI)"]
    end
    subgraph EdgeTier["Edge & API Gateways"]
        IngestGW{"Ingestion Gateway (TLS 1.3, DKIM)"}
        SearchGW{"eDiscovery Gateway (RBAC, MFA)"}
    end
    subgraph CoreServices["Core Services & Workers"]
        ParserWorkers["MIME Parser & Tika Workers"]
        ExportSvc["Evidentiary Export (EDRM / PST)"]
        RetentionDaemon["Retention & Purge Worker"]
    end
    subgraph DataAndEventTier["Data Stores, Caches & WORM Storage"]
        Kafka[["Apache Kafka (Raw MIME Buffer)"]]
        DedupCache[("Redis CAS (SHA-256 Dedup)")]
        SearchEngine[("Elasticsearch (Full-Text Index)")]
        MetaDB[("Aurora PostgreSQL (Holds & Meta)")]
        AuditLog[("Immutable Audit Ledger")]
        HotS3[("S3 Standard WORM (Days 0-90)")]
        ColdS3[("S3 Glacier Archive (Years 1-7+)")]
    end
    MailSrc e1@-->|"SMTP Journal"| IngestGW
    Auditors e2@-->|"Search & Hold"| SearchGW
    IngestGW e3@-->|"Raw MIME"| Kafka
    Kafka e4@-->|"Consume Batch"| ParserWorkers
    ParserWorkers e5@-->|"Check Hash"| DedupCache
    ParserWorkers e6@-->|"Index Tokens"| SearchEngine
    ParserWorkers e7@-->|"Save Headers"| MetaDB
    ParserWorkers e8@-->|"Write WORM"| HotS3
    SearchGW e9@-->|"Boolean Query"| SearchEngine
    SearchGW e10@-->|"Enact Hold"| MetaDB
    SearchGW e11@-->|"Log Query"| AuditLog
    SearchGW e12@-->|"Trigger Export"| ExportSvc
    ExportSvc e13@-->|"Fetch Blobs"| HotS3
    RetentionDaemon e14@-->|"Check Expiry"| MetaDB
    RetentionDaemon e15@-->|"Purge Expired"| ColdS3

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

    class MailSrc,Auditors client
    class IngestGW,SearchGW edge
    class ExportSvc service
    class ParserWorkers,RetentionDaemon worker
    class DedupCache cache
    class SearchEngine,MetaDB,AuditLog,HotS3,ColdS3 storage
    class Kafka queue

    style ClientTier fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style CoreServices fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataAndEventTier fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Deep Dive & Key Technical Challenges

### 1. Global Attachment Deduplication

When the CEO emails all 100,000 employees with a 15MB video:

* A naive archiver writes $100,000 \times 15\text{ MB} = 1.5\text{ Terabytes}$.
* **Content-Addressed Storage (CAS)**:
  * Worker computes $\text{SHA-256}$ of the attachment byte stream.
  * Checks Redis: `EXISTS att:<sha256>`.
  * If the blob already exists in S3, the worker writes a lightweight pointer in the database linking the email to the existing attachment blob.
  * Only 1 copy is saved to S3, reducing storage from $1.5\text{ TB}$ to $15\text{ MB}$ ($99.999\%$ bandwidth and disk savings).

### 2. Legal Hold Priority Over Expiration Daemons

* Background lifecycle workers continuously scan for messages where `retentionExpiry < NOW()`.
* **Guard Condition**:

  ```sql
  DELETE FROM archived_emails 
  WHERE retention_expiry < NOW() 
    AND is_held = FALSE;
  ```

* Before any row or S3 object is purged, the system issues an atomic check against the `legal_holds` table. If any active case attaches to the custodian or email ID, the deletion is aborted.

---

## Additional Notes

### Client-Side User Archive Interaction Flow

For desktop/web clients supporting user-initiated email archiving (e.g. Gmail / Outlook user interface):

1. **User Action**: User clicks "Archive" on an email conversation.
2. **Client Optimistic UI Update**: The client immediately removes the email from the inbox view and places it into the local "Archive" folder for instant responsiveness.
3. **API Dispatch**: Sends `POST /api/v1/emails/{emailId}/archive` with `clientVersion` to protect against concurrent thread updates.
4. **Asynchronous Message Queue**: API Gateway enqueues an `ArchiveEmailEvent` to Kafka and returns `202 Accepted`.
5. **Worker Execution**: Worker checks version conflicts, commits `status = 'ARCHIVED'` to database, invalidates user inbox cache, and broadcasts state update via WebSockets to keep mobile and web clients synchronized.

### Video References

* [![Email Archive System Design Video 1](https://img.youtube.com/vi/PuZvF2EyfBM/0.jpg)](https://www.youtube.com/watch?v=PuZvF2EyfBM)
* [![Email Archive System Design Video 2](https://img.youtube.com/vi/ak1wTYq_opQ/0.jpg)](https://www.youtube.com/watch?v=ak1wTYq_opQ)
