# Parking Garage System Design

[← Back to System Design](system-design.md)

## Overview

A smart parking garage management system orchestrates vehicle access, real-time stall occupancy tracking, dynamic spot allocation, and automated billing across multi-level physical parking structures. Modern installations integrate physical hardware—Automatic License Plate Recognition (ALPR) cameras, ground induction loops, ultrasonic overhead stall sensors, and motorized barrier arms—with edge IoT controllers and cloud reservation platforms.

Designing an enterprise parking garage system requires bridging physical-world hardware with distributed software:

1. **Edge-to-Cloud Hybrid Architecture & Offline Autonomy**: If the facility loses internet connectivity, the local garage controller must continue admitting vehicles, lifting barrier arms, and processing payments without downtime.
2. **Real-Time Spot Allocation & Floor Guidance**: Directing incoming motorists to the nearest available stall matching their vehicle size (Compact, Regular, Large, EV, Handicapped).
3. **Zero Double-Allocation & Race Condition Prevention**: Preventing two drivers (or a drive-up customer and a mobile pre-booking app) from claiming the same physical parking spot simultaneously.
4. **Automated ALPR Ticketless Checkout**: Seamless drive-in and drive-out billing linked to credit card profiles.

---

## Functional Requirements

* **Automated Entry Processing**:
  * Vehicle triggers ground induction loop.
  * ALPR camera captures license plate number.
  * System allocates an optimal stall, prints a timestamped barcode ticket (or links to driver's pre-paid mobile profile), and triggers the motorized barrier arm to lift.
* **Multi-Tier Spot Classification**:
  * Supports **Compact** (motorcycle / small car), **Regular** (sedan / SUV), **Large** (van / truck), **EV** (with electric charger), and **Handicapped / Accessible** stalls across multiple floors.
* **Real-Time Wayfinding & Displays**: Overhead digital LED signboards at garage entrances and floor ramps show real-time available counts per floor and vehicle category.
* **Automated Exit & Payment**:
  * Exit kiosk validates ticket barcode or detects license plate.
  * Calculates fee based on duration, vehicle class, and dynamic peak tariffs.
  * Accepts credit card, contactless Apple Pay/Google Pay, or debits driver's registered account.
  * Lifts barrier arm upon payment confirmation.
* **Mobile Pre-Booking**: Drivers can reserve a guaranteed stall in advance for a specified time window with a 15-minute grace period.

---

## Non-Functional Requirements

* **Offline Edge Resilience**: Garage entry/exit gates must operate at **$100\%$ availability** even during prolonged cloud network outages.
* **Sub-Second Gate Response**: Barrier arm must raise within **$< 1\text{ second}$** of vehicle arrival or payment authorization to prevent street traffic queues.
* **Strict Physical Consistency**: Under no circumstances can the system issue more entry tickets than physical open stalls (Zero over-admission).
* **Fault Tolerance & Safety**: Barrier arms must feature infrared safety sensors to never close on a pedestrian or moving vehicle.

---

## Back of the Envelope Estimations

### Single Facility Scale

* **Garage Capacity**: 5 floors $\times$ 200 stalls $= 1,000\text{ parking spots}$.
* **Daily Vehicle Turnover**: Each spot used by 3 vehicles/day $= 3,000\text{ parking events/day}$.
* **Peak Entry Rate (Morning Rush)**:
  * 600 vehicles arriving between 8:00 AM and 9:00 AM.
  * Average Peak Entry Rate $= \frac{600}{3,600} \approx 0.17\text{ vehicles/sec}$ (1 vehicle every 6 seconds across 2 entry gates).

### Nationwide Fleet Scale (10,000 Garages)

* **Total Network Capacity**: $10,000\text{ facilities} \times 1,000\text{ spots} = 10,000,000\text{ parking spots}$.
* **Daily Ingress/Egress Transactions**:
  $$10,000\text{ garages} \times 3,000 = 30,000,000\text{ parking events/day}$$
* **Cloud API QPS**:
  $$\text{Average QPS} = \frac{30,000,000}{86,400} \approx 347\text{ QPS} \quad (\text{Peak: } \approx 2,500\text{ QPS})$$
* **Storage Requirement**:
  * 30M tickets/day $\times 500\text{ bytes per ticket} \approx 15\text{ GB/day} \implies 5.5\text{ Terabytes/year}$.

---

## High-Level System Architecture

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 25, 'rankSpacing': 45, 'padding': 10}}}%%
flowchart LR
    subgraph HardwareTier["Gate & Floor Hardware"]
        EntryGate["Entry Gate (ALPR & Barrier)"]
        ExitGate["Exit Gate (Kiosk & EMV)"]
        DisplayMgr["Ramp LED Signboards"]
    end
    subgraph EdgeController["Local Edge Controller"]
        BarrierCtrl["Barrier Controller Svc"]
        AllocEngine["Spot Allocation Engine"]
        EdgeDB[("Local Edge DB (SQLite)")]
    end
    subgraph CloudServices["Cloud Fleet Services"]
        BookingSvc["Mobile Booking Service"]
        TariffSvc["Tariff & Billing Svc"]
        TelemetrySvc["Fleet Telemetry Svc"]
    end
    subgraph CloudStorage["Cloud Data Layer"]
        CloudDB[("Aurora PostgreSQL")]
    end
    EntryGate e1@-->|"RS-485 Relay"| BarrierCtrl
    ExitGate e2@-->|"RS-485 Relay"| BarrierCtrl
    BarrierCtrl e3@-->|"Spot Updates"| DisplayMgr
    BarrierCtrl e4@-->|"Match Stall"| AllocEngine
    DisplayMgr e5@-->|"Read Counts"| EdgeDB
    AllocEngine e6@-->|"Local ACID"| EdgeDB
    EdgeDB e7@-->|"MQTT Sync"| BookingSvc
    EdgeDB e8@-->|"MQTT Sync"| TariffSvc
    EdgeDB e9@-->|"Telemetry"| TelemetrySvc
    BookingSvc e10@-->|"Reservations"| CloudDB
    TariffSvc e11@-->|"Rate Cards"| CloudDB
    TelemetrySvc e12@-->|"Metrics"| CloudDB

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
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef worker fill:#0e7490,stroke:#22d3ee,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff

    class EntryGate,ExitGate,DisplayMgr client
    class BarrierCtrl,AllocEngine edge
    class BookingSvc,TariffSvc service
    class TelemetrySvc worker
    class EdgeDB,CloudDB storage

    style HardwareTier fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style EdgeController fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style CloudServices fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style CloudStorage fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Object-Oriented Domain Model

