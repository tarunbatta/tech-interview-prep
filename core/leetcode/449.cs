// Url:https://leetcode.com/problems/serialize-and-deserialize-bst

/*
449. Serialize and Deserialize BST
Medium

Serialization is the process of converting a data structure or object into a sequence of bits so that it can be stored in a file or memory buffer, or transmitted across a network connection link to be reconstructed later in the same or another computer environment.

Design an algorithm to serialize and deserialize a binary search tree. There is no restriction on how your serialization/deserialization algorithm should work. You just need to ensure that a binary search tree can be serialized to a string and this string can be deserialized to the original tree structure.

The encoded string should be as compact as possible.

Note: Do not use class member/global/static variables to store states. Your serialize and deserialize algorithms should be stateless.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem449 {
    public class Solution {
        public static void Init () {
            Codec codec = new Codec ();
            Codec.deserialize(Codec.serialize(null));
        }
    }
    public class Codec {
        // Encodes a tree to a single string.    
        public static string serialize (TreeNode root) {
            return null;
        }

        // Decodes your encoded data to tree.    
        public static TreeNode deserialize (string data) {
            return null;
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
}