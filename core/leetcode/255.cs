// Url: https://leetcode.com/problems/verify-preorder-sequence-in-binary-search-tree

/*
255. Verify Preorder Sequence in Binary Search Tree
Medium

Given an array of unique integers preorder, return true if it is the correct preorder traversal sequence of a binary search tree.

Example 1:
Input: preorder = [5,2,1,3,6]
Output: true

Example 2:
Input: preorder = [5,2,6,1,3]
Output: false

Constraints:
1 <= preorder.length <= 10^4
1 <= preorder[i] <= 10^4
All the elements of preorder are unique.

Follow up: Could you do it using only constant space complexity?
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem255
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(VerifyPreorder(new int[] { 5, 2, 1, 3, 6 })); // True
            Console.WriteLine(VerifyPreorder(new int[] { 5, 2, 6, 1, 3 })); // False
        }

        // Time: O(n) where n is preorder.Length
        // Space: O(n) using a stack (can be reduced to O(1) by reusing input array as stack)
        public static bool VerifyPreorder(int[] preorder)
        {
            if (preorder == null || preorder.Length == 0)
            {
                return true;
            }

            int low = int.MinValue;
            Stack<int> stack = new();

            foreach (int x in preorder)
            {
                if (x < low)
                {
                    return false;
                }

                while (stack.Count > 0 && x > stack.Peek())
                {
                    low = stack.Pop();
                }

                stack.Push(x);
            }

            return true;
        }
    }
}