// Url:https://leetcode.com/problems/inorder-successor-in-bst

/*
285. Inorder Successor in BST
Medium

Given a binary search tree and a node in it, find the in-order successor of that node in the BST.
The successor of a node p is the node with the smallest key greater than p.val.


Example 1:
Input: root = [2,1,3], p = 1
Output: 2
Explanation: 1's in-order successor node is 2. Note that both p and the return value is of TreeNode type.

Example 2:
Input: root = [5,3,6,2,4,null,null,1], p = 6
Output: null
Explanation: There is no in-order successor of the current node, so the answer is null.

Note:
If the given node has no in-order successor in the tree, return null.
It's guaranteed that the values of the tree are unique.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem285 {
    public class Solution {
        public static void Init () {

        }

        public static TreeNode InorderSuccessor (TreeNode root, TreeNode p) {
            if (root == null)
                return null;

            TreeNode c = root;
            // save state in temp when going left instead of right
            TreeNode temp = null;

            while (c != null) {
                if (p.val > c.val) {
                    c = c.right;
                } else if (p.val < c.val) {
                    temp = c;
                    c = c.left;
                } else if (c.val == p.val) {
                    if (c.right != null) {
                        return GetMin(c.right);
                    } else {
                        return temp;
                    }
                }
            }

            return null;
        }

        private static TreeNode GetMin (TreeNode root) {
            if (root.left == null) {
                return root;
            } else {
                return GetMin(root.left);
            }

        }
    }

    public class TreeNode(int x)
    {
        public int val = x;
        public TreeNode left;
        public TreeNode right;
    }
}