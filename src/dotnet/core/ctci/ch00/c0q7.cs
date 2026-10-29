/*
Given an array of distinct integer values, count the number of pairs of integers that have difference k.
For example, given the array {1, 7, 5, 9, 2, 12, 3} and the difference k = 2,
there are four pairs with difference 2: (1, 3), (3, 5), (5, 7), (7, 9)
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c0q7 {
    public class Solution {
        public static void Init (string[] args = null) {
            Console.WriteLine (GetPairs (null, 2));
            Console.WriteLine (GetPairs ([1, 7, 5, 9, 2, 12, 3], 2));
            Console.WriteLine (GetPairs ([1, 7, 5, 9, 2, 12, 3], 0));
        }

        private static string GetPairs (int[] arr, int diff) {
            string result = string.Empty;

            if (arr != null && arr.Length > 0) {
                Array.Sort (arr);

                for (int i = 0; i < arr.Length; i++) {
                    if (Array.IndexOf (arr, arr[i] + diff) >= 0) {
                        result += string.Concat ("{", arr[i], ",", arr[i] + diff, "}");
                    }
                }
            }

            return result;
        }
    }
}