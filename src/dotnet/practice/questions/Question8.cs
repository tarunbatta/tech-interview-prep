using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Write a function to reverse the order of words in a sentence
    /// </summary>
    public class Question8 {
        public static void Init (string[] args) {
            Console.WriteLine ("Empty: " + ReverseOrderOfWords (string.Empty));
            Console.WriteLine ("Null: " + ReverseOrderOfWords (null));
            Console.WriteLine ("Space: " + ReverseOrderOfWords (" "));
            Console.WriteLine ("0: " + ReverseOrderOfWords ("0"));
            Console.WriteLine ("This is a test.: " + ReverseOrderOfWords ("This is a test."));
            Console.WriteLine ("When you reach heaven, give me a call.: " + ReverseOrderOfWords ("When you reach heaven, give me a call."));
        }

        private static string ReverseOrderOfWords (string input) {
            StringBuilder result = new StringBuilder ();

            if (!string.IsNullOrEmpty (input)) {
                StringBuilder buffer = new StringBuilder ();
                List<string> words = new List<string> ();

                for (int i = 0; i < input.Length; i++) {
                    if (input[i] != ' ') {
                        buffer.Append (input[i]);
                    } else {
                        words.Add (buffer.ToString ());
                        words.Add (" ");
                        buffer.Clear ();
                    }

                    if (i == (input.Length - 1)) {
                        words.Add (buffer.ToString ());
                        buffer.Clear ();
                    }
                }

                for (int i = (words.Count - 1); i >= 0; i--) {
                    result.Append (words[i]);
                }
            }

            return result.ToString ();
        }
    }
}