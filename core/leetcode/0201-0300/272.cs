// Url: https://leetcode.com/problems/closest-binary-search-tree-value-ii

/*
272. Closest Binary Search Tree Value II
Hard

Given the root of a binary search tree, a target value, and an integer k, return the k values in the BST that are closest to the target. You may return the answer in any order.

You are guaranteed to have only one unique set of k values in the BST that are closest to the target.

Example 1:
Input: root = [4,2,5,1,3], target = 3.714286, k = 2
Output: [4,3]

Example 2:
Input: root = [1], target = 0.0, k = 1
Output: [1]

Constraints:
The number of nodes in the tree is n.
1 <= k <= n <= 10^4.
0 <= Node.val <= 10^9
-10^9 <= target <= 10^9

Follow up: Assume that the BST is balanced. Could you solve it in less than O(n) runtime (where n = total nodes)?
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem272
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
            TreeNode root = new(4)
            {
                left = new(2)
                {
                    left = new(1),
                    right = new(3)
                },
                right = new(5)
            };

            var res1 = ClosestKValues(root, 3.714286, 2);
            Console.WriteLine(string.Join(", ", res1)); // 3, 4

            var res2 = ClosestKValues(new TreeNode(1), 0.0, 1);
            Console.WriteLine(string.Join(", ", res2)); // 1
        }

        // Time: O(n) with early termination when differences start growing
        // Space: O(h + k) where h is the BST height and k is the queue capacity
        public static IList<int> ClosestKValues(TreeNode root, double target, int k)
        {
            Queue<int> queue = new(k);
            Inorder(root, target, k, queue);
            return [.. queue];
        }

        private static bool Inorder(TreeNode node, double target, int k, Queue<int> queue)
        {
            if (node == null)
            {
                return true;
            }

            if (!Inorder(node.left, target, k, queue))
            {
                return false;
            }

            if (queue.Count < k)
            {
                queue.Enqueue(node.val);
            }
            else
            {
                if (Math.Abs(node.val - target) < Math.Abs(queue.Peek() - target))
                {
                    queue.Dequeue();
                    queue.Enqueue(node.val);
                }
                else
                {
                    // Since inorder traversal is strictly increasing,
                    // any further node will be even further from target.
                    return false;
                }
            }

            return Inorder(node.right, target, k, queue);
        }
    }
}