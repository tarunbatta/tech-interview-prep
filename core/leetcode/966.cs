// Url:https://leetcode.com/problems/vowel-spellchecker

/*
966. Vowel Spellchecker
Medium

Given a wordlist, we want to implement a spellchecker that converts a query word into a correct word.

For a given query word, the spell checker handles two categories of spelling mistakes:
- Capitalization: If the query matches a word in the wordlist (case-insensitive), then the query word is returned with the same case as the case in the wordlist.
  - Example: wordlist = ["yellow"], query = "YellOw": correct = "yellow"
  - Example: wordlist = ["Yellow"], query = "yellow": correct = "Yellow"
  - Example: wordlist = ["yellow"], query = "yellow": correct = "yellow"
- Vowel Errors: If after replacing the vowels ('a', 'e', 'i', 'o', 'u') of the query word with any vowel individually, it matches a word in the wordlist (case-insensitive), then the query word is returned with the same case as the match in the wordlist.
  - Example: wordlist = ["YellOw"], query = "yollow": correct = "YellOw"
  - Example: wordlist = ["YellOw"], query = "yeellow": correct = "" (not a match)
  - Example: wordlist = ["YellOw"], query = "yllw": correct = "" (not a match)

In addition, the spell checker operates under the following precedence rules:
- When the query exactly matches a word in the wordlist (case-sensitive), you should return the same word back.
- When the query matches a word up to capitlization, you should return the first such match in the wordlist.
- When the query matches a word up to vowel errors, you should return the first such match in the wordlist.
- If the query has no matches in the wordlist, you should return the empty string.

Given some queries, return a list of words answer, where answer[i] is the correct word for query = queries[i].

Example 1:
Input: wordlist = ["KiTe","kite","hare","Hare"], queries = ["kite","Kite","KiTe","Hare","HARE","Hear","hear","keti","keet","keto"]
Output: ["kite","KiTe","KiTe","Hare","hare","","","KiTe","","KiTe"]

Example 2:
Input: wordlist = ["yellow"], queries = ["YellOw"]
Output: ["yellow"]

Constraints:
- 1 <= wordlist.length, queries.length <= 5000
- 1 <= wordlist[i].length, queries[i].length <= 7
- wordlist[i] and queries[i] consist only of only English letters.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem966 {
    public class Solution {
        public void Init() {
            string[] wordlist = ["KiTe", "kite", "hare", "Hare"];
            string[] queries = ["kite", "Kite", "KiTe", "Hare", "HARE", "Hear", "hear", "keti", "keet", "keto"];
            Console.WriteLine(string.Join(", ", Spellchecker(wordlist, queries)));
            // "kite", "KiTe", "KiTe", "Hare", "hare", "", "", "KiTe", "", "KiTe"
        }

        // Time: O(W * L + Q * L) where W is wordlist size, Q is queries size, L is word length
        // Space: O(W * L)
        public string[] Spellchecker(string[] wordlist, string[] queries) {
            HashSet<string> exact = [];
            Dictionary<string, string> cap = [];
            Dictionary<string, string> vowel = [];

            foreach (string word in wordlist) {
                exact.Add(word);

                string lower = word.ToLowerInvariant();
                cap.TryAdd(lower, word);

                string devoweled = Devowel(lower);
                vowel.TryAdd(devoweled, word);
            }

            string[] result = new string[queries.Length];
            for (int i = 0; i < queries.Length; i++) {
                string q = queries[i];
                if (exact.Contains(q)) {
                    result[i] = q;
                    continue;
                }

                string lower = q.ToLowerInvariant();
                if (cap.TryGetValue(lower, out var capMatch)) {
                    result[i] = capMatch;
                    continue;
                }

                string devoweled = Devowel(lower);
                if (vowel.TryGetValue(devoweled, out var vowelMatch)) {
                    result[i] = vowelMatch;
                    continue;
                }

                result[i] = string.Empty;
            }

            return result;
        }

        private static string Devowel(string word) {
            StringBuilder sb = new();
            foreach (char c in word) {
                if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u') {
                    sb.Append('*');
                } else {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}