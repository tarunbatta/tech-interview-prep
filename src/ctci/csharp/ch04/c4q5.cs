/*
Validate BST: 

Implement a function to check if a binary tree is a binary search tree.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c4q5 {
    public class Solution {
        public class TreeNode {
            public int Val { get; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public TreeNode (int val) {
                Val = val;
            }
        }

        public static bool ValidateBst (TreeNode root) {
            return ValidateBst (root, null, null);
        }

        public static bool ValidateBst (TreeNode root, int? min, int? max) {
            if (root == null) return true;

            if ((min.HasValue && root.Val <= min.Value) || (max.HasValue && root.Val > max.Value)) {
                return false;
            }

            return ValidateBst (root.Left, min, root.Val) && ValidateBst (root.Right, root.Val, max);
        }

        public static void Init (string[] args = null) {
            TreeNode valid = new TreeNode (20) {
                Left = new TreeNode (10) { Left = new TreeNode (5), Right = new TreeNode (15) },
                Right = new TreeNode (30) { Left = new TreeNode (25), Right = new TreeNode (35) }
            };

            TreeNode invalid = new TreeNode (20) {
                Left = new TreeNode (10) { Left = new TreeNode (25) },
                Right = new TreeNode (30)
            };

            Console.WriteLine ($"Valid BST: {ValidateBst (valid)}");     // True
            Console.WriteLine ($"Invalid BST: {ValidateBst (invalid)}"); // False
        }
    }
}
