/*

*/

using System;

namespace InterviewPreperationGuide.Core.Algorithms.Sorting {
    // Time: O (n^2)
    // Space: O (1)
    public class InsertionSort {
        public static void Init () {
            Display (_InsertionSort (null));
            Display (_InsertionSort ([]));
            Display (_InsertionSort ([1, 7, 5, 23, 9, 34, 12, 100, 2, 6, 0, -5, 25]));
        }

        public static int[] _InsertionSort (int[] arr) {
            if (arr != null && arr.Length > 0) {
                for (int i = 1; i < arr.Length; i++) {
                    int j = i;
                    int temp = arr[j];

                    while (j > 0 && arr[j - 1] >= temp) {
                        arr[j] = arr[j - 1];
                        j--;
                    }

                    arr[j] = temp;
                }
            }

            return arr;
        }

        public static void Display (int[] arr) {
            if (arr != null) {
                for (int x = 0; x < arr.Length; x++) {
                    Console.Write (arr[x] + " ");
                }
            }

            Console.WriteLine ();
        }
    }
}