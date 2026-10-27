// Url:https://leetcode.com/problems/campus-bikes-ii

/*
1066. Campus Bikes II
Medium

On a campus represented on the X-Y plane, there are n workers and m bikes, with n <= m.

You are given the arrays workers and bikes of length n and m respectively, where workers[i] = [xi, yi] and bikes[j] = [xj, yj] represent the positions of the ith worker and jth bike, respectively.

Assign each worker one unique bike so that the sum of the Manhattan distances between each worker and their assigned bike is minimized.

Return the minimum possible sum of Manhattan distances between each worker and their assigned bike.

The Manhattan distance between two points p1 and p2 is Manhattan(p1, p2) = |p1.x - p2.x| + |p1.y - p2.y|.

Example 1:
Input: workers = [[0,0],[2,1]], bikes = [[1,2],[3,3]]
Output: 6
Explanation: 
We assign bike 0 to worker 0, bike 1 to worker 1. The Manhattan distance of both assignments is 3, so the total distance is 6.

Example 2:
Input: workers = [[0,0],[1,1],[2,0]], bikes = [[1,0],[2,2],[2,1]]
Output: 4
Explanation: 
We first assign bike 0 to worker 0, then assign bike 1 to worker 1 or worker 2, bike 2 to worker 2 or worker 1. Both assignments lead to sum of distances 4.

Constraints:
- n == workers.length
- m == bikes.length
- 1 <= n <= m <= 10
- workers[i].length == 2
- bikes[i].length == 2
- 0 <= workers[i][0], workers[i][1], bikes[i][0], bikes[i][1] < 1000
- All the workers and the bikes locations are unique.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1066
{
    public class Solution
    {
        public static void Init()
        {
            int[][] workers1 = [[0, 0], [2, 1]];
            int[][] bikes1 = [[1, 2], [3, 3]];
            Console.WriteLine(AssignBikes(workers1, bikes1)); // 6

            int[][] workers2 = [[0, 0], [1, 1], [2, 0]];
            int[][] bikes2 = [[1, 0], [2, 2], [2, 1]];
            Console.WriteLine(AssignBikes(workers2, bikes2)); // 4
        }

        // Time: O(n * 2^m * m) where n <= m <= 10
        // Space: O(n * 2^m) for memoization table
        public static int AssignBikes(int[][] workers, int[][] bikes)
        {
            int n = workers.Length;
            int m = bikes.Length;
            int[,] memo = new int[n, 1 << m];

            return Dp(0, 0, workers, bikes, memo);
        }

        private static int Dp(int w, int mask, int[][] workers, int[][] bikes, int[,] memo)
        {
            if (w == workers.Length)
            {
                return 0;
            }

            if (memo[w, mask] != 0)
            {
                return memo[w, mask];
            }

            int minDistance = int.MaxValue;
            for (int b = 0; b < bikes.Length; b++)
            {
                if ((mask & (1 << b)) == 0)
                {
                    int dist = Math.Abs(workers[w][0] - bikes[b][0]) + Math.Abs(workers[w][1] - bikes[b][1]);
                    int total = dist + Dp(w + 1, mask | (1 << b), workers, bikes, memo);
                    minDistance = Math.Min(minDistance, total);
                }
            }

            return memo[w, mask] = minDistance;
        }
    }
}