// Url:https://leetcode.com/problems/maximum-depth-of-n-ary-tree

/*
559. Maximum Depth of N-ary Tree
Easy

Given a n-ary tree, find its maximum depth.

The maximum depth is the number of nodes along the longest path from the root node down to the farthest leaf node.

For example, given a 3-ary tree:
 



 

We should return its max depth, which is 3.

 

Note:


	The depth of the tree is at most 1000.
	The total number of nodes is at most 5000.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem559 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static int MaxDepth (Node root) {
            return 0;
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