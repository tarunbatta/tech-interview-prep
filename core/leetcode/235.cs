// Url: https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-search-tree/

/*
235. Lowest Common Ancestor of a Binary Search Tree
Easy

Given a binary search tree (BST), find the lowest common ancestor (LCA) of two given nodes in the BST.

According to the definition of LCA on Wikipedia: “The lowest common ancestor is defined between two nodes p and q as the lowest node in T that has both p and q as descendants (where we allow a node to be a descendant of itself).”

Given binary search tree:  root = [6,2,8,0,4,7,9,null,null,3,5]


 

Example 1:

Input: root = [6,2,8,0,4,7,9,null,null,3,5], p = 2, q = 8
Output: 6
Explanation: The LCA of nodes 2 and 8 is 6.
Example 2:

Input: root = [6,2,8,0,4,7,9,null,null,3,5], p = 2, q = 4
Output: 2
Explanation: The LCA of nodes 2 and 4 is 2, since a node can be a descendant of itself according to the LCA definition.
 

Note:

All of the nodes' values will be unique.
p and q are different and both values will exist in the BST.
*/
using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem235 {
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

    public class Solution {
        public static void Init () {
            TreeNode root = new(1);
            root = new TreeNode(20)
            {
                left = new TreeNode(8),
                right = new TreeNode(22)
            };
            root.left.left = new TreeNode (4);
            root.left.right = new TreeNode(12)
            {
                left = new TreeNode(10),
                right = new TreeNode(14)
            };

            TreeNode r1 = LowestCommonAncestor (root, new TreeNode (10), new TreeNode (14));
            r1.DisplayNode ();

            TreeNode r2 = LowestCommonAncestor (root, new TreeNode (14), new TreeNode (8));
            r2.DisplayNode ();
        }

        public static TreeNode LowestCommonAncestor (TreeNode root, TreeNode p, TreeNode q) {
            if (root == null) {
                return null;
            }

            if (root.val > p.val && root.val > q.val) {
                return LowestCommonAncestor (root.left, p, q);
            }

            if (root.val < p.val && root.val < q.val) {
                return LowestCommonAncestor (root.right, p, q);
            }

            return root;
        }
    }
}