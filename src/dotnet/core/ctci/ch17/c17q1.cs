/*
Add Without Plus: 

Write a function that adds two numbers. You should not use + or any arithmetic operators.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c17q1 {
    public class Solution {
        public static int Add (int a, int b) {
            while (b != 0) {
                int sum = a ^ b;
                int carry = (a & b) << 1;
                a = sum;
                b = carry;
            }
            return a;
        }

        public static void Init (string[] args = null) {
            Console.WriteLine ($"12 + 34 = {Add (12, 34)}");
            Console.WriteLine ($"150 + 250 = {Add (150, 250)}");
        }
    }
}
