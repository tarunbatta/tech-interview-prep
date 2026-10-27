// Url:https://leetcode.com/problems/minimum-path-sum

/*
64. Minimum Path Sum
Medium

Given a m x n grid filled with non-negative numbers, find a path from top left to bottom right which minimizes the sum of all numbers along its path.

Note: You can only move either down or right at any point in time.

Example:

Input:
[
  [1,3,1],
  [1,5,1],
  [4,2,1]
]
Output: 7
Explanation: Because the path 1→3→1→1→1 minimizes the sum.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem64 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (m*n)
        // Space: O (1)
        public static int MinPathSum (int[][] grid) {
            if (grid == null || grid.Length == 0 || grid[0].Length == 0) {
                return 0;
            }

            int m = grid.Length;
            int n = grid[0].Length;

            for (int i = 1; i < m; i++) {
                grid[i][0] += grid[i - 1][0];
            }

            for (int i = 1; i < n; i++) {
                grid[0][i] += grid[0][i - 1];
            }

            for (int i = 1; i < m; i++) {
                for (int j = 1; j < n; j++) {
                    grid[i][j] += Math.Min (grid[i - 1][j], grid[i][j - 1]);
                }
            }

            return grid[m - 1][n - 1];
        }
    }
}