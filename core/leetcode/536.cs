// Url: https://leetcode.com/problems/construct-binary-tree-from-string

/*
536. Construct Binary Tree from String
Medium

You need to construct a binary tree from a string consisting of parenthesis and integers.

The whole input represents a binary tree. It contains an integer followed by zero, one or two pairs of parenthesis. The integer represents the root's value and a pair of parenthesis contains a child binary tree with the same structure.

You always start to construct the left child node of the parent first if it exists.

Example 1:
Input: s = "4(2(3)(1))(6(5))"
Output: [4,2,6,3,1,5]

Example 2:
Input: s = "4(2(3)(1))(6(5)(7))"
Output: [4,2,6,3,1,5,7]

Example 3:
Input: s = "-4(2(3)(1))(6(5)(7))"
Output: [-4,2,6,3,1,5,7]

Constraints:
0 <= s.length <= 3 * 10^4
s consists of digits, '(', ')', and '-' only.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem536
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class Solution
    {
        public void Init()
        {
            TreeNode root = Str2tree("4(2(3)(1))(6(5))");
            Console.WriteLine(root?.val); // 4
        }

        // Time: O(n) where n is s.Length
        // Space: O(h) where h is tree height for recursion stack
        public static TreeNode Str2tree(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            int firstParen = s.IndexOf('(');

            if (firstParen == -1)
            {
                return new TreeNode(int.Parse(s));
            }

            int rootVal = int.Parse(s.Substring(0, firstParen));
            TreeNode root = new(rootVal);

            int start = firstParen;
            int openCount = 0;

            for (int i = start; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    openCount++;
                }
                else if (s[i] == ')')
                {
                    openCount--;
                }

                if (openCount == 0)
                {
                    if (start == firstParen)
                    {
                        // Left subtree content inside parentheses
                        root.left = Str2tree(s.Substring(start + 1, i - start - 1));
                        start = i + 1;
                    }
                    else
                    {
                        // Right subtree content inside parentheses
                        root.right = Str2tree(s.Substring(start + 1, i - start - 1));
                    }
                }
            }

            return root;
        }
    }
}