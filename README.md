# Tech Interview Preparation Guide

A comprehensive, multi-language interview preparation monorepo covering **Data Structures & Algorithms**, **System Design & Distributed Architectures**, **Engineering Management & Leadership**, and **Interactive Applications** — by [Tarun Batta](https://battatech.com) ([battatech.com](https://battatech.com)).

[![Website: battatech.com](https://img.shields.io/badge/Website-battatech.com-blue.svg)](https://battatech.com)
![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)
[![C#](https://img.shields.io/badge/C%23-.NET%2010.0-purple.svg)](src/)
[![JavaScript](https://img.shields.io/badge/JavaScript-Node.js-yellow.svg)](src/qotd/javascript/)
[![System Design](https://img.shields.io/badge/System%20Design-35%20Case%20Studies-blue.svg)](system-design/system-design.md)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

---

## Table of Contents

- [Three Core Pillars](#three-core-pillars)
- [Monorepo Structure](#monorepo-structure)
- [Quick Start & Common Commands](#quick-start--common-commands)
- [Pillar 1: Coding & Data Structures](#pillar-1-coding--data-structures)
  - [Interactive Applications](#interactive-applications)
  - [Categories & Multi-Language Structure](#categories--multi-language-structure)
  - [C# Solution Snippet Template](#c-solution-snippet-template)
- [Pillar 2: System Design & Architecture](#pillar-2-system-design--architecture)
  - [Featured Real-World Project Retrospective](#-featured-real-world-project-retrospective)
  - [System Design Master Guide & 35 Case Studies](#system-design-master-guide--35-case-studies)
- [Pillar 3: Engineering Management & Behavioural](#pillar-3-engineering-management--behavioural)
  - [CAR / STAR Story Framework](#car--star-story-framework)
  - [Role Scope & Strategic Leadership Assessment](#role-scope--strategic-leadership-assessment)
  - [ATS-Friendly Resume Templates](#ats-friendly-resume-templates)
  - [Resume Optimization Tools](#resume-optimization-tools)
- [Reference Cheat Sheets](#reference-cheat-sheets)
  - [Big-O Complexity Tables](#big-o-complexity-tables)
  - [Data Structures Summary](#data-structures-summary)
  - [Sorting Algorithms](#sorting-algorithms)
  - [Searching Algorithms](#searching-algorithms)
  - [Bitwise Operations](#bitwise-operations)
  - [Dynamic Programming](#dynamic-programming)
  - [Regular Expressions](#regular-expressions)
  - [C# Class vs Struct](#c-class-vs-struct)
- [Tooling & Diagram Automation](#tooling--diagram-automation)
- [Contributing & License](#contributing--license)

---

## Three Core Pillars

```text
                               ┌─────────────────────────────────────────────────────────┐
                               │           Tech Interview Preparation Guide              │
                               └────────────────────────────┬────────────────────────────┘
                                                            │
         ┌──────────────────────────────────────────────────┼──────────────────────────────────────────────────┐
         │                                                  │                                                  │
         ▼                                                  ▼                                                  ▼
┌─────────────────────────┐                        ┌─────────────────────────┐                        ┌─────────────────────────┐
│  Coding & Algorithms    │                        │      System Design      │                        │  Leadership & Behaviour │
├─────────────────────────┤                        ├─────────────────────────┤                        ├─────────────────────────┤
│ • .NET 10 Core & LeetCode│                       │ • 35 System Deep Dives  │                        │ • CAR / STAR Framework  │
│ • Java CTCI 6th Edition │                        │ • Architecture Diagrams │                        │ • Strategic Challenges  │
│ • JavaScript Solutions  │                        │ • Estimation Numbers    │                        │ • 20 ATS Resume Models  │
│ • Chess & Expression App│                        │ • Draw.io Tooling       │                        │ • Role Targeting Guide  │
└─────────────────────────┘                        └─────────────────────────┘                        └─────────────────────────┘
```

---

## Monorepo Structure

```text
tech-interview-prep/
├── apps/                          # Executable applications
│   ├── chess-game/                # Console Chess game (.NET 10)
│   ├── expression-tree/           # Expression tree evaluator (.NET 10)
│   └── leetcode-scraper/          # LeetCode problem scraper & C# stub generator (Node.js)
├── src/                           # Problem solutions organized by category & language
│   ├── algorithms/                # Sorting, searching, and general algorithms
│   │   └── csharp/
│   ├── amazon/                    # Amazon interview challenges
│   │   └── csharp/
│   ├── concurrency/               # Threading, synchronization, and concurrent queues
│   │   └── csharp/
│   ├── ctci/                      # Cracking the Coding Interview (Chapters 1–17)
│   │   └── csharp/
│   ├── dataStructure/             # Core data structures (Trie, Heap, Graph, SkipList)
│   │   └── csharp/
│   ├── designPattern/             # Creational, structural, and behavioral patterns
│   │   └── csharp/
│   ├── geeksForGeeks/             # GeeksforGeeks problems
│   │   └── csharp/
│   ├── hackerRank/                # HackerRank problems
│   │   └── csharp/
│   ├── leetcode/                  # LeetCode problems (0001-0100 through 3101-3200)
│   │   └── csharp/
│   ├── qotd/                      # Question of the Day challenges
│   │   ├── csharp/
│   │   └── javascript/
│   └── questions/                 # Practice challenges (Questions 1–54)
│       └── csharp/
├── system-design/                 # 35 Complete System Design case studies & fundamentals
│   ├── system-design.md           # Master hub, interview framework, estimation cheat sheet
│   ├── fundamentals/              # 9-chapter curriculum (networking, DBs, caching, AI/LLM, etc.)
│   └── [case-studies]/            # airbnb, chatgpt-rag-copilot, stock-exchange, uber, etc.
├── behavioural/                   # Leadership and behavioural interview guide
│   ├── behavioural.md             # Questions, CAR format, ATS resume guide
│   └── sample-resume/             # 20 ATS-compliant resume samples (.docx)
├── docs/                          # Guides, roadmaps, and interview question trackers
│   ├── interview-questions.md     # Real interview questions organized by company
│   ├── top100-leetcode.md         # Curated top 100 LeetCode problems
│   └── study-topics.md            # Concurrency, networking, and algorithm references
├── scripts/                       # Monorepo maintenance and generation tools
│   ├── create-system-design.sh    # Scaffold a new system design case study
│   └── template/                  # System design base template (.md with Mermaid diagrams)
├── shared/                        # Shared assets (dictionaries, scowl word lists)
├── package.json                   # Monorepo tasks (builds, test runs, linting)
├── tech-interview-prep.sln        # Root .NET solution linking all projects
└── README.md                      # Unified guide and cheatsheet
```

---

## Quick Start & Common Commands

All builds, executions, and scripts can be run through `npm` or the native toolchains:

### Using NPM Scripts (Recommended)

```bash
# Build all .NET projects and apps via root solution
npm run build

# Run interactive apps
npm run start:chess               # Run the console Chess game
npm run start:expression-tree     # Run Expression Tree evaluator
npm run start:leetcode-scraper    # Run the LeetCode problem scraper

# Run algorithm problem runners
npm run start:dotnet              # Run LeetCode C# entry point (alias: start:dotnet-core)
npm run start:js                  # Run JavaScript QOTD script

# System design tooling
npm run create-system-design -- <name>   # Scaffold new system design folder & template

# Markdown linting
npm run lint:md                   # Check markdown formatting
npm run lint:md:fix               # Auto-fix markdown formatting
```

### Direct Toolchain Commands

#### .NET 10 Solution & Projects

```bash
# Build entire repository (.sln links apps and all category projects)
dotnet build

# Build a single category project
dotnet build src/leetcode/csharp
dotnet build src/algorithms/csharp

# Run applications
dotnet run --project apps/chess-game
dotnet run --project apps/expression-tree
dotnet run --project src/leetcode/csharp
```

#### JavaScript & Node.js

```bash
node src/qotd/javascript/SuperArray.js
node apps/leetcode-scraper/index.js
```

---

## Pillar 1: Coding & Data Structures

### Interactive Applications

1. **[Console Chess Game](apps/chess-game/)** (`apps/chess-game`): Complete object-oriented console implementation of Chess in C# / .NET 10 featuring full board rendering, movement validation, and game loop.
2. **[Expression Tree Evaluator](apps/expression-tree/)** (`apps/expression-tree`): Expression tree parser and evaluator (.NET 10) demonstrating binary expression tree construction, infix/postfix traversal, and evaluation.
3. **[LeetCode Problem Scraper](apps/leetcode-scraper/)** (`apps/leetcode-scraper`): Node.js / Puppeteer scraper that extracts LeetCode problem descriptions and code templates, caches JSON metadata, and generates C# solution stubs.

### Categories & Multi-Language Structure

Solutions under [`src/`](src/) are organized category-first (`src/<category>/<language>/`), allowing solutions across languages (**C#**, **JavaScript**, and future **Python**, **Go**, **Rust**, etc.) to reside side-by-side:

- **LeetCode** ([`src/leetcode/`](src/leetcode/)): Comprehensive LeetCode solutions (0001–3200) in [`csharp/`](src/leetcode/csharp/).
- **Algorithms** ([`src/algorithms/`](src/algorithms/)): Sorting, graph traversal (BFS/DFS, Dijkstra, Bellman-Ford), DP, and MST algorithms in [`csharp/`](src/algorithms/csharp/).
- **Data Structures** ([`src/dataStructure/`](src/dataStructure/)): Core implementations (Trie, Heap, Graph, Disjoint Set, SkipList) in [`csharp/`](src/dataStructure/csharp/).
- **Cracking the Coding Interview (CTCI)** ([`src/ctci/`](src/ctci/)): Complete Chapter 1–17 solutions in [`csharp/`](src/ctci/csharp/).
- **Concurrency** ([`src/concurrency/`](src/concurrency/)): Thread synchronization, read-write locks, blocking queues, and thread-pool implementations in [`csharp/`](src/concurrency/csharp/).
- **Design Patterns** ([`src/designPattern/`](src/designPattern/)): Structural, behavioral, and creational patterns in [`csharp/`](src/designPattern/csharp/).
- **Question of the Day (QOTD)** ([`src/qotd/`](src/qotd/)): Daily algorithmic problems with solutions in [`csharp/`](src/qotd/csharp/) and [`javascript/`](src/qotd/javascript/).
- **Practice Questions** ([`src/questions/`](src/questions/)): 54 curated programming problems in [`csharp/`](src/questions/csharp/).
- **Company & Platform Problems**: Amazon ([`src/amazon/`](src/amazon/)), HackerRank ([`src/hackerRank/`](src/hackerRank/)), and GeeksforGeeks ([`src/geeksForGeeks/`](src/geeksForGeeks/)).
- **Curated Problem Lists**:
  - [Curated Top 100 LeetCode](docs/top100-leetcode.md)
  - [Company Interview Questions](docs/interview-questions.md)
  - [Study Topics & References](docs/study-topics.md)

### C# Solution Snippet Template

Use this VS Code snippet structure for adding new solutions:

```json
"Algo": {
  "prefix": "algo",
  "body": [
    "using System;\n",
    "namespace TechByTarun.InterviewPreperationGuide.Core {",
    "\tpublic class Solution {",
    "\t\tpublic static void Init () {\n",
    "\t\t}\n",
    "\t\tpublic static void Method () {\n",
    "\t\t}",
    "\t}",
    "}"
  ],
  "description": "Algo"
}
```

---

## Pillar 2: System Design & Architecture

All system design theory, core architectural building blocks, capacity estimation cheat sheets, and 35 end-to-end interview case studies are consolidated in **[`system-design/system-design.md`](system-design/system-design.md)**.

### 🌟 Featured Real-World Project Retrospective

- [**DocuSign Billing & Invoicing Modernization Case Study**](system-design/project-retrospective.md) — Production architecture retrospective: Migrating legacy Quartz batch systems to an event-driven MuleSoft & Core microservices integration platform. Features before/after architecture, zero-downtime "learning mode" dual-run strategy, and quantified business impact (reduced invoice latency from 7 days to real-time, 90% reduction in reconciliation discrepancies, 80% DSO reduction).

### System Design Master Guide & 35 Case Studies

Explore the **[System Design Documentation Hub](system-design/system-design.md)** and its **[9-Chapter Fundamentals Curriculum](system-design/system-design.md#system-design-fundamentals-curriculum-9-chapters)**:

- **[Chapter 1: Networking, API Paradigms & Real-Time Protocols](system-design/fundamentals/01-networking-and-api-paradigms.md)** — OSI 7-layer model, TCP vs. UDP vs. QUIC, HTTP/1.1–HTTP/3, **REST vs. GraphQL vs. gRPC**, and **Short Polling vs. Long Polling vs. SSE vs. WebSockets vs. WebRTC** (with stateful WebSocket horizontal scaling).
- **[Chapter 2: Proxies, Load Balancers, API Gateways & CDNs](system-design/fundamentals/02-edge-load-balancing-and-cdn.md)** — Forward vs. Reverse Proxy, L4 vs. L7 Load Balancing, routing algorithms, HA (VRRP/Anycast), API Gateway vs. Service Mesh, and Push vs. Pull CDNs.
- **[Chapter 3: Caching Deep Dive, Compute Scaling & Auto-Scaling](system-design/fundamentals/03-caching-and-scaling.md)** — L1–L6 caching tiers, Redis vs. Memcached, Cache-Aside / Read-Through / Write-Through / Write-Behind / Write-Around, Stampede/Penetration/Avalanche/Hot Key mitigations, Vertical vs. Horizontal Scaling, Auto-Scaling (HPA/KEDA/VPA, connection draining), and Consistent Hashing.
- **[Chapter 4: Databases Deep Dive — SQL vs. NoSQL, Internals & Sharding](system-design/fundamentals/04-databases-sql-vs-nosql.md)** — Relational (ACID) vs. NoSQL (BASE), **Isolation Levels (`MVCC`, Write Skew, Pessimistic vs. Optimistic Locking)**, B+Tree vs. LSM-Tree, Row (OLTP) vs. Columnar (OLAP), Block/File/Object storage (Pre-Signed URLs), **All 10 Database Categories**, Replication, Sharding, Inverted Index, and Geospatial Indexing (Geohash, QuadTree, Uber H3).
- **[Chapter 5: Distributed Systems, Consensus, Clocks, IDs & Messaging](system-design/fundamentals/05-distributed-systems-and-messaging.md)** — CAP & PACELC theorems, Raft/Paxos leader election, Distributed Locks & Fencing Tokens, **2PC vs. Saga vs. Transactional Outbox (CDC)**, Idempotency Keys, **Distributed Unique ID Generation (Twitter Snowflake 64-bit, UUIDv7)**, **Physical vs. Logical Clocks (Lamport, Vector Clocks, TrueTime)**, and Message Queues (RabbitMQ/SQS) vs. Event Streams (Kafka/Kinesis).
- **[Chapter 6: Performance Metrics, Resiliency & Observability](system-design/fundamentals/06-resiliency-observability-and-metrics.md)** — Latency vs. Response Time, Throughput, Little's Law, **P50/P95/P99 Tail Latency Amplification**, Timeouts, Deadline Propagation, Backpressure, SLA/SLO/SLI ("The Nines"), Rate Limiting, Circuit Breakers, and Observability (Metrics, Logs, Tracing).
- **[Chapter 7: Probabilistic Data Structures, Gossip & Stream Processing](system-design/fundamentals/07-probabilistic-structures-and-stream-processing.md)** — **Bloom/Cuckoo Filters**, **Count-Min Sketch (Top-K Heavy Hitters)**, **HyperLogLog (12 KB unique counting)**, **SimHash/MinHash**, **Gossip Protocol & Merkle Trees**, **CRDTs vs. OT**, **Apache Flink Stream Windows & Watermarks**, **CQRS**, **Event Sourcing**, and **Lambda vs. Kappa Architecture**.
- **[Chapter 8: Security, Multi-Region DR & Zero-Downtime Deployments](system-design/fundamentals/08-security-multi-region-and-deployments.md)** — **Session Cookies vs. JWTs (+ JWT Revocation)**, **OAuth 2.0 / OIDC**, **mTLS & SPIFFE**, **RBAC / ABAC / Google Zanzibar ReBAC**, **KMS Envelope Encryption (KEK + DEK)**, **RPO vs. RTO & DR Tiers**, **Cell-Based Architecture & Shuffle Sharding**, **Blue-Green / Canary / Shadow Deployments**, and **Expand-and-Contract DB Migrations**.
- **[Chapter 9: Modern GenAI, LLM & Real-Time ML System Design](system-design/fundamentals/09-genai-llm-and-ml-system-design.md)** — **Production RAG (Semantic Chunking, Hybrid BM25 + Vector ANN, RRF, Cross-Encoder Reranking)**, **LLM Inference Optimization (Prefill vs. Decode, TTFT/ITL, KV-Cache, PagedAttention/vLLM, Continuous Batching, Speculative Decoding)**, **AI Gateway (Semantic Caching, TPM/RPM Rate Limiting, Guardrails)**, **4-Stage Recommendation Funnel**, and **Dual Feature Store**.
- **[35 Interview System Design Case Studies](system-design/system-design.md#interview-system-design-case-studies-35-systems)** & **[Back-of-the-Envelope Calculations Cheat Sheet](system-design/system-design.md#back-of-the-envelope-calculations-cheat-sheet)**.

---

## Pillar 3: Engineering Management & Behavioural

The [`behavioural/`](behavioural/behavioural.md) module covers strategic frameworks, interview questionnaires, and ATS-friendly resume samples for engineering leadership, management, and technical roles.

### CAR / STAR Story Framework

When answering behavioral questions, frame each leadership story with high impact:

- **Challenge (or Situation / Task)**: What was the business context, competitive pressure, or organizational bottleneck?
- **Action**: What specific leadership, technical direction, or organizational initiative did you drive?
- **Results**: What was the measurable business outcome? *(Quantify revenue, latency reduction, reliability improvement, team retention, or delivery timeline).*

### Role Scope & Strategic Leadership Assessment

Before leadership interviews, prepare answers for the core assessment areas:

1. **Career Target**: Target level (EM, Senior EM, Director, VP), target organizations, and strategic challenges you wish to tackle.
2. **Scope of Roles**: Team size, reporting structure, budget, multi-region operations, and key strategic challenges.
3. **Signature Achievements**: 4–5 flagship CAR stories highlighting people leadership, technical strategy, and organizational growth.
4. **Signature Strengths**: Delineation of functional/hard skills (architecture, roadmaps, systems) and soft skills (mentorship, hiring, conflict resolution).

### ATS-Friendly Resume Templates

Located in [`behavioural/sample-resume/`](behavioural/sample-resume/):

- [Ad Exec Resume](behavioural/sample-resume/Ad%20Exec%20Resume%20Sample.docx)
- [Analyst Resume](behavioural/sample-resume/Analyst%20Resume%20Sample.docx)
- [Database Administrator Resume](behavioural/sample-resume/Database%20Administrator%20Resume%20Sample.docx)
- [Financial Advisor Resume](behavioural/sample-resume/Financial%20Advisor%20Resume%20Sample.docx)
- [Financial Analyst Resume](behavioural/sample-resume/Financial%20Analyst%20Resume%20Sample.docx)
- [Financier Resume](behavioural/sample-resume/Financier%20Resume%20Sample.docx)
- [Information Systems Analyst Resume](behavioural/sample-resume/Information%20Systems%20Analyst%20Resume%20Sample.docx)
- [International Law Resume](behavioural/sample-resume/International%20Law%20Resume%20Sample.docx)
- [Lawyer Resume](behavioural/sample-resume/Lawyer%20Resume%20Sample.docx)
- [Logistics Manager Resume](behavioural/sample-resume/Logistics%20Manager%20Resume%20Sample.docx)
- [Marketer Resume](behavioural/sample-resume/Marketer%20Resume%20Sample.docx)
- [Marketing Resume](behavioural/sample-resume/Marketing%20Resume%20Sample.docx)
- [Mechanical Engineer Resume](behavioural/sample-resume/Mechanical%20Engineer%20Resume%20Sample.docx)
- [Nurse Resume](behavioural/sample-resume/Nurse%20Resume%20Sample.docx)
- [Pharmaceutical Resume](behavioural/sample-resume/Pharmacutical%20Resume%20Sample.docx)
- [Programmer Resume](behavioural/sample-resume/Programmer%20Resume%20Sample.docx)
- [Project Manager Resume](behavioural/sample-resume/Project%20Manager%20Resume%20Sample.docx)
- [QA Tester Resume](behavioural/sample-resume/QA%20Tester%20Resume%20Sample.docx)
- [Sales Manager Resume](behavioural/sample-resume/Sales%20Manager%20Resume%20Sample.docx)
- [Teacher Resume](behavioural/sample-resume/Teacher%20Resume%20Sample.docx)

### Resume Optimization Tools

- [**CVWolf**](https://cvwolf.com/index.php): Analyzes job descriptions against your resume to match keywords and optimize for ATS screening algorithms.
- [**CV Word Checker**](https://cvwordchecker.com/): Replaces passive voice and overused verbs with high-impact action verbs.

---

## Reference Cheat Sheets

### Big-O Complexity Tables

#### Common Data Structure Operations

| Data Structure | Avg Access | Avg Search | Avg Insertion | Avg Deletion | Worst Access | Worst Search | Worst Insertion | Worst Deletion | Worst Space |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Array** | $\Theta(1)$ | $\Theta(n)$ | $\Theta(n)$ | $\Theta(n)$ | $O(1)$ | $O(n)$ | $O(n)$ | $O(n)$ | $O(n)$ |
| **Stack** | $\Theta(n)$ | $\Theta(n)$ | $\Theta(1)$ | $\Theta(1)$ | $O(n)$ | $O(n)$ | $O(1)$ | $O(1)$ | $O(n)$ |
| **Queue** | $\Theta(n)$ | $\Theta(n)$ | $\Theta(1)$ | $\Theta(1)$ | $O(n)$ | $O(n)$ | $O(1)$ | $O(1)$ | $O(n)$ |
| **Singly-Linked List** | $\Theta(n)$ | $\Theta(n)$ | $\Theta(1)$ | $\Theta(1)$ | $O(n)$ | $O(n)$ | $O(1)$ | $O(1)$ | $O(n)$ |
| **Doubly-Linked List** | $\Theta(n)$ | $\Theta(n)$ | $\Theta(1)$ | $\Theta(1)$ | $O(n)$ | $O(n)$ | $O(1)$ | $O(1)$ | $O(n)$ |
| **Skip List** | $\Theta(\log n)$ | $\Theta(\log n)$ | $\Theta(\log n)$ | $\Theta(\log n)$ | $O(n)$ | $O(n)$ | $O(n)$ | $O(n)$ | $O(n \log n)$ |
| **Hash Table** | N/A | $\Theta(1)$ | $\Theta(1)$ | $\Theta(1)$ | N/A | $O(n)$ | $O(n)$ | $O(n)$ | $O(n)$ |
| **Binary Search Tree** | $\Theta(\log n)$ | $\Theta(\log n)$ | $\Theta(\log n)$ | $\Theta(\log n)$ | $O(n)$ | $O(n)$ | $O(n)$ | $O(n)$ | $O(n)$ |
| **Cartesian Tree** | N/A | $\Theta(\log n)$ | $\Theta(\log n)$ | $\Theta(\log n)$ | N/A | $O(n)$ | $O(n)$ | $O(n)$ | $O(n)$ |
| **B-Tree** | $\Theta(\log n)$ | $\Theta(\log n)$ | $\Theta(\log n)$ | $\Theta(\log n)$ | $O(\log n)$ | $O(\log n)$ | $O(\log n)$ | $O(\log n)$ | $O(n)$ |
| **Red-Black Tree** | $\Theta(\log n)$ | $\Theta(\log n)$ | $\Theta(\log n)$ | $\Theta(\log n)$ | $O(\log n)$ | $O(\log n)$ | $O(\log n)$ | $O(\log n)$ | $O(n)$ |
| **Splay Tree** | N/A | $\Theta(\log n)$ | $\Theta(\log n)$ | $\Theta(\log n)$ | N/A | $O(\log n)$ | $O(\log n)$ | $O(\log n)$ | $O(n)$ |
| **AVL Tree** | $\Theta(\log n)$ | $\Theta(\log n)$ | $\Theta(\log n)$ | $\Theta(\log n)$ | $O(\log n)$ | $O(\log n)$ | $O(\log n)$ | $O(\log n)$ | $O(n)$ |
| **KD Tree** | $\Theta(\log n)$ | $\Theta(\log n)$ | $\Theta(\log n)$ | $\Theta(\log n)$ | $O(n)$ | $O(n)$ | $O(n)$ | $O(n)$ | $O(n)$ |

#### Array Sorting Algorithms

| Algorithm | Best Time | Average Time | Worst Time | Worst Space |
| :--- | :--- | :--- | :--- | :--- |
| **Quicksort** | $\Omega(n \log n)$ | $\Theta(n \log n)$ | $O(n^2)$ | $O(\log n)$ |
| **Mergesort** | $\Omega(n \log n)$ | $\Theta(n \log n)$ | $O(n \log n)$ | $O(n)$ |
| **Timsort** | $\Omega(n)$ | $\Theta(n \log n)$ | $O(n \log n)$ | $O(n)$ |
| **Heapsort** | $\Omega(n \log n)$ | $\Theta(n \log n)$ | $O(n \log n)$ | $O(1)$ |
| **Bubble Sort** | $\Omega(n)$ | $\Theta(n^2)$ | $O(n^2)$ | $O(1)$ |
| **Insertion Sort** | $\Omega(n)$ | $\Theta(n^2)$ | $O(n^2)$ | $O(1)$ |
| **Selection Sort** | $\Omega(n^2)$ | $\Theta(n^2)$ | $O(n^2)$ | $O(1)$ |
| **Tree Sort** | $\Omega(n \log n)$ | $\Theta(n \log n)$ | $O(n^2)$ | $O(n)$ |
| **Shell Sort** | $\Omega(n \log n)$ | $\Theta(n(\log n)^2)$ | $O(n(\log n)^2)$ | $O(1)$ |
| **Bucket Sort** | $\Omega(n+k)$ | $\Theta(n+k)$ | $O(n^2)$ | $O(n)$ |
| **Radix Sort** | $\Omega(nk)$ | $\Theta(nk)$ | $O(nk)$ | $O(n+k)$ |
| **Counting Sort** | $\Omega(n+k)$ | $\Theta(n+k)$ | $O(n+k)$ | $O(k)$ |
| **Cubesort** | $\Omega(n)$ | $\Theta(n \log n)$ | $O(n \log n)$ | $O(n)$ |

---

### Data Structures Summary

#### Collections Taxonomy

1. **Linear (Ordered by position)**:
   - **Direct Access**: `Array`, `String`, `Struct`.
   - **Sequential Access**: `Stack` (LIFO), `Queue` (FIFO), `Singly/Doubly Linked List`.
   - **Generalized Indexed**: `Hash Table`, `Dictionary`, `Associative Array`.
2. **Non-Linear**:
   - **Hierarchical**: `Tree` (Binary, BST, AVL, Red-Black), `Heap` (Min/Max).
   - **Grouped**: `Set` (unique unordered elements).
   - **Relational / Networks**: `Graph` (directed/undirected), `Weighted Network`.

---

### Sorting Algorithms

- **Bubble Sort**: Compares adjacent pairs, swapping them if out of order. $O(n^2)$ average/worst, $O(1)$ space.
- **Selection Sort**: Repeatedly selects the minimum remaining element and swaps it into place. $O(n^2)$ time, $O(1)$ space.
- **Insertion Sort**: Shifts elements right to insert the current item into its sorted position. Highly efficient for nearly sorted arrays ($O(n)$ best).
- **Merge Sort**: Divide-and-conquer algorithm splitting arrays in halves, sorting recursively, and merging. Guaranteed $O(n \log n)$ time, $O(n)$ space. Stable sort.
- **Quick Sort**: Selects a pivot, partitions elements into less-than and greater-than partitions, and recurses. $O(n \log n)$ average time, $O(\log n)$ space.

---

### Searching Algorithms

- **Sequential / Linear Search**: Inspects elements sequentially. $O(n)$ time. Works on unsorted data.
- **Binary Search**: Halves the search space on sorted inputs. $O(\log n)$ time.

---

### Bitwise Operations

#### Truth Tables

| A | B | A OR B | A AND B | A XOR B |
| :-: | :-: | :---: | :---: | :---: |
| 0 | 0 | 0 | 0 | 0 |
| 0 | 1 | 1 | 0 | 1 |
| 1 | 0 | 1 | 0 | 1 |
| 1 | 1 | 1 | 1 | 0 |

#### C# Integer to Bit Buffer Helper

```csharp
public static string ConvertIntToBits(int val, int bitSize) {
    int mask = 1 << (bitSize - 1);
    StringBuilder bitBuffer = new StringBuilder(35);

    for (int i = 1; i <= bitSize; i++) {
        bitBuffer.Append((val & mask) == 0 ? "0" : "1");
        val <<= 1;
        if (i % 8 == 0) {
            bitBuffer.Append(" ");
        }
    }

    return bitBuffer.ToString();
}
```

---

### Dynamic Programming

Key properties required:

1. **Overlapping Subproblems**: Subproblems are recomputed multiple times.
2. **Optimal Substructure**: The optimal solution to the problem contains optimal solutions to its subproblems.

Approaches:

- **Top-Down (Memoization)**: Recursive decomposition caching subproblem results.
- **Bottom-Up (Tabulation)**: Iterative computation from base cases upwards.

---

### Regular Expressions

#### Quantifiers

- `+` : Match 1 or more occurrences.
- `*` : Match 0 or more occurrences.
- `{n}` : Match exactly $n$ occurrences.
- `{n, m}` : Match between $n$ and $m$ occurrences.
- `.` : Match any character except newlines.

#### Character Classes

- `[a-z]` / `[A-Za-z0-9]` : Character range matches.
- `[^aeiou]` : Negated set (matches any non-vowel).
- `\w` / `\W` : Word character `[A-Za-z0-9_]` vs non-word character.
- `\d` / `\D` : Digit `[0-9]` vs non-digit.
- `\s` / `\S` : Whitespace vs non-whitespace.

#### Assertions

- `^` : Anchor at beginning of input / line.
- `$` : Anchor at end of input / line.
- `\b` : Word boundary.

---

### C# Class vs Struct

| Feature | Class | Structure (`struct`) |
| :--- | :--- | :--- |
| **Type Category** | Reference type | Value type |
| **Memory Allocation** | Allocated on the Managed Heap | Allocated inline (Stack or containing object) |
| **Inheritance** | Supports single inheritance & interfaces | Cannot inherit from other classes/structs (can implement interfaces) |
| **Constructors** | Default parameterless & parameterized constructors | Parameterized constructors; default initialization zeroes memory |
| **Instantiation** | Requires `new` operator | Can be declared without `new` |

```csharp
// Reference type (Heap)
DemoClass obj = new DemoClass();

// Value type (Stack)
DemoStruct val;
```

---

## Tooling & Diagram Automation

The monorepo includes automated bash and npm scripts to simplify maintaining system design documentation:

### 1. Scaffold a New System Design Case Study

```bash
npm run create-system-design -- <system-name>
# Example: npm run create-system-design -- stripe-payments
```

This creates `system-design/<system-name>/` with:

- `<system-name>.md`: Structured architecture template with embedded Mermaid HLD, ER, and detailed System Design diagrams
- Automatically updates `system-design/system-design.md` with an alphabetically sorted index entry.

---

## Contributing & License

Contributions, new interview questions, problem solutions, and system design case studies are welcome! Please ensure all code compiles with zero warnings and markdown files pass linting (`npm run lint:md`).

Distributed under the **MIT License**. See [LICENSE](LICENSE) for more details.
