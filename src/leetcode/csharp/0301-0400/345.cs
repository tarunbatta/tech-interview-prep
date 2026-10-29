// Url:https://leetcode.com/problems/reverse-vowels-of-a-string

/*
345. Reverse Vowels of a String
Easy

Write a function that takes a string as input and reverse only the vowels of a string.

Example 1:

Input: "hello"
Output: "holle"



Example 2:

Input: "leetcode"
Output: "leotcede"


Note:
The vowels does not include the letter "y".
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem345 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n)
        // Space: O (1)
        public static string ReverseVowels (string s) {
            if (string.IsNullOrEmpty (s)) {
                return s;
            }

            char[] arr = s.ToCharArray ();
            HashSet<char> hash =
            [
                'A',
                'E',
                'I',
                'O',
                'U',
                'a',
                'e',
                'i',
                'o',
                'u',
            ];

            int i = 0;
            int j = arr.Length - 1;

            while (i < j) {
                while (i < j && !hash.Contains (arr[i])) i++;
                while (i < j && !hash.Contains (arr[j])) j--;

                Swap(arr, i, j);
                i++;
                j--;
            }

            return new string (arr);
        }

        private static void Swap (char[] arr, int i, int j) {
            if (i < j) {
                char temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
    }
}