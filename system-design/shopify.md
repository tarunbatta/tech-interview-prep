# Shopify System Design

[← Back to System Design](system-design.md)

> **Core Building Blocks Used**: [Chapter 3 — Redis Atomic Flash-Sale Inventory Decrements (`DECRBY` Lua)](fundamentals/03-caching-and-scaling.md#caching-failure-modes--interview-mitigations) · [Chapter 4 — Pod-Sharded Multi-Tenant MySQL & Optimistic Locking](fundamentals/04-databases-sql-vs-nosql.md#sharding--horizontal-partitioning-scaling-writes--storage) · [Chapter 5 — Checkout Saga Pattern & Transactional Outbox](fundamentals/05-distributed-systems-and-messaging.md#4-distributed-transactions-2pc-vs-saga-vs-transactional-outbox) · [Chapter 8 — Cell-Based Pod Architecture (Blast-Radius Isolation)](fundamentals/08-security-multi-region-and-deployments.md#cell-based-architecture--shuffle-sharding-blast-radius-isolation)

## Overview

Shopify is a multi-tenant commerce operating system powering over 4 million active merchants in 175+ countries, processing over $200+ billion in gross merchandise volume (GMV) annually. During global shopping events like Black Friday / Cyber Monday (BFCM), the platform withstands peak traffic exceeding $100,000+\text{ orders/minute}$ and $30+\text{ Terabytes/minute}$ of edge egress.

Designing Shopify requires solving core distributed e-commerce challenges:

1. **Multi-Tenant Pod Architecture**: Isolating thousands of merchants per autonomous, self-contained infrastructure pod to eliminate catastrophic single points of failure.
2. **The "Noisy Neighbor" Problem & Flash Sales**: Protecting normal small businesses from being knocked offline when a celebrity merchant (e.g. Kylie Cosmetics) launches a viral flash sale.
3. **High-Concurrency Inventory Reservation**: Preventing inventory overselling during concurrent checkout races without crippling relational database write locks.
4. **Resilient Webhook Dispatching**: Dispersing billions of asynchronous events to third-party app developers and logistics integrations.

---

## Functional Requirements

* **Multi-Store Management**: Merchants can create custom storefronts, manage product catalogs, variants (size, color, SKU), pricing, and discount codes.
* **Storefront Browsing**: Customers can browse catalog items, search products, and add items to a shopping cart with sub-second latency.
* **High-Concurrency Checkout**: Real-time checkout with inventory reservation, tax calculation, shipping rate estimation, and payment processing.
* **Flash Sale Waiting Room**: Regulate traffic spikes during flash sales by queueing shoppers in a virtual waiting room before entering checkout.
* **Order Processing & Fulfillment**: Tracking order lifecycle (`Pending`, `Paid`, `Unfulfilled`, `Fulfilled`, `Refunded`).
* **App Ecosystem Webhooks**: Trigger signed webhooks to external logistics, ERP, and dropshipping applications on state changes (`orders/create`, `inventory_levels/update`).

---

## Non-Functional Requirements

* **High Availability (99.99%)**: Storefront browsing must remain active even during heavy backend checkout surges.
* **Zero Overselling (Strict Consistency)**: Under no circumstances may an item with 10 units in stock be purchased by 11 customers.
* **Sub-Second Latency**: P95 storefront page load latency **$< 250\text{ ms}$** via edge CDN caching.
* **Horizontal Scalability**: Scalable to withstand $15\times$ baseline traffic surges during Black Friday.
* **Tenant Isolation**: Complete data and operational isolation; one merchant's traffic spike or bad script must never affect another merchant's store.

---

## Back of the Envelope Estimations

### Scale & Traffic

* **Active Stores**: $4\text{ Million merchants}$.
* **Average Daily Visits**: $500\text{ Million page views/day}$.
* **Baseline Orders / Day**: $15\text{ Million orders/day} \approx 175\text{ orders/second}$.
* **Peak Event (BFCM)**:
  * Orders per minute: $120,000\text{ orders/min} \implies 2,000\text{ orders/second}$.
  * Peak HTTP Request QPS: $1,200,000\text{ QPS}$ at CDN edge; $120,000\text{ QPS}$ origin traffic.
* **Checkout Conversion Ratio**: $100\text{ views} \implies 10\text{ cart adds} \implies 1-2\text{ checkouts completed}$.

### Storage Calculations

* **Products Catalog**: 4M stores $\times$ 500 products $\approx 2\text{ Billion product records}$.
* **Product Record Size**: $\approx 2\text{ KB}$ (Title, description, SKU list, pricing, media URLs).
* **Catalog Database Size**:
  $$2,000,000,000 \times 2\text{ KB} = 4\text{ Terabytes}$$ (replicated and cached).
* **Order History Storage**:
  * 15M orders/day $\times 4\text{ KB per order} \approx 60\text{ GB/day} \implies \approx 22\text{ Terabytes/year}$.

---

## High-Level System Architecture (Pod Architecture)

Shopify's groundbreaking engineering architecture partitions the entire platform into isolated **Pods**:

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 25, 'rankSpacing': 45, 'padding': 10}}}%%
flowchart LR
    Shoppers["Shoppers & Buyers (Web / Mobile)"]
    subgraph EdgeTier["Edge & Pod Routing"]
        Edge["Edge CDN & Router (shop_id -> Pod)"]
        WaitRoom["Virtual Waiting Room (Pod 2)"]
    end
    subgraph PodApps["Pod Application Fleets"]
        App1["Storefront App Fleet (Pod 1)"]
        App2["Flash Sale App Fleet (Pod 2)"]
    end
    subgraph PodStorage["Pod Storage & Shared Services"]
        Redis1[("Pod 1 Redis & MySQL")]
        DB2[("Pod 2 Redis & MySQL")]
        Payments["Shopify Payments / Stripe"]
        Webhooks[["Webhook Pipeline (Kafka)"]]
    end
    Shoppers e1@-->|"HTTP Requests"| Edge
    Edge e2@-->|"Standard Shop"| App1
    Edge e3@-->|"Flash Sale"| WaitRoom
    WaitRoom e4@-->|"Admit Batch"| App2
    App1 e5@-->|"Lock & Commit"| Redis1
    App2 e6@-->|"EVALSHA & Write"| DB2
    App1 e7@-->|"2-Phase Auth"| Payments
    App2 e8@-->|"2-Phase Auth"| Payments
    Redis1 e9@-->|"Order Events"| Webhooks
    DB2 e10@-->|"Order Events"| Webhooks

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
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class Shoppers client
    class Edge,WaitRoom edge
    class App1,App2,Payments service
    class Redis1,DB2 storage
    class Webhooks queue

    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style PodApps fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style PodStorage fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Core Components & Mechanics

