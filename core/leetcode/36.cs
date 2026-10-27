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

        public static bool IsValidSudoku (char[][] board) {
            if (board == null || board.Length != 9 || board[0].Length != 9) {
                return false;
            }

            bool result = true;

            // row check
            for (int i = 0; i < board.Length; i++) {
                HashSet<char> rows = [];
                HashSet<char> cols = [];
                HashSet<char> cube = [];

                for (int j = 0; j < board[0].Length; j++) {
                    if (board[i][j] != '.' && !rows.Add (board[i][j])) {
                        return false;
                    }

                    if (board[j][i] != '.' && !cols.Add (board[j][i])) {
                        return false;
                    }

                    int rowIndex = 3 * (i / 3);
                    int colIndex = 3 * (i % 3);

                    if (board[rowIndex + j / 3][colIndex + j % 3] != '.' && !cube.Add (board[rowIndex + j / 3][colIndex + j % 3])) {
                        return false;
                    }
                }
            }

            return result;
        }
    }
}