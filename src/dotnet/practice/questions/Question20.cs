using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Write a function to implement product operator
    /// </summary>
    public class Question20 {
        public static void Init (string[] args) {
            Console.WriteLine (Product (2, 0));
            Console.WriteLine (Product (20, 2));
            Console.WriteLine (Product (21, 5));
            Console.WriteLine (Product (0, 7));
        }

        private static int Product (int a, int b) {
            int result = 0;

            if (a != 0 && b != 0) {
                for (int i = 0; i < b; i++) {
                    result += a;
                }
            }

            return result;
        }
    }
}