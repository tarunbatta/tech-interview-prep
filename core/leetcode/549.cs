// Url: https://leetcode.com/problems/binary-tree-longest-consecutive-sequence-ii

/*
549. Binary Tree Longest Consecutive Sequence II
Medium

Given the root of a binary tree, return the length of the longest consecutive path in the tree.

This path can be either increasing or decreasing.
- For example, [1,2,3,4] and [4,3,2,1] are both considered valid, but the path [1,2,4,3] is not valid.
On the other hand, the path can be in the child-Parent-child order, where not necessarily be parent-child order.

Example 1:
Input: root = [1,2,3]
Output: 2
Explanation: The longest consecutive path is [1, 2] or [2, 1].

Example 2:
Input: root = [2,1,3]
Output: 3
Explanation: The longest consecutive path is [1, 2, 3] or [3, 2, 1].

Constraints:
The number of nodes in the tree is in the range [1, 3 * 10^4].
-3 * 10^4 <= Node.val <= 3 * 10^4
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem549
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
            TreeNode root1 = new(1, new(2), new(3));
            Console.WriteLine(LongestConsecutive(root1)); // 2

            TreeNode root2 = new(2, new(1), new(3));
            Console.WriteLine(LongestConsecutive(root2)); // 3
        }

        // Time: O(n) where n is the number of nodes in the binary tree
        // Space: O(h) where h is the tree height for the recursion stack
        public static int LongestConsecutive(TreeNode root)
        {
            int maxLen = 0;
            PostOrder(root, ref maxLen);
            return maxLen;
        }

        private static (int inc, int dec) PostOrder(TreeNode node, ref int maxLen)
        {
            if (node == null)
            {
                return (0, 0);
            }

            int inc = 1;
            int dec = 1;

            if (node.left != null)
            {
                var (leftInc, leftDec) = PostOrder(node.left, ref maxLen);

                if (node.val == node.left.val + 1)
                {
                    dec = Math.Max(dec, leftDec + 1);
                }
                else if (node.val == node.left.val - 1)
                {
                    inc = Math.Max(inc, leftInc + 1);
                }
            }

            if (node.right != null)
            {
                var (rightInc, rightDec) = PostOrder(node.right, ref maxLen);

                if (node.val == node.right.val + 1)
                {
                    dec = Math.Max(dec, rightDec + 1);
                }
                else if (node.val == node.right.val - 1)
                {
                    inc = Math.Max(inc, rightInc + 1);
                }
            }

            maxLen = Math.Max(maxLen, inc + dec - 1);
            return (inc, dec);
        }
    }
}