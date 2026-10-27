// Url:https://leetcode.com/problems/equal-tree-partition

/*
663. Equal Tree Partition
Medium

Given the root of a binary tree, return true if you can partition the tree into two trees with equal sums of values by deleting exactly one edge.

Example 1:
Input: root = [5,10,10,null,null,2,3]
Output: true
Explanation: 
    5
   / \
 10   10
     /  \
    2    3
Removing edge between 5 and right child 10:
Sum of subtree at 10 is 10 + 2 + 3 = 15.
Sum of remaining tree is 5 + 10 = 15.

Example 2:
Input: root = [1,2,10,null,null,2,20]
Output: false
Explanation: You cannot split the tree into two trees with equal sums after removing exactly one edge.

Constraints:
- The number of nodes in the tree is in the range [1, 10^4].
- -10^5 <= Node.val <= 10^5
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem663 {
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
            TreeNode root1 = new(5,
                new TreeNode(10),
                new TreeNode(10, new TreeNode(2), new TreeNode(3))
            );
            Console.WriteLine(CheckEqualTree(root1)); // True

            TreeNode root2 = new(1,
                new TreeNode(2),
                new TreeNode(10, new TreeNode(2), new TreeNode(20))
            );
            Console.WriteLine(CheckEqualTree(root2)); // False
        }

        // Time: O(n) where n is number of nodes in tree
        // Space: O(n) for subtree sums stack/list and recursion
        public static bool CheckEqualTree(TreeNode root) {
            if (root == null) {
                return false;
            }

            List<long> subtreeSums = [];
            long totalSum = ComputeSubtreeSums(root, subtreeSums);

            // Removing an edge means separating a non-root subtree
            // The last element in subtreeSums is totalSum from the root, so remove it
            subtreeSums.RemoveAt(subtreeSums.Count - 1);

            if (totalSum % 2 != 0) {
                return false;
            }

            long target = totalSum / 2;
            return subtreeSums.Contains(target);
        }

        private static long ComputeSubtreeSums(TreeNode node, List<long> subtreeSums) {
            if (node == null) {
                return 0;
            }

            long sum = node.val + ComputeSubtreeSums(node.left, subtreeSums) + ComputeSubtreeSums(node.right, subtreeSums);
            subtreeSums.Add(sum);
            return sum;
        }
    }
}