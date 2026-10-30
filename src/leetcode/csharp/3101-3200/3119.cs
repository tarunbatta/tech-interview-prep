// Url: https://leetcode.com/problems/maximum-number-of-potholes-that-can-be-fixed/

/*
3119. Maximum Number of Potholes That Can Be Fixed
Medium

You are given a string road consisting of characters 'x' and '.', where:
- 'x' represents a pothole.
- '.' represents a smooth road segment.

You are also given an integer budget.
In each operation, you can choose a contiguous segment of potholes and fix them.
Fixing k contiguous potholes costs k + 1 budget.

Return the maximum number of potholes that can be fixed within the given budget.

Example 1:
Input: road = "..xxx..x....xxx.", budget = 7
Output: 5
Explanation:
- Fix 3 contiguous potholes in the first segment: cost = 3 + 1 = 4. Remaining budget = 3.
- Fix 2 potholes in the third segment: cost = 2 + 1 = 3. Remaining budget = 0.
Total fixed potholes = 3 + 2 = 5.

Example 2:
Input: road = "x.x.xxx...x", budget = 14
Output: 6

Example 3:
Input: road = "..", budget = 5
Output: 0
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem3119
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(MaxPotholes(null, 7));                    // Expected: 0
            Console.WriteLine(MaxPotholes("..xxx..x....xxx.", 7));      // Expected: 5
            Console.WriteLine(MaxPotholes("x.x.xxx...x", 14));          // Expected: 6
            Console.WriteLine(MaxPotholes("..", 5));                    // Expected: 0
            Console.WriteLine(MaxPotholes("xxx", 2));                   // Expected: 1 (cost 1 + 1 = 2)
            Console.WriteLine(MaxPotholes("xxx", 1));                   // Expected: 0 (budget <= 1 cannot fix any)
        }

        // Time: O(n) using bucket / counting sort
        // Space: O(n) for frequency count array
        public static int MaxPotholes(string road, int budget)
        {
            if (string.IsNullOrEmpty(road) || budget <= 1)
            {
                return 0;
            }

            int[] count = new int[road.Length + 1];
            int currentSegmentLength = 0;

            foreach (var c in road)
            {
                if (c == 'x')
                {
                    currentSegmentLength++;
                }
                else if (currentSegmentLength > 0)
                {
                    count[currentSegmentLength]++;
                    currentSegmentLength = 0;
                }
            }

            if (currentSegmentLength > 0)
            {
                count[currentSegmentLength]++;
            }

            int totalFixed = 0;

            for (int len = road.Length; len > 0; len--)
            {
                if (count[len] == 0)
                {
                    continue;
                }

                int costPerSegment = len + 1;
                int fullSegments = Math.Min(count[len], budget / costPerSegment);

                totalFixed += fullSegments * len;
                budget -= fullSegments * costPerSegment;
                count[len] -= fullSegments;

                if (count[len] > 0 && budget > 1)
                {
                    totalFixed += Math.Min(len, budget - 1);
                    break;
                }

                if (budget <= 1)
                {
                    break;
                }
            }

            return totalFixed;
        }
    }
}
