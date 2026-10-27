// Url:https://leetcode.com/problems/word-ladder

/*
127. Word Ladder
Medium

Given two words (beginWord and endWord), and a dictionary's word list, find the length of shortest transformation sequence from beginWord to endWord, such that:

	Only one letter can be changed at a time.
	Each transformed word must exist in the word list. Note that beginWord is not a transformed word.

Note:
	Return 0 if there is no such transformation sequence.
	All words have the same length.
	All words contain only lowercase alphabetic characters.
	You may assume no duplicates in the word list.
	You may assume beginWord and endWord are non-empty and are not the same.


Example 1:

Input:
beginWord = "hit",
endWord = "cog",
wordList = ["hot","dot","dog","lot","log","cog"]

Output: 5

Explanation: As one shortest transformation is "hit" -> "hot" -> "dot" -> "dog" -> "cog",
return its length 5.


Example 2:

Input:
beginWord = "hit"
endWord = "cog"
wordList = ["hot","dot","dog","lot","log"]

Output: 0

Explanation: The endWord "cog" is not in wordList, therefore no possible transformation.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem127 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (LadderLength("hit", "cog", ["hot", "dot", "dog", "lot", "log", "cog"]));
        }

        // Time: O (m * n), where m is length of words
        // Space: O (m * n), where n is total number of words in input list
        public static int LadderLength (string beginWord, string endWord, IList<string> wordList) {
            HashSet<string> hash = [.. wordList];

            if (!hash.Contains (endWord)) {
                return 0;
            }

            int level = 0;
            int wordlen = beginWord.Length;
            Queue<string> q = new Queue<string> ();
            q.Enqueue (beginWord);

            while (q.Count > 0) {
                ++level;

                int levelItems = q.Count;

                for (int i = 0; i < levelItems; i++) {
                    string word = q.Dequeue ();

                    for (int pos = 0; pos < wordlen; pos++) {
                        char[] wordArr = word.ToCharArray ();
                        char originalChar = word[pos];

                        for (char c = 'a'; c <= 'z'; c++) {
                            wordArr[pos] = c;
                            string newWord = new string (wordArr);

                            if (newWord == endWord) {
                                return level + 1;
                            }

                            if (!hash.Contains (newWord)) {
                                continue;
                            }

                            hash.Remove (newWord);

                            q.Enqueue (newWord);
                        }

                        wordArr[pos] = originalChar;
                    }
                }
            }

            return 0;
        }
    }
}