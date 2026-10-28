# TicketMaster System Design

[← Back to System Design](../system-design.md)

## Overview

TicketMaster is the global market leader in ticketing and live entertainment event reservations, selling hundreds of millions of tickets annually. The platform faces extreme, spiky demand: when mega-concerts (e.g., stadium tours) go on sale, **millions of concurrent fans arrive in the exact same second** competing for tens of thousands of fixed seats.

Designing TicketMaster requires solving critical distributed systems challenges:

1. **Virtual Waiting Room & Traffic Smoothing**: Protecting backend transactional databases from millions of simultaneous checkout hits.
2. **Interactive Seat Map Availability**: Sub-second rendering of tens of thousands of seats across multiple tiers.
3. **Zero Double-Selling Guarantee (ACID)**: Granting exclusive, temporary 10-minute seat holds during payment while preventing deadlocks and overselling.
4. **Anti-Bot & Anti-Scalper Protections**: Enforcing rate limits, CAPTCHA, and verified fan tokens.

---

## Functional Requirements

* **Event & Venue Search**: Search live events by artist, tour, city, venue, and date.
* **Interactive Seating Chart**: View stadium seating maps with real-time seat availability color-coded by tier (Available, Held/In-Cart, Sold).
* **Virtual Waiting Room (Queue)**: Ingress queue that admits fans at a controlled, metered rate into the booking funnel.
* **Temporary Seat Hold (10-Minute Lease)**: Fans select specific seats and place a temporary 10-minute hold while entering payment details.
* **Checkout & Payment**: Securely charge credit cards, issue digital mobile tickets with rotating dynamic barcodes, and email confirmations.
* **Ticket Transfer & Resale**: Securely transfer tickets between fan accounts or list tickets on an official fan-to-fan resale marketplace.

---

## Non-Functional Requirements

* **Zero Double-Selling (Strict ACID Consistency)**: Under no circumstances should two fans purchase the exact same seat.
* **Extreme Burst Resilience**: Sustain **$100\times$ traffic surges** at 10:00 AM on ticket launch day without system collapse.
* **Fairness & Anti-Bot Defense**: Ensure queue positions are enforced deterministically and cannot be bypassed via script tampering.
* **Low Latency for Seating Map**: Seating availability lookups must complete within **$< 100\text{ ms}$**.
* **Financial Durability**: 100% auditability for payment settlements, refunds, and ticket ownership transfers.

---

## Back of the Envelope Estimations

### Scale & Spike Traffic

* **Stadium Concert Capacity**: 60,000 seats.
* **Peak Concurrent Fans at On-Sale**: $1,000,000\text{ users competing for 60,000 tickets}$.
* **Waiting Room Ingress Rate**:
  * Admitting $500\text{ users/second}$ into the booking checkout pipeline ensures the database handles transactions comfortably without connection starvation.
* **Seat Hold QPS**:
  * 500 users/sec choosing 2 seats $= 1,000\text{ seat locks/sec}$ (effortlessly handled by Redis Redlock).
* **Daily Average System Scale**:
  * 100,000 events listed globally; 500,000 tickets sold/day on normal days.

---

## High-Level System Architecture

```text
┌────────────────────────────────────────────────────────────────────────┐
│                              FAN CLIENTS                               │
│                   (1M+ Concurrent Users at 10:00 AM)                   │
└───────────────────────────────────┬────────────────────────────────────┘
                                    │
                                    ▼ 1. Ingress Request
┌────────────────────────────────────────────────────────────────────────┐
│                  VIRTUAL WAITING ROOM (EDGE / REDIS)                   │
│         - Cloudflare Waiting Room / Redis Token Bucket Queue           │
│         - Anti-Bot Proof of Work / CAPTCHA Verification               │
│         - Meters Traffic to 500 Admitted Users / Second                │
└───────────────────────────────────┬────────────────────────────────────┘
                                    │
                                    ▼ 2. Admitted with Signed JWT Token
┌────────────────────────────────────────────────────────────────────────┐
│                          API GATEWAY / ENVOY                           │
│                 (Validates Admission Token, SSL, Rate Limit)           │
└─────────────┬─────────────────────┬──────────────────────┬─────────────┘
              │                     │                      │
              ▼                     ▼                      ▼
┌────────────────────────┐┌────────────────────┐┌────────────────────────┐
│   Seat Map Service     ││ Seat Hold Service  ││  Checkout Saga Orch.   │
│ (SVG / Vector Tiles)   ││(10-Min Lease Lock) ││ (Payment + Ledger)     │
└─────────────┬──────────┘└─────────┬──────────┘└──────────┬─────────────┘
              │                     │                      │
              ▼                     ▼                      ▼
┌────────────────────────┐┌────────────────────┐┌────────────────────────┐
│  Redis Seat Bitsets    ││ Redis Distributed  ││  CockroachDB / Aurora  │
│ (0=Avail, 1=Held/Sold) ││ Lock (Redlock)     ││  (ACID Seat Ledger)    │
└────────────────────────┘└────────────────────┘└────────────────────────┘
```

---

## Core Components & Workflows

### 1. Virtual Waiting Room (Traffic Throttling)