```mermaid
classDiagram
    class ParkingGarage {
        +String garageId
        +String name
        +Address address
        +List~ParkingFloor~ floors
        +List~Gate~ entryGates
        +List~Gate~ exitGates
    }
    class ParkingFloor {
        +int floorNumber
        +List~ParkingSpot~ spots
        +DisplayBoard displayBoard
    }
    class ParkingSpot {
        +String spotId
        +int floorNumber
        +SpotType spotType
        +boolean isOccupied
        +int distanceToElevator
        +canFitVehicle(VehicleType type) boolean
    }
    class Vehicle {
        +String licensePlate
        +VehicleType vehicleType
        +String driverId
    }
    class ParkingTicket {
        +String ticketId
        +String spotId
        +String licensePlate
        +Timestamp issuedAt
        +Timestamp paidAt
        +Timestamp exitedAt
        +Decimal feeAmount
        +TicketStatus status
    }
    ParkingGarage "1" *-- "*" ParkingFloor : "contains"
    ParkingFloor "1" *-- "*" ParkingSpot : "manages"
    ParkingSpot "1" -- "*" ParkingTicket : "allocated to"
    Vehicle "1" -- "*" ParkingTicket : "issued"
```

### Spot Allocation Strategy

```java
public enum SpotType {
    COMPACT, REGULAR, LARGE, EV, ACCESSIBLE
}

public class SpotAllocationEngine {
    // Greedy heuristic: allocate closest available spot matching vehicle constraint
    public synchronized ParkingSpot allocateSpot(Vehicle vehicle, int entryGateId) {
        return availableSpots.stream()
            .filter(spot -> spot.canFitVehicle(vehicle.getType()))
            .min(Comparator.comparingInt(spot -> spot.getDistanceToGate(entryGateId)))
            .orElseThrow(() -> new GarageFullException("No suitable spots available"));
    }
}
```

---

## API Specifications

### 1. Ingest Entry Scan (Local Edge Gate)

```http
POST /api/v1/edge/entry
Content-Type: application/json
```

```json
// Request
{
  "gateId": "gate_entry_01",
  "scannedLicensePlate": "7XYZ912",
  "detectedVehicleClass": "SEDAN",
  "timestamp": 1730008500
}

// Response: 200 OK
{
  "ticketId": "tkt_8819231",
  "allocatedSpot": {
    "spotId": "F2-R045",
    "floorNumber": 2,
    "spotType": "REGULAR"
  },
  "action": "LIFT_BARRIER_ARM",
  "barcodeData": "BARCODE_DATA_8819231"
}
```

### 2. Process Exit & Checkout

```http
POST /api/v1/edge/exit
Content-Type: application/json
```

