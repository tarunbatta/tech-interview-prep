using System;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Find all numbers from 1 to 1000, where a<b<c and a^2 + b^2 = c^2
    /// </summary>
    internal class SolveEquation {
        //public static void Main(string[] args)
        //{
        //    FindNumbersOfEquation();
        //}

        public static void FindNumbersOfEquation () {
            for (int i = 0; i <= 1000; i++) {
                for (int j = i + 1; j <= 1000; j++) {
                    int c = i * i + j * j;
                    int k = (int) Math.Sqrt (c);

                    if ((k * k == c) && k <= 1000 && k > j) {
                        Console.WriteLine ("(" + i + ", " + j + ", " + k + ")");
                    }
                }
            }
        }
    }
}