### 1. The Pod Architecture & Noisy Neighbor Isolation

* A **Pod** is a fully functional, self-contained instance of Shopify containing:
  * Application server worker pools (Ruby on Rails / Puma).
  * Sharded MySQL database clusters (Vitess / MySQL master-replicas).
  * In-memory Redis cache and key-value locks.
* **Routing**: The Global Ingress proxy inspects the incoming Host header (`gymshark.com` or `shop1.myshopify.com`), queries an in-memory routing table, and forwards traffic directly to Pod 42.
* **Flash Sale Isolation**: If a merchant announces a flash sale, Shopify operations seamlessly migrate that merchant to a dedicated Pod with pre-warmed Redis clusters, ensuring other merchants experience zero latency degradation.

### 2. High-Concurrency Inventory Reservation Flow

During a flash sale where 50,000 customers fight for 100 sneaker pairs, relational SQL locks (`SELECT FOR UPDATE`) cause thread pool exhaustion and lock wait timeouts.

```mermaid
sequenceDiagram
    autonumber
    actor Shopper
    participant CDN as Edge CDN / Waiting Room
    participant API as Checkout Service
    participant Redis as Redis Inventory Cluster
    participant DB as MySQL Pod DB
    participant Stripe as Shopify Payments
    Shopper->>CDN: Click "Checkout Now"
    CDN->>API: POST /api/checkout/reserve (sku_102, qty: 1)
    API->>Redis: EVALSHA lua_reserve_inventory (sku_102, qty: 1)
    alt Inventory Available (Stock >= 1)
        Redis->>Redis: Decrement Stock & Store 10-Min Reservation Token
        Redis-->>API: 200 OK (Reservation Token: res_99182a)
        API-->>Shopper: Reservation Secured (10-Min Timer Begins)
        Shopper->>API: POST /api/checkout/pay (res_99182a, CardDetails)
        API->>Stripe: Charge Card (Idempotent Key)
        Stripe-->>API: Payment Succeeded
        API->>DB: INSERT INTO orders & Permanently Deduct Stock
        API->>Redis: Confirm Token Consumed
        API-->>Shopper: Order Confirmed (Receipt #88192)
    else Out of Stock (Stock == 0)
        Redis-->>API: 0 Available
        API-->>Shopper: 409 Conflict ("Sorry, item sold out!")
    end
```

