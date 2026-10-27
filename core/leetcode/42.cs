// Url: https://leetcode.com/problems/trapping-rain-water

/*
42. Trapping Rain Water
Hard

Given n non-negative integers representing an elevation map where the width of each bar is 1, compute how much water it is able to trap after raining.


The above elevation map is represented by array [0,1,0,2,1,0,1,3,2,1,2,1]. In this case, 6 units of rain water (blue section) are being trapped. Thanks Marcos for contributing this image!

Example:

Input: [0,1,0,2,1,0,1,3,2,1,2,1]
Output: 6
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem42 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (Trap_a(null));
            Console.WriteLine (Trap_a([0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1]));

            Console.WriteLine (Trap_b(null));
            Console.WriteLine (Trap_b([0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1]));
        }

        // Time: O (n)
        // Space: O (n)
        public static int Trap_a (int[] height) {
            int result = 0;

            if (height == null || height.Length < 2) {
                return result;
            }

            int[] left = new int[height.Length];
            left[0] = height[0];
            for (int i = 1; i < height.Length; i++) {
                left[i] = Math.Max (left[i - 1], height[i]);
            }

            int[] right = new int[height.Length];
            right[height.Length - 1] = height[height.Length - 1];
            for (int i = height.Length - 2; i >= 0; i--) {
                right[i] = Math.Max (right[i + 1], height[i]);
            }

            for (int i = 0; i < height.Length; i++) {
                result += Math.Min (left[i], right[i]) - height[i];
            }

            return result;
        }

        // Time: O (n)
        // Space: O (1)
        public static int Trap_b (int[] heights) {
            var n = heights.Length;
            if (n == 0) return 0;

            var water = 0;
            var seaLevel = 0;

            var left = 0;
            var right = n - 1;

            while (left < right) {
                seaLevel = Math.Max (seaLevel, Math.Min (heights[left], heights[right]));

                water += Math.Max (0, seaLevel - heights[left]) + Math.Max (0, seaLevel - heights[right]);

                if (heights[left] > heights[right]) {
                    right--;
                } else {
                    left++;
                }
            }

            return water;
        }
    }
}