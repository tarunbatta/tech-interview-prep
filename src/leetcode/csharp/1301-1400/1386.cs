// Url: https://leetcode.com/problems/cinema-seat-allocation/

/*
1386. Cinema Seat Allocation
Medium

A cinema has n rows of seats, numbered from 1 to n and there are 10 seats in each row, labelled from 1 to 10 as shown in the figure above.
Given the array reservedSeats containing the numbers of seats and rows of the already reserved seats, return the maximum number of four-person groups you can allocate.
Four-person groups can only sit in 4 contiguous seats in one of these configurations:
- Seats 2, 3, 4, 5
- Seats 6, 7, 8, 9
- Seats 4, 5, 6, 7 (across the aisle)

Example 1:
Input: n = 3, reservedSeats = [[1,2],[1,3],[1,8],[2,6],[3,1],[3,10]]
Output: 4
Explanation:
Row 1: seats 4,5,6,7 can fit 1 family.
Row 2: seats 2,3,4,5 can fit 1 family.
Row 3: seats 2,3,4,5 and 6,7,8,9 can fit 2 families.
Total = 4.

Example 2:
Input: n = 2, reservedSeats = [[2,1],[1,8],[2,6]]
Output: 2

Example 3:
Input: n = 4, reservedSeats = [[4,3],[1,4],[4,6],[1,7]]
Output: 4
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem1386
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(MaxNumberOfFamilies(3, [
                [1, 2], [1, 3], [1, 8],
                [2, 6],
                [3, 1], [3, 10]
            ])); // Expected: 4

            Console.WriteLine(MaxNumberOfFamilies(2, [
                [2, 1], [1, 8], [2, 6]
            ])); // Expected: 2

            Console.WriteLine(MaxNumberOfFamilies(4, [
                [4, 3], [1, 4], [4, 6], [1, 7]
            ])); // Expected: 4
        }

        // Time: O(m) where m is the number of reserved seats
        // Space: O(k) where k is the number of rows with reserved seats (at most m)
        public static int MaxNumberOfFamilies(int n, int[][] reservedSeats)
        {
            if (n <= 0)
            {
                return 0;
            }

            Dictionary<int, int> rowMasks = [];

            if (reservedSeats != null)
            {
                foreach (int[] seat in reservedSeats)
                {
                    int row = seat[0];
                    int col = seat[1];

                    if (!rowMasks.ContainsKey(row))
                    {
                        rowMasks[row] = 0;
                    }

                    rowMasks[row] |= (1 << col);
                }
            }

            const int leftMask = (1 << 2) | (1 << 3) | (1 << 4) | (1 << 5);
            const int rightMask = (1 << 6) | (1 << 7) | (1 << 8) | (1 << 9);
            const int middleMask = (1 << 4) | (1 << 5) | (1 << 6) | (1 << 7);

            // Empty rows each fit 2 families
            int maxFamilies = (n - rowMasks.Count) * 2;

            foreach (var (_, mask) in rowMasks)
            {
                bool leftFree = (mask & leftMask) == 0;
                bool rightFree = (mask & rightMask) == 0;
                bool middleFree = (mask & middleMask) == 0;

                if (leftFree && rightFree)
                {
                    maxFamilies += 2;
                }
                else if (leftFree || rightFree || middleFree)
                {
                    maxFamilies += 1;
                }
            }

            return maxFamilies;
        }
    }
}
