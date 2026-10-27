// Url: https://leetcode.com/problems/reverse-string/

/*
344. Reverse String
Easy

Write a function that reverses a string. The input string is given as an array of characters char[].

Do not allocate extra space for another array, you must do this by modifying the input array in-place with O(1) extra memory.

You may assume all the characters consist of printable ascii characters.

 

Example 1:

Input: ['h','e','l','l','o']
Output: ['o','l','l','e','h']
Example 2:

Input: ['H','a','n','n','a','h']
Output: ['h','a','n','n','a','H']
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem344 {
    public class Solution {
        public static void Init () {
            ReverseString(null);
            ReverseString(['h', 'e', 'l', 'l', 'o']);
            ReverseString(['H', 'a', 'n', 'n', 'a', 'h']);
        }

        public static void ReverseString (char[] s) {
            if (s == null || s.Length <= 1) {
                return;
            }

            int i = 0;
            int j = s.Length - 1;

            while (i < j) {
                char temp = s[i];
                s[i] = s[j];
                s[j] = temp;

                i++;
                j--;
            }
        }
    }
}