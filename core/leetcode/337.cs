// Url:https://leetcode.com/problems/house-robber-iii

/*
337. House Robber III
Medium

The thief has found himself a new place for his thievery again. There is only one entrance to this area, called the "root." Besides the root, each house has one and only one parent house. After a tour, the smart thief realized that "all houses in this place forms a binary tree". It will automatically contact the police if two directly-linked houses were broken into on the same night.

Determine the maximum amount of money the thief can rob tonight without alerting the police.

Example 1:

Input: [3,2,3,null,3,null,1]

     3
    / \
   2   3
    \   \ 
     3   1

Output: 7 
Explanation: Maximum amount of money the thief can rob = 3 + 3 + 1 = 7.

Example 2:

Input: [3,4,5,1,3,null,1]

     3
    / \
   4   5
  / \   \ 
 1   3   1

Output: 9
Explanation: Maximum amount of money the thief can rob = 4 + 5 = 9.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem337 {
    public class Solution {
        public static void Init() {
            TreeNode node_a = new TreeNode (3);
            node_a.left = new TreeNode (2);
            node_a.right = new TreeNode (3);
            node_a.left.right = new TreeNode (3);
            node_a.right.right = new TreeNode (1);
            Console.WriteLine (Rob(node_a));

            TreeNode node_b = new TreeNode (3);
            node_b.left = new TreeNode (4);
            node_b.right = new TreeNode (5);
            node_b.left.left = new TreeNode (1);
            node_b.left.right = new TreeNode (3);
            node_b.right.right = new TreeNode (1);
            Console.WriteLine (Rob(node_b));
        }

        public static int Rob (TreeNode root) {
            if (root == null) {
                return 0;
            }

            int[] result = RobHelper(root);
            return Math.Max (result[0], result[1]);
        }

        private static int[] RobHelper (TreeNode node) {
            if (node == null) {
                return [0, 0];
            }

            int[] result = new int[2];
            int[] left = RobHelper(node.left);
            int[] right = RobHelper(node.right);

            // 0 index when root node is included
            result[0] = node.val + left[1] + right[1];
            result[1] = Math.Max (left[0], left[1]) + Math.Max (right[0], right[1]);

            return result;
        }
    }

    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode (int x) {
            val = x;
        }
    }
}