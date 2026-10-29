using System;
using System.Text;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Convert Integer to String
    /// </summary>
    public class Question2 {
        public static void Init (string[] args) {
            Console.WriteLine ("0: " + ConvertIntToString (0));
            Console.WriteLine ("-1: " + ConvertIntToString (-1));
            Console.WriteLine ("1: " + ConvertIntToString (1));
            Console.WriteLine ("123: " + ConvertIntToString (123));
            Console.WriteLine (Int32.MinValue + ": " + ConvertIntToString (Int32.MinValue));
            Console.WriteLine (Int32.MaxValue + ": " + ConvertIntToString (Int32.MaxValue));
        }

        private static string ConvertIntToString (int input) {
            StringBuilder result = new StringBuilder ();
            StringBuilder tempResult = new StringBuilder ();
            bool isNegative = false;

            if (input == 0) {
                result.Append ("0");
            } else {
                if (input < 0) {
                    input = -input;
                    isNegative = true;
                }

                while (input != 0) {
                    char character = (char) ((input % 10) + '0');
                    tempResult.Append (character);
                    input = input / 10;
                }

                for (int i = (tempResult.Length - 1); i >= 0; i--) {
                    if (isNegative && i == (tempResult.Length - 1)) {
                        result.Append ("-");
                    }

                    result.Append (tempResult[i]);
                }
            }

            return result.ToString ();
        }
    }
}