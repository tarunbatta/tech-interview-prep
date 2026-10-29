// Url:https://leetcode.com/problems/climbing-stairs

/*
70. Climbing Stairs
Easy

You are climbing a stair case. It takes n steps to reach to the top.

Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?

Note: Given n will be a positive integer.

Example 1:

Input: 2
Output: 2
Explanation: There are two ways to climb to the top.
1. 1 step + 1 step
2. 2 steps


Example 2:

Input: 3
Output: 3
Explanation: There are three ways to climb to the top.
1. 1 step + 1 step + 1 step
2. 1 step + 2 steps
3. 2 steps + 1 step
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem70 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (ClimbStairs_a(0));
            Console.WriteLine (ClimbStairs_a(1));
            Console.WriteLine (ClimbStairs_a(2));
            Console.WriteLine (ClimbStairs_a(3));
            Console.WriteLine (ClimbStairs_a(4));
            Console.WriteLine (ClimbStairs_a(5));
        }

        public static int ClimbStairs_a (int n) {
            return ClimbStairsHelper_a(0, n);
        }

        private static int ClimbStairsHelper_a (int i, int n) {
            if (i > n) {
                return 0;
            }

            if (i == n) {
                return 1;
            }

            return ClimbStairsHelper_a(i + 1, n) + ClimbStairsHelper_a(i + 2, n);
        }

        public static int ClimbStairs_b (int n) {
            int[] memo = new int[n + 1];
            return ClimbStairsHelper_b(0, n, memo);
        }

        private static int ClimbStairsHelper_b (int i, int n, int[] memo) {
            if (i > n) {
                return 0;
            }

            if (i == n) {
                return 1;
            }

            if (memo[i] > 0) {
                return memo[i];
            }

            memo[i] = ClimbStairsHelper_b(i + 1, n, memo) + ClimbStairsHelper_b(i + 2, n, memo);

            return memo[i];
        }

        public static int ClimbStairs_c (int n) {
            if (n == 1) {
                return 1;
            }

            int[] dp = new int[n + 1];
            dp[1] = 1;
            dp[2] = 2;

            for (int i = 3; i <= n; i++) {
                dp[i] = dp[i - 1] + dp[i - 2];
            }

            return dp[n];
        }

        public static int ClimbStairs_d (int n) {
            if (n == 1) {
                return 1;
            }

            int first = 1;
            int second = 2;

            for (int i = 3; i <= n; i++) {
                int third = first + second;
                first = second;
                second = third;
            }

            return second;
        }

        public static int ClimbStairs_e (int n) {
            double sqrt5 = Math.Sqrt (5);
            double fibn = Math.Pow ((1 + sqrt5) / 2, n + 1) - Math.Pow ((1 - sqrt5) / 2, n + 1);
            return (int) (fibn / sqrt5);
        }
    }
}