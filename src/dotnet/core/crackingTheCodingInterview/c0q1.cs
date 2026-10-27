/*
Sort an array containing a million integers between 0 and 130 (a reasonable maximum age).
*/

using System;

namespace InterviewPreperationGuide.Core.CrackingTheCodingInterview.c0q1 {
    public class Solution {
        public static void Init (string[] args) {
            DisplaySortedCustomerAge (SortCustomersAge ([40, 23, 11, 56, 98]));
            DisplaySortedCustomerAge (SortCustomersAge ([]));
            DisplaySortedCustomerAge (SortCustomersAge (null));
            DisplaySortedCustomerAge (SortCustomersAge ([40, 0, -10, 10, 63, 10]));
        }

        private static int[] SortCustomersAge (int[] customerAge) {
            int[] result = new int[130];

            if (customerAge != null) {
                for (int i = 0; i < customerAge.Length; i++) {
                    if (customerAge[i] > 0) {
                        result[customerAge[i]]++;
                    }
                }
            }

            return result;
        }

        private static void DisplaySortedCustomerAge (int[] customerAge) {
            if (customerAge != null) {
                for (int i = 0; i < customerAge.Length; i++) {
                    if (customerAge[i] > 0) {
                        for (int j = 0; j < customerAge[i]; j++) {
                            Console.Write (i + " ");
                        }
                    }
                }
            }

            Console.WriteLine (string.Empty);
        }
    }
}