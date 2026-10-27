// Url:https://leetcode.com/problems/find-bottom-left-tree-value

/*
513. Find Bottom Left Tree Value
Medium

Given a binary tree, find the leftmost value in the last row of the tree. 


Example 1:
Input:

    2
   / \
  1   3

Output:
1



  Example 2: 
Input:

        1
       / \
      2   3
     /   / \
    4   5   6
       /
      7

Output:
7



Note:
You may assume the tree (i.e., the given root node) is not NULL.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem513 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static int FindBottomLeftValue (TreeNode root) {
            return 0;
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