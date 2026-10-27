// Url:https://leetcode.com/problems/longest-consecutive-sequence

/*
128. Longest Consecutive Sequence
Hard

Given an unsorted array of integers, find the length of the longest consecutive elements sequence.

Your algorithm should run in O(n) complexity.

Example:

Input: [100, 4, 200, 1, 3, 2]
Output: 4
Explanation: The longest consecutive elements sequence is [1, 2, 3, 4]. Therefore its length is 4.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem128 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n)
        // Space: O (n)
        public static int LongestConsecutive (int[] nums) {
            int result = 0;

            if (nums == null || nums.Length == 0) {
                return result;
            }

            HashSet<int> hash = [];
            foreach (var item in nums) {
                if (!hash.Contains (item)) {
                    hash.Add (item);
                }
            }

            for (int i = 0; i < nums.Length; i++) {
                if (!hash.Contains (nums[i] - 1)) {
                    int currentMax = 1;
                    int currentNum = nums[i];
                    while (hash.Contains (currentNum + 1)) {
                        currentMax++;
                        currentNum++;
                    }

                    result = Math.Max (result, currentMax);
                }
            }

            return result;
        }
    }
}