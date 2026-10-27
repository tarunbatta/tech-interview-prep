using System;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Sum +ve and -ve numbers separately
    /// </summary>
    internal class SumOfPositiveNegativeSeperately {
        //public static void Main(string[] args)
        //{
        //    SumArray(new int[] { 1, 2, 3, 4, -1, -2, -3 });
        //}

        public static void SumArray (int[] arr) {
            int p = 0;
            int n = 0;

            for (int i = 0; i < arr.Length; i++) {
                if (arr[i] > 0) {
                    p += arr[i];
                } else {
                    n += arr[i];
                }
            }

            Console.WriteLine (p);
            Console.WriteLine (n);
        }
    }
}