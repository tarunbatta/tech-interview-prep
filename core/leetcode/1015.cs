// Url:https://leetcode.com/problems/smallest-integer-divisible-by-k

/*
1015. Smallest Integer Divisible by K
Medium

Given a positive integer k, you need to find the length of the smallest positive integer n such that n is divisible by k, and n only contains the digit 1.

Return the length of n. If there is no such n, return -1.

Note: n may not fit in a 64-bit signed integer.

Example 1:
Input: k = 1
Output: 1
Explanation: The smallest such n is 1, which has length 1.

Example 2:
Input: k = 2
Output: -1
Explanation: There is no such integer n divisible by 2.

Example 3:
Input: k = 3
Output: 3
Explanation: The smallest such n is 111, which has length 3.

Constraints:
- 1 <= k <= 10^5
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1015
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(SmallestRepunitDivByK(1)); // 1
            Console.WriteLine(SmallestRepunitDivByK(2)); // -1
            Console.WriteLine(SmallestRepunitDivByK(3)); // 3
        }

        // Time: O(k)
        // Space: O(1)
        public static int SmallestRepunitDivByK(int k)
        {
            if (k % 2 == 0 || k % 5 == 0)
            {
                return -1;
            }

            int remainder = 0;
            for (int length = 1; length <= k; length++)
            {
                remainder = (remainder * 10 + 1) % k;
                if (remainder == 0)
                {
                    return length;
                }
            }

            return -1;
        }
    }
}