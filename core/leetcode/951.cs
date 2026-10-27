// Url:https://leetcode.com/problems/flip-equivalent-binary-trees

/*
951. Flip Equivalent Binary Trees
Medium

For a binary tree T, we can define a flip operation as follows: choose any node, and swap the left and right child subtrees.

A binary tree X is flip equivalent to a binary tree Y if and only if we can make X equal to Y after some number of flip operations.

Write a function that determines whether two binary trees are flip equivalent.  The trees are given by root nodes root1 and root2.

 

Example 1:

Input: root1 = [1,2,3,4,5,6,null,null,null,7,8], root2 = [1,3,2,null,6,4,5,null,null,null,null,8,7]
Output: true
Explanation: We flipped at nodes with values 1, 3, and 5.



 

Note:


	Each tree will have at most 100 nodes.
	Each value in each tree will be a unique integer in the range [0, 99].



 
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem951 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static bool FlipEquiv (TreeNode root1, TreeNode root2) {
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