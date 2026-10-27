// Url:https://leetcode.com/problems/index-pairs-of-a-string

/*
1065. Index Pairs of a String
Easy

Given a string text and an array of strings words, return an array of all index pairs [i, j] so that the substring text[i...j] is in words.

Return the pairs [i, j] in sorted order (i.e., sort them by their first coordinate, and in case of ties sort them by their second coordinate).

Example 1:
Input: text = "thestoryofleetcodeandme", words = ["story","fleet","leetcode"]
Output: [[3,7],[9,13],[10,17]]

Example 2:
Input: text = "ababa", words = ["aba","ab"]
Output: [[0,1],[0,2],[2,3],[2,4]]
Explanation: Notice that matches can overlap, see "aba" is found in [0,2] and [2,4].

Constraints:
- 1 <= text.length <= 100
- 1 <= words.length <= 20
- 1 <= words[i].length <= 50
- text and words[i] consist of lowercase English letters.
- All the strings of words are unique.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem1065 {
    public class Solution {
        private class TrieNode {
            public Dictionary<char, TrieNode> Children { get; } = [];
            public bool IsEnd { get; set; }
        }

        public void Init() {
            string text1 = "thestoryofleetcodeandme";
            string[] words1 = ["story", "fleet", "leetcode"];
            var res1 = IndexPairs(text1, words1);
            Console.WriteLine(string.Join(", ", Array.ConvertAll(res1, p => $"[{p[0]},{p[1]}]"))); // [3,7], [9,13], [10,17]

            string text2 = "ababa";
            string[] words2 = ["aba", "ab"];
            var res2 = IndexPairs(text2, words2);
            Console.WriteLine(string.Join(", ", Array.ConvertAll(res2, p => $"[{p[0]},{p[1]}]"))); // [0,1], [0,2], [2,3], [2,4]
        }

        // Time: O(|text|^2 + Sum(word.Length))
        // Space: O(Sum(word.Length)) for Trie
        public int[][] IndexPairs(string text, string[] words) {
            TrieNode root = new();
            foreach (string word in words) {
                TrieNode curr = root;
                foreach (char c in word) {
                    if (!curr.Children.TryGetValue(c, out var next)) {
                        next = new TrieNode();
                        curr.Children[c] = next;
                    }
                    curr = next;
                }
                curr.IsEnd = true;
            }

            List<int[]> pairs = [];
            for (int i = 0; i < text.Length; i++) {
                TrieNode curr = root;
                for (int j = i; j < text.Length; j++) {
                    if (!curr.Children.TryGetValue(text[j], out var next)) {
                        break;
                    }
                    curr = next;
                    if (curr.IsEnd) {
                        pairs.Add([i, j]);
                    }
                }
            }

            return [.. pairs];
        }
    }
}