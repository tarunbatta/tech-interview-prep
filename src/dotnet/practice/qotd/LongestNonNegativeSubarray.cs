using System;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Find Longest Positive Sub-Array
    /// Given an array of integers, find the indices x and y that maximize y - x such that
    /// the sum of a[x..y] is non-negative (>= 0).
    ///
    /// Example:
    ///   Input:  1, -6, 2, -1, 4, -3
    ///   Output: x = 0, y = 4, Sum = 0
    /// </summary>
    public class LongestNonNegativeSubarray {
        public static void Init () {
            Print (Solve ([1, -6, 2, -1, 4, -3])); // x=0, y=4, sum=0
            Print (Solve ([-1, -2, -3])); // none
            Print (Solve ([3, -1, -1, -1, 5])); // x=0, y=4, sum=5
        }

        /// With prefix sums P (P[0] = 0, P[k] = a[0] + ... + a[k-1]), sum(a[x..y]) = P[y+1] - P[x].
        /// We need the widest (x, j = y + 1) with P[x] <= P[j].
        /// prefixMin[i] = min(P[0..i]) and suffixMax[j] = max(P[j..n]) are both monotonic,
        /// so a single two-pointer sweep finds the widest valid window.
        /// Time: O (n)
        /// Space: O (n)
        public static (int x, int y, int sum) Solve (int[] a) {
            int n = a.Length;
            long[] p = new long[n + 1];
            for (int i = 0; i < n; i++) {
                p[i + 1] = p[i] + a[i];
            }

            long[] prefixMin = new long[n + 1];
            long[] suffixMax = new long[n + 1];
            prefixMin[0] = p[0];
            for (int i = 1; i <= n; i++) {
                prefixMin[i] = Math.Min (prefixMin[i - 1], p[i]);
            }
            suffixMax[n] = p[n];
            for (int i = n - 1; i >= 0; i--) {
                suffixMax[i] = Math.Max (suffixMax[i + 1], p[i]);
            }

            int bestLen = 0; // number of elements in the widest valid window
            int x = 0, j = 0;

            while (x <= n && j <= n) {
                if (prefixMin[x] <= suffixMax[j]) {
                    // Some x' <= x and j' >= j satisfy P[x'] <= P[j'], so width j - x is achievable.
                    bestLen = Math.Max (bestLen, j - x);
                    j++;
                } else {
                    x++;
                }
            }

            if (bestLen == 0) {
                return (-1, -1, 0);
            }

            // Recover concrete indices for the optimal width.
            for (int start = 0; start + bestLen <= n; start++) {
                long sum = p[start + bestLen] - p[start];
                if (sum >= 0) {
                    return (start, start + bestLen - 1, (int) sum);
                }
            }

            return (-1, -1, 0);
        }

        private static void Print ((int x, int y, int sum) r) {
            Console.WriteLine (r.x == -1 ? "none" : $"x={r.x}, y={r.y}, sum={r.sum}");
        }
    }
}
