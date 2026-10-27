// Url:https://leetcode.com/problems/count-primes

/*
204. Count Primes
Easy

Count the number of prime numbers less than a non-negative number, n.

Example:

Input: 10
Output: 4
Explanation: There are 4 prime numbers less than 10, they are 2, 3, 5, 7.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem204 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (CountPrimes(0));
            Console.WriteLine (CountPrimes(3));
            Console.WriteLine (CountPrimes(4));
            Console.WriteLine (CountPrimes(10));
        }

        public static int CountPrimes (int n) {
            int result = 0;

            if (n < 2) {
                return result;
            }

            for (int i = 2; i < n; i++) {
                if (IsPrime(i)) {
                    result++;
                }
            }

            return result;
        }

        private static bool IsPrime (int n) {
            if (n < 2) {
                return false;
            }

            for (int i = 2; i * i <= n; i++) {
                if (n % i == 0) {
                    return false;
                }
            }

            return true;
        }
    }
}