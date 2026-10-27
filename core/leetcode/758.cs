// Url:https://leetcode.com/problems/bold-words-in-string

/*
758. Bold Words in String
Medium

Given an array of keywords words and a string s, make all appearances of all keywords words[i] in s bold using <b> and </b> tags.

- If two keywords overlap, they should be wrapped together by only one pair of closed <b> and </b> tags.
- If two substrings that are bold are consecutive, they should be combined.

Return s after adding the bold tags.

Example 1:
Input: words = ["ab","bc"], s = "aabcd"
Output: "a<b>abc</b>d"
Explanation: Note that returning "a<b>a<b>b</b>c</b>d" would use more tags, so it is incorrect.

Example 2:
Input: words = ["ab","cb"], s = "aabcd"
Output: "a<b>ab</b>cd"

Constraints:
- 1 <= s.length <= 500
- 0 <= words.length <= 50
- 1 <= words[i].length <= 10
- s and words[i] consist of lowercase English letters.
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem758 {
    public class Solution {
        public static void Init() {
            string[] words1 = ["ab", "bc"];
            string s1 = "aabcd";
            Console.WriteLine(BoldWords(words1, s1)); // "a<b>abc</b>d"

            string[] words2 = ["ab", "cb"];
            string s2 = "aabcd";
            Console.WriteLine(BoldWords(words2, s2)); // "a<b>ab</b>cd"
        }

        // Time: O(N * Sum(W)) where N is length of s and W is word lengths
        // Space: O(N) for bold mask and string builder
        public static string BoldWords(string[] words, string s) {
            int n = s.Length;
            bool[] isBold = new bool[n];

            foreach (string word in words) {
                if (string.IsNullOrEmpty(word)) {
                    continue;
                }

                int start = 0;
                while ((start = s.IndexOf(word, start, StringComparison.Ordinal)) != -1) {
                    int end = start + word.Length;
                    for (int i = start; i < end; i++) {
                        isBold[i] = true;
                    }
                    start++;
                }
            }

            StringBuilder sb = new();
            for (int i = 0; i < n; i++) {
                if (isBold[i] && (i == 0 || !isBold[i - 1])) {
                    sb.Append("<b>");
                }

                sb.Append(s[i]);

                if (isBold[i] && (i == n - 1 || !isBold[i + 1])) {
                    sb.Append("</b>");
                }
            }

            return sb.ToString();
        }
    }
}