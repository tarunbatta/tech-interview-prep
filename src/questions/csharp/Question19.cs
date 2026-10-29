using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Write a function to implement division operator
    /// </summary>
    public class Question19 {
        public static void Init (string[] args) {
            Console.WriteLine (Division (2, 0));
            Console.WriteLine (Division (20, 2));
            Console.WriteLine (Division (21, 5));
            Console.WriteLine (Division (0, 7));
        }

        private static int Division (int a, int b) {
            int result = 0;

            if (b <= 0) {
                result = -1;
            } else {
                int sum = b;

                while (sum <= a) {
                    sum += b;
                    result++;
                }
            }

            return result;
        }
    }
}