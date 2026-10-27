// Url:https://leetcode.com/problems/split-bst

/*
776. Split BST
Medium

Given the root of a binary search tree (BST) and an integer target, split the tree into two subtrees where one subtree has nodes that are all smaller or equal to the target value, while the other subtree has all nodes that are greater than the target value. It Is not necessarily the case that the tree contains a node with value target.

Additionally, most of the structure of the original tree should remain. Formally, for any child c with parent p in the original tree, if they are both in the same subtree after the split, then node c should still have the parent p.

Return an array of the two roots of the two subtrees in order [smaller_or_equal_root, greater_root].

Example 1:
Input: root = [4,2,6,1,3,5,7], target = 2
Output: [[2,1],[4,3,6,null,null,5,7]]
Explanation: 
The original BST is:
        4
      /   \
     2     6
    / \   / \
   1   3 5   7
After split with target 2:
Subtree 1 (<= 2):
     2
    /
   1
Subtree 2 (> 2):
        4
      /   \
     3     6
          / \
         5   7

Example 2:
Input: root = [1], target = 1
Output: [[1],[]]

Constraints:
- The number of nodes in the tree is in the range [1, 50].
- 0 <= Node.val, target <= 1000
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem776 {
    public class TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        public int val = val;
        public TreeNode left = left;
        public TreeNode right = right;
    }

    public class Solution {
        public static void Init() {
            TreeNode root1 = new(4,
                new TreeNode(2, new TreeNode(1), new TreeNode(3)),
                new TreeNode(6, new TreeNode(5), new TreeNode(7))
            );
            var res1 = SplitBST(root1, 2);
            Console.WriteLine($"Split 1 root: {res1[0]?.val}, Split 2 root: {res1[1]?.val}"); // 2, 4

            TreeNode root2 = new(1);
            var res2 = SplitBST(root2, 1);
            Console.WriteLine($"Split 1 root: {res2[0]?.val}, Split 2 root: {res2[1]?.val.ToString() ?? "null"}"); // 1, null
        }

        // Time: O(h) where h is the height of the BST
        // Space: O(h) for call stack
        public static TreeNode[] SplitBST(TreeNode root, int target) {
            if (root == null) {
                return [null, null];
            }

            if (root.val <= target) {
                TreeNode[] rightSplit = SplitBST(root.right, target);
                root.right = rightSplit[0];
                return [root, rightSplit[1]];
            } else {
                TreeNode[] leftSplit = SplitBST(root.left, target);
                root.left = leftSplit[1];
                return [leftSplit[0], root];
            }
        }
    }
}