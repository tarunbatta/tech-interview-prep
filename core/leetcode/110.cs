// Url:https://leetcode.com/problems/balanced-binary-tree

/*
110. Balanced Binary Tree
Easy

Given a binary tree, determine if it is height-balanced.

For this problem, a height-balanced binary tree is defined as:


a binary tree in which the depth of the two subtrees of every node never differ by more than 1.


Example 1:

Given the following tree [3,9,20,null,null,15,7]:

    3
   / \
  9  20
    /  \
   15   7

Return true.

Example 2:

Given the following tree [1,2,2,3,3,null,null,4,4]:

       1
      / \
     2   2
    / \
   3   3
  / \
 4   4


Return false.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem110 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n log n)
        // Space: O (n)
        public static bool IsBalanced_a (TreeNode root) {
            if (root == null) {
                return true;
            }

            return Math.Abs (GetTreeHeight_a(root.left) - GetTreeHeight_a(root.right)) < 2 &&
                IsBalanced_a(root.left) &&
                IsBalanced_a(root.right);
        }

        private static int GetTreeHeight_a (TreeNode root) {
            if (root == null) {
                return 0;
            }

            return Math.Max (GetTreeHeight_a(root.left), GetTreeHeight_a(root.right)) + 1;
        }

        // Time: O (n)
        // Space: O (n)
        public static bool IsBalanced_b (TreeNode root) {
            return GetTreeHeight_b(root) != -1;
        }

        private static int GetTreeHeight_b (TreeNode root) {
            if (root == null) {
                return 0;
            }

            int lh = GetTreeHeight_b(root.left);
            int rh = GetTreeHeight_b(root.right);

            if (lh == -1 || rh == -1 || Math.Abs (lh - rh) > 1) {
                return -1;
            }

            return Math.Max (GetTreeHeight_b(root.left), GetTreeHeight_b(root.right)) + 1;
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