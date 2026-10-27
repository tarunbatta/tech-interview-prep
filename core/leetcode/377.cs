// Url:https://leetcode.com/problems/combination-sum-iv

/*
377. Combination Sum IV
Medium

Given an integer array with all positive numbers and no duplicates, find the number of possible combinations that add up to a positive integer target.

Example:

nums = [1, 2, 3]
target = 4

The possible combination ways are:
(1, 1, 1, 1)
(1, 1, 2)
(1, 2, 1)
(1, 3)
(2, 1, 1)
(2, 2)
(3, 1)

Note that different sequences are counted as different combinations.

Therefore the output is 7.
 

Follow up:
What if negative numbers are allowed in the given array?
How does it change the problem?
What limitation we need to add to the question to allow negative numbers?

Credits:
Special thanks to @pbrother for adding this problem and creating all test cases.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem377 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (CombinationSum4(null, 4));
            Console.WriteLine (CombinationSum4([1, 2, 3], 4));
        }

        public static int CombinationSum4 (int[] nums, int target) {
            if (nums == null || nums.Length == 0 || target < 0) {
                return 0;
            }

            if (target == 0) {
                return 1;
            }

            int result = 0;

            for (int i = 0; i < nums.Length; i++) {
                if (target >= nums[i]) {
                    result += CombinationSum4(nums, target - nums[i]);
                }
            }

            return result;
        }

        public static int CombinationSumTopDown (int[] nums, int target) {
            if (nums == null || nums.Length == 0 || target < 0) {
                return 0;
            }

            if (target == 0) {
                return 1;
            }

            int[] dp = new int[target + 1];

            for (int i = 0; i < dp.Length; i++) {
                dp[i] = -1;
            }

            dp[0] = 1;

            return CombinationSumTopDownHelper(nums, dp, target);
        }

        private static int CombinationSumTopDownHelper (int[] nums, int[] dp, int target) {
            if (dp[target] != -1) {
                return dp[target];
            }

            int result = 0;

            for (int i = 0; i < nums.Length; i++) {
                if (target >= nums[i]) {
                    result += CombinationSumTopDownHelper(nums, dp, target - nums[i]);
                }
            }

            dp[target] = result;
            return result;
        }

        public static int CombinationSumBottomUp (int[] nums, int target) {
            if (nums == null || nums.Length == 0 || target < 0) {
                return 0;
            }

            if (target == 0) {
                return 1;
            }

            int[] comb = new int[target + 1];
            comb[0] = 1;

            for (int i = 0; i < comb.Length; i++) {
                for (int j = 0; j < nums.Length; j++) {
                    if (i - nums[j] >= 0) {
                        comb[i] += comb[i - nums[j]];
                    }
                }
            }

            return comb[target];
        }
    }
}