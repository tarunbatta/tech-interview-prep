// Url:https://leetcode.com/problems/diameter-of-binary-tree

/*
543. Diameter of Binary Tree
Easy

Given a binary tree, you need to compute the length of the diameter of the tree. The diameter of a binary tree is the length of the longest path between any two nodes in a tree. This path may or may not pass through the root.



Example:
Given a binary tree 
          1
         / \
        2   3
       / \     
      4   5    



Return 3, which is the length of the path [4,2,1,3] or [5,2,1,3].


Note:
The length of path between two nodes is represented by the number of edges between them.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem543 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        int max = 0;

        // Time: O (n)
        // Space: O (n)
        public int DiameterOfBinaryTree (TreeNode root) {
            maxDepth (root);
            return max;
        }

        private int maxDepth (TreeNode root) {
            if (root == null) return 0;

            int left = maxDepth (root.left);
            int right = maxDepth (root.right);

            max = Math.Max (max, left + right);

            return Math.Max (left, right) + 1;
        }
    }

    public class TreeNode(int x)
    {
        public int val = x;
        public TreeNode left;
        public TreeNode right;
    }
}