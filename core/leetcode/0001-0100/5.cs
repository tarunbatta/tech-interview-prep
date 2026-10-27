// Url: https://leetcode.com/problems/longest-palindromic-substring/

/*
5. Longest Palindromic Substring
Medium

Given a string s, find the longest palindromic substring in s. You may assume that the maximum length of s is 1000.

Example 1:

Input: "babad"
Output: "bab"
Note: "aba" is also a valid answer.

Example 2:

Input: "cbbd"
Output: "bb"
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem5 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (LongestPalindrome (null));
            Console.WriteLine (LongestPalindrome (""));
            Console.WriteLine (LongestPalindrome ("a"));
            Console.WriteLine (LongestPalindrome ("babad"));
            Console.WriteLine (LongestPalindrome ("cbbd"));
            Console.WriteLine (LongestPalindrome ("babcbaabcbaccba"));

            // TODO: Manacher's Algorithm
        }

        private static int start;
        private static int end;

        // Time: O (n^2)
        // Space: O (1)
        public static string LongestPalindrome (string s) {
            start = 0;
            end = 0;

            int len = s.Length;

            if (len < 2) {
                return s;
            }

            for (int i = 0; i < len - 1; i++) {
                //assume odd length, try to extend Palindrome as possible
                expandAroundCenter (s, i, i);

                //assume even length.
                expandAroundCenter (s, i, i + 1);
            }

            return s.Substring (start, end);
        }

        private static void expandAroundCenter (string s, int j, int k) {
            while (j >= 0 && k < s.Length && s[j] == s[k]) {
                j--;
                k++;
            }

            if (end < k - j - 1) {
                start = j + 1;
                end = k - j - 1;
            }
        }
    }
}