/*
Number Max: 

Write a method that finds the maximum of two numbers. You should not use if-else
or any other comparison operator.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c16q7 {
    public class Solution {
        public static int GetMax (int a, int b) {
            // Let diff = a - b
            long diff = (long)a - b;
            // Sign bit: 1 if diff < 0, 0 if diff >= 0
            long sign = (diff >> 63) & 1L;
            // if a >= b, sign is 0, return a; if b > a, sign is 1, return b
            return (int)(a - sign * diff);
        }

        public static void Init (string[] args = null) {
            Console.WriteLine ($"Max(28, 92) = {GetMax (28, 92)}");
            Console.WriteLine ($"Max(100, -50) = {GetMax (100, -50)}");
        }
    }
}
