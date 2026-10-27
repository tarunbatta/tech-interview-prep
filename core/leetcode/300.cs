// Url:https://leetcode.com/problems/longest-increasing-subsequence

/*
300. Longest Increasing Subsequence
Medium

Given an unsorted array of integers, find the length of longest increasing subsequence.

Example:

Input: [10,9,2,5,3,7,101,18]
Output: 4 
Explanation: The longest increasing subsequence is [2,3,7,101], therefore the length is 4. 

Note: 
	There may be more than one LIS combination, it is only necessary for you to return the length.
	Your algorithm should run in O(n2) complexity.

Follow up: Could you improve it to O(n log n) time complexity?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem300 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n^2)
        // Space: O (n)
        public static int LengthOfLIS_a (int[] nums) {
            if (nums.Length == 0) {
                return 0;
            }

            int[] dp = new int[nums.Length];
            dp[0] = 1;
            int maxans = 1;

            for (int i = 1; i < dp.Length; i++) {
                int maxval = 0;

                for (int j = 0; j < i; j++) {
                    if (nums[i] > nums[j]) {
                        maxval = Math.Max (maxval, dp[j]);
                    }
                }

                dp[i] = maxval + 1;
                maxans = Math.Max (maxans, dp[i]);
            }

            return maxans;
        }

        // Time: O (n log (n))
        // Space: O (n)
        public static int LengthOfLIS_b (int[] nums) {
            if (nums == null || nums.Length == 0) {
                return 0;
            }

            int[] dp = new int[nums.Length];
            int len = 0;

            foreach (int num in nums) {
                int i = Array.BinarySearch (dp, 0, len, num);

                if (i < 0) {
                    i = -(i + 1);
                }

                dp[i] = num;

                if (i == len) {
                    len++;
                }
            }

            return len;
        }
    }
}