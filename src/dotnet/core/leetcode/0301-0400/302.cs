// Url: https://leetcode.com/problems/smallest-rectangle-enclosing-black-pixels

/*
302. Smallest Rectangle Enclosing Black Pixels
Hard

You are given an m x n binary matrix image where 0 represents a white pixel and 1 represents a black pixel.

The black pixels are connected (i.e., there is only one black region). Pixels are connected horizontally and vertically.

Given two integers x and y that represents the location of one of the black pixels, return the area of the smallest (axis-aligned) rectangle that encloses all black pixels.

You must write an algorithm with less than O(mn) runtime complexity.

Example 1:
Input: image = [["0","0","1","0"],["0","1","1","0"],["0","1","0","0"]], x = 0, y = 2
Output: 6

Example 2:
Input: image = [["1"]], x = 0, y = 0
Output: 1

Constraints:
m == image.length
n == image[i].length
1 <= m, n <= 100
image[i][j] is either '0' or '1'.
0 <= x < m
0 <= y < n
image[x][y] == '1'.
The black pixels in the image only form one connected component.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem302
{
    public class Solution
    {
        public static void Init()
        {
            char[][] image =
            [
                ['0', '0', '1', '0'],
                ['0', '1', '1', '0'],
                ['0', '1', '0', '0']
            ];

            Console.WriteLine(MinArea(image, 0, 2)); // 6
        }

        // Time: O(m log n + n log m) using binary search across projected rows and columns
        // Space: O(1) auxiliary space
        public static int MinArea(char[][] image, int x, int y)
        {
            if (image == null || image.Length == 0 || image[0].Length == 0)
            {
                return 0;
            }

            int m = image.Length;
            int n = image[0].Length;

            int top = SearchRow(image, 0, x, true);
            int bottom = SearchRow(image, x, m - 1, false);
            int left = SearchCol(image, 0, y, top, bottom, true);
            int right = SearchCol(image, y, n - 1, top, bottom, false);

            return (bottom - top + 1) * (right - left + 1);
        }

        private static int SearchRow(char[][] image, int low, int high, bool findFirst)
        {
            int n = image[0].Length;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                bool hasBlack = false;

                for (int c = 0; c < n; c++)
                {
                    if (image[mid][c] == '1')
                    {
                        hasBlack = true;
                        break;
                    }
                }

                if (hasBlack == findFirst)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return findFirst ? low : high;
        }

        private static int SearchCol(char[][] image, int low, int high, int top, int bottom, bool findFirst)
        {
            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                bool hasBlack = false;

                for (int r = top; r <= bottom; r++)
                {
                    if (image[r][mid] == '1')
                    {
                        hasBlack = true;
                        break;
                    }
                }

                if (hasBlack == findFirst)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return findFirst ? low : high;
        }
    }
}