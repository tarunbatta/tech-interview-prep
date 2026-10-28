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
flowchart TD
    EntryGate["Entry Gate Hardware (ALPR Camera, Induction Loop, Ticket Printer, Barrier Arm)"] -->|"Local Relay (RS-485 / IP)"| BarrierCtrl["Barrier Controller Service"]
    ExitGate["Exit Gate Hardware (ALPR Camera, Barcode Kiosk, EMV Reader, Barrier Arm)"] -->|"Local Relay (RS-485 / IP)"| BarrierCtrl
    subgraph EdgeController["Local Garage Edge Controller (On-Premises Industrial PC)"]
        BarrierCtrl --> AllocEngine["Local Allocation Engine (Closest Stall Match)"]
        DisplayMgr["Floor Display Manager (Ramp LED Signs)"] -->|"Spot Updates"| BarrierCtrl
        DisplayMgr --> EdgeDB[("Local Edge Database (SQLite / Edge Postgres)")]
        AllocEngine <--> EdgeDB
    end
    subgraph CloudPlatform["Central Cloud Reservation Platform"]
        BookingSvc["Mobile Booking Service (Driver App Pre-book)"]
        TariffSvc["Tariff & Billing Service (Dynamic Pricing)"]
        TelemetrySvc["Fleet Telemetry & Monitoring"]
        CloudDB[("Cloud Database Cluster (Aurora PostgreSQL: Multi-Garage Inventory)")]
        BookingSvc --> CloudDB
        TariffSvc --> CloudDB
        TelemetrySvc --> CloudDB
    end
    EdgeDB <-->|"MQTT over TLS (Bi-directional)"| BookingSvc
    EdgeDB <-->|"MQTT over TLS (Bi-directional)"| TariffSvc
    EdgeDB -->|"MQTT over TLS"| TelemetrySvc
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
flowchart TD
    subgraph PhysicalGarage["Physical Garage Hardware & IoT Layer"]
        EntryHW["Physical Entry Gate (ALPR Camera, Ground Induction Loop, Barcode Dispenser, Motorized Barrier Arm)"]
        ExitHW["Physical Exit Gate (ALPR Camera, Barcode/Ticket Scanner, Contactless EMV Reader, Motorized Exit Barrier)"]
        FloorHW["Floor Displays & Spot Sensors (Ultrasonic Overhead Sensors, EV Charger Status Monitor, Real-Time LED Display Signs)"]
    end
    subgraph EdgeCluster["Local Garage Edge Controller (On-Premises Industrial PC)"]
        BarrierRelay["Barrier Controller Relay (Sub-second Local Actuation & Safety Interlock)"]
        SpotEngine["Spot Allocation Engine (Greedy Closest-Stall Match & Dual Quota Guard)"]
        SignBroker["Floor Sign MQTT Broker (Real-Time Wayfinding Updates)"]
        EdgeDB[("Local Offline Database (SQLite / Edge PostgreSQL: Active Tickets, Stall Inventory, Tariff Cards)")]
    end
    subgraph SyncLayer["Edge-to-Cloud Synchronization Tier"]
        SyncBridge["IoT Sync Gateway (MQTT over TLS / WebSockets Broker: Bidirectional Sync & Offline Queue Reconciliation)"]
    end
    subgraph CloudBackend["Central Cloud Services (Multi-Garage Fleet Platform)"]
        MobileApp["Driver Mobile App (iOS / Android: Pre-book Space, QR Pass, Auto-Exit via Plate)"]
        BookingSvc["Mobile App Booking Service (40% Pre-Booking Quota & 30-Min Virtual Holds)"]
        BillingSvc["Dynamic Tariff & Billing Service (Peak Pricing & Duration Calculation)"]
        PayGateway["Payment Gateway Integration (Stripe / EMV Settlement)"]
        Telemetry["Fleet Telemetry & Analytics (Occupancy Heatmaps & Hardware Health)"]
        CloudDB[("Cloud Database Cluster (Aurora PostgreSQL / DynamoDB: Multi-Garage Inventory, Reservations, Billing Ledger)")]
    end
    EntryHW -->|"Vehicle Arrived (RS-485 / IP)"| BarrierRelay
    ExitHW -->|"Vehicle Exit / Checkout Scan"| BarrierRelay
    FloorHW <-->|"Spot Occupied/Free & LED Count"| SignBroker
    BarrierRelay <-->|"Allocate / Validate Ticket"| SpotEngine
    SpotEngine <-->|"ACID Local Read/Write"| EdgeDB
    SignBroker <-->|"Read Available Counts"| EdgeDB
    EdgeDB <-->|"MQTT over TLS (Idempotent Batch Sync)"| SyncBridge
    MobileApp -->|"REST / GraphQL"| BookingSvc
    SyncBridge <-->|"gRPC / Kafka"| BookingSvc
    SyncBridge <-->|"gRPC / Kafka"| BillingSvc
    SyncBridge -->|"Stream Metrics"| Telemetry
    BillingSvc <-->|"Authorize & Capture"| PayGateway
    BookingSvc <-->|"Reserve & Sync Inventory (5% Contingency Pool)"| CloudDB
    BillingSvc <-->|"Record Invoices & Ledger"| CloudDB
    Telemetry -->|"Persist Fleet Logs"| CloudDB
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
