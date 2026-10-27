// Url: https://leetcode.com/problems/graph-valid-tree

/*
261. Graph Valid Tree
Medium

You have a graph of n nodes labeled from 0 to n - 1. You are given an integer n and a list of edges where edges[i] = [ai, bi] indicates that there is an undirected edge between nodes ai and bi in the graph.

Return true if the edges of the given graph make up a valid tree, and false otherwise.

Example 1:
Input: n = 5, edges = [[0,1],[0,2],[0,3],[1,4]]
Output: true

Example 2:
Input: n = 5, edges = [[0,1],[1,2],[2,3],[1,3],[1,4]]
Output: false

Constraints:
1 <= n <= 2000
0 <= edges.length <= 5000
edges[i].length == 2
0 <= ai, bi < n
ai != bi
There are no self-loops or repeated edges.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem261
{
    public class Solution
    {
        public static void Init()
        {
            int[][] edges1 =
            [
                [0, 1],
                [0, 2],
                [0, 3],
                [1, 4]
            ];
            Console.WriteLine(ValidTree(5, edges1)); // True

            int[][] edges2 =
            [
                [0, 1],
                [1, 2],
                [2, 3],
                [1, 3],
                [1, 4]
            ];
            Console.WriteLine(ValidTree(5, edges2)); // False
        }

        // Time: O(n * α(n)) where α is the inverse Ackermann function
        // Space: O(n) for the Union-Find disjoint set array
        public static bool ValidTree(int n, int[][] edges)
        {
            if (n <= 0)
            {
                return false;
            }

            // A tree with n nodes must have exactly n - 1 edges
            if (edges == null || edges.Length != n - 1)
            {
                return false;
            }

            int[] parent = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
            }

            foreach (var edge in edges)
            {
                int rootU = Find(parent, edge[0]);
                int rootV = Find(parent, edge[1]);

                if (rootU == rootV)
                {
                    // Cycle detected
                    return false;
                }

                parent[rootU] = rootV;
            }

            return true;
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