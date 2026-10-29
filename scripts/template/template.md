# Template System Design

[← Back to System Design](system-design.md)

## Overview

## Functional Requirements

## Non-Functional Requirements

## Back of the Envelope Estimations

## High-Level System Architecture

```mermaid
%%{init: {'flowchart': {'nodeSpacing': 25, 'rankSpacing': 45, 'padding': 10}}}%%
flowchart LR
    Client["Client App (Web / Mobile)"]
    subgraph Edge["Edge & Ingress"]
        CDN["Global Edge CDN"]
        LB["Load Balancer / API Gateway"]
    end
    subgraph Services["Application Tier"]
        App["Application Service"]
    end
    subgraph Storage["Data & Cache Tier"]
        Cache[("Distributed Cache (Redis)")]
        DB[("Primary Database")]
    end
    Client e1@-->|"Static Assets"| CDN
    Client e2@-->|"API Requests"| LB
    LB e3@-->|"Route"| App
    App e4@-->|"Read / Write"| Cache
    App e5@-->|"Persist"| DB

    e1@{ animate: true }
    e2@{ animate: true }
    e3@{ animate: true }
    e4@{ animate: true }
    e5@{ animate: true }

    classDef client fill:#1d4ed8,stroke:#60a5fa,stroke-width:2px,color:#ffffff
    classDef edge fill:#6d28d9,stroke:#a78bfa,stroke-width:2px,color:#ffffff
    classDef service fill:#047857,stroke:#34d399,stroke-width:2px,color:#ffffff
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff

    class Client client
    class CDN,LB edge
    class App service
    class Cache cache
    class DB storage

    style Edge fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style Services fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style Storage fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

## Core Components & Data Flows

## API Endpoints

## Object Model & Database Schema

### Database Selection & Rationale

| Workload / Domain | Database Choice | Data Model & CAP Profile | Why This Database? (Justification & Trade-offs) |
| :--- | :--- | :--- | :--- |
| **Core Transactional Data** | **PostgreSQL / Amazon Aurora** | Relational (ACID, **CP / PC-EC**) | Enforces strict referential integrity, multi-row ACID transactions, and strong consistency for core entity mutations. Sharded by `userId` or `resourceId` as volume scales. |
| **High-Throughput / Time-Series Logs** | **Apache Cassandra / DynamoDB** | Wide-Column / Key-Value (**AP / PA-EL**) | Optimized for high-velocity append-only writes and predictable single-digit millisecond reads by partition key, trading cross-partition joins for linear horizontal scalability. |
| **Hot Lookups & Distributed Locks** | **Redis Cluster** | In-Memory Key-Value (**AP / Sub-ms**) | Absorbs $80\text{--}90\%$ of read traffic in front of the primary database and supports atomic operations (`SETNX`, Lua scripts, Sorted Sets) for rate limiting and ephemeral state. |
| **Full-Text & Faceted Search** | **Elasticsearch / OpenSearch** | Inverted Index (**AP / Near Real-Time**) | Enables fuzzy full-text search, multi-attribute filtering, and relevance scoring that would cause expensive full-table scans (`LIKE '%...%'`) in a relational database. |
| **Unstructured Media & Blobs** | **Amazon S3 + Edge CDN** | Object Storage (**11 9s Durability**) | Keeps multi-megabyte binary payloads out of transactional database pages, reducing DB buffer pool pressure and delivering static assets globally from edge PoPs. |

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
%%{init: {'flowchart': {'nodeSpacing': 20, 'rankSpacing': 40, 'padding': 10}}}%%
flowchart LR
    subgraph Clients["Clients Layer"]
        Client["Client Apps (iOS, Android, Web)"]
    end
    subgraph Edge["Edge & Ingress Layer"]
        CDN["Global Edge CDN"]
        Gateway{"API Gateway (Auth, Rate Limit, TLS)"}
    end
    subgraph Services["Core Microservices"]
        ReadSvc["Read Service"]
        WriteSvc["Write Service"]
        Worker["Async Worker Pool"]
    end
    subgraph Storage["Caching, Messaging & Persistence"]
        Blob[("Object Storage (S3)")]
        Redis[("Redis Cluster")]
        PrimaryDB[("Sharded Database")]
        Queue[["Kafka Event Bus"]]
    end
    Client e1@-->|"Static / Media"| CDN
    CDN e2@-->|"Origin Fetch"| Blob
    Client e3@-->|"REST / gRPC"| Gateway
    Gateway e4@-->|"Queries"| ReadSvc
    Gateway e5@-->|"Mutations"| WriteSvc
    ReadSvc e6@-->|"Cache Lookup"| Redis
    ReadSvc e7@-->|"Read Replica"| PrimaryDB
    WriteSvc e8@-->|"ACID Write"| PrimaryDB
    WriteSvc e9@-->|"Publish Event"| Queue
    Worker e10@-->|"Consume & Process"| Queue
    Worker e11@-->|"Hydrate Cache"| Redis
    Worker e12@-->|"Write Media"| Blob

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
    classDef cache fill:#be123c,stroke:#fb7185,stroke-width:2px,color:#ffffff
    classDef storage fill:#b45309,stroke:#fbbf24,stroke-width:2px,color:#ffffff
    classDef queue fill:#3730a3,stroke:#818cf8,stroke-width:2px,color:#ffffff

    class Client client
    class CDN,Gateway edge
    class ReadSvc,WriteSvc service
    class Worker worker
    class Redis cache
    class PrimaryDB,Blob storage
    class Queue queue

    style Clients fill:transparent,stroke:#60a5fa,stroke-width:1.5px,stroke-dasharray: 5 5
    style Edge fill:transparent,stroke:#a78bfa,stroke-width:1.5px,stroke-dasharray: 5 5
    style Services fill:transparent,stroke:#34d399,stroke-width:1.5px,stroke-dasharray: 5 5
    style Storage fill:transparent,stroke:#fbbf24,stroke-width:1.5px,stroke-dasharray: 5 5
```

## Deep Dive & Key Technical Challenges

## Additional Notes

### Video References
