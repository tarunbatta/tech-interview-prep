/*
Eight Queens: 

Write an algorithm to print all ways of arranging eight queens on an 8x8 chess board
so that none of them share the same row, column, or diagonal. In this case, "diagonal" means all
diagonals, not just the two that bisect the board.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c8q12 {
    public class Solution {
        private const int GridSize = 8;

        public static List<int[]> PlaceQueens () {
            List<int[]> results = new List<int[]> ();
            PlaceQueensHelper (0, new int[GridSize], results);
            return results;
        }

        private static void PlaceQueensHelper (int row, int[] columns, List<int[]> results) {
            if (row == GridSize) {
                results.Add ((int[])columns.Clone ());
            } else {
                for (int col = 0; col < GridSize; col++) {
                    if (CheckValid (columns, row, col)) {
                        columns[row] = col;
                        PlaceQueensHelper (row + 1, columns, results);
                    }
                }
            }
        }

        private static bool CheckValid (int[] columns, int row1, int col1) {
            for (int row2 = 0; row2 < row1; row2++) {
                int col2 = columns[row2];
                if (col1 == col2) return false;
                int colDist = Math.Abs (col2 - col1);
                int rowDist = row1 - row2;
                if (colDist == rowDist) return false;
            }
            return true;
        }

        public static void Init (string[] args = null) {
            List<int[]> solutions = PlaceQueens ();
            Console.WriteLine ($"Total 8-Queens Solutions: {solutions.Count}"); // 92
        }
    }
}
