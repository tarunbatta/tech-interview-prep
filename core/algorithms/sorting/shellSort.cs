/*

*/

using System;

namespace InterviewPreperationGuide.Core.Algorithms.Sorting {
    // Time: O ((n log n)^2)
    // Space: O (1)
    public class ShellSort {
        public static void Init () {
            Display (_ShellSort (null));
            Display (_ShellSort ([]));
            Display (_ShellSort ([1, 7, 5, 23, 9, 34, 12, 100, 2, 6, 0, -5, 25]));
        }

        public static int[] _ShellSort (int[] arr) {
            if (arr != null && arr.Length > 0) {
                // Start with a big gap, then reduce the gap
                for (int gap = arr.Length / 2; gap > 0; gap /= 2) {
                    // Do a gapped insertion sort for this gap size. The first gap elements a[0..gap-1] are already in gapped order keep adding one more element until the entire array is gap sorted
                    for (int i = gap; i < arr.Length; i++) {
                        // add a[i] to the elements that have been gap sorted save a[i] in temp and make a hole at position i
                        int temp = arr[i];

                        // shift earlier gap-sorted elements up until the correct location for a[i] is found
                        int j;
                        for (j = i; j >= gap && arr[j - gap] > temp; j -= gap) {
                            arr[j] = arr[j - gap];
                        }

                        // put temp (the original a[i]) in its correct location
                        arr[j] = temp;
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