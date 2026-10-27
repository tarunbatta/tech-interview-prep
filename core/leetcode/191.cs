// Url:https://leetcode.com/problems/number-of-1-bits

/*
191. Number of 1 Bits
Easy

Write a function that takes an unsigned integer and return the number of '1' bits it has (also known as the Hamming weight).

Example 1:

Input: 00000000000000000000000000001011
Output: 3
Explanation: The input binary string 00000000000000000000000000001011 has a total of three '1' bits.

Example 2:

Input: 00000000000000000000000010000000
Output: 1
Explanation: The input binary string 00000000000000000000000010000000 has a total of one '1' bit.

Example 3:

Input: 11111111111111111111111111111101
Output: 31
Explanation: The input binary string 11111111111111111111111111111101 has a total of thirty one '1' bits.

Note:
	Note that in some languages such as Java, there is no unsigned integer type. In this case, the input will be given as signed integer type and should not affect your implementation, as the internal binary representation of the integer is the same whether it is signed or unsigned.
	In Java, the compiler represents the signed integers using 2's complement notation. Therefore, in Example 3 above the input represents the signed integer -3.

Follow up:
If this function is called many times, how would you optimize it?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem191 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (HammingWeight_a(0));
            Console.WriteLine (HammingWeight_a(11));
            Console.WriteLine (HammingWeight_a(128));
            Console.WriteLine (HammingWeight_a(4294967293));

            Console.WriteLine (HammingWeight_b(0));
            Console.WriteLine (HammingWeight_b(11));
            Console.WriteLine (HammingWeight_b(128));
            Console.WriteLine (HammingWeight_b(4294967293));
        }

        public static int HammingWeight_a (uint n) {
            int bits = 0;
            int mask = 1;

            for (int i = 0; i < 32; i++) {
                if ((n & mask) != 0) {
                    bits++;
                }
                mask <<= 1;
            }

            return bits;
        }

        public static int HammingWeight_b (uint n) {
            int sum = 0;

            while (n != 0) {
                sum++;
                n &= (n - 1);
            }

            return sum;
        }
    }
}