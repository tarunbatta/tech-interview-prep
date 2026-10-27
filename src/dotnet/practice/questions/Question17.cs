using System;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Write a function to implement Math.Pow(base, exp)
    /// </summary>
    public class Question17 {
        public static void Init (string[] args) {
            Console.WriteLine ("0^0: " + PowerOf_a (0, 0));
            
            Console.WriteLine ("0^-1: " + PowerOf_a (0, -1));
            
            Console.WriteLine ("0^1: " + PowerOf_a (0, 1));
            
            Console.WriteLine ("1^0: " + PowerOf_a (1, 0));
            
            Console.WriteLine ("2^0: " + PowerOf_a (2, 0));
            
            Console.WriteLine ("2^1: " + PowerOf_a (2, 1));
            
            Console.WriteLine ("2^2: " + PowerOf_a (2, 2));
            
            Console.WriteLine ("2^3: " + PowerOf_a (2, 3));
            
            Console.WriteLine ("2^4: " + PowerOf_a (2, 4));
            
            Console.WriteLine ("2^5: " + PowerOf_a (2, 5));

            

            Console.WriteLine ("0^0: " + PowerOf_b (0, 0));
            
            Console.WriteLine ("0^-1: " + PowerOf_b (0, -1));
            
            Console.WriteLine ("0^1: " + PowerOf_b (0, 1));
            
            Console.WriteLine ("1^0: " + PowerOf_b (1, 0));
            
            Console.WriteLine ("2^0: " + PowerOf_b (2, 0));
            
            Console.WriteLine ("2^1: " + PowerOf_b (2, 1));
            
            Console.WriteLine ("2^2: " + PowerOf_b (2, 2));
            
            Console.WriteLine ("2^3: " + PowerOf_b (2, 3));
            
            Console.WriteLine ("2^4: " + PowerOf_b (2, 4));
            
            Console.WriteLine ("2^5: " + PowerOf_b (2, 5));
            
            Console.WriteLine ("2^5: " + PowerOf_b (2, -5));
        }

        private static int PowerOf_a (int input, int power) {
            if (power < 0) {
                return 0;
            } else if (power == 0 || (input == 0 && power == 1)) {
                return 1;
            } else {
                return input * PowerOf_a (input, power - 1);
            }
        }

        private static double PowerOf_b (int input, int power) {
            if (power == 0 || (input == 0 && power == 1)) {
                return 1;
            } else {
                double d = PowerOf_b (input, power / 2);

                if (d % 2 == 0) {
                    d = d * d;
                } else {
                    d = input * d * d;
                }

                if (power < 0) {
                    return 1 / d;
                } else {
                    return d;
                }
            }
        }
    }
}