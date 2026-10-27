// Url:https://leetcode.com/problems/number-of-islands

/*
200. Number of Islands
Medium

Given a 2d grid map of '1's (land) and '0's (water), count the number of islands. An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically. You may assume all four edges of the grid are all surrounded by water.

Example 1:

Input:
11110
11010
11000
00000

Output: 1


Example 2:

Input:
11000
11000
00100
00011

Output: 3
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem200 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (m*n)
        // Space: O (min(m,n))
        public static int NumIslands (char[][] grid) {
            int count = 0;

            if (grid == null || grid.GetUpperBound (0) < 0 || grid[0] == null) {
                return count;
            }

            int m = grid.Length;
            int n = grid[0].Length;

            if (m == 0 || n == 0) {
                return count;
            }

            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    if (grid[i][j] == '1') {
                        count++;

                        Queue<Point> q = new Queue<Point> ();
                        q.Enqueue (new Point (i, j));
                        grid[i][j] = '0';

                        while (q.Count != 0) {
                            Point p = q.Dequeue ();

                            if (p.x - 1 >= 0 && grid[p.x - 1][p.y] == '1') {
                                q.Enqueue (new Point (p.x - 1, p.y));
                                grid[p.x - 1][p.y] = '0';
                            }

                            if (p.x + 1 < m && grid[p.x + 1][p.y] == '1') {
                                q.Enqueue (new Point (p.x + 1, p.y));
                                grid[p.x + 1][p.y] = '0';
                            }

                            if (p.y - 1 >= 0 && grid[p.x][p.y - 1] == '1') {
                                q.Enqueue (new Point (p.x, p.y - 1));
                                grid[p.x][p.y - 1] = '0';
                            }

                            if (p.y + 1 < n && grid[p.x][p.y + 1] == '1') {
                                q.Enqueue (new Point (p.x, p.y + 1));
                                grid[p.x][p.y + 1] = '0';
                            }
                        }
                    }
                }
            }

            return count;
        }
    }

    public class Point {
        public int x;
        public int y;

        public Point (int _x, int _y) {
            x = _x;
            y = _y;
        }
    }
}