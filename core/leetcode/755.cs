// Url:https://leetcode.com/problems/pour-water

/*
755. Pour Water
Medium

You are given an elevation map given as an integer array heights where heights[i] represents the height of the terrain at index i. The width at each index is 1. You are also given two integers volume and k. volume units of water will fall at index k one by one.

Water can fall at index k according to the following rules:
- First, the droplet tries to move left. It moves to the lowest index i < k such that heights[i] < heights[k] and there is no index between i and k where the terrain is higher than heights[i]. If there are multiple such indices, it chooses the lowest index closest to the left edge.
- If it cannot move left, the droplet tries to move right following the same rules.
- If it cannot move right either, the droplet stays at index k.
- In each step, if the droplet falls into a position, the height at that position increases by 1.

Return heights after volume units of water have been poured.

Example 1:
Input: heights = [2,1,1,2,1,2,2], volume = 4, k = 3
Output: [2,2,2,3,2,2,2]

Example 2:
Input: heights = [1,2,3,4], volume = 2, k = 2
Output: [2,3,3,4]

Example 3:
Input: heights = [3,1,3], volume = 5, k = 1
Output: [4,4,4]

Constraints:
- 1 <= heights.length <= 100
- 0 <= heights[i] <= 99
- 0 <= volume <= 2000
- 0 <= k < heights.length
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem755 {
    public class Solution {
        public static void Init() {
            int[] h1 = [2, 1, 1, 2, 1, 2, 2];
            Console.WriteLine(string.Join(", ", PourWater(h1, 4, 3))); // 2, 2, 2, 3, 2, 2, 2

            int[] h2 = [1, 2, 3, 4];
            Console.WriteLine(string.Join(", ", PourWater(h2, 2, 2))); // 2, 3, 3, 4

            int[] h3 = [3, 1, 3];
            Console.WriteLine(string.Join(", ", PourWater(h3, 5, 1))); // 4, 4, 4
        }

        // Time: O(volume * n) where n is length of heights
        // Space: O(1) in-place
        public static int[] PourWater(int[] heights, int volume, int k) {
            int n = heights.Length;

            for (int v = 0; v < volume; v++) {
                int best = k;

                // Try moving left
                for (int i = k - 1; i >= 0; i--) {
                    if (heights[i] > heights[i + 1]) {
                        break;
                    }
                    if (heights[i] < heights[best]) {
                        best = i;
                    }
                }

                // If no lower position found on left, try right
                if (best == k) {
                    for (int i = k + 1; i < n; i++) {
                        if (heights[i] > heights[i - 1]) {
                            break;
                        }
                        if (heights[i] < heights[best]) {
                            best = i;
                        }
                    }
                }

                heights[best]++;
            }

            return heights;
        }
    }
}