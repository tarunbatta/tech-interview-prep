// Url: https://leetcode.com/problems/minimum-moves-to-equal-array-elements

/*
453. Minimum Moves to Equal Array Elements
Medium

Given an integer array nums of size n, return the minimum number of moves required to make all array elements equal.

In one move, you can increment n - 1 elements of the array by 1.

Example 1:
Input: nums = [1,2,3]
Output: 3
Explanation: Only three moves are needed (remember each move increments two elements):
[1,2,3]  => [2,3,3]  => [3,4,3]  => [4,4,4]

Example 2:
Input: nums = [1,1,1]
Output: 0

Constraints:
n == nums.length
1 <= nums.length <= 10^5
-10^9 <= nums[i] <= 10^9
The answer is guaranteed to fit in a 32-bit integer.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem453
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(MinMoves(new int[] { 1, 2, 3 })); // 3
            Console.WriteLine(MinMoves(new int[] { 1, 1, 1 })); // 0
        }

        // Time: O(n) single pass to find minimum and sum differences
        // Space: O(1) auxiliary space
        public static int MinMoves(int[] nums)
        {
            if (nums == null || nums.Length <= 1)
            {
                return 0;
            }

            int minVal = int.MaxValue;
            foreach (int num in nums)
            {
                if (num < minVal)
                {
                    minVal = num;
                }
            }

            int moves = 0;
            foreach (int num in nums)
            {
                moves += num - minVal;
            }

            return moves;
        }
    }
}