using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Implement a function void reverse(char* str) which reverses a null terminated string.
    /// </summary>
    public class Question25 {
        public static void Init (string[] args) {
            Console.WriteLine ("Empty: " + Reverse (""));
            Console.WriteLine ("Null: " + Reverse (null));
            Console.WriteLine ("a: " + Reverse ("a"));
            Console.WriteLine ("ab: " + Reverse ("ab"));
            Console.WriteLine ("abc: " + Reverse ("abc"));
            Console.WriteLine ("abcd: " + Reverse ("abcd"));
            Console.WriteLine ("abcde: " + Reverse ("abcde"));
        }

        private static string Reverse (string input) {
            string result = string.Empty;

            if (!string.IsNullOrEmpty (input)) {
                int i = 0;
                char temp;

                char[] charactersInString = input.ToCharArray ();

                while (i < input.Length / 2) {
                    temp = charactersInString[i];
                    charactersInString[i] = charactersInString[(input.Length - 1) - i];
                    charactersInString[(input.Length - 1) - i] = temp;

                    i++;
                }

                result = new string (charactersInString);
            }

            return result;
        }
    }
}