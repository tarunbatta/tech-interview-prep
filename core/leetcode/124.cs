// Url:https://leetcode.com/problems/binary-tree-maximum-path-sum

/*
124. Binary Tree Maximum Path Sum
Hard

Given a non-empty binary tree, find the maximum path sum.

For this problem, a path is defined as any sequence of nodes from some starting node to any node in the tree along the parent-child connections. The path must contain at least one node and does not need to go through the root.

Example 1:

Input: [1,2,3]

       1
      / \
     2   3

Output: 6


Example 2:

Input: [-10,9,20,null,null,15,7]

   -10
   / \
  9  20
    /  \
   15   7

Output: 42
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem124 {
    public class Solution {
        public static void Init() {
            TreeNode node = new(10)
            {
                left = new(2),
                right = new(10)
            };
            node.left.left = new(20);
            node.left.right = new(1);
            node.right.right = new(-25)
            {
                left = new(3),
                right = new(4)
            };

            Console.WriteLine (MaxPathSum (node));
        }

        private static int result = int.MinValue;

        // Time: O (n)
        // Space: O (log n)
        public static int MaxPathSum (TreeNode root) {
            result = int.MinValue;
            MaxPathSumUtility (root);
            return result;
        }

        private static int MaxPathSumUtility (TreeNode root) {
            if (root == null) {
                return 0;
            }

            int left = Math.Max (MaxPathSumUtility (root.left), 0);
            int right = Math.Max (MaxPathSumUtility (root.right), 0);

            result = Math.Max (result, root.val + left + right);

            return root.val + Math.Max (left, right);
        }
    }

    public class TreeNode(int x)
    {
        public int val = x;
        public TreeNode left;
        public TreeNode right;
    }
}