// Url: https://leetcode.com/problems/3sum-smaller

/*
259. 3Sum Smaller
Medium

Given an array of n integers nums and an integer target, find the number of index triplets i, j, k with 0 <= i < j < k < n that satisfy the condition nums[i] + nums[j] + nums[k] < target.

Example 1:
Input: nums = [-2,0,1,3], target = 2
Output: 2
Explanation: Because there are two triplets which sums are less than 2:
[-2,0,1]
[-2,0,3]

Example 2:
Input: nums = [], target = 0
Output: 0

Example 3:
Input: nums = [0], target = 0
Output: 0

Constraints:
n == nums.length
0 <= n <= 3500
-100 <= nums[i] <= 100
-100 <= target <= 100
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem259
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(ThreeSumSmaller([-2, 0, 1, 3], 2)); // 2
            Console.WriteLine(ThreeSumSmaller([], 0));         // 0
        }

        // Time: O(n^2) where n is nums.Length
        // Space: O(1) auxiliary space (ignoring sorting stack)
        public static int ThreeSumSmaller(int[] nums, int target)
        {
            if (nums == null || nums.Length < 3)
            {
                return 0;
            }

            Array.Sort(nums);
            int count = 0;

            for (int i = 0; i < nums.Length - 2; i++)
            {
                int left = i + 1;
                int right = nums.Length - 1;

                while (left < right)
                {
                    int sum = nums[i] + nums[left] + nums[right];

                    if (sum < target)
                    {
                        count += right - left;
                        left++;
                    }
                    else
                    {
                        right--;
                    }
                }
            }

            return count;
        }
    }
}