// Url:https://leetcode.com/problems/sum-of-digits-in-the-minimum-number

/*
1085. Sum of Digits in the Minimum Number
Easy

Given an integer array nums of positive integers, return 0 if the sum of the digits of the minimal element in nums is odd, or 1 otherwise.

Example 1:
Input: nums = [34,23,1,24,75,33,54,8]
Output: 0
Explanation: The minimal element is 1, and the sum of those digits is 1 which is odd, so the answer is 0.

Example 2:
Input: nums = [99,77,33,66,55]
Output: 1
Explanation: The minimal element is 33, and the sum of those digits is 3 + 3 = 6 which is even, so the answer is 1.

Constraints:
- 1 <= nums.length <= 100
- 1 <= nums[i] <= 100
*/

using System;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1085 {
    public class Solution {
        public void Init() {
            int[] nums1 = [34, 23, 1, 24, 75, 33, 54, 8];
            Console.WriteLine(SumOfDigits(nums1)); // 0

            int[] nums2 = [99, 77, 33, 66, 55];
            Console.WriteLine(SumOfDigits(nums2)); // 1
        }

        // Time: O(n) where n is the length of nums
        // Space: O(1)
        public int SumOfDigits(int[] nums) {
            int min = nums[0];
            for (int i = 1; i < nums.Length; i++) {
                if (nums[i] < min) {
                    min = nums[i];
                }
            }

            int sum = 0;
            while (min > 0) {
                sum += min % 10;
                min /= 10;
            }

            return sum % 2 == 1 ? 0 : 1;
        }
    }
}