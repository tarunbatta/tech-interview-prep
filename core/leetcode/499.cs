// Url: https://leetcode.com/problems/the-maze-iii

/*
499. The Maze III
Hard

There is a ball in a maze with empty spaces (represented as 0) and walls (represented as 1). The ball can go through the empty spaces by rolling up, down, left or right, but it won't stop rolling until hitting a wall. When the ball stops, it could choose the next direction. There is also a hole in this maze. The ball will drop into the hole if it rolls onto the hole.

Given the m x n maze, the ball's start position and the hole's position, where ball = [ball_row, ball_col] and hole = [hole_row, hole_col], return a string of instructions of all the instructions that the ball should follow to drop into the hole with the shortest distance possible. If there are multiple valid instructions, return the lexicographically smallest one. If the ball can't drop into the hole, return "impossible".

The distance is the number of empty spaces traveled by the ball from the start position (excluded) to the destination (included).

You can assume that the borders of the maze are all walls. The instructions are composed of 'u', 'd', 'l', and 'r'.

Example 1:
Input: maze = [[0,0,0,0,0],[1,1,0,0,1],[0,0,0,0,0],[0,1,0,0,1],[0,1,0,0,0]], ball = [4,3], hole = [0,1]
Output: "lul"
Explanation: There are two shortest ways for the ball to drop into the hole.
The first way is "lul": left -> up -> left.
The second way is "lul": up -> left.
Both have distance 8, but "lul" is lexicographically smaller than "ul".

Example 2:
Input: maze = [[0,0,0,0,0],[1,1,0,0,1],[0,0,0,0,0],[0,1,0,0,1],[0,1,0,0,0]], ball = [4,3], hole = [3,0]
Output: "impossible"
Explanation: The ball cannot reach the hole.

Constraints:
m == maze.length
n == maze[i].length
1 <= m, n <= 100
maze[i][j] is 0 or 1.
ball.length == 2
hole.length == 2
0 <= ball_row, hole_row < m
0 <= ball_col, hole_col < n
Both the ball and the hole exist in an empty space, and they will not be in the same position initially.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem499
{
    public class Solution
    {
        public static void Init()
        {
            int[][] maze =
            [
                [0, 0, 0, 0, 0],
                [1, 1, 0, 0, 1],
                [0, 0, 0, 0, 0],
                [0, 1, 0, 0, 1],
                [0, 1, 0, 0, 0]
            ];

            Console.WriteLine(FindShortestWay(maze, [4, 3], [0, 1])); // lul
            Console.WriteLine(FindShortestWay(maze, [4, 3], [3, 0])); // impossible
        }

        // Time: O(m * n * max(m, n) * log(m * n)) using Dijkstra with priority queue
        // Space: O(m * n) for shortest distances and instruction path records
        public static string FindShortestWay(int[][] maze, int[] ball, int[] hole)
        {
            if (maze == null || maze.Length == 0 || maze[0].Length == 0)
            {
                return "impossible";
            }

            int m = maze.Length;
            int n = maze[0].Length;

            int[,] dist = new int[m, n];
            string[,] path = new string[m, n];

            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    dist[r, c] = int.MaxValue;
                    path[r, c] = string.Empty;
                }
            }

            dist[ball[0], ball[1]] = 0;

            // Directions in lexicographical order: 'd', 'l', 'r', 'u'
            int[] dr = [1, 0, 0, -1];
            int[] dc = [0, -1, 1, 0];
            char[] dirChars = ['d', 'l', 'r', 'u'];

            PriorityQueue<(int r, int c, int d, string p), (int dist, string path)> pq =
                new(Comparer<(int dist, string path)>.Create((a, b) =>
                {
                    if (a.dist != b.dist)
                    {
                        return a.dist.CompareTo(b.dist);
                    }
                    return string.CompareOrdinal(a.path, b.path);
                }));

            pq.Enqueue((ball[0], ball[1], 0, string.Empty), (0, string.Empty));

            while (pq.Count > 0)
            {
                var (r, c, d, p) = pq.Dequeue();

                if (d > dist[r, c] || (d == dist[r, c] && string.CompareOrdinal(p, path[r, c]) > 0))
                {
                    continue;
                }

                if (r == hole[0] && c == hole[1])
                {
                    return p;
                }

                for (int i = 0; i < 4; i++)
                {
                    int nr = r;
                    int nc = c;
                    int step = 0;

                    // Roll until wall or hole
                    while (nr + dr[i] >= 0 && nr + dr[i] < m &&
                           nc + dc[i] >= 0 && nc + dc[i] < n &&
                           maze[nr + dr[i]][nc + dc[i]] == 0)
                    {
                        nr += dr[i];
                        nc += dc[i];
                        step++;

                        if (nr == hole[0] && nc == hole[1])
                        {
                            break;
                        }
                    }

                    int newDist = d + step;
                    string newPath = p + dirChars[i];

                    if (newDist < dist[nr, nc] || (newDist == dist[nr, nc] && string.CompareOrdinal(newPath, path[nr, nc]) < 0))
                    {
                        dist[nr, nc] = newDist;
                        path[nr, nc] = newPath;
                        pq.Enqueue((nr, nc, newDist, newPath), (newDist, newPath));
                    }
                }
            }

            return "impossible";
        }
    }
}