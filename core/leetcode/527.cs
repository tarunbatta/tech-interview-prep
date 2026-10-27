// Url: https://leetcode.com/problems/word-abbreviation

/*
527. Word Abbreviation
Hard

Given an array of distinct strings words, return the minimal possible abbreviations for every word.

The following are the rules for a string abbreviation:
1. The initial abbreviation for each word is: the first character, then the number of characters in between, followed by the last character.
2. If more than one word shares the same abbreviation, then resolve the conflict by using a longer prefix instead of only the first character until the abbreviation is unique.
3. If the abbreviation doesn't make the word shorter, then keep it as the original word.

Example 1:
Input: words = ["like","god","internal","me","internet","interval","intension","face","intrusion"]
Output: ["l2e","god","internal","me","i6t","interval","inte4n","f2e","intr4n"]

Example 2:
Input: words = ["aa","aaa"]
Output: ["aa","aaa"]

Constraints:
1 <= words.length <= 400
2 <= words[i].length <= 400
words[i] consists of lowercase English letters.
All strings of words are unique.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem527
{
    public class Solution
    {
        public static void Init()
        {
            string[] words = ["like", "god", "internal", "me", "internet", "interval", "intension", "face", "intrusion"];
            var res = WordsAbbreviation(words);
            Console.WriteLine(string.Join(", ", res)); // l2e, god, internal, me, i6t, interval, inte4n, f2e, intr4n
        }

        // Time: O(N * L^2) where N is words.Count and L is max word length
        // Space: O(N * L) for storing abbreviations and prefix lengths
        public static IList<string> WordsAbbreviation(IList<string> words)
        {
            if (words == null || words.Count == 0)
            {
                return [];
            }

            int n = words.Count;
            string[] abbr = new string[n];
            int[] prefix = new int[n];

            for (int i = 0; i < n; i++)
            {
                prefix[i] = 1;
                abbr[i] = MakeAbbr(words[i], 1);
            }

            for (int i = 0; i < n; i++)
            {
                while (true)
                {
                    List<int> conflicts = [];

                    for (int j = i + 1; j < n; j++)
                    {
                        if (abbr[i] == abbr[j])
                        {
                            conflicts.Add(j);
                        }
                    }

                    if (conflicts.Count == 0)
                    {
                        break;
                    }

                    conflicts.Add(i);

                    foreach (int idx in conflicts)
                    {
                        prefix[idx]++;
                        abbr[idx] = MakeAbbr(words[idx], prefix[idx]);
                    }
                }
            }

            return abbr;
        }

        private static string MakeAbbr(string word, int k)
        {
            if (word.Length - k <= 2)
            {
                return word;
            }

            return $"{word.Substring(0, k)}{word.Length - 1 - k}{word[^1]}";
        }
    }
}