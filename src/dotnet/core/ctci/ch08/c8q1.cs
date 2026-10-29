/*
Triple Step: 

A child is running up a staircase with n steps and can hop either 1 step, 2 steps, or 3
steps at a time. Implement a method to count how many possible ways the child can run up the
stairs.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c8q1 {
    public class Solution {
        public static int Ways (int n) {
            int[] memo = new int[Math.Max (n + 1, 4)];
            memo[0] = 1;
            memo[1] = 1;
            memo[2] = 2;
            memo[3] = 4;
            for (int i = 4; i <= n; i++) {
                memo[i] = memo[i - 1] + memo[i - 2] + memo[i - 3];
            }
            return memo[n];
        }

        public static void Init (string[] args = null) {
            Console.WriteLine ($"Ways(5): {Ways (5)}");   // 13
            Console.WriteLine ($"Ways(12): {Ways (12)}"); // 927
        }
    }
}
