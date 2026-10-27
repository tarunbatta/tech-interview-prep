// Url: https://leetcode.com/problems/number-of-spaces-cleaning-robot-cleaned/

/*
2061. Number of Spaces Cleaning Robot Cleaned
Medium

A room is represented by a 0-indexed 2D binary matrix room where:
- 0 represents an empty space.
- 1 represents an obstacle.

The robot starts at the top-left corner room[0][0] facing right.
The robot will clean every space it visits.
It moves forward until it hits an obstacle or the wall, then turns 90 degrees clockwise and continues.
If it turns 4 times in place without moving, it stops.

Return the number of clean spaces the robot visits.

Example 1:
Input: room = [
  [0,0,0],
  [1,1,0],
  [0,0,0]
]
Output: 7

Example 2:
Input: room = [
  [0,1,0],
  [1,0,0],
  [0,0,0]
]
Output: 1
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem2061
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(NumberOfCleanRooms(null)); // Expected: 0
            Console.WriteLine(NumberOfCleanRooms([
                [0, 0, 0],
                [1, 1, 0],
                [0, 0, 0]
            ])); // Expected: 7
            Console.WriteLine(NumberOfCleanRooms([
                [0, 1, 0],
                [1, 0, 0],
                [0, 0, 0]
            ])); // Expected: 1
        }

        // Time: O(R * C) since there are R * C * 4 possible (r, c, dir) states
        // Space: O(R * C) to store visited states and cleaned cells
        public static int NumberOfCleanRooms(int[][] room)
        {
            if (room == null || room.Length == 0 || room[0].Length == 0 || room[0][0] == 1)
            {
                return 0;
            }

            int rows = room.Length;
            int cols = room[0].Length;

            // Directions: 0: Right, 1: Down, 2: Left, 3: Up
            int[] dr = [0, 1, 0, -1];
            int[] dc = [1, 0, -1, 0];

            bool[,,] visitedState = new bool[rows, cols, 4];
            bool[,] cleaned = new bool[rows, cols];

            int r = 0;
            int c = 0;
            int dir = 0;
            int cleanedCount = 0;

            while (!visitedState[r, c, dir])
            {
                visitedState[r, c, dir] = true;

                if (!cleaned[r, c])
                {
                    cleaned[r, c] = true;
                    cleanedCount++;
                }

                int nr = r + dr[dir];
                int nc = c + dc[dir];

                if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && room[nr][nc] == 0)
                {
                    // Move forward to the next empty space
                    r = nr;
                    c = nc;
                }
                else
                {
                    // Rotate 90 degrees clockwise
                    dir = (dir + 1) % 4;
                }
            }

            return cleanedCount;
        }
    }
}
