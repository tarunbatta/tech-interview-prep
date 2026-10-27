// Url: https://leetcode.com/problems/max-consecutive-ones-ii

/*
487. Max Consecutive Ones II
Medium

Given a binary array nums, return the maximum number of consecutive 1's in the array if you can flip at most one 0.

Example 1:
Input: nums = [1,0,1,1,0]
Output: 4
Explanation: 
- If we flip the first zero, nums becomes [1,1,1,1,0] and we have 4 consecutive ones.
- If we flip the second zero, nums becomes [1,0,1,1,1] and we have 3 consecutive ones.
The max number of consecutive ones is 4.

Example 2:
Input: nums = [1,0,1,1,0,1]
Output: 4
Explanation: 
- If we flip the first zero, nums becomes [1,1,1,1,0,1] and we have 4 consecutive ones.
- If we flip the second zero, nums becomes [1,0,1,1,1,1] and we have 4 consecutive ones.
The max number of consecutive ones is 4.

Constraints:
1 <= nums.length <= 10^5
nums[i] is either 0 or 1.

Follow up: What if the input numbers come in one by one as an infinite stream? In other words, you can't store all numbers coming from the stream as it's too large to hold in memory. Could you solve it efficiently?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem487
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(FindMaxConsecutiveOnes(new int[] { 1, 0, 1, 1, 0 }));    // 4
            Console.WriteLine(FindMaxConsecutiveOnes(new int[] { 1, 0, 1, 1, 0, 1 })); // 4
        }

        // Time: O(n) where n is nums.Length (sliding window)
        // Space: O(1) auxiliary space (stream-friendly)
        public static int FindMaxConsecutiveOnes(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }

            int left = 0;
            int zeroCount = 0;
            int maxConsecutive = 0;

            for (int right = 0; right < nums.Length; right++)
            {
                if (nums[right] == 0)
                {
                    zeroCount++;
                }

                while (zeroCount > 1)
                {
                    if (nums[left] == 0)
                    {
                        zeroCount--;
                    }

                    left++;
                }

                maxConsecutive = Math.Max(maxConsecutive, right - left + 1);
            }

            return maxConsecutive;
        }
    }
}