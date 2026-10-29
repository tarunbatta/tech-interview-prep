/*
Conversion: 

Write a function to determine the number of bits you would need to flip to convert
integer A to integer B.

EXAMPLE
Input: 29 (or: 11101), 15 (or: 01111)
Output: 2
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c5q6 {
    public class Solution {
        public static int BitSwapRequired (int a, int b) {
            int count = 0;
            for (int c = a ^ b; c != 0; c = (int)((uint)c >> 1)) {
                count += c & 1;
            }
            return count;
        }

        public static void Init (string[] args = null) {
            Console.WriteLine ($"Bits to flip 29 -> 15: {BitSwapRequired (29, 15)}");     // 2
            Console.WriteLine ($"Bits to flip 1024 -> 15: {BitSwapRequired (1024, 15)}"); // 5
        }
    }
}
