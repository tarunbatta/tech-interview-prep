// Url: https://leetcode.com/problems/longest-substring-with-at-most-two-distinct-characters

/*
159. Longest Substring with At Most Two Distinct Characters
Medium

Given a string s, return the length of the longest substring that contains at most two distinct characters.

Example 1:
Input: s = "eceba"
Output: 3
Explanation: The substring is "ece" which its length is 3.

Example 2:
Input: s = "ccaabbb"
Output: 5
Explanation: The substring is "aabbb" which its length is 5.

Constraints:
1 <= s.length <= 10^5
s consists of English letters.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem159
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(LengthOfLongestSubstringTwoDistinct("eceba"));   // 3
            Console.WriteLine(LengthOfLongestSubstringTwoDistinct("ccaabbb")); // 5
        }

        // Time: O(n) where n is s.Length
        // Space: O(1) auxiliary space (at most 3 distinct character entries in map)
        public static int LengthOfLongestSubstringTwoDistinct(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            int maxLength = 0;
            int left = 0;
            Dictionary<char, int> charCounts = new();

            for (int right = 0; right < s.Length; right++)
            {
                char rightChar = s[right];
                charCounts[rightChar] = charCounts.GetValueOrDefault(rightChar, 0) + 1;

                while (charCounts.Count > 2)
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