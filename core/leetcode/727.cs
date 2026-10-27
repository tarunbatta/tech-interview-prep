// Url:https://leetcode.com/problems/minimum-window-subsequence

/*
727. Minimum Window Subsequence
Hard

Given strings s1 and s2, return the minimum contiguous substring part of s1, so that s2 is a subsequence of the part.

If there is no such window in s1 that covers all characters in s2, return the empty string "". If there are multiple such minimum-length windows, return the one with the left-most starting index.

Example 1:
Input: s1 = "abcdebdde", s2 = "bde"
Output: "bcde"
Explanation: 
"bcde" is the answer because it occurs before "bdde" which has the same length.
"deb" is not a smaller window because the elements of s2 in the window must occur in order.

Example 2:
Input: s1 = "jmeqqafrmVk", s2 = "u"
Output: ""

Constraints:
- 1 <= s1.length <= 2 * 10^4
- 1 <= s2.length <= 100
- s1 and s2 consist of lowercase English letters.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem727 {
    public class Solution {
        public static void Init() {
            Console.WriteLine(MinWindow("abcdebdde", "bde")); // "bcde"
            Console.WriteLine(MinWindow("jmeqqafrmVk", "u")); // ""
        }

        // Time: O(|s1| * |s2|)
        // Space: O(1)
        public static string MinWindow(string s1, string s2) {
            int m = s1.Length;
            int n = s2.Length;

            int minLen = int.MaxValue;
            int startIndex = -1;

            int i = 0;
            int j = 0;

            while (i < m) {
                if (s1[i] == s2[j]) {
                    j++;
                    if (j == n) {
                        // Found a match ending at index i, now backtrack to find optimal start
                        int end = i;
                        j--;
                        while (j >= 0) {
                            if (s1[i] == s2[j]) {
                                j--;
                            }
                            i--;
                        }
                        i++; // i is now the start index

                        if (end - i + 1 < minLen) {
                            minLen = end - i + 1;
                            startIndex = i;
                        }

                        j = 0;
                    }
                }
                i++;
            }

            return startIndex == -1 ? "" : s1.Substring(startIndex, minLen);
        }
    }
}