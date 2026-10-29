using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Implement a function which return sum of digits from an input number
    /// </summary>
    public class Question22 {
        public static void Init (string[] args) {
            Console.WriteLine ("000: " + SumOfDigits (000));
            Console.WriteLine ("100: " + SumOfDigits (100));
            Console.WriteLine ("160: " + SumOfDigits (160));
            Console.WriteLine ("518: " + SumOfDigits (518));
            Console.WriteLine ("999: " + SumOfDigits (999));
        }

        private static int SumOfDigits (int n) {
            int result = 0;

            while (n > 0) {
                result += n % 10;
                n = n / 10;
            }

            return result;
        }
    }
}