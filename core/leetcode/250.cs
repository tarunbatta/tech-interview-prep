// Url: https://leetcode.com/problems/count-univalue-subtrees

/*
250. Count Univalue Subtrees
Medium

Given the root of a binary tree, return the number of uni-value subtrees.

A uni-value subtree means all nodes of the subtree have the same value.

Example 1:
Input: root = [5,1,5,5,5,null,5]
Output: 4

Example 2:
Input: root = []
Output: 0

Example 3:
Input: root = [5,5,5,5,5,null,5]
Output: 6

Constraints:
The number of the node in the tree will be in the range [0, 1000].
-1000 <= Node.val <= 1000
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem250
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
            TreeNode root = new(5)
            {
                left = new(1)
                {
                    left = new(5),
                    right = new(5)
                },
                right = new(5)
                {
                    right = new(5)
                }
            };

            Console.WriteLine(CountUnivalSubtrees(root)); // 4
        }

        // Time: O(n) where n is the number of nodes in the tree
        // Space: O(h) where h is the height of the tree (recursion stack)
        public static int CountUnivalSubtrees(TreeNode root)
        {
            int count = 0;
            IsUnival(root, ref count);
            return count;
        }

        private static bool IsUnival(TreeNode node, ref int count)
        {
            if (node == null)
            {
                return true;
            }

            bool isLeftUnival = IsUnival(node.left, ref count);
            bool isRightUnival = IsUnival(node.right, ref count);

            if (!isLeftUnival || !isRightUnival)
            {
                return false;
            }

            if (node.left != null && node.left.val != node.val)
            {
                return false;
            }

            if (node.right != null && node.right.val != node.val)
            {
                return false;
            }

            count++;
            return true;
        }
    }
}