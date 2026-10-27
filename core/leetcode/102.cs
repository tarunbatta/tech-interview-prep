// Url:https://leetcode.com/problems/binary-tree-level-order-traversal

/*
102. Binary Tree Level Order Traversal
Medium

Given a binary tree, return the level order traversal of its nodes' values. (ie, from left to right, level by level).


For example:
Given binary tree [3,9,20,null,null,15,7],
    3
   / \
  9  20
    /  \
   15   7



return its level order traversal as:
[
  [3],
  [9,20],
  [15,7]
]
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem102 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static IList<IList<int>> LevelOrder_a (TreeNode root) {
            var result = new List<IList<int>> ();
            LevelOrderHelper(root, result, 0);

            return result;
        }

        private static void LevelOrderHelper (TreeNode root, List<IList<int>> result, int height) {
            if (root == null) {
                return;
            }

            if (height >= result.Count) {
                result.Add ([]);
            }

            result[height].Add (root.val);

            LevelOrderHelper(root.left, result, height + 1);
            LevelOrderHelper(root.right, result, height + 1);
        }

        public static IList<IList<int>> LevelOrder_b (TreeNode root) {
            var result = new List<IList<int>> ();

            if (root == null) {
                return result;
            }

            var queue = new Queue<TreeNode> ();
            queue.Enqueue (root);

            while (queue.Count != 0) {
                int height = queue.Count;

                List<int> sublist = [];

                for (int i = 0; i < height; i++) {
                    if (queue.Peek ().left != null) {
                        queue.Enqueue (queue.Peek ().left);
                    }

                    if (queue.Peek ().right != null) {
                        queue.Enqueue (queue.Peek ().right);
                    }

                    sublist.Add (queue.Dequeue ().val);
                }

                result.Add (sublist);
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