// Url: https://leetcode.com/problems/word-squares

/*
425. Word Squares
Hard

Given an array of unique strings words, return all the word squares you can build from words. You can return the answer in any order.

A sequence of strings forms a valid word square if the kth row and kth column read the same string, where 0 <= k < max(numRows, numColumns).

For example, the word sequence ["ball","area","lead","lady"] forms a word square because each word reads the same vertically and horizontally.

Example 1:
Input: words = ["area","lead","wall","lady","ball"]
Output: [["ball","area","lead","lady"],["wall","area","lead","lady"]]
Explanation:
The output consists of two word squares. The order of output does not matter (just the order of words in each word square matters).

Example 2:
Input: words = ["abat","baba","atan","atal"]
Output: [["baba","abat","baba","atan"],["baba","abat","baba","atal"]]

Constraints:
1 <= words.length <= 1000
1 <= words[i].length <= 4
All words[i] have the same length.
words[i] consists of only lowercase English letters.
All words[i] are unique.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem425
{
    public class Solution
    {
        public static void Init()
        {
            string[] words1 = ["area", "lead", "wall", "lady", "ball"];
            var res1 = WordSquares(words1);
            foreach (var sq in res1)
            {
                Console.WriteLine($"[{string.Join(", ", sq)}]");
            }
        }

        // Time: O(N * 26^L) worst-case with extensive Trie-prefix pruning, where N is words.Length and L is word length
        // Space: O(N * L) for the Prefix Trie and recursion call stack
        public static IList<IList<string>> WordSquares(string[] words)
        {
            List<IList<string>> result = [];

            if (words == null || words.Length == 0)
            {
                return result;
            }

            int wordLen = words[0].Length;
            TrieNode root = BuildTrie(words);

            List<string> currentSquare = [];

            foreach (string word in words)
            {
                currentSquare.Add(word);
                Backtrack(wordLen, root, currentSquare, result);
                currentSquare.RemoveAt(currentSquare.Count - 1);
            }

            return result;
        }

        private static void Backtrack(int wordLen, TrieNode root, List<string> currentSquare, List<IList<string>> result)
        {
            if (currentSquare.Count == wordLen)
            {
                result.Add(new List<string>(currentSquare));
                return;
            }

            int nextRow = currentSquare.Count;
            StringBuilder prefix = new();

            for (int r = 0; r < nextRow; r++)
            {
                prefix.Append(currentSquare[r][nextRow]);
            }

            List<string> candidates = GetWordsWithPrefix(root, prefix.ToString());

            foreach (string candidate in candidates)
            {
                currentSquare.Add(candidate);
                Backtrack(wordLen, root, currentSquare, result);
                currentSquare.RemoveAt(currentSquare.Count - 1);
            }
        }

        private static TrieNode BuildTrie(string[] words)
        {
            TrieNode root = new();

            foreach (string word in words)
            {
                TrieNode curr = root;
                curr.Words.Add(word);

                foreach (char c in word)
                {
                    int idx = c - 'a';
                    curr.Children[idx] ??= new TrieNode();
                    curr = curr.Children[idx];
                    curr.Words.Add(word);
                }
            }

            return root;
        }

        private static List<string> GetWordsWithPrefix(TrieNode root, string prefix)
        {
            TrieNode curr = root;

            foreach (char c in prefix)
            {
                int idx = c - 'a';

                if (curr.Children[idx] == null)
                {
                    return [];
                }

                curr = curr.Children[idx];
            }

            return curr.Words;
        }

        private class TrieNode
        {
            public TrieNode[] Children = new TrieNode[26];
            public List<string> Words = [];
        }
    }
}