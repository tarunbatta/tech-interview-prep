// Url: https://leetcode.com/problems/path-with-maximum-gold/

/*
1219. Path with Maximum Gold
Medium

In a gold mine grid of size m x n, each cell in this mine has an integer representing the amount of gold in that cell, 0 if it is empty.

Return the maximum amount of gold you can collect under the conditions:
- Every time you are located in a cell you will collect all the gold in that cell.
- From your position, you can walk one step to the left, right, up, or down.
- You can't visit the same cell more than once.
- Never visit a cell with 0 gold.
- You can start and stop collecting gold from any position in the grid that has some gold.

Example 1:
Input: grid = [[0,6,0],[5,8,7],[0,9,0]]
Output: 24
Explanation:
[[0,6,0],
 [5,8,7],
 [0,9,0]]
Path to get the maximum gold, 9 -> 8 -> 7.

Example 2:
Input: grid = [[1,0,7],[2,0,6],[3,4,5],[0,3,0],[9,0,20]]
Output: 28
Explanation:
[[1,0,7],
 [2,0,6],
 [3,4,5],
 [0,3,0],
 [9,0,20]]
Path to get the maximum gold, 1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1219
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(GetMaximumGold(null)); // Expected: 0
            Console.WriteLine(GetMaximumGold([
                [0, 6, 0],
                [5, 8, 7],
                [0, 9, 0]
            ])); // Expected: 24
            Console.WriteLine(GetMaximumGold([
                [1, 0, 7],
                [2, 0, 6],
                [3, 4, 5],
                [0, 3, 0],
                [9, 0, 20]
            ])); // Expected: 28
        }

        // Time: O(K * 3^K) where K is the number of cells with gold (K <= 25)
        // Space: O(K) recursion call stack depth
        public static int GetMaximumGold(int[][] grid)
        {
            if (grid == null || grid.Length == 0 || grid[0].Length == 0)
            {
                return 0;
            }

            int maxGold = 0;
            int rows = grid.Length;
            int cols = grid[0].Length;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (grid[r][c] > 0)
                    {
                        maxGold = Math.Max(maxGold, Dfs(grid, r, c));
                    }
                }
            }

            return maxGold;
        }

        private static int Dfs(int[][] grid, int r, int c)
        {
            int gold = grid[r][c];
            grid[r][c] = 0; // Temporarily mark as visited without extra memory

            int maxNext = 0;
            int[] dr = [0, 1, 0, -1];
            int[] dc = [1, 0, -1, 0];

            for (int d = 0; d < 4; d++)
            {
                int nr = r + dr[d];
                int nc = c + dc[d];

                if (nr >= 0 && nr < grid.Length && nc >= 0 && nc < grid[0].Length && grid[nr][nc] > 0)
                {
                    maxNext = Math.Max(maxNext, Dfs(grid, nr, nc));
                }
            }

            grid[r][c] = gold; // Backtrack / restore
            return gold + maxNext;
        }
    }
}
