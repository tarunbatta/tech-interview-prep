// Url:https://leetcode.com/problems/divide-array-into-increasing-sequences

/*
1121. Divide Array Into Increasing Sequences
Hard

Given an integer array nums sorted in non-decreasing order and an integer k, return true if this array can be divided into one or more disjoint increasing subsequences of length at least k, or false otherwise.

Example 1:
Input: nums = [1,2,2,3,3,4,4], k = 3
Output: true
Explanation: The array can be divided into two subsequences [1,2,3,4] and [2,3,4] with lengths at least 3 each.

Example 2:
Input: nums = [5,6,6,7,8], k = 3
Output: false
Explanation: There is no way to divide the array using the given conditions.

Constraints:
- 1 <= k <= nums.length <= 10^5
- 1 <= nums[i] <= 10^5
- nums is sorted in non-decreasing order.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1121 {
    public class Solution {
        public void Init() {
            int[] nums1 = [1, 2, 2, 3, 3, 4, 4];
            Console.WriteLine(CanDivideIntoSubsequences(nums1, 3)); // True

            int[] nums2 = [5, 6, 6, 7, 8];
            Console.WriteLine(CanDivideIntoSubsequences(nums2, 3)); // False
        }

        // Time: O(n) where n is the length of nums
        // Space: O(1)
        public bool CanDivideIntoSubsequences(int[] nums, int k) {
            int maxFreq = 1;
            int currentFreq = 1;

            for (int i = 1; i < nums.Length; i++) {
                if (nums[i] == nums[i - 1]) {
                    currentFreq++;
                    maxFreq = Math.Max(maxFreq, currentFreq);
                } else {
                    currentFreq = 1;
                }
            }

            return nums.Length >= maxFreq * k;
        }
    }
}