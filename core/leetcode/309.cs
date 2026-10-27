// Url:https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-cooldown

/*
309. Best Time to Buy and Sell Stock with Cooldown
Medium

Say you have an array for which the ith element is the price of a given stock on day i.

Design an algorithm to find the maximum profit. You may complete as many transactions as you like (ie, buy one and sell one share of the stock multiple times) with the following restrictions:


	You may not engage in multiple transactions at the same time (ie, you must sell the stock before you buy again).
	After you sell your stock, you cannot buy stock on next day. (ie, cooldown 1 day)


Example:

Input: [1,2,3,0,2]
Output: 3 
Explanation: transactions = [buy, sell, cooldown, buy, sell]
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem309
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(MaxProfit(null));              // Expected: 0
            Console.WriteLine(MaxProfit([]));                // Expected: 0
            Console.WriteLine(MaxProfit([1, 2, 3, 0, 2]));   // Expected: 3
            Console.WriteLine(MaxProfit([1]));               // Expected: 0
        }

        // Time: O(n) where n is the number of days
        // Space: O(1) auxiliary space
        public static int MaxProfit(int[] prices)
        {
            if (prices == null || prices.Length <= 1)
            {
                return 0;
            }

            int held = -prices[0];
            int sold = 0;
            int reset = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                int price = prices[i];
                int prevSold = sold;
                sold = held + price;
                held = Math.Max(held, reset - price);
                reset = Math.Max(reset, prevSold);
            }

            return Math.Max(sold, reset);
        }
    }
}