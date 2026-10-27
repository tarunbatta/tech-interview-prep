using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Find first non-repeated word in a sentence
    /// </summary>
    public class Question5 {
        public static void Init (string[] args) {
            Console.WriteLine ("Empty: " + GetFirstNonRepeatedWord (string.Empty));
            Console.WriteLine ("Null: " + GetFirstNonRepeatedWord (null));
            Console.WriteLine ("Space: " + GetFirstNonRepeatedWord (" "));
            Console.WriteLine ("0: " + GetFirstNonRepeatedWord ("0"));
            Console.WriteLine ("This is a test.: " + GetFirstNonRepeatedWord ("This is a test."));
            Console.WriteLine ("Hello Hello World: " + GetFirstNonRepeatedWord ("Hello Hello World"));
            Console.WriteLine ("Hello Hello World!!: " + GetFirstNonRepeatedWord ("Hello Hello World!!"));
        }

        private static string GetFirstNonRepeatedWord (string input) {
            string result = string.Empty;

            if (!string.IsNullOrEmpty (input)) {
                Dictionary<string, int> inputDictionary = new Dictionary<string, int> ();
                StringBuilder word = new StringBuilder ();

                for (int i = 0; i < input.Length; i++) {
                    if (char.IsLetter (input[i])) {
                        word.Append (input[i]);
                    }

                    if (!char.IsLetter (input[i]) || i == (input.Length - 1)) {
                        if (inputDictionary.ContainsKey (word.ToString ())) {
                            inputDictionary[word.ToString ()]++;
                        } else {
                            inputDictionary.Add (word.ToString (), 1);
                        }

                        word.Clear ();
                    }
                }

                foreach (var item in inputDictionary) {
                    if (item.Value == 1) {
                        result = item.Key;
                        break;
                    }
                }
            }

            return result;
        }
    }
}