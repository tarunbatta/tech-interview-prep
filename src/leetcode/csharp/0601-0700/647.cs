// Url:https://leetcode.com/problems/palindromic-substrings

/*
647. Palindromic Substrings
Medium

Given a string, your task is to count how many palindromic substrings in this string.

The substrings with different start indexes or end indexes are counted as different substrings even they consist of same characters.

Example 1:

Input: "abc"
Output: 3
Explanation: Three palindromic strings: "a", "b", "c".


Example 2:

Input: "aaa"
Output: 6
Explanation: Six palindromic strings: "a", "a", "a", "aa", "aa", "aaa".


Note:
	The input string length won't exceed 1000.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem647 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        int count = 0;

        // Time: O (n^2)
        // Space: O (1)
        public int CountSubstrings (string s) {
            if (string.IsNullOrEmpty (s)) {
                return count;
            }

            for (int i = 0; i < s.Length; i++) {
                //assume odd length, try to extend Palindrome as possible
                expandAroundCenter (s, i, i);

                //assume even length.
                expandAroundCenter (s, i, i + 1);
            }

            return count;
        }

        private void expandAroundCenter (string s, int j, int k) {
            while (j >= 0 && k < s.Length && s[j] == s[k]) {
                count++;
                j--;
                k++;
            }
        }
    }
}