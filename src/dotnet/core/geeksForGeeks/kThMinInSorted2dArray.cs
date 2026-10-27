/*
Kth smallest element in a row-wise and column-wise sorted 2D array | Set 1
http://www.geeksforgeeks.org/kth-smallest-element-in-a-row-wise-and-column-wise-sorted-2d-array-set-1/

Given an n x n matrix, where every row and column is sorted in non-decreasing order. Find the kth smallest element in the given 2D array.

For example, consider the following 2D array.

10, 20, 30, 40
15, 25, 35, 45
24, 29, 37, 48
32, 33, 39, 50
The 3rd smallest element is 20 and 7th smallest element is 30
*/

using System;

namespace InterviewPreperationGuide.Core.geeksForGeeks.kThMinInSorted2dArray
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(kThSmallest(new int[4, 4] { { 10, 20, 30, 40 }, { 15, 25, 35, 45 }, { 25, 29, 37, 48 }, { 32, 33, 39, 50 },
            }, 7));
        }

        public static int kThSmallest(int[,] arr, int k)
        {
            return 0;
        }
    }
}