// Url:https://leetcode.com/problems/longest-repeating-substring

/*
1062. Longest Repeating Substring
Medium

Given a string s, find the length of the longest repeating substring(s). Return 0 if no repeating substring exists.

Example 1:
Input: s = "abcd"
Output: 0
Explanation: There is no repeating substring.

Example 2:
Input: s = "abbaba"
Output: 2
Explanation: The longest repeating substrings are "ab" and "ba", each of which occurs twice.

Example 3:
Input: s = "aabcaabdaab"
Output: 3
Explanation: The longest repeating substring is "aab", which occurs 3 times.

Example 4:
Input: s = "aaaaa"
Output: 4
Explanation: The longest repeating substring is "aaaa", which occurs twice.

Constraints:
- The string s consists of only lowercase English letters from 'a' - 'z'.
- 1 <= s.length <= 1500
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem1062 {
    public class Solution {
        public static void Init() {
            Console.WriteLine(LongestRepeatingSubstring("abcd")); // 0
            Console.WriteLine(LongestRepeatingSubstring("abbaba")); // 2
            Console.WriteLine(LongestRepeatingSubstring("aabcaabdaab")); // 3
            Console.WriteLine(LongestRepeatingSubstring("aaaaa")); // 4
        }

        // Time: O(n log n) with binary search and rolling hash
        // Space: O(n) for rolling hash set
        public static int LongestRepeatingSubstring(string s) {
            int n = s.Length;
            int low = 1;
            int high = n - 1;
            int ans = 0;

            while (low <= high) {
                int mid = low + (high - low) / 2;
                if (HasRepeating(s, mid)) {
                    ans = mid;
                    low = mid + 1;
                } else {
                    high = mid - 1;
                }
            }

            return ans;
        }

        private static bool HasRepeating(string s, int len) {
            const ulong b = 31;
            ulong power = 1;
            for (int i = 0; i < len; i++) {
                power *= b;
            }

            ulong hash = 0;
            for (int i = 0; i < len; i++) {
                hash = hash * b + (ulong)s[i];
            }

            HashSet<ulong> seen = [hash];

            for (int i = len; i < s.Length; i++) {
                hash = hash * b + (ulong)s[i] - (ulong)s[i - len] * power;
                if (!seen.Add(hash)) {
                    return true;
                }
            }

            return false;
        }
    }
}