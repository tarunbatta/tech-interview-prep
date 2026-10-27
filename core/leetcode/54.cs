// Url:https://leetcode.com/problems/spiral-matrix

/*
54. Spiral Matrix
Medium

Given a matrix of m x n elements (m rows, n columns), return all elements of the matrix in spiral order.

Example 1:

Input:
[
 [ 1, 2, 3 ],
 [ 4, 5, 6 ],
 [ 7, 8, 9 ]
]
Output: [1,2,3,6,9,8,7,4,5]


Example 2:
Input:
[
  [1, 2, 3, 4],
  [5, 6, 7, 8],
  [9,10,11,12]
]
Output: [1,2,3,4,8,12,11,10,9,5,6,7]
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem54 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (SpiralOrder([
                [1, 2, 3, 4],
                    [5, 6, 7, 8],
                    [9, 10, 11, 12],
                    [13, 14, 15, 16]
            ]));
            Console.WriteLine (SpiralOrder([
                [1, 2, 3, 4, 5, 6],
                    [7, 8, 9, 10, 11, 12],
                    [13, 14, 15, 16, 17, 18]
            ]));
        }

        public static IList<int> SpiralOrder (int[][] matrix) {
            var result = new List<int> ();

            if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) {
                return result;
            }
            int height = matrix.Length;
            int width = matrix[0].Length;

            int rowStart = 0;
            int rowEnd = height;
            int colStart = 0;
            int colEnd = width;

            while ((rowStart < rowEnd) && (colStart < colEnd)) {
                for (int i = colStart; i < colEnd; i++) {
                    result.Add (matrix[rowStart][i]);
                }
                rowStart++;

                for (int i = rowStart; i < rowEnd; i++) {
                    result.Add (matrix[i][colEnd - 1]);
                }
                colEnd--;

                if (rowStart < rowEnd) {
                    for (int i = colEnd - 1; i >= colStart; i--) {
                        result.Add (matrix[rowEnd - 1][i]);
                    }
                    rowEnd--;
                }

                if (colStart < colEnd) {
                    for (int i = rowEnd - 1; i >= rowStart; i--) {
                        result.Add (matrix[i][colStart]);
                    }
                    colStart++;
                }
            }

            return result;
        }
    }
}