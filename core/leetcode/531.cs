// Url: https://leetcode.com/problems/lonely-pixel-i

/*
531. Lonely Pixel I
Medium

Given an m x n picture consisting of black 'B' and white 'W' pixels, return the number of black lonely pixels.

A black lonely pixel is a character 'B' that located at a specific position where the same row and same column don't have any other black pixels.

Example 1:
Input: picture = [["W","W","B"],["W","B","W"],["B","W","W"]]
Output: 3
Explanation: All the three 'B's are black lonely pixels.

Example 2:
Input: picture = [["B","B","B"],["B","B","W"],["B","B","B"]]
Output: 0

Constraints:
m == picture.length
n == picture[i].length
1 <= m, n <= 500
picture[i][j] is 'W' or 'B'.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem531
{
    public class Solution
    {
        public static void Init()
        {
            char[][] pic1 =
            [
                ['W', 'W', 'B'],
                ['W', 'B', 'W'],
                ['B', 'W', 'W']
            ];
            Console.WriteLine(FindLonelyPixel(pic1)); // 3

            char[][] pic2 =
            [
                ['B', 'B', 'B'],
                ['B', 'B', 'W'],
                ['B', 'B', 'B']
            ];
            Console.WriteLine(FindLonelyPixel(pic2)); // 0
        }

        // Time: O(m * n) two-pass matrix traversal
        // Space: O(m + n) for row and column black pixel counts
        public static int FindLonelyPixel(char[][] picture)
        {
            if (picture == null || picture.Length == 0 || picture[0].Length == 0)
            {
                return 0;
            }

            int m = picture.Length;
            int n = picture[0].Length;

            int[] rowCount = new int[m];
            int[] colCount = new int[n];

            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (picture[r][c] == 'B')
                    {
                        rowCount[r]++;
                        colCount[c]++;
                    }
                }
            }

            int lonelyCount = 0;

            for (int r = 0; r < m; r++)
            {
                if (rowCount[r] == 1)
                {
                    for (int c = 0; c < n; c++)
                    {
                        if (picture[r][c] == 'B' && colCount[c] == 1)
                        {
                            lonelyCount++;
                        }
                    }
                }
            }

            return lonelyCount;
        }
    }
}