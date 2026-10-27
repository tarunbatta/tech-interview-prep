// Url: https://leetcode.com/problems/valid-palindrome/

/*
125. Valid Palindrome
Easy

Given a string, determine if it is a palindrome, considering only alphanumeric characters and ignoring cases.

Note: For the purpose of this problem, we define empty string as valid palindrome.

Example 1:

Input: "A man, a plan, a canal: Panama"
Output: true
Example 2:

Input: "race a car"
Output: false
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem125 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (IsPalindrome(null));
            Console.WriteLine (IsPalindrome(""));
            Console.WriteLine (IsPalindrome("madam"));
            Console.WriteLine (IsPalindrome("A man, a plan, a canal: Panama"));
            Console.WriteLine (IsPalindrome("race a car"));
        }

        public static bool IsPalindrome (string s) {
            if (s == null || s.Length <= 1) {
                return true;
            }

            bool result = true;
            int i = 0;
            int j = s.Length - 1;

            while (i < j) {
                if (!char.IsLetterOrDigit (s[i])) {
                    i++;
                } else if (!char.IsLetterOrDigit (s[j])) {
                    j--;
                } else {
                    if (char.ToLower (s[i]) != char.ToLower (s[j])) {
                        return false;
                    }

                    i++;
                    j--;
                }
            }

            return result;
        }
    }
}