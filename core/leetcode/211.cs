// Url:https://leetcode.com/problems/add-and-search-word-data-structure-design

/*
211. Add and Search Word - Data structure design
Medium

Design a data structure that supports the following two operations:

void addWord(word)
bool search(word)


search(word) can search a literal word or a regular expression string containing only letters a-z or .. A . means it can represent any one letter.

Example:

addWord("bad")
addWord("dad")
addWord("mad")
search("pad") -> false
search("bad") -> true
search(".ad") -> true
search("b..") -> true


Note:
You may assume that all words are consist of lowercase letters a-z.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem211 {
    public class Solution {
        public static void Init () {
            string word = "";
            WordDictionary obj = new WordDictionary ();
            WordDictionary.AddWord(word);
            bool param_2 = WordDictionary.Search(word);
        }
    }

    public class WordDictionary {
        public WordDictionary () {

        }

        // Adds a word into the data structure.
        public static void AddWord (string word) {

        }

        //  Returns if the word is in the data structure. A word could contain the dot character '.' to represent any one letter.
        public static bool Search (string word) {
            return false;
        }
    }
}