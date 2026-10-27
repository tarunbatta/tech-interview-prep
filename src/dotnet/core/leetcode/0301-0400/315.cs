// Url:https://leetcode.com/problems/count-of-smaller-numbers-after-self

/*
315. Count of Smaller Numbers After Self
Hard

You are given an integer array nums and you have to return a new counts array. The counts array has the property where counts[i] is the number of smaller elements to the right of nums[i].

Example:

Input: [5,2,6,1]
Output: [2,1,1,0] 
Explanation:
To the right of 5 there are 2 smaller elements (2 and 1).
To the right of 2 there is only 1 smaller element (1).
To the right of 6 there is 1 smaller element (1).
To the right of 1 there is 0 smaller element.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem315
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(string.Join(", ", CountSmaller([5, 2, 6, 1]))); // Expected: 2, 1, 1, 0
            Console.WriteLine(string.Join(", ", CountSmaller([-1])));          // Expected: 0
            Console.WriteLine(string.Join(", ", CountSmaller([-1, -1])));      // Expected: 0, 0
        }

        // Time: O(n log n) where n is the number of elements
        // Space: O(n) auxiliary space for merge sort and tracking indices
        public static IList<int> CountSmaller(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return [];
            }

            int n = nums.Length;
            int[] counts = new int[n];
            (int val, int originalIndex)[] items = new (int, int)[n];

            for (int i = 0; i < n; i++)
            {
                items[i] = (nums[i], i);
            }

            (int val, int originalIndex)[] temp = new (int, int)[n];
            MergeSort(items, temp, counts, 0, n - 1);

            return counts;
        }

        private static void MergeSort((int val, int originalIndex)[] items, (int val, int originalIndex)[] temp, int[] counts, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            int mid = left + (right - left) / 2;
            MergeSort(items, temp, counts, left, mid);
            MergeSort(items, temp, counts, mid + 1, right);
            Merge(items, temp, counts, left, mid, right);
        }

        private static void Merge((int val, int originalIndex)[] items, (int val, int originalIndex)[] temp, int[] counts, int left, int mid, int right)
        {
            int i = left;
            int j = mid + 1;
            int k = left;
            int rightCounter = 0;

            while (i <= mid && j <= right)
            {
                if (items[j].val < items[i].val)
                {
                    rightCounter++;
                    temp[k++] = items[j++];
                }
                else
                {
                    counts[items[i].originalIndex] += rightCounter;
                    temp[k++] = items[i++];
                }
            }

            while (i <= mid)
            {
                counts[items[i].originalIndex] += rightCounter;
                temp[k++] = items[i++];
            }

            while (j <= right)
            {
                temp[k++] = items[j++];
            }

            for (int p = left; p <= right; p++)
            {
                items[p] = temp[p];
            }
        }
    }
}