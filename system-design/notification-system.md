# Notification System Design

[← Back to System Design](system-design.md)

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
flowchart TD
    Upstreams["Upstream Services (Auth, Payment, Social)"] -->|"POST /v1/notifications"| Gateway["Ingestion API Gateway"]
    Gateway -->|"Validate & Check"| Guard["Deduplication & Preference Guard (Redis)"]
    Guard -->|"P0 Critical"| P0["Kafka Topic: P0 (OTP / Critical)"]
    Guard -->|"P1 Informational"| P1["Kafka Topic: P1 (Informational)"]
    Guard -->|"P2 Marketing"| P2["Kafka Topic: P2 (Promotions / Bulk)"]
    P0 --> Dispatcher["Dispatcher & Template Workers"]
    P1 --> Dispatcher
    P2 --> Dispatcher
    Dispatcher --> APNs["Apple APNs (HTTP/2)"]
    Dispatcher --> FCM["Google FCM (gRPC)"]
    Dispatcher --> SMS["SMS Gateway (Twilio)"]
    Dispatcher --> Email["Email Gateway (SendGrid / SES)"]
    APNs --> iOS["iOS Devices"]
    FCM --> Android["Android Devices"]
    SMS --> Carriers["SMS Carriers"]
    Email --> Inboxes["Email Inboxes"]
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
flowchart TD
    subgraph Producers["Upstream Trigger Services"]
        AuthSvc["Auth Service (2FA / OTP)"]
        PaySvc["Payment Service (Fraud / Receipts)"]
        SocialSvc["Social Graph (Messages / Mentions)"]
        MktgSvc["Marketing Platform (Campaigns / Promos)"]
    end
    subgraph IngestionTier["Ingestion & Guard Layer"]
        IngestAPI["Notification Ingestion API (Rate Limiting, Schema Validation, Idempotency Check)"]
        DedupeGuard["Deduplication & Rate Guard (Redis Sliding Window: 5-min Dedupe, Quiet Hours, Frequency Cap)"]
        DelayedQueue["Delayed Notification Scheduler (Redis ZSet: Quiet Hours Wakeup)"]
    end
    subgraph PriorityQueues["Isolated Priority Queue Hub (Apache Kafka)"]
        KafkaP0["Kafka P0: Critical Queue (OTP, Security, Payment Alerts - SLA < 1s)"]
        KafkaP1["Kafka P1: Informational Queue (Friend Requests, Direct Messages - SLA < 10s)"]
        KafkaP2["Kafka P2: Marketing / Promo Queue (Discounts, Weekly Digests - Off-Peak Batched)"]
        KafkaDLQ["Kafka Dead-Letter Queue (DLQ) & Token Cleanup Topic"]
    end
    subgraph DispatchTier["Notification Dispatcher & Enrichment Tier"]
        Dispatcher["Notification Dispatcher Fleet (Template Hydration, Localization, Token Resolution)"]
        UserDB[("User Preference & Device DB (PostgreSQL / DynamoDB: APNs/FCM Tokens, Opt-Outs, Locale)")]
        CircuitBreaker["Dynamic Circuit Breaker & Fallback Router (Resilience4j / Envoy Outlier Detection)"]
    end
    subgraph Gateways["Downstream Channel Gateways & Providers"]
        APNs["Apple APNs Gateway (iOS Push - Persistent HTTP/2)"]
        FCM["Firebase FCM Gateway (Android Push - Multiplexed gRPC)"]
        SMS["SMS Gateway (Twilio Primary / Sinch Failover)"]
        Email["Email Gateway (SendGrid Primary / Amazon SES Failover)"]
        InApp["In-App Notification Hub (WebSockets / SSE Cluster)"]
    end
    AuthSvc -->|"POST /v1/notifications"| IngestAPI
    PaySvc -->|"POST /v1/notifications"| IngestAPI
    SocialSvc -->|"POST /v1/notifications"| IngestAPI
    MktgSvc -->|"POST /v1/notifications"| IngestAPI
    IngestAPI <-->|"SET NX EX 300 / Check DND"| DedupeGuard
    DedupeGuard -->|"Defer During Quiet Hours (P1/P2)"| DelayedQueue
    DelayedQueue -->|"Wake Timestamp Reached"| IngestAPI
    IngestAPI -->|"Route P0 (Bypass DND)"| KafkaP0
    IngestAPI -->|"Route P1"| KafkaP1
    IngestAPI -->|"Route P2"| KafkaP2
    KafkaP0 -->|"Consume Critical"| Dispatcher
    KafkaP1 -->|"Consume Informational"| Dispatcher
    KafkaP2 -->|"Consume Bulk"| Dispatcher
    Dispatcher <-->|"Fetch Preferences & Active Tokens"| UserDB
    Dispatcher -->|"Dispatch Hydrated Payloads"| CircuitBreaker
    CircuitBreaker -->|"iOS Push"| APNs
    CircuitBreaker -->|"Android Push"| FCM
    CircuitBreaker -->|"Rate-Limited SMS"| SMS
    CircuitBreaker -->|"Circuit-Protected Email"| Email
    CircuitBreaker -->|"Real-Time Stream"| InApp
    APNs -.->|"410 Unregistered / BadDeviceToken"| KafkaDLQ
    FCM -.->|"Stale Token / Delivery Error"| KafkaDLQ
    KafkaDLQ -.->|"Prune Dead Tokens (is_active = FALSE)"| UserDB
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
