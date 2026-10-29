/*
Amazon OA: Find Pair With Max Appeal Sum

Find a pair with maximum appeal sum. Given an array A, appeal is computed by A[i] + A[j] + abs(i - j).
Return the pair of indices (i, j). Note: i might equal j.

Example 1:
Input: A = [1, 3, -1]
Output: [1, 1]
Explanation: Appeal = A[1] + A[1] + abs(0) = 3 + 3 + 0 = 6

Example 2:
Input: A = [1, 6, 1, 1, 1, 1, 7]
Output: [1, 6]
Explanation: Appeal = 6 + 7 + abs(1 - 6) = 18
*/

using System;

namespace InterviewPreperationGuide.Core.Amazon {
    public class MaxAppealSum {
        public static void Init () {
            Print (Solve ([1, 3, -1])); // [1, 1] = 6
            Print (Solve ([1, 6, 1, 1, 1, 1, 7])); // [1, 6] = 18
            Print (Solve ([6, 2, 7, 4, 4, 1, 6])); // [0, 6] = 18
        }

        // For i <= j: A[i] + A[j] + j - i = (A[i] - i) + (A[j] + j).
        // Sweep j, keeping the best (A[i] - i) seen so far for i <= j.
        // Time: O (n)
        // Space: O (1)
        public static (int i, int j, int appeal) Solve (int[] a) {
            int bestI = 0;
            (int i, int j, int appeal) best = (0, 0, int.MinValue);

            for (int j = 0; j < a.Length; j++) {
                if (a[j] - j > a[bestI] - bestI) {
                    bestI = j;
                }

                int appeal = a[bestI] - bestI + a[j] + j;
                if (appeal > best.appeal) {
                    best = (bestI, j, appeal);
                }
            }

            return best;
        }

        private static void Print ((int i, int j, int appeal) r) {
            Console.WriteLine ($"[{r.i}, {r.j}] = {r.appeal}");
        }
    }
}
