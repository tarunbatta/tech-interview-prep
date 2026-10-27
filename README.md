# Interview Preperation Guide

A multi-language monorepo containing comprehensive interview preparation resources, data structures, algorithms, problem solutions (LeetCode, Cracking the Coding Interview, HackerRank), and mini-applications.

## Monorepo Structure

* **`apps/`**: Standalone executable applications
  * `chess-game/`: Interactive console Chess game (.NET 10)
  * `expression-tree/`: Expression tree evaluator (.NET 10)
* **`src/`**: Core libraries and problem solutions organized by ecosystem
  * `dotnet/`
    * `core/`: Core algorithms, data structures, LeetCode, and C# CTCI solutions (.NET 10)
    * `practice/`: Practice questions (1–54) and Question of the Day (QOTD) problems (.NET 10)
  * `javascript/`: JavaScript problem solutions (QOTD scripts)
  * `java/`: Cracking the Coding Interview (6th edition) Java solutions and notes
* **`docs/`**: Curated interview preparation notes and tracking
  * `docs/interview-questions.md`: Real interview questions by company
  * `docs/top100-leetcode.md`: Curated top 100 LeetCode list
  * `docs/todo.md`: Study roadmap and problem tracker
* **`shared/assets/`**: Shared dictionary files (`words.txt`, `words_scowl.txt`)

## Running & Building

### .NET Projects (.NET 10)

```bash
# Build .NET projects
dotnet build src/dotnet/core
dotnet build src/dotnet/practice
dotnet build apps/chess-game
dotnet build apps/expression-tree

# Run Core entry point
dotnet run --project src/dotnet/core

# Run Chess Game
dotnet run --project apps/chess-game

# Run Expression Tree
dotnet run --project apps/expression-tree
```

### JavaScript

```bash
cd src/javascript
node qotd/SuperArray.js
```

---

