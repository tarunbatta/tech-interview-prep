/*
Successor: 

Write an algorithm to find the "next" node (i.e., in-order successor) of a given node in a
binary search tree. You may assume that each node has a link to its parent.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c4q6 {
    public class Solution {
        public class TreeNode {
            public int Val { get; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
            public TreeNode Parent { get; set; }

            public TreeNode (int val) {
                Val = val;
            }
        }

        public static TreeNode Successor (TreeNode n) {
            if (n == null) return null;

            if (n.Right != null) {
                return LeftMostChild (n.Right);
            } else {
                TreeNode q = n;
                TreeNode x = q.Parent;
                while (x != null && x.Left != q) {
                    q = x;
                    x = x.Parent;
                }
                return x;
            }
        }

        public static TreeNode LeftMostChild (TreeNode n) {
            if (n == null) return null;
            while (n.Left != null) {
                n = n.Left;
            }
            return n;
        }

        public static void Init (string[] args = null) {
            TreeNode a = new TreeNode (20);
            TreeNode b = new TreeNode (10);
            TreeNode c = new TreeNode (30);
            TreeNode d = new TreeNode (5);
            TreeNode e = new TreeNode (15);

            a.Left = b; b.Parent = a;
            a.Right = c; c.Parent = a;
            b.Left = d; d.Parent = b;
            b.Right = e; e.Parent = b;

            TreeNode succ = Successor (e);
            Console.WriteLine ($"Successor of 15: {succ?.Val}"); // 20
        }
    }
}
