// Url:https://leetcode.com/problems/perfect-squares

/*
279. Perfect Squares
Medium

Given a positive integer n, find the least number of perfect square numbers (for example, 1, 4, 9, 16, ...) which sum to n.

Example 1:

Input: n = 12
Output: 3 
Explanation: 12 = 4 + 4 + 4.

Example 2:

Input: n = 13
Output: 2
Explanation: 13 = 4 + 9.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem279 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (NumSquares(-1));
            Console.WriteLine (NumSquares(0));
            Console.WriteLine (NumSquares(1));
            Console.WriteLine (NumSquares(5));
            Console.WriteLine (NumSquares(12));
            Console.WriteLine (NumSquares(13));
            Console.WriteLine (NumSquares(16));
        }

        public static int NumSquares (int n) {
            if (n <= 0) {
                return 0;
            }

            int num = (int) Math.Sqrt (n);

            if (num * num == n) {
                return 1;
            }

            int[] result = new int[n + 1];

            for (int i = 0; i < n + 1; i++) {
                result[i] = Int32.MaxValue;
            }

            result[0] = 0;

            for (int i = 1; i * i <= n; i++) {
                result[i * i] = 1;

                for (int j = i * i + 1; j <= n; j++) {
                    result[j] = Math.Min (result[j], result[j - i * i] + 1);
                }
            }

            return result[n];
        }
    }
}