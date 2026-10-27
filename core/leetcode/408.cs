// Url: https://leetcode.com/problems/valid-word-abbreviation

/*
408. Valid Word Abbreviation
Easy

A string can be abbreviated by replacing any number of non-adjacent, non-empty substrings with their lengths. The lengths should not have leading zeros.

For example, a string such as "substitution" could be abbreviated as (but not limited to):
- "s10n" ("s ubstitutio n")
- "sub4u4" ("sub stit u tion")
- "12" ("substitution")
- "su3i1u2on" ("su bst i t u ti on")
- "substitution" (no substrings replaced)

The following are not valid abbreviations:
- "s55n" ("s ubsti tutio n", the replaced substrings are adjacent)
- "s010n" (has leading zeros)
- "s0ubstitution" (replaces an empty substring)

Given a string word and an abbreviation abbr, return whether the string matches the given abbreviation.

Example 1:
Input: word = "internationalization", abbr = "i12iz4n"
Output: true
Explanation: The word "internationalization" can be abbreviated as "i12iz4n" ("i nternational iz atio n").

Example 2:
Input: word = "apple", abbr = "a2e"
Output: false
Explanation: The word "apple" cannot be abbreviated as "a2e".

Constraints:
1 <= word.length <= 20
word consists of only lowercase English letters.
1 <= abbr.length <= 10
abbr consists of lowercase English letters and digits.
All the integers in abbr will fit in a 32-bit integer.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem408
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(ValidWordAbbreviation("internationalization", "i12iz4n")); // True
            Console.WriteLine(ValidWordAbbreviation("apple", "a2e"));                    // False
        }

        // Time: O(m + n) where m is word.Length and n is abbr.Length
        // Space: O(1) auxiliary space
        public static bool ValidWordAbbreviation(string word, string abbr)
        {
            if (word == null || abbr == null)
            {
                return false;
            }

            int i = 0;
            int j = 0;

            while (i < word.Length && j < abbr.Length)
            {
                if (char.IsDigit(abbr[j]))
                {
                    // Leading zeros are invalid
                    if (abbr[j] == '0')
                    {
                        return false;
                    }

                    int num = 0;
                    while (j < abbr.Length && char.IsDigit(abbr[j]))
                    {
                        num = num * 10 + (abbr[j] - '0');
                        j++;
                    }

                    i += num;
                }
                else
                {
                    if (word[i] != abbr[j])
                    {
                        return false;
                    }

                    i++;
                    j++;
                }
            }

            return i == word.Length && j == abbr.Length;
        }
    }
}