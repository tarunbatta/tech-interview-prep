# Amazon E-Commerce System Design

[← Back to System Design](system-design.md)

## Overview

Amazon is the world's largest online retail e-commerce platform, serving over 300 million active customer accounts and listing hundreds of millions of unique products (SKUs). The system operates globally, handling everything from product browsing and personalized recommendations to multi-item shopping carts, high-throughput flash-sale checkout, payment settlement, inventory tracking, and warehouse fulfillment.

In a system design interview, designing Amazon focuses on:

1. **Catalog & Search**: High-volume, faceted search over hundreds of millions of items with dynamic pricing.
2. **Shopping Cart Persistence**: Highly available cart state that survives cross-device logins.
3. **Inventory Reservation & Flash Sales**: Strict ACID inventory reservation preventing overselling when hundreds of thousands of users attempt to purchase limited stock simultaneously (e.g., Prime Day).
4. **Checkout Saga Orchestration**: Distributed coordination across inventory holds, payment authorization, order creation, and fulfillment.

---

## Functional Requirements

* **Product Catalog & Search**: Browse hierarchical product categories; search products by keywords with faceted filters (brand, price, ratings, Prime shipping).
* **Shopping Cart**: Add, update, remove items; save for later; persist cart across user devices.
* **Checkout & 1-Click Buy**: Select delivery address, choose shipping speed, and complete payment.
* **Inventory Reservation**: Temporarily reserve items during checkout to prevent overselling; auto-release stock if the user abandons checkout.
* **Order Tracking & History**: View past orders, real-time shipment status, and return requests.
* **Reviews & Q&A**: Verified-purchase customer reviews and ratings.

---

## Non-Functional Requirements

* **Ultra-Low Browse Latency**: Catalog and search results must load in **$< 150\text{ ms}$** (P95).
* **High Availability for Browsing (99.999%)**: Browsing and adding items to cart must never fail.
* **Strict Consistency for Inventory & Billing**: Zero overselling of stock; payments must be strictly idempotent (never double-charge a customer).
* **Massive Peak Scale**: Handle **$10\times$ to $50\times$ traffic surges** during Prime Day flash sales.
* **Durability**: Placed orders and financial audit records must have 100% durability.

---

## Back of the Envelope Estimations

### Scale & Concurrency

* **Registered Customers**: $300\text{ Million}$.
* **Active Product Catalog**: $100\text{ Million unique SKUs}$.
* **Daily Active Users (DAU)**: $50\text{ Million}$.
* **Daily Product Views**:
  $$50,000,000\text{ users} \times 10\text{ views/day} = 500,000,000\text{ views/day}$$
* **Average Catalog Read QPS**:
  $$\text{Read QPS} = \frac{500,000,000}{86,400} \approx 5,800\text{ QPS} \quad (\text{Peak: } \approx 25,000\text{ QPS})$$
* **Daily Orders Placed**: $\approx 10,000,000\text{ orders/day}$.
* **Average Order Write QPS**:
  $$\text{Order QPS} = \frac{10,000,000}{86,400} \approx 115\text{ orders/sec} \quad (\text{Peak / Flash Sales: } \approx 5,000\text{ orders/sec})$$

### Storage Requirements

* **Product Catalog Size**:
  * Average $5\text{ KB}$ per SKU (title, description, specs, category, pricing, variations):
  $$100,000,000 \times 5\text{ KB} \approx 500\text{ Gigabytes}$$
  *(Product metadata fits entirely within a distributed cache / DynamoDB table).*
* **Order Records Storage**:
  * Average order payload $\approx 2\text{ KB}$:
  $$10,000,000\text{ orders/day} \times 2\text{ KB} = 20\text{ GB/day} \implies \approx 7.3\text{ TB/year}$$

---

## High-Level System Architecture

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 25, 'rankSpacing': 45, 'padding': 10}}}%%
flowchart LR
    Client["Client Apps (Web / Mobile App)"]
    subgraph EdgeTier["Edge & Ingress"]
        CDN["CloudFront CDN (Product Images)"]
        GW["API Gateway / Envoy"]
    end
    subgraph AppBackend["Application Services"]
        CatalogSvc["Catalog Service"]
        CartSvc["Cart Service"]
        CheckoutSaga["Checkout Saga"]
    end
    subgraph StorageLayer["Storage & Reservation"]
        S3[("AWS S3 (Asset Masters)")]
        ES[("Elasticsearch Cluster")]
        DynamoCart[("DynamoDB (Carts)")]
        InvSvc[("Redis / Inventory Locks")]
    end
    Client e1@-->|"1. Fetch Media"| CDN
    CDN e2@-->|"Origin Pull"| S3
    Client e3@-->|"2. REST / GraphQL"| GW
    GW e4@-->|"Browse / Search"| CatalogSvc
    GW e5@-->|"Cart Ops"| CartSvc
    GW e6@-->|"Place Order"| CheckoutSaga
    CatalogSvc e7@-->|"Full-Text Query"| ES
    CartSvc e8@-->|"Read / Write"| DynamoCart
    CheckoutSaga e9@-->|"Reserve Stock"| InvSvc

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
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff

    class Client client
    class CDN,GW edge
    class CatalogSvc,CartSvc,CheckoutSaga service
    class InvSvc cache
    class S3,ES,DynamoCart storage

    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style AppBackend fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style StorageLayer fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Core Workflows & Deep Dives

