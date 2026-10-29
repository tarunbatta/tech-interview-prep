/*
Coins: 

Given an infinite number of quarters (25 cents), dimes (10 cents), nickels (5 cents), and
pennies (1 cent), write code to calculate the number of ways of representing n cents.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c8q11 {
    public class Solution {
        private static readonly int[] Denoms = { 25, 10, 5, 1 };

        public static int CountWays (int amount) {
            int[,] map = new int[amount + 1, Denoms.Length];
            return CountWaysHelper (amount, Denoms, 0, map);
        }

        private static int CountWaysHelper (int amount, int[] denoms, int index, int[,] map) {
            if (map[amount, index] > 0) return map[amount, index];
            if (index >= denoms.Length - 1) return 1;

            int denomAmount = denoms[index];
            int ways = 0;
            for (int i = 0; i * denomAmount <= amount; i++) {
                int amountRemaining = amount - i * denomAmount;
                ways += CountWaysHelper (amountRemaining, denoms, index + 1, map);
            }
            map[amount, index] = ways;
            return ways;
        }

        public static void Init (string[] args = null) {
            Console.WriteLine ($"Ways for 25 cents: {CountWays (25)}"); // 13
            Console.WriteLine ($"Ways for 10 cents: {CountWays (10)}"); // 4
        }
    }
}
