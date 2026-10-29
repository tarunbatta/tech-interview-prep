/*

*/

using System;

namespace InterviewPreperationGuide.Core.Algorithms.Sorting {
    // Time: O (n^2)
    // Space: O (1)
    public class SelectionSort {
        public static void Init () {
            Display (_SelectionSort (null));
            Display (_SelectionSort ([]));
            Display (_SelectionSort ([1, 7, 5, 23, 9, 34, 12, 100, 2, 6, 0, -5, 25]));
        }

        public static int[] _SelectionSort (int[] arr) {
            if (arr != null && arr.Length > 0) {
                for (int i = 0; i < arr.Length; i++) {
                    int min = i;

                    for (int j = i + 1; j < arr.Length; j++) {
                        if (arr[j] < arr[min]) {
                            min = j;
                        }
                    }

                    int temp = arr[min];
                    arr[min] = arr[i];
                    arr[i] = temp;
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