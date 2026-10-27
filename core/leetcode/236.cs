// Url: https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/

/*
236. Lowest Common Ancestor of a Binary Tree
Medium

Given a binary tree, find the lowest common ancestor (LCA) of two given nodes in the tree.

According to the definition of LCA on Wikipedia: “The lowest common ancestor is defined between two nodes p and q as the lowest node in T that has both p and q as descendants (where we allow a node to be a descendant of itself).”

Given the following binary tree:  root = [3,5,1,6,2,0,8,null,null,7,4]


Example 1:

Input: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 1
Output: 3
Explanation: The LCA of nodes 5 and 1 is 3.
Example 2:

Input: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 4
Output: 5
Explanation: The LCA of nodes 5 and 4 is 5, since a node can be a descendant of itself according to the LCA definition.
 

Note:

All of the nodes' values will be unique.
p and q are different and both values will exist in the binary tree.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem236 {
    public class Solution {
        public static void Init () {
            TreeNode root = new(1)
            {
                left = new TreeNode(2),
                right = new TreeNode(3)
            };
            root.left.left = new TreeNode (4);
            root.left.right = new TreeNode (5);
            root.right.left = new TreeNode (6);
            root.right.right = new TreeNode (7);

            TreeNode r1 = LowestCommonAncestor (root, new TreeNode (4), new TreeNode (5));
            r1.DisplayNode ();

            TreeNode r2 = LowestCommonAncestor (root, new TreeNode (4), new TreeNode (6));
            r2.DisplayNode ();
        }

        // Time: O (n)
        // Space: O (n)
        public static TreeNode LowestCommonAncestor (TreeNode root, TreeNode p, TreeNode q) {
            if (root == null) {
                return null;
            }

            if (root.val == p.val || root.val == q.val) {
                return root;
            }

            TreeNode left = LowestCommonAncestor (root.left, p, q);
            TreeNode right = LowestCommonAncestor (root.right, p, q);

            if (left != null && right != null) {
                return root;
            }

            return (left != null) ? left : right;
        }
    }

    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode (int x) { val = x; }

        public void DisplayNode () {
            if (this != null) {
                Console.WriteLine (this.val);
            }
        }
    }
}