/*
Check Balanced: 

Implement a function to check if a binary tree is balanced. For the purposes of
this question, a balanced tree is defined to be a tree such that the heights of the two subtrees of any
node never differ by more than one.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c4q4 {
    public class Solution {
        public class TreeNode {
            public int Val { get; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public TreeNode (int val) {
                Val = val;
            }
        }

        public static int CheckHeight (TreeNode root) {
            if (root == null) return 0;

            int left = CheckHeight (root.Left);
            if (left == -1) return -1;

            int right = CheckHeight (root.Right);
            if (right == -1) return -1;

            int diff = Math.Abs (left - right);
            if (diff > 1) return -1;
            return Math.Max (left, right) + 1;
        }

        public static bool IsBalanced (TreeNode root) {
            return CheckHeight (root) != -1;
        }

        public static void Init (string[] args = null) {
            TreeNode balanced = new TreeNode (1) {
                Left = new TreeNode (2),
                Right = new TreeNode (3)
            };
            TreeNode unbalanced = new TreeNode (1) {
                Left = new TreeNode (2) {
                    Left = new TreeNode (3)
                }
            };

            Console.WriteLine ($"Tree 1 Balanced: {IsBalanced (balanced)}"); // True
            Console.WriteLine ($"Tree 2 Balanced: {IsBalanced (unbalanced)}"); // False
        }
    }
}
