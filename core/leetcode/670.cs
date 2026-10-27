// Url: https://leetcode.com/problems/maximum-swap/

/*
670. Maximum Swap
Medium

Given a non-negative integer, you could swap two digits at most once to get the maximum valued number. Return the maximum valued number you could get.

Example 1:
Input: 2736
Output: 7236
Explanation: Swap the number 2 and the number 7.
Example 2:
Input: 9973
Output: 9973
Explanation: No swap.
Note:
The given number is in the range [0, 10^8]
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem670 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (MaximumSwap_a(0));
            Console.WriteLine (MaximumSwap_a(2736));
            Console.WriteLine (MaximumSwap_a(9973));
            Console.WriteLine (MaximumSwap_a(98368));

            Console.WriteLine (MaximumSwap_b(0));
            Console.WriteLine (MaximumSwap_b(2736));
            Console.WriteLine (MaximumSwap_b(9973));
            Console.WriteLine (MaximumSwap_b(98368));
        }

        public static int MaximumSwap_a (int num) {
            int result = 0;

            if (num == 0) {
                return result;
            }

            string n = num.ToString ();
            result = num;

            for (int i = 0; i < n.Length - 1; i++) {
                for (int j = i + 1; j < n.Length; j++) {
                    string start = (i != 0 ? n.Substring (0, i) : "");
                    string mid = n.Substring (i + 1, j - i - 1);
                    string end = (j == n.Length ? "" : n.Substring (j + 1));
                    string temp = start + n[j] + mid + n[i] + end;

                    if (!string.IsNullOrEmpty (temp) && Convert.ToInt32 (temp) > result) {
                        result = Convert.ToInt32 (temp);
                    }
                }
            }

            return result;
        }

        // At each digit of the input number in order, if there is a larger digit that occurs later, 
        // we know that the best swap must occur with the digit we are currently considering.
        public static int MaximumSwap_b (int num) {
            int result = 0;

            if (num == 0) {
                return result;
            }

            char[] n = num.ToString ().ToCharArray ();

            int[] last = new int[10];
            for (int i = 0; i < n.Length; i++) {
                last[n[i] - '0'] = i;
            }

            for (int i = 0; i < n.Length; i++) {
                for (int d = 9; d > n[i] - '0'; d--) {
                    if (last[d] > i) {
                        char tmp = n[i];
                        n[i] = n[last[d]];
                        n[last[d]] = tmp;
                        return Convert.ToInt32 (new String (n));
                    }
                }
            }

            return num;
        }
    }
}