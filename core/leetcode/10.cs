// Url: https://leetcode.com/problems/regular-expression-matching/

/*
10. Regular Expression Matching
Hard

Given an input string (s) and a pattern (p), implement regular expression matching with support for '.' and '*'.

'.' Matches any single character.
'*' Matches zero or more of the preceding element.
The matching should cover the entire input string (not partial).

Note:

s could be empty and contains only lowercase letters a-z.
p could be empty and contains only lowercase letters a-z, and characters like . or *.
Example 1:

Input:
s = "aa"
p = "a"
Output: false
Explanation: "a" does not match the entire string "aa".
Example 2:

Input:
s = "aa"
p = "a*"
Output: true
Explanation: '*' means zero or more of the preceding element, 'a'. Therefore, by repeating 'a' once, it becomes "aa".
Example 3:

Input:
s = "ab"
p = ".*"
Output: true
Explanation: ".*" means "zero or more (*) of any character (.)".
Example 4:

Input:
s = "aab"
p = "c*a*b"
Output: true
Explanation: c can be repeated 0 times, a can be repeated 1 time. Therefore, it matches "aab".
Example 5:

Input:
s = "mississippi"
p = "mis*is*p*."
Output: false
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem10 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (sp)
        // Space: O (sp)
        public static bool IsMatch (string s, string p) {
            bool[, ] dp = new bool[s.Length + 1, p.Length + 1];
            dp[0, 0] = true;

            // deals with patterns line a* or a*b* or a*b*c*
            // dp[0,i] means if patter.Substring(0, i) matches empty string
            for (int i = 1; i < dp.GetLength (1); i++) {
                if (p[i - 1] == '*') {
                    dp[0, i] = dp[0, i - 2];
                }
            }

            for (int i = 1; i < dp.GetLength (0); i++) {
                for (int j = 1; j < dp.GetLength (1); j++) {
                    if (p[j - 1] == '.' || s[i - 1] == p[j - 1]) {
                        dp[i, j] = dp[i - 1, j - 1];
                    } else if (p[j - 1] == '*') {
                        dp[i, j] = dp[i, j - 2];

                        if (p[j - 2] == '.' || s[i - 1] == p[j - 2]) {
                            dp[i, j] = dp[i, j] | dp[i - 1, j];
                        }
                    } else {
                        dp[i, j] = false;
                    }
                }
            }

            return dp[s.Length, p.Length];
        }
    }
}