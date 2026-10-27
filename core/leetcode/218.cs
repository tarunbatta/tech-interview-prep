// Url: https://leetcode.com/problems/the-skyline-problem/

/*
The Skyline Problem
Hard

A city's skyline is the outer contour of the silhouette formed by all the buildings in that city when viewed from a distance. Now suppose you are given the locations and height of all the buildings as shown on a cityscape photo (Figure A), write a program to output the skyline formed by these buildings collectively (Figure B).

Buildings  Skyline Contour
The geometric information of each building is represented by a triplet of integers [Li, Ri, Hi], where Li and Ri are the x coordinates of the left and right edge of the ith building, respectively, and Hi is its height. It is guaranteed that 0 ≤ Li, Ri ≤ INT_MAX, 0 < Hi ≤ INT_MAX, and Ri - Li > 0. You may assume all buildings are perfect rectangles grounded on an absolutely flat surface at height 0.

For instance, the dimensions of all buildings in Figure A are recorded as: [ [2 9 10], [3 7 15], [5 12 12], [15 20 10], [19 24 8] ] .

The output is a list of "key points" (red dots in Figure B) in the format of [ [x1,y1], [x2, y2], [x3, y3], ... ] that uniquely defines a skyline. A key point is the left endpoint of a horizontal line segment. Note that the last key point, where the rightmost building ends, is merely used to mark the termination of the skyline, and always has zero height. Also, the ground in between any two adjacent buildings should be considered part of the skyline contour.

For instance, the skyline in Figure B should be represented as:[ [2 10], [3 15], [7 12], [12 0], [15 10], [20 8], [24, 0] ].

Notes:

The number of buildings in any input list is guaranteed to be in the range [0, 10000].
The input list is already sorted in ascending order by the left x position Li.
The output list must be sorted by the x position.
There must be no consecutive horizontal lines of equal height in the output skyline. For instance, [...[2 3], [4 5], [7 5], [11 5], [12 7]...] is not acceptable; the three lines of height 5 should be merged into one in the final output as such: [...[2 3], [4 5], [12 7], ...]
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem218 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (GetSkyline(null));
            Console.WriteLine (GetSkyline([[2, 9, 10], [3, 7, 15], [5, 12, 12], [15, 20, 10], [19, 24, 8]]));
        }

        // Time: O (n log (n))
        // Space: O (n)
        public static IList<IList<int>> GetSkyline (int[][] buildings) {
            IList<IList<int>> result = [];

            if (buildings == null || buildings.Length == 0 || buildings[0].Length == 0) {
                return null;
            }

            List<Point> points = [];

            foreach (var item in buildings) {
                int start = item[0];
                int end = item[1];
                int h = item[2];
                points.Add (new Point (start, h, true));
                points.Add (new Point (end, h, false));
            }

            points.Sort ((x, y) => x.CompareTo (y));

            Dictionary<int, int> queue = new()
            {
                { 0, 1 }
            };

            int previousMaxHeight = 0;

            foreach (var point in points) {
                if (point.isStart) {
                    if (!queue.ContainsKey (point.height)) {
                        queue.Add (point.height, 1);
                    } else {
                        queue[point.height]++;
                    }
                } else {
                    if (queue[point.height] == 1) {
                        queue.Remove (point.height);
                    } else {
                        queue[point.height]--;
                    }
                }

                int currentMaxHeight = queue.Keys.Max ();

                if (previousMaxHeight != currentMaxHeight) {
                    result.Add ([point.x, currentMaxHeight]);
                    previousMaxHeight = currentMaxHeight;
                }
            }

            return result;
        }
    }

    public class Point(int x, int height, bool isStart) : IComparer<Point> {
        public int x = x;
        public int height = height;

        public bool isStart = isStart;

        public int Compare (Point x, Point y) {
            if (x == null || y == null) {
                return 0;
            }

            return x.CompareTo (y);
        }

        public int CompareTo (Point p) {
            if (this.x != p.x) {
                return this.x - p.x;
            } else {
                if (this.isStart && p.isStart) {
                    return p.height - this.height;
                }
                if (!this.isStart && !p.isStart) {
                    return this.height - p.height;
                }
                return this.isStart ? -1 : 1;
            }
        }
    }
}