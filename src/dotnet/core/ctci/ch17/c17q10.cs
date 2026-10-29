/*
Majority Element: 

A majority element is an element that makes up more than half of the items in
an array. Given a positive integers array, find the majority element. If there is no majority element,
return -1. Do this in O(N) time and 0(1) space.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c17q10 {
    public class Solution {
        public static int FindMajority (int[] nums) {
            int candidate = GetCandidate (nums);
            return ValidateMajority (nums, candidate) ? candidate : -1;
        }

        private static int GetCandidate (int[] nums) {
            int majority = 0;
            int count = 0;
            foreach (int n in nums) {
                if (count == 0) {
                    majority = n;
                }
                if (n == majority) {
                    count++;
                } else {
                    count--;
                }
            }
            return majority;
        }

        private static bool ValidateMajority (int[] nums, int candidate) {
            int count = 0;
            foreach (int n in nums) {
                if (n == candidate) count++;
            }
            return count > nums.Length / 2;
        }

        public static void Init (string[] args = null) {
            int[] nums = { 1, 2, 5, 5, 9, 5, 5, 3, 2 };
            Console.WriteLine ($"Majority element: {FindMajority (nums)}"); // -1 (count is 4 out of 9, not > 4.5)
            int[] nums2 = { 5, 2, 5, 5, 9, 5, 5, 3, 5 };
            Console.WriteLine ($"Majority element 2: {FindMajority (nums2)}"); // 5
        }
    }
}
