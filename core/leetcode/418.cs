// Url: https://leetcode.com/problems/sentence-screen-fitting

/*
418. Sentence Screen Fitting
Medium

Given a rows x cols screen and a sentence represented by a list of strings, return the number of times the given sentence can be fitted on the screen.

The order of words in the sentence must remain unchanged, and a word cannot be split into two lines. A single space must separate two consecutive words in a line.

Example 1:
Input: sentence = ["hello","world"], rows = 2, cols = 8
Output: 1
Explanation:
hello---
world---
The character '-' signifies an empty space on the screen.

Example 2:
Input: sentence = ["a", "bcd", "e"], rows = 3, cols = 6
Output: 2
Explanation:
a-bcd- 
e-a---
bcd-e-
The character '-' signifies an empty space on the screen.

Example 3:
Input: sentence = ["i","had","apple","pie"], rows = 4, cols = 5
Output: 1
Explanation:
i-had
apple
pie-i
had--
The character '-' signifies an empty space on the screen.

Constraints:
1 <= sentence.length <= 100
1 <= sentence[i].length <= 10
sentence[i] consists of lowercase English letters.
1 <= rows, cols <= 2 * 10^4
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem418
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(WordsTyping(new string[] { "hello", "world" }, 2, 8));           // 1
            Console.WriteLine(WordsTyping(new string[] { "a", "bcd", "e" }, 3, 6));           // 2
            Console.WriteLine(WordsTyping(new string[] { "i", "had", "apple", "pie" }, 4, 5)); // 1
        }

        // Time: O(n * cols / avg_word_len + rows) where n is sentence.Length
        // Space: O(n) for next word and word count lookup tables
        public static int WordsTyping(string[] sentence, int rows, int cols)
        {
            if (sentence == null || sentence.Length == 0 || rows <= 0 || cols <= 0)
            {
                return 0;
            }

            int n = sentence.Length;
            int[] nextWord = new int[n];
            int[] wordCount = new int[n];

            for (int i = 0; i < n; i++)
            {
                int curr = i;
                int len = 0;
                int count = 0;

                while (len + sentence[curr].Length <= cols)
                {
                    len += sentence[curr].Length + 1; // +1 for space
                    curr = (curr + 1) % n;
                    count++;
                }

                nextWord[i] = curr;
                wordCount[i] = count;
            }

            int totalWords = 0;
            int currentWordIdx = 0;

            for (int r = 0; r < rows; r++)
            {
                totalWords += wordCount[currentWordIdx];
                currentWordIdx = nextWord[currentWordIdx];
            }

            return totalWords / n;
        }
    }
}