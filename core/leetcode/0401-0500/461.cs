// Url:https://leetcode.com/problems/hamming-distance

/*
461. Hamming Distance
Easy

The Hamming distance between two integers is the number of positions at which the corresponding bits are different.

Given two integers x and y, calculate the Hamming distance.

Note:
0 ≤ x, y < 231.

Example:
Input: x = 1, y = 4

Output: 2

Explanation:
1   (0 0 0 1)
4   (0 1 0 0)
       ↑   ↑

The above arrows point to positions where the corresponding bits are different.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem461 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (HammingDistance(1, 4));
        }

        public static int HammingDistance (int x, int y) {
            int xor = x ^ y;
            int result = 0;

            for (int i = 0; i < 32; i++) {
                result += (xor >> i) & 1;
            }

            return result;
        }
    }
}