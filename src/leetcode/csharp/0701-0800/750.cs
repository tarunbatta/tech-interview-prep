// Url:https://leetcode.com/problems/number-of-corner-rectangles

/*
750. Number Of Corner Rectangles
Medium

Given an m x n integer matrix grid where each entry is only 0 or 1, return the number of corner rectangles.

A corner rectangle consists of four distinct 1's on the grid that form an axis-aligned rectangle. Only the four corners need to have the value 1. Also, all four corners must be distinct.

Example 1:
Input: grid = [[1,0,0,1,0],[0,0,1,0,1],[0,0,0,1,0],[1,0,1,0,1]]
Output: 1
Explanation: There is only one corner rectangle, with corners grid[1][2], grid[1][4], grid[3][2], grid[3][4].

Example 2:
Input: grid = [[1,1,1],[1,1,1],[1,1,1]]
Output: 9
Explanation: There are four 2x2 rectangles, four 2x3 and 3x2 rectangles, and one 3x3 rectangle.

Example 3:
Input: grid = [[1,1,1,1]]
Output: 0
Explanation: Rectangles must have four distinct corners.

Constraints:
- m == grid.length
- n == grid[i].length
- 1 <= m, n <= 200
- grid[i][j] is either 0 or 1.
- The number of 1's in the grid is in the range [1, 6000].
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem750 {
    public class Solution {
        public static void Init() {
            int[][] grid1 = [
                [1, 0, 0, 1, 0],
                [0, 0, 1, 0, 1],
                [0, 0, 0, 1, 0],
                [1, 0, 1, 0, 1]
            ];
            Console.WriteLine(CountCornerRectangles(grid1)); // 1

            int[][] grid2 = [
                [1, 1, 1],
                [1, 1, 1],
                [1, 1, 1]
            ];
            Console.WriteLine(CountCornerRectangles(grid2)); // 9
        }

        // Time: O(m^2 * n) where m is rows count and n is columns count
        // Space: O(1)
        public static int CountCornerRectangles(int[][] grid) {
            int m = grid.Length;
            int n = grid[0].Length;
            int total = 0;

            for (int r1 = 0; r1 < m - 1; r1++) {
                for (int r2 = r1 + 1; r2 < m; r2++) {
                    int count = 0;
                    for (int c = 0; c < n; c++) {
                        if (grid[r1][c] == 1 && grid[r2][c] == 1) {
                            count++;
                        }
                    }
                    if (count > 1) {
                        total += count * (count - 1) / 2;
                    }
                }
            }

            return total;
        }
    }
}