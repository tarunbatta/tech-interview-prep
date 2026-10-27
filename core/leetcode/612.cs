// Url:https://leetcode.com/problems/shortest-distance-in-a-plane

/*
612. Shortest Distance in a Plane
Medium

Table: Point2D

+-------------+------+
| Column Name | Type |
+-------------+------+
| x           | int  |
| y           | int  |
+-------------+------+
(x, y) is the primary key column for this table.
Each row of this table indicates the position of a point on the X-Y plane.

The distance between two points p1(x1, y1) and p2(x2, y2) is sqrt((x2 - x1)^2 + (y2 - y1)^2).

Write a solution to report the shortest distance between any two points from the Point2D table. Round the distance to two decimal places.

The result format is in the following example.

Example 1:

Input: 
Point2D table:
+----+----+
| x  | y  |
+----+----+
| -1 | -1 |
| 0  | 0  |
| -1 | -2 |
+----+----+
Output: 
+----------+
| shortest |
+----------+
| 1.00     |
+----------+
Explanation: The shortest distance is 1.00 between points (-1, -1) and (-1, -2).
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem612 {
    public class Point2D {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Solution {
        public void Init() {
            List<Point2D> points = [
                new() { X = -1, Y = -1 },
                new() { X = 0, Y = 0 },
                new() { X = -1, Y = -2 }
            ];

            Console.WriteLine($"{ShortestDistance(points):F2}"); // 1.00
        }

        // Time: O(n^2) where n is the number of points
        // Space: O(1)
        public double ShortestDistance(List<Point2D> points) {
            if (points.Count < 2) {
                return 0.0;
            }

            double minDistance = double.MaxValue;

            for (int i = 0; i < points.Count; i++) {
                for (int j = i + 1; j < points.Count; j++) {
                    double dx = points[i].X - points[j].X;
                    double dy = points[i].Y - points[j].Y;
                    double dist = Math.Sqrt(dx * dx + dy * dy);
                    if (dist < minDistance) {
                        minDistance = dist;
                    }
                }
            }

            return Math.Round(minDistance, 2);
        }
    }
}