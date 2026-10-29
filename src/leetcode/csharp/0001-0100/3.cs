// Url: https://leetcode.com/problems/longest-substring-without-repeating-characters/

/*
3. Longest Substring Without Repeating Characters
Medium

Given a string, find the length of the longest substring without repeating characters.

Example 1:

Input: "abcabcbb"
Output: 3 
Explanation: The answer is "abc", with the length of 3. 
Example 2:

Input: "bbbbb"
Output: 1
Explanation: The answer is "b", with the length of 1.
Example 3:

Input: "pwwkew"
Output: 3
Explanation: The answer is "wke", with the length of 3. 
             Note that the answer must be a substring, "pwke" is a subsequence and not a substring.
*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.LeetCode.problem3 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (LengthOfLongestSubstring(null));
            Console.WriteLine (LengthOfLongestSubstring("abcabcbb"));
            Console.WriteLine (LengthOfLongestSubstring("bbbbb"));
            Console.WriteLine (LengthOfLongestSubstring("pwwkew"));
            Console.WriteLine (LengthOfLongestSubstring("dvdf"));
        }

        // Time: O (n)
        // Space: O (min (m,n))
        public static int LengthOfLongestSubstring (String s) {
            int result = 0;

            if (string.IsNullOrEmpty (s)) {
                return result;
            }

            Hashtable hash = [];

            for (int i = 0, j = 0; i < s.Length; i++) {
                if (hash.ContainsKey (s[i])) {
                    j = Math.Max (j, (int) hash[s[i]] + 1);
                    hash[s[i]] = i;
                } else {
                    hash.Add (s[i], i);
                }

                result = Math.Max (result, i - j + 1);
            }

            return result;
        }
    }
}