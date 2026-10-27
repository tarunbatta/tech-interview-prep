// Url:https://leetcode.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal

/*
105. Construct Binary Tree from Preorder and Inorder Traversal
Medium

Given preorder and inorder traversal of a tree, construct the binary tree.

Note:
You may assume that duplicates do not exist in the tree.

For example, given

preorder = [3,9,20,15,7]
inorder = [9,3,15,20,7]

Return the following binary tree:

    3
   / \
  9  20
    /  \
   15   7
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem105 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n)
        // Space: O (n)
        public static TreeNode BuildTree (int[] preorder, int[] inorder) {
            return Helper(0, 0, inorder.Length - 1, preorder, inorder);
        }

        // Preorder traversing implies that PRE[0] is the root node.
        // Then we can find this PRE[0] in IN, say it's IN[5].
        // Now we know that IN[5] is root, so we know that IN[0] - IN[4] is on the left side, IN[6] to the end is on the right side.
        // Recursively doing this on subarrays
        private static TreeNode Helper (int preStart, int inStart, int inEnd, int[] preorder, int[] inorder) {
            if (preStart > preorder.Length - 1 || inStart > inEnd) {
                return null;
            }

            TreeNode root = new(preorder[preStart]);
            int inIndex = GetInOrderIndex(root, inStart, inEnd, inorder);

            root.left = Helper(preStart + 1, inStart, inIndex - 1, preorder, inorder);
            root.right = Helper(preStart + inIndex - inStart + 1, inIndex + 1, inEnd, preorder, inorder);

            return root;
        }

        private static int GetInOrderIndex (TreeNode root, int inStart, int inEnd, int[] inorder) {
            int result = -1;

            for (int i = inStart; i <= inEnd; i++) {
                if (inorder[i] == root.val) {
                    result = i;
                    break;
                }
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