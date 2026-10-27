// Url: https://leetcode.com/problems/boundary-of-binary-tree

/*
545. Boundary of Binary Tree
Medium

The boundary of a binary tree is the concatenation of the root, the left boundary, the leaves ordered from left-to-right, and the reverse order of the right boundary.

The left boundary is the set of nodes defined by the following:
- The root node's left child is in the left boundary. If the root does not have a left child, then the left boundary is empty.
- If a node is in the left boundary and has a left child, then the left child is in the left boundary.
- If a node is in the left boundary, has no left child, but has a right child, then the right child is in the left boundary.
- The leftmost leaf is not in the left boundary.

The right boundary is similar to the left boundary, except it is the right side of the root, uses right child first, and excludes the rightmost leaf.

Given the root of a binary tree, return the values of its boundary in a counter-clockwise direction starting from the root.

Example 1:
Input: root = [1,null,2,3,4]
Output: [1,3,4,2]

Example 2:
Input: root = [1,2,3,4,5,6,null,null,null,7,8,9,10]
Output: [1,2,4,7,8,9,10,6,3]

Constraints:
The number of nodes in the tree is in the range [1, 10^4].
-1000 <= Node.val <= 1000
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem545
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
        public void Init()
        {
            TreeNode root = new(1)
            {
                right = new(2)
                {
                    left = new(3),
                    right = new(4)
                }
            };

            Console.WriteLine(string.Join(", ", BoundaryOfBinaryTree(root))); // 1, 3, 4, 2
        }

        // Time: O(n) where n is the number of nodes in the tree
        // Space: O(h) where h is the tree height for the call stack
        public static IList<int> BoundaryOfBinaryTree(TreeNode root)
        {
            List<int> result = new();

            if (root == null)
            {
                return result;
            }

            if (!IsLeaf(root))
            {
                result.Add(root.val);
            }

            // Left boundary
            TreeNode curr = root.left;
            while (curr != null)
            {
                if (!IsLeaf(curr))
                {
                    result.Add(curr.val);
                }

                curr = curr.left ?? curr.right;
            }

            // Leaves
            AddLeaves(root, result);

            // Right boundary (collected and then reversed)
            Stack<int> rightBoundary = new();
            curr = root.right;
            while (curr != null)
            {
                if (!IsLeaf(curr))
                {
                    rightBoundary.Push(curr.val);
                }

                curr = curr.right ?? curr.left;
            }

            while (rightBoundary.Count > 0)
            {
                result.Add(rightBoundary.Pop());
            }

            return result;
        }

        private static bool IsLeaf(TreeNode node)
        {
            return node != null && node.left == null && node.right == null;
        }

        private static void AddLeaves(TreeNode node, List<int> result)
        {
            if (node == null)
            {
                return;
            }

            if (IsLeaf(node))
            {
                result.Add(node.val);
                return;
            }

            AddLeaves(node.left, result);
            AddLeaves(node.right, result);
        }
    }
}