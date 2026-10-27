// Url: https://leetcode.com/problems/the-maze

/*
490. The Maze
Medium

There is a ball in a maze with empty spaces (represented as 0) and walls (represented as 1). The ball can go through the empty spaces by rolling up, down, left or right, but it won't stop rolling until hitting a wall. When the ball stops, it could choose the next direction.

Given the m x n maze, the ball's start position and the destination, where start = [start_row, start_col] and destination = [destination_row, destination_col], return true if the ball can stop at the destination, otherwise return false.

You may assume that the borders of the maze are all walls (see examples).

Example 1:
Input: maze = [[0,0,1,0,0],[0,0,0,0,0],[0,0,0,1,0],[1,1,0,1,1],[0,0,0,0,0]], start = [0,4], destination = [4,4]
Output: true
Explanation: One possible way is : left -> down -> left -> down -> right -> down -> right.

Example 2:
Input: maze = [[0,0,1,0,0],[0,0,0,0,0],[0,0,0,1,0],[1,1,0,1,1],[0,0,0,0,0]], start = [0,4], destination = [3,2]
Output: false
Explanation: There is no way for the ball to stop at the destination. Notice that you can pass through the destination but you cannot stop there.

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
The given maze does not contain border (like the red lines in the example pictures), but you could assume the border of the maze are all walls.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem490
{
    public class Solution
    {
        public static void Init()
        {
            int[][] maze =
            [
                [0, 0, 1, 0, 0],
                [0, 0, 0, 0, 0],
                [0, 0, 0, 1, 0],
                [1, 1, 0, 1, 1],
                [0, 0, 0, 0, 0]
            ];

            Console.WriteLine(HasPath(maze, [0, 4], [4, 4])); // True
            Console.WriteLine(HasPath(maze, [0, 4], [3, 2])); // False
        }

        // Time: O(m * n * max(m, n)) where ball rolls across the grid in each direction until hitting walls
        // Space: O(m * n) for visited states and BFS queue
        public static bool HasPath(int[][] maze, int[] start, int[] destination)
        {
            if (maze == null || maze.Length == 0 || maze[0].Length == 0)
            {
                return false;
            }

            int m = maze.Length;
            int n = maze[0].Length;

            bool[,] visited = new bool[m, n];
            Queue<(int r, int c)> queue = new();

            queue.Enqueue((start[0], start[1]));
            visited[start[0], start[1]] = true;

            int[] dr = [-1, 1, 0, 0];
            int[] dc = [0, 0, -1, 1];

            while (queue.Count > 0)
            {
                var (r, c) = queue.Dequeue();

                if (r == destination[0] && c == destination[1])
                {
                    return true;
                }

                for (int d = 0; d < 4; d++)
                {
                    int nr = r;
                    int nc = c;

                    // Roll until hitting a wall
                    while (nr + dr[d] >= 0 && nr + dr[d] < m &&
                           nc + dc[d] >= 0 && nc + dc[d] < n &&
                           maze[nr + dr[d]][nc + dc[d]] == 0)
                    {
                        nr += dr[d];
                        nc += dc[d];
                    }

                    if (!visited[nr, nc])
                    {
                        visited[nr, nc] = true;
                        queue.Enqueue((nr, nc));
                    }
                }
            }

            return false;
        }
    }
}