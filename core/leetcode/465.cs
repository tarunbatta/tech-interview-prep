// Url: https://leetcode.com/problems/optimal-account-balancing

/*
465. Optimal Account Balancing
Hard

You are given an array of transactions transactions where transactions[i] = [fromi, toi, amounti] indicates that the person with ID = fromi gave amounti $ to the person with ID = toi.

Return the minimum number of transactions required to settle the debt.

Example 1:
Input: transactions = [[0,1,10],[2,0,5]]
Output: 2
Explanation:
Person #0 gave person #1 $10.
Person #2 gave person #0 $5.
Two transactions are needed. One way to settle the debt is person #1 pays person #0 and #2 $5 each.

Example 2:
Input: transactions = [[0,1,10],[1,0,1],[1,2,5],[2,0,5]]
Output: 1
Explanation:
Person #0 gave person #1 $10.
Person #1 gave person #0 $1.
Person #1 gave person #2 $5.
Person #2 gave person #0 $5.
Therefore, person #1 only need to give person #0 $4, and all debt is settled.

Constraints:
1 <= transactions.length <= 8
transactions[i].length == 3
0 <= fromi, toi <= 12
fromi != toi
1 <= amounti <= 100
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem465
{
    public class Solution
    {
        public static void Init()
        {
            int[][] trans1 =
            [
                [0, 1, 10],
                [2, 0, 5]
            ];
            Console.WriteLine(MinTransfers(trans1)); // 2

            int[][] trans2 =
            [
                [0, 1, 10],
                [1, 0, 1],
                [1, 2, 5],
                [2, 0, 5]
            ];
            Console.WriteLine(MinTransfers(trans2)); // 1
        }

        // Time: O((N - 1)!) where N is the number of unsettled accounts (N <= 12)
        // Space: O(N) for recursion stack and net balance list
        public static int MinTransfers(int[][] transactions)
        {
            if (transactions == null || transactions.Length == 0)
            {
                return 0;
            }

            Dictionary<int, long> balanceMap = [];

            foreach (var trans in transactions)
            {
                int from = trans[0];
                int to = trans[1];
                int amount = trans[2];

                balanceMap[from] = balanceMap.GetValueOrDefault(from, 0) - amount;
                balanceMap[to] = balanceMap.GetValueOrDefault(to, 0) + amount;
            }

            List<long> debts = [];
            foreach (var balance in balanceMap.Values)
            {
                if (balance != 0)
                {
                    debts.Add(balance);
                }
            }

            return Settle(0, debts);
        }

        private static int Settle(int start, List<long> debts)
        {
            while (start < debts.Count && debts[start] == 0)
            {
                start++;
            }

            if (start == debts.Count)
            {
                return 0;
            }

            int minTransactions = int.MaxValue;

            for (int i = start + 1; i < debts.Count; i++)
            {
                // Only settle between a debtor and a creditor (opposite signs)
                if (debts[start] * debts[i] < 0)
                {
                    debts[i] += debts[start];
                    minTransactions = Math.Min(minTransactions, 1 + Settle(start + 1, debts));
                    debts[i] -= debts[start];

                    // Optimal greedy cut: if exact mutual settlement found, don't explore further branches
                    if (debts[start] + debts[i] == 0)
                    {
                        break;
                    }
                }
            }

            return minTransactions;
        }
    }
}