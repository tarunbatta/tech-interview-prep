// Url: https://leetcode.com/problems/longest-substring-with-at-most-k-distinct-characters

/*
340. Longest Substring with At Most K Distinct Characters
Medium

Given a string s and an integer k, return the length of the longest substring of s that contains at most k distinct characters.

Example 1:
Input: s = "eceba", k = 2
Output: 3
Explanation: The substring is "ece" with its length 3.

Example 2:
Input: s = "aa", k = 1
Output: 2
Explanation: The substring is "aa" with its length 2.

Constraints:
1 <= s.length <= 5 * 10^4
0 <= k <= 50
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem340
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(LengthOfLongestSubstringKDistinct("eceba", 2)); // 3
            Console.WriteLine(LengthOfLongestSubstringKDistinct("aa", 1));    // 2
        }

        // Time: O(n) where n is s.Length
        // Space: O(k) auxiliary space for the character count hash map
        public static int LengthOfLongestSubstringKDistinct(string s, int k)
        {
            if (string.IsNullOrEmpty(s) || k <= 0)
            {
                return 0;
            }

            int maxLength = 0;
            int left = 0;
            Dictionary<char, int> charCounts = [];

            for (int right = 0; right < s.Length; right++)
            {
                char c = s[right];
                charCounts[c] = charCounts.GetValueOrDefault(c, 0) + 1;

                while (charCounts.Count > k)
                {
                    char leftChar = s[left];
                    charCounts[leftChar]--;

                    if (charCounts[leftChar] == 0)
                    {
                        charCounts.Remove(leftChar);
                    }

                    left++;
                }

                maxLength = Math.Max(maxLength, right - left + 1);
            }

            return maxLength;
        }
    }
}