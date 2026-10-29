using System;
using System.Text;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Write a function to reverse a string
    /// </summary>
    public class Question7 {
        public static void Init (string[] args) {
            Console.WriteLine ("Empty: " + ReverseString (string.Empty));
            Console.WriteLine ("Null: " + ReverseString (null));
            Console.WriteLine ("Space: " + ReverseString (" "));
            Console.WriteLine ("0: " + ReverseString ("0"));
            Console.WriteLine ("abcd: " + ReverseString ("abcd"));
            Console.WriteLine ("a12#d: " + ReverseString ("a12#d"));
        }

        private static string ReverseString (string input) {
            StringBuilder result = new StringBuilder ();

            if (!string.IsNullOrEmpty (input)) {
                for (int i = input.Length - 1; i >= 0; i--) {
                    result.Append (input[i]);
                }
            }

            return result.ToString ();
        }
    }
}