Allowing 1 million fans to hit database transactions simultaneously causes server crashes.

* **How the Virtual Queue Works**:
  1. Fans arrive before 10:00 AM and enter a pre-queue holding page hosted on a CDN edge.
  2. At 10:00 AM sharp, the queue randomizes arrivals to prevent bot head-starts and assigns sequential FIFO numbers.
  3. A rate-limiter periodically admits a batch of $N$ users (e.g., 500 users/second) by issuing a cryptographically signed, short-lived HMAC / JWT token.
  4. Only requests presenting this valid admission token are accepted by the backend API Gateway.

### 2. The 10-Minute Seat Hold Mechanism

```mermaid
sequenceDiagram
    autonumber
    actor Fan
    participant Gateway as API Gateway
    participant HoldSvc as Seat Hold Service
    participant Redis as Redis Lock & Bitset
    participant Saga as Checkout Saga
    participant DB as Ticket DB (ACID)

    Fan->>Gateway: POST /api/v1/seats/hold (ShowId, [Seat_101, Seat_102])
    Gateway->>HoldSvc: Process Hold Request
    HoldSvc->>Redis: Execute Atomic Lua Script (Check Status & SETNX with 600s TTL)
    alt Seats Available & Locked
        Redis-->>HoldSvc: Lock Granted (10-minute timer started)
        HoldSvc-->>Fan: 200 OK (Seats Held: 10:00 Remaining)
        Fan->>Gateway: POST /api/v1/checkout/pay (PaymentToken)
        Gateway->>Saga: Authorize Payment ($180.00)
        Saga->>DB: INSERT INTO sold_tickets (show_id, seat_id, fan_id)
        DB-->>Saga: Commit Success
        Saga->>Redis: Mark Seat as PERMANENTLY_SOLD (Remove Lease Lock)
        Saga-->>Fan: 201 Created (Digital Barcode Ticket Issued)
    else Seat Already Held / Sold
        Redis-->>HoldSvc: Lock Denied
        HoldSvc-->>Fan: 409 Conflict ("Seat already taken by another fan")
    end
```

* **Relational Unique Key Guarantee**:

  ```sql
  CREATE TABLE concert_seats (
      concert_id BIGINT NOT NULL,
      seat_number VARCHAR(32) NOT NULL,
      status VARCHAR(16) NOT NULL, -- AVAILABLE, HELD, SOLD
      holder_id BIGINT,
      hold_expires_at TIMESTAMP,
      PRIMARY KEY (concert_id, seat_number)
  );
  ```

---

## API Specifications

### 1. Hold Seats (10-Minute Countdown)

```http
POST /api/v1/concerts/{concertId}/seats/hold
Authorization: Bearer <admission_jwt_token>
Content-Type: application/json
```

```json
// Request
{
  "seatNumbers": ["SEC102-ROW12-S1", "SEC102-ROW12-S2"],
  "idempotencyKey": "idem_hold_8912401"
}

// Response: 200 OK
{
  "holdId": "hld_981240a",
  "heldUntilTimestamp": 1730006600,
  "seats": ["SEC102-ROW12-S1", "SEC102-ROW12-S2"],
  "pricePerTicket": 125.00,
  "serviceFee": 22.50,
  "totalDue": 295.00
}
```

---

## Object Model & Database Schema

```text
┌───────────────────────┐          ┌───────────────────────┐
│        Concert        │1        *│      ConcertSeat      │
├───────────────────────┤──────────├───────────────────────┤
│ concertId: PK (UUID)  │          │ concertId: PK, FK     │
│ artistName: Varchar   │          │ seatNumber: PK (Str)  │
│ venueName: Varchar    │          │ tierCategory: Enum    │
│ eventDate: Timestamp  │          │ price: Decimal        │
│ totalCapacity: Int    │          │ status: Enum          │
└──────────┬────────────┘          │ currentHoldId: UUID   │
           │1                      └───────────────────────┘
           │*
┌──────────▼────────────┐
│      TicketOrder      │
├───────────────────────┤
│ orderId: PK (UUID)    │
│ concertId: FK         │
│ customerId: FK        │
│ seatList: Array[Str]  │
│ totalAmount: Decimal  │
│ barcodeHash: Varchar  │
│ status: Enum          │
└───────────────────────┘
```

---

## System Design Diagram

![TicketMaster System Design](ticketmaster.svg)

[Download Draw.io File](ticketmaster.drawio)

---

## Deep Dive & Key Technical Challenges

### 1. Dynamic Barcodes (Anti-Counterfeiting)

* Static PDF tickets or printed QR codes are easily screenshotted and scalped multiple times.
* **Rotating TOTP Dynamic Barcodes**: The mobile ticket barcode regenerates every 15 seconds using a Time-based One-Time Password (TOTP) algorithm anchored to a shared cryptographic secret. Offline turnstile scanners at the venue validate the token cryptographically even during local network dropouts.

---

## Additional Notes

### Video References

* [![TicketMaster System Design Video](https://img.youtube.com/vi/lbaNYrcun38/0.jpg)](https://www.youtube.com/watch?v=lbaNYrcun38)
