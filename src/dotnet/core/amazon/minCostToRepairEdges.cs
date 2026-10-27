/*
Amazon OA: Min Cost to Repair Edges (MST) / Min Cost to Connect All Nodes (Min Cost to Add New Roads)

Both are Kruskal's MST with some edges pre-selected at zero cost.

Min Cost to Repair Edges:
There's an undirected connected graph with n nodes labeled 1..n. Some edges are broken.
Given edges (all edges), and edgesToRepair where edgesToRepair[i] = [u, v, cost] is a broken edge,
find the minimum total cost to repair edges so that the whole graph is connected again.

Example:
n = 5, edges = [[1, 2], [2, 3], [3, 4], [4, 5], [1, 5]], edgesToRepair = [[1, 2, 12], [3, 4, 30], [1, 5, 8]]
Output: 20  (repair [1,2] and [1,5])

Min Cost to Connect All Nodes:
Given n nodes, a list of existing edges, and a list of new roads with costs, find the minimum cost to add new
roads so that all nodes are connected. Return -1 if impossible.

Example:
n = 6, edges = [[1, 4], [4, 5], [2, 3]], newEdges = [[1, 2, 5], [1, 3, 10], [1, 6, 2], [5, 6, 5]]
Output: 7  (add [1,2] and [1,6])
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.Amazon {
    public class MinCostToRepairEdges {
        public static void Init () {
            Console.WriteLine (RepairEdges (5,
                [[1, 2], [2, 3], [3, 4], [4, 5], [1, 5]],
                [[1, 2, 12], [3, 4, 30], [1, 5, 8]])); // 20

            Console.WriteLine (RepairEdges (6,
                [[1, 2], [2, 3], [4, 5], [3, 5], [1, 6], [2, 4]],
                [[1, 6, 410], [2, 4, 800]])); // 410

            Console.WriteLine (ConnectAllNodes (6,
                [[1, 4], [4, 5], [2, 3]],
                [[1, 2, 5], [1, 3, 10], [1, 6, 2], [5, 6, 5]])); // 7

            Console.WriteLine (ConnectAllNodes (4, [[1, 2]], [[3, 4, 1]])); // -1
        }

        // Unbroken edges cost 0; broken edges cost their repair price. Run Kruskal.
        // Time: O (E log E)
        // Space: O (n + E)
        public static int RepairEdges (int n, int[][] edges, int[][] edgesToRepair) {
            HashSet<(int, int)> broken = new HashSet<(int, int)> ();
            foreach (int[] e in edgesToRepair) {
                broken.Add (Key (e[0], e[1]));
            }

            List<int[]> all = new List<int[]> (edgesToRepair);
            foreach (int[] e in edges) {
                if (!broken.Contains (Key (e[0], e[1]))) {
                    all.Add ([e[0], e[1], 0]);
                }
            }

            return Kruskal (n, all);
        }

        // Existing edges cost 0; new edges cost their build price. Run Kruskal.
        // Time: O (E log E)
        // Space: O (n + E)
        public static int ConnectAllNodes (int n, int[][] edges, int[][] newEdges) {
            List<int[]> all = new List<int[]> (newEdges);
            foreach (int[] e in edges) {
                all.Add ([e[0], e[1], 0]);
            }

            return Kruskal (n, all);
        }

        private static int Kruskal (int n, List<int[]> edges) {
            int[] parent = Enumerable.Range (0, n + 1).ToArray ();
            int components = n, cost = 0;

            foreach (int[] e in edges.OrderBy (e => e[2])) {
                int a = Find (parent, e[0]), b = Find (parent, e[1]);
                if (a != b) {
                    parent[a] = b;
                    cost += e[2];
                    components--;
                }
            }

            return components == 1 ? cost : -1;
        }

        private static int Find (int[] parent, int x) {
            while (parent[x] != x) {
                parent[x] = parent[parent[x]]; // path halving
                x = parent[x];
            }
            return x;
        }

        private static (int, int) Key (int u, int v) => u < v ? (u, v) : (v, u);
    }
}
