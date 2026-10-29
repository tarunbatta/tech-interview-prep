/*
Search in Rotated Array: 

Given a sorted array of n integers that has been rotated an unknown
number of times, write code to find an element in the array. You may assume that the array was
originally sorted in increasing order.

EXAMPLE
Input: find 5 in {15, 16, 19, 20, 25, 1, 3, 4, 5, 7, 10, 14}
Output: 8 (the index of 5 in the array)
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c10q3 {
    public class Solution {
        public static int Search (int[] nums, int low, int high, int target) {
            if (high < low) return -1;
            int mid = low + (high - low) / 2;
            if (nums[mid] == target) return mid;

            // Left is normally ordered
            if (nums[low] < nums[mid]) {
                if (target >= nums[low] && target < nums[mid]) {
                    return Search (nums, low, mid - 1, target);
                } else {
                    return Search (nums, mid + 1, high, target);
                }
            }
            // Right is normally ordered
            else if (nums[mid] < nums[low]) {
                if (target > nums[mid] && target <= nums[high]) {
                    return Search (nums, mid + 1, high, target);
                } else {
                    return Search (nums, low, mid - 1, target);
                }
            }
            // Left half is all duplicates
            else {
                if (nums[mid] != nums[high]) {
                    return Search (nums, mid + 1, high, target);
                } else {
                    int result = Search (nums, low, mid - 1, target);
                    if (result != -1) return result;
                    return Search (nums, mid + 1, high, target);
                }
            }
        }

        public static void Init (string[] args = null) {
            int[] arr = { 15, 16, 19, 20, 25, 1, 3, 4, 5, 7, 10, 14 };
            Console.WriteLine ($"Index of 5: {Search (arr, 0, arr.Length - 1, 5)}"); // 8
        }
    }
}
