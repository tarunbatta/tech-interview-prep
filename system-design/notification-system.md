# Notification System Design

[← Back to System Design](system-design.md)

> **Core Building Blocks Used**: [Chapter 3 — KEDA Queue-Depth Worker Auto-Scaling](fundamentals/03-caching-and-scaling.md#3-auto-scaling-nodes-in-production) · [Chapter 5 — Priority Message Queues (Kafka/SQS), Transactional Outbox & Idempotency](fundamentals/05-distributed-systems-and-messaging.md#8-asynchronous-messaging-message-queues-vs-pubsub-event-streams) · [Chapter 6 — Token Bucket Rate Limiting, Circuit Breakers & Exponential Backoff + Jitter](fundamentals/06-resiliency-observability-and-metrics.md#2-resiliency-fault-tolerance--reliability-patterns)

## Overview

A modern distributed notification system serves as the mission-critical communication backbone for consumer and enterprise platforms. It delivers hundreds of millions of notifications daily across multiple communication channels—Mobile Push (iOS APNs, Android FCM), SMS, Email, and In-App WebSockets.

Designing an enterprise-grade notification platform presents rigorous distributed engineering challenges:

1. **Multi-Channel Fan-Out & Rate Heterogeneity**: Pushing high-volume bursts to external provider gateways with wildly varying throughput limits (e.g. APNs handles millions of concurrent HTTP/2 streams, while SMS carrier aggregators strictly throttle to 100 SMS/sec per shortcode).
2. **Strict Priority Queuing & SLA Isolation**: Ensuring promotional marketing campaigns never delay time-sensitive security alerts (e.g., 2FA verification codes, fraud warnings).
3. **Deduplication & User Fatigue Controls**: Preventing duplicate notifications across system retries and enforcing quiet hours (Do Not Disturb) and frequency caps.
4. **Third-Party Fault Tolerance**: Handling sudden downtime or throttling from external gateways with automated circuit breaking and dynamic fallback.

---

## Functional Requirements

* **Multi-Channel Delivery**: Support iOS Push (Apple APNs), Android Push (Firebase Cloud Messaging - FCM), SMS (Twilio, Sinch), Email (Amazon SES, SendGrid), and In-App persistent notifications.
* **Priority Tiers**:
  * **P0 (Critical)**: Two-Factor Auth (OTP), fraud alerts, password resets. Delivered in $< 1\text{ second}$.
  * **P1 (Informational)**: Direct messages, ride arrival updates, order status. Delivered in $< 10\text{ seconds}$.
  * **P2 (Marketing / Bulk)**: Promotional discounts, product recommendations, newsletters. Batched during off-peak hours.
* **User Preferences & Consent**: Users can opt in/out per channel and category, specify quiet hours in their local timezone, and set global daily frequency caps.
* **Template Hydration & Localization**: Dynamic templates parameterized with localized strings, currency formats, and user variables.
* **Delivery & Engagement Tracking**: Track end-to-end status (`queued`, `sent`, `delivered`, `failed`, `opened`, `clicked`).

---

## Non-Functional Requirements

* **Low Latency**: P0 notifications delivered to external provider gateways in **$< 500\text{ ms}$** P99.
* **High Throughput**: Capable of ingesting and dispatching over **$100,000\text{ notifications/second}$** during flash sales or breaking news.
* **Zero Lost Critical Alerts**: At-least-once delivery guarantee for all transactional notifications with dead-letter queue (DLQ) support.
* **Deduplication Guarantee**: Deduplicate identical messages sent within a configurable sliding window (e.g., 5 minutes).
* **Fault Isolation**: Outages or latency spikes in one channel (e.g., SMS provider outage) must never cascade to push or email channels.

---

## Back of the Envelope Estimations

### Traffic & Scale

* **Daily Active Users (DAU)**: $100\text{ Million}$.
* **Average Notifications per User / Day**: $5\text{ notifications/day}$.
* **Daily Notification Volume**:
  $$100,000,000\text{ users} \times 5\text{ alerts} = 500,000,000\text{ notifications/day}$$
* **Average Delivery QPS**:
  $$\text{Average QPS} = \frac{500,000,000}{86,400} \approx 5,787\text{ QPS}$$
* **Peak Delivery QPS (Breaking News / Flash Sale)**: $15\times$ average $= \approx 85,000\text{ QPS}$.

### Channel Distribution

* **Push (APNs / FCM)**: $60\% \implies 300\text{M/day} \quad (\approx 50,000\text{ peak QPS})$.
* **Email (SES / SendGrid)**: $25\% \implies 125\text{M/day} \quad (\approx 21,000\text{ peak QPS})$.
* **SMS (Twilio / Sinch)**: $10\% \implies 50\text{M/day} \quad (\approx 8,500\text{ peak QPS})$.
* **In-App WebSockets**: $5\% \implies 25\text{M/day} \quad (\approx 4,200\text{ peak QPS})$.

---

## High-Level System Architecture

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 25, 'rankSpacing': 45, 'padding': 10}}}%%
flowchart LR
    Upstreams["Upstream Services (Auth, Pay, Social)"]
    subgraph EdgeTier["Ingestion & Guard"]
        Gateway["Ingestion API Gateway"]
        Guard[("Redis Dedupe & Pref Guard")]
    end
    subgraph QueueAndWorkers["Priority Queues & Dispatcher"]
        P0[["Kafka P0: Critical (OTP)"]]
        P1[["Kafka P1: Informational"]]
        P2[["Kafka P2: Marketing Bulk"]]
        Dispatcher["Dispatcher & Template Workers"]
    end
    subgraph Providers["Channel Gateways"]
        APNs["Apple APNs (iOS Push)"]
        FCM["Google FCM (Android Push)"]
        SMS["SMS Gateway (Twilio)"]
        Email["Email Gateway (SendGrid)"]
    end
    Upstreams e1@-->|"POST /notify"| Gateway
    Gateway e2@<-->|"Dedupe & DND"| Guard
    Gateway e3@-->|"P0 (<1s)"| P0
    Gateway e4@-->|"P1 (<10s)"| P1
    Gateway e5@-->|"P2 (Batch)"| P2
    P0 e6@-->|"Consume"| Dispatcher
    P1 e7@-->|"Consume"| Dispatcher
    P2 e8@-->|"Consume"| Dispatcher
    Dispatcher e9@-->|"HTTP/2"| APNs
    Dispatcher e10@-->|"gRPC"| FCM
    Dispatcher e11@-->|"Shortcode"| SMS
    Dispatcher e12@-->|"SMTP / API"| Email

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

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class Upstreams client
    class Gateway edge
    class Guard cache
    class P0,P1,P2 queue
    class Dispatcher worker
    class APNs,FCM,SMS,Email storage

    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style QueueAndWorkers fill:transparent,stroke:#818cf8,stroke-width:1.5px,stroke-dasharray: 5 5
    style Providers fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Core Components & Processing Pipelines

