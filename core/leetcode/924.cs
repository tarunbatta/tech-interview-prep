// Url:https://leetcode.com/problems/minimize-malware-spread

/*
924. Minimize Malware Spread
Hard

You are given a network of n nodes represented by an n x n adjacency matrix graph, where the ith node is directly connected to the jth node if graph[i][j] == 1.

Some nodes initial are initially infected by malware. Whenever two nodes are directly connected, and at least one node was infected by malware, both nodes will be infected by malware. This spread of malware will continue until no more nodes can be infected in this manner.

Suppose M(initial) is the final number of nodes infected with malware in the entire network after the spread of malware stops. We will remove exactly one node from initial.

Return the node that, if removed, would minimize M(initial). If multiple nodes could be removed to minimize M(initial), return such a node with the smallest index.

Note that if a node was removed from the initial list of infected nodes, it might still be infected later as a result of the malware spread.

Example 1:
Input: graph = [[1,1,0],[1,1,0],[0,0,1]], initial = [0,1]
Output: 0

Example 2:
Input: graph = [[1,0,0],[0,1,0],[0,0,1]], initial = [0,2]
Output: 0

Example 3:
Input: graph = [[1,1,1],[1,1,1],[1,1,1]], initial = [1,2]
Output: 1

Constraints:
- n == graph.length
- n == graph[i].length
- 2 <= n <= 300
- graph[i][j] is 0 or 1.
- graph[i][j] == graph[j][i]
- graph[i][i] == 1.
- 1 <= initial.length <= n
- 0 <= initial[i] <= n - 1
- All the integers in initial are unique.
*/

using System;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem924 {
    public class Solution {
        public static void Init() {
            int[][] g1 = [
                [1, 1, 0],
                [1, 1, 0],
                [0, 0, 1]
            ];
            int[] init1 = [0, 1];
            Console.WriteLine(MinMalwareSpread(g1, init1)); // 0

            int[][] g2 = [
                [1, 0, 0],
                [0, 1, 0],
                [0, 0, 1]
            ];
            int[] init2 = [0, 2];
            Console.WriteLine(MinMalwareSpread(g2, init2)); // 0

            int[][] g3 = [
                [1, 1, 1],
                [1, 1, 1],
                [1, 1, 1]
            ];
            int[] init3 = [1, 2];
            Console.WriteLine(MinMalwareSpread(g3, init3)); // 1
        }

        // Time: O(n^2) where n is the number of nodes
        // Space: O(n) for DSU structures
        public static int MinMalwareSpread(int[][] graph, int[] initial) {
            int n = graph.Length;
            int[] parent = new int[n];
            int[] size = new int[n];

            for (int i = 0; i < n; i++) {
                parent[i] = i;
                size[i] = 1;
            }

            int Find(int i) {
                if (parent[i] != i) {
                    parent[i] = Find(parent[i]);
                }
                return parent[i];
            }

            void Union(int i, int j) {
                int rootI = Find(i);
                int rootJ = Find(j);
                if (rootI != rootJ) {
                    parent[rootI] = rootJ;
                    size[rootJ] += size[rootI];
                }
            }

            for (int i = 0; i < n; i++) {
                for (int j = i + 1; j < n; j++) {
                    if (graph[i][j] == 1) {
                        Union(i, j);
                    }
                }
            }

            int[] malwareCount = new int[n];
            foreach (int node in initial) {
                malwareCount[Find(node)]++;
            }

            Array.Sort(initial);

            int maxSaved = -1;
            int ans = initial[0];

            foreach (int node in initial) {
                int root = Find(node);
                if (malwareCount[root] == 1) {
                    if (size[root] > maxSaved) {
                        maxSaved = size[root];
                        ans = node;
                    }
                }
            }

            return ans;
        }
    }
}