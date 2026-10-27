// Url: https://leetcode.com/problems/binary-tree-longest-consecutive-sequence

/*
298. Binary Tree Longest Consecutive Sequence
Medium

Given the root of a binary tree, return the length of the longest consecutive sequence path.

The path refers to any sequence of nodes from some starting node to any node in the tree along the parent-child connections. The longest consecutive path needs to be from parent to child (cannot be the reverse). Consecutive means each child value is parent value + 1.

Example 1:
Input: root = [1,null,3,2,4,null,null,null,5]
Output: 3
Explanation: Longest consecutive sequence path is 3-4-5, so return 3.

Example 2:
Input: root = [2,null,3,2,null,1]
Output: 2
Explanation: Longest consecutive sequence path is 2-3, not 3-2-1, so return 2.

Constraints:
The number of nodes in the tree is in the range [1, 3 * 10^4].
-3 * 10^4 <= Node.val <= 3 * 10^4
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem298
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
                right = new(3)
                {
                    left = new(2),
                    right = new(4)
                    {
                        right = new(5)
                    }
                }
            };

            Console.WriteLine(LongestConsecutive(root)); // 3
        }

        // Time: O(n) where n is the number of nodes in the tree
        // Space: O(h) where h is the height of the tree (recursion stack)
        public static int LongestConsecutive(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            int maxLength = 0;
            Dfs(root, null, 0, ref maxLength);
            return maxLength;
        }

        private static void Dfs(TreeNode node, TreeNode parent, int length, ref int maxLength)
        {
            if (node == null)
            {
                return;
            }

            length = (parent != null && node.val == parent.val + 1) ? length + 1 : 1;
            maxLength = Math.Max(maxLength, length);

            Dfs(node.left, node, length, ref maxLength);
            Dfs(node.right, node, length, ref maxLength);
        }
    }
}