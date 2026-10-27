using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Find the factorial of a number
    /// </summary>
    public class Question1 {
        public static void Init (string[] args) {
            Console.WriteLine ("Implementation by for loop:");
            
            Console.WriteLine ("-1: " + Factorial_a (-1));
            
            Console.WriteLine ("0: " + Factorial_a (0));
            
            Console.WriteLine ("1: " + Factorial_a (1));
            
            Console.WriteLine ("2: " + Factorial_a (2));
            
            Console.WriteLine ("3: " + Factorial_a (3));
            
            Console.WriteLine ("4: " + Factorial_a (4));
            
            Console.WriteLine ("5: " + Factorial_a (5));
            
            Console.WriteLine ("6: " + Factorial_a (6));
            
            Console.WriteLine ("7: " + Factorial_a (7));
            
            Console.WriteLine ("8: " + Factorial_a (8));
            
            Console.WriteLine ("9: " + Factorial_a (9));
            
            Console.WriteLine ("10: " + Factorial_a (10));

            

            Console.WriteLine ("Implementation by recursion:");
            
            Console.WriteLine ("-1: " + Factorial_b (-1));
            
            Console.WriteLine ("0: " + Factorial_b (0));
            
            Console.WriteLine ("1: " + Factorial_b (1));
            
            Console.WriteLine ("2: " + Factorial_b (2));
            
            Console.WriteLine ("3: " + Factorial_b (3));
            
            Console.WriteLine ("4: " + Factorial_b (4));
            
            Console.WriteLine ("5: " + Factorial_b (5));
            
            Console.WriteLine ("6: " + Factorial_b (6));
            
            Console.WriteLine ("7: " + Factorial_b (7));
            
            Console.WriteLine ("8: " + Factorial_b (8));
            
            Console.WriteLine ("9: " + Factorial_b (9));
            
            Console.WriteLine ("10: " + Factorial_b (10));
        }

        private static int Factorial_a (int input) {
            int result = 1;

            for (int i = input; i > 0; i--) {
                result = result * i;
            }

            return result;
        }

        private static int Factorial_b (int input) {
            if (input < 0) {
                return -1;
            } else if (input == 0) {
                return 1;
            } else {
                return input * Factorial_b (input - 1);
            }
        }
    }
}