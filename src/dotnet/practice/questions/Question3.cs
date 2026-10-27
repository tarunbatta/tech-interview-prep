using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Write a function for Int32 Parse
    /// </summary>
    public class Question3 {
        public static void Init (string[] args) {
            Console.WriteLine ("Empty: " + ParseInt32 (string.Empty));
            Console.WriteLine ("Null: " + ParseInt32 (null));
            Console.WriteLine ("Space: " + ParseInt32 (" "));
            Console.WriteLine ("0: " + ParseInt32 ("0"));
            Console.WriteLine ("123: " + ParseInt32 ("123"));
            Console.WriteLine ("-45: " + ParseInt32 ("-45"));
            Console.WriteLine ("7b: " + ParseInt32 ("7b"));
            Console.WriteLine ("abc: " + ParseInt32 ("abc"));
            Console.WriteLine ("!@#: " + ParseInt32 ("!@#"));
            Console.WriteLine (Int32.MinValue.ToString () + ": " + ParseInt32 (Int32.MinValue.ToString ()));
            Console.WriteLine (Int32.MaxValue.ToString () + ": " + ParseInt32 (Int32.MaxValue.ToString ()));
            Console.WriteLine ("2147483648: " + ParseInt32 ("2147483648"));
            Console.WriteLine ("-2147483649: " + ParseInt32 ("-2147483649"));
        }

        /// <summary>
        /// Parses the string to Integer.
        /// This function doesn't cater overflow and underflow conditions.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static int ParseInt32 (string input) {
            int result = 0;

            if (!string.IsNullOrEmpty (input)) {
                bool isNegative = false;
                int counter = 0;

                if (input.Length > 0) {
                    if (input[0] == '-') {
                        isNegative = true;
                        counter = 1;
                    }

                    while (counter < input.Length) {
                        int number = input[counter] - '0';

                        if (number >= 0 && number <= 9) {
                            unchecked {
                                result = result * 10;
                                result = result + number;
                            }
                        } else {
                            result = 0;
                            break;
                        }

                        counter++;
                    }

                    if (isNegative) {
                        result = result * -1;
                    }
                }
            }

            return result;
        }
    }
}