### 1. Deduplication Engine (Redis Sliding Window)

Network glitches often cause upstream microservices to retry requests, threatening duplicate text messages or duplicate pushes.

* **Fingerprint Generation**:
  $$\text{DedupeKey} = \text{MD5}(user\_id + \text{":"} + notification\_type + \text{":"} + content\_hash)$$
* **Atomic Redis Guard**:

  ```text
  SET dedupe:user_102:order_shipped:a819b "1" NX EX 300
  ```

  * If key exists, the request returns immediately with `200 OK (Deduplicated)` without queuing another alert.

### 2. Quiet Hours (Do Not Disturb) Evaluation

* Users frequently set quiet hours (e.g., $10\text{ PM}$ to $8\text{ AM}$).
* **Timezone Offset Table**:
  * The system maintains the user's localized timezone offset in their profile.
  * For **P0 (Critical)**: Quiet hours are bypassed entirely (user needs the fraud code immediately).
  * For **P1 / P2**: If current local time falls in the quiet window, the message is scheduled into a delayed Kafka queue or Redis Sorted Set (`ZADD scheduled_notifications <wake_timestamp> <payload>`) to wake when quiet hours expire.

---

## API Specifications

### 1. Send Notification

```http
POST /v1/notifications
Authorization: Bearer <service_token>
Content-Type: application/json
```

