// Url:https://leetcode.com/problems/n-ary-tree-postorder-traversal

/*
590. N-ary Tree Postorder Traversal
Easy

Given an n-ary tree, return the postorder traversal of its nodes' values.

For example, given a 3-ary tree:

 



 

Return its postorder traversal as: [5,6,3,2,4,1].
 

Note:

Recursive solution is trivial, could you do it iteratively?
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem590 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static IList<int> Postorder (Node root) {
            return null;
        }
    }

    public class Node {
        public int val;
        public IList<Node> children;

        public Node () {

        }

        public Node (int _val, IList<Node> _children) {
            val = _val;
            children = _children;
        }
    }
}