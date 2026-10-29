using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Check whether a string is a Palindrome
    /// </summary>
    public class Question6 {
        public static void Init (string[] args) {
            Console.WriteLine ("Empty: " + CheckIfPalindrome (string.Empty));
            Console.WriteLine ("Null: " + CheckIfPalindrome (null));
            Console.WriteLine ("Space: " + CheckIfPalindrome (" "));
            Console.WriteLine ("0: " + CheckIfPalindrome ("0"));
            Console.WriteLine ("abcdEdcba: " + CheckIfPalindrome ("abcdEdcba"));
            Console.WriteLine ("abcddcba: " + CheckIfPalindrome ("abcddcba"));
            Console.WriteLine ("xyzxyz: " + CheckIfPalindrome ("xyzxyz"));
        }

        private static bool CheckIfPalindrome (string input) {
            bool result = true;

            if (!string.IsNullOrEmpty (input)) {
                for (int i = 0, j = (input.Length - 1); i < (input.Length / 2); i++, j--) {
                    if (input[i] != input[j]) {
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