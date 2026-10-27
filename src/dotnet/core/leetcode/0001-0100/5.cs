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

            // Manacher's Algorithm
            Console.WriteLine (LongestPalindromeManacher (""));
            Console.WriteLine (LongestPalindromeManacher ("a"));
            Console.WriteLine (LongestPalindromeManacher ("babad")); // bab
            Console.WriteLine (LongestPalindromeManacher ("cbbd")); // bb
            Console.WriteLine (LongestPalindromeManacher ("babcbaabcbaccba")); // abcbaabcba
        }

        private static int start;
        private static int end;

        // Time: O (n^2)
        // Space: O (1)
        public static string LongestPalindrome (string s) {
            start = 0;
            end = 0;

            if (s == null || s.Length < 2) {
                return s;
            }

            int len = s.Length;

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

        // Manacher's Algorithm
        // Transform "abc" into "^#a#b#c#$" so every palindrome has odd length,
        // then reuse previously computed radii via mirror positions around the
        // right-most palindrome center.
        // Time: O (n)
        // Space: O (n)
        public static string LongestPalindromeManacher (string s) {
            if (string.IsNullOrEmpty (s) || s.Length < 2) {
                return s;
            }

            StringBuilder sb = new StringBuilder ("^");
            foreach (char c in s) {
                sb.Append ('#').Append (c);
            }
            sb.Append ("#$");

            string t = sb.ToString ();
            int[] p = new int[t.Length];
            int center = 0, right = 0;

            for (int i = 1; i < t.Length - 1; i++) {
                if (i < right) {
                    p[i] = Math.Min (right - i, p[2 * center - i]);
                }

                while (t[i + p[i] + 1] == t[i - p[i] - 1]) {
                    p[i]++;
                }

                if (i + p[i] > right) {
                    center = i;
                    right = i + p[i];
                }
            }

            int maxLen = 0, centerIndex = 0;
            for (int i = 1; i < t.Length - 1; i++) {
                if (p[i] > maxLen) {
                    maxLen = p[i];
                    centerIndex = i;
                }
            }

            return s.Substring ((centerIndex - maxLen) / 2, maxLen);
        }
    }
}