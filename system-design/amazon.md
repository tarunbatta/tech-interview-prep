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
flowchart TD
    Client["Client Apps (Web / Mobile App)"]
    subgraph MediaPipeline["Media Pipeline & CDN"]
        CDN["CloudFront CDN (Product Images)"]
        S3["AWS S3 (Asset Masters)"]
    end
    subgraph AppBackend["Application Backend"]
        GW["API Gateway / Envoy"]
        CatalogSvc["Catalog Service"]
        CartSvc["Cart Service"]
        CheckoutSaga["Checkout Saga"]
    end
    subgraph StorageLayer["Storage & Reservation Layer"]
        ES[("Elasticsearch Cluster")]
        DynamoCart[("DynamoDB (Carts)")]
        InvSvc["Inventory Service (Locks)"]
    end
    Client -->|"1. Fetch Media (CloudFront)"| CDN
    S3 -->|"Origin Pull"| CDN
    Client -->|"2. Dynamic APIs (REST / GraphQL)"| GW
    GW -->|"Browse / Search"| CatalogSvc
    GW -->|"Cart Operations"| CartSvc
    GW -->|"Place Order"| CheckoutSaga
    CatalogSvc -->|"Full-Text Query"| ES
    CartSvc -->|"Read / Write Cart"| DynamoCart
    CheckoutSaga -->|"Reserve Stock"| InvSvc
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
flowchart TD
    subgraph ClientEdge["Client & Edge Delivery Tier"]
        Client["Customer Client (Web / Mobile App: Search, Cart & 1-Click Checkout)"]
        CDN["Global Edge CDN (CloudFront: Product Images, Reviews & Static Assets)"]
        S3[("AWS S3 Asset Store (Product Media Masters)")]
    end
    subgraph GatewayTier["API Gateway Tier"]
        GW{"API Gateway / Envoy (Auth, Rate Limiting, SSL, Idempotency Key Check)"}
    end
    subgraph BrowsingAndCart["Catalog & Cart Services"]
        CatalogSvc["Product Catalog Service (100M+ SKUs, Categories, Attributes, Buy Box ML)"]
        ES[("Elasticsearch Cluster (Full-Text Search, Faceted Filtering & Autocomplete)")]
        CartSvc["Shopping Cart Service (Session & Cross-Device User Carts)"]
        DynamoCart[("DynamoDB / Redis (Shopping Cart KV Store, Partition Key: CustomerId)")]
    end
    subgraph CheckoutAndFulfillment["Checkout Saga, Inventory & Fulfillment"]
        OrderSaga["Order & Checkout Orchestrator (Distributed Saga: Reserve -> Charge -> Create Order)"]
        InvSvc["Inventory Service (Two-Phase Reservation with 15-min TTL, Atomic Redis Lua Decr)"]
        RedisInv[("Redis Inventory Cache (Flash Sale Atomic Counters & Lease Holds)")]
        PaySvc["Payment Service (Credit Cards, 1-Click Pay, Strict Idempotency Tokens)"]
        OrderDB[("Order DB (Aurora MySQL: Immutable Order Records & Financial Audit Ledger)")]
        WMS["Fulfillment & Warehouse Management (WMS: Package Dispatch & Tracking)"]
    end
    Client -->|"Fetch Static Media"| CDN
    CDN -->|"Origin Pull"| S3
    Client -->|"REST / GraphQL"| GW
    GW -->|"Browse / Search"| CatalogSvc
    CatalogSvc -->|"Query Index"| ES
    GW -->|"Cart CRUD"| CartSvc
    CartSvc -->|"Sub-ms Read/Write"| DynamoCart
    GW -->|"POST /api/v1/checkout"| OrderSaga
    OrderSaga -->|"1. Reserve Hold"| InvSvc
    InvSvc -->|"Execute Lua Script (DECRBY + SETEX)"| RedisInv
    OrderSaga -->|"2. Charge Card (Idempotent)"| PaySvc
    OrderSaga -->|"3. Commit Order"| OrderDB
    OrderDB -->|"Dispatch Event"| WMS
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
