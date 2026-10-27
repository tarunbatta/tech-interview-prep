using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Write a function to Calculate Fibonacci Number
    /// </summary>
    public class Question16 {
        public static void Init (string[] args) {
            Console.WriteLine ("0: " + FibonacciNumber (0));
            Console.WriteLine ("1: " + FibonacciNumber (1));
            Console.WriteLine ("2: " + FibonacciNumber (2));
            Console.WriteLine ("3: " + FibonacciNumber (3));
            Console.WriteLine ("4: " + FibonacciNumber (4));
            Console.WriteLine ("5: " + FibonacciNumber (5));
            Console.WriteLine ("6: " + FibonacciNumber (6));
            Console.WriteLine ("7: " + FibonacciNumber (7));
        }

        private static int FibonacciNumber (int input) {
            if (input <= 0) {
                return 0;
            } else if (input == 1) {
                return 1;
            } else {
                return FibonacciNumber (input - 1) + FibonacciNumber (input - 2);
            }
        }
    }
}