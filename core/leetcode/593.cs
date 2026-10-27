// Url: https://leetcode.com/problems/valid-square

/*
593. Valid Square
Medium

Given the coordinates of four points in 2D space, return whether the four points could construct a square.

The coordinate (x,y) of a point is represented by an integer array with two integers.

Example:

Input: p1 = [0,0], p2 = [1,1], p3 = [1,0], p4 = [0,1]
Output: True
 

Note:

All the input integers are in the range [-10000, 10000].
A valid square has four equal sides with positive length and four equal angles (90-degree angles).
Input points have no order.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem593 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (ValidSquare (null, [1, 1], [1, 0], [0, 1]));
            Console.WriteLine (ValidSquare ([0, 0], [1, 1], [1, 0], [0, 1]));
        }

        public static bool ValidSquare (int[] p1, int[] p2, int[] p3, int[] p4) {
            bool result = false;

            if (!IsValidPoint(p1) || !IsValidPoint(p2) || !IsValidPoint(p3) || !IsValidPoint(p4)) {
                return false;
            }

            if (IsSquare(p1, p2, p3, p4) || IsSquare(p1, p2, p4, p3) || IsSquare(p1, p3, p2, p4) || IsSquare(p1, p3, p4, p2)) {
                result = true;
            }

            return result;
        }

        public static bool IsValidPoint (int[] p) {
            bool result = true;

            if (p == null || p.Length != 2) {
                return false;
            }

            for (int i = 0; i < p.Length; i++) {
                if (p[i] < -10000 || p[i] > 10000) {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public static double CalculatePointsDistance (int[] x, int[] y) {
            return Math.Sqrt (Math.Pow (Math.Abs (x[0] - y[0]), 2) + Math.Pow (Math.Abs (x[1] - y[1]), 2));
        }

        public static bool IsSquare (int[] p1, int[] p2, int[] p3, int[] p4) {
            bool result = false;

            double s1 = CalculatePointsDistance(p1, p2);
            double s2 = CalculatePointsDistance(p2, p3);
            double s3 = CalculatePointsDistance(p3, p4);
            double s4 = CalculatePointsDistance(p1, p4);
            double d1 = CalculatePointsDistance(p1, p3);
            double d2 = CalculatePointsDistance(p2, p4);

            if (s1 == 0 || s2 == 0 || s3 == 0 || s4 == 0 || d1 == 0 || d2 == 0) {
                return false;
            }

            if (s1 == s2 && s2 == s3 && s3 == s4 && s4 == s1 && d1 == d2) {
                result = true;
            }

            return result;
        }
    }
}