// Url:https://leetcode.com/problems/encode-n-ary-tree-to-binary-tree

/*
431. Encode N-ary Tree to Binary Tree
Hard

Design an algorithm to encode an N-ary tree into a binary tree and decode the binary tree to get the original N-ary tree. An N-ary tree is a rooted tree in which each node has no more than N children. Similarly, a binary tree is a rooted tree in which each node has no more than 2 children. There is no restriction on how your encode/decode algorithm should work. You just need to ensure that an N-ary tree can be encoded to a binary tree and this binary tree can be decoded to the original N-nary tree structure.

Nary-Tree input serialization is represented in their level order traversal, each group of children is separated by the null value (See following example).

For example, you may encode the following 3-ary tree to a binary tree in this way:

Input: root = [1,null,3,2,4,null,5,6]
Note that the above is just an example which might or might not work. You do not necessarily need to follow this format, so please be creative and come up with different approaches yourself.

Constraints:

The height of the n-ary tree is less than or equal to 1000
The total number of nodes is between [0, 10^4]
Do not use class member/global/static variables to store states. Your encode and decode algorithms should be stateless.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem431 {
    public class Solution {
        public static void Init () {
            Node root = null;
            Codec codec = new();
            Codec.decode(Codec.encode(root));
        }
    }

    // (Next Level -> left, Same Level -> right)
    public class Codec {
        // Encodes an n-ary tree to a binary tree.
        public static TreeNode encode (Node root) {
            if (root == null) {
                return null;
            }

            TreeNode result = new(root.val);

            if (root.children.Count > 0) {
                result.left = encode(root.children[0]);
            }

            TreeNode current = result.left;

            for (int i = 1; i < root.children.Count; i++) {
                current.right = encode(root.children[i]);
                current = current.right;
            }

            return result;
        }

        // Decodes your binary tree to an n-ary tree.
        public static Node decode (TreeNode root) {
            if (root == null) {
                return null;
            }

            Node result = new(root.val, []);
            TreeNode current = root.left;

            while (current != null) {
                result.children.Add (decode(current));
                current = current.right;
            }

            return result;
        }
    }

    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode (int x) {
            val = x;
        }
    }

    public class Node {
        public int val;
        public IList<Node> children;

        public Node () { }

        public Node (int _val) {
            val = _val;
        }

        public Node (int _val, IList<Node> _children) {
            val = _val;
            children = _children;
        }
    }
}