---

## API Specifications

### 1. Reserve Inventory (Checkout Step 1)

```http
POST /api/v1/checkouts/reserve
Authorization: Bearer <cart_session_token>
Content-Type: application/json
```

```json
// Request
{
  "shopId": "shp_99182",
  "items": [
    {"variantId": "var_881920", "quantity": 1}
  ],
  "idempotencyKey": "idem_chk_182910a"
}

// Response: 200 OK
{
  "reservationToken": "tok_res_88192a",
  "reservationExpiresAt": 1730007000,
  "subtotal": 120.00,
  "status": "RESERVED"
}
```

### 2. Complete Order & Payment

```http
POST /api/v1/checkouts/complete
Authorization: Bearer <cart_session_token>
Content-Type: application/json
```

```json
// Request
{
  "reservationToken": "tok_res_88192a",
  "paymentMethodId": "pm_card_visa_4242",
  "shippingAddress": {
    "name": "Jane Doe",
    "street": "123 Market St",
    "city": "San Francisco",
    "zip": "94105"
  }
}

// Response: 201 Created
{
  "orderId": "ord_881923104",
  "orderNumber": "#1042",
  "financialStatus": "PAID",
  "fulfillmentStatus": "UNFULFILLED",
  "totalPrice": 132.50
}
```

---

## Object Model & Database Schema

### Database Selection & Rationale

