// Url:https://leetcode.com/problems/kth-smallest-element-in-a-bst

/*
230. Kth Smallest Element in a BST
Medium

Given a binary search tree, write a function kthSmallest to find the kth smallest element in it.

Note: 
You may assume k is always valid, 1 ≤ k ≤ BST's total elements.

Example 1:

Input: root = [3,1,4,null,2], k = 1
   3
  / \
 1   4
  \
   2
Output: 1

Example 2:

Input: root = [5,3,6,2,4,null,null,1], k = 3
       5
      / \
     3   6
    / \
   2   4
  /
 1
Output: 3


Follow up:
What if the BST is modified (insert/delete operations) often and you need to find the kth smallest frequently? How would you optimize the kthSmallest routine?
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem230 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n)
        // Space: O (n)
        public static int KthSmallest_a (TreeNode root, int k) {
            if (root == null || k == 0) {
                return 0;
            }

            List<int> l = [];
            InOrder(root, l);

            return k > l.Count ? 0 : l[k - 1];
        }

        private static void InOrder (TreeNode root, List<int> l) {
            if (root == null) {
                return;
            }

            InOrder(root.left, l);
            l.Add (root.val);
            InOrder(root.right, l);
        }

        // Time: O (log (n) + k)
        // Space: O (log (n) + k)
        public static int KthSmallest_b (TreeNode root, int k) {
            Stack<TreeNode> stack = new();

            while (true) {
                while (root != null) {
                    stack.Push (root);
                    root = root.left;
                }

                root = stack.Pop ();
                if (--k == 0) {
                    return root.val;
                }

                root = root.right;
            }
        }

        // Time: O (log (n))
        // Space: O (1)
        public static int KthSmallest_c (TreeNode root, int k) {
            if (root == null) {
                return 0;
            }

            int[] result = new int[2];
            InOrder(root, result, k);
            return result[1];
        }

        private static void InOrder (TreeNode root, int[] nums, int k) {
            if (root == null) {
                return;
            }

            InOrder(root.left, nums, k);

            if (++nums[0] == k) {
                nums[1] = root.val;
                return;
            }

            InOrder(root.right, nums, k);
        }
    }

    public class TreeNode(int x)
    {
        public int val = x;
        public TreeNode left;
        public TreeNode right;
    }
}