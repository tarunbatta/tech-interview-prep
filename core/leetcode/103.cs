// Url:https://leetcode.com/problems/binary-tree-zigzag-level-order-traversal

/*
103. Binary Tree Zigzag Level Order Traversal
Medium

Given a binary tree, return the zigzag level order traversal of its nodes' values. (ie, from left to right, then right to left for the next level and alternate between).


For example:
Given binary tree [3,9,20,null,null,15,7],
    3
   / \
  9  20
    /  \
   15   7

return its zigzag level order traversal as:
[
  [3],
  [20,9],
  [15,7]
]
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem103 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n)
        // Space: O (n)
        public static IList<IList<int>> ZigzagLevelOrder (TreeNode root) {
            IList<IList<int>> result = [];

            if (root == null) {
                return result;
            }

            Queue<TreeNode> q = new Queue<TreeNode> ();
            q.Enqueue (root);
            int size = 1;
            bool order = true;

            while (q.Count > 0) {
                List<int> l = [];

                for (int i = 0; i < size; i++) {
                    TreeNode n = q.Dequeue ();
                    l.Add (n.val);

                    if (n.left != null) {
                        q.Enqueue (n.left);
                    }

                    if (n.right != null) {
                        q.Enqueue (n.right);
                    }
                }

                if (!order) {
                    l.Reverse ();
                }

                size = q.Count;
                result.Add (l);
                order = !order;
            }

            return result;
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