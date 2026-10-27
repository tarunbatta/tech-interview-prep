// Url: https://leetcode.com/problems/rotate-array/

/*
189. Rotate Array
Easy

Given an array, rotate the array to the right by k steps, where k is non-negative.

Example 1:

Input: [1,2,3,4,5,6,7] and k = 3
Output: [5,6,7,1,2,3,4]

Explanation:
rotate 1 steps to the right: [7,1,2,3,4,5,6]
rotate 2 steps to the right: [6,7,1,2,3,4,5]
rotate 3 steps to the right: [5,6,7,1,2,3,4]

Example 2:

Input: [-1,-100,3,99] and k = 2
Output: [3,99,-1,-100]

Explanation: 
rotate 1 steps to the right: [99,-1,-100,3]
rotate 2 steps to the right: [3,99,-1,-100]

Note:
Try to come up as many solutions as you can, there are at least 3 different ways to solve this problem.
Could you do it in-place with O(1) extra space?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem189 {
    public class Solution {
        public static void Init() {
            Rotate(null, 0);
            Rotate([1, 2, 3, 4, 5, 6, 7], 3);
            Rotate([-1, -100, 3, 99], 2);
        }

        // Time: O (n)
        // Space: O (1)
        public static void Rotate (int[] nums, int k) {
            if (nums == null || nums.Length == 0 || k <= 0 || k == nums.Length) {
                return;
            }

            k = k % nums.Length;

            Reverse(nums, 0, nums.Length - 1);
            Reverse(nums, 0, k - 1);
            Reverse(nums, k, nums.Length - 1);
        }

        private static void Reverse (int[] nums, int start, int end) {
            while (start < end) {
                int temp = nums[start];

                nums[start] = nums[end];
                nums[end] = temp;

                start++;
                end--;
            }
        }
    }
}