/*
Write code to swap the minimum and maximum element in an integer array.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c0q9 {
    public class Solution {
        public static void Init (string[] args = null) {
            Console.WriteLine (SwapMinMax ([4, 9, 5, 2, 3, 1, 7]));
        }

        private static string SwapMinMax (int[] arr) {
            int min = GetMinIndex (arr);
            int max = GetMaxIndex (arr);
            var newArray = Swap (arr, min, max);
            return string.Join (",", newArray);
        }

        private static int[] Swap (int[] arr, int min, int max) {
            int[] newArray = arr;
            int temp = 0;

            temp = newArray[min];
            newArray[min] = newArray[max];
            newArray[max] = temp;

            return newArray;
        }

        private static int GetMinIndex (int[] arr) {
            int result = -1;

            if (arr != null && arr.Length > 0) {
                int min = arr[0];
                result = 0;

                for (int i = 1; i < arr.Length; i++) {
                    if (arr[i] <= min) {
                        min = arr[i];
                        result = i;
                    }
                }
            }

            return result;
        }

        private static int GetMaxIndex (int[] arr) {
            int result = -1;

            if (arr != null && arr.Length > 0) {
                int max = arr[0];
                result = 0;

                for (int i = 1; i < arr.Length; i++) {
                    if (arr[i] >= max) {
                        max = arr[i];
                        result = i;
                    }
                }
            }

            return result;
        }
    }
}