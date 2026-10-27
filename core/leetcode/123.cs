// Url:https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iii

/*
123. Best Time to Buy and Sell Stock III
Hard

Say you have an array for which the ith element is the price of a given stock on day i.

Design an algorithm to find the maximum profit. You may complete at most two transactions.

Note: You may not engage in multiple transactions at the same time (i.e., you must sell the stock before you buy again).

Example 1:

Input: [3,3,5,0,0,3,1,4]
Output: 6
Explanation: Buy on day 4 (price = 0) and sell on day 6 (price = 3), profit = 3-0 = 3.
             Then buy on day 7 (price = 1) and sell on day 8 (price = 4), profit = 4-1 = 3.

Example 2:

Input: [1,2,3,4,5]
Output: 4
Explanation: Buy on day 1 (price = 1) and sell on day 5 (price = 5), profit = 5-1 = 4.
             Note that you cannot buy on day 1, buy on day 2 and sell them later, as you are
             engaging multiple transactions at the same time. You must sell before buying again.


Example 3:

Input: [7,6,4,3,1]
Output: 0
Explanation: In this case, no transaction is done, i.e. max profit = 0.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem123 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (MaxProfit_a([3, 3, 5, 0, 0, 3, 1, 4]));
            Console.WriteLine (MaxProfit_a([1, 2, 3, 4, 5]));

            Console.WriteLine (MaxProfit_b([3, 3, 5, 0, 0, 3, 1, 4]));
            Console.WriteLine (MaxProfit_b([1, 2, 3, 4, 5]));
        }

        public static int MaxProfit_a (int[] prices) {
            int result = 0;

            if (prices != null && prices.Length > 1) {
                int[] left = new int[prices.Length];
                int[] right = new int[prices.Length];

                left[0] = 0;
                int min = prices[0];
                for (int i = 1; i < prices.Length; i++) {
                    min = Math.Min (min, prices[i]);
                    left[i] = Math.Max (left[i - 1], prices[i] - min);
                }

                right[prices.Length - 1] = 0;
                int max = prices[prices.Length - 1];
                for (int i = prices.Length - 2; i >= 0; i--) {
                    max = Math.Max (max, prices[i]);
                    right[i] = Math.Max (right[i + 1], max - prices[i]);
                }

                for (int i = 0; i < prices.Length; i++) {
                    result = Math.Max (result, left[i] + right[i]);
                }
            }

            return result;
        }

        public static int MaxProfit_b (int[] prices) {
            int oneBuyOneSell = 0;
            int twoBuyTwoSell = 0;
            int oneBuy = Int32.MaxValue;
            int twoBuy = Int32.MaxValue;

            for (int i = 0; i < prices.Length; i++) {
                int p = prices[i];
                oneBuy = Math.Min (oneBuy, p);
                oneBuyOneSell = Math.Max (oneBuyOneSell, p - oneBuy);
                twoBuy = Math.Min (twoBuy, p - oneBuyOneSell);
                twoBuyTwoSell = Math.Max (twoBuyTwoSell, p - twoBuy);
            }

            return twoBuyTwoSell;
        }
    }
}