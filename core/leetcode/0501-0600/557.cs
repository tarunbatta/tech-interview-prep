// Url:https://leetcode.com/problems/reverse-words-in-a-string-iii

/*
557. Reverse Words in a String III
Easy

Given a string, you need to reverse the order of characters in each word within a sentence while still preserving whitespace and initial word order.

Example 1:
Input: "Let's take LeetCode contest"
Output: "s'teL ekat edoCteeL tsetnoc"



Note:
In the string, each word is separated by single space and there will not be any extra space in the string.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem557 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static string ReverseWords (string s) {
            if (string.IsNullOrEmpty (s)) {
                return s;
            }

            char[] arr = s.ToCharArray ();
            int i = 0;
            int j = 0;

            while (j < arr.Length) {
                if (arr[j] == ' ') {
                    Reverse(arr, i, j - 1);
                    i = j + 1;
                }

                j++;
            }

            if (j == arr.Length) {
                Reverse(arr, i, j - 1);
            }

            return new string (arr);
        }

        private static void Reverse (char[] arr, int i, int j) {
            while (i < j) {
                char temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;

                i++;
                j--;
            }
        }
    }
}