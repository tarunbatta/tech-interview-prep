// Url: https://leetcode.com/problems/valid-sudoku/

/*
36. Valid Sudoku
Medium

Determine if a 9x9 Sudoku board is valid. Only the filled cells need to be validated according to the following rules:

Each row must contain the digits 1-9 without repetition.
Each column must contain the digits 1-9 without repetition.
Each of the 9 3x3 sub-boxes of the grid must contain the digits 1-9 without repetition.

A partially filled sudoku which is valid.

The Sudoku board could be partially filled, where empty cells are filled with the character '.'.

Example 1:

Input:
[
  ['5','3','.','.','7','.','.','.','.'],
  ['6','.','.','1','9','5','.','.','.'],
  ['.','9','8','.','.','.','.','6','.'],
  ['8','.','.','.','6','.','.','.','3'],
  ['4','.','.','8','.','3','.','.','1'],
  ['7','.','.','.','2','.','.','.','6'],
  ['.','6','.','.','.','.','2','8','.'],
  ['.','.','.','4','1','9','.','.','5'],
  ['.','.','.','.','8','.','.','7','9']
]
Output: true
Example 2:

Input:
[
  ['8','3','.','.','7','.','.','.','.'],
  ['6','.','.','1','9','5','.','.','.'],
  ['.','9','8','.','.','.','.','6','.'],
  ['8','.','.','.','6','.','.','.','3'],
  ['4','.','.','8','.','3','.','.','1'],
  ['7','.','.','.','2','.','.','.','6'],
  ['.','6','.','.','.','.','2','8','.'],
  ['.','.','.','4','1','9','.','.','5'],
  ['.','.','.','.','8','.','.','7','9']
]
Output: false
Explanation: Same as Example 1, except with the 5 in the top left corner being 
    modified to 8. Since there are two 8's in the top left 3x3 sub-box, it is invalid.
Note:

A Sudoku board (partially filled) could be valid but is not necessarily solvable.
Only the filled cells need to be validated according to the mentioned rules.
The given board contain only digits 1-9 and the character '.'.
The given board size is always 9x9.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem36 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (IsValidSudoku(null));
            Console.WriteLine (IsValidSudoku([
                ['5', '3', '.', '.', '7', '.', '.', '.', '.'],
                    ['6', '.', '.', '1', '9', '5', '.', '.', '.'],
                    ['.', '9', '8', '.', '.', '.', '.', '6', '.'],
                    ['8', '.', '.', '.', '6', '.', '.', '.', '3'],
                    ['4', '.', '.', '8', '.', '3', '.', '.', '1'],
                    ['7', '.', '.', '.', '2', '.', '.', '.', '6'],
                    ['.', '6', '.', '.', '.', '.', '2', '8', '.'],
                    ['.', '.', '.', '4', '1', '9', '.', '.', '5'],
                    ['.', '.', '.', '.', '8', '.', '.', '7', '9']
            ]));
            Console.WriteLine (IsValidSudoku([
                ['8', '3', '.', '.', '7', '.', '.', '.', '.'],
                    ['6', '.', '.', '1', '9', '5', '.', '.', '.'],
                    ['.', '9', '8', '.', '.', '.', '.', '6', '.'],
                    ['8', '.', '.', '.', '6', '.', '.', '.', '3'],
                    ['4', '.', '.', '8', '.', '3', '.', '.', '1'],
                    ['7', '.', '.', '.', '2', '.', '.', '.', '6'],
                    ['.', '6', '.', '.', '.', '.', '2', '8', '.'],
                    ['.', '.', '.', '4', '1', '9', '.', '.', '5'],
                    ['.', '.', '.', '.', '8', '.', '.', '7', '9']
            ]));
        }

        // Time: O(1) - exactly 81 cells examined
        // Space: O(1) - fixed 9x9 boolean arrays
        public static bool IsValidSudoku(char[][] board)
        {
            if (board == null || board.Length != 9 || board[0].Length != 9)
            {
                return false;
            }

            bool[,] rows = new bool[9, 9];
            bool[,] cols = new bool[9, 9];
            bool[,] boxes = new bool[9, 9];

            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    char ch = board[r][c];
                    if (ch == '.')
                    {
                        continue;
                    }

                    int digit = ch - '1';
                    if (digit < 0 || digit > 8)
                    {
                        return false;
                    }

                    int boxIdx = (r / 3) * 3 + (c / 3);

                    if (rows[r, digit] || cols[c, digit] || boxes[boxIdx, digit])
                    {
                        return false;
                    }

                    rows[r, digit] = true;
                    cols[c, digit] = true;
                    boxes[boxIdx, digit] = true;
                }
            }

            return true;
        }
    }
}