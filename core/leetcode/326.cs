// Url:https://leetcode.com/problems/power-of-three

/*
326. Power of Three
Easy

Given an integer, write a function to determine if it is a power of three.

Example 1:

Input: 27
Output: true


Example 2:

Input: 0
Output: false

Example 3:

Input: 9
Output: true

Example 4:

Input: 45
Output: false

Follow up:
Could you do it without using any loop / recursion?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem326 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (IsPowerOfThree(27));
            Console.WriteLine (IsPowerOfThree(0));
            Console.WriteLine (IsPowerOfThree(9));
            Console.WriteLine (IsPowerOfThree(45));
        }

        public static bool IsPowerOfThree (int n) {
            if (n < 1) {
                return false;
            }

            while (n % 3 == 0) {
                n /= 3;
            }

            return n == 1;
        }
    }
}