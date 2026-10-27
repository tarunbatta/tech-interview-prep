// Url:https://leetcode.com/problems/maximum-depth-of-binary-tree

/*
104. Maximum Depth of Binary Tree
Easy

Given a binary tree, find its maximum depth.

The maximum depth is the number of nodes along the longest path from the root node down to the farthest leaf node.

Note: A leaf is a node with no children.

Example:

Given binary tree [3,9,20,null,null,15,7],

    3
   / \
  9  20
    /  \
   15   7

return its depth = 3.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem104 {
    public class Solution {
        public static void Init () {
            TreeNode node = new TreeNode (2);
            node.left = new TreeNode (3);
            node.right = new TreeNode (4);
            node.left.left = new TreeNode (4);
            node.left.right = new TreeNode (5);

            Console.WriteLine (MaxDepth(node));
        }

        public static int MaxDepth (TreeNode root) {
            if (root == null) {
                return 0;
            }

            int left = MaxDepth(root.left);
            int right = MaxDepth(root.right);

            return 1 + Math.Max (left, right);
        }
    }

    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode (int x) {
            val = x;
        }
    }
}