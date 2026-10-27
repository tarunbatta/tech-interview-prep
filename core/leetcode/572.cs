// Url:https://leetcode.com/problems/subtree-of-another-tree

/*
572. Subtree of Another Tree
Easy

Given two non-empty binary trees s and t, check whether tree t has exactly the same structure and node values with a subtree of s. A subtree of s is a tree consists of a node in s and all of this node's descendants. The tree s could also be considered as a subtree of itself.


Example 1:

Given tree s:
     3
    / \
   4   5
  / \
 1   2

Given tree t:
   4 
  / \
 1   2

Return true, because t has the same structure and node values with a subtree of s.


Example 2:

Given tree s:
     3
    / \
   4   5
  / \
 1   2
    /
   0

Given tree t:
   4
  / \
 1   2

Return false.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem572 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static bool IsSubtree (TreeNode s, TreeNode t) {
            return false;
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