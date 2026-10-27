/*

*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.DataStructure.Trie {
    public class TrieNode {
        public bool isWord;
        public TrieNode[] childrens;
        public const int ASCIIA = 97;

        public TrieNode () {
            this.isWord = false;
            this.childrens = new TrieNode[26];
        }

        public static bool Contains (char c) {
            bool result = false;
            int n = Convert.ToByte (c) - ASCIIA;

            if (n < 26) {
                result = true;
            }

            return result;
        }

        public TrieNode GetChild (char c) {
            int n = Convert.ToByte (c) - ASCIIA;
            return this.childrens[n];
        }
    }

    public class Trie {
        public TrieNode root;

        public Trie () {
            this.root = new TrieNode ();
        }

        public void Insert (String word) {
            if (root != null && !string.IsNullOrEmpty (word)) {
                TrieNode node = root;

                foreach (var item in word.ToCharArray ()) {
                    node = _Insert(item, node);
                }

                node.isWord = true;
            }
        }

        private static TrieNode _Insert (char c, TrieNode node) {
            if (TrieNode.Contains(c)) {
                return node.GetChild (c);
            } else {
                int n = Convert.ToByte (c) - TrieNode.ASCIIA;
                TrieNode t = new TrieNode ();
                node.childrens[n] = t;
                return t;
            }
        }

        public bool Contains (String word) {
            bool result = true;

            if (root != null && !string.IsNullOrEmpty (word)) {
                TrieNode node = root;

                foreach (var item in word.ToCharArray ()) {
                    node = _Contains(item, node);

                    if (node == null) {
                        result = false;
                        break;
                    }
                }

                if (node == null) {
                    result = false;
                }
            }

            return result;
        }

        private static TrieNode _Contains (char c, TrieNode node) {
            if (TrieNode.Contains(c)) {
                return node.GetChild (c);
            } else {
                return null;
            }
        }
    }
}