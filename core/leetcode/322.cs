// Url:https://leetcode.com/problems/coin-change

/*
322. Coin Change
Medium

You are given coins of different denominations and a total amount of money amount. Write a function to compute the fewest number of coins that you need to make up that amount. If that amount of money cannot be made up by any combination of the coins, return -1.

Example 1:

Input: coins = [1, 2, 5], amount = 11
Output: 3 
Explanation: 11 = 5 + 5 + 1

Example 2:

Input: coins = [2], amount = 3
Output: -1


Note:
You may assume that you have an infinite number of each kind of coin.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem322
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(CoinChange([1, 2, 5], 11)); // Expected: 3
            Console.WriteLine(CoinChange([2], 3));        // Expected: -1
            Console.WriteLine(CoinChange([1], 0));        // Expected: 0
        }

        // Time: O(amount * n) where n is the number of coin denominations
        // Space: O(amount)
        public static int CoinChange(int[] coins, int amount)
        {
            if (amount < 0 || coins == null || coins.Length == 0)
            {
                return -1;
            }

            if (amount == 0)
            {
                return 0;
            }

            int[] dp = new int[amount + 1];
            Array.Fill(dp, amount + 1);
            dp[0] = 0;

            for (int i = 1; i <= amount; i++)
            {
                foreach (int c in coins)
                {
                    if (i >= c)
                    {
                        dp[i] = Math.Min(dp[i], dp[i - c] + 1);
                    }
                }
            }

            return dp[amount] > amount ? -1 : dp[amount];
        }
    }
}