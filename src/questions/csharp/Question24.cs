using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Gives a string with sequence of words in CamelCase as a string of letters.
    /// Print the number of words in string.
    /// </summary>
    public class Question24 {
        public static void Init (string[] args) {
            Console.WriteLine (WordCount ("hello"));
            Console.WriteLine (WordCount ("helloWorld"));
        }

        private static int WordCount (string str) {
            int counter = 0;

            if (!string.IsNullOrEmpty (str)) {
                counter = 1;
                char[] array = str.ToCharArray ();

                for (int i = 0; i < array.Length; i++) {
                    if ((int) array[i] >= (int)
                        'A' && (int) array[i] <= (int)
                        'Z') {
                        counter++;
                    }
                }
            }

            return counter;
        }
    }
}