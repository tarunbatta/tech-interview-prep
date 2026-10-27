// Url: https://leetcode.com/problems/maximum-size-subarray-sum-equals-k

/*
325. Maximum Size Subarray Sum Equals k
Medium

Given an integer array nums and an integer k, return the maximum length of a subarray that sums to k. If there is not one, return 0 instead.

Example 1:
Input: nums = [1,-1,5,-2,3], k = 3
Output: 4
Explanation: The subarray [1, -1, 5, -2] sums to 3 and is the longest.

Example 2:
Input: nums = [-2,-1,2,1], k = 1
Output: 2
Explanation: The subarray [-1, 2] sums to 1 and is the longest.

Constraints:
1 <= nums.length <= 2 * 10^5
-10^4 <= nums[i] <= 10^4
-10^9 <= k <= 10^9
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem325
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(MaxSubArrayLen(new int[] { 1, -1, 5, -2, 3 }, 3)); // 4
            Console.WriteLine(MaxSubArrayLen(new int[] { -2, -1, 2, 1 }, 1));   // 2
        }

        // Time: O(n) where n is nums.Length
        // Space: O(n) for the prefix sum hash map
        public static int MaxSubArrayLen(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }

            Dictionary<long, int> prefixMap = new();
            prefixMap[0] = -1;

            long prefixSum = 0;
            int maxLen = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                prefixSum += nums[i];

                if (prefixMap.TryGetValue(prefixSum - k, out int prevIndex))
                {
                    maxLen = Math.Max(maxLen, i - prevIndex);
                }

                // Only store first occurrence of prefixSum to maximize length
                if (!prefixMap.ContainsKey(prefixSum))
                {
                    prefixMap[prefixSum] = i;
                }
            }

            return maxLen;
        }
    }
}