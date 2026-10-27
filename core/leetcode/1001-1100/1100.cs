// Url:https://leetcode.com/problems/find-k-length-substrings-with-no-repeated-characters

/*
1100. Find K-Length Substrings With No Repeated Characters
Medium

Given a string s and an integer k, return the number of substrings in s of length k with no repeated characters.

Example 1:
Input: s = "havefunonleetcode", k = 5
Output: 6
Explanation: There are 6 substrings they are: 'havef','avefu','vefun','efuno','etcod','tcode'.

Example 2:
Input: s = "home", k = 5
Output: 0
Explanation: Notice k can be larger than the length of s. In this case, it is not possible to find any substring.

Constraints:
- 1 <= s.length <= 10^4
- s consists of lowercase English letters.
- 1 <= k <= 10^4
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1100 {
    public class Solution {
        public static void Init() {
            Console.WriteLine(NumKLenSubstrNoRepeats("havefunonleetcode", 5)); // 6
            Console.WriteLine(NumKLenSubstrNoRepeats("home", 5)); // 0
        }

        // Time: O(n) where n is the length of string s
        // Space: O(1) fixed 26 lowercase English letters frequency table
        public static int NumKLenSubstrNoRepeats(string s, int k) {
            if (k > 26 || k > s.Length) {
                return 0;
            }

            int[] counts = new int[26];
            int distinct = 0;
            int validCount = 0;

            for (int i = 0; i < k; i++) {
                int c = s[i] - 'a';
                if (counts[c] == 0) {
                    distinct++;
                }
                counts[c]++;
            }

            if (distinct == k) {
                validCount++;
            }

            for (int i = k; i < s.Length; i++) {
                int inChar = s[i] - 'a';
                if (counts[inChar] == 0) {
                    distinct++;
                }
                counts[inChar]++;

                int outChar = s[i - k] - 'a';
                counts[outChar]--;
                if (counts[outChar] == 0) {
                    distinct--;
                }

                if (distinct == k) {
                    validCount++;
                }
            }

            return validCount;
        }
    }
}