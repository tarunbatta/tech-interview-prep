// Url: https://leetcode.com/problems/closest-binary-search-tree-value

/*
270. Closest Binary Search Tree Value
Easy

Given the root of a binary search tree and a target value, return the value in the BST that is closest to the target. If there are multiple answers, return the smallest.

Example 1:
Input: root = [4,2,5,1,3], target = 3.714286
Output: 4

Example 2:
Input: root = [1], target = 4.428571
Output: 1

Constraints:
The number of nodes in the tree is in the range [1, 10^4].
0 <= Node.val <= 10^9
-10^9 <= target <= 10^9
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem270
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
            TreeNode root = new(4)
            {
                left = new(2)
                {
                    left = new(1),
                    right = new(3)
                },
                right = new(5)
            };

            Console.WriteLine(ClosestValue(root, 3.714286)); // 4
            Console.WriteLine(ClosestValue(root, 3.5));      // 3 (smallest on tie)
        }

        // Time: O(h) where h is the height of the BST
        // Space: O(1) auxiliary space (iterative traversal)
        public static int ClosestValue(TreeNode root, double target)
        {
            int closest = root.val;
            TreeNode curr = root;

            while (curr != null)
            {
                double currDiff = Math.Abs(curr.val - target);
                double closestDiff = Math.Abs(closest - target);

                if (currDiff < closestDiff || (Math.Abs(currDiff - closestDiff) < 1e-9 && curr.val < closest))
                {
                    closest = curr.val;
                }

                if (target < curr.val)
                {
                    curr = curr.left;
                }
                else
                {
                    curr = curr.right;
                }
            }

            return closest;
        }
    }
}