// Url:https://leetcode.com/problems/longest-univalue-path

/*
687. Longest Univalue Path
Easy

Given a binary tree, find the length of the longest path where each node in the path has the same value. This path may or may not pass through the root.

The length of path between two nodes is represented by the number of edges between them.

 

Example 1:

Input:

              5
             / \
            4   5
           / \   \
          1   1   5


Output: 2

 

Example 2:

Input:

              1
             / \
            4   5
           / \   \
          4   4   5


Output: 2

 

Note: The given binary tree has not more than 10000 nodes. The height of the tree is not more than 1000.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem687 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static int LongestUnivaluePath (TreeNode root) {
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