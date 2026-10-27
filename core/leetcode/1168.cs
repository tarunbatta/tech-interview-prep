namespace InterviewPreperationGuide.Core.LeetCode.problem1168
{
    using System;
    using System.Collections.Generic;

    // 1168. Optimize Water Distribution in a Village
    // https://leetcode.com/problems/optimize-water-distribution-in-a-village/
    //
    // There are n houses in a village. We want to supply water for all the houses by building wells and laying pipes.
    //
    // For each house i (1-indexed from 1 to n), we can either build a well directly in that house with cost wells[i - 1],
    // or pipe in water from another house to house i. The costs to lay pipes between houses are given by the array pipes,
    // where each pipes[j] = [house1_j, house2_j, cost_j] represents the cost to connect house1_j and house2_j together.
    // Connections are bidirectional.
    //
    // Return the minimum cost to supply water to all houses.
    //
    // Example 1:
    // Input: n = 3, wells = [1,2,2], pipes = [[1,2,1],[2,3,1]]
    // Output: 3
    // Explanation: 
    // The best strategy is to build a well in the first house with cost 1 and connect the remaining houses to house 1
    // through pipes with costs 1 + 1 = 2.
    // Total cost = 1 + 1 + 1 = 3.
    //
    // Constraints:
    // 2 <= n <= 10^4
    // wells.length == n
    // 0 <= wells[i] <= 10^5
    // 1 <= pipes.length <= 10^4
    // pipes[j].length == 3
    // 1 <= house1_j, house2_j <= n
    // 0 <= cost_j <= 10^5
    // house1_j != house2_j
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(MinCostToSupplyWater(3, [1, 2, 2],
            [
                [1, 2, 1],
                [2, 3, 1]
            ])); // 3

            Console.WriteLine(MinCostToSupplyWater(2, [1, 1],
            [
                [1, 2, 1]
            ])); // 2
        }

        // Time: O((V + E) log(V + E)) where V = n + 1, E = pipes.length + n (Kruskal's MST)
        // Space: O(V + E) for edges list and Union-Find disjoint set
        public static int MinCostToSupplyWater(int n, int[] wells, int[][] pipes)
        {
            var edges = new List<Edge>();

            // Virtual node 0 represents the main water source.
            // Edge from 0 to house i has cost equal to building a well at house i.
            for (int i = 0; i < wells.Length; i++)
            {
                edges.Add(new Edge(0, i + 1, wells[i]));
            }

            foreach (var pipe in pipes)
            {
                edges.Add(new Edge(pipe[0], pipe[1], pipe[2]));
            }

            // Sort edges by cost ascending
            edges.Sort((a, b) => a.Cost.CompareTo(b.Cost));

            var uf = new UnionFind(n + 1);
            int totalCost = 0;
            int edgesCount = 0;

            foreach (var edge in edges)
            {
                if (uf.Union(edge.U, edge.V))
                {
                    totalCost += edge.Cost;
                    edgesCount++;
                    if (edgesCount == n)
                    {
                        break;
                    }
                }
            }

            return totalCost;
        }

        private class Edge
        {
            public int U { get; }
            public int V { get; }
            public int Cost { get; }

            public Edge(int u, int v, int cost)
            {
                U = u;
                V = v;
                Cost = cost;
            }
        }

        private class UnionFind
        {
            private readonly int[] parent;
            private readonly int[] rank;

            public UnionFind(int n)
            {
                parent = new int[n];
                rank = new int[n];
                for (int i = 0; i < n; i++)
                {
                    parent[i] = i;
                }
            }

            public int Find(int x)
            {
                if (parent[x] != x)
                {
                    parent[x] = Find(parent[x]);
                }

                return parent[x];
            }

            public bool Union(int x, int y)
            {
                int rootX = Find(x);
                int rootY = Find(y);

                if (rootX == rootY)
                {
                    return false;
                }

                if (rank[rootX] < rank[rootY])
                {
                    parent[rootX] = rootY;
                }
                else if (rank[rootX] > rank[rootY])
                {
                    parent[rootY] = rootX;
                }
                else
                {
                    parent[rootY] = rootX;
                    rank[rootX]++;
                }

                return true;
            }
        }
    }
}