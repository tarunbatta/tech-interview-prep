// Url: https://leetcode.com/problems/add-strings

/*
415. Add Strings
Easy

Given two non-negative integers num1 and num2 represented as string, return the sum of num1 and num2.

Note:

The length of both num1 and num2 is < 5100.
Both num1 and num2 contains only digits 0-9.
Both num1 and num2 does not contain any leading zero.
You must not use any built-in BigInteger library or convert the inputs to integer directly.
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem415 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static string AddStrings (string num1, string num2) {
            if (string.IsNullOrEmpty (num1)) {
                return num2;
            }

            if (string.IsNullOrEmpty (num2)) {
                return num1;
            }

            StringBuilder sb = new StringBuilder ();
            int carry = 0;
            int i = num1.Length - 1;
            int j = num2.Length - 1;

            while (i >= 0 || j >= 0 || carry == 1) {
                int x = i < 0 ? 0 : num1[i] - '0';
                int y = j < 0 ? 0 : num2[j] - '0';

                int sum = carry + x + y;
                sb.Append (sum % 10);

                carry = sum / 10;
                i--;
                j--;
            }

            var s = sb.ToString ().ToCharArray ();
            Array.Reverse (s);
            return new string (s);
        }
    }
}