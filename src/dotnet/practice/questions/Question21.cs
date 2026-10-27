using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Implement the Perfect Square Root function
    /// </summary>
    public class Question21 {
        public static void Init (string[] args) {
            Console.WriteLine ("-1: " + IsSquareRoot (-1));
            Console.WriteLine ("0: " + IsSquareRoot (0));
            Console.WriteLine ("1: " + IsSquareRoot (1));
            Console.WriteLine ("2: " + IsSquareRoot (2));
            Console.WriteLine ("3: " + IsSquareRoot (3));
            Console.WriteLine ("4: " + IsSquareRoot (4));
            Console.WriteLine ("5: " + IsSquareRoot (5));
        }

        private static int IsSquareRoot (int n) {
            for (int i = 1; i * i <= n; i++) {
                if (i * i == n) {
                    return i;
                }
            }

            return -1;
        }
    }
}