/*
Flip Bit to Win: 

You have an integer and you can flip exactly one bit from a 0 to a 1. Write code to
find the length of the longest sequence of 1 s you could create.

EXAMPLE
Input: 1775 (or: 11011101111)
Output: 8
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c5q3 {
    public class Solution {
        public static int LongestSequence (int n) {
            int maxSeq = 0;
            for (int i = 0; i < 32; i++) {
                maxSeq = Math.Max (maxSeq, LongestSequenceOfOnes (n, i));
            }
            return maxSeq;
        }

        private static int LongestSequenceOfOnes (int n, int ignore) {
            int longestSeq = 0;
            int seq = 0;
            for (int i = 0; i < 32; i++) {
                if (GetBit (n, i) || i == ignore) {
                    seq++;
                    longestSeq = Math.Max (seq, longestSeq);
                } else {
                    seq = 0;
                }
            }
            return longestSeq;
        }

        private static bool GetBit (int num, int i) {
            return (num & (1 << i)) != 0;
        }

        public static void Init (string[] args = null) {
            Console.WriteLine ($"Longest seq (1775): {LongestSequence (1775)}"); // 8
            Console.WriteLine ($"Longest seq (122): {LongestSequence (122)}");   // 5
        }
    }
}
