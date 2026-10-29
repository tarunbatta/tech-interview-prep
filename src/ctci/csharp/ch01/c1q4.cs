/*
Palindrome Permutation: 

Given a string, write a function to check if it is a permutation of a palindrome.
A palindrome is a word or phrase that is the same forwards and backwards. A permutation
is a rearrangement of letters. The palindrome does not need to be limited to just dictionary words.

EXAMPLE
Input: Tact Coa
Output: True (permutations: "taco cat", "atco cta", etc.)
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c1q4 {
    public class Solution {
        public static void Init (string[] args = null) {
            Console.WriteLine (IsPalindromePermutation ("Tact Coa")); // True
            Console.WriteLine (IsPalindromePermutation ("test"));     // False
        }

        public static bool IsPalindromePermutation (string str) {
            if (str == null) {
                return false;
            }

            Dictionary<char, int> map = new Dictionary<char, int> ();
            foreach (char ch in str) {
                char c = char.ToLower (ch);
                if (!char.IsLetter (c)) {
                    continue;
                }

                if (map.ContainsKey (c)) {
                    map[c]++;
                } else {
                    map[c] = 1;
                }
            }

            int oddCount = 0;
            foreach (int count in map.Values) {
                if (count % 2 != 0) {
                    oddCount++;
                }
            }

            return oddCount <= 1;
        }
    }
}
