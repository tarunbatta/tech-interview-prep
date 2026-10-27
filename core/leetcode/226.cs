// Url:https://leetcode.com/problems/invert-binary-tree

/*
226. Invert Binary Tree
Easy

Invert a binary tree.

Example:

Input:

     4
   /   \
  2     7
 / \   / \
1   3 6   9

Output:

     4
   /   \
  7     2
 / \   / \
9   6 3   1

Trivia:
This problem was inspired by this original tweet by Max Howell:

Google: 90% of our engineers use the software you wrote (Homebrew), but you can’t invert a binary tree on a whiteboard so f*** off.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem226 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static TreeNode InvertTree (TreeNode root) {
            return null;
        }
    }

    public class TreeNode(int x)
    {
        public int val = x;
        public TreeNode left;
        public TreeNode right;
    }
}