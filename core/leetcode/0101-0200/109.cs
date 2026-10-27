// Url:https://leetcode.com/problems/convert-sorted-list-to-binary-search-tree

/*
109. Convert Sorted List to Binary Search Tree
Medium

Given a singly linked list where elements are sorted in ascending order, convert it to a height balanced BST.

For this problem, a height-balanced binary tree is defined as a binary tree in which the depth of the two subtrees of every node never differ by more than 1.

Example:

Given the sorted linked list: [-10,-3,0,5,9],

One possible answer is: [0,-3,9,-10,null,5], which represents the following height balanced BST:

      0
     / \
   -3   9
   /   /
 -10  5
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem109 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static TreeNode SortedListToBST (ListNode head) {
            return null;
        }
    }

    public class ListNode(int x)
    {
        public int val = x;
        public ListNode next;
    }

    public class TreeNode(int x)
    {
        public int val = x;
        public TreeNode left;
        public TreeNode right;
    }
}