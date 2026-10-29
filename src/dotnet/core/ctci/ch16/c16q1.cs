/*
Number Swapper: 

Write a function to swap a number in place (that is, without temporary variables).
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c16q1 {
    public class Solution {
        public static void Swap (ref int a, ref int b) {
            a ^= b;
            b ^= a;
            a ^= b;
        }

        public static void Init (string[] args = null) {
            int a = 5;
            int b = 9;
            Console.WriteLine ($"Before swap: a={a}, b={b}");
            Swap (ref a, ref b);
            Console.WriteLine ($"After swap: a={a}, b={b}");
        }
    }
}
