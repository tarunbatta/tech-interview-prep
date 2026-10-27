/*
A ransom note can be formed by cutting words out of a magazine to form a new sentence.
How would you figure out if a ransom note(represented as a string) can be formed from a given magazine(string)?
*/

using System;

namespace InterviewPreperationGuide.Core.CrackingTheCodingInterview.c0q4 {
    internal class Solution {
        public static void Init (string[] args) {
            Console.WriteLine (IsRandsomNotePossible ("age", "random note magazine"));
            Console.WriteLine (IsRandsomNotePossible ("zebra", "random note magazine"));
        }

        private static bool IsRandsomNotePossible (string note, string magazine) {
            bool result = true;

            if (!string.IsNullOrEmpty (note) && !string.IsNullOrEmpty (magazine)) {
                int[] letters = new int[256];

                for (int i = 0; i < magazine.Length; i++) {
                    if (string.Compare (magazine[i].ToString (), " ") != 0) {
                        int characterValue = Convert.ToInt32 (magazine[i]);
                        letters[characterValue]++;
                    }
                }

                for (int i = 0; i < note.Length; i++) {
                    int characterValue = Convert.ToInt32 (note[i]);
                    letters[characterValue]--;

                    if (letters[characterValue] < 0) {
                        result = false;
                    }
                }
            } else {
                result = false;
            }

            return result;
        }
    }
}