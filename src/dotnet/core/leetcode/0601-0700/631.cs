// Url:https://leetcode.com/problems/design-excel-sum-formula

/*
631. Design Excel Sum Formula
Hard

Design the basic function of Excel and implement the function of the sum formula.

Implement the Excel class:
- Excel(int height, char width) Initializes the object with that height and width of the sheet. The height is an integer and at most 26. The width is a character and at most 'Z'. The rows are 1-indexed and columns are 'A'-indexed.
- void Set(int row, char column, int val) Changes the value at cell (row, column) to val.
- int Get(int row, char column) Returns the value at cell (row, column).
- int Sum(int row, char column, string[] numbers) Sets the value at cell (row, column) to be the sum of cells represented by numbers and returns the value at cell (row, column). This cell may depend on other cells, which may in turn depend on others. When a cell changes, any cells depending on it should update accordingly.

numbers is an array of strings representing individual cells like "A1" or cell ranges like "A1:B2".

Example 1:
Excel excel = new Excel(3, 'C');
// construct a 3*3 2D array with all zero.
//   A B C
// 1 0 0 0
// 2 0 0 0
// 3 0 0 0
excel.Set(1, 'A', 2);
excel.Sum(3, 'C', ["A1", "A1:B2"]);
excel.Set(2, 'B', 2);
excel.Get(3, 'C'); // return 4

Constraints:
- 1 <= height <= 26
- 'A' <= width <= 'Z'
- 1 <= row <= height
- 'A' <= column <= width
- -100 <= val <= 100
- 1 <= numbers.length <= 5
- numbers[i] has the format "ColRow" or "ColRow1:ColRow2".
- At most 100 calls will be made to set, get, and sum.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem631 {
    public class Excel {
        private class Cell {
            public int Value { get; set; }
            public Dictionary<(int Row, int Col), int> Formula { get; set; }
        }

        private readonly Cell[,] _sheet;

        // Time: O(H * W)
        // Space: O(H * W)
        public Excel(int height, char width) {
            int cols = width - 'A' + 1;
            _sheet = new Cell[height + 1, cols];
            for (int r = 1; r <= height; r++) {
                for (int c = 0; c < cols; c++) {
                    _sheet[r, c] = new Cell();
                }
            }
        }

        // Time: O(1)
        // Space: O(1)
        public void Set(int row, char column, int val) {
            int col = column - 'A';
            _sheet[row, col].Value = val;
            _sheet[row, col].Formula = null;
        }

        // Time: O(V) where V is the number of cells reachable in dependency graph
        // Space: O(depth) for recursion stack
        public int Get(int row, char column) {
            int col = column - 'A';
            return EvaluateCell(row, col);
        }

        // Time: O(K + V) where K is number of cells in numbers ranges, V is evaluation cost
        // Space: O(K) to store formula
        public int Sum(int row, char column, string[] numbers) {
            int col = column - 'A';
            Dictionary<(int Row, int Col), int> formula = [];

            foreach (string token in numbers) {
                int colonIndex = token.IndexOf(':');
                if (colonIndex == -1) {
                    var cellCoord = ParseCell(token);
                    formula[cellCoord] = formula.GetValueOrDefault(cellCoord, 0) + 1;
                } else {
                    var (r1, c1) = ParseCell(token[..colonIndex]);
                    var (r2, c2) = ParseCell(token[(colonIndex + 1)..]);

                    for (int r = Math.Min(r1, r2); r <= Math.Max(r1, r2); r++) {
                        for (int c = Math.Min(c1, c2); c <= Math.Max(c1, c2); c++) {
                            formula[(r, c)] = formula.GetValueOrDefault((r, c), 0) + 1;
                        }
                    }
                }
            }

            _sheet[row, col].Formula = formula;
            return EvaluateCell(row, col);
        }

        private int EvaluateCell(int row, int col) {
            Cell cell = _sheet[row, col];
            if (cell.Formula == null) {
                return cell.Value;
            }

            int sum = 0;
            foreach (var kvp in cell.Formula) {
                sum += kvp.Value * EvaluateCell(kvp.Key.Row, kvp.Key.Col);
            }

            return sum;
        }

        private static (int Row, int Col) ParseCell(string cellStr) {
            int col = cellStr[0] - 'A';
            int row = int.Parse(cellStr[1..]);
            return (row, col);
        }
    }

    public class Solution {
        public static void Init() {
            Excel excel = new(3, 'C');
            excel.Set(1, 'A', 2);
            Console.WriteLine(excel.Sum(3, 'C', ["A1", "A1:B2"])); // 4
            excel.Set(2, 'B', 2);
            Console.WriteLine(excel.Get(3, 'C')); // 6
        }
    }
}