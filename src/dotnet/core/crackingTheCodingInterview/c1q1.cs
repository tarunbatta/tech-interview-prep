/*
Is Unique: 

Implement an algorithm to determine if a string has all unique characters.
What if you cannot use additional data structures?
*/

using System;

namespace InterviewPreperationGuide.Core.CrackingTheCodingInterview.c1q1 {
    public class Solution {
        public static void Init (string[] args) {
            Console.WriteLine ("abcdѐ: " + HasUniqueCharacters1 ("abcdѐ"));
            Console.WriteLine ("Empty: " + HasUniqueCharacters1 (""));
            Console.WriteLine ("Null: " + HasUniqueCharacters1 (null));
            Console.WriteLine ("abbc: " + HasUniqueCharacters1 ("abbc"));
            Console.WriteLine ("abca: " + HasUniqueCharacters1 ("abca"));

            Console.WriteLine ("abcd: " + HasUniqueCharacters2 ("abcd"));
            Console.WriteLine ("Empty: " + HasUniqueCharacters2 (""));
            Console.WriteLine ("Null: " + HasUniqueCharacters2 (null));
            Console.WriteLine ("abbc: " + HasUniqueCharacters2 ("abbc"));
            Console.WriteLine ("abca: " + HasUniqueCharacters2 ("abca"));

            Console.WriteLine ("abcd: " + HasUniqueCharacters3 ("abcd"));
            Console.WriteLine ("Empty: " + HasUniqueCharacters3 (""));
            Console.WriteLine ("Null: " + HasUniqueCharacters3 (null));
            Console.WriteLine ("abbc: " + HasUniqueCharacters3 ("abbc"));
            Console.WriteLine ("abca: " + HasUniqueCharacters3 ("abca"));
        }

        private static bool HasUniqueCharacters1 (string input) {
            if (string.IsNullOrEmpty (input)) {
                return true;
            }

            for (int i = 0; i < input.Length; i++) {
                for (int j = 0; j < input.Length; j++) {
                    if (input[i] == input[j] && i != j) {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool HasUniqueCharacters2 (string input) {
            if (string.IsNullOrEmpty (input)) {
                return true;
            }

            if (input.Length > 256) {
                return false;
            }

            bool[] characterset = new bool[256];
            for (int i = 0; i < input.Length; i++) {
                int characterValue = Convert.ToInt32 (input[i]);

                if (characterset[characterValue]) {
                    return false;
                }

                characterset[characterValue] = true;
            }

            return true;
        }

        private static bool HasUniqueCharacters3 (string input) {
            if (string.IsNullOrEmpty (input)) {
                return true;
            }

            int checker = 0;

            for (int i = 0; i < input.Length; i++) {
                int characterValue = Convert.ToInt32 (input[i]);

                if ((checker & (1 << characterValue)) > 0) {
                    return false;
                }

                checker |= (1 << characterValue);
            }

            return true;
        }
    }
}