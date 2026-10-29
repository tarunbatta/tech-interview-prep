using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Ju Steven’s Counting
    ///
    /// Ju Steven, is an eccentric individual. He doesnt like odd numbers. The way he counts is 0,2,4,6,8,20,22,24,26 .. and so on.
    /// Write a function that convert from normal number system to Ju Steven’s system and vice versa.
    ///
    /// normal 0 = Ju Steven’s 0
    /// normal 1 = Ju Steven’s 2
    /// normal 2 = Ju Steven’s 4
    /// </summary>
    public class JuStevenCounting {
        public static void Init () {
            Console.WriteLine (ConvertDecimalToJuStevenNumber (0));
            Console.WriteLine (ConvertDecimalToJuStevenNumber (1));
            Console.WriteLine (ConvertDecimalToJuStevenNumber (2));
            Console.WriteLine (ConvertDecimalToJuStevenNumber (99));
            Console.WriteLine (ConvertDecimalToJuStevenNumber (15));

            Console.WriteLine (ConvertJuStevenNumberToDecimal (0));
            Console.WriteLine (ConvertJuStevenNumberToDecimal (2));
            Console.WriteLine (ConvertJuStevenNumberToDecimal (4));
            Console.WriteLine (ConvertJuStevenNumberToDecimal (688));
            Console.WriteLine (ConvertJuStevenNumberToDecimal (60));
        }

        public static int ConvertDecimalToJuStevenNumber (int num) {
            int result = 0;

            if (num > 0) {
                List<int> invalids = new List<int> () { 1, 3, 5, 7, 9 };

                for (int i = 0; i < num; i++) {
                    result++;

                    while (ContainsInvalidNumber (result, invalids)) {
                        result++;
                    }
                }
            }

            return result;
        }

        public static int ConvertJuStevenNumberToDecimal (int num) {
            int result = 0;

            if (num > 0) {
                List<int> invalids = new List<int> () { 1, 3, 5, 7, 9 };

                while (num >= 1) {
                    num--;
                    result++;

                    while (ContainsInvalidNumber (num, invalids)) {
                        num--;
                    }
                }
            }

            return result;
        }

        private static bool ContainsInvalidNumber (int num, List<int> invalids) {
            while (num >= 1) {
                if (invalids.Contains (num % 10)) {
                    return true;
                } else {
                    num = num / 10;
                }
            }

            return false;
        }
    }
}