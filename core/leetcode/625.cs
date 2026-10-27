// Url:https://leetcode.com/problems/minimum-factorization

/*
625. Minimum Factorization
Medium

Given a positive 32-bit signed integer a, find the smallest positive integer b whose multiplication of each digit equals to a.

If there is no answer or the answer is not within the range of 32-bit signed integer, then return 0.

Example 1:
Input: 48
Output: 68

Example 2:
Input: 15
Output: 35

Constraints:
- 1 <= a <= 2^31 - 1
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem625 {
    public class Solution {
        public void Init() {
            Console.WriteLine(SmallestFactorization(48)); // 68
            Console.WriteLine(SmallestFactorization(15)); // 35
            Console.WriteLine(SmallestFactorization(1)); // 1
            Console.WriteLine(SmallestFactorization(11)); // 0
        }

        // Time: O(log a)
        // Space: O(1)
        public int SmallestFactorization(int num) {
            if (num < 2) {
                return num;
            }

            long result = 0;
            long multiplier = 1;

            for (int digit = 9; digit >= 2; digit--) {
                while (num % digit == 0) {
                    result += digit * multiplier;
                    multiplier *= 10;
                    num /= digit;

                    if (result > int.MaxValue) {
                        return 0;
                    }
                }
            }

            return num > 1 ? 0 : (int)result;
        }
    }
}