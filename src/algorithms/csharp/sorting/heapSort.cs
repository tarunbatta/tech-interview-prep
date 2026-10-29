/*

*/

using System;

namespace InterviewPreperationGuide.Core.Algorithms.Sorting {
    // Time: O (n log n)
    // Space: O (1)
    public class HeapSort {
        public static void Init () {
            Display (_HeapSort (null));
            Display (_HeapSort ([]));
            Display (_HeapSort ([1, 7, 5, 23, 9, 34, 12, 100, 2, 6, 0, -5, 25]));
        }

        public static int[] _HeapSort (int[] arr) {
            if (arr != null && arr.Length > 0) {
                int n = arr.Length;

                // Build heap (rearrange array)
                for (int i = n / 2 - 1; i >= 0; i--) {
                    Heapify (arr, n, i);
                }

                // One by one extract an element from heap
                for (int i = n - 1; i >= 0; i--) {
                    // Move current root to end
                    int temp = arr[0];
                    arr[0] = arr[i];
                    arr[i] = temp;

                    // call max heapify on the reduced heap
                    Heapify (arr, i, 0);
                }
            }

            return arr;
        }

        // To heapify a subtree rooted with node i which is an index in arr[]. n is size of heap
        public static void Heapify (int[] arr, int n, int i) {
            int largest = i; // Initialize largest as root
            int l = 2 * i + 1; // left = 2*i + 1
            int r = 2 * i + 2; // right = 2*i + 2

            // If left child is larger than root
            if (l < n && arr[l] > arr[largest]) {
                largest = l;
            }

            // If right child is larger than largest so far
            if (r < n && arr[r] > arr[largest]) {
                largest = r;
            }

            // If largest is not root
            if (largest != i) {
                int swap = arr[i];
                arr[i] = arr[largest];
                arr[largest] = swap;

                // Recursively heapify the affected sub-tree
                Heapify (arr, n, largest);
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