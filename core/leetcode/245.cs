// Url: https://leetcode.com/problems/shortest-word-distance-iii

/*
245. Shortest Word Distance III
Medium

Given an array of strings wordsDict and two strings that already exist in the array word1 and word2, return the shortest distance between these two words in the list.

Note that word1 and word2 may be the same. It is guaranteed that if word1 == word2, the word appears at least twice in the list.

Example 1:
Input: wordsDict = ["practice", "makes", "perfect", "coding", "makes"], word1 = "makes", word2 = "coding"
Output: 1

Example 2:
Input: wordsDict = ["practice", "makes", "perfect", "coding", "makes"], word1 = "makes", word2 = "makes"
Output: 3

Constraints:
1 <= wordsDict.length <= 10^5
1 <= wordsDict[i].length <= 10
wordsDict[i] consists of lowercase English letters.
word1 and word2 are in wordsDict.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem245
{
    public class Solution
    {
        public void Init()
        {
            string[] words = { "practice", "makes", "perfect", "coding", "makes" };
            Console.WriteLine(ShortestWordDistance(words, "makes", "coding")); // 1
            Console.WriteLine(ShortestWordDistance(words, "makes", "makes"));  // 3
        }

        // Time: O(n) where n is wordsDict.Length
        // Space: O(1) auxiliary space
        public static int ShortestWordDistance(string[] wordsDict, string word1, string word2)
        {
            if (wordsDict == null || wordsDict.Length == 0)
            {
                return 0;
            }

            int p1 = -1;
            int p2 = -1;
            int minDistance = int.MaxValue;
            bool isSameWord = word1 == word2;

            for (int i = 0; i < wordsDict.Length; i++)
            {
                string word = wordsDict[i];

                if (isSameWord)
                {
                    if (word == word1)
                    {
                        if (p1 != -1)
                        {
                            minDistance = Math.Min(minDistance, i - p1);
                        }

                        p1 = i;
                    }
                }
                else
                {
                    if (word == word1)
                    {
                        p1 = i;

                        if (p2 != -1)
                        {
                            minDistance = Math.Min(minDistance, p1 - p2);
                        }
                    }
                    else if (word == word2)
                    {
                        p2 = i;

                        if (p1 != -1)
                        {
                            minDistance = Math.Min(minDistance, p2 - p1);
                        }
                    }
                }
            }

            return minDistance;
        }
    }
}