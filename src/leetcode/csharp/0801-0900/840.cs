// Url: https://leetcode.com/problems/magic-squares-in-grid

/*
840. Magic Squares In Grid
Easy

A 3 x 3 magic square is a 3 x 3 grid filled with distinct numbers from 1 to 9 such that each row, column, and both diagonals all have the same sum.

Given an grid of integers, how many 3 x 3 "magic square" subgrids are there?  (Each subgrid is contiguous).

 

Example 1:

Input: [[4,3,8,4],
        [9,5,1,9],
        [2,7,6,2]]
Output: 1
Explanation: 
The following subgrid is a 3 x 3 magic square:
438
951
276

while this one is not:
384
519
762

In total, there is only one magic square inside the given grid.
Note:

1 <= grid.length <= 10
1 <= grid[0].length <= 10
0 <= grid[i][j] <= 15
*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.LeetCode.problem840 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (NumMagicSquaresInside([
                [4, 3, 8, 4],
                    [9, 5, 1, 9],
                    [2, 7, 6, 2]
            ]));

            Console.WriteLine (NumMagicSquaresInside([
                [8]
            ]));

            Console.WriteLine (NumMagicSquaresInside([
                [2, 7, 6],
                    [1, 5, 9],
                    [4, 3, 8]
            ]));

            Console.WriteLine (NumMagicSquaresInside([
                [5, 5, 5],
                    [5, 5, 5],
                    [5, 5, 5]
            ]));
        }

        public static int NumMagicSquaresInside (int[][] grid) {
            int result = 0;

            if (grid == null || grid.Length < 3 || grid[0].Length < 3) {
                return result;
            }

            for (int i = 0; i < grid.Length - 2; i++) {
                for (int j = 0; j < grid[0].Length - 2; j++) {
                    if (IsMagicSquare(grid, i, j)) {
                        result++;
                    }
                }
            }

            return result;
        }

        public static bool IsMagicSquare (int[][] grid, int i, int j) {
            bool result = false;
            Hashtable hash = [];

            for (int x = 0; x < 3; x++) {
                for (int y = 0; y < 3; y++) {
                    int t = grid[i + x][j + y];

                    if (hash.ContainsKey (t) || t < 1 || t > 9) {
                        return false;
                    }

                    hash.Add (t, 1);
                }
            }

            int row1 = grid[i][j] + grid[i][j + 1] + grid[i][j + 2];
            int row2 = grid[i + 1][j] + grid[i + 1][j + 1] + grid[i + 1][j + 2];
            int row3 = grid[i + 2][j] + grid[i + 2][j + 1] + grid[i + 2][j + 2];
            int col1 = grid[i][j] + grid[i + 1][j] + grid[i + 2][j];
            int col2 = grid[i][j + 1] + grid[i + 1][j + 1] + grid[i + 2][j + 1];
            int col3 = grid[i][j + 2] + grid[i + 1][j + 2] + grid[i + 2][j + 2];
            int diag1 = grid[i][j] + grid[i + 1][j + 1] + grid[i + 2][j + 2];
            int diag2 = grid[i][j + 2] + grid[i + 1][j + 1] + grid[i + 2][j];

            if ((row1 == row2) && (row1 == row3) && (col1 == col2) && (col1 == col3) && (diag1 == diag2) && (row1 == col1) && (col1 == diag1)) {
                result = true;
            }

            return result;
        }
    }
}