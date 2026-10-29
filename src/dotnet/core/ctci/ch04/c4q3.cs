/*
List of Depths: 

Given a binary tree, design an algorithm which creates a linked list of all the nodes
at each depth (e.g., if you have a tree with depth D, you'll have D linked lists).
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c4q3 {
    public class Solution {
        public class TreeNode {
            public int Val { get; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public TreeNode (int val) {
                Val = val;
            }
        }

        public static List<LinkedList<TreeNode>> CreateList (TreeNode root) {
            List<LinkedList<TreeNode>> res = new List<LinkedList<TreeNode>> ();
            if (root == null) return res;

            LinkedList<TreeNode> current = new LinkedList<TreeNode> ();
            current.AddLast (root);

            while (current.Count > 0) {
                res.Add (current);
                LinkedList<TreeNode> parents = current;
                current = new LinkedList<TreeNode> ();
                foreach (TreeNode parent in parents) {
                    if (parent.Left != null) current.AddLast (parent.Left);
                    if (parent.Right != null) current.AddLast (parent.Right);
                }
            }

            return res;
        }

        public static void Init (string[] args = null) {
            TreeNode root = new TreeNode (1) {
                Left = new TreeNode (2) {
                    Left = new TreeNode (4),
                    Right = new TreeNode (5)
                },
                Right = new TreeNode (3) {
                    Left = new TreeNode (6)
                }
            };

            List<LinkedList<TreeNode>> lists = CreateList (root);
            Console.WriteLine ($"Total Depths: {lists.Count}");
            for (int i = 0; i < lists.Count; i++) {
                Console.Write ($"Depth {i}: ");
                foreach (TreeNode n in lists[i]) {
                    Console.Write (n.Val + " ");
                }
                Console.WriteLine ();
            }
        }
    }
}
