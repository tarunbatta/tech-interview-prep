// Url:https://leetcode.com/problems/range-sum-of-bst

/*
938. Range Sum of BST
Easy

Given the root node of a binary search tree, return the sum of values of all nodes with value between L and R (inclusive).
The binary search tree is guaranteed to have unique values.


Example 1:

Input: root = [10,5,15,3,7,null,18], L = 7, R = 15
Output: 32


Example 2:

Input: root = [10,5,15,3,7,13,18,1,null,6], L = 6, R = 10
Output: 23


Note:
	The number of nodes in the tree is at most 10000.
	The final answer is guaranteed to be less than 2^31.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem938 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n)
        // Space: O (n)
        public static int RangeSumBST (TreeNode root, int L, int R) {
            int result = 0;

            if (root == null) {
                return result;
            }

            Queue<TreeNode> q = new();
            q.Enqueue (root);

            while (q.Count > 0) {
                TreeNode current = q.Dequeue ();

                if (current.val >= L && current.val <= R) {
                    result += current.val;
                }

                if (current.left != null && current.val > L) {
                    q.Enqueue (current.left);
                }

                if (current.right != null && current.val < R) {
                    q.Enqueue (current.right);
                }
            }

            return result;
        }
    }

    public class TreeNode(int x)
    {
        public int val = x;
        public TreeNode left;
        public TreeNode right;
    }
}