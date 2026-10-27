// Url:https://leetcode.com/problems/reverse-words-in-a-string-ii

/*
186. Reverse Words in a String II
Medium

Given an input string , reverse the string word by word. 

Example:

Input:  ['t','h','e',' ','s','k','y',' ','i','s',' ','b','l','u','e']
Output: ['b','l','u','e',' ','i','s',' ','s','k','y',' ','t','h','e']

Note: 
A word is defined as a sequence of non-space characters.
The input string does not contain leading or trailing spaces.
The words are always separated by a single space.

Follow up: Could you do it in-place without allocating extra space?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem186 {
    public class Solution {
        public static void Init() {
            ReverseWords (null);
            ReverseWords ([]);
            ReverseWords (['t', 'h', 'e', ' ', 's', 'k', 'y', ' ', 'i', 's', ' ', 'b', 'l', 'u', 'e']);
        }

        // Time: O (n)
        // Space: O (1)
        public static void ReverseWords (char[] s) {
            if (s == null || s.Length == 0) {
                return;
            }

            int n = s.Length;

            ReverseString(s, 0, n - 1);
            ReverseWords(s, n);
            int end = CleanSpaces(s, n);
        }

        private static void ReverseWords (char[] c, int n) {
            int i = 0;
            int j = 0;

            while (i < n) {
                while (i < j || i < n && c[i] == ' ') i++;
                while (j < i || j < n && c[j] != ' ') j++;

                ReverseString(c, i, j - 1);
            }
        }

        private static int CleanSpaces (char[] c, int n) {
            int i = 0;
            int j = 0;

            while (j < n) {
                while (j < n && c[j] == ' ') j++;
                while (j < n && c[j] != ' ') c[i++] = c[j++];
                while (j < n && c[j] == ' ') j++;

                if (j < n) {
                    c[i++] = ' ';
                }
            }

            return i;
        }

        private static void ReverseString (char[] c, int i, int j) {
            while (i < j) {
                char t = c[i];
                c[i++] = c[j];
                c[j--] = t;
            }
        }
    }
}