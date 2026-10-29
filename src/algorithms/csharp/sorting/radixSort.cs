/*

*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.Algorithms.Sorting {
    // Time: O (nk)
    // Space: O (n+k)
    public class RadixSort {
        public enum DigitType {
            ones = 1,
            tens = 10
        }

        public static void Init () {
            int[] n = [91, 46, 85, 15, 92, 35, 31, 22];
            Queue[] q = new Queue[10];
            for (int i = 0; i < 10; i++) {
                q[i] = new Queue ();
            }

            _RadixSort (q, n, DigitType.ones);
            _RadixSort (q, n, DigitType.tens);
        }

        public static void _RadixSort (Queue[] q, int[] n, DigitType digit) {
            Display (n);

            for (int x = 0; x <= n.GetUpperBound (0); x++) {
                int snum;

                if (digit == DigitType.ones) {
                    snum = n[x] % 10;
                } else {
                    snum = n[x] / 10;
                }

                q[snum].Enqueue (n[x]);
            }

            Build (q, n);
            Display (n);
        }

        public static void Build (Queue[] q, int[] n) {
            int y = 0;

            for (int x = 0; x <= 9; x++) {
                while (q[x].Count > 0) {
                    n[y] = Int32.Parse (q[x].Dequeue ().ToString ());
                    y++;
                }
            }
        }

        public static void Display (int[] arr) {
            if (arr != null) {
                for (int x = 0; x < arr.Length; x++) {
                    Console.Write (arr[x] + " ");
                }
            }

            Console.WriteLine ();
        }
    }
}