// Url: https://leetcode.com/problems/one-edit-distance

/*
161. One Edit Distance
Medium

Given two strings s and t, return true if they are both one edit distance apart, otherwise return false.

An edit between two strings is one of the following:
- Insert exactly one character into s to get t.
- Delete exactly one character from s to get t.
- Replace exactly one character of s with a different character to get t.

Example 1:
Input: s = "ab", t = "acb"
Output: true
Explanation: We can insert 'c' into s to get t.

Example 2:
Input: s = "", t = ""
Output: false
Explanation: We cannot get t from s by only one step.

Constraints:
0 <= s.length, t.length <= 10^4
s and t consist of lowercase letters, uppercase letters, and digits.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem161
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(IsOneEditDistance("ab", "acb")); // True
            Console.WriteLine(IsOneEditDistance("", ""));      // False
            Console.WriteLine(IsOneEditDistance("a", ""));     // True
            Console.WriteLine(IsOneEditDistance("ab", "ab"));  // False
        }

        // Time: O(n) where n is min(s.Length, t.Length)
        // Space: O(1) auxiliary space using ReadOnlySpan slicing
        public static bool IsOneEditDistance(string s, string t)
        {
            s ??= string.Empty;
            t ??= string.Empty;

            if (s.Length > t.Length)
            {
                return IsOneEditDistance(t, s);
            }

            int diff = t.Length - s.Length;

            if (diff > 1)
            {
                return false;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != t[i])
                {
                    if (diff == 0)
                    {
                        // Replace character: remaining suffixes must match exactly
                        return s.AsSpan(i + 1).SequenceEqual(t.AsSpan(i + 1));
                    }
                    else
                    {
                        // Delete/Insert character: rest of s from i must match rest of t from i + 1
                        return s.AsSpan(i).SequenceEqual(t.AsSpan(i + 1));
                    }
                }
            }

            // If no differences found, strings are identical up to s.Length.
            // One edit distance is only true if t has exactly one more character at the end.
            return diff == 1;
        }
    }
}