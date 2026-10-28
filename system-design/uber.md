# Uber System Design

[← Back to System Design](system-design.md)

## Overview

Uber is a real-time, two-sided mobility marketplace connecting riders with drivers. The platform handles real-time driver GPS tracking, dynamic dispatch and matching algorithms, route navigation, dynamic pricing (surge), payment settlement, and two-way rating systems.

At its core, the architecture solves a massive distributed geospatial coordination problem: processing millions of real-time location pings every second, querying proximity within sub-millisecond latencies, dynamically balancing supply and demand across localized geographic partitions, and maintaining strict consistency across trip lifecycles.

---

## Functional Requirements

* **Rider Ride Request**: Riders can enter pickup and dropoff destinations, view estimated fares and pickup ETAs across vehicle categories (UberX, Comfort, Black, XL), and request a trip.
* **Driver Location Ingestion**: Active drivers broadcast continuous GPS coordinates every 3–4 seconds.
* **Driver Dispatch & Matching**: Find, rank, and notify the optimal available driver within a target geographic radius; allow driver 15 seconds to accept or decline.
* **Real-Time Trip Tracking**: Riders track the matched driver's real-time vehicle movement, orientation, and updated ETA on a live map via WebSockets.
* **Trip Lifecycle State Machine**: Manage deterministic states: `REQUESTED` $\to$ `DISPATCHED` $\to$ `ACCEPTED` $\to$ `ARRIVED` $\to$ `IN_PROGRESS` $\to$ `COMPLETED` $\to$ `PAID`.
* **Dynamic Surge Pricing**: Automatically adjust fare multipliers in real-time when ride demand outstrips available driver supply in a localized area.
* **Payments & Split Fares**: Automatically charge rider credit card / digital wallet upon dropoff; calculate driver earnings and platform commissions.
* **Mutual Ratings & Reviews**: 1-to-5 star ratings and reviews submitted by both rider and driver post-trip.

---

## Non-Functional Requirements

* **Sub-Second Matching Latency**: Finding and alerting matching drivers must complete within **< 1.0–2.0 seconds** of trip request.
* **Low-Latency GPS Streaming**: Driver location updates must appear on rider maps within **< 2–3 seconds** end-to-end.
* **High Availability (99.99%)**: Core trip booking, active ride tracking, and driver safety services must remain resilient across regional cloud outages.
* **Geospatial Write Scalability**: The location ingestion tier must seamlessly handle **hundreds of thousands of writes per second** without database contention.
* **Consistency Isolation**:
  * **Eventual Consistency** for location tracking: Dropping an isolated driver location ping is acceptable.
  * **Strict Consistency** for dispatch matching and billing: Two drivers must **never** be assigned the same rider, and a driver cannot be double-booked.
* **Data Durability**: Trip paths, financial transactions, and billing ledgers must have zero data loss.

---

## Back of the Envelope Estimations

### Scale & Concurrency

* **Global Active Riders**: $150\text{ Million MAU}$.
* **Global Active Drivers**: $6\text{ Million drivers}$.
* **Peak Concurrent Active Drivers**: $1,200,000\text{ drivers}$ online simultaneously.
* **GPS Broadcast Interval**: Every $4\text{ seconds}$ per active driver.
* **Location Ingestion QPS**:
  $$\text{Average Ingestion QPS} = \frac{1,200,000\text{ drivers}}{4\text{ seconds}} = 300,000\text{ writes/sec (300K QPS)}$$
