# Template System Design

[← Back to System Design](system-design.md)

## Overview

## Functional Requirements

## Non-Functional Requirements

## Back of the Envelope Estimations

## High-Level System Architecture

```mermaid
flowchart TD
    Client["Client App (Web / Mobile)"] --> CDN["Global Edge CDN"]
    Client --> LB["Load Balancer / API Gateway"]
    LB --> App["Application Service"]
    App --> Cache[("Distributed Cache (Redis)")]
    App --> DB[("Primary Database")]
```

## Core Components & Data Flows

## API Endpoints

## Object Model & Database Schema

```mermaid
erDiagram
    User ||--o{ Resource : "owns"
    User {
        bigint userId PK
        varchar_255 email
        timestamp createdAt
    }
    Resource {
        bigint resourceId PK
        bigint userId FK
        varchar_255 status
        timestamp updatedAt
    }
```

## System Design Diagram

```mermaid
flowchart TD
    subgraph Clients["Clients Layer"]
        Client["Client Apps (iOS, Android, Web)"]
    end
    subgraph Edge["Edge & Ingress Layer"]
        CDN["Global Edge CDN"]
        Gateway["API Gateway (Auth, Rate Limit, TLS)"]
    end
    subgraph Services["Core Microservices"]
        ReadSvc["Read Service"]
        WriteSvc["Write Service"]
        Worker["Async Worker Pool"]
    end
    subgraph Storage["Caching, Messaging & Persistence"]
        Redis[("Redis Cluster")]
        Queue{{"Kafka Event Bus"}}
        PrimaryDB[("Sharded Database")]
        Blob[("Object Storage (S3)")]
    end
    Client -->|"Static / Media"| CDN
    CDN -->|"Origin Fetch"| Blob
    Client -->|"REST / gRPC"| Gateway
    Gateway --> ReadSvc
    Gateway --> WriteSvc
    ReadSvc --> Redis
    ReadSvc --> PrimaryDB
    WriteSvc --> PrimaryDB
    WriteSvc --> Queue
    Queue --> Worker
    Worker --> Redis
    Worker --> Blob
```

## Deep Dive & Key Technical Challenges

## Additional Notes

### Video References
