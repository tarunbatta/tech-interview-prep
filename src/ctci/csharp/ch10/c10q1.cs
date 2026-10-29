/*
Sorted Merge: 

You are given two sorted arrays, A and B, where A has a large enough buffer at the
end to hold B. Write a method to merge B into A in sorted order.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c10q1 {
    public class Solution {
        public static void Merge (int[] a, int[] b, int lastA, int lastB) {
            int indexA = lastA - 1;
            int indexB = lastB - 1;
            int indexMerged = lastA + lastB - 1;

            while (indexB >= 0) {
                if (indexA >= 0 && a[indexA] > b[indexB]) {
                    a[indexMerged] = a[indexA];
                    indexA--;
                } else {
                    a[indexMerged] = b[indexB];
                    indexB--;
                }
                indexMerged--;
            }
        }

        public static void Init (string[] args = null) {
            int[] a = new int[] { 1, 4, 7, 8, 9, 15, 0, 0, 0, 0, 0, 0 };
            int[] b = new int[] { 2, 3, 5, 11, 12, 16 };
            Merge (a, b, 6, 6);
            Console.WriteLine ($"Merged: {string.Join (", ", a)}");
        }
    }
}
