// Url:https://leetcode.com/problems/path-with-maximum-minimum-value

/*
1102. Path With Maximum Minimum Value
Medium

Given an m x n integer matrix grid, return the maximum score of a path from (0, 0) to (m - 1, n - 1).

The score of a path is the minimum value in that path.
- For example, the value of the path 8 -> 4 -> 5 -> 9 is 4.

A path moves some number of times from one cell to an adjacent cell in any of the 4 cardinal directions, and cannot visit the same cell more than once.

Example 1:
Input: grid = [[5,4,5],[1,2,6],[7,4,6]]
Output: 4
Explanation: The path with the maximum score is: [0,0] -> [0,1] -> [0,2] -> [1,2] -> [2,2].

Example 2:
Input: grid = [[2,2,1,2,2,2],[1,2,2,2,1,2]]
Output: 2

Example 3:
Input: grid = [[3,4,6,3,4],[0,2,1,1,7],[8,8,3,2,7],[3,2,4,9,8],[4,1,2,0,0],[4,6,5,4,3]]
Output: 3

Constraints:
- m == grid.length
- n == grid[i].length
- 1 <= m, n <= 100
- 0 <= grid[i][j] <= 10^9
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem1102 {
    public class Solution {
        public void Init() {
            int[][] g1 = [
                [5, 4, 5],
                [1, 2, 6],
                [7, 4, 6]
            ];
            Console.WriteLine(MaximumMinimumPath(g1)); // 4

            int[][] g2 = [
                [2, 2, 1, 2, 2, 2],
                [1, 2, 2, 2, 1, 2]
            ];
            Console.WriteLine(MaximumMinimumPath(g2)); // 2
        }

        // Time: O(m * n * log(m * n))
        // Space: O(m * n)
        public int MaximumMinimumPath(int[][] grid) {
            int m = grid.Length;
            int n = grid[0].Length;

            PriorityQueue<(int R, int C, int Score), int> pq = new();
            bool[,] visited = new bool[m, n];

            pq.Enqueue((0, 0, grid[0][0]), -grid[0][0]);
            visited[0, 0] = true;

            int[] dr = [-1, 1, 0, 0];
            int[] dc = [0, 0, -1, 1];

            while (pq.Count > 0) {
                var (r, c, score) = pq.Dequeue();

                if (r == m - 1 && c == n - 1) {
                    return score;
                }

                for (int d = 0; d < 4; d++) {
                    int nr = r + dr[d];
                    int nc = c + dc[d];

                    if (nr >= 0 && nr < m && nc >= 0 && nc < n && !visited[nr, nc]) {
                        visited[nr, nc] = true;
                        int nextScore = Math.Min(score, grid[nr][nc]);
                        pq.Enqueue((nr, nc, nextScore), -nextScore);
                    }
                }
            }

            return 0;
        }
    }
}