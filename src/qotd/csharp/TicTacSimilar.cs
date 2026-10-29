using System;
using System.Collections;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Tic-Tac-Similar
    /// Given two tic-tac-toe 3x3 boards, can return true if they are “similar” and false if they are not.
    /// Two boards are similar if they are “equivalent” for all practical purposes.
    ///
    /// For example, if you have X in the middle and O in a corner (doesnt matter which corner, all 4 combinations are similar).
    /// </summary>
    public class TicTacSimilar {
        public static void Init () {
            Console.WriteLine (IsBoardSimilar (null, null));
            Console.WriteLine (IsBoardSimilar (new char[, ] { { 'x', 'x', 'o' }, { 'x', 'x', 'o' }, { 'x', 'x', 'o' } },
                new char[, ] { { 'o', 'x', 'x' }, { 'o', 'x', 'x' }, { 'o', 'x', 'x' } }));
        }

        public static bool IsBoardSimilar (char[, ] board1, char[, ] board2) {
            bool result = false;

            if (board1 != null && board2 != null) {
                int row1 = board1.GetLength (0);
                int col1 = board1.GetLength (1);
                int row2 = board2.GetLength (0);
                int col2 = board2.GetLength (1);

                if (row1 == row2 && col1 == col2) {
                    Hashtable hash = new Hashtable ();
                    char[] flatArray = new char[row1 * col1];

                    for (int i = 0; i < row1; i++) {
                        for (int j = 0; j < col1; j++) {
                            flatArray[(row1 * i) + j] = board1[i, j];
                        }
                    }
                    hash.Add (string.Join (",", flatArray), 1);

                    for (int j = col1 - 1, k = 0; j >= 0 && k < row1; j--, k++) {
                        for (int i = 0, l = 0; i < row1 && l < col1; i++, l++) {
                            flatArray[(row1 * k) + l] = board1[i, j];
                        }
                    }
                    hash.Add (string.Join (",", flatArray), 1);

                    for (int i = row1 - 1, k = 0; i >= 0 && k < row1; i--, k++) {
                        for (int j = col1 - 1, l = 0; j >= 0 && l < col1; j--, l++) {
                            flatArray[(row1 * k) + l] = board1[i, j];
                        }
                    }
                    hash.Add (string.Join (",", flatArray), 1);

                    for (int j = 0, k = 0; j < col1 && k < row1; j++, k++) {
                        for (int i = row1 - 1, l = 0; i >= 0 && l < col1; i--, l++) {
                            flatArray[(row1 * k) + l] = board1[i, j];
                        }
                    }
                    hash.Add (string.Join (",", flatArray), 1);

                    for (int i = 0; i < row2; i++) {
                        for (int j = 0; j < col2; j++) {
                            flatArray[(row2 * i) + j] = board2[i, j];
                        }
                    }

                    result = hash.ContainsKey (string.Join (",", flatArray));
                }
            }

            return result;
        }
    }
}