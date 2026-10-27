/*
Zero Matrix: Write an algorithm such that if an element in an MxN matrix is 0, its entire row
and column are set to 0.
*/

using System;

namespace InterviewPreperationGuide.Core.CrackingTheCodingInterview.c1q8 {
    public class Solution {
        public static void Init (string[] args) {
            int[, ] matrix1 = new int[, ] { { 1, 2, 3 }, { 4, 0, 6 }, { 7, 8, 9 } };
            int[, ] matrix2 = new int[, ] { { 1, 2, 3, 4 }, { 5, 0, 7, 8 }, { 9, 10, 0, 12 }, { 13, 14, 15, 16 } };
            int m1 = 3;
            int n1 = 3;
            int m2 = 4;
            int n2 = 4;

            Console.WriteLine ("Original Martix1:");
            DisplayMatrix (matrix1, m1, n1);

            Console.WriteLine ("Martix1 with Zero Setting:");
            SetZeroinMatrix (matrix1, m1, n1);

            Console.WriteLine ("Original Martix2:");
            DisplayMatrix (matrix2, m2, n2);

            Console.WriteLine ("Martix2 with Zero Setting:");
            SetZeroinMatrix (matrix2, m2, n2);
        }

        private static void SetZeroinMatrix (int[, ] matrix, int m, int n) {
            bool[] row = new bool[m];
            bool[] col = new bool[n];

            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    if (matrix[i, j] == 0) {
                        row[i] = true;
                        col[j] = true;
                    }
                }
            }

            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    if (row[i] || col[j]) {
                        matrix[i, j] = 0;
                    }
                }
            }

            DisplayMatrix (matrix, m, n);
        }

        private static void DisplayMatrix (int[, ] matrix, int m, int n) {
            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    Console.Write (matrix[i, j] + "\t");
                }

                Console.WriteLine ("");
            }
        }
    }
}