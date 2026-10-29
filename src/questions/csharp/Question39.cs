using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Given a string and a dictionary. Get the number of words present in the string.
    /// </summary>
    public class Question39 {
        public static void Init (string[] args) {
            GetWordCount (null, "i like mango");
            
            GetWordCount (new string[] { "i", "like", "sam", "sung", "samsung", "mobile", "ice", "cream", "icecream", "man", "go", "mango" }, null);
            
            GetWordCount (new string[] { "i", "like", "sam", "sung", "samsung", "mobile", "ice", "cream", "icecream", "man", "go", "mango" }, "i like mango");
        }

        private static int GetWordCount (string[] dict, string str) {
            int result = 0;

            if (dict != null && dict.Length > 0 && !string.IsNullOrEmpty (str)) {
                Dictionary<string, bool> hash = new Dictionary<string, bool> ();

                for (int i = 0; i < dict.Length; i++) {
                    if (!hash.ContainsKey (dict[i])) {
                        hash.Add (dict[i], true);
                    }
                }

                int start = 0;
                int end = 0;
                int counter = 0;

                while (counter < str.Length) {
                    if ((counter + 1 < str.Length && str[counter + 1] == ' ') || counter == str.Length - 1) {
                        end = counter;

                        string word = str.Substring (start, end - start + 1);

                        if (hash.ContainsKey (word)) {
                            result++;
                        }

                        start = counter + 2;
                    } else {
                        end++;
                    }

                    counter++;
                }
            }

            Console.WriteLine (result + " words can be found in sentence '" + str + "' from the dictionary '" + ((dict == null) ? "" : string.Join (",", dict)) + "'");

            return result;
        }
    }
}