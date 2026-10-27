// Url:https://leetcode.com/problems/armstrong-number

/*
1134. Armstrong Number
Easy

Given an integer n, return true if and only if it is an Armstrong number.

The k-digit number n is an Armstrong number if and only if the kth power of each digit sums to n.

Example 1:
Input: n = 153
Output: true
Explanation: 153 is a 3-digit number, and 153 = 1^3 + 5^3 + 3^3.

Example 2:
Input: n = 123
Output: false
Explanation: 123 is a 3-digit number, and 123 != 1^3 + 2^3 + 3^3 = 36.

Constraints:
- 1 <= n <= 10^8
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1134 {
    public class Solution {
        public void Init() {
            Console.WriteLine(IsArmstrong(153)); // True
            Console.WriteLine(IsArmstrong(123)); // False
        }

        // Time: O(log_10 n)
        // Space: O(1)
        public bool IsArmstrong(int n) {
            int k = 0;
            int temp = n;
            while (temp > 0) {
                k++;
                temp /= 10;
            }

            temp = n;
            long sum = 0;
            while (temp > 0) {
                int digit = temp % 10;
                long p = 1;
                for (int i = 0; i < k; i++) {
                    p *= digit;
                }
                sum += p;
                temp /= 10;
            }

            return sum == n;
        }
    }
}