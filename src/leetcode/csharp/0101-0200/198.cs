// Url: https://leetcode.com/problems/house-robber/

/*
198. House Robber
Easy

You are a professional robber planning to rob houses along a street. Each house has a certain amount of money stashed, the only constraint stopping you from robbing each of them is that adjacent houses have security system connected and it will automatically contact the police if two adjacent houses were broken into on the same night.

Given a list of non-negative integers representing the amount of money of each house, determine the maximum amount of money you can rob tonight without alerting the police.

Example 1:

Input: [1,2,3,1]
Output: 4
Explanation: Rob house 1 (money = 1) and then rob house 3 (money = 3).
             Total amount you can rob = 1 + 3 = 4.
Example 2:

Input: [2,7,9,3,1]
Output: 12
Explanation: Rob house 1 (money = 2), rob house 3 (money = 9) and rob house 5 (money = 1).
             Total amount you can rob = 2 + 9 + 1 = 12.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem198 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (Rob(null));
            Console.WriteLine (Rob([]));
            Console.WriteLine (Rob([1, 2, 3, 1]));
            Console.WriteLine (Rob([2, 7, 9, 3, 1]));
        }

        public static int Rob (int[] nums) {
            if (nums == null || nums.Length == 0) {
                return 0;
            }

            int a = 0;
            int b = 0;

            for (int i = 0; i < nums.Length; i++) {
                if (i % 2 == 0) {
                    a = Math.Max (a + nums[i], b);
                } else {
                    b = Math.Max (a, b + nums[i]);
                }
            }

            return Math.Max (a, b);
        }
    }
}