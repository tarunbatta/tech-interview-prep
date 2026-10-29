# Study Topics & References

Non-code notes carried over from the old `todo.md`. The coding problems from that list now live under
[`src/`](../src) (see [`amazon/`](../src/amazon/csharp), [`concurrency/`](../src/concurrency/csharp),
[`algorithms/others/`](../src/algorithms/csharp/others) and [`leetcode/`](../src/leetcode/csharp)).

## Concurrency & Distributed Systems

Common interview discussion topics (eBay and others), cross-linked to the **[9-Chapter System Design Fundamentals Curriculum](../system-design/system-design.md#system-design-fundamentals-curriculum-9-chapters)**, **[35 System Design Case Studies](../system-design/system-design.md#interview-system-design-case-studies-35-systems)**, and C# implementations:

- **Concurrency fundamentals: threads vs. processes, locks, condition variables, deadlock, starvation** — see [Chapter 5: Local Concurrency Primitives vs. Distributed Locks](../system-design/fundamentals/05-distributed-systems-and-messaging.md#local-concurrency-primitives-vs-distributed-locks) and [Chapter 4: Database Isolation Levels & Concurrency Control](../system-design/fundamentals/04-databases-sql-vs-nosql.md#database-isolation-levels--concurrency-control-ddia-deep-dive)
- **Read-write locks** — see C# implementation [`readWriteLock.cs`](../src/concurrency/csharp/readWriteLock.cs) and [Chapter 5: Local Concurrency Primitives](../system-design/fundamentals/05-distributed-systems-and-messaging.md#local-concurrency-primitives-vs-distributed-locks)
- **Blocking concurrent queue / producer-consumer** — see C# implementation [`blockingQueue.cs`](../src/concurrency/csharp/blockingQueue.cs), [Chapter 6: Backpressure](../system-design/fundamentals/06-resiliency-observability-and-metrics.md#timeouts-deadlines--backpressure), and lock-free ring buffers in [Stock Exchange Matching Engine](../system-design/stock-exchange.md)
- **Sliding window protocol (flow control & rate limiting)** — see [Chapter 1: TCP Flow & Congestion Control](../system-design/fundamentals/01-networking-and-api-paradigms.md#transport-layer-tcp-vs-udp-vs-quic), [Chapter 6: Sliding Window Rate Limiting](../system-design/fundamentals/06-resiliency-observability-and-metrics.md#rate-limiting-algorithms-comparison), and [Rate Limiter Design](../system-design/rate-limiter.md)
- **Two-phase commit (2PC) and its failure modes** — see [Chapter 5: 2PC vs. Saga vs. Transactional Outbox](../system-design/fundamentals/05-distributed-systems-and-messaging.md#4-distributed-transactions-2pc-vs-saga-vs-transactional-outbox), [ATM Design](../system-design/atm.md), and [Payment System Design](../system-design/payment-system.md)
- **Pub/sub messaging & event streams** — see [Chapter 5: Message Queues (RabbitMQ/SQS) vs. Pub/Sub Streams (Kafka)](../system-design/fundamentals/05-distributed-systems-and-messaging.md#8-asynchronous-messaging-message-queues-vs-pubsub-event-streams) and [Chapter 7: Stream Processing (Apache Flink)](../system-design/fundamentals/07-probabilistic-structures-and-stream-processing.md#3-stream-processing--event-driven-data-architectures)
- **Stateless services and 3-tier architecture** — see [Chapter 3: Stateless vs. Stateful Compute Tiers & Auto-Scaling](../system-design/fundamentals/03-caching-and-scaling.md#stateless-vs-stateful-compute-tiers) and [End-to-End Request Lifecycle Architecture](../system-design/system-design.md#end-to-end-request-lifecycle-architecture)
- **Distributed key-value stores** — see [Distributed Key-Value Store (DynamoDB / Cassandra) Case Study](../system-design/distributed-key-value-store.md), [Chapter 3: Consistent Hashing](../system-design/fundamentals/03-caching-and-scaling.md#4-consistent-hashing), [Chapter 4: LSM-Tree vs. B+Tree](../system-design/fundamentals/04-databases-sql-vs-nosql.md#btree-vs-lsm-tree-why-sql-vs-nosql-engines-differ), and [Chapter 7: Gossip Protocol & Merkle Trees](../system-design/fundamentals/07-probabilistic-structures-and-stream-processing.md#2-decentralized-cluster-protocols--conflict-resolution)
- **Alert / monitoring system design** — see [Metrics Monitoring & Alerting Design](../system-design/metrics-monitoring.md), [Notification System Design](../system-design/notification-system.md), and [Chapter 6: SLAs/SLOs & Observability](../system-design/fundamentals/06-resiliency-observability-and-metrics.md#3-observability-metrics-logs--distributed-tracing)
- **WebSockets vs. long polling vs. short polling vs. SSE** — see [Chapter 1: Real-Time Protocols & Stateful WebSocket Scaling](../system-design/fundamentals/01-networking-and-api-paradigms.md#3-real-time-protocols-polling-vs-sse-vs-websockets-vs-webrtc), [WhatsApp Design](../system-design/whatsapp.md), and [ChatGPT & Enterprise RAG Copilot](../system-design/chatgpt-rag-copilot.md)
- **SQL basics: joins, aggregation, indexes & isolation levels** — see [Chapter 4: Relational (SQL) vs. NoSQL, B+Tree Indexes & Sharding](../system-design/fundamentals/04-databases-sql-vs-nosql.md)

## Networking

- **OSI 7-Layer Model & Transport Protocols (TCP/UDP/QUIC)** — see [Chapter 1: Networking & OSI 7-Layer Model](../system-design/fundamentals/01-networking-and-api-paradigms.md#1-networking--osi-7-layer-model) and [Wikipedia](https://en.wikipedia.org/wiki/OSI_model)
- **L4/L7 Load Balancing, VRRP Failover & BGP Anycast Routing** — see [Chapter 2: Proxies, Load Balancers, API Gateways & CDNs](../system-design/fundamentals/02-edge-load-balancing-and-cdn.md#high-availability-ha-for-load-balancers)
- [Open Shortest Path First (OSPF)](https://en.wikipedia.org/wiki/Open_Shortest_Path_First)
- [Link-state routing protocol](https://en.wikipedia.org/wiki/Link-state_routing_protocol)
- [Border Gateway Protocol (BGP)](https://en.wikipedia.org/wiki/Border_Gateway_Protocol)

## Algorithms Reference

| Topic | Reference | Implementation |
| --- | --- | --- |
| Manacher's algorithm | [Wikipedia](https://en.wikipedia.org/wiki/Longest_palindromic_substring#Manacher's_algorithm) | [5.cs](../src/leetcode/csharp/0001-0100/5.cs) |
| Miller–Rabin primality | [Wikipedia](https://en.wikipedia.org/wiki/Miller%E2%80%93Rabin_primality_test) | [millerRabin.cs](../src/algorithms/csharp/others/millerRabin.cs) |
| Knapsack problem | [Wikipedia](https://en.wikipedia.org/wiki/Knapsack_problem) | [knapsack.cs](../src/algorithms/csharp/others/knapsack.cs) |
| Bin packing problem | [Wikipedia](https://en.wikipedia.org/wiki/Bin_packing_problem) | [binPacking.cs](../src/algorithms/csharp/others/binPacking.cs) |
| Edit distance | [Wikipedia](https://en.wikipedia.org/wiki/Edit_distance) | [72.cs](../src/leetcode/csharp/0001-0100/72.cs) |
| Minimum spanning tree | [Wikipedia](https://en.wikipedia.org/wiki/Minimum_spanning_tree) | [minimumSpanningTree.cs](../src/algorithms/csharp/others/minimumSpanningTree.cs) |
| Bellman–Ford | [Wikipedia](https://en.wikipedia.org/wiki/Bellman%E2%80%93Ford_algorithm) | [bellmanFord.cs](../src/algorithms/csharp/search/bellmanFord.cs) |
| Knuth's Mastermind algorithm | [Math StackExchange](http://math.stackexchange.com/questions/1192961/knuths-mastermind-algorithm) | — |
| Benford's law | [Wikipedia](https://en.wikipedia.org/wiki/Benford%27s_law) | — |

## Interview Fundamentals Checklist

- **Arrays / Strings:** palindrome check, merge sorted arrays, in-place reverse, substring search, all sorting
  algorithms, binary search in rotated array, max-profit stock, matrix multiplication, find duplicates, spiral matrix
- **Linked Lists:** reverse, insert/delete, cycle detection + start node, merge sorted lists, split by even/odd index,
  clone list with random pointer, flatten / unflatten
- **Trees:** balanced check, recursive + iterative traversals, BFS/DFS, BST from sorted array, mirror check, max path
  sum, lowest common ancestor, vertical sum (binary and n-ary)
- **Backtracking:** permutations, combinations, subsets, N-Queens, phone keypad letter combinations, Sudoku
- **Hash tables:** counting occurrences, tuple keys
- **Dynamic programming:** stair climbing (1/2/3 steps), grid paths with obstacles — both top-down and bottom-up

## External Practice Links

- [Kth smallest element in a row/column-sorted matrix (GfG)](http://www.geeksforgeeks.org/kth-smallest-element-in-a-row-wise-and-column-wise-sorted-2d-array-set-1/)
- [Merge sort for linked list (GfG)](http://www.geeksforgeeks.org/merge-sort-for-linked-list/)
- [Array rotation (GfG)](http://www.geeksforgeeks.org/array-rotation/)
- [Merge an array of size n into one of size m+n (GfG)](http://www.geeksforgeeks.org/merge-one-array-of-size-n-into-another-one-of-size-mn/)
- [Longest common substring (ProgramCreek)](http://www.programcreek.com/2015/04/longest-common-substring-java/)
- [Divide two integers (ProgramCreek)](http://www.programcreek.com/2014/05/leetcode-divide-two-integers-java/)
- [Google Code Jam archive](https://codingcompetitions.withgoogle.com/codejam/archive)
