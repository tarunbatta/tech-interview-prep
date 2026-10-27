// Url: https://leetcode.com/problems/convex-polygon

/*
469. Convex Polygon
Medium

You are given an array of points where points[i] = [xi, yi] represents the coordinates of a vertex of a polygon in the x-y plane. The points are given in order around the polygon.

Return true if the polygon is convex and false otherwise.

A polygon is convex if and only if all its interior angles are strictly less than 180°.

Example 1:
Input: points = [[0,0],[0,5],[5,5],[5,0]]
Output: true

Example 2:
Input: points = [[0,0],[0,10],[10,10],[10,0],[5,5]]
Output: false

Constraints:
3 <= points.length <= 10^4
points[i].length == 2
-10^4 <= xi, yi <= 10^4
All the given points are unique.
No three consecutive points are on the same line.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem469
{
    public class Solution
    {
        public void Init()
        {
            var p1 = new List<IList<int>>
            {
                new List<int> { 0, 0 },
                new List<int> { 0, 5 },
                new List<int> { 5, 5 },
                new List<int> { 5, 0 }
            };
            Console.WriteLine(IsConvex(p1)); // True

            var p2 = new List<IList<int>>
            {
                new List<int> { 0, 0 },
                new List<int> { 0, 10 },
                new List<int> { 10, 10 },
                new List<int> { 10, 0 },
                new List<int> { 5, 5 }
            };
            Console.WriteLine(IsConvex(p2)); // False
        }

        // Time: O(n) where n is points.Count (single pass cross-product orientation check)
        // Space: O(1) auxiliary space
        public static bool IsConvex(IList<IList<int>> points)
        {
            if (points == null || points.Count < 3)
            {
                return false;
            }

            int n = points.Count;
            long prevCrossProduct = 0;

            for (int i = 0; i < n; i++)
            {
                var p1 = points[i];
                var p2 = points[(i + 1) % n];
                var p3 = points[(i + 2) % n];

                long dx1 = p2[0] - p1[0];
                long dy1 = p2[1] - p1[1];
                long dx2 = p3[0] - p2[0];
                long dy2 = p3[1] - p2[1];

                long cross = dx1 * dy2 - dy1 * dx2;

                if (cross != 0)
                {
                    if (cross * prevCrossProduct < 0)
                    {
                        return false;
                    }

                    prevCrossProduct = cross;
                }
            }

            return true;
        }
    }
}