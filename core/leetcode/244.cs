// Url:https://leetcode.com/problems/shortest-word-distance-ii

/*
244. Shortest Word Distance II
Medium

Design a class which receives a list of words in the constructor, and implements a method that takes two words word1 and word2 and return the shortest distance between these two words in the list. Your method will be called repeatedly many times with different parameters. 

Example:
Assume that words = ["practice", "makes", "perfect", "coding", "makes"].

Input: word1 = “coding”, word2 = “practice”
Output: 3
Input: word1 = "makes", word2 = "coding"
Output: 1
Note:
You may assume that word1 does not equal to word2, and word1 and word2 are both in the list.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem244 {
    public class Solution {
        public static void Init () {
            WordDistance obj = new(["practice", "makes", "perfect", "coding", "makes"]);
            int param_1 = WordDistance.Shortest("“coding”", "“practice”");
        }
    }

    public class WordDistance {

        public WordDistance (string[] words) {

        }

        public static int Shortest (string word1, string word2) {
            return 0;
        }
    }
}