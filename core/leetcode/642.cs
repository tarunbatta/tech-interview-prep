// Url:https://leetcode.com/problems/design-search-autocomplete-system

/*
642. Design Search Autocomplete System
Hard

Design a search autocomplete system for a search engine. Users may input a sentence (at least one word and end with a special character '#').

For each character they type except '#', you need to return the top 3 historical hot sentences that have the same prefix as the part of sentence already typed. Here are the specific rules:
- The hot degree for a sentence is defined as the number of times a user typed the exactly same sentence before.
- The returned top 3 hot sentences should be sorted by hot degree (The first is the hottest one). If several sentences have the same degree of hot, you need to use ASCII-code order (smaller one appears first).
- If less than 3 hot sentences exist, return as many as you can.
- When the input is '#', it means the sentence ends, and the sentence should be saved as a historical sentence in the system.

Implement the AutocompleteSystem class:
- AutocompleteSystem(string[] sentences, int[] times) Initializes the object with the sentences and times arrays.
- IList<string> Input(char c) This indicates that the user typed the character c.
  - Returns an empty array [] if c == '#' and stores the inputted sentence in the system.
  - Returns the top 3 historically hot sentences that have the same prefix as the sentence typed so far. If there are fewer than 3 matches, return all of them.

Example 1:
Input
["AutocompleteSystem", "input", "input", "input", "input"]
[[["i love you", "island", "iroman", "i love leetcode"], [5, 3, 2, 2]], ["i"], [" "], ["a"], ["#"]]
Output
[null, ["i love you", "island", "iroman"], ["i love you", "i love leetcode"], [], []]

Constraints:
- n == sentences.length
- n == times.length
- 1 <= n <= 100
- 1 <= sentences[i].length <= 100
- 1 <= times[i] <= 50
- c is a lowercase English letter, a hash '#', or space ' '.
- Each tested sentence will be a sequence of characters c that end with the character '#'.
- At most 5000 calls will be made to input.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem642 {
    public class AutocompleteSystem {
        private class TrieNode {
            public Dictionary<char, TrieNode> Children { get; } = [];
            public HashSet<string> Sentences { get; } = [];
        }

        private readonly TrieNode _root = new();
        private readonly Dictionary<string, int> _counts = [];
        private readonly StringBuilder _currentSentence = new();
        private TrieNode _currentNode;

        // Time: O(N * L) where N is number of sentences and L is average length
        // Space: O(N * L)
        public AutocompleteSystem(string[] sentences, int[] times) {
            _currentNode = _root;
            for (int i = 0; i < sentences.Length; i++) {
                _counts[sentences[i]] = times[i];
                InsertSentence(sentences[i]);
            }
        }

        // Time: O(M log M) where M is the number of matching candidate sentences
        // Space: O(M)
        public IList<string> Input(char c) {
            if (c == '#') {
                string completed = _currentSentence.ToString();
                _counts[completed] = _counts.GetValueOrDefault(completed, 0) + 1;
                InsertSentence(completed);

                _currentSentence.Clear();
                _currentNode = _root;
                return [];
            }

            _currentSentence.Append(c);

            if (_currentNode != null) {
                _currentNode = _currentNode.Children.GetValueOrDefault(c);
            }

            if (_currentNode == null) {
                return [];
            }

            return [.. _currentNode.Sentences
                .OrderByDescending(s => _counts[s])
                .ThenBy(s => s, StringComparer.Ordinal)
                .Take(3)];
        }

        private void InsertSentence(string sentence) {
            TrieNode curr = _root;
            curr.Sentences.Add(sentence);

            foreach (char ch in sentence) {
                if (!curr.Children.TryGetValue(ch, out var next)) {
                    next = new TrieNode();
                    curr.Children[ch] = next;
                }
                curr = next;
                curr.Sentences.Add(sentence);
            }
        }
    }

    public class Solution {
        public static void Init() {
            string[] sentences = ["i love you", "island", "iroman", "i love leetcode"];
            int[] times = [5, 3, 2, 2];

            AutocompleteSystem system = new(sentences, times);
            Console.WriteLine(string.Join(", ", system.Input('i'))); // "i love you", "island", "iroman"
            Console.WriteLine(string.Join(", ", system.Input(' '))); // "i love you", "i love leetcode"
            Console.WriteLine(string.Join(", ", system.Input('a'))); // empty
            Console.WriteLine(string.Join(", ", system.Input('#'))); // empty
        }
    }
}