// Url:https://leetcode.com/problems/best-time-to-buy-and-sell-stock

/*
121. Best Time to Buy and Sell Stock
Easy

Say you have an array for which the ith element is the price of a given stock on day i.

If you were only permitted to complete at most one transaction (i.e., buy one and sell one share of the stock), design an algorithm to find the maximum profit.

Note that you cannot sell a stock before you buy one.

Example 1:

Input: [7,1,5,3,6,4]
Output: 5
Explanation: Buy on day 2 (price = 1) and sell on day 5 (price = 6), profit = 6-1 = 5.
             Not 7-1 = 6, as selling price needs to be larger than buying price.


Example 2:

Input: [7,6,4,3,1]
Output: 0
Explanation: In this case, no transaction is done, i.e. max profit = 0.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem121 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (MaxProfit_a(null));
            Console.WriteLine (MaxProfit_a([]));
            Console.WriteLine (MaxProfit_a([7, 1, 5, 3, 6, 4]));
            Console.WriteLine (MaxProfit_a([7, 6, 4, 3, 1]));

            Console.WriteLine (MaxProfit_b(null));
            Console.WriteLine (MaxProfit_b([]));
            Console.WriteLine (MaxProfit_b([7, 1, 5, 3, 6, 4]));
            Console.WriteLine (MaxProfit_b([7, 6, 4, 3, 1]));
        }

        public static int MaxProfit_a (int[] prices) {
            int result = 0;

            if (prices == null || prices.Length == 0) {
                return result;
            }

            for (int i = 0; i < prices.Length - 1; i++) {
                for (int j = i + 1; j < prices.Length; j++) {
                    int profit = prices[j] - prices[i];
                    if (profit > result) {
                        result = profit;
                    }
                }
            }

            return result;
        }

        public static int MaxProfit_b (int[] prices) {
            if (prices == null || prices.Length == 0) {
                return 0;
            }

            int minprice = Int32.MaxValue;
            int maxprofit = 0;

            for (int i = 0; i < prices.Length; i++) {
                if (prices[i] < minprice) {
                    minprice = prices[i];
                } else if (prices[i] - minprice > maxprofit) {
                    maxprofit = prices[i] - minprice;
                }
            }

            return maxprofit;
        }
    }
}