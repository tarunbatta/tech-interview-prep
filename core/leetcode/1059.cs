// Url:https://leetcode.com/problems/all-paths-from-source-lead-to-destination

/*
1059. All Paths from Source Lead to Destination
Medium

Given the edges of a directed graph where edges[i] = [ai, bi] indicates there is a directed edge from node ai to node bi, and two nodes source and destination of this graph, determine whether or not all paths starting from source eventually end at destination, that is:
- At least one path exists from the source node to the destination node
- If a path exists from the source node to a node with no outgoing edges, then that node is equal to destination.
- The number of possible paths from source to destination is a finite number.

Return true if and only if all paths from source lead to destination.

Example 1:
Input: n = 3, edges = [[0,1],[0,2]], source = 0, destination = 2
Output: false
Explanation: It is possible to reach and get stuck on both node 1 and node 2.

Example 2:
Input: n = 4, edges = [[0,1],[0,3],[1,2],[2,1]], source = 0, destination = 3
Output: false
Explanation: We have two paths: 0 -> 3 and 0 -> 1 -> 2 -> 1 -> 2 -> ... (infinite cycle).

Example 3:
Input: n = 4, edges = [[0,1],[0,2],[1,3],[2,3]], source = 0, destination = 3
Output: true

Constraints:
- 1 <= n <= 10^4
- 0 <= edges.length <= 10^4
- edges[i].length == 2
- 0 <= ai, bi <= n - 1
- 0 <= source, destination <= n - 1
- The given graph may have self-loops and parallel edges.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem1059 {
    public class Solution {
        public static void Init() {
            int[][] edges1 = [[0, 1], [0, 2]];
            Console.WriteLine(LeadsToDestination(3, edges1, 0, 2)); // False

            int[][] edges2 = [[0, 1], [0, 3], [1, 2], [2, 1]];
            Console.WriteLine(LeadsToDestination(4, edges2, 0, 3)); // False

            int[][] edges3 = [[0, 1], [0, 2], [1, 3], [2, 3]];
            Console.WriteLine(LeadsToDestination(4, edges3, 0, 3)); // True
        }

        // Time: O(V + E) where V is the number of nodes n and E is the number of edges
        // Space: O(V + E) to store adjacency list and recursion stack
        public static bool LeadsToDestination(int n, int[][] edges, int source, int destination) {
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++) {
                graph[i] = [];
            }

            foreach (var edge in edges) {
                graph[edge[0]].Add(edge[1]);
            }

            if (graph[destination].Count > 0) {
                return false;
            }

            int[] states = new int[n]; // 0: unvisited, 1: visiting, 2: visited
            return Dfs(graph, source, destination, states);
        }

        private static bool Dfs(List<int>[] graph, int node, int destination, int[] states) {
            if (states[node] != 0) {
                return states[node] == 2;
            }

            if (graph[node].Count == 0) {
                return node == destination;
            }

            states[node] = 1;

            foreach (int neighbor in graph[node]) {
                if (!Dfs(graph, neighbor, destination, states)) {
                    return false;
                }
            }

            states[node] = 2;
            return true;
        }
    }
}