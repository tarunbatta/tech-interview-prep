/*
Recursive Multiply: 

Write a recursive function to multiply two positive integers without using the
* operator.You can use addition, subtraction, and bit shifting, but you should minimize the number
of operations.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c8q5 {
    public class Solution {
        public static int Multiply (int a, int b) {
            int smaller = a < b ? a : b;
            int bigger = a < b ? b : a;
            return MinOpsMultiplyHelper (smaller, bigger);
        }

        private static int MinOpsMultiplyHelper (int smaller, int bigger) {
            if (smaller == 0) return 0;
            if (smaller == 1) return bigger;

            int s = smaller >> 1;
            int halfProd = MinOpsMultiplyHelper (s, bigger);

            if (smaller % 2 == 0) {
                return halfProd + halfProd;
            } else {
                return halfProd + halfProd + bigger;
            }
        }

        public static void Init (string[] args = null) {
            Console.WriteLine ($"5 * 3 = {Multiply (5, 3)}");
            Console.WriteLine ($"25 * 4 = {Multiply (25, 4)}");
            Console.WriteLine ($"9 * 9 = {Multiply (9, 9)}");
        }
    }
}
