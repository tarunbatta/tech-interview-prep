// Url:https://leetcode.com/problems/product-of-array-except-self

/*
238. Product of Array Except Self
Medium

Given an array nums of n integers where n > 1,  return an array output such that output[i] is equal to the product of all the elements of nums except nums[i].

Example:

Input:  [1,2,3,4]
Output: [24,12,8,6]


Note: Please solve it without division and in O(n).

Follow up:
Could you solve it with constant space complexity? (The output array does not count as extra space for the purpose of space complexity analysis.)
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem238 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n)
        // Space: O (1)
        public static int[] ProductExceptSelf (int[] nums) {
            if (nums == null || nums.Length == 0) {
                return nums;
            }

            int[] result = new int[nums.Length];

            int runningPrefix = 1;

            for (int i = 0; i < nums.Length; i++) {
                result[i] = runningPrefix;
                runningPrefix *= nums[i];
            }

            int runningSuffix = 1;
            for (int i = nums.Length - 1; i >= 0; i--) {
                result[i] *= runningSuffix;
                runningSuffix *= nums[i];
            }

            return result;
        }
    }
}