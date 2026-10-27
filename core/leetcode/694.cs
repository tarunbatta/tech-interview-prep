// Url:https://leetcode.com/problems/number-of-distinct-islands

/*
694. Number of Distinct Islands
Medium

You are given an m x n binary matrix grid. An island is a group of 1's (representing land) connected 4-directionally (horizontal or vertical.) You may assume all four edges of the grid are surrounded by water.

An island is considered to be the same as another if and only if one island can be translated (and not rotated or reflected) to equal the other.

Return the number of distinct islands.

Example 1:
Input: grid = [[1,1,0,0,0],[1,1,0,0,0],[0,0,0,1,1],[0,0,0,1,1]]
Output: 1

Example 2:
Input: grid = [[1,1,0,1,1],[1,0,0,0,0],[0,0,0,0,1],[1,1,0,1,1]]
Output: 3

Constraints:
- m == grid.length
- n == grid[i].length
- 1 <= m, n <= 50
- grid[i][j] is either 0 or 1.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem694 {
    public class Solution {
        public void Init() {
            int[][] grid1 = [
                [1, 1, 0, 0, 0],
                [1, 1, 0, 0, 0],
                [0, 0, 0, 1, 1],
                [0, 0, 0, 1, 1]
            ];
            Console.WriteLine(NumDistinctIslands(grid1)); // 1

            int[][] grid2 = [
                [1, 1, 0, 1, 1],
                [1, 0, 0, 0, 0],
                [0, 0, 0, 0, 1],
                [1, 1, 0, 1, 1]
            ];
            Console.WriteLine(NumDistinctIslands(grid2)); // 3
        }

        // Time: O(m * n) where m, n are grid dimensions
        // Space: O(m * n) for visited matrix and distinct shapes
        public int NumDistinctIslands(int[][] grid) {
            int m = grid.Length;
            int n = grid[0].Length;
            bool[][] visited = new bool[m][];
            for (int i = 0; i < m; i++) {
                visited[i] = new bool[n];
            }

            HashSet<string> uniqueIslands = [];

            for (int r = 0; r < m; r++) {
                for (int c = 0; c < n; c++) {
                    if (grid[r][c] == 1 && !visited[r][c]) {
                        StringBuilder sb = new();
                        Dfs(grid, r, c, r, c, visited, sb);
                        uniqueIslands.Add(sb.ToString());
                    }
                }
            }

            return uniqueIslands.Count;
        }

        private static void Dfs(int[][] grid, int r, int c, int r0, int c0, bool[][] visited, StringBuilder sb) {
            visited[r][c] = true;
            sb.Append($"{r - r0},{c - c0};");

            int[] dr = [-1, 1, 0, 0];
            int[] dc = [0, 0, -1, 1];

            for (int d = 0; d < 4; d++) {
                int nr = r + dr[d];
                int nc = c + dc[d];

                if (nr >= 0 && nr < grid.Length && nc >= 0 && nc < grid[0].Length &&
                    grid[nr][nc] == 1 && !visited[nr][nc]) {
                    Dfs(grid, nr, nc, r0, c0, visited, sb);
                }
            }
        }
    }
}