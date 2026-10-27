// Url:https://leetcode.com/problems/connecting-cities-with-minimum-cost

/*
1135. Connecting Cities With Minimum Cost
Medium

There are n cities labeled from 1 to n. You are given the integer n and an array connections where connections[i] = [xi, yi, costi] indicates that the cost of connecting city xi and city yi (bidirectional network) is costi.

Return the minimum cost to connect all the n cities such that there is at least one path between each pair of cities. If it is impossible to connect all the n cities, return -1.

The cost is the sum of the connections' costs used.

Example 1:
Input: n = 3, connections = [[1,2,5],[1,3,6],[2,3,1]]
Output: 6
Explanation: Choosing any 2 edges will connect all cities so we choose the minimum 2.

Example 2:
Input: n = 4, connections = [[1,2,3],[3,4,4]]
Output: -1
Explanation: There is no way to connect all cities even if all edges are used.

Constraints:
- 1 <= n <= 10^4
- 1 <= connections.length <= 10^4
- connections[i].length == 3
- 1 <= xi, yi <= n
- xi != yi
- 0 <= costi <= 10^5
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1135 {
    public class Solution {
        public void Init() {
            int[][] conn1 = [[1, 2, 5], [1, 3, 6], [2, 3, 1]];
            Console.WriteLine(MinimumCost(3, conn1)); // 6

            int[][] conn2 = [[1, 2, 3], [3, 4, 4]];
            Console.WriteLine(MinimumCost(4, conn2)); // -1
        }

        // Time: O(E log E) where E is connections length
        // Space: O(V) where V is n
        public int MinimumCost(int n, int[][] connections) {
            Array.Sort(connections, (a, b) => a[2].CompareTo(b[2]));

            int[] parent = new int[n + 1];
            for (int i = 1; i <= n; i++) {
                parent[i] = i;
            }

            int Find(int i) {
                if (parent[i] != i) {
                    parent[i] = Find(parent[i]);
                }
                return parent[i];
            }

            bool Union(int i, int j) {
                int rootI = Find(i);
                int rootJ = Find(j);
                if (rootI != rootJ) {
                    parent[rootI] = rootJ;
                    return true;
                }
                return false;
            }

            int totalCost = 0;
            int edgesCount = 0;

            foreach (var conn in connections) {
                if (Union(conn[0], conn[1])) {
                    totalCost += conn[2];
                    edgesCount++;
                    if (edgesCount == n - 1) {
                        return totalCost;
                    }
                }
            }

            return n == 1 ? 0 : -1;
        }
    }
}