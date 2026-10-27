// Url: https://leetcode.com/problems/minimum-moves-to-spread-stones-over-grid/

/*
2850. Minimum Moves to Spread Stones Over Grid
Medium

You are given a 0-indexed 2D integer matrix grid of size 3 x 3, representing the number of stones in each cell.
The grid contains exactly 9 stones in total between all cells.

A move consists of moving a single stone from its current cell to any other cell if the two cells share a side.

Return the minimum number of moves required to place one stone in each cell.

Example 1:
Input: grid = [[1,1,0],[1,1,1],[1,2,1]]
Output: 3
Explanation: One possible sequence of moves to place one stone in each cell is : 
1- Move one stone from cell (2,1) to cell (2,2).
2- Move one stone from cell (2,2) to cell (1,2).
3- Move one stone from cell (1,2) to cell (0,2).
In total, 3 moves were used to place one stone in each cell.
It can be shown that 3 is the minimum number of moves required to place one stone in each cell.

Example 2:
Input: grid = [[1,3,0],[1,0,0],[1,0,3]]
Output: 4
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem2850
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(MinimumMoves([
                [1, 1, 0],
                [1, 1, 1],
                [1, 2, 1]
            ])); // Expected: 3

            Console.WriteLine(MinimumMoves([
                [1, 3, 0],
                [1, 0, 0],
                [1, 0, 3]
            ])); // Expected: 4

            Console.WriteLine(MinimumMoves([
                [1, 1, 1],
                [1, 1, 1],
                [1, 1, 1]
            ])); // Expected: 0
        }

        // Time: O(Z!) where Z <= 8 is the number of zero cells (at most 8!)
        // Space: O(Z) for zero coordinates list and recursion call stack
        public static int MinimumMoves(int[][] grid)
        {
            if (grid == null || grid.Length != 3 || grid[0].Length != 3)
            {
                return 0;
            }

            List<(int r, int c)> zeros = [];

            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (grid[r][c] == 0)
                    {
                        zeros.Add((r, c));
                    }
                }
            }

            if (zeros.Count == 0)
            {
                return 0;
            }

            return Dfs(grid, zeros, 0);
        }

        private static int Dfs(int[][] grid, List<(int r, int c)> zeros, int index)
        {
            if (index == zeros.Count)
            {
                return 0;
            }

            int minMoves = int.MaxValue;
            var (zr, zc) = zeros[index];

            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (grid[r][c] > 1)
                    {
                        grid[r][c]--;
                        int dist = Math.Abs(zr - r) + Math.Abs(zc - c);
                        minMoves = Math.Min(minMoves, dist + Dfs(grid, zeros, index + 1));
                        grid[r][c]++;
                    }
                }
            }

            return minMoves;
        }
    }
}
