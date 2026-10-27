// Url: https://leetcode.com/problems/perfect-number/

/*
507. Perfect Number
Easy

We define the Perfect Number is a positive integer that is equal to the sum of all its positive divisors except itself.

Now, given an integer n, write a function that returns true when it is a perfect number and false when it is not.

Example:
Input: 28
Output: True
Explanation: 28 = 1 + 2 + 4 + 7 + 14

Note: The input number n will not exceed 100,000,000. (1e8)
*/
using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem507 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static bool CheckPerfectNumber (int num) {
            if (num < 0 || num % 2 != 0) {
                return false;
            }

            int sum = 1;

            for (int i = 2; i * i < num; i++) {
                if (num % i == 0) {
                    sum += i + num / i;;
                }
            }

            return num == sum;
        }
    }
}