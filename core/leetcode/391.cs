// Url: https://leetcode.com/problems/perfect-rectangle/

/*
391. Perfect Rectangle

Given N axis-aligned rectangles where N > 0, determine if they all together form an exact cover of a rectangular region.

Each rectangle is represented as a bottom-left point and a top-right point. For example, a unit square is represented as [1,1,2,2]. (coordinate of bottom-left point is (1, 1) and top-right point is (2, 2)).


Example 1:

rectangles = [
  [1,1,3,3],
  [3,1,4,2],
  [3,2,4,4],
  [1,3,2,4],
  [2,3,3,4]
]

Return true. All 5 rectangles together form an exact cover of a rectangular region.
 

 

Example 2:

rectangles = [
  [1,1,2,3],
  [1,3,2,4],
  [3,1,4,2],
  [3,2,4,4]
]

Return false. Because there is a gap between the two rectangular regions.
 

 

Example 3:

rectangles = [
  [1,1,3,3],
  [3,1,4,2],
  [1,3,2,4],
  [3,2,4,4]
]

Return false. Because there is a gap in the top center.
 

 

Example 4:

rectangles = [
  [1,1,3,3],
  [3,1,4,2],
  [1,3,2,4],
  [2,2,4,4]
]

Return false. Because two of the rectangles overlap with each other.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem391 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (IsRectangleCover(null));
            Console.WriteLine (IsRectangleCover([
                [1, 1, 3, 3],
                    [3, 1, 4, 2],
                    [3, 2, 4, 4],
                    [1, 3, 2, 4],
                    [2, 3, 3, 4]
            ]));
            Console.WriteLine (IsRectangleCover([
                [1, 1, 2, 3],
                    [1, 3, 2, 4],
                    [3, 1, 4, 2],
                    [3, 2, 4, 4]
            ]));
            Console.WriteLine (IsRectangleCover([
                [1, 1, 3, 3],
                    [3, 1, 4, 2],
                    [1, 3, 2, 4],
                    [3, 2, 4, 4]
            ]));
            Console.WriteLine (IsRectangleCover([
                [1, 1, 3, 3],
                    [3, 1, 4, 2],
                    [1, 3, 2, 4],
                    [2, 2, 4, 4]
            ]));
        }

        public static bool IsRectangleCover (int[][] rectangles) {
            if (rectangles == null || rectangles.Length == 0 || rectangles[0].Length == 0) {
                return false;
            }

            int x1 = Int32.MaxValue;
            int y1 = Int32.MaxValue;
            int x2 = Int32.MinValue;
            int y2 = Int32.MinValue;

            HashSet<String> set = [];
            int area = 0;

            foreach (var rect in rectangles) {
                x1 = Math.Min (rect[0], x1);
                y1 = Math.Min (rect[1], y1);
                x2 = Math.Max (rect[2], x2);
                y2 = Math.Max (rect[3], y2);

                area += (rect[2] - rect[0]) * (rect[3] - rect[1]);

                String s1 = rect[0] + " " + rect[1];
                String s2 = rect[0] + " " + rect[3];
                String s3 = rect[2] + " " + rect[3];
                String s4 = rect[2] + " " + rect[1];

                if (!set.Add (s1)) set.Remove (s1);
                if (!set.Add (s2)) set.Remove (s2);
                if (!set.Add (s3)) set.Remove (s3);
                if (!set.Add (s4)) set.Remove (s4);
            }

            if (!set.Contains (x1 + " " + y1) || !set.Contains (x1 + " " + y2) || !set.Contains (x2 + " " + y1) || !set.Contains (x2 + " " + y2) || set.Count != 4) return false;

            // 1. the large rectangle area should be equal to the sum of small rectangles
            // 2. count of all the points should be even, and that of all the four corner points should be one
            return area == (x2 - x1) * (y2 - y1);
        }
    }
}