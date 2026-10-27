// Url: https://leetcode.com/problems/plus-one/

/*
66. Plus One
Easy

Given a non-empty array of digits representing a non-negative integer, plus one to the integer.

The digits are stored such that the most significant digit is at the head of the list, and each element in the array contain a single digit.

You may assume the integer does not contain any leading zero, except the number 0 itself.

Example 1:
Input: [1,2,3]
Output: [1,2,4]
Explanation: The array represents the integer 123.

Example 2:
Input: [4,3,2,1]
Output: [4,3,2,2]
Explanation: The array represents the integer 4321.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem66 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (PlusOne([1, 2, 3]));
            Console.WriteLine (PlusOne([4, 3, 2, 1]));
        }

        public static int[] PlusOne (int[] digits) {
            int carry = 1;

            for (int i = digits.Length - 1; i >= 0; i--) {
                int temp = carry + digits[i];
                int num = temp % 10;
                carry = temp / 10;
                digits[i] = num;
            }

            if (carry == 1) {
                int[] result = new int[digits.Length + 1];
                result[0] = 1;

                for (int i = 0; i < digits.Length; i++) {
                    result[i + 1] = digits[i];
                }

                return result;
            }

            return digits;
        }
    }
}