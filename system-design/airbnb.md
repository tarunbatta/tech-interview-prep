# Airbnb System Design

[← Back to System Design](system-design.md)

## Overview

Airbnb is a global two-sided online marketplace connecting guests looking for accommodations (vacation rentals, boutique stays, experiences) with hosts looking to monetize their properties. The platform serves over 150 million users and 7+ million active listings across 220+ countries.

A comprehensive system design for Airbnb centers around three core distributed systems challenges:

1. **Low-Latency Geospatial & Availability Search**: Filtering millions of listings by location, price, amenities, and real-time open date intervals within $< 200\text{ ms}$.
2. **Zero Double-Booking Guarantee (ACID)**: Ensuring two guests never successfully book overlapping dates for the same listing, even during concurrent checkout races.
3. **Availability Calendar Modeling**: High-performance representation and querying of 365-day availability vectors per listing.

---

## Functional Requirements

* **Search & Discovery**: Guests can search properties by destination (city, coordinates, radius), dates (check-in / check-out), guest count, price bounds, and amenities (WiFi, pool, pets).
* **Listing Management**: Hosts can create and manage property listings, upload high-resolution photos, configure house rules, and set base/seasonal pricing.
* **Availability Calendar**: Hosts manage an interactive calendar blocking dates or adjusting daily rates; guests can view real-time open dates.
* **Reservation & 15-Minute Hold**: Guests can initiate a reservation, placing a temporary 15-minute distributed lock on the requested dates to complete payment without losing the room.
* **Payment Processing**: Process payments securely (credit cards, PayPal, Apple Pay) with split fees; hold funds in escrow and release payout to the host 24 hours after successful guest check-in.
* **Reviews & Trust**: Mutual double-blind reviews (neither party sees the other's review until both submit or 14 days expire).

---

## Non-Functional Requirements

* **Ultra-Low Search Latency**: P95 search query response time must be **$< 200\text{ ms}$**.
* **Zero Double-Bookings (Strict Consistency)**: Under no circumstances should two distinct users successfully reserve the same property on overlapping dates.
* **High Availability for Search (99.99%)**: Searching and browsing listings must remain available even if backend booking or payment services experience transient degradation.
* **Idempotent Payment Transactions**: Network retries during checkout must never double-charge the guest.
* **High Durability**: Financial ledger records, booking confirmations, and host contracts must have 100% durability across multi-region backups.

---

## Back of the Envelope Estimations

### Scale & Traffic

* **Total Registered Users**: $150\text{ Million}$.
* **Active Property Listings**: $7\text{ Million listings globally}$.
* **Daily Active Users (DAU)**: $25\text{ Million}$.
* **Search Queries per Day**:
  $$25,000,000\text{ users} \times 4\text{ searches/day} = 100,000,000\text{ searches/day}$$
* **Average Search QPS**:
  $$\text{Search QPS} = \frac{100,000,000}{86,400} \approx 1,160\text{ QPS} \quad (\text{Peak: } \approx 4,000\text{ QPS})$$
* **Bookings per Day**: $\approx 1,000,000\text{ bookings/day}$.
* **Booking Creation QPS**:
  $$\text{Booking QPS} = \frac{1,000,000}{86,400} \approx 12\text{ writes/sec} \quad (\text{Peak: } \approx 50\text{ QPS})$$

### Storage Requirements

* **Listing Metadata**: $\approx 2\text{ KB}$ per listing.
  $$7,000,000 \times 2\text{ KB} \approx 14\text{ GB (easily cached in RAM)}$$
* **Listing Photos**:
  * Average 20 photos per listing @ $1.5\text{ MB}$ compressed $= 30\text{ MB per listing}$.
  * Total Media Storage:
    $$7,000,000 \times 30\text{ MB} \approx 210\text{ Terabytes}$$ (stored in AWS S3 and cached via edge CDN).
* **Availability Calendar Cache (Redis Bitmaps)**:
  * 365 days represented as a 365-bit bitfield per listing ($46\text{ bytes per listing per year}$).
  * Total Calendar RAM in Redis:
    $$7,000,000 \times 46\text{ bytes} \approx 322\text{ Megabytes!}$$
    *(Enables blazing fast, microsecond bitwise AND intersection operations).*

---

## High-Level System Architecture

```mermaid
flowchart TD
    Client["Client Apps (iOS / Android / Web)"]
    subgraph MediaPipeline["Media Pipeline & CDN"]
        CDN["Global Edge CDN (Photos / Reviews)"]
        S3["AWS S3 (Master Photos)"]
    end
    subgraph AppBackend["Application Backend"]
        GW["API Gateway / Envoy"]
        SearchSvc["Search Service"]
        AvailSvc["Availability Service"]
        BookingSvc["Booking Service"]
    end
    subgraph DataLayer["Storage & Caching Layer"]
        ES[("Elasticsearch Cluster")]
        Redis[("Redis Calendar Bitmaps")]
        Aurora[("MySQL / Aurora (ACID)")]
    end
    Client -->|"1. Fetch Media (Edge CDN)"| CDN
    S3 -->|"Origin Pull"| CDN
    Client -->|"2. Dynamic APIs (REST / GraphQL)"| GW
    GW -->|"Search Queries"| SearchSvc
    GW -->|"Calendar Checks"| AvailSvc
    GW -->|"Reservations"| BookingSvc
    SearchSvc -->|"Geo & Filter Query"| ES
    AvailSvc -->|"Bitwise Operations"| Redis
    BookingSvc -->|"ACID Transactions"| Aurora
```

---

## Core Components & Workflows

### 1. High-Performance Availability Calendar (Redis Bitmaps)

Checking whether a listing is available for an 8-day check-in range via standard SQL `BETWEEN` joins across 7 million rows causes catastrophic latency.

* **The Bitmap Solution**:
  * For every listing, maintain a 365-bit bitfield in Redis for the upcoming year:
    * `0` = Date is available.
    * `1` = Date is booked or blocked by host.
  * Day 0 = Jan 1st, Day 1 = Jan 2nd, etc.
* **Checking Availability**:
  * A guest searches for dates Day 10 to Day 15 (a 6-day stay).
  * The service executes a single Redis `BITFIELD GET` or masks the byte range. If any bit in the range is `1`, the property is unavailable.
  * **Bitwise Search Intersections**: To filter available listings in a city, the Search Service intersects the city's listing candidates against the calendar bitmap in sub-milliseconds.

### 2. Concurrency Control & Double-Booking Prevention

When a guest initiates checkout, the requested dates must be protected against race conditions.

```mermaid
sequenceDiagram
    autonumber
    actor Guest
    participant Gateway as API Gateway
    participant BookingSvc as Booking Service
    participant Redis as Redis Lock & Cache
    participant DB as Relational DB (Aurora)
    participant Payment as Payment Gateway
    Guest->>Gateway: POST /api/v1/bookings/reserve (ListingId, Dates)
    Gateway->>BookingSvc: Reserve Request
    BookingSvc->>Redis: Acquire 15-min Distributed Lock (SETNX lock:listing_102)
    alt Lock Acquired & Dates Open
        BookingSvc->>Redis: Mark Dates as RESERVED in Bitmap
        BookingSvc->>DB: INSERT INTO reservations (Status: PENDING_PAYMENT)
        BookingSvc-->>Guest: Hold Granted (15-Minute Countdown)
        Guest->>BookingSvc: POST /api/v1/bookings/pay (PaymentToken)
        BookingSvc->>Payment: Charge Card (Idempotent Key)
        Payment-->>BookingSvc: Payment Success
        BookingSvc->>DB: UPDATE reservations SET status = 'CONFIRMED'
        BookingSvc->>Redis: Release Distributed Lock (Keep Dates as BOOKED)
        BookingSvc-->>Guest: Booking Confirmed & Receipt Issued
    else Lock Failed or Dates Taken
        BookingSvc-->>Guest: 409 Conflict ("Dates no longer available")
    end
```

* **Relational Hard Constraint (Final Defense)**:
  * Even if Redis crashes, the relational database guarantees zero double-bookings via a database constraint table:

  ```sql
  CREATE TABLE listing_booked_dates (
      listing_id BIGINT NOT NULL,
      stay_date DATE NOT NULL,
      reservation_id VARCHAR(64) NOT NULL,
      PRIMARY KEY (listing_id, stay_date)
  );
  ```

  * During final checkout commit, the transaction attempts to insert each day of the stay. If another user committed the same day micro-seconds earlier, the `PRIMARY KEY` violation triggers a clean rollback with zero financial discrepancy.

---

## API Specifications

### 1. Search Listings

```http
GET /api/v1/listings/search?lat=37.7749&lon=-122.4194&radiusKm=20&checkIn=2026-11-01&checkOut=2026-11-06&guests=2
Authorization: Bearer <token>
```

```json
// Response: 200 OK
{
  "listings": [
    {
      "listingId": "lst_9918231",
      "title": "Sunlit Modern Loft near Union Square",
      "pricePerNight": 195.00,
      "currency": "USD",
      "rating": 4.94,
      "reviewCount": 128,
      "thumbnailUrl": "https://cdn.airbnb.com/pictures/lst_9918231_thumb.webp",
      "location": {"lat": 37.7891, "lon": -122.4014}
    }
  ],
  "totalResults": 412,
  "nextPageCursor": "cur_8812a"
}
```

### 2. Reserve Listing (Initiate 15-Minute Hold)

```http
POST /api/v1/bookings/reserve
Authorization: Bearer <guest_token>
Content-Type: application/json
```

```json
// Request
{
  "listingId": "lst_9918231",
  "checkInDate": "2026-11-01",
  "checkOutDate": "2026-11-06",
  "numberOfGuests": 2,
  "idempotencyKey": "idem_8910248102a"
}

// Response: 201 Created
{
  "reservationId": "resv_449012a",
  "holdExpiresAt": 1730006100,
  "totalPrice": 975.00,
  "cleaningFee": 80.00,
  "serviceFee": 120.00,
  "status": "HOLD_PENDING_PAYMENT"
}
```

---

## Object Model & Database Schema

```mermaid
erDiagram
    USER ||--o{ LISTING : "hosts"
    USER ||--o{ RESERVATION : "books"
    LISTING ||--o{ RESERVATION : "has"
    LISTING ||--o{ LISTING_DATE : "tracks"
    RESERVATION |o--o{ LISTING_DATE : "locks"
    USER {
        bigint userId PK
        varchar_255 email
        boolean isHost
        decimal rating
        timestamp createdAt
    }
    LISTING {
        bigint listingId PK
        bigint hostId FK
        varchar_255 title
        decimal pricePerNight
        decimal latitude
        decimal longitude
        varchar_32 roomType
        int maxGuests
    }
    RESERVATION {
        uuid resvId PK
        bigint guestId FK
        bigint listingId FK
        date checkInDate
        date checkOutDate
        varchar_32 status
        decimal totalPrice
    }
    LISTING_DATE {
        bigint listingId PK,FK
        date stayDate PK
        uuid reservationId FK
        decimal price
        boolean isBlocked
    }
```

---

## System Design Diagram

```mermaid
flowchart TD
    subgraph ClientTier["Client & Edge Media Tier"]
        Client["Guest & Host Apps (iOS / Android / Web)"]
        CDN["Global Edge CDN (Listing Photos, Floor Plans & Reviews)"]
        S3[("AWS S3 Object Store (Master Photos & Proofs)")]
    end
    subgraph GatewayTier["API Gateway & Routing Tier"]
        GW{"API Gateway / Envoy (Auth, Rate Limiting, SSL, Idempotency Key Check)"}
    end
    subgraph CoreServices["Core Application Microservices"]
        SearchSvc["Search Service (Geospatial Bounding Box, Date Range & Amenity Filter)"]
        AvailSvc["Availability Service (Listing Date Bitmaps: 365 bits/year)"]
        BookingSvc["Booking & Reservation Svc (Saga Orchestrator, 15-min Distributed Lock & Lease)"]
        PaymentSvc["Payment Service (Stripe / Bank: 2PC, Escrow Hold, 24h Post-Checkin Payout)"]
        PricingSvc["Dynamic Smart Pricing Engine (Nightly ML Hydration & Supply Adjustment)"]
        ICalSync["iCal Sync Worker Pool (Two-Way RFC 5545 OTA Polling)"]
    end
    subgraph DataAndEventTier["Data Stores, Caches & Event Bus"]
        ES[("Elasticsearch Cluster (Geo-Point Index, Price, Room Type, Ratings)")]
        RedisCal[("Redis Calendar Cache (Bitmaps: 0=Avail, 1=Booked, Sub-ms Intersection)")]
        BookingDB[("Booking DB (MySQL / Aurora ACID, Unique Index: listing_id + stay_date)")]
        Kafka[["Kafka Event Bus (Topics: booking-confirmed, listing-updated, reviews)"]]
    end
    Client -->|"Static Media Fetch"| CDN
    CDN -->|"Origin Pull"| S3
    Client -->|"REST / GraphQL"| GW
    GW -->|"GET /api/v1/listings/search"| SearchSvc
    GW -->|"POST /api/v1/bookings/reserve"| BookingSvc
    SearchSvc -->|"Geo-Distance & Attribute Query"| ES
    SearchSvc -->|"Filter Open Dates"| AvailSvc
    AvailSvc -->|"Bitfield GET & Bitwise AND"| RedisCal
    BookingSvc -->|"Acquire 15-min Lock (SETNX) & Update Bitmap"| RedisCal
    BookingSvc -->|"Commit Hold & ACID Constraint Check"| BookingDB
    BookingSvc -->|"Execute Charge & Escrow Hold"| PaymentSvc
    BookingSvc -->|"Publish Booking & Listing Events"| Kafka
    Kafka -.->|"Async Index Sync"| ES
    PricingSvc -->|"Hydrate Daily Rates"| RedisCal
    ICalSync -->|"Sync External OTA Blocks"| BookingDB
    ICalSync -->|"Update Availability Bits"| RedisCal
```

---

## Deep Dive & Key Technical Challenges

### 1. Geospatial Indexing & Search Ranking

* **Elasticsearch with Geo-Distance Filters**: Properties are indexed using Elasticsearch `geo_point` fields. The primary search query matches bounding boxes or radial distances from the target center.
* **Multi-Factor Ranking Model**:
  * Listing quality score (photo count, resolution, verified status).
  * Host response rate and Superhost status.
  * Historic conversion rate and competitive pricing against neighborhood medians.

### 2. Handling Host Cancellation & Escrow Payouts

* **Escrow Hold**: Guests are charged upon booking, but funds are not sent to the host immediately. Funds sit in an escrow clearing account to safeguard against host scams or last-minute property destruction.
* **Automated Payout Trigger**: 24 hours after verified guest check-in (providing the guest has not opened a dispute via Customer Support), a background Kafka consumer triggers an automated ACH / wire payout to the host via Stripe Connect.

### 3. Dynamic Smart Pricing Engine

* **Offline Model Training**: XGBoost / Deep Neural Net trained on historical bookings, seasonal demand, local event calendars (concerts, conferences), lead times, and competitor market supply.
* **Online Pricing Hydration**: Daily prices per listing are updated every night and written to Redis cache. Real-time adjustments occur when local neighborhood supply dips below 15% occupancy.

### 4. Cross-Platform Calendar Synchronization (iCal / RFC 5545)

* Hosts frequently list properties on multiple platforms (Airbnb, VRBO, Booking.com).
* **Two-Way iCal Polling**:
  * A worker pool periodically fetches external `.ics` feeds every 15–30 minutes, parses `VEVENT` blocks, and atomically blocks conflicting dates in the local Redis availability bitmap and SQL ledger.
  * Airbnb exposes unique host iCal endpoints with cryptographic token verification (`/calendar/ical/<listing_id>.ics?token=...`) so external OTAs can pull Airbnb bookings.
* **Race Condition Mitigation**: If a direct Airbnb guest and an external VRBO sync attempt to claim the same night simultaneously, the atomic DB constraint on `listing_booked_dates` rejects the later write and sends an immediate alert to the host.

### 5. Instant Book vs Request-to-Book Workflow

```mermaid
flowchart TD
    Start["Guest Initiates Booking"]
    Hold15["Acquire 15-min Hold"]
    Pay["Execute Payment"]
    Confirmed["Booking Confirmed"]
    HoldNotif["Place Hold & Send Host Notification"]
    Decision{"Host 24-hr Decision"}
    Accept["Host Accepts"]
    Reject["Host Rejects / Expires"]
    AuthPay["Trigger Payment Authorization"]
    Release["Release Calendar Hold"]
    Start -->|"Instant Book"| Hold15
    Hold15 -->|"Proceed to Checkout"| Pay
    Pay -->|"Charge Success"| Confirmed
    Start -->|"Request to Book"| HoldNotif
    HoldNotif -->|"Await Response"| Decision
    Decision -->|"Approved"| Accept
    Decision -->|"Declined or Timeout"| Reject
    Accept -->|"Capture Funds"| AuthPay
    AuthPay -->|"Settled"| Confirmed
    Reject -->|"Free Dates"| Release
```

---

## Additional Notes

### Video References

* [![Airbnb System Design Video](https://img.youtube.com/vi/m67Mjbx6DMY/0.jpg)](https://www.youtube.com/watch?v=m67Mjbx6DMY)
