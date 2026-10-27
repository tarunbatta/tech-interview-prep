// Url:https://leetcode.com/problems/letter-combinations-of-a-phone-number

/*
17. Letter Combinations of a Phone Number
Medium

Given a string containing digits from 2-9 inclusive, return all possible letter combinations that the number could represent.

A mapping of digit to letters (just like on the telephone buttons) is given below. Note that 1 does not map to any letters.



Example:

Input: "23"
Output: ["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"].


Note:

Although the above answer is in lexicographical order, your answer could be in any order you want.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem17 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (LetterCombinations_a("23"));
        }

        // Time: O (4^n)
        // Space: O (4^n)
        public static IList<string> LetterCombinations_a (string digits) {
            if (digits == null || digits.Length == 0) {
                return [];
            }

            string[] buttons = ["0", "1", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz"];
            Queue<string> q = new Queue<string> ();
            q.Enqueue ("");

            for (int i = 0; i < digits.ToCharArray ().Length; i++) {
                char[] letters = buttons[(char) digits[i] - '0'].ToCharArray ();

                while (q.Peek ().Length == i) {
                    string remove = q.Dequeue ();

                    foreach (var c in letters) {
                        q.Enqueue (remove + c);
                    }
                }
            }

            return [.. q];
        }

        Dictionary<string, string> phone = new Dictionary<string, string> () { { "2", "abc" }, { "3", "def" }, { "4", "ghi" }, { "5", "jkl" }, { "6", "mno" }, { "7", "pqrs" }, { "8", "tuv" }, { "9", "wxyz" }
        };

        List<string> output = [];

        // Time: O (3^n * 4^m), where n is the number of digits in the input that maps to 3 letters
        // Space: O (3^n * 4^m), where m is the number of digits in the input that maps to 4 letters
        public IList<string> LetterCombinations_b (string digits) {
            if (digits.Length != 0) {
                backtrack ("", digits);
            }

            return output;
        }

        private void backtrack (string combination, string next_digits) {
            // if there is no more digits to check
            if (next_digits.Length == 0) {
                // the combination is done
                output.Add (combination);
            }
            // if there are still digits to check
            else {
                // iterate over all letters which map 
                // the next available digit
                String digit = next_digits.Substring (0, 1);
                String letters = phone[digit];

                for (int i = 0; i < letters.Length; i++) {
                    String letter = letters.Substring (i, 1);
                    // append the current letter to the combination
                    // and proceed to the next digits
                    backtrack (combination + letter, next_digits.Substring (1));
                }
            }
        }
    }
}