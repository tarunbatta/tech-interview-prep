// Url: https://leetcode.com/problems/wiggle-sort

/*
280. Wiggle Sort
Medium

Given an integer array nums, reorder it such that nums[0] <= nums[1] >= nums[2] <= nums[3]...

You may assume the input array always has a valid answer.

Example 1:
Input: nums = [3,5,2,1,6,4]
Output: [3,5,1,6,2,4]
Explanation: [1,6,2,5,3,4] is also accepted.

Example 2:
Input: nums = [6,6,5,6,3,8]
Output: [6,6,5,6,3,8]

Constraints:
1 <= nums.length <= 5 * 10^4
0 <= nums[i] <= 10^4
It is guaranteed that there will be an answer for the given input nums.

Follow up: Could you solve it in O(n) time complexity?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem280
{
    public class Solution
    {
        public static void Init()
        {
            int[] nums1 = [3, 5, 2, 1, 6, 4];
            WiggleSort(nums1);
            Console.WriteLine(string.Join(", ", nums1)); // e.g. 3, 5, 1, 6, 2, 4

            int[] nums2 = [6, 6, 5, 6, 3, 8];
            WiggleSort(nums2);
            Console.WriteLine(string.Join(", ", nums2));
        }

        // Time: O(n) where n is nums.Length
        // Space: O(1) in-place
        public static void WiggleSort(int[] nums)
        {
            if (nums == null || nums.Length <= 1)
            {
                return;
            }

            for (int i = 0; i < nums.Length - 1; i++)
            {
                if ((i % 2 == 0 && nums[i] > nums[i + 1]) ||
                    (i % 2 == 1 && nums[i] < nums[i + 1]))
                {
                    (nums[i], nums[i + 1]) = (nums[i + 1], nums[i]);
                }
            }
        }
    }
}