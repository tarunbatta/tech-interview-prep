// Url:https://leetcode.com/problems/path-sum-iv

/*
666. Path Sum IV
Medium

If the depth of a tree is smaller than 5, then this tree can be represented by an array of three-digit integers. For each integer:
- The hundreds digit represents the depth d of this node, 1 <= d <= 4.
- The tens digit represents the position p of this node at that level, 1 <= p <= 8. (1-indexed from left to right)
- The units digit represents the value v of this node, 0 <= v <= 9.

Given an array of ascending three-digit integers nums representing a binary tree with the depth smaller than 5, return the sum of all paths from the root towards the leaves.

It is guaranteed that the given array represents a valid connected binary tree.

Example 1:
Input: nums = [113, 215, 221]
Output: 12
Explanation: 
The tree that the list represents is:
    3
   / \
  5   1
The path sum is (3 + 5) + (3 + 1) = 12.

Example 2:
Input: nums = [113, 221]
Output: 4
Explanation: 
The tree that the list represents is: 
    3
     \
      1
The path sum is (3 + 1) = 4.

Constraints:
- 1 <= nums.length <= 15
- 110 <= nums[i] <= 489
- nums represents a valid binary tree with depth less than 5.
- nums is sorted in ascending order.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem666 {
    public class Solution {
        public void Init() {
            int[] nums1 = [113, 215, 221];
            Console.WriteLine(PathSum(nums1)); // 12

            int[] nums2 = [113, 221];
            Console.WriteLine(PathSum(nums2)); // 4
        }

        // Time: O(n) where n is the number of nodes (at most 15)
        // Space: O(n) to store the tree map
        public int PathSum(int[] nums) {
            if (nums == null || nums.Length == 0) {
                return 0;
            }

            Dictionary<(int Depth, int Pos), int> tree = [];
            foreach (int num in nums) {
                int d = num / 100;
                int p = (num % 100) / 10;
                int v = num % 10;
                tree[(d, p)] = v;
            }

            int totalSum = 0;
            Dfs(1, 1, 0, tree, ref totalSum);
            return totalSum;
        }

        private static void Dfs(int d, int p, int currentSum, Dictionary<(int Depth, int Pos), int> tree, ref int totalSum) {
            if (!tree.TryGetValue((d, p), out int val)) {
                return;
            }

            currentSum += val;

            bool hasLeft = tree.ContainsKey((d + 1, 2 * p - 1));
            bool hasRight = tree.ContainsKey((d + 1, 2 * p));

            if (!hasLeft && !hasRight) {
                totalSum += currentSum;
                return;
            }

            if (hasLeft) {
                Dfs(d + 1, 2 * p - 1, currentSum, tree, ref totalSum);
            }
            if (hasRight) {
                Dfs(d + 1, 2 * p, currentSum, tree, ref totalSum);
            }
        }
    }
}