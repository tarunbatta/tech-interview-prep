// Url: https://leetcode.com/problems/sparse-matrix-multiplication

/*
311. Sparse Matrix Multiplication
Medium

Given two sparse matrices mat1 of size m x k and mat2 of size k x n, return the result of mat1 x mat2. You may assume that multiplication is always possible.

Example 1:
Input: mat1 = [[1,0,0],[-1,0,3]], mat2 = [[7,0,0],[0,0,0],[0,0,1]]
Output: [[7,0,0],[-7,0,3]]

Example 2:
Input: mat1 = [[0]], mat2 = [[0]]
Output: [[0]]

Constraints:
m == mat1.length
k == mat1[i].length == mat2.length
n == mat2[i].length
1 <= m, n, k <= 100
-100 <= mat1[i][j], mat2[i][j] <= 100
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem311
{
    public class Solution
    {
        public static void Init()
        {
            int[][] mat1 =
            [
                [1, 0, 0],
                [-1, 0, 3]
            ];

            int[][] mat2 =
            [
                [7, 0, 0],
                [0, 0, 0],
                [0, 0, 1]
            ];

            int[][] res = Multiply(mat1, mat2);
            for (int i = 0; i < res.Length; i++)
            {
                Console.WriteLine(string.Join(", ", res[i]));
            }
        }

        // Time: O(non_zeros(mat1) * n) where n is mat2[0].Length (skips all zero multiplications)
        // Space: O(1) auxiliary space (excluding result matrix)
        public static int[][] Multiply(int[][] mat1, int[][] mat2)
        {
            if (mat1 == null || mat2 == null || mat1.Length == 0 || mat2.Length == 0)
            {
                return [];
            }

            int m = mat1.Length;
            int k = mat1[0].Length;
            int n = mat2[0].Length;

            int[][] result = new int[m][];
            for (int i = 0; i < m; i++)
            {
                result[i] = new int[n];
            }

            for (int i = 0; i < m; i++)
            {
                for (int p = 0; p < k; p++)
                {
                    if (mat1[i][p] != 0)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (mat2[p][j] != 0)
                            {
                                result[i][j] += mat1[i][p] * mat2[p][j];
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}