## Snippet for Algo

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
},
```

---

## Big O Cheat Sheet

### Common Data Structure Operations

| Data Structure     | Time Complexity - Avg - Access | Time Complexity - Avg - Search | Time Complexity - Avg - Insertion | Time Complexity - Average - Deletion | Time Complexity - Worst - Access | Time Complexity - Worst - Search | Time Complexity - Worst - Insertion | Time Complexity - Worst - Deletion | Space Complexity Worst |
| ------------------ | ------------------------------ | ------------------------------ | --------------------------------- | ------------------------------------ | -------------------------------- | -------------------------------- | ----------------------------------- | ---------------------------------- | ---------------------- |
| Array              | Θ(1)                           | Θ(n)                           | Θ(n)                              | Θ(n)                                 | O(1)                             | O(n)                             | O(n)                                | O(n)                               | O(n)                   |
| Stack              | Θ(n)                           | Θ(n)                           | Θ(1)                              | Θ(1)                                 | O(n)                             | O(n)                             | O(1)                                | O(1)                               | O(n)                   |
| Queue              | Θ(n)                           | Θ(n)                           | Θ(1)                              | Θ(1)                                 | O(n)                             | O(n)                             | O(1)                                | O(1)                               | O(n)                   |
| Singly-Linked List | Θ(n)                           | Θ(n)                           | Θ(1)                              | Θ(1)                                 | O(n)                             | O(n)                             | O(1)                                | O(1)                               | O(n)                   |
| Doubly-Linked List | Θ(n)                           | Θ(n)                           | Θ(1)                              | Θ(1)                                 | O(n)                             | O(n)                             | O(1)                                | O(1)                               | O(n)                   |
| Skip List          | Θ(log(n))                      | Θ(log(n))                      | Θ(log(n))                         | Θ(log(n))                            | O(n)                             | O(n)                             | O(n)                                | O(n)                               | O(n log(n))            |
| Hash Table         | N/A                            | Θ(1)                           | Θ(1)                              | Θ(1)                                 | N/A                              | O(n)                             | O(n)                                | O(n)                               | O(n)                   |
| Binary Search Tree | Θ(log(n))                      | Θ(log(n))                      | Θ(log(n))                         | Θ(log(n))                            | O(n)                             | O(n)                             | O(n)                                | O(n)                               | O(n)                   |
| Cartesian Tree     | N/A                            | Θ(log(n))                      | Θ(log(n))                         | Θ(log(n))                            | N/A                              | O(n)                             | O(n)                                | O(n)                               | O(n)                   |
| B-Tree             | Θ(log(n))                      | Θ(log(n))                      | Θ(log(n))                         | Θ(log(n))                            | O(log(n))                        | O(log(n))                        | O(log(n))                           | O(log(n))                          | O(n)                   |
| Red-Black Tree     | Θ(log(n))                      | Θ(log(n))                      | Θ(log(n))                         | Θ(log(n))                            | O(log(n))                        | O(log(n))                        | O(log(n))                           | O(log(n))                          | O(n)                   |
| Splay Tree         | N/A                            | Θ(log(n))                      | Θ(log(n))                         | Θ(log(n))                            | N/A                              | O(log(n))                        | O(log(n))                           | O(log(n))                          | O(n)                   |
| AVL Tree           | Θ(log(n))                      | Θ(log(n))                      | Θ(log(n))                         | Θ(log(n))                            | O(log(n))                        | O(log(n))                        | O(log(n))                           | O(log(n))                          | O(n)                   |
| KD Tree            | Θ(log(n))                      | Θ(log(n))                      | Θ(log(n))                         | Θ(log(n))                            | O(n)                             | O(n)                             | O(n)                                | O(n)                               | O(n)                   |

### Array Sorting Algorithms

| Algorithm      | Best Time Complexity | Avg Time Complexity | Worst Time Complexity | Worst Space Complexity |
| -------------- | -------------------- | ------------------- | --------------------- | ---------------------- |
| Quicksort      | Ω(n log(n))          | Θ(n log(n))         | O(n^2)                | O(log(n))              |
| Mergesort      | Ω(n log(n))          | Θ(n log(n))         | O(n log(n))           | O(n)                   |
| Timsort        | Ω(n)                 | Θ(n log(n))         | O(n log(n))           | O(n)                   |
| Heapsort       | Ω(n log(n))          | Θ(n log(n))         | O(n log(n))           | O(1)                   |
| Bubble Sort    | Ω(n)                 | Θ(n^2)              | O(n^2)                | O(1)                   |
| Insertion Sort | Ω(n)                 | Θ(n^2)              | O(n^2)                | O(1)                   |
| Selection Sort | Ω(n^2)               | Θ(n^2)              | O(n^2)                | O(1)                   |
| Tree Sort      | Ω(n log(n))          | Θ(n log(n))         | O(n^2)                | O(n)                   |
| Shell Sort     | Ω(n log(n))          | Θ(n(log(n))^2)      | O(n(log(n))^2)        | O(1)                   |
| Bucket Sort    | Ω(n+k)               | Θ(n+k)              | O(n^2)                | O(n)                   |
| Radix Sort     | Ω(nk)                | Θ(nk)               | O(nk)                 | O(n+k)                 |
| Counting Sort  | Ω(n+k)               | Θ(n+k)              | O(n+k)                | O(k)                   |
| Cubesort       | Ω(n)                 | Θ(n log(n))         | O(n log(n))           | O(n)                   |

---

## Data Structures

### Collection

1. Linear: ordered by position
    1. Direct Access: elements accessed by index
        1. String
        1. Array
        1. Struct
    1. Sequential Access: elements stored in sequential order
        1. Stack
        1. Queue
        1. Generalized Indexed Collections
            1. Hash Table
            1. Dictionary
1. Non-linear
    1. Hierarchical
        1. Tree
        1. Heap
    1. Grouped
        1. Set
        1. Graph
        1. Networks

### Integer

* Int32 is implemented as structure

### String

* are immutable, means once initialized, it can’t be changed
* on modification a copy of the string is created

### StringBuilder

* Are mutable

### Array

* indexed collection of data
* data could be either build-in type or user defined type
* Multi dimensional Array
  * has the same number of elements in each row

  ```csharp
  int sales[,] = new int[12,30];
  ```

* Jagged Array
  * each row is an array of arrays
  * could have different number of elements in each row.

  ```csharp
  int days[][] = new int[12][];
  ```

### ArrayList

* used where size of array is unknown
* automatically resizes when array is out of storage space
* initial capacity is 16 and it increases by 16 when out of storage space
* has build-in enumerator that iterates through all the objects in arraylist

### Struct

* A composite data type that holds data that may consist of different data types
* Can’t be inherited or derived

### Stack

* LIFO: last in first out
* Items can only be removed from the Top
* Operations
  * Push: adding an item
  * Pop: removing an item

### Queue

* FIFO: first in first out
* Items are added to Rear and removed from Front
* Operations
  * Enqueue: adding an item
  * Dequeue: removing an item
* Uses
  * System programming for scheduling operating system tasks
  * Simulation studies
* Priority Queue : special queue where the item is removed based on the highest priority

### Hash Table

* Stores set of data values associated with keys
* a special function, called a hash function, takes one data value and transforms the value (called the key) into an integer index that is used to retrieve the data. The index is then used to access the data record associated with the key.

### Dictionary

* consists of series of key-value pairs, called associations
* also called associative arrays
* index doesn't have to be integer

### Linked List

* can be used in almost every situation where an array is used, except if you need random access to the items in the list, when an array is probably the best choice.

### Tree

* one element is root and it has other child elements below it as leaves
* binary tree
  * each node as no more than 2 children
* binary search tree
  * is a binary tree where data with lesser values are stored in left nodes and values with greater values are stored in right nodes
  * also called ordered / sorted binary tree
  * items are sorted in a way to enable fast lookup
* Uses
  * file system in operating systems

### Heap

* tree where smallest data value is always placed in root
* deletion of root causes tree to reorder
* Uses
  * Heap Sort

## Set

* collection of unordered data values where each value is unique
* operations like union and intersection ca be performed on sets

### Graph

* set of nodes and set of edges that connect the nodes
* Uses
  * logistics and job scheduling

### Network

* type of graph where edges carry a weight (acts as cost during traversal)

---

## Sorting

### Bubble Sort

* Time Complexity: O(n^2)
* Space Complexity: O(1)
* comparing elements and swapping them if the value to the left is greater than the value to the right

### Selection Sort

* Time Complexity: O(n^2)
* Space Complexity: O(1)
* starts at beginning of the array, considering the first element as minimum
* compares first element with every other element in the array
* swaps elements if a minimum was found

### Insertion Sort

* Time Complexity: O(n^2)
* Space Complexity: O(1)
* The outer loop moves element by element through the array whereas the inner loop compares the element chosen in the outer loop to the element next to it in the array. If the element selected by the outer loop is less than the element selected by the inner loop, array elements are shifted over to the right to make room for the inner loop element

### Merge Sort

* Time Complexity: O(n log(n))
* Space Complexity: O(n)

### Quick Sort

* Time Complexity: O(n log(n))
* Space Complexity: O(log(n))

---

## Search

### Sequential Search

* searching for an item sequentially and looping on all items

### Binary Search

* items are already sorted in order

---

## BitWise

### OR

| 0   | 0   | 1   |
| --- | --- | --- |
| 0   | 1   | 1   |
| 1   | 0   | 1   |
| 1   | 1   | 1   |

### And

| 0   | 1   | 0   |
| --- | --- | --- |
| 0   | 0   | 0   |
| 1   | 0   | 0   |
| 1   | 1   | 1   |

### XOR

| 0   | 0   | 0   |
| --- | --- | --- |
| 0   | 1   | 1   |
| 1   | 0   | 1   |
| 1   | 1   | 0   |

```csharp
public static string ConvertIntToBits (int val, int bitSize) {
    int mask = 1 << (bitSize - 1);
    StringBuilder bitBuffer = new StringBuilder (35);

    for (int i = 1; i <= bitSize; i++) {
        if ((val & mask) == 0) {
            bitBuffer.Append ("0");
        } else {
            bitBuffer.Append ("1");
        }

        val <<= 1;

        if ((i % 8) == 0) {
            bitBuffer.Append (" ");
        }
    }

    return bitBuffer.ToString ();
}
```

---

## Dynamic Programming

It has the following properties,

1. Overlapping Subproblems
1. Optimal Substructure

It can be solved by the following approaches,

1. Top-down approach
1. Bottom-up approach

## Regular Expressions

### Quantifiers

* \+ : match one or more of the immediately preceding character
* \* : match zero or more of the immediately preceding character
* {n} : n is the number of matches to find
* {n, m} : n is the minimum number of matches and m is the maximum number of matches to find
* . : matches any character in a string

### Character Classes

* [] : groups the character that needs to be matched
  * [a-z] : matches lower case characters
  * [a-zA-Z] : matches both lower and upper case characters
  * [0-9] : matches all numbers
  * [^aeiou] : matches all non-vowels
* ^ : reverse or negation of followed characters
* \w : similar to [A-Za-z0-9]
* \W : negation of \w
* \d : similar to [0-9]
* \D : similar to [^0-9]
* \s : white space character
* \S : non-white space character

### Assertions

* ^ : matches only at beginning of string
* $ : matches only at end of string
* \b : match can only occur at the beginning or end of a word that is separated by spaces

---

## System Design

### Load Banalcer

#### LB Levels

* A layer 3 load-balancer takes routing decisions based on IP addressing alone (source & destination).

* A layer 4 load-balancer takes routing decision based on IPs and TCP or UDP ports. It has a packet view of the traffic exchanged between the client and a server which means it takes decisions packet by packet. This allows you to redirect traffic based on the ports in use (so port 80/443 for http/https, port 5060 for SIP etc) and so you can have multiple pools of VoiP and web servers and load balance specific flows across the pools. However a layer 4 load balancer can not see the content of the data being passed through it or make routing decisions based on the content itself.

* Layer 7 load balancer operates at the high-level application layer, which deals with the actual content of each message. HTTP is the predominant Layer 7 protocol for website traffic on the Internet. Layer 7 load balancers route network traffic in a much more sophisticated way than Layer 4 load balancers, particularly applicable to TCP-based traffic such as HTTP. A Layer 7 load balancer terminates the network traffic and reads the message within. It can make a load-balancing decision based on the content of the message (the URL or cookie, for example). It then makes a new TCP connection to the selected upstream server (or reuses an existing one, by means of HTTP Keepalives) and writes the request to the server.

##### Benefits of Layer 7 Load Balancing

Layer 7 load balancing is more CPU-intensive than packet-based Layer 4 load balancing, but rarely causes degraded performance on a modern server. Layer 7 load balancing enables the load balancer to make smarter load-balancing decisions, and to apply optimizations and changes to the content (such as compression and encryption). It uses buffering to offload slow connections from the upstream servers, which improves performance.

So in answer to your question, Layer 7 load balancers offer loads of benefits to network managers around how data is routed and managed. Since they can see the data within the network traffic, L7 load balancers can make routing decisions based on the application traffic status itself (eg http time-outs on one web server can trigger new requests to be delivered to another), and there is much more granular control.

Are Layer 7 Load balancers needed all the time? No not at all. Layer 4 Load balancing alone can be a really successful, but if you can justify them commercially and operationally, then L7 Load Balancers add a very powerful capability that can increase service availability and dramatically improve user experiences of those services.

---

## Difference between Class and Struct

### Class

* Class is a reference type and its object is created on the heap memory.
* Class can inherit the another class.
* Class can have the all types of constructor and destructor.
* The member variable of class can be initialized directly.
* Class object can not be created without using the new keyword, it means we have to use it.

```csharp
Demo obj=new Demo();
```

### Structure

* Structure is a value type that is why its object is created on the stack memory.
* Structure does not support the inheritance.
* Structure can only have the parametrized constructor. it means a structure can not have the non-parametrized constructor,default constructor and destructor also.
* The member variable of structure can not be initialized directly.
* Structure object can be created without using the new keyword.(optional)

```csharp
Demo obj;
```

---
