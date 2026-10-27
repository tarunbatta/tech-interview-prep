# Tag Service System Design

[← Back to System Design](../system-design.md)

## Overview

A tag service is a system that manages and organizes content through metadata tags, enabling efficient content categorization and retrieval. The system needs to handle tag creation, association, and querying across multiple content types, support tag hierarchies, and provide fast tag-based search capabilities. It requires efficient indexing mechanisms, flexible tag relationships, and a scalable infrastructure to support millions of tags and their associations while maintaining quick search performance.

## Functional Requirements

## Non-Functional Requirements

* Fault Tolerant
* Scalable
  * Throughput
  * Storage
* Resilience

## Back of the Envelope Estimations

* Daily Active Users (DAU): 10^6
* Operations per User per Day: 10
* Total Daily Operations: 10^7
* QPS: 10^7 / 10^5 = 100

* Read/Write Ratio: 10 reads per write
* Total Write Operations: 1 million write operations/day
* Total Read Operations: 9 million read operations/day

* Bytes per tag: 16 characters × 2 bytes = 32 bytes
* Total throughput: 10^7 × 32 bytes = 320 MB
* Monthly New Tags: 30 million tags/month
* Storage Requirement per Day: 1,000,000 tags × 32 bytes = 32 MB/day
* Storage Requirement per Month: 32 MB × 30 days = 960 MB (~1 GB)

## API Endpoints

``` c-sharp
POST /tag 
{
  tagId,
  postId
}
returns 

GET /tag
{
  tagId
}
returns List<Tag>;

GET /post
{
  postId
}
returns List<Tag>;
```

## Object Model

Tag

* tagId
* tagname

Item

* itemId
* itemName

TagItem

* itemId
* tagId

## System Design Diagram

![Tag Service System Design](tag-service.svg)

[Download Draw.io File](tag-service.drawio)

## Additional Notes
