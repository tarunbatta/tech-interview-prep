/*
First Common Ancestor: 

Design an algorithm and write code to find the first common ancestor
of two nodes in a binary tree. Avoid storing additional nodes in a data structure. NOTE: This is not
necessarily a binary search tree.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c4q8 {
    public class Solution {
        public class TreeNode {
            public int Val { get; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public TreeNode (int val) {
                Val = val;
            }
        }

        public static TreeNode CommonAncestor (TreeNode root, TreeNode p, TreeNode q) {
            if (root == null || p == null || q == null) return null;
            if (root == p || root == q) return root;

            TreeNode left = CommonAncestor (root.Left, p, q);
            TreeNode right = CommonAncestor (root.Right, p, q);

            if (left != null && right != null) return root;
            return left ?? right;
        }

        public static void Init (string[] args = null) {
            TreeNode a = new TreeNode (20);
            TreeNode b = new TreeNode (10);
            TreeNode c = new TreeNode (30);
            TreeNode d = new TreeNode (5);
            TreeNode e = new TreeNode (15);
            TreeNode f = new TreeNode (3);
            TreeNode g = new TreeNode (7);
            TreeNode h = new TreeNode (17);

            a.Left = b; a.Right = c;
            b.Left = d; b.Right = e;
            d.Left = f; d.Right = g;
            e.Right = h;

            TreeNode ancestor = CommonAncestor (a, g, h);
            Console.WriteLine ($"LCA of 7 and 17: {ancestor?.Val}"); // 10
        }
    }
}
