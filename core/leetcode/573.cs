// Url: https://leetcode.com/problems/squirrel-simulation

/*
573. Squirrel Simulation
Medium

You are given two integers height and width representing a garden of size height x width. You are also given:
- an array tree where tree = [tree_r, tree_c] is the position of the tree,
- an array squirrel where squirrel = [squirrel_r, squirrel_c] is the position of the squirrel,
- an array nuts where nuts[i] = [nut_i_r, nut_i_c] is the position of the ith nut.

The squirrel can only take at most one nut at one time and can move in four directions - up, down, left and right, to the adjacent cell. The distance is represented by the number of moves.

Return the minimal distance for the squirrel to collect all the nuts and put them under the tree one by one.

The distance is calculated by Manhattan Distance: |x1 - x2| + |y1 - y2|.

Example 1:
Input: height = 5, width = 7, tree = [2,2], squirrel = [4,4], nuts = [[3,0], [2,5]]
Output: 12

Example 2:
Input: height = 1, width = 3, tree = [0,1], squirrel = [0,0], nuts = [[0,2]]
Output: 3

Constraints:
1 <= height, width <= 100
tree.length == 2
squirrel.length == 2
1 <= nuts.length <= 5000
nuts[i].length == 2
0 <= tree_r, squirrel_r, nut_i_r < height
0 <= tree_c, squirrel_c, nut_i_c < width
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem573
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(MinDistance(5, 7, [2, 2], [4, 4], [[3, 0], [2, 5]])); // 12
            Console.WriteLine(MinDistance(1, 3, [0, 1], [0, 0], [[0, 2]]));         // 3
        }

        // Time: O(n) where n is nuts.Length (single pass Manhattan distance calculation)
        // Space: O(1) auxiliary space
        public static int MinDistance(int height, int width, int[] tree, int[] squirrel, int[][] nuts)
        {
            if (nuts == null || nuts.Length == 0)
            {
                return 0;
            }

            int totalRoundTrip = 0;
            int maxSavings = int.MinValue;

            foreach (var nut in nuts)
            {
                int distToTree = Math.Abs(nut[0] - tree[0]) + Math.Abs(nut[1] - tree[1]);
                int distToSquirrel = Math.Abs(nut[0] - squirrel[0]) + Math.Abs(nut[1] - squirrel[1]);

                totalRoundTrip += 2 * distToTree;

                // Savings = distToTree - distToSquirrel (maximizing savings minimizes total distance)
                maxSavings = Math.Max(maxSavings, distToTree - distToSquirrel);
            }

            return totalRoundTrip - maxSavings;
        }
    }
}