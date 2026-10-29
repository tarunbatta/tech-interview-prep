// Url:https://leetcode.com/problems/reverse-words-in-a-string

/*
151. Reverse Words in a String
Medium

Given an input string, reverse the string word by word.


Example 1:

Input: "the sky is blue"
Output: "blue is sky the"


Example 2:

Input: "  hello world!  "
Output: "world! hello"
Explanation: Your reversed string should not contain leading or trailing spaces.


Example 3:

Input: "a good   example"
Output: "example good a"
Explanation: You need to reduce multiple spaces between two words to a single space in the reversed string.

Note:
	A word is defined as a sequence of non-space characters.
	Input string may contain leading or trailing spaces. However, your reversed string should not contain leading or trailing spaces.
	You need to reduce multiple spaces between two words to a single space in the reversed string. 

Follow up:
For C programmers, try to solve it in-place in O(1) extra space.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem151 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (ReverseWords (null));
            Console.WriteLine (ReverseWords (""));
            Console.WriteLine (ReverseWords ("the sky is blue"));
            Console.WriteLine (ReverseWords ("  hello world!  "));
            Console.WriteLine (ReverseWords ("a good   example"));
        }

        // Time: O (n)
        // Space: O (n)
        public static string ReverseWords (string s) {
            if (string.IsNullOrEmpty (s)) {
                return s;
            }

            char[] c = s.ToCharArray ();
            int n = s.Length;

            ReverseString(c, 0, n - 1);
            ReverseWords(c, n);
            int end = CleanSpaces(c, n);

            return new string (c, 0, end);
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