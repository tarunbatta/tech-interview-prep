// Url: https://leetcode.com/problems/contains-duplicate/

/*
217. Contains Duplicate
Easy

Given an array of integers, find if the array contains any duplicates.

Your function should return true if any value appears at least twice in the array, and it should return false if every element is distinct.

Example 1:
Input: [1,2,3,1]
Output: true

Example 2:
Input: [1,2,3,4]
Output: false

Example 3:
Input: [1,1,1,3,3,4,3,2,4,2]
Output: true
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem217 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (ContainsDuplicate(null));
            Console.WriteLine (ContainsDuplicate([1, 2, 3, 1]));
            Console.WriteLine (ContainsDuplicate([1, 2, 3, 4]));
            Console.WriteLine (ContainsDuplicate([1, 1, 1, 3, 3, 4, 3, 2, 4, 2]));
        }

        public static bool ContainsDuplicate (int[] nums) {
            bool result = false;
            if (nums == null || nums.Length <= 1) {
                return result;
            }

            Array.Sort (nums);
            int i = 1;

            while (i < nums.Length) {
                if (nums[i - 1] == nums[i]) {
                    return true;
                }

                i++;
            }

            return result;
        }
    }
}