/*
A sorted array has been rotated so that the elements might appear in the order 3
456712. How would you find the minimum element? You may assume that the array has
all unique elements.
*/

using System;

namespace InterviewPreperationGuide.Core.CrackingTheCodingInterview.c0q3 {
    public class Solution {
        public static void Init (string[] args) {
            Console.WriteLine (GetMinimumInSortedArray ([3, 4, 5, 6, 1, 2]));
            Console.WriteLine (GetMinimumInSortedArray ([9, 12, 14, 4, 6, 7, 8]));
        }

        private static int GetMinimumInSortedArray (int[] arr) {
            int result = 0;

            if (arr != null && arr.Length > 0) {
                result = RecursiveCheck (arr, 0, arr.Length - 1);
            }

            return result;
        }

        private static int RecursiveCheck (int[] arr, int firstIndex, int lastIndex) {
            int result = 0;
            int midIndex = (firstIndex + lastIndex + 1) / 2;

            if (arr[midIndex] > arr[lastIndex]) {
                result = RecursiveCheck (arr, midIndex, lastIndex);
            } else if (arr[midIndex] < arr[lastIndex]) {
                result = RecursiveCheck (arr, firstIndex, midIndex);
            } else {
                result = arr[midIndex];
            }

            return result;
        }
    }
}