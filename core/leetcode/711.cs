// Url:https://leetcode.com/problems/number-of-distinct-islands-ii

/*
711. Number of Distinct Islands II
Hard

You are given an m x n binary matrix grid. An island is a group of 1's (representing land) connected 4-directionally (horizontal or vertical.) You may assume all four edges of the grid are surrounded by water.

An island is considered to be the same as another if and only if one island can be translated, rotated (by 90, 180, or 270 degrees), or reflected (horizontally or vertically) to equal the other.

Return the number of distinct islands.

Example 1:
Input: grid = [[1,1,0,0,0],[1,0,0,0,0],[0,0,0,0,1],[0,0,0,1,1]]
Output: 1
Explanation: The two islands are considered the same because if we make a 180 degrees clockwise rotation on the first island, then a vertical reflection, it will equal the second island.

Example 2:
Input: grid = [[1,1,1,0,0],[1,0,0,0,1],[0,1,0,0,1],[0,1,1,1,0]]
Output: 2
Explanation: The two islands are different.

Constraints:
- m == grid.length
- n == grid[i].length
- 1 <= m, n <= 50
- grid[i][j] is either 0 or 1.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem711 {
    public class Solution {
        public void Init() {
            int[][] grid1 = [
                [1, 1, 0, 0, 0],
                [1, 0, 0, 0, 0],
                [0, 0, 0, 0, 1],
                [0, 0, 0, 1, 1]
            ];
            Console.WriteLine(NumDistinctIslands2(grid1)); // 1

            int[][] grid2 = [
                [1, 1, 1, 0, 0],
                [1, 0, 0, 0, 1],
                [0, 1, 0, 0, 1],
                [0, 1, 1, 1, 0]
            ];
            Console.WriteLine(NumDistinctIslands2(grid2)); // 2
        }

        // Time: O(m * n * log(m * n))
        // Space: O(m * n)
        public int NumDistinctIslands2(int[][] grid) {
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
                        List<(int R, int C)> cells = [];
                        Dfs(grid, r, c, visited, cells);
                        uniqueIslands.Add(GetCanonicalShape(cells));
                    }
                }
            }

            return uniqueIslands.Count;
        }

        private static void Dfs(int[][] grid, int r, int c, bool[][] visited, List<(int R, int C)> cells) {
            visited[r][c] = true;
            cells.Add((r, c));

            int[] dr = [-1, 1, 0, 0];
            int[] dc = [0, 0, -1, 1];

            for (int d = 0; d < 4; d++) {
                int nr = r + dr[d];
                int nc = c + dc[d];

                if (nr >= 0 && nr < grid.Length && nc >= 0 && nc < grid[0].Length &&
                    grid[nr][nc] == 1 && !visited[nr][nc]) {
                    Dfs(grid, nr, nc, visited, cells);
                }
            }
        }

        private static string GetCanonicalShape(List<(int R, int C)> cells) {
            string canonical = null;

            for (int rot = 0; rot < 4; rot++) {
                for (int refl = 0; refl < 2; refl++) {
                    List<(int R, int C)> transformed = [];

                    foreach (var (r, c) in cells) {
                        int x = r;
                        int y = c;

                        // Rotations: (x, y) -> (y, -x) -> (-x, -y) -> (-y, x)
                        for (int k = 0; k < rot; k++) {
                            int temp = x;
                            x = y;
                            y = -temp;
                        }

                        // Reflection: flip across x-axis
                        if (refl == 1) {
                            x = -x;
                        }

                        transformed.Add((x, y));
                    }

                    transformed.Sort((a, b) => a.R != b.R ? a.R.CompareTo(b.R) : a.C.CompareTo(b.C));

                    int baseR = transformed[0].R;
                    int baseC = transformed[0].C;

                    StringBuilder sb = new();
                    foreach (var pt in transformed) {
                        sb.Append($"{pt.R - baseR},{pt.C - baseC};");
                    }

                    string shapeStr = sb.ToString();
                    if (canonical == null || string.CompareOrdinal(shapeStr, canonical) < 0) {
                        canonical = shapeStr;
                    }
                }
            }

            return canonical!;
        }
    }
}