// Url: https://leetcode.com/problems/k-th-smallest-in-lexicographical-order/

/*
440. K-th Smallest in Lexicographical Order
Hard

Given integers n and k, find the lexicographically k-th smallest integer in the range from 1 to n.

Note: 1 ≤ k ≤ n ≤ 109.

Example:

Input:
n: 13   k: 2

Output:
10

Explanation:
The lexicographical order is [1, 10, 11, 12, 13, 2, 3, 4, 5, 6, 7, 8, 9], so the second smallest number is 10.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem440 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (FindKthNumber(13, 2));
        }

        public static int FindKthNumber (int n, int k) {
            if (n == 0 || k == 0 || k > n) {
                return 0;
            }

            int result = 1;
            k = k - 1;

            while (k > 0) {
                int steps = CallSteps(n, result, result + 1);
                if (steps <= k) {
                    result += 1;
                    k -= steps;
                } else {
                    result *= 10;
                    k -= 1;
                }
            }

            return result;
        }

        public static int CallSteps (int n, int n1, int n2) {
            int result = 0;

            while (n1 <= n) {
                result += Math.Min (n + 1, n2) - n1;
                n1 *= 10;
                n2 *= 10;
            }

            return result;
        }
    }
}