```json
// Request
{
  "recipientUserId": "usr_9918231a",
  "priority": "P0",
  "templateId": "tpl_two_factor_auth",
  "channels": ["SMS", "PUSH"],
  "parameters": {
    "authCode": "849201",
    "expirationMinutes": 5
  },
  "idempotencyKey": "idem_auth_9918231_202610271945"
}

// Response: 202 Accepted
{
  "notificationId": "notif_8819201389",
  "status": "QUEUED",
  "estimatedDeliveryMs": 350
}
```

### 2. Update User Notification Preferences

```http
PUT /v1/users/{userId}/preferences
Authorization: Bearer <user_token>
Content-Type: application/json
```

```json
// Request
{
  "channels": {
    "push": true,
    "email": true,
    "sms": false
  },
  "quietHours": {
    "enabled": true,
    "start": "22:00",
    "end": "08:00",
    "timezone": "America/New_York"
  },
  "marketingOptOut": true
}

// Response: 200 OK
{
  "updatedAt": "2026-10-27T19:46:00Z"
}
```

---

## Object Model & Database Schema

### Database Selection & Rationale

| Workload / Subsystem | Database Choice | Data Model & CAP Profile | Why This Database? (Justification & Trade-offs) |
| :--- | :--- | :--- | :--- |
| **User Profiles, Preferences & Device Tokens** | **Amazon Aurora PostgreSQL (or Sharded MySQL)** | Relational (ACID, **CP / PC-EC**) | **Why:** `User`, `DeviceToken`, and `UserPreference` (opt-in/out flags, quiet hours, timezone) require strong consistency (`CP`) so that when a user opts out of marketing SMS or revokes a stale APNs/FCM token, the change is respected immediately with zero compliance violations (TCPA/GDPR). Read-heavy worker lookups are cached in Redis with write-through invalidation. |
| **5-Minute Deduplication Guard & Rate Caps** | **Redis Cluster** | In-Memory Key-Value / ZSet (**CP on Primary**) | **Why:** At $85,000\text{ Peak QPS}$, checking whether an upstream retry is a duplicate or exceeds a user's daily frequency cap must happen in $< 1\text{ ms}$. Redis `SET dedupe:<md5> 1 NX EX 300` atomically rejects duplicate alerts in memory, while Redis Sorted Sets (`ZADD`) schedule deferred notifications until a user's local Quiet Hours expire. |
| **Notification Delivery Audit Log (`NotificationLog`)** | **Apache Cassandra / Amazon DynamoDB** | Wide-Column / Key-Value (**AP / PA-EL**) | **Why:** Recording $500\text{ Million+}$ status transitions per day (`queued` $\to$ `sent` $\to$ `delivered` $\to$ `opened`) generates massive append-heavy write traffic. Cassandra/DynamoDB partitions by `userId` (with `deliveredAt` clustering key) or `notifId`, absorbing tens of thousands of writes/sec without relational B-Tree index fragmentation and supporting automatic TTL expiration (e.g., 90 days). |

```mermaid
erDiagram
    User ||--o{ DeviceToken : "owns"
    User ||--o{ NotificationLog : "receives"
    User ||--o{ UserPreference : "configures"
    User {
        bigint userId PK
        varchar_255 email
        varchar_50 phoneNumber
        varchar_50 timezone
        timestamp createdAt
    }
    DeviceToken {
        varchar_128 deviceId PK
        bigint userId FK
        enum_platform platform
        text token
        boolean isActive
        timestamp lastSeenAt
    }
    NotificationLog {
        uuid notifId PK
        bigint userId FK
        enum_channel channel
        enum_priority priority
        enum_status status
        varchar_128 providerRefId
        timestamp deliveredAt
    }
    UserPreference {
        bigint userId PK,FK
        enum_channel channel PK
        enum_category category PK
        boolean isEnabled
        time quietStart
        time quietEnd
    }
```

---

