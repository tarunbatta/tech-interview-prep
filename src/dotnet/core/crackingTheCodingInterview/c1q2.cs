/*
Check Permutation: 

Given two strings, write a method to decide if one is a permutation of the other.
*/

using System;
using System.Linq;

namespace InterviewPreperationGuide.Core.CrackingTheCodingInterview.c1q2 {
    public class Solution {
        public static void Init (string[] args) {
            Console.WriteLine (CheckStringPermutation1 ("", ""));

            Console.WriteLine (CheckStringPermutation1 (null, null));

            Console.WriteLine (CheckStringPermutation1 ("", null));

            Console.WriteLine (CheckStringPermutation1 (null, ""));

            Console.WriteLine (CheckStringPermutation1 ("abcd", "efgh"));

            Console.WriteLine (CheckStringPermutation1 ("abcd", "bdac"));

            Console.WriteLine (CheckStringPermutation1 ("abcd", "bdAc"));

            Console.WriteLine (CheckStringPermutation1 ("abcde", "bdac"));

            Console.WriteLine (CheckStringPermutation2 ("", ""));

            Console.WriteLine (CheckStringPermutation2 (null, null));

            Console.WriteLine (CheckStringPermutation2 ("", null));

            Console.WriteLine (CheckStringPermutation2 (null, ""));

            Console.WriteLine (CheckStringPermutation2 ("abcd", "efgh"));

            Console.WriteLine (CheckStringPermutation2 ("abcd", "bdac"));

            Console.WriteLine (CheckStringPermutation2 ("abcd", "bdAc"));

            Console.WriteLine (CheckStringPermutation2 ("abcde", "bdac"));
        }

        private static string SortString (string input) {
            string result = string.Empty;

            if (!string.IsNullOrEmpty (input)) {
                char[] charactersInString = input.ToCharArray ();
                Array.Sort (charactersInString);
                result = new string (charactersInString);
            }

            return result;
        }

        private static bool CheckStringPermutation1 (string inputA, string inputB) {
            if (string.IsNullOrEmpty (inputA) || string.IsNullOrEmpty (inputB)) {
                return false;
            }

            if (inputA.Length != inputB.Length) {
                return false;
            }

            return string.Compare (SortString (inputA), SortString (inputB)) == 0;
        }

        private static bool CheckStringPermutation2 (string inputA, string inputB) {
            if (string.IsNullOrEmpty (inputA) || string.IsNullOrEmpty (inputB)) {
                return false;
            }

            if (inputA.Length != inputB.Length) {
                return false;
            }

            int[] letters = new int[256];
            char[] charactersinStringA = [.. inputA];

            for (int i = 0; i < inputA.Length; i++) {
                int characterValue = Convert.ToInt32 (inputA[i]);
                letters[characterValue]++;
            }

            for (int i = 0; i < inputB.Length; i++) {
                int characterValue = Convert.ToInt32 (inputB[i]);
                letters[characterValue]--;

                if (letters[characterValue] < 0) {
                    return false;
                }
            }

            return true;
        }
    }
}