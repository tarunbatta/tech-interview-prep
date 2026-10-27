// Url:https://leetcode.com/problems/count-complete-tree-nodes

/*
222. Count Complete Tree Nodes
Medium

Given a complete binary tree, count the number of nodes.

Note: 

Definition of a complete binary tree from Wikipedia:
In a complete binary tree every level, except possibly the last, is completely filled, and all nodes in the last level are as far left as possible. It can have between 1 and 2h nodes inclusive at the last level h.

Example:

Input: 
    1
   / \
  2   3
 / \  /
4  5 6

Output: 6
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem222 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static int CountNodes (TreeNode root) {
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