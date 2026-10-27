// Url:https://leetcode.com/problems/binary-tree-paths

/*
257. Binary Tree Paths
Easy

Given a binary tree, return all root-to-leaf paths.

Note: A leaf is a node with no children.

Example:

Input:

   1
 /   \
2     3
 \
  5

Output: ["1->2->5", "1->3"]

Explanation: All root-to-leaf paths are: 1->2->5, 1->3
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem257 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n)
        // Space: O (h), where h s the height of the tree
        public static IList<string> BinaryTreePaths (TreeNode root) {
            IList<string> result = [];

            if (root == null) {
                return result;
            }

            Dfs(root, result, "");

            return result;
        }

        private static void Dfs (TreeNode node, IList<string> result, string path) {
            path += node.val;

            if (node.left == null && node.right == null) {
                result.Add (path);
                return;
            }

            if (node.left != null)
                Dfs(node.left, result, path + "->");

            if (node.right != null)
                Dfs(node.right, result, path + "->");
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