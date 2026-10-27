// Url: https://leetcode.com/problems/robot-room-cleaner

/*
489. Robot Room Cleaner
Hard

You are controlling a robot that is located somewhere inside a room. The room is modeled as an m x n binary grid where 0 represents a wall and 1 represents an empty slot.

The robot starts at an unknown location in the room that is guaranteed to be empty, and you do not have access to the grid, but you can move the robot using the given API Robot.

You are tasked to use the robot to clean the entire room (i.e., clean every empty cell in the room). The robot with the four given APIs can move forward, turn left, or turn right. Each turn is 90 degrees.

When the robot tries to move into a wall cell, its bumper sensor detects the obstacle, and it stays on the current cell.

Design an algorithm to clean the entire room using the following APIs:
- bool move(): Returns true if the cell in front is open and robot moves into the cell. Returns false if the cell in front is blocked and robot stays in the current cell.
- void turnLeft(): Robot turns 90 degrees to the left.
- void turnRight(): Robot turns 90 degrees to the right.
- void clean(): Clean the current cell.

Constraints:
m == room.length
n == room[i].length
1 <= m <= 100
1 <= n <= 200
room[i][j] is either 0 or 1.
0 <= row < m
0 <= col < n
room[row][col] == 1
All the empty cells can be visited from the starting position.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem489
{
    public interface Robot
    {
        bool Move();
        void TurnLeft();
        void TurnRight();
        void Clean();
    }

    public class MockRobot : Robot
    {
        private readonly int[][] _room;
        private int _r;
        private int _c;
        private int _dir; // 0: Up, 1: Right, 2: Down, 3: Left
        private readonly HashSet<(int, int)> _cleaned;

        private static readonly int[] dr = [-1, 0, 1, 0];
        private static readonly int[] dc = [0, 1, 0, -1];

        public MockRobot(int[][] room, int startR, int startC)
        {
            _room = room;
            _r = startR;
            _c = startC;
            _dir = 0;
            _cleaned = [];
        }

        public bool Move()
        {
            int nr = _r + dr[_dir];
            int nc = _c + dc[_dir];

            if (nr >= 0 && nr < _room.Length && nc >= 0 && nc < _room[0].Length && _room[nr][nc] == 1)
            {
                _r = nr;
                _c = nc;
                return true;
            }

            return false;
        }

        public void TurnLeft() => _dir = (_dir + 3) % 4;
        public void TurnRight() => _dir = (_dir + 1) % 4;
        public void Clean() => _cleaned.Add((_r, _c));
        public int CleanedCount => _cleaned.Count;
    }

    public class Solution
    {
        private static readonly int[] dr = [-1, 0, 1, 0];
        private static readonly int[] dc = [0, 1, 0, -1];

        public static void Init()
        {
            int[][] room =
            [
                [1, 1, 1, 1, 1, 0, 1, 1],
                [1, 1, 1, 1, 1, 0, 1, 1],
                [1, 0, 1, 1, 1, 1, 1, 1],
                [0, 0, 0, 1, 0, 0, 0, 0],
                [1, 1, 1, 1, 1, 1, 1, 1]
            ];

            MockRobot robot = new(room, 1, 3);
            CleanRoom(robot);
            Console.WriteLine(robot.CleanedCount); // Total reachable empty cells cleaned
        }

        // Time: O(4 * (N - M)) where N is total cells and M is obstacle cells (visits every accessible cell and checks 4 directions)
        // Space: O(N - M) for tracking visited cell coordinates in relative coordinate frame
        public static void CleanRoom(Robot robot)
        {
            if (robot == null)
            {
                return;
            }

            HashSet<(int r, int c)> visited = [];
            Dfs(robot, 0, 0, 0, visited);
        }

        private static void Dfs(Robot robot, int r, int c, int d, HashSet<(int, int)> visited)
        {
            visited.Add((r, c));
            robot.Clean();

            for (int i = 0; i < 4; i++)
            {
                int newD = (d + i) % 4;
                int nr = r + dr[newD];
                int nc = c + dc[newD];

                if (!visited.Contains((nr, nc)) && robot.Move())
                {
                    Dfs(robot, nr, nc, newD, visited);

                    // Backtrack to previous position and face original direction
                    robot.TurnRight();
                    robot.TurnRight();
                    robot.Move();
                    robot.TurnRight();
                    robot.TurnRight();
                }

                robot.TurnRight();
            }
        }
    }
}