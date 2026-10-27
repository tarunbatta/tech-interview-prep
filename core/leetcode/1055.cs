// Url:https://leetcode.com/problems/shortest-way-to-form-string

/*
1055. Shortest Way to Form String
Medium

A subsequence of a string is a new string that is formed from the original string by deleting some (can be none) of the characters without disturbing the relative positions of the remaining characters. (i.e., "ace" is a subsequence of "abcde" while "aec" is not).

Given two strings source and target, return the minimum number of subsequences of source such that their concatenation equals target. If the task is impossible, return -1.

Example 1:
Input: source = "abc", target = "abcbc"
Output: 2
Explanation: The target "abcbc" can be formed by "abc" and "bc", which are subsequences of source "abc".

Example 2:
Input: source = "abc", target = "acdbc"
Output: -1
Explanation: The target string cannot be constructed from the source string because the character "d" in the target string does not exist in the source string.

Example 3:
Input: source = "xyz", target = "xzyxz"
Output: 3
Explanation: The target string can be constructed as follows "xz" + "y" + "xz".

Constraints:
- 1 <= source.length, target.length <= 1000
- source and target consist of lowercase English letters.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem1055 {
    public class Solution {
        public void Init() {
            Console.WriteLine(ShortestWay("abc", "abcbc")); // 2
            Console.WriteLine(ShortestWay("abc", "acdbc")); // -1
            Console.WriteLine(ShortestWay("xyz", "xzyxz")); // 3
        }

        // Time: O(|source| * |target|)
        // Space: O(1)
        public int ShortestWay(string source, string target) {
            HashSet<char> sourceChars = [..source];
            foreach (char c in target) {
                if (!sourceChars.Contains(c)) {
                    return -1;
                }
            }

            int count = 0;
            int tIndex = 0;

            while (tIndex < target.Length) {
                count++;
                int sIndex = 0;

                while (sIndex < source.Length && tIndex < target.Length) {
                    if (source[sIndex] == target[tIndex]) {
                        tIndex++;
                    }
                    sIndex++;
                }
            }

            return count;
        }
    }
}