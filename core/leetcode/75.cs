// Url:https://leetcode.com/problems/sort-colors

/*
75. Sort Colors
Medium

Given an array with n objects colored red, white or blue, sort them in-place so that objects of the same color are adjacent, with the colors in the order red, white and blue.

Here, we will use the integers 0, 1, and 2 to represent the color red, white, and blue respectively.

Note: You are not suppose to use the library's sort function for this problem.

Example:

Input: [2,0,2,1,1,0]
Output: [0,0,1,1,2,2]

Follow up:


	A rather straight forward solution is a two-pass algorithm using counting sort.
	First, iterate the array counting number of 0's, 1's, and 2's, then overwrite array with total number of 0's, then 1's and followed by 2's.
	Could you come up with a one-pass algorithm using only constant space?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem75 {
    public class Solution {
        public static void Init() {
            SortColors(null);
            SortColors([2, 0, 2, 1, 1, 0]);
        }

        public static void SortColors (int[] nums) {
            if (nums == null || nums.Length < 2) {
                return;
            }

            int p1 = 0;
            int p2 = nums.Length - 1;
            int current = 0;

            while (current <= p2) {
                if (nums[current] == 0) {
                    Swap(nums, p1, current);
                    p1++;
                    current++;
                } else if (nums[current] == 2) {
                    Swap(nums, current, p2);
                    p2--;
                } else {
                    current++;
                }
            }
        }

        private static void Swap (int[] nums, int i, int j) {
            if (i != j) {
                int temp = nums[i];
                nums[i] = nums[j];
                nums[j] = temp;
            }
        }
    }
}