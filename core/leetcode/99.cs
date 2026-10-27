// Url:https://leetcode.com/problems/recover-binary-search-tree

/*
99. Recover Binary Search Tree
Hard

Two elements of a binary search tree (BST) are swapped by mistake.

Recover the tree without changing its structure.

Example 1:

Input: [1,3,null,null,2]

   1
  /
 3
  \
   2

Output: [3,1,null,null,2]

   3
  /
 1
  \
   2


Example 2:

Input: [3,1,4,null,null,2]

  3
 / \
1   4
   /
  2

Output: [2,1,4,null,null,3]

  2
 / \
1   4
   /
  3


Follow up:
	A solution using O(n) space is pretty straight forward.
	Could you devise a constant space solution?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem99 {
    public class Solution {
        public static void Init() {
            TreeNode node = new TreeNode (3);
            node.left = new TreeNode (1);
            node.right = new TreeNode (4);
            node.right.left = new TreeNode (2);

            RecoverTree (node);
        }

        static TreeNode first = null;
        static TreeNode second = null;
        static TreeNode prev = new TreeNode (Int32.MinValue);

        // Time: O (n)
        // Space: O (n)
        public static void RecoverTree (TreeNode root) {
            first = null;
            second = null;
            prev = new TreeNode (Int32.MinValue);

            traverse (root);

            // Swap the values of the two nodes
            int temp = first.val;
            first.val = second.val;
            second.val = temp;
        }

        private static void traverse (TreeNode node) {
            if (node == null)
                return;

            traverse (node.left);

            // Start of "do some business", 
            // If first element has not been found, assign it to prev 
            if (first == null && prev.val > node.val) {
                first = prev;
            }

            // If first element is found, assign the second element to the node 
            if (first != null && prev.val > node.val) {
                second = node;
            }
            prev = node;
            // End of "do some business"
            traverse (node.right);
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