// Url:https://leetcode.com/problems/delete-nodes-and-return-forest

/*
1110. Delete Nodes And Return Forest
Medium

Given the root of a binary tree, each node in the tree has a distinct value.

After deleting all nodes with a value in to_delete, we are left with a forest (a disjoint union of trees).

Return the roots of the trees in the remaining forest.  You may return the result in any order.

 
Example 1:
Input: root = [1,2,3,4,5,6,7], to_delete = [3,5]
Output: [[1,2,null,4],[6],[7]]


Constraints:
	The number of nodes in the given tree is at most 1000.
	Each node has a distinct value between 1 and 1000.
	to_delete.length <= 1000
	to_delete contains distinct values between 1 and 1000.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem1110 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n)
        // Space: O (h + n), where h is height of tree
        public static IList<TreeNode> DelNodes (TreeNode root, int[] to_delete) {
            IList<TreeNode> result = [];

            List<int> toBeDeleted = [.. to_delete];

            Helper(root, result, toBeDeleted, true);

            return result;
        }

        // If a node is root (has no parent) and isn't deleted, when will we add it to the result.
        private static TreeNode Helper (TreeNode node, IList<TreeNode> result, List<int> toBeDeleted, bool isRoot) {
            if (node == null) {
                return null;
            }

            bool deleted = toBeDeleted.Contains (node.val);

            if (isRoot && !deleted) {
                result.Add (node);
            }

            node.left = Helper(node.left, result, toBeDeleted, deleted);
            node.right = Helper(node.right, result, toBeDeleted, deleted);

            return deleted ? null : node;
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