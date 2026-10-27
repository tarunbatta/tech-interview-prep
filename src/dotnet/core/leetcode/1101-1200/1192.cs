// Url: https://leetcode.com/problems/critical-connections-in-a-network/

/*
1192. Critical Connections in a Network
Hard

There are n servers numbered from 0 to n - 1 connected by undirected server-to-server connections forming a network
where connections[i] = [ai, bi] represents a connection between servers ai and bi. Any server can reach other servers
directly or indirectly through the network.

A critical connection is a connection that, if removed, will make some servers unable to reach some other server.

Return all critical connections in the network in any order.

Example 1:
Input: n = 4, connections = [[0,1],[1,2],[2,0],[1,3]]
Output: [[1,3]]
Explanation: [[3,1]] is also accepted.

Example 2:
Input: n = 2, connections = [[0,1]]
Output: [[0,1]]

Constraints:
- 2 <= n <= 10^5
- n - 1 <= connections.length <= 10^5
- 0 <= ai, bi <= n - 1
- ai != bi
- There are no repeated connections.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem1192 {
    public class Solution {
        public static void Init () {
            Print (CriticalConnections (4, [[0, 1], [1, 2], [2, 0], [1, 3]])); // [1,3]
            Print (CriticalConnections (2, [[0, 1]])); // [0,1]
            Print (CriticalConnections (6, [[0, 1], [1, 2], [2, 0], [1, 3], [3, 4], [4, 5], [5, 3]])); // [1,3]
        }

        // Tarjan's bridge-finding algorithm.
        // disc[u] = discovery time of u; low[u] = earliest discovery time reachable
        // from u's DFS subtree using at most one back edge.
        // Edge (u, v) is a bridge iff low[v] > disc[u].
        // Iterative DFS avoids stack overflow for n up to 10^5.
        // Time: O (V + E)
        // Space: O (V + E)
        public static IList<IList<int>> CriticalConnections (int n, IList<IList<int>> connections) {
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++) {
                graph[i] = new List<int> ();
            }

            foreach (IList<int> edge in connections) {
                graph[edge[0]].Add (edge[1]);
                graph[edge[1]].Add (edge[0]);
            }

            int[] disc = new int[n];
            int[] low = new int[n];
            int[] parent = new int[n];
            int[] nextChild = new int[n];
            Array.Fill (disc, -1);
            Array.Fill (parent, -1);

            List<IList<int>> result = new List<IList<int>> ();
            int time = 0;

            for (int root = 0; root < n; root++) {
                if (disc[root] != -1) {
                    continue;
                }

                Stack<int> stack = new Stack<int> ();
                stack.Push (root);
                disc[root] = low[root] = time++;

                while (stack.Count > 0) {
                    int u = stack.Peek ();

                    if (nextChild[u] < graph[u].Count) {
                        int v = graph[u][nextChild[u]++];

                        if (disc[v] == -1) {
                            parent[v] = u;
                            disc[v] = low[v] = time++;
                            stack.Push (v);
                        } else if (v != parent[u]) {
                            low[u] = Math.Min (low[u], disc[v]);
                        }
                    } else {
                        stack.Pop ();
                        int p = parent[u];

                        if (p != -1) {
                            low[p] = Math.Min (low[p], low[u]);

                            if (low[u] > disc[p]) {
                                result.Add (new List<int> { p, u });
                            }
                        }
                    }
                }
            }

            return result;
        }

        private static void Print (IList<IList<int>> edges) {
            List<string> parts = new List<string> ();
            foreach (IList<int> e in edges) {
                parts.Add ($"[{e[0]},{e[1]}]");
            }

            Console.WriteLine ($"[{string.Join (",", parts)}]");
        }
    }
}
