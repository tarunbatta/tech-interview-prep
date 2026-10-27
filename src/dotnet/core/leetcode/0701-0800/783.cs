// Url:https://leetcode.com/problems/minimum-distance-between-bst-nodes

/*
783. Minimum Distance Between BST Nodes
Easy

Given a Binary Search Tree (BST) with the root node root, return the minimum difference between the values of any two different nodes in the tree.

Example :

Input: root = [4,2,6,1,3,null,null]
Output: 1
Explanation:
Note that root is a TreeNode object, not an array.

The given tree [4,2,6,1,3,null,null] is represented by the following diagram:

          4
        /   \
      2      6
     / \    
    1   3  

while the minimum difference in this tree is 1, it occurs between node 1 and node 2, also between node 3 and node 2.


Note:
	The size of the BST will be between 2 and 100.
	The BST is always valid, each node's value is an integer, and each node's value is different.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem783 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static int MinDiffInBST (TreeNode root) {
            return 0;
        }
    }

    public class TreeNode(int x)
    {
        public int val = x;
        public TreeNode left;
        public TreeNode right;
    }
}