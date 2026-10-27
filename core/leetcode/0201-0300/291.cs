// Url: https://leetcode.com/problems/word-pattern-ii

/*
291. Word Pattern II
Hard

Given a pattern and a string s, return true if s matches the pattern.

A string s matches a pattern if there is some bijection mapping of single characters to non-empty strings such that if each character in pattern is replaced by the string it maps to, then the resulting string is s. A bijection mapping means that no two characters map to the same string, and no character maps to two different strings.

Example 1:
Input: pattern = "abab", s = "redblueredblue"
Output: true
Explanation: One possible mapping is as follows:
'a' -> "red"
'b' -> "blue"

Example 2:
Input: pattern = "aaaa", s = "asdasdasdasd"
Output: true
Explanation: One possible mapping is as follows:
'a' -> "asd"

Example 3:
Input: pattern = "aabb", s = "xyzabcxzyabc"
Output: false

Constraints:
1 <= pattern.length, s.length <= 20
pattern and s consist of only lowercase English letters.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem291
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(WordPatternMatch("abab", "redblueredblue")); // True
            Console.WriteLine(WordPatternMatch("aaaa", "asdasdasdasd"));   // True
            Console.WriteLine(WordPatternMatch("aabb", "xyzabcxzyabc"));   // False
        }

        // Time: O(C(s.Length, pattern.Length)) worst case with backtracking and pruning
        // Space: O(pattern.Length + s.Length) for mapping dictionaries and call stack
        public static bool WordPatternMatch(string pattern, string s)
        {
            if (pattern == null || s == null)
            {
                return false;
            }

            Dictionary<char, string> p2s = [];
            HashSet<string> sSet = [];

            return Backtrack(pattern, 0, s, 0, p2s, sSet);
        }

        private static bool Backtrack(string pattern, int pIdx, string s, int sIdx, Dictionary<char, string> p2s, HashSet<string> sSet)
        {
            if (pIdx == pattern.Length && sIdx == s.Length)
            {
                return true;
            }

            if (pIdx == pattern.Length || sIdx == s.Length)
            {
                return false;
            }

            char c = pattern[pIdx];

            if (p2s.TryGetValue(c, out string mapped))
            {
                if (sIdx + mapped.Length <= s.Length && s.AsSpan(sIdx, mapped.Length).SequenceEqual(mapped.AsSpan()))
                {
                    return Backtrack(pattern, pIdx + 1, s, sIdx + mapped.Length, p2s, sSet);
                }

                return false;
            }

            for (int len = 1; sIdx + len <= s.Length; len++)
            {
                string sub = s.Substring(sIdx, len);

                if (sSet.Contains(sub))
                {
                    continue;
                }

                p2s[c] = sub;
                sSet.Add(sub);

                if (Backtrack(pattern, pIdx + 1, s, sIdx + len, p2s, sSet))
                {
                    return true;
                }

                p2s.Remove(c);
                sSet.Remove(sub);
            }

            return false;
        }
    }
}