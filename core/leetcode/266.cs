// Url: https://leetcode.com/problems/palindrome-permutation

/*
266. Palindrome Permutation
Easy

Given a string s, return true if a permutation of the string could form a palindrome and false otherwise.

Example 1:
Input: s = "code"
Output: false

Example 2:
Input: s = "aab"
Output: true

Example 3:
Input: s = "carerac"
Output: true

Constraints:
1 <= s.length <= 5000
s consists of only lowercase English letters.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem266
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(CanPermutePalindrome("code"));    // False
            Console.WriteLine(CanPermutePalindrome("aab"));     // True
            Console.WriteLine(CanPermutePalindrome("carerac")); // True
        }

        // Time: O(n) where n is s.Length
        // Space: O(1) auxiliary space (at most 26 entries for lowercase letters)
        public static bool CanPermutePalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return true;
            }

            HashSet<char> oddChars = new();

            foreach (char c in s)
            {
                if (!oddChars.Add(c))
                {
                    oddChars.Remove(c);
                }
            }

            return oddChars.Count <= 1;
        }
    }
}