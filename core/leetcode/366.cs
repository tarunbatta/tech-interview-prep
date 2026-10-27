// Url: https://leetcode.com/problems/find-leaves-of-binary-tree

/*
366. Find Leaves of Binary Tree
Medium

Given the root of a binary tree, collect a tree's nodes as if you were doing this:
- Collect all the leaf nodes.
- Remove all the leaf nodes.
- Repeat until the tree is empty.

Example 1:
Input: root = [1,2,3,4,5]
Output: [[4,5,3],[2],[1]]
Explanation:
[[3,5,4],[2],[1]] and [[3,4,5],[2],[1]] are also considered correct answers since per each level the order does not matter.

Example 2:
Input: root = [1]
Output: [[1]]

Constraints:
The number of nodes in the tree is in the range [1, 100].
-100 <= Node.val <= 100
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem366
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
                left = new(2)
                {
                    left = new(4),
                    right = new(5)
                },
                right = new(3)
            };

            var res = FindLeaves(root);
            foreach (var level in res)
            {
                Console.WriteLine($"[{string.Join(", ", level)}]");
            }
        }

        // Time: O(n) where n is the number of nodes in the tree
        // Space: O(h) where h is the tree height (recursion stack)
        public static IList<IList<int>> FindLeaves(TreeNode root)
        {
            List<IList<int>> result = new();
            GetHeight(root, result);
            return result;
        }

        private static int GetHeight(TreeNode node, List<IList<int>> result)
        {
            if (node == null)
            {
                return -1;
            }

            int height = 1 + Math.Max(GetHeight(node.left, result), GetHeight(node.right, result));

            if (result.Count == height)
            {
                result.Add(new List<int>());
            }

            result[height].Add(node.val);

            return height;
        }
    }
}