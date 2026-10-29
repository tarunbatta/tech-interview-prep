// Url:https://leetcode.com/problems/construct-binary-tree-from-preorder-and-postorder-traversal

/*
889. Construct Binary Tree from Preorder and Postorder Traversal
Medium

Return any binary tree that matches the given preorder and postorder traversals.

Values in the traversals pre and post are distinct positive integers.

 


Example 1:

Input: pre = [1,2,4,5,3,6,7], post = [4,5,2,6,7,3,1]
Output: [1,2,3,4,5,6,7]


 

Note:


	1 <= pre.length == post.length <= 30
	pre[] and post[] are both permutations of 1, 2, ..., pre.length.
	It is guaranteed an answer exists. If there exists multiple answers, you can return any of them.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem889 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static TreeNode ConstructFromPrePost (int[] pre, int[] post) {
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