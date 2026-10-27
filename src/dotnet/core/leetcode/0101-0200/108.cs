using System.ComponentModel.Design;
// Url:https://leetcode.com/problems/convert-sorted-array-to-binary-search-tree

/*
108. Convert Sorted Array to Binary Search Tree
Easy

Given an array where elements are sorted in ascending order, convert it to a height balanced BST.

For this problem, a height-balanced binary tree is defined as a binary tree in which the depth of the two subtrees of every node never differ by more than 1.

Example:

Given the sorted array: [-10,-3,0,5,9],

One possible answer is: [0,-3,9,-10,null,5], which represents the following height balanced BST:

      0
     / \
   -3   9
   /   /
 -10  5
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem108 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static TreeNode SortedArrayToBST (int[] nums) {
            if (nums == null || nums.Length == 0) {
                return null;
            }

            return SortedArrayToBSTHelper(nums, 0, nums.Length - 1);
        }

        private static TreeNode SortedArrayToBSTHelper (int[] nums, int low, int high) {
            if (low > high) {
                return null;
            }

            int mid = low + (high - low) / 2;

            TreeNode node = new(nums[mid])
            {
                left = SortedArrayToBSTHelper(nums, low, mid - 1),
                right = SortedArrayToBSTHelper(nums, mid + 1, high)
            };

            return node;
        }
    }

    public class TreeNode(int x)
    {
        public int val = x;
        public TreeNode left;
        public TreeNode right;
    }
}