// Url:https://leetcode.com/problems/4sum

/*
18. 4Sum
Medium

Given an array nums of n integers and an integer target, are there elements a, b, c, and d in nums such that a + b + c + d = target? Find all unique quadruplets in the array which gives the sum of target.

Note:

The solution set must not contain duplicate quadruplets.

Example:

Given array nums = [1, 0, -1, 0, -2, 2], and target = 0.

A solution set is:
[
  [-1,  0, 0, 1],
  [-2, -1, 1, 2],
  [-2,  0, 0, 2]
]
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem18 {
    public class Solution {
        public void Init () {
            Console.WriteLine (FourSum(null, 0));
            Console.WriteLine (FourSum([1, 0, -1], 0));
            Console.WriteLine (FourSum([1, 0, -1, 0, -2, 2], 0));
        }

        public static IList<IList<int>> FourSum (int[] nums, int target) {
            IList<IList<int>> result = [];

            if (nums != null && nums.Length >= 4) {
                Array.Sort (nums);
                Dictionary<string, List<int>> dict = [];

                for (int i = 0; i < nums.Length - 3; i++) {
                    for (int j = i + 1; j < nums.Length - 2; j++) {
                        int k = j + 1;
                        int l = nums.Length - 1;

                        while (k < l) {
                            int sum = nums[i] + nums[j] + nums[k] + nums[l];

                            if (sum > target) {
                                l--;
                            } else if (sum < target) {
                                k++;
                            } else {
                                string key = string.Concat (nums[i], ",", nums[j], ",", nums[k], ",", nums[l]);

                                List<int> val = [nums[i], nums[j], nums[k], nums[l]];

                                if (dict.TryAdd(key, val)) {
                                    result.Add (val);
                                }

                                k++;
                                l--;
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}