```json
// Request
{
  "gateId": "gate_exit_01",
  "ticketId": "tkt_8819231",
  "paymentMethod": "EMV_CONTACTLESS",
  "paymentToken": "tok_visa_4242"
}

// Response: 200 OK
{
  "totalDurationMinutes": 145,
  "chargedAmount": 18.50,
  "currency": "USD",
  "paymentStatus": "SUCCESS",
  "action": "LIFT_BARRIER_ARM"
}
```

---

## System Design Diagram

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph PhysicalGarage["Hardware & Clients"]
        EntryHW["Entry Gate (ALPR & Barrier)"]
        ExitHW["Exit Gate (Scanner & EMV)"]
        FloorHW["Ultrasonic Sensors & Signs"]
        MobileApp["Driver Mobile App (Pre-Book)"]
    end
    subgraph EdgeCluster["Local Garage Edge Controller"]
        BarrierRelay["Barrier Controller Relay"]
        SpotEngine["Spot Allocation Engine"]
        SignBroker["Floor Sign MQTT Broker"]
        SyncBridge{"IoT Sync Gateway (MQTT/TLS)"}
    end
    subgraph CloudBackend["Cloud Fleet Microservices"]
        BookingSvc["Booking Svc (40% Quota)"]
        BillingSvc["Dynamic Tariff & Billing"]
        PayGateway["Payment Gateway (Stripe)"]
        Telemetry["Fleet Telemetry & Health"]
    end
    subgraph DataTier["Edge & Cloud Persistence"]
        EdgeDB[("Local Edge DB (SQLite)")]
        CloudDB[("Aurora PostgreSQL Cluster")]
    end
    EntryHW e1@-->|"Vehicle Arrived"| BarrierRelay
    ExitHW e2@-->|"Checkout Scan"| BarrierRelay
    FloorHW e3@-->|"Stall Status"| SignBroker
    BarrierRelay e4@-->|"Allocate Stall"| SpotEngine
    SpotEngine e5@-->|"ACID Read/Write"| EdgeDB
    SignBroker e6@-->|"Open Counts"| EdgeDB
    SpotEngine e7@-->|"Batch Sync"| SyncBridge
    MobileApp e8@-->|"REST / GraphQL"| BookingSvc
    SyncBridge e9@-->|"gRPC Sync"| BookingSvc
    SyncBridge e10@-->|"gRPC Sync"| BillingSvc
    SyncBridge e11@-->|"Stream Metrics"| Telemetry
    BillingSvc e12@-->|"Auth & Capture"| PayGateway
    BookingSvc e13@-->|"Reserve Spot"| CloudDB
    BillingSvc e14@-->|"Write Ledger"| CloudDB
    Telemetry e15@-->|"Persist Logs"| CloudDB

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
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class EntryHW,ExitHW,FloorHW,MobileApp client
    class BarrierRelay,SpotEngine,SyncBridge edge
    class SignBroker queue
    class BookingSvc,BillingSvc,PayGateway service
    class Telemetry worker
    class EdgeDB,CloudDB storage

    style PhysicalGarage fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style EdgeCluster fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style CloudBackend fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style DataTier fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

---

## Deep Dive & Key Technical Challenges

### 1. Edge-to-Cloud Synchronization & Offline Mode

* Garages frequently experience fiber cuts or local ISP outages.
* **Local Autonomous Operation**:
  * The Edge Controller maintains local SQLite/PostgreSQL tables containing current stall allocations, active tickets, and local tariff rate cards.
  * Gate barriers open and close normally via local hardware relays without making WAN calls.
* **Reconciliation on Reconnect**:
  * Upon WAN restoration, the Edge Controller publishes an idempotent batch sync via MQTT to the cloud backend.
  * Pre-bookings made in the mobile app during the disconnection window are merged; if a conflict occurs, the system utilizes a dedicated reserve pool of 5% contingency stalls.

### 2. Preventing Drive-Up vs. Pre-Booking Race Conditions

* If a mobile user pre-books spot `F1-R010` for 2:00 PM, a physical drive-up customer entering at 1:55 PM must not be routed into that space.
* **Dual Inventory Bucketing**:
  * The system separates physical capacity into two pools:
    1. **Pre-Booking Quota (Max 40% of garage)**.
    2. **Drive-Up Transient Quota (60% of garage)**.
  * When a pre-booking is confirmed, a virtual hold is placed. The spot allocation engine marks that spot unavailable for physical drive-up customers starting 30 minutes prior to reservation start time.

---

## Additional Notes

### Video References

* [![Parking Garage System Design Video 1](https://img.youtube.com/vi/NtMvNh0WFVM/0.jpg)](https://www.youtube.com/watch?v=NtMvNh0WFVM)
* [![Parking Garage System Design Video 2](https://img.youtube.com/vi/eV5Xh6jNfmU/0.jpg)](https://www.youtube.com/watch?v=eV5Xh6jNfmU)
