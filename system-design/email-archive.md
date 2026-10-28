# Email Archive System Design

[← Back to System Design](system-design.md)

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
flowchart TD
    MailSrc["Corporate Mail Servers (Exchange / O365 / Google Workspace)"]
    IngestGW["Journal Ingestion Gateway (TLS 1.3, DKIM/SPF, Envelope Decapsulation)"]
    Kafka["Ingestion Queue & Buffer (Apache Kafka Raw MIME Stream)"]
    subgraph Workers["Parser & Deduplication Workers"]
        MIMEParser["MIME Header & Body Parser (To, From, Cc, Bcc, Text)"]
        Dedup["SHA-256 Attachment Deduplicator (Content-Addressed Blobs)"]
        MIMEParser -->|"Extract Attachments"| Dedup
    end
    SearchIdx[("eDiscovery Search Index (Elasticsearch / OpenSearch)")]
    RelDB[("Relational DB (Aurora PostgreSQL - Custodians & Legal Holds)")]
    WORM[("Compliant WORM Store (S3 Object Lock & Glacier Tiering)")]
    MailSrc -->|"SMTP Journal Feed"| IngestGW
    IngestGW -->|"Enqueue Raw RFC 822"| Kafka
    Kafka -->|"Consume Stream"| MIMEParser
    MIMEParser -->|"1. Inverted Index"| SearchIdx
    MIMEParser -->|"2. Save Metadata"| RelDB
    Dedup -->|"3. Write Immutable Blobs"| WORM
```

---

## Tiered WORM Storage Strategy

To meet strict legal compliance without exploding cloud storage costs:

```mermaid
flowchart TD
    Ingest["Email Ingested"] --> Hot["Hot Tier: S3 Standard + Elasticsearch (Day 0 - 90)"]
    Hot -->|"Lifecycle Rule (Day 91)"| Warm["Warm Tier: S3 Glacier Instant Retrieval (Day 91 - 365)"]
    Warm -->|"Lifecycle Rule (Year 1)"| Cold["Cold Tier: S3 Glacier Deep Archive (Year 1 - Year 7+)"]
    Cold -->|"Lifecycle Expiration (Unless Legal Hold Active)"| Purge["Permanent Cryptographic Purge"]
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
flowchart TD
    subgraph SourcesAndUsers["External Sources & Compliance Users"]
        MailSrc["Corporate Mail Servers (Exchange / Office 365 / Google Workspace)"]
        Auditors["Legal & Compliance Officers (eDiscovery Web Console)"]
    end
    subgraph GatewayTier["Gateway & Security Layer"]
        IngestGW{"Ingestion Gateway (MTA / Journal Receiver - TLS 1.3, DKIM/SPF)"}
        SearchGW{"eDiscovery API Gateway (RBAC, MFA, Immutable Audit Query Log)"}
    end
    subgraph IngestionPipeline["Asynchronous Ingestion & Deduplication Pipeline"]
        Kafka[("Ingestion Event Stream (Apache Kafka - Zero-Loss Raw Buffer)")]
        ParserWorkers["MIME Parser & Deduplication Fleet (RFC 822 Parse, Tika Doc Text Extraction)"]
        DedupCache[("Redis CAS Index (SHA-256 Attachment Hash Lookup)")]
    end
    subgraph IndexingAndMetadata["Indexing, Metadata & Legal Hold State Machine"]
        SearchEngine[("Full-Text eDiscovery Index (Elasticsearch / OpenSearch Cluster)")]
        MetaDB[("Metadata & Legal Hold DB (Aurora PostgreSQL - Headers, Custodians, Holds)")]
        AuditLog[("Tamper-Evident Audit Log (Hash-Chained Access & Query Ledger)")]
        RetentionDaemon["Retention & Expiration Worker (Purges Expired Non-Held Records)"]
    end
    subgraph TieredWORM["Compliant Tiered WORM Storage (AWS S3 Object Lock - SEC 17a-4)"]
        HotS3[("Hot Tier: S3 Standard (Days 0-90, LegalHold Flag)")]
        WarmS3[("Warm Tier: S3 Glacier Instant Retrieval (Days 91-365)")]
        ColdS3[("Cold Tier: S3 Glacier Deep Archive (Years 1-7+)")]
        ExportSvc["Evidentiary Export Service (EDRM XML / PST with SHA-256 Manifest)"]
        HotS3 -->|"S3 Lifecycle Policy (90d)"| WarmS3
        WarmS3 -->|"S3 Lifecycle Policy (365d)"| ColdS3
    end
    MailSrc -->|"SMTP Journal Stream"| IngestGW
    Auditors -->|"Boolean Search, Hold & Export"| SearchGW
    IngestGW -->|"Publish Raw MIME"| Kafka
    Kafka -->|"Consume Batch"| ParserWorkers
    ParserWorkers -->|"EXISTS att:sha256"| DedupCache
    ParserWorkers -->|"Index Tokenized Body & Attachment Text"| SearchEngine
    ParserWorkers -->|"Commit Headers & References"| MetaDB
    ParserWorkers -->|"Store Immutable EML & Unique Attachments"| HotS3
    SearchGW -->|"Execute Boolean / Date / Custodian Query"| SearchEngine
    SearchGW -->|"Enact / Release Legal Hold"| MetaDB
    SearchGW -->|"Append Access Event"| AuditLog
    SearchGW -->|"Trigger Case Export"| ExportSvc
    ExportSvc -->|"Fetch Locked Blobs"| HotS3
    RetentionDaemon -->|"Scan retentionExpiry < NOW() AND isHeld = FALSE"| MetaDB
    RetentionDaemon -->|"Purge Eligible Objects"| ColdS3
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
