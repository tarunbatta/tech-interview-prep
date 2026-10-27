// Url: https://leetcode.com/problems/output-contest-matches

/*
544. Output Contest Matches
Medium

During the NBA playoffs, we always arrange the rather strong team to play with the rather weak team, like make the rank 1 team play with the rank nth team, which is a good strategy to make the contest more interesting.

Given the number of teams n, you need to generate their contest matches in the form of a string.

The team with rank 1 is the best, while the team with rank n is the worst. We use parentheses '(', ')' and commas ',' to represent the contest team relationship. One round of the contest matches is represented by the form of (Team1, Team2), where Team1 is better than Team2.

Example 1:
Input: n = 4
Output: "((1,4),(2,3))"
Explanation:
In the first round, we pair the team 1 and 4, the team 2 and 3, which is (1,4),(2,3).
In the second round, the winners of (1,4) and (2,3) will play each other, which is ((1,4),(2,3)).
Since we have only one game left, that is the final answer.

Example 2:
Input: n = 8
Output: "(((1,8),(4,5)),((2,7),(3,6)))"
Explanation:
First round: (1,8),(2,7),(3,6),(4,5)
Second round: ((1,8),(4,5)),((2,7),(3,6))
Third round: (((1,8),(4,5)),((2,7),(3,6)))
Since we have only one game left, that is the final answer.

Constraints:
n == 2^k where k in range [1, 12].
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem544
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(FindContestMatch(4)); // ((1,4),(2,3))
            Console.WriteLine(FindContestMatch(8)); // (((1,8),(4,5)),((2,7),(3,6)))
        }

        // Time: O(n log n) across log2(n) tournament rounds
        // Space: O(n) for match pair string array
        public static string FindContestMatch(int n)
        {
            if (n <= 0)
            {
                return string.Empty;
            }

            string[] teams = new string[n];
            for (int i = 0; i < n; i++)
            {
                teams[i] = (i + 1).ToString();
            }

            while (n > 1)
            {
                for (int i = 0; i < n / 2; i++)
                {
                    teams[i] = $"({teams[i]},{teams[n - 1 - i]})";
                }

                n /= 2;
            }

            return teams[0];
        }
    }
}