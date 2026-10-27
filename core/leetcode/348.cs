// Url:https://leetcode.com/problems/design-tic-tac-toe

/*
348. Design Tic-Tac-Toe
Medium

Design a Tic-tac-toe game that is played between two players on a n x n grid.

You may assume the following rules:

A move is guaranteed to be valid and is placed on an empty block.
Once a winning condition is reached, no more moves is allowed.
A player who succeeds in placing n of their marks in a horizontal, vertical, or diagonal row wins the game.
Example:
Given n = 3, assume that player 1 is "X" and player 2 is "O" in the board.

TicTacToe toe = new TicTacToe(3);

toe.move(0, 0, 1); -> Returns 0 (no one wins)
|X| | |
| | | |    // Player 1 makes a move at (0, 0).
| | | |

toe.move(0, 2, 2); -> Returns 0 (no one wins)
|X| |O|
| | | |    // Player 2 makes a move at (0, 2).
| | | |

toe.move(2, 2, 1); -> Returns 0 (no one wins)
|X| |O|
| | | |    // Player 1 makes a move at (2, 2).
| | |X|

toe.move(1, 1, 2); -> Returns 0 (no one wins)
|X| |O|
| |O| |    // Player 2 makes a move at (1, 1).
| | |X|

toe.move(2, 0, 1); -> Returns 0 (no one wins)
|X| |O|
| |O| |    // Player 1 makes a move at (2, 0).
|X| |X|

toe.move(1, 0, 2); -> Returns 0 (no one wins)
|X| |O|
|O|O| |    // Player 2 makes a move at (1, 0).
|X| |X|

toe.move(2, 1, 1); -> Returns 1 (player 1 wins)
|X| |O|
|O|O| |    // Player 1 makes a move at (2, 1).
|X|X|X|
Follow up:
Could you do better than O(n2) per move() operation?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem348 {
    public class Solution {
        public static void Init () {

        }
    }

    public class TicTacToe_a(int n)
    {
        int diagonal = 0;
        int antidiagonal = 0;
        int[] rows = new int[n];
        int[] cols = new int[n];
        int n = n;

        /** Player {player} makes a move at ({row}, {col}).
            @param row The row of the board.
            @param col The column of the board.
            @param player The player, can be either 1 or 2.
            @return The current winning condition, can be either:
                    0: No one wins.
                    1: Player 1 wins.
                    2: Player 2 wins. */
        public int Move (int row, int col, int player) {
            var num = 1;
            if (player == 2) {
                num = -1;
            }
            rows[col] += num;
            cols[row] += num;

            if (row == col) {
                diagonal += num;
            }

            if (row == n - col - 1) {
                antidiagonal += num;
            }

            if (rows[col] == n || cols[row] == n || diagonal == n || antidiagonal == n) {
                return 1;
            } else if (rows[col] == -n || cols[row] == -n || diagonal == -n || antidiagonal == -n) {
                return 2;
            } else {
                return 0;
            }
        }
    }

    public class TicTacToe_b(int n)
    {
        private int[, ] board = new int[n, n];
        private int n = n;

        /** Player {player} makes a move at ({row}, {col}).
            @param row The row of the board.
            @param col The column of the board.
            @param player The player, can be either 1 or 2.
            @return The current winning condition, can be either:
                    0: No one wins.
                    1: Player 1 wins.
                    2: Player 2 wins. */
        public int Move (int row, int col, int player) {
            board[row, col] = player;

            return (CheckHorizontal (row, player) ||
                    CheckVertical (col, player) ||
                    CheckRightDiag (player) ||
                    CheckLeftDiag (player)) ?
                player :
                0;
        }

        private bool CheckHorizontal (int row, int player) {
            for (int i = 0; i < n; i++) {
                if (board[row, i] != player) {
                    return false;
                }
            }

            return true;
        }

        private bool CheckVertical (int col, int player) {
            for (int i = 0; i < n; i++) {
                if (board[i, col] != player) {
                    return false;
                }
            }

            return true;
        }

        private bool CheckRightDiag (int player) {
            int j = 0;
            for (int i = n - 1; i >= 0; i--) {
                if (board[i, j++] != player) {
                    return false;
                }
            }

            return true;
        }

        private bool CheckLeftDiag (int player) {
            for (int i = 0; i < n; i++) {
                if (board[i, i] != player) {
                    return false;
                }
            }

            return true;
        }
    }
}