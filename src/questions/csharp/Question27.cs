using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Write a program that prints the numbers from 1 to 100. But for multiples of three print “Fizz”
    /// instead of the number and for the multiples of five print “Buzz”. For numbers which are multiples
    /// of both three and five print “FizzBuzz”.
    /// </summary>
    public class Question27 {
        public static void Init (string[] args) {
            FizzBuzz ();
        }

        private static void FizzBuzz () {
            for (int i = 1; i <= 100; i++) {
                string result = string.Empty;

                if (i % 3 == 0) {
                    result = "Fizz";
                }

                if (i % 5 == 0) {
                    result += "Buzz";
                }

                if (string.IsNullOrEmpty (result)) {
                    Console.WriteLine (i);
                } else {
                    Console.WriteLine (result);
                }
            }
        }
    }
}