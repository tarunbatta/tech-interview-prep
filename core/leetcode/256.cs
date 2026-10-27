// Url: https://leetcode.com/problems/paint-house

/*
256. Paint House
Medium

There is a row of n houses, where each house can be painted one of three colors: red, blue, or green. The cost of painting each house with a certain color is different. You have to paint all the houses such that no two adjacent houses have the same color.

The cost of painting each house with a certain color is represented by an n x 3 cost matrix costs.

For example, costs[0][0] is the cost of painting house 0 with the color red; costs[1][2] is the cost of painting house 1 with color green, and so on...

Return the minimum cost to paint all houses.

Example 1:
Input: costs = [[17,2,17],[16,16,5],[14,3,19]]
Output: 10
Explanation: Paint house 0 into blue, paint house 1 into green, paint house 2 into blue.
Minimum cost: 2 + 5 + 3 = 10.

Example 2:
Input: costs = [[7,6,2]]
Output: 2

Constraints:
costs.length == n
costs[i].length == 3
1 <= n <= 100
1 <= costs[i][j] <= 20
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem256
{
    public class Solution
    {
        public void Init()
        {
            int[][] costs1 = new int[][]
            {
                new int[] { 17, 2, 17 },
                new int[] { 16, 16, 5 },
                new int[] { 14, 3, 19 }
            };
            Console.WriteLine(MinCost(costs1)); // 10

            int[][] costs2 = new int[][]
            {
                new int[] { 7, 6, 2 }
            };
            Console.WriteLine(MinCost(costs2)); // 2
        }

        // Time: O(n) where n is costs.Length
        // Space: O(1) auxiliary space
        public static int MinCost(int[][] costs)
        {
            if (costs == null || costs.Length == 0)
            {
                return 0;
            }

            int prevR = costs[0][0];
            int prevB = costs[0][1];
            int prevG = costs[0][2];

            for (int i = 1; i < costs.Length; i++)
            {
                int currR = costs[i][0] + Math.Min(prevB, prevG);
                int currB = costs[i][1] + Math.Min(prevR, prevG);
                int currG = costs[i][2] + Math.Min(prevR, prevB);

                prevR = currR;
                prevB = currB;
                prevG = currG;
            }

            return Math.Min(prevR, Math.Min(prevB, prevG));
        }
    }
}