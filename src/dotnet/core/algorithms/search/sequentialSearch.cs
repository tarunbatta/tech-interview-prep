/*

*/

using System;

namespace InterviewPreperationGuide.Core.Algorithms.Search {
    public class SequentialSearch {
        public static void Init () {
            Console.WriteLine (_SequentialSearch (9, null));
            Console.WriteLine (_SequentialSearch (9, []));
            Console.WriteLine (_SequentialSearch (9, [1, 7, 5, 23, 9, 34, 12, 100, 2, 6, 0, -5, 25]));

            Console.WriteLine (_SelfOrganizingSequentialSearch (9, null));
            Console.WriteLine (_SelfOrganizingSequentialSearch (9, []));
            Console.WriteLine (_SelfOrganizingSequentialSearch (9, [1, 7, 5, 23, 9, 34, 12, 100, 2, 6, 0, -5, 25]));
        }

        public static int _SequentialSearch (int value, int[] arr) {
            if (arr != null && arr.Length > 0) {
                for (int i = arr.Length - 1; i >= 0; i--) {
                    if (arr[i] == value) {
                        return i;
                    }
                }
            }

            return -1;
        }

        public static int _SelfOrganizingSequentialSearch (int value, int[] arr) {
            if (arr != null && arr.Length > 0) {
                for (int i = arr.Length - 1; i >= 0; i--) {
                    if (arr[i] == value) {
                        Swap (ref arr, i, i - 1);
                        return i;
                    }
                }
            }

            return -1;
        }

        public static void Swap (ref int[] arr, int a, int b) {
            if (arr != null && arr.Length > 0 && a > -1 && b > -1 && a < arr.Length && b < arr.Length) {
                int temp = arr[a];
                arr[a] = arr[b];
                arr[b] = temp;
            }
        }
    }
}