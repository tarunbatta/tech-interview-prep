// Url:https://leetcode.com/problems/unique-paths

/*
62. Unique Paths
Medium

A robot is located at the top-left corner of a m x n grid (marked 'Start' in the diagram below).

The robot can only move either down or right at any point in time. The robot is trying to reach the bottom-right corner of the grid (marked 'Finish' in the diagram below).

How many possible unique paths are there?


Above is a 7 x 3 grid. How many possible unique paths are there?

Note: m and n will be at most 100.

Example 1:

Input: m = 3, n = 2
Output: 3
Explanation:
From the top-left corner, there are a total of 3 ways to reach the bottom-right corner:
1. Right -> Right -> Down
2. Right -> Down -> Right
3. Down -> Right -> Right


Example 2:

Input: m = 7, n = 3
Output: 28
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem62 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (UniquePaths_BottomUp(3, 2));
            Console.WriteLine (UniquePaths_BottomUp(7, 3));
        }

        public static int UniquePaths_BottomUp (int m, int n) {
            if (m <= 0 || n <= 0) {
                return 0;
            }

            int[, ] grid = new int[m, n];

            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    if (i == 0 || j == 0) {
                        grid[i, j] = 1;
                    } else {
                        grid[i, j] = grid[i - 1, j] + grid[i, j - 1];
                    }
                }
            }

            return grid[m - 1, n - 1];
        }

        public static int UniquePaths_Recursive (int m, int n) {
            if (m <= 0 || n <= 0) {
                return 0;
            }

            if (m == 1 || n == 1) {
                return 1;
            }

            return UniquePaths_Recursive(m - 1, n) + UniquePaths_Recursive(m, n - 1);
        }

        public static int UniquePaths_TopDown (int m, int n) {
            if (m <= 0 || n <= 0) {
                return 0;
            }

            int[, ] grid = new int[m, n];

            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    grid[i, j] = -1;
                }
            }

            return UniquePaths_TopDownHelper(grid, m - 1, n - 1);
        }

        public static int UniquePaths_TopDownHelper (int[, ] grid, int i, int j) {
            if (i == 0 || j == 0) {
                return 1;
            }

            if (grid[i, j] == -1) {
                grid[i, j] = UniquePaths_TopDownHelper(grid, i - 1, j) + UniquePaths_TopDownHelper(grid, i, j - 1);
            }

            return grid[i, j];
        }
    }
}