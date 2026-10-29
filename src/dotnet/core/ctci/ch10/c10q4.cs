/*
Sorted Search, No Size: 

You are given an array-like data structure Listy which lacks a size
method. It does, however, have an elementAt (i) method that returns the element at index i in
0(1) time. If i is beyond the bounds of the data structure, it returns -1. (For this reason, the data
structure only supports positive integers.) Given a Listy which contains sorted, positive integers,
find the index at which an element x occurs. If x occurs multiple times, you may return any index.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c10q4 {
    public class Solution {
        public class Listy {
            private readonly int[] _array;

            public Listy (int[] array) {
                _array = array;
            }

            public int ElementAt (int i) {
                if (i < 0 || i >= _array.Length) return -1;
                return _array[i];
            }
        }

        public static int Search (Listy list, int value) {
            int index = 1;
            while (list.ElementAt (index) != -1 && list.ElementAt (index) < value) {
                index *= 2;
            }
            return BinarySearch (list, value, index / 2, index);
        }

        private static int BinarySearch (Listy list, int value, int low, int high) {
            while (low <= high) {
                int mid = low + (high - low) / 2;
                int middle = list.ElementAt (mid);
                if (middle > value || middle == -1) {
                    high = mid - 1;
                } else if (middle < value) {
                    low = mid + 1;
                } else {
                    return mid;
                }
            }
            return -1;
        }

        public static void Init (string[] args = null) {
            Listy listy = new Listy (new int[] { 1, 2, 5, 8, 24, 35 });
            Console.WriteLine ($"Search 24: {Search (listy, 24)}"); // 4
            Console.WriteLine ($"Search 1: {Search (listy, 1)}");   // 0
            Console.WriteLine ($"Search 35: {Search (listy, 35)}"); // 5
            Console.WriteLine ($"Search 99: {Search (listy, 99)}"); // -1
        }
    }
}
