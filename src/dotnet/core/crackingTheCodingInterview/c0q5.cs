/*
Find the permutations of a word in a sentence.
*/

using System;

namespace InterviewPreperationGuide.Core.CrackingTheCodingInterview.c0q5 {
    public class Solution {
        public static void Init (string[] args) {
            Console.WriteLine (PermutationsOfWordInSentence (null, null));
            Console.WriteLine (PermutationsOfWordInSentence ("abbc", null));
            Console.WriteLine (PermutationsOfWordInSentence (null, "cbabadebbabbebabaabcebabe"));
            Console.WriteLine (PermutationsOfWordInSentence ("abbc", "cbabadebbabbebabaabcebabe"));
        }

        private static int PermutationsOfWordInSentence (string word, string sentence) {
            int result = 0;

            if (!string.IsNullOrEmpty (word) && !string.IsNullOrEmpty (sentence)) {
                char[] wordArray = word.ToCharArray ();
                Array.Sort (wordArray);
                string sortedWord = wordArray.ToString ();

                for (int i = 0; i < sentence.Length; i++) {
                    if ((i + 4) < sentence.Length) {
                        char[] sectionArray = sentence.Substring (i, 4).ToCharArray ();
                        Array.Sort (sectionArray);
                        string sortedSection = sectionArray.ToString ();

                        if (string.Compare (sortedWord, sortedSection) == 0) {
                            result++;
                        }
                    }
                }
            }

            return result;
        }
    }
}