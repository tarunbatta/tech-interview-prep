// Url:https://leetcode.com/problems/maximum-average-subarray-ii

/*
644. Maximum Average Subarray II
Hard

You are given an integer array nums consisting of n elements, and an integer k.

Find a contiguous subarray whose length is greater than or equal to k that has the maximum average value, and return this value. Any answer with a calculation error less than 10^-5 will be accepted.

Example 1:
Input: nums = [1,12,-5,-6,50,3], k = 4
Output: 12.75000
Explanation:
- When the length is 4, averages are: (1 + 12 - 5 - 6) / 4 = 0.5, (12 - 5 - 6 + 50) / 4 = 12.75, (-5 - 6 + 50 + 3) / 4 = 10.5
- When the length is 5, averages are: (1 + 12 - 5 - 6 + 50) / 5 = 10.4, (12 - 5 - 6 + 50 + 3) / 5 = 10.8
- When the length is 6, the average is: (1 + 12 - 5 - 6 + 50 + 3) / 6 = 9.0
The maximum average is 12.75.

Example 2:
Input: nums = [5], k = 1
Output: 5.00000

Constraints:
- 1 <= k <= nums.length <= 10^4
- -10^4 <= nums[i] <= 10^4
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem644 {
    public class Solution {
        public void Init() {
            int[] nums1 = [1, 12, -5, -6, 50, 3];
            int k1 = 4;
            Console.WriteLine($"{FindMaxAverage(nums1, k1):F5}"); // 12.75000

            int[] nums2 = [5];
            int k2 = 1;
            Console.WriteLine($"{FindMaxAverage(nums2, k2):F5}"); // 5.00000
        }

        // Time: O(n * log((max - min) / eps))
        // Space: O(1)
        public double FindMaxAverage(int[] nums, int k) {
            double low = nums[0];
            double high = nums[0];

            foreach (int num in nums) {
                low = Math.Min(low, num);
                high = Math.Max(high, num);
            }

            while (high - low > 1e-5) {
                double mid = low + (high - low) / 2.0;
                if (CanFindLargerAverage(nums, k, mid)) {
                    low = mid;
                } else {
                    high = mid;
                }
            }

            return low;
        }

        private static bool CanFindLargerAverage(int[] nums, int k, double mid) {
            double currentSum = 0;
            double prevSum = 0;
            double minPrevSum = 0;

            for (int i = 0; i < k; i++) {
                currentSum += nums[i] - mid;
            }

            if (currentSum >= 0) {
                return true;
            }

            for (int i = k; i < nums.Length; i++) {
                currentSum += nums[i] - mid;
                prevSum += nums[i - k] - mid;
                minPrevSum = Math.Min(minPrevSum, prevSum);

                if (currentSum - minPrevSum >= 0) {
                    return true;
                }
            }

            return false;
        }
    }
}