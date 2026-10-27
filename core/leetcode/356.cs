// Url: https://leetcode.com/problems/line-reflection

/*
356. Line Reflection
Medium

Given n points on a 2D plane, find if there is such a line parallel to the y-axis that reflects the given points symmetrically.

In other words, answer whether or not if there exists a line that after reflecting all points over the given line, the original points' set is the same as the reflected points.

Note that there may be duplicate points.

Example 1:
Input: points = [[1,1],[-1,1]]
Output: true
Explanation: We can choose the line x = 0.

Example 2:
Input: points = [[1,1],[-1,-1]]
Output: false
Explanation: We can't choose any line.

Constraints:
n == points.length
1 <= n <= 10^4
-10^8 <= points[i][j] <= 10^8
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem356
{
    public class Solution
    {
        public void Init()
        {
            int[][] points1 = new int[][]
            {
                new int[] { 1, 1 },
                new int[] { -1, 1 }
            };
            Console.WriteLine(IsReflected(points1)); // True

            int[][] points2 = new int[][]
            {
                new int[] { 1, 1 },
                new int[] { -1, -1 }
            };
            Console.WriteLine(IsReflected(points2)); // False
        }

        // Time: O(n) where n is points.Length
        // Space: O(n) for the hash set of unique points
        public static bool IsReflected(int[][] points)
        {
            if (points == null || points.Length == 0)
            {
                return true;
            }

            int minX = int.MaxValue;
            int maxX = int.MinValue;
            HashSet<(int x, int y)> pointSet = new();

            foreach (var point in points)
            {
                int x = point[0];
                int y = point[1];

                minX = Math.Min(minX, x);
                maxX = Math.Max(maxX, x);
                pointSet.Add((x, y));
            }

            long sum = (long)minX + maxX;

            foreach (var (x, y) in pointSet)
            {
                long reflectedX = sum - x;

                if (reflectedX < int.MinValue || reflectedX > int.MaxValue || !pointSet.Contains(((int)reflectedX, y)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}