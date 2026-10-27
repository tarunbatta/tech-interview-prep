// Url:https://leetcode.com/problems/wildcard-matching

/*
44. Wildcard Matching
Hard

Given an input string (s) and a pattern (p), implement wildcard pattern matching with support for '?' and '*'.

'?' Matches any single character.
'*' Matches any sequence of characters (including the empty sequence).


The matching should cover the entire input string (not partial).

Note:
	s could be empty and contains only lowercase letters a-z.
	p could be empty and contains only lowercase letters a-z, and characters like ? or *.


Example 1:

Input:
s = "aa"
p = "a"
Output: false
Explanation: "a" does not match the entire string "aa".


Example 2:

Input:
s = "aa"
p = "*"
Output: true
Explanation: '*' matches any sequence.


Example 3:

Input:
s = "cb"
p = "?a"
Output: false
Explanation: '?' matches 'c', but the second letter is 'a', which does not match 'b'.


Example 4:

Input:
s = "adceb"
p = "*a*b"
Output: true
Explanation: The first '*' matches the empty sequence, while the second '*' matches the substring "dce".


Example 5:

Input:
s = "acdcb"
p = "a*c?b"
Output: false
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem44 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (IsMatch("aa", "a"));
            Console.WriteLine (IsMatch("aa", "*"));
            Console.WriteLine (IsMatch("cb", "?a"));
            Console.WriteLine (IsMatch("adceb", "*a*b"));
            Console.WriteLine (IsMatch("acdcb", "a*c?b"));
        }

        // Time: O (n)
        // Space: O (1)
        public static bool IsMatch (string s, string p) {
            int i = 0;
            int j = 0;
            int match = 0;
            int starIdx = -1;

            while (i < s.Length) {
                // advancing both pointers
                if (j < p.Length && (p[j] == '?' || s[i] == p[j])) {
                    i++;
                    j++;
                }
                // * found, only advancing pattern pointer
                else if (j < p.Length && p[j] == '*') {
                    starIdx = j;
                    match = i;
                    j++;
                }
                // last pattern pointer was *, advancing string pointer
                else if (starIdx != -1) {
                    j = starIdx + 1;
                    match++;
                    i = match;
                }
                //current pattern pointer is not star, last patter pointer was not *
                //characters do not match
                else return false;
            }

            //check for remaining characters in pattern
            while (j < p.Length && p[j] == '*')
                j++;

            return j == p.Length;
        }
    }
}