# Study Topics & References

Non-code notes carried over from the old `todo.md`. The coding problems from that list now live in
[`src/dotnet/core`](../src/dotnet/core) (see [`amazon/`](../src/dotnet/core/amazon), [`concurrency/`](../src/dotnet/core/concurrency),
[`algorithms/others/`](../src/dotnet/core/algorithms/others) and [`leetcode/`](../src/dotnet/core/leetcode)).

## Concurrency & Distributed Systems

Common interview discussion topics (eBay and others):

- Concurrency fundamentals: threads vs. processes, locks, condition variables, deadlock, starvation
- Read-write locks — see [readWriteLock.cs](../src/dotnet/core/concurrency/readWriteLock.cs)
- Blocking concurrent queue / producer-consumer — see [blockingQueue.cs](../src/dotnet/core/concurrency/blockingQueue.cs)
- Sliding window protocol (flow control over an unreliable link)
- Two-phase commit (2PC) and its failure modes
- Pub/sub messaging
- Stateless services and 3-tier architecture
- Distributed key-value stores
- Alert / monitoring system design — see also [notification-system](../system-design/notification-system)
- WebSockets vs. long polling vs. short polling
- SQL basics: joins, aggregation, indexes

## Networking

- [OSI model](https://en.wikipedia.org/wiki/OSI_model)
- [Open Shortest Path First (OSPF)](https://en.wikipedia.org/wiki/Open_Shortest_Path_First)
- [Link-state routing protocol](https://en.wikipedia.org/wiki/Link-state_routing_protocol)
- [Border Gateway Protocol (BGP)](https://en.wikipedia.org/wiki/Border_Gateway_Protocol)

## Algorithms Reference

| Topic | Reference | Implementation |
| --- | --- | --- |
| Manacher's algorithm | [Wikipedia](https://en.wikipedia.org/wiki/Longest_palindromic_substring#Manacher's_algorithm) | [5.cs](../src/dotnet/core/leetcode/0001-0100/5.cs) |
| Miller–Rabin primality | [Wikipedia](https://en.wikipedia.org/wiki/Miller%E2%80%93Rabin_primality_test) | [millerRabin.cs](../src/dotnet/core/algorithms/others/millerRabin.cs) |
| Knapsack problem | [Wikipedia](https://en.wikipedia.org/wiki/Knapsack_problem) | [knapsack.cs](../src/dotnet/core/algorithms/others/knapsack.cs) |
| Bin packing problem | [Wikipedia](https://en.wikipedia.org/wiki/Bin_packing_problem) | [binPacking.cs](../src/dotnet/core/algorithms/others/binPacking.cs) |
| Edit distance | [Wikipedia](https://en.wikipedia.org/wiki/Edit_distance) | [72.cs](../src/dotnet/core/leetcode/0001-0100/72.cs) |
| Minimum spanning tree | [Wikipedia](https://en.wikipedia.org/wiki/Minimum_spanning_tree) | [minimumSpanningTree.cs](../src/dotnet/core/algorithms/others/minimumSpanningTree.cs) |
| Bellman–Ford | [Wikipedia](https://en.wikipedia.org/wiki/Bellman%E2%80%93Ford_algorithm) | [bellmanFord.cs](../src/dotnet/core/algorithms/search/bellmanFord.cs) |
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
