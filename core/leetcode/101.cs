// Url:https://leetcode.com/problems/symmetric-tree

/*
101. Symmetric Tree
Easy

Given a binary tree, check whether it is a mirror of itself (ie, symmetric around its center).

For example, this binary tree [1,2,2,3,4,4,3] is symmetric:

    1
   / \
  2   2
 / \ / \
3  4 4  3


 

But the following [1,2,2,null,3,null,3] is not:

    1
   / \
  2   2
   \   \
   3    3


 

Note:
Bonus points if you could solve it both recursively and iteratively.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem101 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static bool IsSymmetric_a (TreeNode root) {
            return IsSymmetricHelper(root, root);
        }

        private static bool IsSymmetricHelper (TreeNode t1, TreeNode t2) {
            if (t1 == null && t2 == null) {
                return true;
            }

            if (t1 == null || t2 == null) {
                return false;
            }

            return (t1.val == t2.val) && IsSymmetricHelper(t1.right, t2.left) && IsSymmetricHelper(t1.left, t2.right);
        }

        public static bool IsSymmetric_b (TreeNode root) {
            Queue<TreeNode> q = new Queue<TreeNode> ();
            q.Enqueue (root);
            q.Enqueue (root);

            while (q.Count != 0) {
                TreeNode t1 = q.Dequeue ();
                TreeNode t2 = q.Dequeue ();

                if (t1 == null && t2 == null) continue;
                if (t1 == null || t2 == null) return false;
                if (t1.val != t2.val) return false;

                q.Enqueue (t1.left);
                q.Enqueue (t2.right);
                q.Enqueue (t1.right);
                q.Enqueue (t2.left);
            }

            return true;
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