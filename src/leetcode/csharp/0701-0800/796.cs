// Url: https://leetcode.com/problems/rotate-string/

/*
796. Rotate String
Easy

We are given two strings, A and B.

A shift on A consists of taking string A and moving the leftmost character to the rightmost position. For example, if A = 'abcde', then it will be 'bcdea' after one shift on A. Return True if and only if A can become B after some number of shifts on A.

Example 1:
Input: A = 'abcde', B = 'cdeab'
Output: true

Example 2:
Input: A = 'abcde', B = 'abced'
Output: false
Note:

A and B will have length at most 100.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem796 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (RotateString(null, null));
            Console.WriteLine (RotateString("", ""));
            Console.WriteLine (RotateString("abcde", ""));
            Console.WriteLine (RotateString("", "cdeab"));
            Console.WriteLine (RotateString("abcde", "cdeab"));
            Console.WriteLine (RotateString("abcde", "abced"));
            Console.WriteLine (RotateString("aa", "a"));
        }

        public static bool RotateString (string A, string B) {
            return A.Length == B.Length && (B + B).Contains (A);
        }
    }
}