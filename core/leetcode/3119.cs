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
            Console.WriteLine(MaxPotholes(null, 7));                     // Expected: 0
            Console.WriteLine(MaxPotholes("..xxx..x....xxx.", 7));      // Expected: 5
            Console.WriteLine(MaxPotholes("x.x.xxx...x", 14));          // Expected: 6
            Console.WriteLine(MaxPotholes("..", 5));                    // Expected: 0
            Console.WriteLine(MaxPotholes("xxx", 2));                   // Expected: 1 (cost 1 + 1 = 2)
            Console.WriteLine(MaxPotholes("xxx", 1));                   // Expected: 0 (budget <= 1 cannot fix any)
        }

        // Time: O(n log n) with sorting (or O(n) with bucket sort)
        // Space: O(n) to store segment lengths
        public static int MaxPotholes(string road, int budget)
        {
            if (string.IsNullOrEmpty(road) || budget <= 1)
            {
                return 0;
            }

            List<int> segments = [];
            int currentLength = 0;

            foreach (char c in road)
            {
                if (c == 'x')
                {
                    currentLength++;
                }
                else if (currentLength > 0)
                {
                    segments.Add(currentLength);
                    currentLength = 0;
                }
            }

            if (currentLength > 0)
            {
                segments.Add(currentLength);
            }

            segments.Sort((a, b) => b.CompareTo(a));

            int totalFixed = 0;

            foreach (int len in segments)
            {
                if (budget <= 1)
                {
                    break;
                }

                int canFix = Math.Min(len, budget - 1);
                totalFixed += canFix;
                budget -= canFix + 1;
            }

            return totalFixed;
        }
    }
}
