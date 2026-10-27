// Url: https://leetcode.com/problems/implement-strstr/

/*
28. Implement strStr()
Easy

Implement strStr().

Return the index of the first occurrence of needle in haystack, or -1 if needle is not part of haystack.

Example 1:
Input: haystack = "hello", needle = "ll"
Output: 2

Example 2:
Input: haystack = "aaaaa", needle = "bba"
Output: -1

Clarification:
What should we return when needle is an empty string? This is a great question to ask during an interview.

For the purpose of this problem, we will return 0 when needle is an empty string. This is consistent to C's strstr() and Java's indexOf().
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem28 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (StrStr_a(null, null));
            Console.WriteLine (StrStr_a("", ""));
            Console.WriteLine (StrStr_a("hello", null));
            Console.WriteLine (StrStr_a(null, "ll"));
            Console.WriteLine (StrStr_a("a", "a"));
            Console.WriteLine (StrStr_a("hello", "ll"));
            Console.WriteLine (StrStr_a("aaaaa", "bba"));

            Console.WriteLine (StrStr_b(null, null));
            Console.WriteLine (StrStr_b("", ""));
            Console.WriteLine (StrStr_b("hello", null));
            Console.WriteLine (StrStr_b(null, "ll"));
            Console.WriteLine (StrStr_b("hello", "ll"));
            Console.WriteLine (StrStr_b("aaaaa", "bba"));
        }

        // Time: O ((n-l)l), where n is length of haystack, and l is length of needle
        // Space: O (1)
        public static int StrStr_a (string haystack, string needle) {
            if (needle == null) {
                return -1;
            }

            if (haystack == null) {
                return -1;
            }

            int l1 = haystack.Length;
            int l2 = needle.Length;

            if (l1 < l2) {
                return -1;
            } else if (l2 == 0) {
                return 0;
            }

            int result = -1;

            for (int i = 0; i <= l1 - l2; i++) {
                int j = 0;

                for (; j < l2; j++) {
                    if (haystack[i + j] != needle[j]) {
                        break;
                    }
                }

                if (j == l2) {
                    return i;
                }
            }

            return result;
        }

        // Time: O ()
        // Space: O ()
        public static int StrStr_b (string haystack, string needle) {
            if ((haystack == null && needle != null) || (haystack != null && needle == null) || (haystack != null && needle != null && haystack.Length < needle.Length)) {
                return -1;
            }

            if ((haystack == null && needle == null) || (haystack != null && needle != null && haystack.Length == 0 && needle.Length == 0)) {
                return 0;
            }

            int result = -1;

            // TODO KMP

            return result;
        }
    }
}