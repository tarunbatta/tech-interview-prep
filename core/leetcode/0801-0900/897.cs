// Url:https://leetcode.com/problems/increasing-order-search-tree

/*
897. Increasing Order Search Tree
Easy

Given the root of a binary search tree, rearrange the tree in in-order so that the leftmost node in the tree is now the root of the tree, and every node has no left child and only one right child.

Example 1:
Input: root = [5,3,6,2,4,null,8,1,null,null,null,7,9]
Output: [1,null,2,null,3,null,4,null,5,null,6,null,7,null,8,null,9]

Example 2:
Input: root = [5,1,7]
Output: [1,null,5,null,7]

Constraints:
- The number of nodes in the given tree will be in the range [1, 100].
- 0 <= Node.val <= 1000
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem897 {
    public class TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        public int val = val;
        public TreeNode left = left;
        public TreeNode right = right;
    }

    public class Solution {
        public static void Init() {
            TreeNode root1 = new(5,
                new TreeNode(3, new TreeNode(2, new TreeNode(1)), new TreeNode(4)),
                new TreeNode(6, null, new TreeNode(8, new TreeNode(7), new TreeNode(9)))
            );
            TreeNode res1 = IncreasingBST(root1);
            Console.WriteLine(res1.val); // 1

            TreeNode root2 = new(5, new TreeNode(1), new TreeNode(7));
            TreeNode res2 = IncreasingBST(root2);
            Console.WriteLine(res2.val); // 1
        }

        // Time: O(n) where n is the number of nodes in the BST
        // Space: O(h) where h is the tree height for recursion stack
        public static TreeNode IncreasingBST(TreeNode root) {
            TreeNode dummy = new(0);
            TreeNode curr = dummy;
            InOrder(root, ref curr);
            return dummy.right;
        }

        private static void InOrder(TreeNode node, ref TreeNode curr) {
            if (node == null) {
                return;
            }

            InOrder(node.left, ref curr);

            node.left = null;
            curr.right = node;
            curr = node;

            InOrder(node.right, ref curr);
        }
    }
}