/*

*/

using System;

namespace InterviewPreperationGuide.Core.Algorithms.Sorting {
    // Time: O (n^2)
    // Space: O (1)
    public class BubbleSort {
        public static void Init () {
            Display (_BubbleSort (null));
            Display (_BubbleSort ([]));
            Display (_BubbleSort ([1, 7, 5, 23, 9, 34, 12, 100, 2, 6, 0, -5, 25]));
        }

        public static int[] _BubbleSort (int[] arr) {
            if (arr != null && arr.Length > 0) {
                for (int i = arr.Length - 1; i >= 0; i--) {
                    for (int j = 0; j < i; j++) {
                        if (arr[i] < arr[j]) {
                            int temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
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