### 1. Flash Sale Inventory Reservation (Zero Overselling)

When a flash sale launches (e.g., 1,000 discounted PlayStation consoles with 50,000 concurrent checkout clicks), hitting a relational database row with `UPDATE inventory SET stock = stock - 1` results in catastrophic row lock contention and database timeouts.

* **Two-Phase Inventory Reservation**:
  1. **Phase 1: Temporary Reservation (Hold)**:
     * When user enters checkout, the Inventory Service decrements stock using an **atomic Redis Lua script**:

     ```lua
     local stock = redis.call('GET', KEYS[1])
     if stock and tonumber(stock) >= tonumber(ARGV[1]) then
         redis.call('DECRBY', KEYS[1], ARGV[1])
         redis.call('SETEX', KEYS[2], 900, ARGV[1]) -- 15-min lease hold
         return 1
     else
         return 0
     end
     ```

     * If return is `1`, a 15-minute lease hold is granted.
  2. **Phase 2: Final Commit or Rollback**:
     * If payment succeeds: Inventory Service updates the relational inventory ledger asynchronously and permanently clears the hold.
     * If payment fails or times out: A scheduled cleanup job or TTL expiry automatically restores the reserved quantity (`INCRBY`).

### 2. Distributed Checkout Saga Orchestrator

Checkout is an asynchronous multi-service transaction:

```mermaid
sequenceDiagram
    autonumber
    actor Customer
    participant Gateway as API Gateway
    participant Saga as Checkout Saga Orchestrator
    participant Inv as Inventory Service
    participant Pay as Payment Gateway
    participant OrderDB as Order DB (Aurora)
    Customer->>Gateway: POST /api/v1/checkout (CartId, Address, CardToken)
    Gateway->>Saga: Start Checkout Saga
    Saga->>Inv: Step 1: Reserve Inventory (Hold 15 mins)
    alt Inventory Reserved
        Inv-->>Saga: Hold Confirmed
        Saga->>Pay: Step 2: Authorize & Capture Payment (Idempotency Token)
        alt Payment Successful
            Pay-->>Saga: Payment Captured ($129.99)
            Saga->>OrderDB: Step 3: Insert Order (Status: CONFIRMED)
            OrderDB-->>Saga: Order Created (OrderId: amzn_8812)
            Saga-->>Customer: 201 Created (Order Placed)
        else Payment Declined
            Pay-->>Saga: Payment Failed
            Saga->>Inv: Compensating Txn: Release Inventory Hold
            Saga-->>Customer: 402 Payment Required ("Card Declined")
        end
    else Out of Stock
        Inv-->>Saga: 0 Items Available
        Saga-->>Customer: 409 Conflict ("Item sold out")
    end
```

---

## API Specifications

### 1. Search Catalog

```http
GET /api/v1/products/search?q=mechanical+keyboard&category=electronics&priceMax=150&primeOnly=true
```

```json
// Response: 200 OK
{
  "products": [
    {
      "skuId": "B089K891AZ",
      "title": "Wireless RGB Mechanical Gaming Keyboard",
      "price": 89.99,
      "rating": 4.7,
      "reviewCount": 18400,
      "isPrimeEligible": true,
      "inStock": true,
      "thumbnailUrl": "https://m.media-amazon.com/images/I/71xyz.jpg"
    }
  ],
  "totalResults": 1420
}
```

### 2. Place Order (Checkout)

```http
POST /api/v1/checkout
Authorization: Bearer <customer_token>
Content-Type: application/json
```

```json
// Request
{
  "cartId": "cart_99182a",
  "shippingAddressId": "addr_10284",
  "paymentMethodId": "pm_visa_8812",
  "idempotencyKey": "idem_tok_9918231024"
}

// Response: 201 Created
{
  "orderId": "114-8829104-9918231",
  "totalAmount": 97.41,
  "status": "ORDER_CONFIRMED",
  "estimatedDeliveryDate": "2026-10-29"
}
```

