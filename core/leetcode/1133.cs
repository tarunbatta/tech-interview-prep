// Url:https://leetcode.com/problems/largest-unique-number

/*
1133. Largest Unique Number
Easy

Given an integer array nums, return the largest integer that only occurs once. If no integer occurs once, return -1.

Example 1:
Input: nums = [5,7,3,9,4,9,8,3,1]
Output: 8
Explanation: The maximum integer in the array is 9 but it is repeated. The number 8 occurs only once, so it is the answer.

Example 2:
Input: nums = [9,9,8,8]
Output: -1
Explanation: There is no number that occurs only once.

Constraints:
- 1 <= nums.length <= 2000
- 0 <= nums[i] <= 1000
*/

using System;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1133 {
    public class Solution {
        public static void Init() {
            int[] nums1 = [5, 7, 3, 9, 4, 9, 8, 3, 1];
            Console.WriteLine(LargestUniqueNumber(nums1)); // 8

            int[] nums2 = [9, 9, 8, 8];
            Console.WriteLine(LargestUniqueNumber(nums2)); // -1
        }

        // Time: O(n) where n is the length of nums
        // Space: O(n) to store frequencies
        public static int LargestUniqueNumber(int[] nums) {
            var unique = nums
                .GroupBy(n => n)
                .Where(g => g.Count() == 1)
                .Select(g => g.Key)
                .ToList();

            return unique.Count > 0 ? unique.Max() : -1;
        }
    }
}