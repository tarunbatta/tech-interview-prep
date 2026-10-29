/*
Magic Index: 

A magic index in an array A[ 1 ... n - 1] is defined to be an index such that A[ i] =
i. Given a sorted array of distinct integers, write a method to find a magic index, if one exists, in
array A.

FOLLOW UP
What if the values are not distinct?
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c8q3 {
    public class Solution {
        public static int MagicSlow (int[] arr) {
            for (int i = 0; i < arr.Length; i++) {
                if (arr[i] == i) return i;
            }
            return -1;
        }

        public static int MagicFast (int[] arr, int low, int high) {
            if (high < low) return -1;
            int pivot = low + (high - low) / 2;
            if (arr[pivot] == pivot) {
                return pivot;
            } else if (arr[pivot] < pivot) {
                return MagicFast (arr, pivot + 1, high);
            } else {
                return MagicFast (arr, low, pivot - 1);
            }
        }

        public static void Init (string[] args = null) {
            int[] arr1 = { -5, -2, 1, 3, 5, 9 };
            int[] arr2 = { -5, -2, 1, 2, 3, 5 };
            Console.WriteLine ($"Magic slow: {MagicSlow (arr1)}");                      // 3
            Console.WriteLine ($"Magic fast: {MagicFast (arr2, 0, arr2.Length - 1)}"); // 5
        }
    }
}
