// Url:https://leetcode.com/problems/candy-crush

/*
723. Candy Crush
Medium

This question is about implementing a basic elimination algorithm for Candy Crush.

Given an m x n integer array board representing the grid of candy where different positive integers board[i][j] represent different types of candies. A value of 0 in the cell represents that the cell is empty.

The given board represents the state of the game following the player's move. Now, you need to restore the board to a stable state by following these rules:
1. If three or more candies of the same type are adjacent vertically or horizontally, crush them all at the same time - these positions become empty.
2. After crushing all candies simultaneously, if an empty space on the board has candies on top of itself, then these candies will drop until they hit a candy or bottom at the same time. No new candies will drop outside the top boundary.
3. After the above steps, there may exist more candies that can be crushed. If so, you need to repeat the above steps.
4. If there does not exist more candies that can be crushed (i.e., the board is stable), then return the current board.

Example 1:
Input: board = [[110,5,112,113,114],[210,211,5,213,214],[310,311,3,313,314],[410,411,412,5,414],[5,1,512,3,3],[610,4,1,613,614],[710,1,2,713,714],[810,1,2,1,1],[1,1,2,2,2],[4,1,4,4,1014]]
Output: [[0,0,0,0,0],[0,0,0,0,0],[0,0,0,0,0],[110,0,0,0,114],[210,0,0,0,214],[310,0,0,113,314],[410,0,0,213,414],[610,211,112,313,614],[710,311,412,613,714],[810,411,512,713,1014]]

Constraints:
- m == board.length
- n == board[i].length
- 3 <= m, n <= 50
- 1 <= board[i][j] <= 2000
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem723 {
    public class Solution {
        public static void Init() {
            int[][] board = [
                [110, 5, 112, 113, 114],
                [210, 211, 5, 213, 214],
                [310, 311, 3, 313, 314],
                [410, 411, 412, 5, 414],
                [5, 1, 512, 3, 3],
                [610, 4, 1, 613, 614],
                [710, 1, 2, 713, 714],
                [810, 1, 2, 1, 1],
                [1, 1, 2, 2, 2],
                [4, 1, 4, 4, 1014]
            ];

            var result = CandyCrush(board);
            Console.WriteLine($"Result rows: {result.Length}, cols: {result[0].Length}");
        }

        // Time: O((m * n)^2)
        // Space: O(1) in-place
        public static int[][] CandyCrush(int[][] board) {
            int m = board.Length;
            int n = board[0].Length;
            bool shouldContinue = true;

            while (shouldContinue) {
                shouldContinue = false;

                // Mark horizontally
                for (int r = 0; r < m; r++) {
                    for (int c = 0; c < n - 2; c++) {
                        int v = Math.Abs(board[r][c]);
                        if (v != 0 && v == Math.Abs(board[r][c + 1]) && v == Math.Abs(board[r][c + 2])) {
                            board[r][c] = -v;
                            board[r][c + 1] = -v;
                            board[r][c + 2] = -v;
                            shouldContinue = true;
                        }
                    }
                }

                // Mark vertically
                for (int r = 0; r < m - 2; r++) {
                    for (int c = 0; c < n; c++) {
                        int v = Math.Abs(board[r][c]);
                        if (v != 0 && v == Math.Abs(board[r + 1][c]) && v == Math.Abs(board[r + 2][c])) {
                            board[r][c] = -v;
                            board[r + 1][c] = -v;
                            board[r + 2][c] = -v;
                            shouldContinue = true;
                        }
                    }
                }

                // Gravity drop
                if (shouldContinue) {
                    for (int c = 0; c < n; c++) {
                        int writeRow = m - 1;
                        for (int r = m - 1; r >= 0; r--) {
                            if (board[r][c] > 0) {
                                board[writeRow--][c] = board[r][c];
                            }
                        }
                        while (writeRow >= 0) {
                            board[writeRow--][c] = 0;
                        }
                    }
                }
            }

            return board;
        }
    }
}