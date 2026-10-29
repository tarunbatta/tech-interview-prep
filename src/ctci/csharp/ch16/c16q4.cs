/*
Tic Tac Win: 

Design an algorithm to figure out if someone has won a game of tic-tac-toe.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c16q4 {
    public class Solution {
        public static char WhoWon (char[][] board) {
            for (int i = 0; i < 3; i++) {
                // Check rows
                if (HasWinner (board[i][0], board[i][1], board[i][2])) return board[i][0];
                // Check cols
                if (HasWinner (board[0][i], board[1][i], board[2][i])) return board[0][i];
            }
            // Diagonals
            if (HasWinner (board[0][0], board[1][1], board[2][2])) return board[0][0];
            if (HasWinner (board[0][2], board[1][1], board[2][0])) return board[0][2];

            return ' ';
        }

        private static bool HasWinner (char a, char b, char c) {
            return a != ' ' && a == b && b == c;
        }

        public static void Init (string[] args = null) {
            char[][] board1 = new char[][] {
                new char[] { 'X', 'O', 'X' },
                new char[] { 'X', 'X', 'O' },
                new char[] { 'X', 'O', 'O' }
            };
            Console.WriteLine ($"Winner board 1: '{WhoWon (board1)}'"); // X
        }
    }
}