## System Design Diagram

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph Producers["Trigger Services"]
        AuthSvc["Auth & Payment (P0 OTP / Fraud)"]
        SocialSvc["Social & Marketing (P1 / P2)"]
    end
    subgraph IngestionTier["Ingestion & Priority Queues"]
        IngestAPI{"Notification Ingestion Gateway"}
        KafkaP0[["Kafka P0: Critical (<1s SLA)"]]
        KafkaP1[["Kafka P1/P2: Info & Bulk"]]
        KafkaDLQ[["Kafka DLQ & Token Cleanup"]]
    end
    subgraph DispatchTier["Guard, Enrichment & Routing"]
        DedupeGuard[("Redis Guard (5m Dedupe & DND)")]
        UserDB[("User Pref & Token DB (Postgres)")]
        Dispatcher["Dispatcher & Template Workers"]
    end
    subgraph Gateways["Downstream Channel Gateways"]
        APNs["Apple APNs (iOS HTTP/2)"]
        FCM["Firebase FCM (Android gRPC)"]
        SMS["SMS Gateway (Twilio / Sinch)"]
        Email["Email Gateway (SendGrid / SES)"]
        InApp["In-App Hub (WebSockets / SSE)"]
    end
    AuthSvc e1@-->|"POST P0"| IngestAPI
    SocialSvc e2@-->|"POST P1/P2"| IngestAPI
    IngestAPI e3@<-->|"SETNX & Quiet Hrs"| DedupeGuard
    IngestAPI e4@-->|"Enqueue P0"| KafkaP0
    IngestAPI e5@-->|"Enqueue P1/P2"| KafkaP1
    KafkaP0 e6@-->|"Consume P0"| Dispatcher
    KafkaP1 e7@-->|"Consume P1/P2"| Dispatcher
    Dispatcher e8@<-->|"Tokens & Prefs"| UserDB
    Dispatcher e9@-->|"iOS Push"| APNs
    Dispatcher e10@-->|"Android Push"| FCM
    Dispatcher e11@-->|"Circuit SMS"| SMS
    Dispatcher e12@-->|"Circuit Email"| Email
    Dispatcher e13@-->|"Live Stream"| InApp
    APNs e14@-.->|"410 Stale Token"| KafkaDLQ
    FCM e15@-.->|"Unregistered"| KafkaDLQ
    KafkaDLQ e16@-.->|"Prune Token"| UserDB

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
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class AuthSvc,SocialSvc client
    class IngestAPI edge
    class KafkaP0,KafkaP1,KafkaDLQ queue
    class DedupeGuard cache
    class UserDB,APNs,FCM,SMS,Email,InApp storage
    class Dispatcher worker

    style Producers fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style IngestionTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style DispatchTier fill:transparent,stroke:#22d3ee,stroke-width:1.5px,stroke-dasharray: 5 5
    style Gateways fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Deep Dive & Key Technical Challenges

### 1. Handling Third-Party Outages with Dynamic Circuit Breakers

If an external provider (e.g., SendGrid) experiences an outage:

* **Circuit Breaker Pattern**:
  * Track error rates using resilience patterns (e.g., Resilience4j or Envoy Outlier Detection).
  * If failure rate exceeds $15\%$ over a 30-second rolling window, trip the circuit to **OPEN**.
* **Automated Fallback**:
  * Route all pending emails to secondary provider (Amazon SES).
  * Periodically send $1\%$ canary traffic to SendGrid. Once canary succeeds, transition circuit to **HALF-OPEN** then **CLOSED**.

### 2. Push Notification Token Invalidation & Churn

* When users delete an app or get a new phone, device tokens become stale.
* APNs and FCM return specific error codes: `410 Unregistered` or `BadDeviceToken`.
* Dispatcher workers publish invalidation events to a cleanup topic:

  ```sql
  UPDATE device_tokens SET is_active = FALSE WHERE token = 'stale_token_xyz';
  ```

* Pruning dead tokens prevents sending wasted network calls to Apple/Google servers.

---

## Additional Notes

### Video References

* [![Notification System Design Video 1](https://img.youtube.com/vi/bBTPZ9NdSk8/0.jpg)](https://www.youtube.com/watch?v=bBTPZ9NdSk8)
* [![Notification System Design Video 2](https://img.youtube.com/vi/J-5JozlYIqI/0.jpg)](https://www.youtube.com/watch?v=J-5JozlYIqI)
