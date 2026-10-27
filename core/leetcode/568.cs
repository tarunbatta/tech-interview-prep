// Url: https://leetcode.com/problems/maximum-vacation-days

/*
568. Maximum Vacation Days
Hard

LeetCode wants to give one of its best employees the option to travel among n cities code-named from 0 to n - 1. Initially, the employee is in city 0 on Monday of the first week, and can travel between cities on Mondays.

The connectivity of cities is represented by an n x n matrix flights, where flights[i][j] = 1 indicates that there is a flight from city i to city j, and flights[i][j] = 0 otherwise. Also, flights[i][i] == 0 for all i.

You are given another n x k matrix days representing vacation days, where days[i][j] represents the maximum vacation days the employee can take in city i during week j.

Return the maximum vacation days the employee can take during k weeks.

Example 1:
Input: flights = [[0,1,1],[1,0,1],[1,1,0]], days = [[1,3,1],[6,0,3],[3,3,3]]
Output: 12
Explanation:
Ans = 6 + 3 + 3 = 12.
One of the best strategies is:
1st week : fly from city 0 to city 1 on Monday, and play 6 days and work 1 day.
2nd week : fly from city 1 to city 2 on Monday, and play 3 days and work 4 days.
3rd week : stay at city 2 on Monday, and play 3 days and work 4 days.

Example 2:
Input: flights = [[0,0,0],[0,0,0],[0,0,0]], days = [[1,1,1],[7,7,7],[7,7,7]]
Output: 3

Example 3:
Input: flights = [[0,1,1],[1,0,0],[1,0,0]], days = [[3,4,4],[2,0,5],[0,1,1]]
Output: 9

Constraints:
n == flights.length
n == flights[i].length
n == days.length
k == days[i].length
1 <= n, k <= 100
flights[i][j] is either 0 or 1.
0 <= days[i][j] <= 7
*/

using System;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem568
{
    public class Solution
    {
        public void Init()
        {
            int[][] flights = new int[][]
            {
                new int[] { 0, 1, 1 },
                new int[] { 1, 0, 1 },
                new int[] { 1, 1, 0 }
            };

            int[][] days = new int[][]
            {
                new int[] { 1, 3, 1 },
                new int[] { 6, 0, 3 },
                new int[] { 3, 3, 3 }
            };

            Console.WriteLine(MaxVacationDays(flights, days)); // 12
        }

        // Time: O(K * N^2) where N is cities count and K is weeks count
        // Space: O(N) for current and next week DP states
        public static int MaxVacationDays(int[][] flights, int[][] days)
        {
            if (flights == null || days == null || flights.Length == 0 || days.Length == 0)
            {
                return 0;
            }

            int n = flights.Length;
            int k = days[0].Length;

            int[] dp = new int[n];
            Array.Fill(dp, -1);
            dp[0] = 0; // Starts at city 0

            for (int week = 0; week < k; week++)
            {
                int[] nextDp = new int[n];
                Array.Fill(nextDp, -1);

                for (int dest = 0; dest < n; dest++)
                {
                    for (int origin = 0; origin < n; origin++)
                    {
                        if (dp[origin] != -1 && (origin == dest || flights[origin][dest] == 1))
                        {
                            nextDp[dest] = Math.Max(nextDp[dest], dp[origin] + days[dest][week]);
                        }
                    }
                }

                dp = nextDp;
            }

            return dp.Max();
        }
    }
}