// Url: https://leetcode.com/problems/single-number/

/*
136. Single Number
Easy

Given a non-empty array of integers, every element appears twice except for one. Find that single one.

Note:

Your algorithm should have a linear runtime complexity. Could you implement it without using extra memory?

Example 1:
Input: [2,2,1]
Output: 1

Example 2:
Input: [4,1,2,1,2]
Output: 4
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem136 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (SingleNumber(null));
            Console.WriteLine (SingleNumber([2, 2, 1]));
            Console.WriteLine (SingleNumber([4, 1, 2, 1, 2]));
        }

        public static int SingleNumber (int[] nums) {
            int result = 0;

            for (int i = 0; i < nums.Length; i++) {
                result ^= nums[i];
            }

            return result;
        }
    }
}