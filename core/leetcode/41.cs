// Url:https://leetcode.com/problems/first-missing-positive

/*
41. First Missing Positive
Hard

Given an unsorted integer array, find the smallest missing positive integer.

Example 1:

Input: [1,2,0]
Output: 3


Example 2:

Input: [3,4,-1,1]
Output: 2


Example 3:

Input: [7,8,9,11,12]
Output: 1


Note:

Your algorithm should run in O(n) time and uses constant extra space.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem41
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(FirstMissingPositive(null));                 // Expected: 1
            Console.WriteLine(FirstMissingPositive([]));                   // Expected: 1
            Console.WriteLine(FirstMissingPositive([1, 2, 0]));            // Expected: 3
            Console.WriteLine(FirstMissingPositive([3, 4, -1, 1]));        // Expected: 2
            Console.WriteLine(FirstMissingPositive([7, 8, 9, 11, 12]));    // Expected: 1
        }

        // Time: O(n) where n is the length of nums (each number is placed in its home position at most once)
        // Space: O(1) auxiliary space (in-place cyclic sort)
        public static int FirstMissingPositive(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return 1;
            }

            int n = nums.Length;

            for (int i = 0; i < n; i++)
            {
                while (nums[i] > 0 && nums[i] <= n && nums[nums[i] - 1] != nums[i])
                {
                    Swap(nums, i, nums[i] - 1);
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (nums[i] != i + 1)
                {
                    return i + 1;
                }
            }

            return n + 1;
        }

        private static void Swap(int[] nums, int i, int j)
        {
            (nums[i], nums[j]) = (nums[j], nums[i]);
        }
    }
}