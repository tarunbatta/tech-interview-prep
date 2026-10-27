// Url:https://leetcode.com/problems/n-ary-tree-level-order-traversal

/*
429. N-ary Tree Level Order Traversal
Easy

Given an n-ary tree, return the level order traversal of its nodes' values. (ie, from left to right, level by level).

For example, given a 3-ary tree:

 



 

We should return its level order traversal:

[
     [1],
     [3,2,4],
     [5,6]
]


 

Note:


	The depth of the tree is at most 1000.
	The total number of nodes is at most 5000.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem429 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static IList<IList<int>> LevelOrder (Node root) {
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