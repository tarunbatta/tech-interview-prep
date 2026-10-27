// Url: https://leetcode.com/problems/android-unlock-patterns

/*
351. Android Unlock Patterns
Medium

Android devices have a special lock screen with a 3 x 3 grid of keys. The keys are numbered 1 through 9.

The lock screen has following rules:
- Each pattern must connect at least m keys and at most n keys.
- All the keys in a pattern must be distinct.
- If the line connecting two consecutive keys in the pattern passes through any other keys, the other keys must have previously in that pattern been selected. (e.g. 1-3 requires 2 to have been visited first, 1-9 requires 5).

Given two integers m and n, return the total number of unlock patterns of the Android lock screen, which consist of a minimum of m keys and a maximum of n keys.

Example 1:
Input: m = 1, n = 1
Output: 9

Example 2:
Input: m = 1, n = 2
Output: 65

Constraints:
1 <= m, n <= 9
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem351
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(NumberOfPatterns(1, 1)); // 9
            Console.WriteLine(NumberOfPatterns(1, 2)); // 65
        }

        // Time: O(1) bounded by total valid permutation paths across at most 9 digits
        // Space: O(1) recursion stack bounded by depth 9
        public static int NumberOfPatterns(int m, int n)
        {
            int[,] skip = new int[10, 10];
            skip[1, 3] = skip[3, 1] = 2;
            skip[4, 6] = skip[6, 4] = 5;
            skip[7, 9] = skip[9, 7] = 8;
            skip[1, 7] = skip[7, 1] = 4;
            skip[2, 8] = skip[8, 2] = 5;
            skip[3, 9] = skip[9, 3] = 6;
            skip[1, 9] = skip[9, 1] = 5;
            skip[3, 7] = skip[7, 3] = 5;

            bool[] visited = new bool[10];
            int totalPatterns = 0;

            for (int len = m; len <= n; len++)
            {
                // 1, 3, 7, 9 are symmetric corners
                totalPatterns += Dfs(visited, skip, 1, len - 1) * 4;

                // 2, 4, 6, 8 are symmetric edges
                totalPatterns += Dfs(visited, skip, 2, len - 1) * 4;

                // 5 is the center
                totalPatterns += Dfs(visited, skip, 5, len - 1);
            }

            return totalPatterns;
        }

        private static int Dfs(bool[] visited, int[,] skip, int current, int remain)
        {
            if (remain == 0)
            {
                return 1;
            }

            visited[current] = true;
            int count = 0;

            for (int next = 1; next <= 9; next++)
            {
                if (!visited[next])
                {
                    int intermediate = skip[current, next];

                    if (intermediate == 0 || visited[intermediate])
                    {
                        count += Dfs(visited, skip, next, remain - 1);
                    }
                }
            }

            visited[current] = false;
            return count;
        }
    }
}