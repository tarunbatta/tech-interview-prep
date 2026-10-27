// Url: https://leetcode.com/problems/factor-combinations

/*
254. Factor Combinations
Medium

Numbers can be regarded as the product of their factors.
For example, 8 = 2 x 2 x 2 = 2 x 4.

Given an integer n, return all possible combinations of its factors. You may return the answer in any order.

Note that the factors should be in the range [2, n - 1].

Example 1:
Input: n = 1
Output: []

Example 2:
Input: n = 12
Output: [[2,6],[3,4],[2,2,3]]

Example 3:
Input: n = 37
Output: []

Constraints:
1 <= n <= 10^7
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem254
{
    public class Solution
    {
        public static void Init()
        {
            var res1 = GetFactors(12);
            foreach (var r in res1)
            {
                Console.WriteLine($"[{string.Join(", ", r)}]");
            }

            var res2 = GetFactors(37);
            Console.WriteLine(res2.Count); // 0
        }

        // Time: O(O(n^(1/2) * 2^(factors))) where factors <= log2(n)
        // Space: O(log n) recursion stack depth and current combination buffer
        public static IList<IList<int>> GetFactors(int n)
        {
            List<IList<int>> result = [];

            if (n <= 3)
            {
                return result;
            }

            Dfs(n, 2, [], result);
            return result;
        }

        private static void Dfs(int n, int start, List<int> path, List<IList<int>> result)
        {
            for (int i = start; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    // Add [path..., i, n / i]
                    path.Add(i);
                    path.Add(n / i);
                    result.Add(new List<int>(path));
                    path.RemoveAt(path.Count - 1);

                    // Recurse on n / i with minimum factor i
                    Dfs(n / i, i, path, result);
                    path.RemoveAt(path.Count - 1);
                }
            }
        }
    }
}