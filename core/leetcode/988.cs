// Url:https://leetcode.com/problems/smallest-string-starting-from-leaf

/*
988. Smallest String Starting From Leaf
Medium

Given the root of a binary tree, each node has a value from 0 to 25 representing the letters 'a' to 'z': a value of 0 represents 'a', a value of 1 represents 'b', and so on.

Find the lexicographically smallest string that starts at a leaf of this tree and ends at the root.

(As a reminder, any shorter prefix of a string is lexicographically smaller: for example, "ab" is lexicographically smaller than "aba".  A leaf of a node is a node that has no children.)



 







Example 1:



Input: [0,1,2,3,4,3,4]
Output: "dba"



Example 2:



Input: [25,1,3,1,3,0,2]
Output: "adz"



Example 3:



Input: [2,2,1,null,1,0,null,0]
Output: "abc"


 

Note:


	The number of nodes in the given tree will be between 1 and 8500.
	Each node in the tree will have a value between 0 and 25.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem988 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static string SmallestFromLeaf (TreeNode root) {
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