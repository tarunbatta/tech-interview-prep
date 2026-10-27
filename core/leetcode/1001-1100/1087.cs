// Url:https://leetcode.com/problems/brace-expansion

/*
1087. Brace Expansion
Medium

You are given a string s representing a list of words. Each letter in the word has one or more options.
- If there is one option, the letter is represented as the letter itself.
- If there is more than one option, curly braces mark the options. For example, "{a,b,c}" represents options ["a", "b", "c"].

For example, if s = "{a,b,c}d{e,f}", this represents the words ["ade", "adf", "bde", "bdf", "cde", "cdf"].

Return all words that can be formed in this manner, sorted in lexicographical order.

Example 1:
Input: s = "{a,b}c{d,e}f"
Output: ["acdf","acef","bcdf","bcef"]

Example 2:
Input: s = "abcd"
Output: ["abcd"]

Constraints:
- 1 <= s.length <= 50
- s consists of lowercase English letters, '{', '}', and ','.
- s contains no nested curly brackets.
- All characters inside a pair of consecutive opening and closing curly brackets are separated by ','.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem1087 {
    public class Solution {
        public static void Init() {
            Console.WriteLine(string.Join(", ", Expand("{a,b}c{d,e}f"))); // "acdf", "acef", "bcdf", "bcef"
            Console.WriteLine(string.Join(", ", Expand("abcd"))); // "abcd"
        }

        // Time: O(K * L) where K is number of formed words and L is word length
        // Space: O(K * L) to store words and groups
        public static string[] Expand(string s) {
            List<List<char>> groups = [];
            int i = 0;

            while (i < s.Length) {
                if (s[i] == '{') {
                    int end = s.IndexOf('}', i);
                    string inside = s.Substring(i + 1, end - i - 1);
                    string[] parts = inside.Split(',');
                    List<char> options = [];
                    foreach (string p in parts) {
                        options.Add(p[0]);
                    }
                    options.Sort();
                    groups.Add(options);
                    i = end + 1;
                } else {
                    groups.Add([s[i]]);
                    i++;
                }
            }

            List<string> result = [];
            StringBuilder sb = new();
            Backtrack(groups, 0, sb, result);
            return [.. result];
        }

        private static void Backtrack(List<List<char>> groups, int idx, StringBuilder sb, List<string> result) {
            if (idx == groups.Count) {
                result.Add(sb.ToString());
                return;
            }

            foreach (char c in groups[idx]) {
                sb.Append(c);
                Backtrack(groups, idx + 1, sb, result);
                sb.Remove(sb.Length - 1, 1);
            }
        }
    }
}