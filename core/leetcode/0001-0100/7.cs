// Url: https://leetcode.com/problems/reverse-integer/

/*
7. Reverse Integer
Easy

Given a 32-bit signed integer, reverse digits of an integer.

Example 1:

Input: 123
Output: 321
Example 2:

Input: -123
Output: -321
Example 3:

Input: 120
Output: 21
Note:
Assume we are dealing with an environment which could only store integers within the 32-bit signed integer range: [−231,  231 − 1]. For the purpose of this problem, assume that your function returns 0 when the reversed integer overflows.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem7 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (Reverse(0));
            Console.WriteLine (Reverse(123));
            Console.WriteLine (Reverse(-321));
            Console.WriteLine (Reverse(120));
        }

        // Time: O(log(x))
        // Space: O(1)
        public static int Reverse (int x) {
            int result = 0;

            while (x != 0) {
                int pop = x % 10;
                x /= 10;

                if (result > Int32.MaxValue / 10 || (result == Int32.MaxValue / 10 && pop > 7)) return 0;
                if (result < Int32.MinValue / 10 || (result == Int32.MinValue / 10 && pop < -8)) return 0;

                result = result * 10 + pop;
            }

            return result;
        }
    }
}