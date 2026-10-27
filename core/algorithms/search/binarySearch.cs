/*

*/

using System;

namespace InterviewPreperationGuide.Core.Algorithms.Search {
    public class BinarySearch {
        public static void Init () {
            Console.WriteLine (_BinarySearch (9, null));
            Console.WriteLine (_BinarySearch (9, []));
            Console.WriteLine (_BinarySearch (9, [1, 3, 5, 7, 9, 13, 15, 17, 21]));

            int[] arr = [1, 3, 5, 7, 9, 13, 15, 17, 21];
            Console.WriteLine (_RBinarySearch (9, arr, 0, arr.Length - 1));
        }

        public static int _BinarySearch (int value, int[] arr) {
            if (arr != null && arr.Length > 0) {
                int start = 0;
                int end = arr.Length - 1;

                while (start <= end) {
                    int mid = (int) ((start + end) / 2);

                    if (arr[mid] == value) {
                        return mid;
                    } else if (arr[mid] > value) {
                        end = mid - 1;
                    } else {
                        start = mid + 1;
                    }
                }
            }

            return -1;
        }

        public static int _RBinarySearch (int value, int[] arr, int start, int end) {
            if (start > end) {
                return -1;
            } else {
                int mid = (int) ((start + end) / 2);

                if (arr[mid] == value) {
                    return mid;
                } else if (arr[mid] > value) {
                    _RBinarySearch (value, arr, start, mid - 1);
                } else {
                    _RBinarySearch (value, arr, mid + 1, end);
                }
            }

            return -1;
        }
    }
}