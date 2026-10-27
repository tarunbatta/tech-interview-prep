// Url:https://leetcode.com/problems/number-of-valid-subarrays

/*
1063. Number of Valid Subarrays
Hard

Given an integer array nums, return the number of non-empty subarrays with the leftmost element of the subarray not larger than other elements in the subarray.

A subarray is a contiguous part of an array.

Example 1:
Input: nums = [1,4,2,5,3]
Output: 11
Explanation: There are 11 valid subarrays: [1],[4],[2],[5],[3],[1,4],[2,5],[1,4,2],[2,5,3],[1,4,2,5],[1,4,2,5,3].

Example 2:
Input: nums = [3,2,1]
Output: 3
Explanation: The 3 valid subarrays are: [3],[2],[1].

Example 3:
Input: nums = [2,2,2]
Output: 6
Explanation: There are 6 valid subarrays: [2],[2],[2],[2,2],[2,2],[2,2,2].

Constraints:
- 1 <= nums.length <= 5 * 10^4
- 0 <= nums[i] <= 10^5
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem1063 {
    public class Solution {
        public static void Init() {
            int[] nums1 = [1, 4, 2, 5, 3];
            Console.WriteLine(ValidSubarrays(nums1)); // 11

            int[] nums2 = [3, 2, 1];
            Console.WriteLine(ValidSubarrays(nums2)); // 3

            int[] nums3 = [2, 2, 2];
            Console.WriteLine(ValidSubarrays(nums3)); // 6
        }

        // Time: O(n) where n is the length of nums
        // Space: O(n) for monotonic stack
        public static int ValidSubarrays(int[] nums) {
            int n = nums.Length;
            int count = 0;
            Stack<int> stack = [];

            for (int i = 0; i < n; i++) {
                while (stack.Count > 0 && nums[stack.Peek()] > nums[i]) {
                    int prevIdx = stack.Pop();
                    count += i - prevIdx;
                }
                stack.Push(i);
            }

            while (stack.Count > 0) {
                int prevIdx = stack.Pop();
                count += n - prevIdx;
            }

            return count;
        }
    }
}