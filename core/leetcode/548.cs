// Url: https://leetcode.com/problems/split-array-with-equal-sum

/*
548. Split Array with Equal Sum
Medium

Given an integer array nums of length n, return true if there exists triplets (i, j, k) satisfy the following conditions:
- 0 < i, i + 1 < j, j + 1 < k < n - 1
- The sum of subarrays (0, i - 1), (i + 1, j - 1), (j + 1, k - 1) and (k + 1, n - 1) are equal.
A subarray (l, r) represents a slice of the original array from the element at index l to the element at index r.

Example 1:
Input: nums = [1,2,1,2,1,2,1]
Output: true
Explanation:
i = 1, j = 3, k = 5. 
sum(0, i - 1) = sum(0, 0) = 1
sum(i + 1, j - 1) = sum(2, 2) = 1
sum(j + 1, k - 1) = sum(4, 4) = 1
sum(k + 1, n - 1) = sum(6, 6) = 1

Example 2:
Input: nums = [1,2,1,2,1,2,1,2]
Output: false

Constraints:
n == nums.length
1 <= n <= 2000
-10^6 <= nums[i] <= 10^6
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem548
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(SplitArray(new int[] { 1, 2, 1, 2, 1, 2, 1 }));    // True
            Console.WriteLine(SplitArray(new int[] { 1, 2, 1, 2, 1, 2, 1, 2 })); // False
        }

        // Time: O(n^2) fixing middle cut j and matching prefix halves
        // Space: O(n) for prefix sum array and hash set
        public static bool SplitArray(int[] nums)
        {
            if (nums == null || nums.Length < 7)
            {
                return false;
            }

            int n = nums.Length;
            int[] prefix = new int[n];
            prefix[0] = nums[0];

            for (int idx = 1; idx < n; idx++)
            {
                prefix[idx] = prefix[idx - 1] + nums[idx];
            }

            for (int j = 3; j < n - 3; j++)
            {
                HashSet<int> seenSums = new();

                // Check left half around cut i
                for (int i = 1; i < j - 1; i++)
                {
                    int sum1 = prefix[i - 1];
                    int sum2 = prefix[j - 1] - prefix[i];

                    if (sum1 == sum2)
                    {
                        seenSums.Add(sum1);
                    }
                }

                // Check right half around cut k
                for (int k = j + 2; k < n - 1; k++)
                {
                    int sum3 = prefix[k - 1] - prefix[j];
                    int sum4 = prefix[n - 1] - prefix[k];

                    if (sum3 == sum4 && seenSums.Contains(sum3))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}