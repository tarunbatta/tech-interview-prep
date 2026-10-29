// Url: https://leetcode.com/problems/word-search-ii/

/*
212. Word Search II
Hard

Given a 2D board and a list of words from the dictionary, find all words in the board.

Each word must be constructed from letters of sequentially adjacent cell, where "adjacent" cells are those horizontally or vertically neighboring. The same letter cell may not be used more than once in a word.

 

Example:

Input: 
board = [
  ['o','a','a','n'],
  ['e','t','a','e'],
  ['i','h','k','r'],
  ['i','f','l','v']
]
words = ["oath","pea","eat","rain"]

Output: ["eat","oath"]
 

Note:

All inputs are consist of lowercase letters a-z.
The values of words are distinct.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem212 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (findWords(null, null));
            Console.WriteLine (findWords([
                ['o', 'a', 'a', 'n'],
                    ['e', 't', 'a', 'e'],
                    ['i', 'h', 'k', 'r'],
                    ['i', 'f', 'l', 'v']
            ], ["oath", "pea", "eat", "rain"]));
        }

        public static IList<String> findWords (char[][] board, String[] words) {
            List<String> result = [];

            if (board == null || board.Length == 0 || board[0].Length == 0 || words == null || words.Length == 0) {
                return result;
            }

            TrieNode root = buildTrie(words);

            for (int i = 0; i < board.Length; i++) {
                for (int j = 0; j < board[0].Length; j++) {
                    dfs(board, i, j, root, result);
                }
            }

            return result;
        }

        public static void dfs (char[][] board, int i, int j, TrieNode p, List<String> res) {
            char c = board[i][j];

            if (c == '#' || p.next[c - 'a'] == null) {
                return;
            }

            p = p.next[c - 'a'];

            if (p.word != null) { // found one
                res.Add (p.word);
                p.word = null; // de-duplicate
            }

            board[i][j] = '#';

            if (i > 0) dfs(board, i - 1, j, p, res);
            if (j > 0) dfs(board, i, j - 1, p, res);
            if (i < board.Length - 1) dfs(board, i + 1, j, p, res);
            if (j < board[0].Length - 1) dfs(board, i, j + 1, p, res);

            board[i][j] = c;
        }

        public static TrieNode buildTrie (String[] words) {
            TrieNode root = new();

            foreach (String w in words) {
                TrieNode p = root;

                foreach (char c in w.ToCharArray ()) {
                    int i = c - 'a';

                    if (p.next[i] == null) {
                        p.next[i] = new TrieNode ();
                    }

                    p = p.next[i];
                }

                p.word = w;
            }

            return root;
        }
    }

    public class TrieNode {
        public TrieNode[] next = new TrieNode[26];
        public string word;
    }
}