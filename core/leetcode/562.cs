// Url: https://leetcode.com/problems/longest-line-of-consecutive-one-in-matrix

/*
562. Longest Line of Consecutive One in Matrix
Medium

Given an m x n binary matrix mat, return the length of the longest line of consecutive one in the matrix.

The line could be horizontal, vertical, diagonal, or anti-diagonal.

Example 1:
Input: mat = [[0,1,1,0],[0,1,1,0],[0,0,0,1]]
Output: 3

Example 2:
Input: mat = [[1,1,1,1],[0,1,1,0],[0,0,0,1]]
Output: 4

Constraints:
m == mat.length
n == mat[i].length
1 <= m, n <= 10^4
1 <= m * n <= 10^4
mat[i][j] is either 0 or 1.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem562
{
    public class Solution
    {
        public void Init()
        {
            int[][] mat1 = new int[][]
            {
                new int[] { 0, 1, 1, 0 },
                new int[] { 0, 1, 1, 0 },
                new int[] { 0, 0, 0, 1 }
            };
            Console.WriteLine(LongestLine(mat1)); // 3

            int[][] mat2 = new int[][]
            {
                new int[] { 1, 1, 1, 1 },
                new int[] { 0, 1, 1, 0 },
                new int[] { 0, 0, 0, 1 }
            };
            Console.WriteLine(LongestLine(mat2)); // 4
        }

        // Time: O(m * n) single pass dynamic programming
        // Space: O(m * n) for 4-directional DP table
        public static int LongestLine(int[][] mat)
        {
            if (mat == null || mat.Length == 0 || mat[0].Length == 0)
            {
                return 0;
            }

            int m = mat.Length;
            int n = mat[0].Length;

            // dp[r, c, 0]: horizontal, 1: vertical, 2: diagonal, 3: anti-diagonal
            int[,,] dp = new int[m, n, 4];
            int maxLine = 0;

            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (mat[r][c] == 1)
                    {
                        dp[r, c, 0] = (c > 0 ? dp[r, c - 1, 0] : 0) + 1;
                        dp[r, c, 1] = (r > 0 ? dp[r - 1, c, 1] : 0) + 1;
                        dp[r, c, 2] = (r > 0 && c > 0 ? dp[r - 1, c - 1, 2] : 0) + 1;
                        dp[r, c, 3] = (r > 0 && c < n - 1 ? dp[r - 1, c + 1, 3] : 0) + 1;

                        maxLine = Math.Max(maxLine, Math.Max(dp[r, c, 0], Math.Max(dp[r, c, 1], Math.Max(dp[r, c, 2], dp[r, c, 3]))));
                    }
                }
            }

            return maxLine;
        }
    }
}