using System;
using System.Collections;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Minimum swaps to make two arrays identical
    /// Given two arrays which have same values but in different order, we need to make second array same as first array using minimum number of swaps.
    /// Examples:
    /// Input  : arrA[] = {3, 6, 4, 8},
    /// arrB[] = {4, 6, 8, 3}
    /// Output : 2
    /// we can make arrB to same as arrA in 2 swaps
    /// which are shown below,
    /// swap 4 with 8,   arrB = {8, 6, 4, 3}
    /// swap 8 with 3,   arrB = {3, 6, 4, 8}
    /// </summary>
    public class MinArraySwap {
        public static void Init (string[] args) {
            Console.WriteLine ("Minimum Sawps: " + GetMinArraySwap (new int[] { 3, 6, 4, 8 }, new int[] { 4, 6, 8, 3 }));
        }

        private static void PrintArray (int[] arr) {
            if (arr != null && arr.Length > 0) {
                for (int i = 0; i < arr.Length; i++) {
                    Console.Write (arr[i] + " ");
                }

                Console.WriteLine ();
            }
        }

        private static int GetMinArraySwap (int[] arrA, int[] arrB) {
            PrintArray (arrA);
            PrintArray (arrB);

            int result = 0;
            Hashtable lookup = new Hashtable ();

            for (int i = 0; i < arrA.Length; i++) {
                if (arrA[i] != arrB[i]) {
                    lookup[arrB[i]] = i;
                }
            }

            for (int i = 0; i < arrA.Length; i++) {
                if (arrA[i] != arrB[i]) {
                    int index = (int) lookup[arrA[i]];

                    int temp = arrB[i];
                    arrB[i] = arrB[index];
                    arrB[index] = temp;

                    lookup[arrB[index]] = index;

                    result++;
                }
            }

            return result;
        }
    }
}