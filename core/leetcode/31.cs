// Url: https://leetcode.com/problems/next-permutation/

/*
31. Next Permutation
Medium

Implement next permutation, which rearranges numbers into the lexicographically next greater permutation of numbers.

If such arrangement is not possible, it must rearrange it as the lowest possible order (ie, sorted in ascending order).

The replacement must be in-place and use only constant extra memory.

Here are some examples. Inputs are in the left-hand column and its corresponding outputs are in the right-hand column.

1,2,3 → 1,3,2
3,2,1 → 1,2,3
1,1,5 → 1,5,1
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem31 {
    public class Solution {
        public static void Init() {
            NextPermutation (null);
            NextPermutation ([1, 2, 3]);
            NextPermutation ([3, 2, 1]);
            NextPermutation ([1, 1, 5]);
        }

        public static void NextPermutation (int[] nums) {
            if (nums == null || nums.Length == 0) {
                return;
            };

            int i = nums.Length - 2;

            while (i >= 0 && nums[i + 1] <= nums[i]) {
                i--;
            }

            if (i >= 0) {
                int j = nums.Length - 1;

                while (j >= 0 && nums[j] <= nums[i]) {
                    j--;
                }

                Swap(nums, i, j);
            }

            Reverse(nums, i + 1);
        }

        private static void Reverse (int[] nums, int start) {
            int i = start;
            int j = nums.Length - 1;

            while (i < j) {
                Swap(nums, i, j);
                i++;
                j--;
            }
        }
        private static void Swap (int[] nums, int i, int j) {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
    }
}