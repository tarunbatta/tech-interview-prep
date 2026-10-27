// Url:https://leetcode.com/problems/maximum-distance-in-arrays

/*
624. Maximum Distance in Arrays
Medium

You are given m arrays, where each array is sorted in ascending order.

You can pick up two integers from two different arrays (each array picks one) and calculate the distance. We define the distance between two integers a and b to be their absolute difference |a - b|.

Return the maximum distance.

Example 1:
Input: arrays = [[1,2,3],[4,5],[1,2,3]]
Output: 4
Explanation: One way to reach the maximum distance 4 is to pick 1 in the first or third array and pick 5 in the second array.

Example 2:
Input: arrays = [[1],[1]]
Output: 0

Constraints:
- m == arrays.length
- 2 <= m <= 10^5
- 1 <= arrays[i].length <= 500
- -10^4 <= arrays[i][j] <= 10^4
- arrays[i] is sorted in ascending order.
- There will be at most 10^5 integers in all the arrays.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem624 {
    public class Solution {
        public static void Init() {
            IList<IList<int>> arrays1 = [
                [1, 2, 3],
                [4, 5],
                [1, 2, 3]
            ];
            Console.WriteLine(MaxDistance(arrays1)); // 4

            IList<IList<int>> arrays2 = [
                [1],
                [1]
            ];
            Console.WriteLine(MaxDistance(arrays2)); // 0
        }

        // Time: O(m) where m is the number of arrays
        // Space: O(1)
        public static int MaxDistance(IList<IList<int>> arrays) {
            int maxDist = 0;
            int minVal = arrays[0][0];
            int maxVal = arrays[0][arrays[0].Count - 1];

            for (int i = 1; i < arrays.Count; i++) {
                int currentMin = arrays[i][0];
                int currentMax = arrays[i][arrays[i].Count - 1];

                maxDist = Math.Max(maxDist, Math.Max(Math.Abs(currentMax - minVal), Math.Abs(maxVal - currentMin)));

                minVal = Math.Min(minVal, currentMin);
                maxVal = Math.Max(maxVal, currentMax);
            }

            return maxDist;
        }
    }
}