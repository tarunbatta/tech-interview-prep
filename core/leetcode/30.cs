// Url:https://leetcode.com/problems/substring-with-concatenation-of-all-words

/*
30. Substring with Concatenation of All Words
Hard

You are given a string, s, and a list of words, words, that are all of the same length. Find all starting indices of substring(s) in s that is a concatenation of each word in words exactly once and without any intervening characters.

Example 1:

Input:
  s = "barfoothefoobarman",
  words = ["foo","bar"]
Output: [0,9]
Explanation: Substrings starting at index 0 and 9 are "barfoor" and "foobar" respectively.
The output order does not matter, returning [9,0] is fine too.


Example 2:

Input:
  s = "wordgoodgoodgoodbestword",
  words = ["word","good","best","word"]
Output: []
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem30 {
    public class Solution {
        public static void Init () {

        }

        // Time: O (n * m), where m is word length
        // Space: O (1)
        public static IList<int> FindSubstring (string s, string[] words) {
            var result = new List<int> ();

            if (string.IsNullOrEmpty (s) || words == null || words.Length == 0) {
                return result;
            }

            IDictionary<string, int> wordsWithCount = new Dictionary<string, int> ();

            foreach (string w in words) {
                wordsWithCount[w] = wordsWithCount.ContainsKey (w) ? wordsWithCount[w] + 1 : 1;
            }

            int wordCount = words[0].Length;
            IDictionary<string, int> currentWordsWithCount = new Dictionary<string, int> ();

            for (int i = 0; i < wordCount; i++) {
                currentWordsWithCount.Clear ();

                for (int left = i, right = i; right + words.Length * wordCount <= s.Length; left += wordCount) {
                    string word = s.Substring (left, wordCount);

                    if (!wordsWithCount.ContainsKey (word)) {
                        currentWordsWithCount.Clear ();
                        right = left + wordCount;
                        continue;
                    }

                    currentWordsWithCount[word] = currentWordsWithCount.ContainsKey (word) ? currentWordsWithCount[word] + 1 : 1;

                    if (currentWordsWithCount[word] > wordsWithCount[word]) {
                        while (currentWordsWithCount[word] > wordsWithCount[word]) {
                            currentWordsWithCount[s.Substring (right, wordCount)]--;
                            right += wordCount;
                        }
                    } else if ((left - right) / wordCount == words.Length - 1) {
                        result.Add (right);
                        currentWordsWithCount[s.Substring (right, wordCount)]--;
                        right += wordCount;
                    }
                }
            }

            return result;
        }
    }
}