// Url: https://leetcode.com/problems/serialize-and-deserialize-binary-tree/

/*
297. Serialize and Deserialize Binary Tree
Hard

Serialization is the process of converting a data structure or object into a sequence of bits so that it can be stored in a file or memory buffer, or transmitted across a network connection link to be reconstructed later in the same or another computer environment.

Design an algorithm to serialize and deserialize a binary tree. There is no restriction on how your serialization/deserialization algorithm should work. You just need to ensure that a binary tree can be serialized to a string and this string can be deserialized to the original tree structure.

Example: 

You may serialize the following tree:

    1
   / \
  2   3
     / \
    4   5

as "[1,2,3,null,null,4,5]"
Clarification: The above format is the same as how LeetCode serializes a binary tree. You do not necessarily need to follow this format, so please be creative and come up with different approaches yourself.

Note: Do not use class member/global/static variables to store states. Your serialize and deserialize algorithms should be stateless.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem297 {
    public class Solution {
        public static void Init () {
            TreeNode root = null;

            Codec codec = new Codec ();
            Codec.deserialize(Codec.serialize(root));
        }
    }

    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode (int x) { val = x; }
    }

    public class Codec {
        private const string _spliter = ",";
        private const string _null = "X";

        // Encodes a tree to a single string.
        public static String serialize (TreeNode root) {
            StringBuilder sb = new StringBuilder ();
            serializeHelper(root, sb);
            return sb.ToString ();
        }

        private static void serializeHelper (TreeNode node, StringBuilder sb) {
            if (node == null) {
                sb.Append (_null).Append (_spliter);
            } else {
                sb.Append (node.val).Append (_spliter);
                serializeHelper(node.left, sb);
                serializeHelper(node.right, sb);
            }
        }

        // Decodes your encoded data to tree.
        public static TreeNode deserialize (String data) {
            List<String> nodes = [.. data.Split (_spliter)];
            return deserializeHelper(nodes);
        }

        private static TreeNode deserializeHelper (List<String> nodes) {
            string val = nodes[0];
            nodes.RemoveAt (0);

            if (val == _null) {
                return null;
            } else {
                TreeNode node = new TreeNode (Convert.ToInt32 (val));
                node.left = deserializeHelper(nodes);
                node.right = deserializeHelper(nodes);
                return node;
            }
        }
    }
}