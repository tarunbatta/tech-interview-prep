// Url:https://leetcode.com/problems/campus-bikes

/*
1057. Campus Bikes
Medium

On a campus represented on the X-Y plane, there are n workers and m bikes, with n <= m.

You are given the arrays workers and bikes of length n and m respectively, where workers[i] = [xi, yi] and bikes[j] = [xj, yj] represent the positions of the ith worker and jth bike, respectively.

We assign one unique bike to each worker by repeating the following until each worker has a bike:
1. Find the pair (worker_i, bike_j) with the shortest Manhattan distance between each other, and assign the bike to the worker.
2. If there are multiple (worker_i, bike_j) pairs with the same shortest Manhattan distance, we choose the pair with the smallest worker index. If there are multiple pairs with the same smallest worker index, then we choose the pair with the smallest bike index.
3. Once a worker and a bike are paired, they will not be considered for any future pairings.

Return an array result of length n, where result[i] is the index (0-indexed) of the bike that the ith worker is assigned to.

The Manhattan distance between two points p1 and p2 is Manhattan(p1, p2) = |p1.x - p2.x| + |p1.y - p2.y|.

Example 1:
Input: workers = [[0,0],[2,1]], bikes = [[1,2],[3,3]]
Output: [1,0]
Explanation: Worker 1 grabs Bike 0 at distance 1. Worker 0 grabs Bike 1 at distance 3.

Example 2:
Input: workers = [[0,0],[1,1],[2,0]], bikes = [[1,0],[2,2],[2,1]]
Output: [0,2,1]

Constraints:
- n == workers.length
- m == bikes.length
- 1 <= n <= m <= 1000
- workers[i].length == bikes[j].length == 2
- 0 <= xi, yi, xj, yj < 1000
- All worker and bike locations are distinct.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem1057 {
    public class Solution {
        public void Init() {
            int[][] workers1 = [[0, 0], [2, 1]];
            int[][] bikes1 = [[1, 2], [3, 3]];
            Console.WriteLine(string.Join(", ", AssignBikes(workers1, bikes1))); // 1, 0

            int[][] workers2 = [[0, 0], [1, 1], [2, 0]];
            int[][] bikes2 = [[1, 0], [2, 2], [2, 1]];
            Console.WriteLine(string.Join(", ", AssignBikes(workers2, bikes2))); // 0, 2, 1
        }

        // Time: O(n * m) using bucket sort since maximum Manhattan distance is 2000
        // Space: O(n * m) to store all worker-bike pairs in buckets
        public int[] AssignBikes(int[][] workers, int[][] bikes) {
            int n = workers.Length;
            int m = bikes.Length;

            List<(int Worker, int Bike)>[] buckets = new List<(int, int)>[2001];

            for (int w = 0; w < n; w++) {
                for (int b = 0; b < m; b++) {
                    int dist = Math.Abs(workers[w][0] - bikes[b][0]) + Math.Abs(workers[w][1] - bikes[b][1]);
                    if (buckets[dist] == null) {
                        buckets[dist] = [];
                    }
                    buckets[dist].Add((w, b));
                }
            }

            int[] result = new int[n];
            bool[] workerAssigned = new bool[n];
            bool[] bikeAssigned = new bool[m];
            int assignedCount = 0;

            for (int d = 0; d <= 2000; d++) {
                if (buckets[d] == null) {
                    continue;
                }

                foreach (var (w, b) in buckets[d]) {
                    if (!workerAssigned[w] && !bikeAssigned[b]) {
                        workerAssigned[w] = true;
                        bikeAssigned[b] = true;
                        result[w] = b;
                        assignedCount++;

                        if (assignedCount == n) {
                            return result;
                        }
                    }
                }
            }

            return result;
        }
    }
}