// Url:https://leetcode.com/problems/confusing-number-ii

/*
1088. Confusing Number II
Hard

A confusing number is a number that when rotated 180 degrees becomes a different number with each digit valid.

We can rotate digits of a number by 180 degrees to form new digits.
- When 0, 1, 6, 8, and 9 are rotated 180 degrees, they become 0, 1, 9, 8, and 6 respectively.
- When 2, 3, 4, 5, and 7 are rotated 180 degrees, they become invalid.

Note that after rotating a number, we can ignore leading zeros.
- For example, after rotating 8000, we have 0008 which is considered as just 8.

Given an integer n, return the number of confusing numbers in the inclusive range [1, n].

Example 1:
Input: n = 20
Output: 6
Explanation: The confusing numbers are [6,9,10,16,18,19].
6 converts to 9.
9 converts to 6.
10 converts to 01 which is 1.
16 converts to 91.
18 converts to 81.
19 converts to 61.

Example 2:
Input: n = 100
Output: 19
Explanation: The confusing numbers are [6,9,10,16,18,19,60,61,66,68,80,81,86,89,90,91,98,99,100].

Constraints:
- 1 <= n <= 10^9
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem1088 {
    public class Solution {
        public void Init() {
            Console.WriteLine(ConfusingNumberII(20)); // 6
            Console.WriteLine(ConfusingNumberII(100)); // 19
        }

        private static readonly (int Digit, int Rotated)[] ValidDigits = [
            (0, 0),
            (1, 1),
            (6, 9),
            (8, 8),
            (9, 6)
        ];

        // Time: O(5^(log_10 n))
        // Space: O(log_10 n) for recursion stack
        public int ConfusingNumberII(int n) {
            int count = 0;

            // First digit must be non-zero (1, 6, 8, 9)
            foreach (var (d, rot) in ValidDigits) {
                if (d != 0) {
                    Dfs(d, rot, 10, n, ref count);
                }
            }

            return count;
        }

        private static void Dfs(long num, long rotated, long place, int n, ref int count) {
            if (num > n) {
                return;
            }

            if (num != rotated) {
                count++;
            }

            foreach (var (d, rot) in ValidDigits) {
                long nextNum = num * 10 + d;
                if (nextNum > n) {
                    continue;
                }
                long nextRot = rot * place + rotated;
                Dfs(nextNum, nextRot, place * 10, n, ref count);
            }
        }
    }
}