---

## Object Model & Database Schema

```mermaid
erDiagram
    CUSTOMER ||--o{ ORDER : "places"
    CUSTOMER ||--o{ CART_ITEM : "maintains"
    ORDER ||--|{ ORDER_ITEM : "contains"
    PRODUCT_SKU ||--o{ CART_ITEM : "added_to"
    PRODUCT_SKU ||--o{ ORDER_ITEM : "purchased_in"
    CUSTOMER {
        uuid customerId PK
        varchar_255 email
        boolean isPrime
        uuid defaultAddressId
        timestamp createdAt
    }
    PRODUCT_SKU {
        varchar_64 skuId PK
        varchar_255 title
        decimal price
        int stockQuantity
        boolean isPrimeEligible
    }
    CART_ITEM {
        uuid cartId PK,FK
        varchar_64 skuId PK,FK
        int quantity
        timestamp addedAt
    }
    ORDER {
        varchar_64 orderId PK
        uuid customerId FK
        decimal totalAmount
        varchar_32 status
        json shippingAddress
        timestamp createdAt
    }
    ORDER_ITEM {
        varchar_64 orderId PK,FK
        varchar_64 skuId PK,FK
        decimal unitPrice
        int quantity
    }
```

---

## System Design Diagram

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph ClientTier["Clients"]
        Client["Customer Client (Web / Mobile App)"]
    end
    subgraph EdgeTier["Edge & API Gateway"]
        CDN["CloudFront CDN (Images & Reviews)"]
        GW{"API Gateway / Envoy (Auth, Rate Limit)"}
    end
    subgraph CoreServices["Core Microservices & Orchestration"]
        CatalogSvc["Catalog Service (100M+ SKUs & Buy Box)"]
        CartSvc["Shopping Cart Svc (Cross-Device Sync)"]
        OrderSaga["Checkout Orchestrator (Distributed Saga)"]
        InvSvc["Inventory Service (15m Hold & Lua)"]
        PaySvc["Payment Service (1-Click & Idempotency)"]
    end
    subgraph DataAndFulfillment["Data Stores, Caches & Fulfillment"]
        S3[("AWS S3 Store (Product Media)")]
        ES[("Elasticsearch (Faceted Search)")]
        DynamoCart[("DynamoDB / Redis (Cart KV Store)")]
        RedisInv[("Redis Cache (Flash Sale Counters)")]
        OrderDB[("Aurora MySQL (Immutable Orders)")]
        WMS[["Warehouse WMS (Fulfillment Queue)"]]
    end
    Client e1@-->|"Static Media"| CDN
    CDN e2@-->|"Origin Pull"| S3
    Client e3@-->|"REST / GraphQL"| GW
    GW e4@-->|"Browse / Search"| CatalogSvc
    GW e5@-->|"Cart CRUD"| CartSvc
    GW e6@-->|"POST /checkout"| OrderSaga
    CatalogSvc e7@-->|"Query Index"| ES
    CartSvc e8@-->|"Sub-ms R/W"| DynamoCart
    OrderSaga e9@-->|"1. Reserve Hold"| InvSvc
    OrderSaga e10@-->|"2. Charge Card"| PaySvc
    OrderSaga e11@-->|"3. Commit Order"| OrderDB
    OrderSaga e12@-->|"4. Dispatch"| WMS
    InvSvc e13@-->|"Lua DECRBY"| RedisInv
    PaySvc e14@-->|"Audit Ledger"| OrderDB

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

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class Client client
    class CDN,GW edge
    class CatalogSvc,CartSvc,OrderSaga service
    class InvSvc,PaySvc worker
    class RedisInv cache
    class S3,ES,DynamoCart,OrderDB storage
    class WMS queue

    style ClientTier fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style EdgeTier fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style CoreServices fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataAndFulfillment fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Deep Dive & Key Technical Challenges

### 1. Dynamic Pricing & Buy Box Algorithm

* Multiple third-party sellers frequently offer the exact same product SKU.
* **The Buy Box Machine Learning Model**: Factors seller rating, fulfillment method (Fulfilled by Amazon / FBA vs. Merchant), shipping speed, price, and defect rate to award the "Add to Cart" button to the optimal seller.

### 2. Idempotency in Payment Processing

* If network times out between Amazon and the card processor, retrying with the same `idempotencyKey` returns the cached authorization token without charging the customer twice.

---

## Additional Notes

### Video References

* [![Amazon System Design Video](https://img.youtube.com/vi/EpASu_1dUdE/0.jpg)](https://www.youtube.com/watch?v=EpASu_1dUdE)
