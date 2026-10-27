// Url:https://leetcode.com/problems/rectangle-overlap

/*
836. Rectangle Overlap
Easy

A rectangle is represented as a list [x1, y1, x2, y2], where (x1, y1) are the coordinates of its bottom-left corner, and (x2, y2) are the coordinates of its top-right corner.

Two rectangles overlap if the area of their intersection is positive.  To be clear, two rectangles that only touch at the corner or edges do not overlap.

Given two (axis-aligned) rectangles, return whether they overlap.

Example 1:

Input: rec1 = [0,0,2,2], rec2 = [1,1,3,3]
Output: true


Example 2:

Input: rec1 = [0,0,1,1], rec2 = [1,0,2,1]
Output: false


Notes:


	Both rectangles rec1 and rec2 are lists of 4 integers.
	All coordinates in rectangles will be between -10^9 and 10^9.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem836 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static bool IsRectangleOverlap (int[] rec1, int[] rec2) {
            return rec1[0] < rec2[2] && rec1[1] < rec2[3] && rec2[0] < rec1[2] && rec2[1] < rec1[3];
        }
    }
}