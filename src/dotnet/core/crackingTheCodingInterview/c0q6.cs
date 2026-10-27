/*
Print all positive integer solutions to the equation a3 + b3 = c3 + d3
where a, b, c and d are integers between 1 and 1000
*/

using System;

namespace InterviewPreperationGuide.Core.CrackingTheCodingInterview.c0q6 {
    public class Solution {
        public static void Init (string[] args) {
            Console.WriteLine (GetPositiveIntegersInEquation (1000));
        }

        private static string GetPositiveIntegersInEquation (int limit) {
            string result = string.Empty;

            for (int i = 1; i <= limit; i++) {
                for (int j = 1; j <= limit; j++) {
                    double calc = Math.Pow (i, 3) + Math.Pow (j, 3);
                    result += string.Concat ("{", i, ",", j, "}");
                }
            }

            return result;
        }
    }
}