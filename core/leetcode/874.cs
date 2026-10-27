// Url:https://leetcode.com/problems/walking-robot-simulation

/*
874. Walking Robot Simulation
Easy

A robot on an infinite grid starts at point (0, 0) and faces north.  The robot can receive one of three possible types of commands:
	-2: turn left 90 degrees
	-1: turn right 90 degrees
	1 <= x <= 9: move forward x units


Some of the grid squares are obstacles. 

The i-th obstacle is at grid point (obstacles[i][0], obstacles[i][1])

If the robot would try to move onto them, the robot stays on the previous grid square instead (but still continues following the rest of the route.)

Return the square of the maximum Euclidean distance that the robot will be from the origin.


Example 1:

Input: commands = [4,-1,3], obstacles = []
Output: 25
Explanation: robot will go to (3, 4)


Example 2:

Input: commands = [4,-1,4,-2,4], obstacles = [[2,4]]
Output: 65
Explanation: robot will be stuck at (1, 4) before turning left and going to (1, 8)


Note:
	0 <= commands.length <= 10000
	0 <= obstacles.length <= 10000
	-30000 <= obstacle[i][0] <= 30000
	-30000 <= obstacle[i][1] <= 30000
	The answer is guaranteed to be less than 2 ^ 31.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem874 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (RobotSim([4, -1, 3], null));
            Console.WriteLine (RobotSim([4, -1, 4, -2, 4], [[2, 4]]));
        }

        // Time: O (m*x), where m is number of commands
        // Space: O (n), where n is number of obstacles
        public static int RobotSim (int[] commands, int[][] obstacles) {
            int result = 0;

            if (commands == null || commands.Length == 0) {
                return result;
            }

            int[] dx = [0, 1, 0, -1];
            int[] dy = [1, 0, -1, 0];
            int x = 0;
            int y = 0;
            int direction = 0;

            HashSet<string> set = [];
            if (obstacles != null) {
                for (int i = 0; i < obstacles.Length; i++) {
                    set.Add (obstacles[i][0] + " " + obstacles[i][1]);
                }
            }

            for (int i = 0; i < commands.Length; i++) {
                if (commands[i] == -2) {
                    direction = (direction + 3) % 4;
                } else if (commands[i] == -1) {
                    direction = (direction + 1) % 4;
                } else if (commands[i] >= 1 && commands[i] <= 9) {
                    int c = commands[i];
                    while (c-- > 0 && !set.Contains ((x + dx[direction]) + " " + (y + dy[direction]))) {
                        x += dx[direction];
                        y += dy[direction];
                    }
                }

                result = Math.Max (result, x * x + y * y);
            }

            return result;
        }
    }
}