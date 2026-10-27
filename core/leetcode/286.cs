// Url: https://leetcode.com/problems/walls-and-gates

/*
286. Walls and Gates
Medium

You are given an m x n grid rooms initialized with these three possible values:
- -1: A wall or an obstacle.
- 0: A gate.
- INF: Infinity means an empty room. We use the value 2^31 - 1 = 2147483647 to represent INF as you may assume that the distance to a gate is less than 2147483647.

Fill each empty room with the distance to its nearest gate. If it is impossible to reach a gate, it should be filled with INF.

Example 1:
Input: rooms = [[2147483647,-1,0,2147483647],[2147483647,2147483647,2147483647,-1],[2147483647,-1,2147483647,-1],[0,-1,2147483647,2147483647]]
Output: [[3,-1,0,1],[2,2,1,-1],[1,-1,2,-1],[0,-1,3,4]]

Example 2:
Input: rooms = [[-1]]
Output: [[-1]]

Constraints:
m == rooms.length
n == rooms[i].length
1 <= m, n <= 250
rooms[i][j] is -1, 0, or 2^31 - 1.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem286
{
    public class Solution
    {
        public const int INF = 2147483647;

        public static void Init()
        {
            int[][] rooms =
            [
                [INF, -1, 0, INF],
                [INF, INF, INF, -1],
                [INF, -1, INF, -1],
                [0, -1, INF, INF]
            ];

            WallsAndGates(rooms);

            for (int r = 0; r < rooms.Length; r++)
            {
                Console.WriteLine(string.Join(", ", rooms[r]));
            }
        }

        // Time: O(m * n) where m is rows and n is columns
        // Space: O(m * n) for the multi-source BFS queue
        public static void WallsAndGates(int[][] rooms)
        {
            if (rooms == null || rooms.Length == 0 || rooms[0].Length == 0)
            {
                return;
            }

            int m = rooms.Length;
            int n = rooms[0].Length;
            Queue<(int r, int c)> queue = new();

            // Enqueue all gates as starting points
            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (rooms[r][c] == 0)
                    {
                        queue.Enqueue((r, c));
                    }
                }
            }

            int[] dr = [-1, 1, 0, 0];
            int[] dc = [0, 0, -1, 1];

            while (queue.Count > 0)
            {
                var (r, c) = queue.Dequeue();

                for (int d = 0; d < 4; d++)
                {
                    int nr = r + dr[d];
                    int nc = c + dc[d];

                    if (nr >= 0 && nr < m && nc >= 0 && nc < n && rooms[nr][nc] == INF)
                    {
                        rooms[nr][nc] = rooms[r][c] + 1;
                        queue.Enqueue((nr, nc));
                    }
                }
            }
        }
    }
}