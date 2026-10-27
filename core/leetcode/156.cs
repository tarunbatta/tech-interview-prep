// Url: https://leetcode.com/problems/binary-tree-upside-down

/*
156. Binary Tree Upside Down
Medium

Given the root of a binary tree, turn the tree upside down and return the new root.

You can turn a binary tree upside down with the following steps:
1. The original left child becomes the new root.
2. The original root becomes the new right child.
3. The original right child becomes the new left child.

The mentioned steps are done level by level. It is guaranteed that every right node has a sibling (a left node with the same parent) and has no children.

Example 1:
Input: root = [1,2,3,4,5]
Output: [4,5,2,null,null,3,1]

Example 2:
Input: root = []
Output: []

Example 3:
Input: root = [1]
Output: [1]

Constraints:
The number of nodes in the tree will be in the range [0, 10].
1 <= Node.val <= 10
Every right node in the tree has a sibling (a left node that shares the same parent).
Every right node in the tree has no children.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem156
{
    public class TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        public int val = val;
        public TreeNode left = left;
        public TreeNode right = right;
    }

    public class Solution
    {
        public static void Init()
        {
            TreeNode root = new(1)
            {
                left = new(2)
                {
                    left = new(4),
                    right = new(5)
                },
                right = new(3)
            };

            TreeNode newRoot = UpsideDownBinaryTree(root);
            Console.WriteLine(newRoot != null ? newRoot.val : "null"); // 4
        }

        // Time: O(n) where n is the number of nodes along the left boundary
        // Space: O(1) auxiliary space
        public static TreeNode UpsideDownBinaryTree(TreeNode root)
        {
            TreeNode curr = root;
            TreeNode prev = null;
            TreeNode next = null;
            TreeNode temp = null;

            while (curr != null)
            {
                next = curr.left;
                curr.left = temp;
                temp = curr.right;
                curr.right = prev;
                prev = curr;
                curr = next;
            }

            return prev;
        }
    }
}