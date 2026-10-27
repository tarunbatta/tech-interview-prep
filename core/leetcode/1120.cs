// Url:https://leetcode.com/problems/maximum-average-subtree

/*
1120. Maximum Average Subtree
Medium

Given the root of a binary tree, return the maximum average value of a subtree of that tree. Answers within 10^-5 of the actual answer will be accepted.

A subtree of a tree is any node of that tree plus all its descendants.

The average value of a tree is the sum of its values, divided by the number of nodes.

Example 1:
Input: root = [5,6,1]
Output: 6.00000
Explanation: 
For the node with value = 5 we have an average of (5 + 6 + 1) / 3 = 4.
For the node with value = 6 we have an average of 6 / 1 = 6.
For the node with value = 1 we have an average of 1 / 1 = 1.
So the answer is 6 which is the maximum.

Example 2:
Input: root = [0,null,1]
Output: 1.00000

Constraints:
- The number of nodes in the tree is in the range [1, 10^4].
- 0 <= Node.val <= 10^5
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1120 {
    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null) {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class Solution {
        public static void Init() {
            TreeNode root1 = new(5, new TreeNode(6), new TreeNode(1));
            Console.WriteLine($"{MaximumAverageSubtree(root1):F5}"); // 6.00000

            TreeNode root2 = new(0, null, new TreeNode(1));
            Console.WriteLine($"{MaximumAverageSubtree(root2):F5}"); // 1.00000
        }

        // Time: O(n) where n is the number of nodes in the tree
        // Space: O(h) where h is the tree height for recursion stack
        public static double MaximumAverageSubtree(TreeNode root) {
            double maxAvg = 0.0;
            PostOrder(root, ref maxAvg);
            return maxAvg;
        }

        private static (long Sum, int Count) PostOrder(TreeNode node, ref double maxAvg) {
            if (node == null) {
                return (0, 0);
            }

            var left = PostOrder(node.left, ref maxAvg);
            var right = PostOrder(node.right, ref maxAvg);

            long totalSum = node.val + left.Sum + right.Sum;
            int totalCount = 1 + left.Count + right.Count;

            double avg = (double)totalSum / totalCount;
            if (avg > maxAvg) {
                maxAvg = avg;
            }

            return (totalSum, totalCount);
        }
    }
}