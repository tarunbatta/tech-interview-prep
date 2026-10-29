/*
Pairwise Swap: 

Write a program to swap odd and even bits in an integer with as few instructions as
possible (e.g., bit 0 and bit 1 are swapped, bit 2 and bit 3 are swapped, and so on).
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c5q7 {
    public class Solution {
        public static int SwapOddEvenBits (int x) {
            return (int)(((uint)(x & unchecked((int)0xaaaaaaaa)) >> 1) | ((uint)(x & 0x55555555) << 1));
        }

        public static void Init (string[] args = null) {
            int n = 538;
            int swapped = SwapOddEvenBits (n);
            Console.WriteLine ($"Swap {n} -> {swapped}");
        }
    }
}
