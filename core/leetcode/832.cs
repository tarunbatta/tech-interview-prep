// Url: https://leetcode.com/problems/flipping-an-image/

/*
832. Flipping an Image
Easy

Given a binary matrix A, we want to flip the image horizontally, then invert it, and return the resulting image.

To flip an image horizontally means that each row of the image is reversed.  For example, flipping [1, 1, 0] horizontally results in [0, 1, 1].

To invert an image means that each 0 is replaced by 1, and each 1 is replaced by 0. For example, inverting [0, 1, 1] results in [1, 0, 0].

Example 1:

Input: [[1,1,0],[1,0,1],[0,0,0]]
Output: [[1,0,0],[0,1,0],[1,1,1]]
Explanation: First reverse each row: [[0,1,1],[1,0,1],[0,0,0]].
Then, invert the image: [[1,0,0],[0,1,0],[1,1,1]]
Example 2:

Input: [[1,1,0,0],[1,0,0,1],[0,1,1,1],[1,0,1,0]]
Output: [[1,1,0,0],[0,1,1,0],[0,0,0,1],[1,0,1,0]]
Explanation: First reverse each row: [[0,0,1,1],[1,0,0,1],[1,1,1,0],[0,1,0,1]].
Then invert the image: [[1,1,0,0],[0,1,1,0],[0,0,0,1],[1,0,1,0]]
Notes:

1 <= A.length = A[0].length <= 20
0 <= A[i][j] <= 1
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem832 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (FlipAndInvertImage(null));
            Console.WriteLine (FlipAndInvertImage([[1, 1, 0], [1, 0, 1], [0, 0, 0]]));
            Console.WriteLine (FlipAndInvertImage([[1, 1, 0, 0], [1, 0, 0, 1], [0, 1, 1, 1], [1, 0, 1, 0]]));
        }

        public static int[][] FlipAndInvertImage (int[][] A) {
            if (A == null) {
                return null;
            }

            int r = A.Length;
            int c = A[0].Length;

            if (r == 0 || c == 0) {
                return null;
            }

            for (int i = 0; i < r; i++) {
                for (int j = 0; j < c / 2; j++) {
                    int temp = A[i][j];
                    A[i][j] = A[i][c - 1 - j];
                    A[i][c - 1 - j] = temp;
                }

                for (int j = 0; j < c; j++) {
                    A[i][j] ^= 1;
                }
            }

            return A;
        }
    }
}