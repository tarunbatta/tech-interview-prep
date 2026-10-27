// Url: https://leetcode.com/problems/binary-tree-vertical-order-traversal

/*
314. Binary Tree Vertical Order Traversal
Medium

Given the root of a binary tree, return the vertical order traversal of its nodes' values. (i.e., from top to bottom, column by column).

If two nodes are in the same row and column, the order should be from left to right.

Example 1:
Input: root = [3,9,20,null,null,15,7]
Output: [[9],[3,15],[20],[7]]

Example 2:
Input: root = [3,9,8,4,0,1,7]
Output: [[4],[9],[3,0,1],[8],[7]]

Constraints:
The number of nodes in the tree is in the range [0, 100].
-100 <= Node.val <= 100
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem314
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
            TreeNode root = new(3)
            {
                left = new(9),
                right = new(20)
                {
                    left = new(15),
                    right = new(7)
                }
            };

            var res = VerticalOrder(root);
            foreach (var col in res)
            {
                Console.WriteLine($"[{string.Join(", ", col)}]");
            }
        }

        // Time: O(n) where n is the number of nodes in the tree
        // Space: O(n) for BFS queue and column hash map
        public static IList<IList<int>> VerticalOrder(TreeNode root)
        {
            List<IList<int>> result = new();

            if (root == null)
            {
                return result;
            }

            Dictionary<int, List<int>> colMap = new();
            Queue<(TreeNode node, int col)> queue = new();
            queue.Enqueue((root, 0));

            int minCol = 0;
            int maxCol = 0;

            while (queue.Count > 0)
            {
                var (node, col) = queue.Dequeue();

                if (!colMap.TryGetValue(col, out List<int> list))
                {
                    list = new List<int>();
                    colMap[col] = list;
                }

                list.Add(node.val);
                minCol = Math.Min(minCol, col);
                maxCol = Math.Max(maxCol, col);

                if (node.left != null)
                {
                    queue.Enqueue((node.left, col - 1));
                }

                if (node.right != null)
                {
                    queue.Enqueue((node.right, col + 1));
                }
            }

            for (int c = minCol; c <= maxCol; c++)
            {
                if (colMap.TryGetValue(c, out List<int> colList))
                {
                    result.Add(colList);
                }
            }

            return result;
        }
    }
}