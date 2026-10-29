/*

*/

using System;

namespace InterviewPreperationGuide.Core.Algorithms.Sorting {
    // Time: O (n log n)
    // Space: O (log n)
    public class QuickSort {
        public void Init () {
            _QuickSort (null);
            _QuickSort ([]);
            _QuickSort ([1, 7, 5, 23, 9, 34, 12, 100, 2, 6, 0, -5, 25]);
        }

        public void _QuickSort (int[] arr) {
            if (arr != null && arr.Length > 0) {
                Sort(arr, 0, arr.Length - 1);
            }

            Display(arr);
        }

        public static void Sort (int[] arr, int low, int high) {
            if (low < high) {
                //pi is partitioning index, arr[pi] is now at right place
                int pi = Partition(arr, low, high);

                // Recursively sort elements before partition and after partition
                Sort(arr, low, pi - 1);
                Sort(arr, pi + 1, high);
            }
        }

        public static int Partition (int[] arr, int low, int high) {
            int pivot = arr[high];
            int i = (low - 1); // index of smaller element

            for (int j = low; j < high; j++) {
                // If current element is smaller than or equal to pivot
                if (arr[j] <= pivot) {
                    i++;

                    // swap arr[i] and arr[j]
                    Swap(arr, i, j);
                }
            }

            // swap arr[i+1] and arr[high] (or pivot)
            Swap(arr, i + 1, high);

            return i + 1;
        }

        private static void Swap (int[] arr, int i, int j) {
            if (i < j && i >= 0 && j < arr.Length) {
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
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