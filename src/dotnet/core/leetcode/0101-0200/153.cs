// Url:https://leetcode.com/problems/find-minimum-in-rotated-sorted-array

/*
153. Find Minimum in Rotated Sorted Array
Medium

Suppose an array sorted in ascending order is rotated at some pivot unknown to you beforehand.

(i.e.,  [0,1,2,4,5,6,7] might become  [4,5,6,7,0,1,2]).

Find the minimum element.

You may assume no duplicate exists in the array.

Example 1:

Input: [3,4,5,1,2] 
Output: 1


Example 2:

Input: [4,5,6,7,0,1,2]
Output: 0
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem153 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (log (n))
        // Space: O (1)
        public static int FindMin (int[] nums) {
            if (nums == null || nums.Length == 0) {
                return 0;
            }

            int left = 0;
            int right = nums.Length - 1;

            if (nums.Length == 1 || nums[right] > nums[left]) {
                return nums[0];
            }

            while (left <= right) {
                int mid = left + (right - left) / 2;

                if (nums[mid] > nums[mid + 1]) {
                    return nums[mid + 1];
                }

                if (nums[mid - 1] > nums[mid]) {
                    return nums[mid];
                }

                if (nums[left] < nums[mid]) {
                    left = mid + 1;
                } else {
                    right = mid - 1;
                }
            }

            return -1;
        }
    }
}