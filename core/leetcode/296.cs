// Url: https://leetcode.com/problems/best-meeting-point

/*
296. Best Meeting Point
Hard

Given an m x n binary grid grid where each 1 marks the home of one friend, return the minimal total Manhattan distance to a meeting point.

The Manhattan Distance between two points (p1, p2) and (q1, q2) is |p1 - q1| + |p2 - q2|.

Example 1:
Input: grid = [[1,0,0,0,1],[0,0,0,0,0],[0,0,1,0,0]]
Output: 6
Explanation: Given three friends living at (0,0), (0,4), and (2,2):
The point (0,2) is an ideal meeting point, as the total travel distance of 2 + 2 + 2 = 6 is minimal.
So return 6.

Example 2:
Input: grid = [[1,1]]
Output: 1

Constraints:
m == grid.length
n == grid[i].length
1 <= m, n <= 200
grid[i][j] is either 0 or 1.
There will be at least two friends in the grid.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem296
{
    public class Solution
    {
        public void Init()
        {
            int[][] grid1 = new int[][]
            {
                new int[] { 1, 0, 0, 0, 1 },
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 1, 0, 0 }
            };
            Console.WriteLine(MinTotalDistance(grid1)); // 6

            int[][] grid2 = new int[][]
            {
                new int[] { 1, 1 }
            };
            Console.WriteLine(MinTotalDistance(grid2)); // 1
        }

        // Time: O(m * n) where m is rows and n is columns
        // Space: O(k) where k is the number of friends in the grid
        public static int MinTotalDistance(int[][] grid)
        {
            if (grid == null || grid.Length == 0 || grid[0].Length == 0)
            {
                return 0;
            }

            int m = grid.Length;
            int n = grid[0].Length;

            // Collect rows in ascending order
            List<int> rows = new();
            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (grid[r][c] == 1)
                    {
                        rows.Add(r);
                    }
                }
            }

            // Collect columns in ascending order
            List<int> cols = new();
            for (int c = 0; c < n; c++)
            {
                for (int r = 0; r < m; r++)
                {
                    if (grid[r][c] == 1)
                    {
                        cols.Add(c);
                    }
                }
            }

            return Get1DDistance(rows) + Get1DDistance(cols);
        }

        private static int Get1DDistance(List<int> points)
        {
            int distance = 0;
            int left = 0;
            int right = points.Count - 1;

            while (left < right)
            {
                distance += points[right] - points[left];
                left++;
                right--;
            }

            return distance;
        }
    }
}