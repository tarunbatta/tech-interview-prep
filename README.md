# Tech Interview Preparation Guide

A comprehensive, multi-language interview preparation monorepo covering **Data Structures & Algorithms**, **System Design & Distributed Architectures**, **Engineering Management & Leadership**, and **Interactive Applications** — by [Tarun Batta](https://battatech.com) ([battatech.com](https://battatech.com)).

[![Website: battatech.com](https://img.shields.io/badge/Website-battatech.com-blue.svg)](https://battatech.com)
![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)](https://dotnet.microsoft.com/)
[![Java](https://img.shields.io/badge/Java-CTCI%206th-orange.svg)](src/java/)
[![JavaScript](https://img.shields.io/badge/JavaScript-Node.js-yellow.svg)](src/javascript/)
[![System Design](https://img.shields.io/badge/System%20Design-28%20Case%20Studies-blue.svg)](system-design/system-design.md)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

---

## Table of Contents

- [Three Core Pillars](#three-core-pillars)
- [Monorepo Structure](#monorepo-structure)
- [Quick Start & Common Commands](#quick-start--common-commands)
- [Pillar 1: Coding & Data Structures](#pillar-1-coding--data-structures)
  - [Interactive Applications](#interactive-applications)
  - [Languages & Problem Sets](#languages--problem-sets)
  - [C# Solution Snippet Template](#c-solution-snippet-template)
- [Pillar 2: System Design & Architecture](#pillar-2-system-design--architecture)
  - [Featured Real-World Project Retrospective](#-featured-real-world-project-retrospective)
  - [28 Interview System Design Case Studies](#28-interview-system-design-case-studies)
  - [Back-of-the-Envelope Calculations Cheat Sheet](#back-of-the-envelope-calculations-cheat-sheet)
  - [Load Balancing Cheat Sheet](#load-balancing-cheat-sheet)
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
│ • .NET 10 Core & LeetCode│                       │ • 28 System Deep Dives  │                        │ • CAR / STAR Framework  │
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
│   └── expression-tree/           # Expression tree evaluator (.NET 10)
├── src/                           # Code solutions by ecosystem
│   ├── dotnet/
│   │   ├── core/                  # Core algorithms, data structures, LeetCode, CTCI C#
│   │   └── practice/              # Practice questions (1–54) and QOTD problems
│   ├── java/                      # Cracking the Coding Interview (6th ed.) Java solutions
│   └── javascript/                # JavaScript QOTD and utility solutions
├── system-design/                 # 28 Complete System Design case studies
│   ├── system-design.md           # Master index, estimation cheat sheet, references
│   └── [case-studies]/            # airbnb, uber, whatsapp, youtube, etc.
├── behavioural/                   # Leadership and behavioural interview guide
│   ├── behavioural.md             # Questions, CAR format, ATS resume guide
│   └── sample-resume/             # 20 ATS-compliant resume samples (.docx)
├── docs/                          # Guides, roadmaps, and interview question trackers
│   ├── interview-questions.md     # Real interview questions organized by company
│   ├── top100-leetcode.md         # Curated top 100 LeetCode problems
│   └── study-topics.md            # Concurrency, networking, and algorithm references
├── scripts/                       # Monorepo maintenance and generation tools
│   ├── convert-diagrams.sh        # Automated Draw.io to SVG diagram exporter
│   ├── create-system-design.sh    # Scaffold a new system design case study
│   └── template/                  # System design base templates (.drawio, .md, .svg)
├── shared/                        # Shared assets (dictionaries, scowl word lists)
├── package.json                   # Monorepo tasks (builds, test runs, diagram conversions)
└── README.md                      # Unified guide and cheatsheet
```

---

## Quick Start & Common Commands

All builds, executions, and scripts can be run through `npm` or the native toolchains:

### Using NPM Scripts (Recommended)

```bash
# Build all .NET projects and apps
npm run build

# Run interactive apps
npm run start:chess               # Run the console Chess game
npm run start:expression-tree     # Run Expression Tree evaluator

# Run algorithm problem runners
npm run start:dotnet-core         # Run .NET Core entry point
npm run start:practice            # Run .NET practice suite
npm run start:js                  # Run JavaScript QOTD script

# System design tooling
npm run create-system-design -- <name>   # Scaffold new system design folder & template
npm run convert-diagrams                # Export all updated .drawio diagrams to .svg

# Markdown linting
npm run lint:md                   # Check markdown formatting
npm run lint:md:fix               # Auto-fix markdown formatting
```

### Direct Toolchain Commands

#### .NET 10 Projects

```bash
# Build all projects
dotnet build src/dotnet/core
dotnet build src/dotnet/practice
dotnet build apps/chess-game
dotnet build apps/expression-tree

# Run applications
dotnet run --project apps/chess-game
dotnet run --project apps/expression-tree
dotnet run --project src/dotnet/core
```

#### JavaScript & Node.js

```bash
node src/javascript/qotd/SuperArray.js
```

---

## Pillar 1: Coding & Data Structures

### Interactive Applications

1. **[Console Chess Game](apps/chess-game/)** (`apps/chess-game`): Complete object-oriented console implementation of Chess in C# / .NET 10 featuring full board rendering, movement validation, and game loop.
2. **[Expression Tree Evaluator](apps/expression-tree/)** (`apps/expression-tree`): Expression tree parser and evaluator (.NET 10) demonstrating binary expression tree construction, infix/postfix traversal, and evaluation.

### Languages & Problem Sets

- **.NET 10 Core Solutions** ([`src/dotnet/core`](src/dotnet/core/)):
  - Core data structure implementations (Trie, Heap, Graph, Disjoint Set, SkipList).
  - Comprehensive LeetCode solutions categorized by topic (Arrays, Two Pointers, Trees, Dynamic Programming).
  - Cracking the Coding Interview C# solutions.
- **.NET 10 Practice Suite** ([`src/dotnet/practice`](src/dotnet/practice/)):
  - 54 dedicated practice challenges and Question of the Day (QOTD) problems.
- **Java CTCI 6th Edition** ([`src/java/`](src/java/)):
  - Solutions and notes for Chapters 1 through 17 from Gayle Laakmann McDowell's *Cracking the Coding Interview*.
- **JavaScript Problem Sets** ([`src/javascript/`](src/javascript/)):
  - Data manipulation algorithms, custom collection extensions, and algorithmic scripts.
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

The [`system-design/`](system-design/system-design.md) directory contains real-world production engineering retrospectives alongside 28 comprehensive architecture case studies, complete with functional/non-functional requirements, API schemas, object models, data storage estimates, and editable `.drawio` + vector `.svg` diagrams.

### 🌟 Featured Real-World Project Retrospective

- [**DocuSign Billing & Invoicing Modernization Case Study**](system-design/project-retrospective/project-retrospective.md) — Production architecture retrospective: Migrating legacy Quartz batch systems to an event-driven MuleSoft & Core microservices integration platform. Features before/after architecture, zero-downtime "learning mode" dual-run strategy, and quantified business impact (reduced invoice latency from 7 days to real-time, 90% reduction in reconciliation discrepancies, 80% DSO reduction).

### 28 Interview System Design Case Studies

| System | Focus Areas | Documentation |
| :--- | :--- | :--- |
| **Airbnb** | Search indexing, booking concurrency, payments, availability calendar | [Airbnb Design](system-design/airbnb/airbnb.md) |
| **Amazon** | E-commerce catalog, cart persistence, inventory reservation, checkout | [Amazon Design](system-design/amazon/amazon.md) |
| **Amazon Prime** | Video streaming CDN, chunking, adaptive bitrate, metadata service | [Prime Design](system-design/amazon-prime/amazon-prime.md) |
| **ATM** | Hardware sensors, ISO 8583 protocol, financial ACID saga, HSM encryption | [ATM Design](system-design/atm/atm.md) |
| **Autocomplete Search** | Trie cache, typeahead latency, prefix indexing, ranking | [Autocomplete Design](system-design/autocomplete-search/autocomplete-search.md) |
| **Bitly** | URL shortening, Base62 encoding, high read-to-write ratio caching | [Bitly Design](system-design/bitly/bitly.md) |
| **DocuSign** | Digital signatures, audit trails, document hashing, encryption | [DocuSign Design](system-design/docusign/docusign.md) |
| **Dropbox** | Block-level sync, deduplication, conflict resolution, chunking | [Dropbox Design](system-design/dropbox/dropbox.md) |
| **Email Archive** | Large-scale cold storage, full-text search indexing, compression | [Email Archive Design](system-design/email-archive/email-archive.md) |
| **Google Docs** | Real-time collaborative editing, OT / CRDT, WebSocket sync | [Google Docs Design](system-design/google-docs/google-docs.md) |
| **Google Drive** | File metadata, chunked upload/download, versioning, quota service | [Google Drive Design](system-design/google-drive/google-drive.md) |
| **Instagram** | Feed generation, image delivery CDN, followers graph, fanout | [Instagram Design](system-design/instagram/instagram.md) |
| **Netflix** | Open Connect CDN, dynamic optimizer transcode, ABR, state bookmarks | [Netflix Design](system-design/netflix/netflix.md) |
| **News Feed** | Fanout-on-write vs fanout-on-read, timeline caching, ranking | [News Feed Design](system-design/news-feed/news-feed.md) |
| **Notification System** | Push notifications, SMS/Email gateways, deduplication, rate limits | [Notification Design](system-design/notification-system/notification-system.md) |
| **Parking Garage** | Concurrency control, spot allocation, ticket validation, sensors | [Parking Garage Design](system-design/parking-garage/parking-garage.md) |
| **PasteBin** | Text storage, expiring keys, S3/blob integration, rate limiting | [PasteBin Design](system-design/pastebin/pastebin.md) |
| **Rate Limiter** | Token bucket, sliding window counter, Redis distributed locking | [Rate Limiter Design](system-design/rate-limiter/rate-limiter.md) |
| **Shopify** | Multi-tenant architecture, order processing, flash-sale traffic handling | [Shopify Design](system-design/shopify/shopify.md) |
| **Social Media** | Hybrid fanout, pre-signed media upload, social graph, timeline cache | [Social Media Design](system-design/social-media/social-media.md) |
| **Tag Service** | Inverted index, tag aggregation, lookup performance, caching | [Tag Service Design](system-design/tag-service/tag-service.md) |
| **TicketMaster** | Virtual queues, fair ticketing, seat reservation locking | [TicketMaster Design](system-design/ticketmaster/ticketmaster.md) |
| **TikTok** | Video feed recommendation pipeline, transcode pipeline, delivery | [TikTok Design](system-design/tiktok/tiktok.md) |
| **Twitter** | Distributed timeline, Redis fanout cluster, search indexing | [Twitter Design](system-design/twitter/twitter.md) |
| **Uber** | Geospatial indexing (QuadTree/H3), driver dispatch, location updates | [Uber Design](system-design/uber/uber.md) |
| **Web Crawler** | Distributed frontier queue, robots.txt parser, deduplication, DNS | [Web Crawler Design](system-design/webcrawler/webcrawler.md) |
| **WhatsApp** | Real-time chat, WebSocket connection managers, E2E encryption | [WhatsApp Design](system-design/whatsapp/whatsapp.md) |
| **YouTube** | Video upload pipeline, transcode farm, CDN distribution, views counting | [YouTube Design](system-design/youtube/youtube.md) |

### Back-of-the-Envelope Calculations Cheat Sheet

#### Powers of Two Quick Reference

| Power | Exact Value (Bytes) | Approx Value | Unit |
| :--- | :--- | :--- | :--- |
| **$2^{10}$** | 1,024 | 1 Thousand | 1 KB |
| **$2^{20}$** | 1,048,576 | 1 Million | 1 MB |
| **$2^{30}$** | 1,073,741,824 | 1 Billion | 1 GB |
| **$2^{40}$** | 1,099,511,627,776 | 1 Trillion | 1 TB |
| **$2^{50}$** | 1,125,899,906,842,624 | 1 Quadrillion | 1 PB |

#### SQL Data Type Storage Rules

- **ASCII Character**: 1 Byte
- **Unicode Character**: 2 Bytes
- **String of length $N$**:
  - UTF-8 / ASCII: $N$ bytes
  - Unicode (UTF-16): $2 \times N$ bytes
- **UUID / GUID**: 16 Bytes (binary) or 36 Bytes (string formatted)
- **Timestamp**: 4 Bytes (32-bit Unix) or 8 Bytes (64-bit datetime)

#### Traffic Conversion Rules of Thumb

```text
1 Million requests / month   ≈  0.4 requests / second
2.6 Million requests / month ≈  1.0 request / second
10 Million requests / month  ≈  4.0 requests / second
100 Million requests / month ≈  40 requests / second
1 Billion requests / month   ≈  400 requests / second
```

### Load Balancing Cheat Sheet

- **Layer 3 (Network)**: Routing decisions based on IP address alone (source & destination).
- **Layer 4 (Transport)**: Routing decisions based on IP and TCP/UDP ports. State-aware routing without inspecting payload contents. High throughput, lower CPU overhead.
- **Layer 7 (Application)**: Content-based routing inspecting HTTP headers, cookies, URLs, and JSON payloads. Enables SSL termination, intelligent routing to microservices, and gzip/brotli compression.

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

- `<system-name>.md`: Structured architecture template
- `<system-name>.drawio`: Source diagram template
- `<system-name>.svg`: Vector graphic template
- Automatically updates `system-design/system-design.md` with an alphabetically sorted index entry.

### 2. Export Draw.io Diagrams to SVG

```bash
npm run convert-diagrams
```

- Scans `system-design/` for modified `.drawio` files using SHA-256 caching (`.diagram-tracking`).
- Exports changed diagrams to `.svg` with automatic background styling.
- Prerequisite: `draw.io` desktop application installed.

---

## Contributing & License

Contributions, new interview questions, problem solutions, and system design case studies are welcome! Please ensure all code compiles with zero warnings and markdown files pass linting (`npm run lint:md`).

Distributed under the **MIT License**. See [LICENSE](LICENSE) for more details.
