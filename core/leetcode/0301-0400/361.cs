// Url: https://leetcode.com/problems/bomb-enemy

/*
361. Bomb Enemy
Medium

Given an m x n matrix grid where each cell is either a wall 'W', an enemy 'E' or empty '0', return the maximum enemies you can kill using one bomb. You can only place the bomb in an empty cell.

The bomb kills all the enemies in the same row and column from the planted position until it hits the wall since it is too strong to be destroyed.

Example 1:
Input: grid = [["0","E","0","0"],["E","0","W","E"],["0","E","0","0"]]
Output: 3
Explanation: Placing a bomb at (1,1) kills 3 enemies.

Example 2:
Input: grid = [["W","W","W"],["0","0","0"],["E","E","E"]]
Output: 1

Constraints:
m == grid.length
n == grid[i].length
1 <= m, n <= 500
grid[i][j] is either 'W', 'E', or '0'.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem361
{
    public class Solution
    {
        public static void Init()
        {
            char[][] grid1 =
            [
                ['0', 'E', '0', '0'],
                ['E', '0', 'W', 'E'],
                ['0', 'E', '0', '0']
            ];
            Console.WriteLine(MaxKilledEnemies(grid1)); // 3

            char[][] grid2 =
            [
                ['W', 'W', 'W'],
                ['0', '0', '0'],
                ['E', 'E', 'E']
            ];
            Console.WriteLine(MaxKilledEnemies(grid2)); // 1
        }

        // Time: O(m * n) where each cell is checked at most twice
        // Space: O(n) for column enemies cache
        public static int MaxKilledEnemies(char[][] grid)
        {
            if (grid == null || grid.Length == 0 || grid[0].Length == 0)
            {
                return 0;
            }

            int m = grid.Length;
            int n = grid[0].Length;

            int maxEnemies = 0;
            int rowHits = 0;
            int[] colHits = new int[n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // Recalculate row hits if starting a row or immediately after a wall
                    if (j == 0 || grid[i][j - 1] == 'W')
                    {
                        rowHits = 0;
                        for (int k = j; k < n && grid[i][k] != 'W'; k++)
                        {
                            if (grid[i][k] == 'E')
                            {
                                rowHits++;
                            }
                        }
                    }

                    // Recalculate column hits if starting a column or immediately after a wall
                    if (i == 0 || grid[i - 1][j] == 'W')
                    {
                        colHits[j] = 0;
                        for (int k = i; k < m && grid[k][j] != 'W'; k++)
                        {
                            if (grid[k][j] == 'E')
                            {
                                colHits[j]++;
                            }
                        }
                    }

                    if (grid[i][j] == '0')
                    {
                        maxEnemies = Math.Max(maxEnemies, rowHits + colHits[j]);
                    }
                }
            }

            return maxEnemies;
        }
    }
}