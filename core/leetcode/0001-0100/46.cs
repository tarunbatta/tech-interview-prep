// Url: https://leetcode.com/problems/permutations/

/*
46. Permutations
Medium

Given a collection of distinct integers, return all possible permutations.

Example:

Input: [1,2,3]
Output:
[
  [1,2,3],
  [1,3,2],
  [2,1,3],
  [2,3,1],
  [3,1,2],
  [3,2,1]
]
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem46 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (Permute(null));
            Console.WriteLine (Permute([1]));
            Console.WriteLine (Permute([1, 2]));
            Console.WriteLine (Permute([1, 2, 3]));
        }

        public static IList<IList<int>> Permute (int[] nums) {
            IList<IList<int>> result = null;

            if (nums == null || nums.Length == 0) {
                return result;
            }

            result = [];

            DFS(nums, result, []);

            return result;
        }

        public static void DFS (int[] nums, IList<IList<int>> result, List<int> current) {
            if (current.Count == nums.Length) {
                result.Add (current);
                return;
            }

            for (int i = 0; i < nums.Length; i++) {
                if (!current.Contains (nums[i])) {
                    List<int> newList = [.. current, nums[i]];
                    DFS(nums, result, newList);
                }
            }
        }
    }
}