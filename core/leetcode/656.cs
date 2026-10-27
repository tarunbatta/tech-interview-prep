// Url:https://leetcode.com/problems/coin-path

/*
656. Coin Path
Hard

You are given an integer array coins (1-indexed) of length n and an integer maxJump. You can jump to index i from index j if and only if:
- i < j <= min(i + maxJump, n)
- coins[j] != -1

Each index j has a cost of coins[j] that you have to pay every time you jump to index j.
You start at index 1 and want to reach index n with the minimum total cost.

Return the path of indices you visited as an array of integers with minimum total cost. If there are multiple paths with the same minimum cost, return the lexicographically smallest path. If you cannot reach index n, return an empty array.

A path p1 is lexicographically smaller than p2 if for the first index i where p1[i] != p2[i], p1[i] < p2[i].

Example 1:
Input: coins = [1,2,4,-1,2], maxJump = 2
Output: [1,3,5]

Example 2:
Input: coins = [1,2,4,-1,2], maxJump = 1
Output: []

Constraints:
- 1 <= coins.length <= 1000
- -1 <= coins[i] <= 100
- coins[1] != -1
- 1 <= maxJump <= 100
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem656 {
    public class Solution {
        public void Init() {
            int[] coins1 = [1, 2, 4, -1, 2];
            int maxJump1 = 2;
            Console.WriteLine(string.Join(", ", CheapestJump(coins1, maxJump1))); // 1, 3, 5

            int[] coins2 = [1, 2, 4, -1, 2];
            int maxJump2 = 1;
            Console.WriteLine(string.Join(", ", CheapestJump(coins2, maxJump2))); // empty
        }

        // Time: O(n * maxJump) where n is length of coins
        // Space: O(n) for dp array and next pointers
        public IList<int> CheapestJump(int[] coins, int maxJump) {
            int n = coins.Length;
            if (coins[n - 1] == -1 || coins[0] == -1) {
                return [];
            }

            long[] dp = new long[n];
            int[] next = new int[n];
            Array.Fill(dp, long.MaxValue);
            Array.Fill(next, -1);

            dp[n - 1] = coins[n - 1];

            for (int i = n - 2; i >= 0; i--) {
                if (coins[i] == -1) {
                    continue;
                }

                int limit = Math.Min(i + maxJump, n - 1);
                for (int j = i + 1; j <= limit; j++) {
                    if (dp[j] != long.MaxValue) {
                        long cost = coins[i] + dp[j];
                        if (cost < dp[i]) {
                            dp[i] = cost;
                            next[i] = j;
                        }
                    }
                }
            }

            if (dp[0] == long.MaxValue) {
                return [];
            }

            List<int> path = [];
            int curr = 0;
            while (curr != -1) {
                path.Add(curr + 1);
                curr = next[curr];
            }

            return path;
        }
    }
}