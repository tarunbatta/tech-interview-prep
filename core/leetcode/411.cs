// Url: https://leetcode.com/problems/minimum-unique-word-abbreviation

/*
411. Minimum Unique Word Abbreviation
Hard

A string can be abbreviated by replacing any number of non-adjacent, non-empty substrings with their lengths. The lengths should not have leading zeros.

Given a string target and an array of strings dictionary, return an abbreviation of target with the shortest possible length such that it is not an abbreviation of any string in dictionary. If there are multiple shortest abbreviations, return any of them.

Example 1:
Input: target = "apple", dictionary = ["blade"]
Output: "a4"
Explanation: The shortest abbreviation of "apple" is "5", but it is an abbreviation of "blade".
The next shortest abbreviations are "a4" and "4e". Both of them are not abbreviations of "blade", so "a4" is returned.

Example 2:
Input: target = "apple", dictionary = ["blade","plain","amber"]
Output: "1p3"

Constraints:
m == target.length
n == dictionary.length
1 <= m <= 21
0 <= n <= 1000
1 <= dictionary[i].length <= 100
target and dictionary[i] consist of lowercase English letters.
dictionary does not contain target.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem411
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(MinAbbreviation("apple", ["blade"]));                   // a4
            Console.WriteLine(MinAbbreviation("apple", ["blade", "plain", "amber"])); // 1p3
        }

        // Time: O(2^m + D) where m is target.Length and D is dictionary.Length
        // Space: O(D) for difference bitmasks
        public static string MinAbbreviation(string target, string[] dictionary)
        {
            if (string.IsNullOrEmpty(target))
            {
                return string.Empty;
            }

            int n = target.Length;
            List<int> diffMasks = [];

            // Filter dictionary: only words of identical length can collide
            if (dictionary != null)
            {
                foreach (string word in dictionary)
                {
                    if (word.Length == n)
                    {
                        int mask = 0;
                        for (int i = 0; i < n; i++)
                        {
                            if (target[i] != word[i])
                            {
                                mask |= (1 << (n - 1 - i));
                            }
                        }

                        diffMasks.Add(mask);
                    }
                }
            }

            // If no dictionary word has length n, abbreviate the entire word to its length
            if (diffMasks.Count == 0)
            {
                return n.ToString();
            }

            int minLen = int.MaxValue;
            int bestMask = (1 << n) - 1;

            // Search through all 2^n possible masks
            for (int mask = 0; mask < (1 << n); mask++)
            {
                int len = GetAbbrLength(mask, n);

                if (len >= minLen)
                {
                    continue;
                }

                bool valid = true;
                foreach (int diff in diffMasks)
                {
                    // If mask & diff == 0, the abbreviation does not retain any differing character
                    if ((mask & diff) == 0)
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    minLen = len;
                    bestMask = mask;
                }
            }

            return ConstructAbbreviation(target, bestMask, n);
        }

        private static int GetAbbrLength(int mask, int n)
        {
            int length = 0;
            int zeros = 0;

            for (int i = 0; i < n; i++)
            {
                if ((mask & (1 << (n - 1 - i))) != 0)
                {
                    if (zeros > 0)
                    {
                        length++;
                        zeros = 0;
                    }

                    length++;
                }
                else
                {
                    zeros++;
                }
            }

            if (zeros > 0)
            {
                length++;
            }

            return length;
        }

        private static string ConstructAbbreviation(string target, int mask, int n)
        {
            StringBuilder sb = new();
            int zeros = 0;

            for (int i = 0; i < n; i++)
            {
                if ((mask & (1 << (n - 1 - i))) != 0)
                {
                    if (zeros > 0)
                    {
                        sb.Append(zeros);
                        zeros = 0;
                    }

                    sb.Append(target[i]);
                }
                else
                {
                    zeros++;
                }
            }

            if (zeros > 0)
            {
                sb.Append(zeros);
            }

            return sb.ToString();
        }
    }
}