// Url:https://leetcode.com/problems/missing-element-in-sorted-array

/*
1060. Missing Element in Sorted Array
Medium

Given an integer array nums which is sorted in ascending order and all of its elements are unique and given also an integer k, return the kth missing number starting from the leftmost number of the array.

Example 1:
Input: nums = [4,7,9,10], k = 1
Output: 5
Explanation: The first missing number is 5.

Example 2:
Input: nums = [4,7,9,10], k = 3
Output: 8
Explanation: The missing numbers are [5,6,8,...], hence the third missing number is 8.

Example 3:
Input: nums = [1,2,4], k = 3
Output: 6
Explanation: The missing numbers are [3,5,6,7,...], hence the third missing number is 6.

Constraints:
- 1 <= nums.length <= 5 * 10^4
- 1 <= nums[i] <= 10^7
- nums is sorted in ascending order, and all the elements are unique.
- 1 <= k <= 10^8
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1060 {
    public class Solution {
        public void Init() {
            int[] nums1 = [4, 7, 9, 10];
            Console.WriteLine(MissingElement(nums1, 1)); // 5
            Console.WriteLine(MissingElement(nums1, 3)); // 8

            int[] nums2 = [1, 2, 4];
            Console.WriteLine(MissingElement(nums2, 3)); // 6
        }

        // Time: O(log n) where n is the length of nums
        // Space: O(1)
        public int MissingElement(int[] nums, int k) {
            int n = nums.Length;

            int MissingCount(int idx) => nums[idx] - nums[0] - idx;

            if (k > MissingCount(n - 1)) {
                return nums[n - 1] + (k - MissingCount(n - 1));
            }

            int left = 0;
            int right = n - 1;

            while (left < right) {
                int mid = left + (right - left + 1) / 2;
                if (MissingCount(mid) < k) {
                    left = mid;
                } else {
                    right = mid - 1;
                }
            }

            return nums[left] + (k - MissingCount(left));
        }
    }
}