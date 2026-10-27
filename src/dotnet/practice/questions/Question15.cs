using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Write a function to find all permutations of a string
    /// </summary>
    public class Question15 {
        public static void Init (string[] args) {
            GetPermutations (null);
            
            GetPermutations ("abcd");
        }

        private static void GetPermutations (string input) {
            GetPermutations (input, string.Empty);
        }

        private static void GetPermutations (string input, string prefix) {
            if (!string.IsNullOrEmpty (input)) {
                for (int i = 0; i < input.Length; i++) {
                    string remaining = input.Substring (0, i) + input.Substring (i + 1);
                    GetPermutations (remaining, prefix + (input[i]).ToString ());
                }
            } else {
                Console.WriteLine (prefix);
            }
        }
    }
}