// Url: https://leetcode.com/problems/largest-perimeter-triangle/

/*
976. Largest Perimeter Triangle
Easy

Given an integer array nums, return the largest perimeter of a triangle with a non-zero area, formed from three of these lengths. If it is impossible to form any triangle of a non-zero area, return 0.

Example 1:
Input: nums = [2,1,2]
Output: 5
Explanation: You can form a triangle with three side lengths: 1, 2, and 2.

Example 2:
Input: nums = [1,2,1,10]
Output: 0
Explanation: 
You cannot use the side lengths 1, 1, and 2 to form a triangle.
You cannot use the side lengths 1, 1, and 10 to form a triangle.
You cannot use the side lengths 1, 2, and 10 to form a triangle.
As we cannot use any three side lengths to form a triangle of non-zero area, we return 0.

Constraints:
3 <= nums.length <= 10^4
1 <= nums[i] <= 10^6
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem976
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(LargestPerimeter([2, 1, 2])); // 5
            Console.WriteLine(LargestPerimeter([1, 2, 1, 10])); // 0
        }

        // Time: O(n log n) where n is nums.Length (dominated by sorting)
        // Space: O(1) auxiliary space (or O(log n) for intro-sort stack)
        public static int LargestPerimeter(int[] nums)
        {
            if (nums == null || nums.Length < 3)
            {
                return 0;
            }

            Array.Sort(nums);

            for (int i = nums.Length - 1; i >= 2; i--)
            {
                if (nums[i - 2] + nums[i - 1] > nums[i])
                {
                    return nums[i - 2] + nums[i - 1] + nums[i];
                }
            }

            return 0;
        }
    }
}