| Workload / Subsystem | Database Choice | Data Model & CAP Profile | Why This Database? (Justification & Trade-offs) |
| :--- | :--- | :--- | :--- |
| **Per-Pod Merchant Data (`Shop`, `Product`, `Order`)** | **Pod-Isolated MySQL / Vitess Clusters** | Sharded Relational (ACID, **CP / PC-EC**) | **Why:** Orders, payments, refunds, and merchant catalog mutations require strict relational ACID guarantees. Rather than a single monolithic database (where one viral merchant's lock contention brings down all 4M stores), Shopify partitions MySQL into self-contained **Pods** keyed by `shopId`. All rows for a given merchant live inside the same Pod (`podId`), enabling fast local SQL joins (`Order` $\bowtie$ `ProductVariant`) with complete blast-radius isolation. |
| **Flash-Sale Inventory Reservation & Pod Cache** | **Pod-Isolated Redis Cluster (Atomic Lua)** | In-Memory Key-Value (**CP on Primary**) | **Why:** During Black Friday / Kylie Cosmetics flash sales ($50,000$ concurrent buyers for 100 units), SQL `SELECT ... FOR UPDATE` on `ProductVariant.inventoryQuantity` causes severe thread-pool exhaustion. Executing an atomic Redis Lua script (`EVALSHA`) decrements inventory and issues a 10-minute reservation token in microseconds before touching MySQL. |
| **Storefront HTML & Liquid Fragment Cache** | **Cloudflare Edge CDN + Redis** | Edge Object / KV Cache (**AP / Sub-50ms**) | **Why:** Absorbs $1.2\text{M QPS}$ of storefront browsing traffic ($100:1$ browse-to-checkout ratio) at the network edge, with instant cache-tag purge ($< 150\text{ ms}$) triggered via MySQL CDC when a merchant updates a product price. |
| **Cross-Shop Analytics & Merchant Reporting** | **Google BigQuery / Apache Iceberg (via Kafka CDC)** | Cloud Data Warehouse / OLAP (**Analytical**) | **Why:** Merchants query historical sales trends, cohort retention, and BFCM live globes without running heavy analytical `GROUP BY` scans on transactional Pod MySQL databases. |

```mermaid
erDiagram
    Shop ||--o{ Product : "lists"
    Shop ||--o{ Order : "processes"
    Product ||--o{ ProductVariant : "has"
    Shop {
        bigint shopId PK
        varchar_100 name
        varchar_255 domain
        int podId
        varchar_3 currency
        timestamp createdAt
    }
    Product {
        bigint productId PK
        bigint shopId FK
        varchar_255 title
        text bodyHtml
        varchar_100 vendor
        timestamp publishedAt
    }
    ProductVariant {
        bigint variantId PK
        bigint productId FK
        varchar_64 sku
        decimal price
        int inventoryQuantity
    }
    Order {
        bigint orderId PK
        bigint shopId FK
        bigint customerId FK
        decimal totalAmount
        enum_status financialStatus
        timestamp createdAt
    }
```

---

## System Design Diagram

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph Users["Clients & Admins"]
        Buyers["Shoppers & Mobile Buyers"]
        Merchants["Merchants & Store Admins"]
    end
    subgraph EdgeRouting["Edge CDN & Pod Routing"]
        CDN["Global Edge CDN (Liquid Cache)"]
        RoutingGW{"Global Routing GW (shop_id)"}
        WaitRoom["Virtual Waiting Room (FIFO)"]
    end
    subgraph PodCompute["Pod App & Worker Fleets"]
        Pod1App["Pod 1 App & Sidekiq Fleet"]
        Pod2App["Pod 2 Flash Checkout Fleet"]
        PaymentSvc["Shopify Payments / Stripe"]
    end
    subgraph DataAndBackplane["Pod Datastores & Webhooks"]
        Pod1Redis[("Pod 1 Redis (Locks & Cache)")]
        Pod1MySQL[("Pod 1 MySQL (Vitess Shards)")]
        Pod2Redis[("Pod 2 Flash Redis (EVALSHA)")]
        Pod2MySQL[("Pod 2 Dedicated MySQL")]
        WebhookPipe[["Webhook Dispatcher (Kafka)"]]
    end
    Buyers e1@-->|"GET Storefront"| CDN
    Buyers e2@-->|"POST Checkout"| RoutingGW
    Merchants e3@-->|"Admin API"| RoutingGW
    CDN e4@-->|"Cache Miss"| RoutingGW
    RoutingGW e5@-->|"Standard Shop"| Pod1App
    RoutingGW e6@-->|"Flash Sale"| WaitRoom
    WaitRoom e7@-->|"Admit Batch"| Pod2App
    Pod1App e8@-->|"Atomic Lock"| Pod1Redis
    Pod1App e9@-->|"Read/Write"| Pod1MySQL
    Pod1MySQL e10@-.->|"Purge (<150ms)"| CDN
    Pod2App e11@-->|"Reserve Stock"| Pod2Redis
    Pod2App e12@-->|"Commit Order"| Pod2MySQL
    Pod1App e13@-->|"Charge Card"| PaymentSvc
    Pod2App e14@-->|"Charge Card"| PaymentSvc
    Pod1MySQL e15@-.->|"CDC Events"| WebhookPipe
    Pod2MySQL e16@-.->|"CDC Events"| WebhookPipe

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
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class Buyers,Merchants client
    class CDN,RoutingGW,WaitRoom edge
    class Pod1App,Pod2App,PaymentSvc service
    class Pod1Redis,Pod2Redis cache
    class Pod1MySQL,Pod2MySQL storage
    class WebhookPipe queue

    style Users fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style EdgeRouting fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style PodCompute fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataAndBackplane fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Deep Dive & Key Technical Challenges

### 1. Liquid Template Engine & Storefront Caching

* Every Shopify theme uses the **Liquid** templating language.
* Compiling Liquid templates dynamically on every page load would destroy server CPU.
* **Edge Caching with Micro-Invalidation**:
  * Storefront HTML pages are heavily cached at edge CDNs.
  * Personalization (cart item count, user greeting) is decoupled from the main page and hydrated client-side via lightweight asynchronous JSON calls (`/cart.js`).
  * When a merchant modifies a product description or price, an invalidation webhook purges the edge CDN cache key (`shop:product:102`) in $< 150\text{ ms}$.

### 2. High-Throughput Webhook Delivery Engine

* Shopify generates billions of webhooks daily for app developers (`orders/create`, `fulfillments/update`).
* **Architecture**:
  * Database changes publish change data capture (CDC) events via Kafka.
  * Sidekiq / Go background worker pools consume from Kafka topics.
  * Workers sign payloads with the merchant's app shared secret:
    $$\text{HMAC-SHA256}(\text{payload}, \text{shared\_secret})$$
  * Failed endpoints are retried using exponential backoff over 48 hours; repeatedly unresponsive endpoints have their webhook subscriptions automatically disabled to preserve cluster bandwidth.

---

## Additional Notes

### Video References

* [![Shopify System Design Video 1](https://img.youtube.com/vi/9yY6A0HvDT4/0.jpg)](https://www.youtube.com/watch?v=9yY6A0HvDT4)
* [![Shopify System Design Video 2](https://img.youtube.com/vi/U2Acfwcah80/0.jpg)](https://www.youtube.com/watch?v=U2Acfwcah80)
