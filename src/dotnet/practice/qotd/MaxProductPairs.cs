using System;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Given an array, find the pairs that gives max product
    /// </summary>
    internal class MaxProductPairs {
        //public static void Main(string[] args)
        //{
        //    Console.WriteLine(FindMaxProduct(new int[] {7}));
        //    Console.WriteLine(FindMaxProduct(new int[] {7, 1, 9, 8}));
        //    Console.WriteLine(FindMaxProduct(new int[] {7, 1, 9, 9, 1}));
        //    Console.WriteLine(FindMaxProduct(new int[] {-9, -8, 0, 4, 5}));
        //}

        public static int FindMaxProduct (int[] arr) {
            int result = 0;

            if (arr != null && arr.Length > 0) {
                if (arr.Length == 1) {
                    result = arr[0];
                } else {
                    Array.Sort (arr);
                    int a = arr[0] * arr[1];
                    int b = arr[arr.Length - 1] * arr[arr.Length - 2];

                    result = a > b ? a : b;
                }
            }

            return result;
        }
    }
}