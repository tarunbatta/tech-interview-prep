// Url:https://leetcode.com/problems/shortest-distance-in-a-line

/*
613. Shortest Distance in a Line
Easy

Table: Point

+-------------+------+
| Column Name | Type |
+-------------+------+
| x           | int  |
+-------------+------+
x is the primary key column for this table.
Each row of this table indicates the position of a point on the X-axis.

Find the shortest distance between any two points from the Point table.

The result format is in the following example.

Example 1:

Input: 
Point table:
+----+
| x  |
+----+
| -1 |
| 0  |
| 2  |
+----+
Output: 
+----------+
| shortest |
+----------+
| 1        |
+----------+
Explanation: The shortest distance is between points -1 and 0 which is |(-1) - 0| = 1.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem613 {
    public class Point {
        public int X { get; set; }
    }

    public class Solution {
        public static void Init() {
            List<Point> points = [
                new() { X = -1 },
                new() { X = 0 },
                new() { X = 2 }
            ];

            Console.WriteLine(ShortestDistance(points)); // 1
        }

        // Time: O(n log n) where n is the number of points (due to sorting)
        // Space: O(n) to store sorted values
        public static int ShortestDistance(List<Point> points) {
            if (points.Count < 2) {
                return 0;
            }

            var sortedX = points.Select(p => p.X).OrderBy(x => x).ToList();
            int minDiff = int.MaxValue;

            for (int i = 1; i < sortedX.Count; i++) {
                minDiff = Math.Min(minDiff, sortedX[i] - sortedX[i - 1]);
            }

            return minDiff;
        }
    }
}