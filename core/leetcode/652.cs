// Url:https://leetcode.com/problems/find-duplicate-subtrees

/*
652. Find Duplicate Subtrees
Medium

Given a binary tree, return all duplicate subtrees. For each kind of duplicate subtrees, you only need to return the root node of any one of them.

Two trees are duplicate if they have the same structure with same node values.

Example 1: 

        1
       / \
      2   3
     /   / \
    4   2   4
       /
      4


The following are two duplicate subtrees:

      2
     /
    4


and

    4

Therefore, you need to return above trees' root in the form of a list.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem652 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static IList<TreeNode> FindDuplicateSubtrees (TreeNode root) {
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