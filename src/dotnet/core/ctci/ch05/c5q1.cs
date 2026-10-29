/*
Insertion: 

You are given two 32-bit numbers, N and M, and two bit positions, i and
j. Write a method to insert M into N such that M starts at bit j and ends at bit i. You
can assume that the bits j through i have enough space to fit all of M. That is, if
M = 10011, you can assume that there are at least 5 bits between j and i. You would not, for
example, have j = 3 and i = 2, because M could not fully fit between bit 3 and bit 2.

EXAMPLE
Input: N 10000000000, M 10011, i 2, j 6
Output: N 10001001100
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c5q1 {
    public class Solution {
        public static int UpdateBit (int num, int i, bool bitIs1) {
            int value = bitIs1 ? 1 : 0;
            int mask = ~(1 << i);
            return (num & mask) | (value << i);
        }

        public static int Insertion (int n, int m, int i, int j) {
            int k = 0;
            for (int pos = i; pos <= j; pos++) {
                n = UpdateBit (n, pos, (m & (1 << k)) != 0);
                k++;
            }
            return n;
        }

        public static void Init (string[] args = null) {
            int n = 1024; // 10000000000 in binary
            int m = 19;   // 10011 in binary
            int res = Insertion (n, m, 2, 6);
            Console.WriteLine ($"Result: {res} (binary: {Convert.ToString (res, 2)})"); // 1100 (10001001100)
        }
    }
}
