// Url:https://leetcode.com/problems/add-bold-tag-in-string

/*
616. Add Bold Tag in String
Medium

You are given a string s and an array of strings words.

You should add a closed pair of bold tag <b> and </b> to wrap the substrings in s that exist in words.
- If two such substrings overlap, you should wrap them together with only one pair of closed bold-tag.
- If two substrings wrapped by bold tags are consecutive, you should combine them.

Return the result string after adding the bold tags.

Example 1:
Input: s = "abcxyz123", words = ["abc","123"]
Output: "<b>abc</b>xyz<b>123</b>"

Example 2:
Input: s = "aaabbb", words = ["aa","b"]
Output: "<b>aaabbb</b>"
Explanation: 
"aa" appears as a substring at index 0 and index 1.
"b" appears as a substring at index 3, index 4, and index 5.
We use <b> to wrap the substrings, and since they overlap or are consecutive, we wrap them together.

Constraints:
- 1 <= s.length <= 1000
- 0 <= words.length <= 100
- 1 <= words[i].length <= 1000
- s and words[i] consist of English letters and digits.
- All the values of words are unique.
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem616 {
    public class Solution {
        public static void Init() {
            string s1 = "abcxyz123";
            string[] words1 = ["abc", "123"];
            Console.WriteLine(AddBoldTag(s1, words1)); // "<b>abc</b>xyz<b>123</b>"

            string s2 = "aaabbb";
            string[] words2 = ["aa", "b"];
            Console.WriteLine(AddBoldTag(s2, words2)); // "<b>aaabbb</b>"
        }

        // Time: O(N * Sum(W)) where N is length of s and W is word lengths
        // Space: O(N) for bold mask and string builder
        public static string AddBoldTag(string s, string[] words) {
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