// Url: https://leetcode.com/problems/shortest-distance-from-all-buildings

/*
317. Shortest Distance from All Buildings
Hard

You are given an m x n grid grid of values 0, 1, or 2, where:
- each 0 marks an empty land that you can pass by freely,
- each 1 marks a building that you cannot pass through, and
- each 2 marks an obstacle that you cannot pass through.

You want to build a single house on an empty land that reaches all buildings in the shortest total travel distance. You can only move up, down, left, and right.

Return the minimum total travel distance to build such a house. If it is not possible to build such a house according to the above rules, return -1.

Example 1:
Input: grid = [[1,0,2,0,1],[0,0,0,0,0],[0,0,1,0,0]]
Output: 7
Explanation: Given three buildings at (0,0), (0,4), (2,2), and an obstacle at (0,2).
The point (1,2) is an ideal empty land to build a house, as the total travel distance of 3+3+1=7 is minimal.
So return 7.

Example 2:
Input: grid = [[1,0]]
Output: 1

Example 3:
Input: grid = [[1]]
Output: -1

Constraints:
m == grid.length
n == grid[i].length
1 <= m, n <= 50
grid[i][j] is either 0, 1, or 2.
There will be at least one building in the grid.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem317
{
    public class Solution
    {
        public static void Init()
        {
            int[][] grid1 =
            [
                [1, 0, 2, 0, 1],
                [0, 0, 0, 0, 0],
                [0, 0, 1, 0, 0]
            ];
            Console.WriteLine(ShortestDistance(grid1)); // 7

            int[][] grid2 =
            [
                [1, 0]
            ];
            Console.WriteLine(ShortestDistance(grid2)); // 1
        }

        // Time: O(B * m * n) where B is the number of buildings, and m, n are grid dimensions
        // Space: O(m * n) for distances and reach count matrices
        public static int ShortestDistance(int[][] grid)
        {
            if (grid == null || grid.Length == 0 || grid[0].Length == 0)
            {
                return -1;
            }

            int m = grid.Length;
            int n = grid[0].Length;

            int[,] totalDistance = new int[m, n];
            int[,] reachCount = new int[m, n];
            int totalBuildings = 0;

            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (grid[r][c] == 1)
                    {
                        totalBuildings++;
                        Bfs(grid, r, c, totalDistance, reachCount, m, n);
                    }
                }
            }

            int minDistance = int.MaxValue;

            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (grid[r][c] == 0 && reachCount[r, c] == totalBuildings)
                    {
                        minDistance = Math.Min(minDistance, totalDistance[r, c]);
                    }
                }
            }

            return minDistance == int.MaxValue ? -1 : minDistance;
        }

        private static void Bfs(int[][] grid, int startR, int startC, int[,] totalDistance, int[,] reachCount, int m, int n)
        {
            Queue<(int r, int c)> queue = new();
            bool[,] visited = new bool[m, n];

            queue.Enqueue((startR, startC));
            visited[startR, startC] = true;

            int distance = 0;
            int[] dr = [-1, 1, 0, 0];
            int[] dc = [0, 0, -1, 1];

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;
                distance++;

                for (int i = 0; i < levelSize; i++)
                {
                    var (r, c) = queue.Dequeue();

                    for (int d = 0; d < 4; d++)
                    {
                        int nr = r + dr[d];
                        int nc = c + dc[d];

                        if (nr >= 0 && nr < m && nc >= 0 && nc < n && !visited[nr, nc] && grid[nr][nc] == 0)
                        {
                            visited[nr, nc] = true;
                            totalDistance[nr, nc] += distance;
                            reachCount[nr, nc]++;
                            queue.Enqueue((nr, nc));
                        }
                    }
                }
            }
        }
    }
}