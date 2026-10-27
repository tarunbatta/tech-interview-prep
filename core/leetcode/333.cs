// Url: https://leetcode.com/problems/largest-bst-subtree

/*
333. Largest BST Subtree
Medium

Given the root of a binary tree, find the largest subtree, which is also a Binary Search Tree (BST), where the largest means subtree has the largest number of nodes.

A Binary Search Tree (BST) is a tree in which all the nodes follow the below-mentioned properties:
- The left subtree of a node contains only nodes with keys less than the node's key.
- The right subtree of a node contains only nodes with keys greater than the node's key.
- Both the left and right subtrees must also be binary search trees.

Example 1:
Input: root = [10,5,15,1,8,null,7]
Output: 3
Explanation: The Largest BST Subtree in this case is the highlighted one: [5,1,8]. The return value is the subtree's size, which is 3.

Example 2:
Input: root = [4,2,7,2,3,5,null,2,null,null,null,null,null,1]
Output: 2

Constraints:
The number of nodes in the tree is in the range [0, 10^4].
-10^4 <= Node.val <= 10^4

Follow up: Can you figure out ways to solve it with O(n) time complexity?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem333
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class Solution
    {
        public static void Init()
        {
            TreeNode root = new(10)
            {
                left = new(5)
                {
                    left = new(1),
                    right = new(8)
                },
                right = new(15)
                {
                    right = new(7)
                }
            };

            Console.WriteLine(LargestBSTSubtree(root)); // 3
        }

        // Time: O(n) where n is the number of nodes (bottom-up postorder traversal)
        // Space: O(h) where h is the tree height (recursion stack)
        public static int LargestBSTSubtree(TreeNode root)
        {
            int maxBstSize = 0;
            PostOrder(root, ref maxBstSize);
            return maxBstSize;
        }

        private static (bool isBst, int size, int min, int max) PostOrder(TreeNode node, ref int maxBstSize)
        {
            if (node == null)
            {
                return (true, 0, int.MaxValue, int.MinValue);
            }

            var left = PostOrder(node.left, ref maxBstSize);
            var right = PostOrder(node.right, ref maxBstSize);

            if (left.isBst && right.isBst &&
                (node.left == null || node.val > left.max) &&
                (node.right == null || node.val < right.min))
            {
                int currentSize = left.size + right.size + 1;
                maxBstSize = Math.Max(maxBstSize, currentSize);

                int currentMin = node.left != null ? left.min : node.val;
                int currentMax = node.right != null ? right.max : node.val;

                return (true, currentSize, currentMin, currentMax);
            }

            return (false, 0, 0, 0);
        }
    }
}