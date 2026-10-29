/*
Contiguous Sequence:
You are given an array of integers (both positive and negative).
Find the contiguous sequence with the largest sum.
Return the sum.
    ///
EXAMPLE
Input: 2, -8, 3, -2, 4, -10
Output: 5 (i.e., { 3, -2, 4})
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c16q17 {
    public class Solution {
        public static void Init (string[] args = null) {
            Console.WriteLine (GetLargesttSum ([2, -8, 3, -2, 4, -10]));

            Console.WriteLine (GetLargesttSum ([-2, -3, 4, -1, -2, 1, 5, -3]));
        }

        // Kadane's algorithm
        public static int GetLargesttSum (int[] nums) {
            int result = 0;
            int maxSum = 0;

            if (nums != null && nums.Length > 0) {
                for (int i = 0; i < nums.Length; i++) {
                    maxSum = maxSum + nums[i];

                    if (maxSum < 0) {
                        maxSum = 0;
                    }

                    if (result < maxSum) {
                        result = maxSum;
                    }
                }
            }

            return result;
        }
    }
}