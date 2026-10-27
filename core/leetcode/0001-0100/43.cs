// Url: https://leetcode.com/problems/multiply-strings/

/*
43. Multiply Strings
Medium

Given two non-negative integers num1 and num2 represented as strings, return the product of num1 and num2, also represented as a string.

Example 1:

Input: num1 = "2", num2 = "3"
Output: "6"
Example 2:

Input: num1 = "123", num2 = "456"
Output: "56088"
Note:

The length of both num1 and num2 is < 110.
Both num1 and num2 contain only digits 0-9.
Both num1 and num2 do not contain any leading zero, except the number 0 itself.
You must not use any built-in BigInteger library or convert the inputs to integer directly.
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem43 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static string Multiply (string num1, string num2) {
            int[] res = new int[num1.Length + num2.Length];

            for (int i = num1.Length - 1; i >= 0; i--) {
                for (int j = num2.Length - 1; j >= 0; j--) {
                    //num1[i] * num2[j] will be placed at indices [i + j, i + j + 1]
                    int p1 = i + j;
                    int p2 = i + j + 1;

                    int mult = (num2[j] - '0') * (num1[i] - '0');
                    int sum = mult + res[p2];

                    res[p2] = sum % 10;
                    res[p1] += (sum / 10);
                }
            }

            StringBuilder ret = new();
            for (int i = 0; i < res.Length; i++) {
                if (ret.Length == 0 && res[i] == 0) continue;
                ret.Append (res[i]);
            }

            return ret.Length == 0 ? "0" : ret.ToString ();
        }
    }
}