* **Peak Ingestion QPS** (New Year's Eve / Bad weather, $2\times$):
  $$\text{Peak QPS} = 600,000\text{ writes/sec}$$

### Network & Storage Sizing

* **Location Ping Payload**: $\approx 128\text{ bytes}$ (`driverId`, `lat`, `lon`, `heading`, `speed`, `timestamp`, `status`).
* **Ingestion Network Bandwidth**:
  $$300,000\text{ req/sec} \times 128\text{ bytes} \approx 38.4\text{ MB/sec} \approx 307\text{ Mbps}$$
* **Daily Ingestion Storage (Raw Telemetry)**:
  $$300,000 \times 86,400\text{ seconds} \times 128\text{ bytes} \approx 3.3\text{ Terabytes/day}$$
* **In-Memory Active Driver Cache Size**:
  $$1,200,000\text{ drivers} \times 256\text{ bytes in-memory index} \approx 307\text{ Megabytes}$$
  *(Fits easily within an in-memory Redis cluster or memory-mapped Ringpop cluster).*
* **Completed Daily Trips**: $\approx 25\text{ Million trips/day}$.
* **Daily Trip Database Storage**:
  $$25,000,000 \times 2\text{ KB} \approx 50\text{ GB/day} \implies \approx 18.25\text{ TB/year}$$

---

## System Architecture

```mermaid
flowchart TD
    Rider["Rider App"] -->|"REST / WS"| Gateway["API Gateway / Envoy Proxy (TLS Termination, Auth, Rate Limiting)"]
    Driver["Driver App"] -->|"WS / gRPC (GPS Pings)"| Gateway
    Gateway --> TripSvc["Trip & Booking Service (Trip State Machine, REST)"]
    Gateway --> LocIngest["Location Ingestion Service (High-Throughput Go)"]
    LocIngest --> KafkaLoc["Kafka Location Topic (Partitioned by H3 Index)"]
    KafkaLoc --> GeoStore[("Geospatial Driver Store (Redis / H3)")]
    KafkaLoc --> SurgeEngine["Surge Engine (Flink / Spark Supply/Demand)"]
    TripSvc --> Dispatch["Dispatch & Matching Engine (Batched Optimization)"]
    GeoStore -->|"Queries Radius"| Dispatch
    Dispatch --> PushNotif["Push Notification Service (WebSockets / APNs / FCM)"]
```

---

## Core Components & Data Flows

### 1. Geospatial Partitioning: Why Uber Built H3

Traditional SQL indexing (`B-Trees`) cannot efficiently query two-dimensional coordinates. Naive radial queries require scanning huge bounding boxes.

* **Geohash & Google S2 (Squares / Rectangles)**:
  * Rectangular cells suffer from **boundary distortion**: adjacent cells across diagonal corners are $\sqrt{2} \approx 1.414\times$ further apart than orthogonal neighbors.
  * Complicates continuous radius search and supply/demand heat smoothing.
* **Uber H3 (Hexagonal Hierarchical Spatial Index)**:
  * Hexagons have exactly **one uniform neighbor distance**: all 6 adjacent neighbors are equidistant from the center.
  * Minimizes quantization error when smoothing spatial demand curves.
  * Hierarchical: 16 resolution levels (Resolution 7 $\approx 1.2\text{ km}$ edge length; Resolution 8 $\approx 460\text{ meters}$, ideal for neighborhood dispatch).

```mermaid
flowchart TD
    subgraph H3_Hierarchy["Uber H3 Hexagonal Cell Hierarchies"]
        H8["Resolution 8 Cell (~460m) - Micro Dispatch"]
        H7["Resolution 7 Cell (~1.2km) - Surge Pricing Aggregation"]
        H6["Resolution 6 Cell (~3.2km) - City Sector Supply/Demand"]
        H8 --> H7
        H7 --> H6
    end
```

### 2. Location Ingestion Pipeline

1. **Driver GPS Ping**: Driver client emits `(driverId, lat, lon, heading, timestamp)` over a persistent bidirectional WebSocket or gRPC channel.
2. **H3 Cell Resolution**: The Location Ingestion worker translates `(lat, lon)` into an `H3Index` integer (64-bit uint).
3. **Kafka Partitioning**: The message is published to Kafka partitioned by `H3Index`, guaranteeing that all driver events in the same geographic locality flow into the same stream processor.
4. **In-Memory Store Update**:
   * Redis `GEOADD` / sorted sets: Stores current active drivers per H3 cell with an expiry TTL of 15 seconds. If a driver drops connection, their presence automatically expires from search results.
   * Telemetry writes to cold storage (Cassandra / S3) asynchronously for historical auditing and route reconstruction.

### 3. Dispatch & Matching Engine (Batched Matching vs Greedy)

* **Naive Greedy Matching**: Assigns the first nearest driver to a requesting rider immediately.
  * *Flaw*: Causes local sub-optimal outcomes (e.g., Driver A gets matched to Rider 1 who is 2 minutes away, leaving Rider 2 waiting 15 minutes for a distant driver).
* **Batched Bipartite Matching**:
  * Collects ride requests and available drivers over a **3-to-5 second rolling batch window**.
  * Constructs a bipartite graph where edge weights represent estimated pickup ETA calculated via routing graphs.
  * Solves using the **Hungarian Algorithm / Minimum-Cost Maximum-Flow (MCMF)** to minimize aggregate customer wait times across the city sector.

```mermaid
sequenceDiagram
    autonumber
    actor Rider
    participant Gateway as "API Gateway"
    participant TripSvc as "Trip Service"
    participant Dispatcher as "Dispatch & Matching Engine"
    participant GeoStore as "Geospatial Cache (H3 / Redis)"
    participant Driver as "Driver App"
    Rider->>Gateway: POST /api/v1/trips/request (Pickup, Dropoff)
    Gateway->>TripSvc: Create Trip (Status: REQUESTED)
    TripSvc->>Dispatcher: Match Trip (Pickup H3 Cell: 8826856235fffff)
    Dispatcher->>GeoStore: Query Available Drivers in H3 Cell + Ring 1
    GeoStore-->>Dispatcher: Candidate Driver IDs [D1, D2, D3]
    Dispatcher->>Dispatcher: Rank Candidates (ETA, Acceptance Rate, Direction)
    Dispatcher->>Driver: Push Ride Offer (15s Countdown)
    alt Driver Accepts within 15 seconds
        Driver->>Dispatcher: POST /api/v1/trips/accept
        Dispatcher->>TripSvc: Lock Match & Transition State to ACCEPTED
        TripSvc->>Rider: Push Notification: Driver Matched (Name, Car, Live GPS)
    else Driver Declines or Times Out
        Driver-->>Dispatcher: Timeout / Decline
        Dispatcher->>Dispatcher: Cascade Offer to Next Ranked Candidate (D2)
    end
```

### 4. Dynamic Surge Pricing Engine

* **Calculation Window**: Runs every 15–30 seconds via Apache Flink.
* **Supply vs Demand Ratio**:
  $$\text{Surge Factor} = f\left(\frac{\text{Unmatched Ride Requests in Cell } H_i}{\text{Available Drivers in Cell } H_i}\right)$$
* **Spatial Smoothing**: If cell $A$ has surge $2.0\times$ and adjacent cell $B$ has $1.0\times$, drivers would unbalance the system by flocking across borders. H3 neighbor smoothing averages the multiplier across $k$-rings ($R=1$ neighbors) using Gaussian weights to create gradual geographic transitions.

---

## API Endpoints

### 1. Request a Ride

```http
POST /api/v1/trips/request
Authorization: Bearer <rider_token>
Content-Type: application/json
```

```json
// Request
{
  "pickupLocation": {"lat": 37.7749, "lon": -122.4194},
  "dropoffLocation": {"lat": 37.7891, "lon": -122.4014},
  "vehicleTier": "UBERX",
  "quoteId": "qte_998124ab"
}

// Response: 201 Created
{
  "tripId": "trip_88102a7b",
  "status": "SEARCHING_FOR_DRIVER",
  "estimatedPickupMinutes": 4,
  "fareAmount": 24.50,
  "currency": "USD"
}
```

### 2. Driver Location Ping (WebSocket / gRPC Stream)

```protobuf
message DriverLocationPing {
  string driver_id = 1;
  double latitude = 2;
  double longitude = 3;
  float heading = 4;
  float speed_mps = 5;
  int64 timestamp = 6;
  enum DriverStatus status = 7; // AVAILABLE, EN_ROUTE_PICKUP, ON_TRIP
}
```

### 3. Accept Ride Offer

```http
POST /api/v1/trips/{tripId}/accept
Authorization: Bearer <driver_token>
Content-Type: application/json
```

```json
// Request
{
  "driverId": "drv_5512",
  "currentLocation": {"lat": 37.7755, "lon": -122.4180}
}

// Response: 200 OK
{
  "tripId": "trip_88102a7b",
  "status": "ACCEPTED",
  "rider": {
    "firstName": "Alex",
    "rating": 4.92,
    "pickupAddress": "550 Market St"
  }
}
```

---

## Object Model & Database Schema

```mermaid
erDiagram
    RIDER ||--o{ TRIP : "requests"
    DRIVER ||--o{ TRIP : "fulfills"
    DRIVER ||--o{ VEHICLE : "drives"
    RIDER {
        uuid riderId PK
        varchar_100 name
        varchar_255 email
        varchar_20 phone
        decimal rating
        uuid defaultPaymentId
    }
    DRIVER {
        uuid driverId PK
        varchar_64 licenseNum
        decimal rating
        boolean isOnline
        varchar_32 status
        bigint currentH3Index
    }
    VEHICLE {
        uuid vehicleId PK
        uuid driverId FK
        varchar_64 make
        varchar_64 model
        int year
        varchar_20 licensePlate
        varchar_32 tier
    }
    TRIP {
        uuid tripId PK
        uuid riderId FK
        uuid driverId FK
        varchar_32 vehicleTier
        varchar_32 status
        double pickupLat
        double pickupLon
        double dropoffLat
        double dropoffLon
        decimal fareAmount
        decimal surgeMultiplier
        timestamp startedAt
        timestamp endedAt
    }
```

---

## System Design Diagram

```mermaid
flowchart TD
    subgraph Clients_And_Gateway["Mobile Clients & Gateway Layer"]
        RiderApp["Rider App (iOS / Android: Request Ride, Live WS Map Track)"]
        DriverApp["Driver App (iOS / Android: 4s GPS Telemetry, Accept/Decline Offer)"]
        Gateway{"API Gateway / Envoy Proxy (TLS, Auth, Rate Limiting, WebSocket Pool)"}
        NotifSvc["Push Notification Service (Apple APNs / FCM / WebSockets)"]
    end
    subgraph Location_And_Geospatial["Location Ingestion & Geospatial Pipeline"]
        LocIngest["Location Ingestion Service (High-Throughput Go Workers: Lat/Lon to H3 Cell & HMM Map Matching)"]
        Kafka["Apache Kafka (Topic: driver-locations, Partitioned by H3Index)"]
        GeoCache[("Geospatial Cache (Redis / H3: Active Drivers per H3 Cell, 15s TTL Auto-Expire)")]
        ColdTelemetry[("Trajectory Cold Store (Cassandra / AWS S3: Route Reconstruction & Billing Audit)")]
    end
    subgraph Trip_And_Dispatch["Trip Lifecycle & Batched Dispatch Layer"]
        TripSvc["Trip & Booking Service (Trip State Machine: Requested -> Dispatched -> Accepted -> Completed)"]
        TripDB[("Trip Database (PostgreSQL: Optimistic Locking via Version Column, Prevents Double-Dispatch)")]
        DispatchEngine["Dispatch & Matching Engine (Batched Bipartite Matching, Hungarian Algorithm / MCMF)"]
    end
    subgraph Surge_And_Pricing["Dynamic Surge Pricing Engine"]
        SurgeEngine["Dynamic Surge Engine (Apache Flink: Supply/Demand per H3 Cell, Gaussian k-ring Smoothing)"]
    end
    RiderApp -->|"REST / WebSocket"| Gateway
    DriverApp -->|"gRPC / WebSocket (4s GPS Pings)"| Gateway
    Gateway -->|"GPS Stream"| LocIngest
    Gateway -->|"Request / Accept Trip"| TripSvc
    LocIngest -->|"Publish by H3Index"| Kafka
    Kafka -->|"Update Driver Coords (GEOADD)"| GeoCache
    Kafka -->|"Archive Trajectory"| ColdTelemetry
    Kafka -->|"Real-Time Supply/Demand"| SurgeEngine
    SurgeEngine -.->|"Dynamic Multiplier"| TripSvc
    TripSvc -->|"ACID State & Version Check"| TripDB
    TripSvc -->|"Trigger Match"| DispatchEngine
    DispatchEngine -->|"Query H3 k-ring Available Drivers"| GeoCache
    DispatchEngine -->|"Send Offer (15s Countdown)"| NotifSvc
    NotifSvc -->|"Deliver Push / WS Frame"| Gateway
```

---

## Deep Dive & Key Technical Challenges

### 1. Preventing Double-Dispatch Race Conditions

If two drivers accept the same ride offer simultaneously, only one must succeed:

* **Distributed Locking (Redis Redlock / DB Versioning)**:

  ```sql
  UPDATE trips 
  SET driver_id = 'drv_5512', status = 'ACCEPTED', version = version + 1
  WHERE trip_id = 'trip_88102a7b' AND status = 'SEARCHING_FOR_DRIVER';
  ```

* If row count returned is `0`, another driver won the race. The loser receives an immediate `409 Conflict` and is placed back into the available pool.

### 2. Live Map Smoothing & Map Matching

Raw mobile GPS chips provide jittery, inaccurate points with up to 10-meter errors.

* **Map Matching (Hidden Markov Models - HMM)**: Snaps noisy raw coordinates to the underlying road network graph, taking one-way streets, turn restrictions, and legal speed limits into account.
* **Dead Reckoning & Kalman Filters**: On the rider's frontend client, vehicle motion between 4-second GPS ticks is animated smoothly using linear velocity interpolation and bearing dead reckoning.

### 3. Driver Disconnection & Reconnection Resilience

* **Heartbeat Timeout**: If no location ping is received from an active driver for 15 seconds, the system marks the driver as `DISCONNECTED` and removes them from the geospatial index.
* **Mid-Trip Disconnect**: If a driver loses cellular connection while an active trip is in progress, the driver app records the trip route offline in local storage. Once cellular signal returns, the app syncs the completed GPS trajectory and the backend computes the final fare accurately based on actual odometer distance.

---

## Additional Notes

### Video References

* [![Uber System Design Video 1](https://img.youtube.com/vi/lsKU38RKQSo/0.jpg)](https://www.youtube.com/watch?v=lsKU38RKQSo)
* [![Uber System Design Video 2](https://img.youtube.com/vi/umWABit-wbk/0.jpg)](https://www.youtube.com/watch?v=umWABit-wbk)
