/*
Sorted Matrix Search: 

Given an M x N matrix in which each row and each column is sorted in
ascending order, write a method to find an element.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c10q9 {
    public class Solution {
        public static (int row, int col)? FindElement (int[][] matrix, int element) {
            if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return null;

            int row = 0;
            int col = matrix[0].Length - 1;

            while (row < matrix.Length && col >= 0) {
                if (matrix[row][col] == element) {
                    return (row, col);
                } else if (matrix[row][col] > element) {
                    col--;
                } else {
                    row++;
                }
            }
            return null;
        }

        public static void Init (string[] args = null) {
            int[][] matrix = new int[][] {
                new int[] { 15, 20, 40, 85 },
                new int[] { 20, 35, 80, 95 },
                new int[] { 30, 55, 95, 105 },
                new int[] { 40, 80, 100, 120 }
            };

            var pos = FindElement (matrix, 55);
            if (pos.HasValue) {
                Console.WriteLine ($"Found 55 at row {pos.Value.row}, col {pos.Value.col}");
            } else {
                Console.WriteLine ("55 not found");
            }
        }
    }
}
