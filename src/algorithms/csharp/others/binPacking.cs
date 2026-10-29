/*
Bin Packing Problem
https://en.wikipedia.org/wiki/Bin_packing_problem

Pack items of given sizes into the fewest bins of fixed capacity.
The problem is NP-hard, so in interviews the expected answer is usually a greedy heuristic:

- Next Fit:            keep one open bin; open a new one when the item doesn't fit.     <= 2 * OPT
- First Fit:           put the item in the first bin it fits in.                         <= 1.7 * OPT
- First Fit Decreasing: sort descending, then First Fit.                                <= 11/9 * OPT + 6/9
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.Algorithms.Others {
    public class BinPacking {
        public static void Init () {
            int[] items = [4, 8, 1, 4, 2, 1];
            int capacity = 10;

            Console.WriteLine ($"Next Fit: {NextFit (items, capacity)}"); // 3
            Console.WriteLine ($"First Fit: {FirstFit (items, capacity)}"); // 2
            Console.WriteLine ($"First Fit Decreasing: {FirstFitDecreasing (items, capacity)}"); // 2

            int[] items2 = [2, 5, 4, 7, 1, 3, 8];
            Console.WriteLine ($"Next Fit: {NextFit (items2, capacity)}"); // 5
            Console.WriteLine ($"First Fit: {FirstFit (items2, capacity)}"); // 4
            Console.WriteLine ($"First Fit Decreasing: {FirstFitDecreasing (items2, capacity)}"); // 3
        }

        // Time: O (n)
        // Space: O (1)
        public static int NextFit (int[] items, int capacity) {
            int bins = 0;
            int remaining = 0;

            foreach (int item in items) {
                if (item > remaining) {
                    bins++;
                    remaining = capacity;
                }
                remaining -= item;
            }

            return bins;
        }

        // Time: O (n * bins); O (n log n) possible with a segment tree over bins
        // Space: O (bins)
        public static int FirstFit (int[] items, int capacity) {
            List<int> remaining = new List<int> ();

            foreach (int item in items) {
                if (item > capacity) {
                    throw new ArgumentException ($"Item {item} exceeds bin capacity {capacity}");
                }

                int idx = remaining.FindIndex (r => r >= item);
                if (idx == -1) {
                    remaining.Add (capacity - item);
                } else {
                    remaining[idx] -= item;
                }
            }

            return remaining.Count;
        }

        // Time: O (n log n + n * bins)
        // Space: O (n)
        public static int FirstFitDecreasing (int[] items, int capacity) {
            return FirstFit (items.OrderByDescending (x => x).ToArray (), capacity);
        }
    }
}
