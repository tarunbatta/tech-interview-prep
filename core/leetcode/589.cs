// Url:https://leetcode.com/problems/n-ary-tree-preorder-traversal

/*
589. N-ary Tree Preorder Traversal
Easy

Given an n-ary tree, return the preorder traversal of its nodes' values.

For example, given a 3-ary tree:

 



 

Return its preorder traversal as: [1,3,5,6,2,4].

 

Note:

Recursive solution is trivial, could you do it iteratively?
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem589 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static IList<int> Preorder (Node root) {
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