using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Find Lucky Number for a given number N, such that the sum of its digits recursively are calculated
    /// until its a single digit integer.
    /// </summary>
    public class Question30 {
        public static void Init (string[] args) {
            Console.WriteLine (LuckyNumber (0));
            Console.WriteLine (LuckyNumber (10));
            Console.WriteLine (LuckyNumber (4));
            Console.WriteLine (LuckyNumber (12345));
            Console.WriteLine (LuckyNumber (7543));
        }

        private static int LuckyNumber (int n) {
            if (n < 10) {
                return n;
            } else {
                return LuckyNumber (SumOfDigits (n));
            }
        }

        private static int SumOfDigits (int n) {
            if ((n / 10) == 0) {
                return n;
            } else {
                return (n % 10) + SumOfDigits (n / 10);
            }
        }
    }
}