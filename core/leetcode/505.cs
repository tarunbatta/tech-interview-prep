// Url: https://leetcode.com/problems/the-maze-ii

/*
505. The Maze II
Medium

There is a ball in a maze with empty spaces (represented as 0) and walls (represented as 1). The ball can go through the empty spaces by rolling up, down, left or right, but it won't stop rolling until hitting a wall. When the ball stops, it could choose the next direction.

Given the m x n maze, the ball's start position and the destination, where start = [start_row, start_col] and destination = [destination_row, destination_col], return the shortest distance for the ball to stop at the destination. If the ball cannot stop at destination, return -1.

The distance is the number of empty spaces traveled by the ball from the start position (excluded) to the destination (included).

You may assume that the borders of the maze are all walls.

Example 1:
Input: maze = [[0,0,1,0,0],[0,0,0,0,0],[0,0,0,1,0],[1,1,0,1,1],[0,0,0,0,0]], start = [0,4], destination = [4,4]
Output: 12
Explanation: One possible way is : left -> down -> left -> down -> right -> down -> right.
The length of the path is 1 + 1 + 3 + 1 + 2 + 2 + 2 = 12.

Example 2:
Input: maze = [[0,0,1,0,0],[0,0,0,0,0],[0,0,0,1,0],[1,1,0,1,1],[0,0,0,0,0]], start = [0,4], destination = [3,2]
Output: -1
Explanation: There is no way for the ball to stop at the destination.

Constraints:
m == maze.length
n == maze[i].length
1 <= m, n <= 100
maze[i][j] is 0 or 1.
start.length == 2
destination.length == 2
0 <= start_row, destination_row < m
0 <= start_col, destination_col < n
Both the ball and the destination exist in an empty space, and they will not be in the same position initially.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem505
{
    public class Solution
    {
        public void Init()
        {
            int[][] maze = new int[][]
            {
                new int[] { 0, 0, 1, 0, 0 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 1, 0 },
                new int[] { 1, 1, 0, 1, 1 },
                new int[] { 0, 0, 0, 0, 0 }
            };

            Console.WriteLine(ShortestDistance(maze, new int[] { 0, 4 }, new int[] { 4, 4 })); // 12
            Console.WriteLine(ShortestDistance(maze, new int[] { 0, 4 }, new int[] { 3, 2 })); // -1
        }

        // Time: O(m * n * max(m, n) * log(m * n)) using Dijkstra's algorithm
        // Space: O(m * n) for shortest distance table and priority queue
        public static int ShortestDistance(int[][] maze, int[] start, int[] destination)
        {
            if (maze == null || maze.Length == 0 || maze[0].Length == 0)
            {
                return -1;
            }

            int m = maze.Length;
            int n = maze[0].Length;

            int[,] dist = new int[m, n];
            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    dist[r, c] = int.MaxValue;
                }
            }

            dist[start[0], start[1]] = 0;

            PriorityQueue<(int r, int c), int> pq = new();
            pq.Enqueue((start[0], start[1]), 0);

            int[] dr = { -1, 1, 0, 0 };
            int[] dc = { 0, 0, -1, 1 };

            while (pq.Count > 0)
            {
                var (r, c) = pq.Dequeue();

                if (r == destination[0] && c == destination[1])
                {
                    return dist[r, c];
                }

                for (int i = 0; i < 4; i++)
                {
                    int nr = r;
                    int nc = c;
                    int step = 0;

                    while (nr + dr[i] >= 0 && nr + dr[i] < m &&
                           nc + dc[i] >= 0 && nc + dc[i] < n &&
                           maze[nr + dr[i]][nc + dc[i]] == 0)
                    {
                        nr += dr[i];
                        nc += dc[i];
                        step++;
                    }

                    if (dist[r, c] + step < dist[nr, nc])
                    {
                        dist[nr, nc] = dist[r, c] + step;
                        pq.Enqueue((nr, nc), dist[nr, nc]);
                    }
                }
            }

            int result = dist[destination[0], destination[1]];
            return result == int.MaxValue ? -1 : result;
        }
    }
}