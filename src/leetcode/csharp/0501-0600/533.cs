// Url: https://leetcode.com/problems/lonely-pixel-ii

/*
533. Lonely Pixel II
Medium

Given an m x n picture consisting of black 'B' and white 'W' pixels and an integer target, return the number of black lonely pixels.

A black lonely pixel is a character 'B' at (r, c) such that:
- Row r and column c both contain exactly target black pixels.
- All the rows that have a black pixel in column c have the same row content.

Example 1:
Input: picture = [["W","B","W","B","B","W"],["W","B","W","B","B","W"],["W","B","W","B","B","W"],["W","W","B","W","B","W"]], target = 3
Output: 6
Explanation: All the green 'B's are black lonely pixels.
- The rows 0, 1, 2 have the same row content: "WBWBBW".
- For col 1, row 0, 1, 2 all have 'B', and col 1 has 3 'B's. So (0,1), (1,1), (2,1) are lonely.
- For col 3, row 0, 1, 2 all have 'B', and col 3 has 3 'B's. So (0,3), (1,3), (2,3) are lonely.
- Total is 6.

Example 2:
Input: picture = [["W","W","B"],["W","W","B"],["W","W","B"]], target = 1
Output: 0

Constraints:
m == picture.length
n == picture[i].length
1 <= m, n <= 200
picture[i][j] is 'W' or 'B'.
1 <= target <= min(m, n)
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem533
{
    public class Solution
    {
        public static void Init()
        {
            char[][] pic =
            [
                ['W', 'B', 'W', 'B', 'B', 'W'],
                ['W', 'B', 'W', 'B', 'B', 'W'],
                ['W', 'B', 'W', 'B', 'B', 'W'],
                ['W', 'W', 'B', 'W', 'B', 'W']
            ];

            Console.WriteLine(FindBlackPixel(pic, 3)); // 6
        }

        // Time: O(m * n) where m is rows and n is columns
        // Space: O(m * n) for storing row strings and frequency counts
        public static int FindBlackPixel(char[][] picture, int target)
        {
            if (picture == null || picture.Length == 0 || picture[0].Length == 0 || target <= 0)
            {
                return 0;
            }

            int m = picture.Length;
            int n = picture[0].Length;

            int[] colCount = new int[n];
            Dictionary<string, int> rowFreq = [];
            string[] rowStrings = new string[m];

            for (int r = 0; r < m; r++)
            {
                int rowBCount = 0;
                char[] chars = new char[n];

                for (int c = 0; c < n; c++)
                {
                    chars[c] = picture[r][c];

                    if (picture[r][c] == 'B')
                    {
                        rowBCount++;
                        colCount[c]++;
                    }
                }

                string rowStr = new(chars);
                rowStrings[r] = rowStr;

                if (rowBCount == target)
                {
                    rowFreq[rowStr] = rowFreq.GetValueOrDefault(rowStr, 0) + 1;
                }
            }

            int lonelyCount = 0;

            for (int r = 0; r < m; r++)
            {
                string rowStr = rowStrings[r];

                if (rowFreq.TryGetValue(rowStr, out int count) && count >= target)
                {
                    for (int c = 0; c < n; c++)
                    {
                        if (picture[r][c] == 'B' && colCount[c] == target)
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