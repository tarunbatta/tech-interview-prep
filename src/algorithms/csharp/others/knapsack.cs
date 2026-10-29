/*
0/1 Knapsack Problem
https://en.wikipedia.org/wiki/Knapsack_problem

Given n items with weights w[i] and values v[i], and a knapsack of capacity W,
choose a subset of items (each at most once) maximizing total value with total weight <= W.

dp[i][c] = best value using the first i items with capacity c
         = max(dp[i-1][c], dp[i-1][c - w[i]] + v[i])   if w[i] <= c
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Algorithms.Others {
    public class Knapsack {
        public static void Init () {
            int[] weights = [1, 3, 4, 5];
            int[] values = [1, 4, 5, 7];
            int capacity = 7;

            Console.WriteLine (MaxValue (weights, values, capacity)); // 9
            Console.WriteLine (MaxValueOptimized (weights, values, capacity)); // 9

            List<int> chosen = ChosenItems (weights, values, capacity);
            Console.WriteLine ($"Items: [{string.Join (",", chosen)}]"); // [1,2] (weights 3 + 4)

            Console.WriteLine (MaxValueMemo ([10, 20, 30], [60, 100, 120], 50)); // 220
        }

        // Bottom-up 2D table.
        // Time: O (n * W)
        // Space: O (n * W)
        public static int MaxValue (int[] w, int[] v, int capacity) {
            int[, ] dp = BuildTable (w, v, capacity);
            return dp[w.Length, capacity];
        }

        // Space-optimized: iterate capacity downwards so each item is used at most once.
        // Time: O (n * W)
        // Space: O (W)
        public static int MaxValueOptimized (int[] w, int[] v, int capacity) {
            int[] dp = new int[capacity + 1];

            for (int i = 0; i < w.Length; i++) {
                for (int c = capacity; c >= w[i]; c--) {
                    dp[c] = Math.Max (dp[c], dp[c - w[i]] + v[i]);
                }
            }

            return dp[capacity];
        }

        // Top-down recursion with memoization.
        // Time: O (n * W)
        // Space: O (n * W)
        public static int MaxValueMemo (int[] w, int[] v, int capacity) {
            int?[, ] memo = new int?[w.Length, capacity + 1];
            return Solve (w, v, 0, capacity, memo);
        }

        private static int Solve (int[] w, int[] v, int i, int c, int?[, ] memo) {
            if (i == w.Length || c == 0) {
                return 0;
            }

            if (memo[i, c].HasValue) {
                return memo[i, c].Value;
            }

            int skip = Solve (w, v, i + 1, c, memo);
            int take = w[i] <= c ? v[i] + Solve (w, v, i + 1, c - w[i], memo) : 0;

            memo[i, c] = Math.Max (skip, take);
            return memo[i, c].Value;
        }

        // Reconstruct which item indices were chosen by walking the table backwards.
        public static List<int> ChosenItems (int[] w, int[] v, int capacity) {
            int[, ] dp = BuildTable (w, v, capacity);
            List<int> items = new List<int> ();

            int cap = capacity;
            for (int i = w.Length; i > 0; i--) {
                if (dp[i, cap] != dp[i - 1, cap]) {
                    items.Add (i - 1);
                    cap -= w[i - 1];
                }
            }

            items.Reverse ();
            return items;
        }

        private static int[, ] BuildTable (int[] w, int[] v, int capacity) {
            int n = w.Length;
            int[, ] dp = new int[n + 1, capacity + 1];

            for (int i = 1; i <= n; i++) {
                for (int c = 0; c <= capacity; c++) {
                    dp[i, c] = dp[i - 1, c];

                    if (w[i - 1] <= c) {
                        dp[i, c] = Math.Max (dp[i, c], dp[i - 1, c - w[i - 1]] + v[i - 1]);
                    }
                }
            }

            return dp;
        }
    }
}
