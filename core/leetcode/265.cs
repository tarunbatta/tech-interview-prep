// Url: https://leetcode.com/problems/paint-house-ii

/*
265. Paint House II
Hard

There are a row of n houses, each house can be painted with one of the k colors. The cost of painting each house with a certain color is different. You have to paint all the houses such that no two adjacent houses have the same color.

The cost of painting each house with a certain color is represented by an n x k cost matrix costs.

For example, costs[0][0] is the cost of painting house 0 with color 0; costs[1][2] is the cost of painting house 1 with color 2, and so on...

Return the minimum cost to paint all houses.

Example 1:
Input: costs = [[1,5,3],[2,9,4]]
Output: 5
Explanation:
Paint house 0 into color 0, paint house 1 into color 2. Minimum cost: 1 + 4 = 5; 
Or paint house 0 into color 2, paint house 1 into color 0. Minimum cost: 3 + 2 = 5.

Example 2:
Input: costs = [[1,3],[2,4]]
Output: 5

Constraints:
costs.length == n
costs[i].length == k
1 <= n <= 100
2 <= k <= 20
1 <= costs[i][j] <= 20

Follow up: Could you solve it in O(nk) runtime?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem265
{
    public class Solution
    {
        public static void Init()
        {
            int[][] costs1 =
            [
                [1, 5, 3],
                [2, 9, 4]
            ];
            Console.WriteLine(MinCostII(costs1)); // 5

            int[][] costs2 =
            [
                [1, 3],
                [2, 4]
            ];
            Console.WriteLine(MinCostII(costs2)); // 5
        }

        // Time: O(n * k) where n is costs.Length and k is the number of colors
        // Space: O(1) auxiliary space
        public static int MinCostII(int[][] costs)
        {
            if (costs == null || costs.Length == 0 || costs[0].Length == 0)
            {
                return 0;
            }

            int prevMin1 = 0;
            int prevMin2 = 0;
            int prevColor1 = -1;

            for (int i = 0; i < costs.Length; i++)
            {
                int currMin1 = int.MaxValue;
                int currMin2 = int.MaxValue;
                int currColor1 = -1;

                for (int c = 0; c < costs[i].Length; c++)
                {
                    int cost = costs[i][c] + (c == prevColor1 ? prevMin2 : prevMin1);

                    if (cost < currMin1)
                    {
                        currMin2 = currMin1;
                        currMin1 = cost;
                        currColor1 = c;
                    }
                    else if (cost < currMin2)
                    {
                        currMin2 = cost;
                    }
                }

                prevMin1 = currMin1;
                prevMin2 = currMin2;
                prevColor1 = currColor1;
            }

            return prevMin1;
        }
    }
}