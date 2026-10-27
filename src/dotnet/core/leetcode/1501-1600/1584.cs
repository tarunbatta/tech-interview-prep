// Url: https://leetcode.com/problems/min-cost-to-connect-all-points/

/*
1584. Min Cost to Connect All Points
Medium

You are given an array points representing integer coordinates of some points on a 2D-plane, where points[i] = [xi, yi].

The cost of connecting two points [xi, yi] and [xj, yj] is the manhattan distance between them: |xi - xj| + |yi - yj|.

Return the minimum cost to make all points connected. All points are connected if there is exactly one simple path
between any two points.

Example 1:
Input: points = [[0,0],[2,2],[3,10],[5,2],[7,0]]
Output: 20

Example 2:
Input: points = [[3,12],[-2,5],[-4,1]]
Output: 18

Constraints:
- 1 <= points.length <= 1000
- -10^6 <= xi, yi <= 10^6
- All pairs (xi, yi) are distinct.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1584 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (MinCostConnectPoints ([[0, 0], [2, 2], [3, 10], [5, 2], [7, 0]])); // 20
            Console.WriteLine (MinCostConnectPoints ([[3, 12], [-2, 5], [-4, 1]])); // 18
            Console.WriteLine (MinCostConnectPoints ([[0, 0]])); // 0
        }

        // Prim's algorithm on the implicit complete graph.
        // Using an O(V^2) array-based Prim (no heap) is optimal for dense graphs,
        // since E = V^2 here and a heap would cost O(V^2 log V).
        // Time: O (n^2)
        // Space: O (n)
        public static int MinCostConnectPoints (int[][] points) {
            int n = points.Length;
            int[] minDist = new int[n];
            bool[] inTree = new bool[n];
            Array.Fill (minDist, int.MaxValue);
            minDist[0] = 0;

            int total = 0;

            for (int added = 0; added < n; added++) {
                int u = -1;
                for (int i = 0; i < n; i++) {
                    if (!inTree[i] && (u == -1 || minDist[i] < minDist[u])) {
                        u = i;
                    }
                }

                inTree[u] = true;
                total += minDist[u];

                for (int v = 0; v < n; v++) {
                    if (!inTree[v]) {
                        int d = Math.Abs (points[u][0] - points[v][0]) + Math.Abs (points[u][1] - points[v][1]);
                        if (d < minDist[v]) {
                            minDist[v] = d;
                        }
                    }
                }
            }

            return total;
        }
    }
}
