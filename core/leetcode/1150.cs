// Url:https://leetcode.com/problems/check-if-a-number-is-majority-element-in-a-sorted-array

/*
1150. Check If a Number Is Majority Element in a Sorted Array
Easy

Given an integer array nums sorted in non-decreasing order and an integer target, return true if target is a majority element, or false otherwise.

A majority element in an array nums is an element that appears more than nums.length / 2 times in the array.

Example 1:
Input: nums = [2,4,5,5,5,5,5,6,6], target = 5
Output: true
Explanation: The value 5 appears 5 times and the length of the array is 9. Thus, 5 is a majority element because 5 > 9/2 is true.

Example 2:
Input: nums = [10,100,101,101], target = 101
Output: false
Explanation: The value 101 appears 2 times and the length of the array is 4. Thus, 101 is not a majority element because 2 > 4/2 is false.

Constraints:
- 1 <= nums.length <= 1000
- 1 <= nums[i], target <= 10^9
- nums is sorted in non-decreasing order.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1150 {
    public class Solution {
        public void Init() {
            int[] nums1 = [2, 4, 5, 5, 5, 5, 5, 6, 6];
            Console.WriteLine(IsMajorityElement(nums1, 5)); // True

            int[] nums2 = [10, 100, 101, 101];
            Console.WriteLine(IsMajorityElement(nums2, 101)); // False
        }

        // Time: O(log n) where n is length of nums
        // Space: O(1)
        public bool IsMajorityElement(int[] nums, int target) {
            int n = nums.Length;
            int firstIdx = FirstOccurrence(nums, target);

            if (firstIdx == -1) {
                return false;
            }

            int majorityIdx = firstIdx + n / 2;
            return majorityIdx < n && nums[majorityIdx] == target;
        }

        private static int FirstOccurrence(int[] nums, int target) {
            int low = 0;
            int high = nums.Length - 1;
            int ans = -1;

            while (low <= high) {
                int mid = low + (high - low) / 2;
                if (nums[mid] == target) {
                    ans = mid;
                    high = mid - 1;
                } else if (nums[mid] < target) {
                    low = mid + 1;
                } else {
                    high = mid - 1;
                }
            }

            return ans;
        }
    }
}