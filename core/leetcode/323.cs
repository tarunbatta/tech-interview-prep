// Url: https://leetcode.com/problems/number-of-connected-components-in-an-undirected-graph

/*
323. Number of Connected Components in an Undirected Graph
Medium

You have a graph of n nodes. You are given an integer n and an array edges where edges[i] = [ai, bi] indicates that there is an edge between ai and bi in the graph.

Return the number of connected components in the graph.

Example 1:
Input: n = 5, edges = [[0,1],[1,2],[3,4]]
Output: 2

Example 2:
Input: n = 5, edges = [[0,1],[1,2],[2,3],[3,4]]
Output: 1

Constraints:
1 <= n <= 2000
1 <= edges.length <= 5000
edges[i].length == 2
0 <= ai <= bi < n
ai != bi
There are no repeated edges.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem323
{
    public class Solution
    {
        public static void Init()
        {
            int[][] edges1 =
            [
                [0, 1],
                [1, 2],
                [3, 4]
            ];
            Console.WriteLine(CountComponents(5, edges1)); // 2

            int[][] edges2 =
            [
                [0, 1],
                [1, 2],
                [2, 3],
                [3, 4]
            ];
            Console.WriteLine(CountComponents(5, edges2)); // 1
        }

        // Time: O(n + e * α(n)) where e is edges.Length and α is inverse Ackermann function
        // Space: O(n) for Union-Find parent array
        public static int CountComponents(int n, int[][] edges)
        {
            if (n <= 0)
            {
                return 0;
            }

            int[] parent = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
            }

            int count = n;

            if (edges != null)
            {
                foreach (var edge in edges)
                {
                    int rootU = Find(parent, edge[0]);
                    int rootV = Find(parent, edge[1]);

                    if (rootU != rootV)
                    {
                        parent[rootU] = rootV;
                        count--;
                    }
                }
            }

            return count;
        }

        private static int Find(int[] parent, int i)
        {
            if (parent[i] != i)
            {
                parent[i] = Find(parent, parent[i]);
            }

            return parent[i];
        }
    }
}