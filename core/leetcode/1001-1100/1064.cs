// Url:https://leetcode.com/problems/fixed-point

/*
1064. Fixed Point
Easy

Given an array of distinct integers arr sorted in ascending order, return the smallest index i that satisfies arr[i] == i. If there is no such index, return -1.

Example 1:
Input: arr = [-10,-5,0,3,7]
Output: 3
Explanation: For the given array, arr[0] = -10, arr[1] = -5, arr[2] = 0, arr[3] = 3, thus the output is 3.

Example 2:
Input: arr = [0,2,5,8,17]
Output: 0
Explanation: arr[0] = 0, thus the output is 0.

Example 3:
Input: arr = [-10,-5,3,4,7,9]
Output: -1
Explanation: There is no such i that arr[i] == i, thus the output is -1.

Constraints:
- 1 <= arr.length < 10^4
- -10^9 <= arr[i] <= 10^9
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1064
{
    public class Solution
    {
        public static void Init()
        {
            int[] arr1 = [-10, -5, 0, 3, 7];
            Console.WriteLine(FixedPoint(arr1)); // 3

            int[] arr2 = [0, 2, 5, 8, 17];
            Console.WriteLine(FixedPoint(arr2)); // 0

            int[] arr3 = [-10, -5, 3, 4, 7, 9];
            Console.WriteLine(FixedPoint(arr3)); // -1
        }

        // Time: O(log n) where n is the length of arr
        // Space: O(1)
        public static int FixedPoint(int[] arr)
        {
            int low = 0;
            int high = arr.Length - 1;
            int ans = -1;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                if (arr[mid] == mid)
                {
                    ans = mid;
                    high = mid - 1; // look for a smaller index on the left
                }
                else if (arr[mid] < mid)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }

            return ans;
        }
    }
}