// Url: https://leetcode.com/problems/missing-ranges

/*
163. Missing Ranges
Easy

You are given an inclusive range [lower, upper] and a sorted unique integer array nums, where all elements are within the inclusive range.

A number x is considered missing if x is in the range [lower, upper] and x is not in nums.

Return the shortest sorted list of ranges that exactly covers all the missing numbers. That is, no element of nums is in any of the ranges, and each missing number is in one of the ranges.

Each range [a,b] in the list should be output as:
- [a, b] if a <= b

Example 1:
Input: nums = [0,1,3,50,75], lower = 0, upper = 99
Output: [[2,2],[4,49],[51,74],[76,99]]
Explanation: The ranges are:
[2,2]
[4,49]
[51,74]
[76,99]

Example 2:
Input: nums = [-1], lower = -1, upper = -1
Output: []
Explanation: There are no missing ranges since there are no missing numbers.

Constraints:
-10^9 <= lower <= upper <= 10^9
0 <= nums.length <= 100
lower <= nums[i] <= upper
All the values of nums are unique.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem163
{
    public class Solution
    {
        public void Init()
        {
            var res1 = FindMissingRanges(new int[] { 0, 1, 3, 50, 75 }, 0, 99);
            foreach (var r in res1)
            {
                Console.WriteLine($"[{r[0]},{r[1]}]");
            }

            var res2 = FindMissingRanges(new int[] { -1 }, -1, -1);
            Console.WriteLine(res2.Count); // 0
        }

        // Time: O(n) where n is nums.Length
        // Space: O(1) auxiliary space (excluding result list)
        public static IList<IList<int>> FindMissingRanges(int[] nums, int lower, int upper)
        {
            List<IList<int>> result = new();
            long current = lower;

            if (nums != null)
            {
                foreach (int num in nums)
                {
                    if (num > current)
                    {
                        result.Add(new List<int> { (int)current, num - 1 });
                    }

                    current = (long)num + 1;
                }
            }

            if (current <= upper)
            {
                result.Add(new List<int> { (int)current, upper });
            }

            return result;
        }
    }
}