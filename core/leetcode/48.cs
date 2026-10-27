// Url: https://leetcode.com/problems/rotate-image/

/*
48. Rotate Image
Medium

You are given an n x n 2D matrix representing an image.

Rotate the image by 90 degrees (clockwise).

Note:

You have to rotate the image in-place, which means you have to modify the input 2D matrix directly. DO NOT allocate another 2D matrix and do the rotation.

Example 1:
Given input matrix = 
[
  [1,2,3],
  [4,5,6],
  [7,8,9]
],

rotate the input matrix in-place such that it becomes:
[
  [7,4,1],
  [8,5,2],
  [9,6,3]
]

Example 2:
Given input matrix =
[
  [ 5, 1, 9,11],
  [ 2, 4, 8,10],
  [13, 3, 6, 7],
  [15,14,12,16]
], 

rotate the input matrix in-place such that it becomes:
[
  [15,13, 2, 5],
  [14, 3, 4, 1],
  [12, 6, 8, 9],
  [16, 7,10,11]
]
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem48 {
    public class Solution {
        public static void Init () {
            Rotate_a(null);
            Rotate_a([
                [1, 2, 3],
                    [4, 5, 6],
                    [7, 8, 9]
            ]);
            Rotate_a([
                [5, 1, 9, 11],
                    [2, 4, 8, 10],
                    [13, 3, 6, 7],
                    [15, 14, 12, 16]
            ]);

            Rotate_b(null);
            Rotate_b([
                [1, 2, 3],
                    [4, 5, 6],
                    [7, 8, 9]
            ]);
            Rotate_b([
                [5, 1, 9, 11],
                    [2, 4, 8, 10],
                    [13, 3, 6, 7],
                    [15, 14, 12, 16]
            ]);
        }

        public static void Rotate_a (int[][] matrix) {
            int n = matrix.Length;
            if (matrix == null || n == 0) {
                return;
            }

            for (int i = 0; i < n / 2; i++) {
                int first = i;
                int last = n - 1 - i;

                for (int j = first; j < last; j++) {
                    int offset = j - first;
                    int top = matrix[first][j];

                    matrix[first][j] = matrix[last - offset][first];
                    matrix[last - offset][first] = matrix[last][last - offset];
                    matrix[last][last - offset] = matrix[j][last];
                    matrix[j][last] = top;
                }
            }
        }

        public static void Rotate_b (int[][] matrix) {
            int n = matrix.Length;

            // transpose the matrix
            for (int i = 0; i < n; i++) {
                for (int j = i; j < n; j++) {
                    int temp = 0;
                    temp = matrix[i][j];
                    matrix[i][j] = matrix[j][i];
                    matrix[j][i] = temp;
                }
            }

            // flip it symmetrically
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n / 2; j++) {
                    int temp = 0;
                    temp = matrix[i][j];
                    matrix[i][j] = matrix[i][n - 1 - j];
                    matrix[i][n - 1 - j] = temp;
                }
            }
        }
    }
}