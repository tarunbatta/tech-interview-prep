// Url:https://leetcode.com/problems/implement-magic-dictionary

/*
676. Implement Magic Dictionary
Medium

Implement a magic directory with buildDict, and search methods.



For the method buildDict, you'll be given a list of non-repetitive words to build a dictionary.



For the method search, you'll be given a word, and judge whether if you modify exactly one character into another character in this word, the modified word is in the dictionary you just built.


Example 1:
Input: buildDict(["hello", "leetcode"]), Output: Null
Input: search("hello"), Output: False
Input: search("hhllo"), Output: True
Input: search("hell"), Output: False
Input: search("leetcoded"), Output: False



Note:

You may assume that all the inputs are consist of lowercase letters a-z.
For contest purpose, the test data is rather small by now. You could think about highly efficient algorithm after the contest.
Please remember to RESET your class variables declared in class MagicDictionary, as static/class variables are persisted across multiple test cases. Please see here for more details.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem676 {
    public class Solution {
        public static void Init () {
            MagicDictionary obj = new MagicDictionary ();
            MagicDictionary.BuildDict(null);
            bool param_2 = MagicDictionary.Search(null);
        }
    }

    public class MagicDictionary {
        public MagicDictionary () { }

        // Build a dictionary through a list of words
        public static void BuildDict (string[] dict) { }

        // Returns if there is any word in the trie that equals to the given word after modifying exactly one character  
        public static bool Search (string word) {
            return false;
        }
    }
}