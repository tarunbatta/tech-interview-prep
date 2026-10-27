// Url: https://leetcode.com/problems/remove-k-digits/

/*
402. Remove K Digits
Medium

Given a non-negative integer num represented as a string, remove k digits from the number so that the new number is the smallest possible.

Note:
The length of num is less than 10002 and will be ≥ k.
The given num does not contain any leading zero.
Example 1:

Input: num = "1432219", k = 3
Output: "1219"
Explanation: Remove the three digits 4, 3, and 2 to form the new number 1219 which is the smallest.
Example 2:

Input: num = "10200", k = 1
Output: "200"
Explanation: Remove the leading 1 and the number is 200. Note that the output must not contain leading zeroes.
Example 3:

Input: num = "10", k = 2
Output: "0"
Explanation: Remove all the digits from the number and it is left with nothing which is 0.
*/

using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem402 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (RemoveKdigits(null, 1));
            Console.WriteLine (RemoveKdigits("1234567890", 9));
            Console.WriteLine (RemoveKdigits("10200", 1));
            Console.WriteLine (RemoveKdigits("1432219", 3));
            Console.WriteLine (RemoveKdigits("10", 2));
        }

        public static string RemoveKdigits (string num, int k) {
            if (string.IsNullOrEmpty (num)) {
                return null;
            } else if (k == 0) {
                return num;
            }

            string result = null;
            Stack stack = new Stack ();
            int i = 0;

            while (i < num.Length) {
                // whenever we meet a digit which is less than the previous digit, discard the previous one
                while (k > 0 && stack.Count != 0 && Convert.ToInt32 ((char) stack.Peek () - '0') > Convert.ToInt32 (num[i] - '0')) {
                    stack.Pop ();
                    k--;
                }

                stack.Push (num[i]);
                i++;
            }

            while (k > 0) {
                stack.Pop ();
                k--;
            }

            StringBuilder stringBuilder = new StringBuilder ();
            while (stack.Count > 0) {
                stringBuilder.Append (stack.Pop ());
            }

            result = new string ([.. stringBuilder.ToString ().Reverse ()]);
            stringBuilder = new StringBuilder (result);

            for (int j = 0; j < stringBuilder.Length; j++) {
                if (stringBuilder[j] == '0') {
                    stringBuilder.Remove (j, 1);
                    j--;
                } else {
                    break;
                }
            }

            result = stringBuilder.Length == 0 ? "0" : stringBuilder.ToString ();

            return result;
        }
    }
}