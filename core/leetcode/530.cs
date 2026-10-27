// Url:https://leetcode.com/problems/minimum-absolute-difference-in-bst

/*
530. Minimum Absolute Difference in BST
Easy

Given a binary search tree with non-negative values, find the minimum absolute difference between values of any two nodes.

Example:

Input:

   1
    \
     3
    /
   2

Output:
1

Explanation:
The minimum absolute difference is 1, which is the difference between 2 and 1 (or between 2 and 3).


 

Note: There are at least two nodes in this BST.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem530 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static int GetMinimumDifference (TreeNode root) {
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