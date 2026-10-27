using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Find the first non-repeated character in an input string
    /// </summary>
    public class Question4 {
        public static void Init (string[] args) {
            Console.WriteLine ("Empty: " + GetFirstNonRepeatedCharacter (string.Empty));
            Console.WriteLine ("Null: " + GetFirstNonRepeatedCharacter (null));
            Console.WriteLine ("Space: " + GetFirstNonRepeatedCharacter (" "));
            Console.WriteLine ("0: " + GetFirstNonRepeatedCharacter ("0"));
            Console.WriteLine ("abcd: " + GetFirstNonRepeatedCharacter ("abcd"));
            Console.WriteLine ("aabbccccdeeeee: " + GetFirstNonRepeatedCharacter ("aabbccccdeeeee"));
        }

        private static char GetFirstNonRepeatedCharacter (string input) {
            char result = char.MinValue;

            if (!string.IsNullOrEmpty (input)) {
                Dictionary<char, int> inputDictionary = new Dictionary<char, int> ();

                for (int i = 0; i < input.Length; i++) {
                    if (inputDictionary.ContainsKey (input[i])) {
                        inputDictionary[input[i]]++;
                    } else {
                        inputDictionary.Add (input[i], 1);
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