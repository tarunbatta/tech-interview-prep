using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Write a function to implement modulus operator
    /// </summary>
    public class Question18 {
        public static void Init (string[] args) {
            Console.WriteLine (Modulus (2, 0));
            Console.WriteLine (Modulus (20, 2));
            Console.WriteLine (Modulus (21, 5));
            Console.WriteLine (Modulus (0, 7));
        }

        private static int Modulus (int a, int b) {
            if (b <= 0) {
                return -1;
            }

            int division = a / b;

            return a - (b * division);
        }
    }
}