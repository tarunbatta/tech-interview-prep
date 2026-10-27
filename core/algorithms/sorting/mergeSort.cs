/*

*/

using System;

namespace InterviewPreperationGuide.Core.Algorithms.Sorting {
    // Time: O (n log n)
    // Space: O (n)
    public class MergeSort {
        public static void Init () {
            _MergeSort (null);
            _MergeSort ([]);
            _MergeSort ([1, 7, 5, 23, 9, 34, 12, 100, 2, 6, 0, -5, 25]);
        }

        public static void _MergeSort (int[] arr) {
            if (arr != null && arr.Length > 0) {
                Sort (arr, 0, arr.Length - 1);
            }

            Display (arr);
        }

        public static void Sort (int[] arr, int low, int high) {
            if (low < high) {
                int mid = (int) (low + high) / 2;

                Sort (arr, low, mid);
                Sort (arr, mid + 1, high);
                Merge (arr, low, mid, high);
            }
        }

        public static void Merge (int[] arr, int low, int mid, int high) {
            // Find sizes of two subarrays to be merged
            int n1 = mid - low + 1;
            int n2 = high - mid;

            /* Create temp arrays */
            int[] L = new int[n1];
            int[] R = new int[n2];

            /*Copy data to temp arrays*/
            for (int x = 0; x < n1; x++) {
                L[x] = arr[low + x];
            }

            for (int y = 0; y < n2; y++) {
                R[y] = arr[mid + 1 + y];
            }

            /* Merge the temp arrays */

            // Initial indexes of first and second subarrays
            int i = 0, j = 0;

            // Initial index of merged subarry array
            int k = low;
            while (i < n1 && j < n2) {
                if (L[i] <= R[j]) {
                    arr[k] = L[i];
                    i++;
                } else {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            /* Copy remaining elements of L[] if any */
            while (i < n1) {
                arr[k] = L[i];
                i++;
                k++;
            }

            /* Copy remaining elements of R[] if any */
            while (j < n2) {
                arr[k] = R[j];
                j++;
                k++;
            }
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