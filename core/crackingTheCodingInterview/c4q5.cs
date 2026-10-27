/*
Validate BST: Implement a function to check if a binary tree is a binary search tree.
*/

using System;

namespace InterviewPreperationGuide.Core.CrackingTheCodingInterview.c4q5 {
    public class Solution {
        public static BstNode previousNode = null;

        public static void Init (string[] args) {
            BstNode root_a = new(20)
            {
                left = new BstNode(10),
                right = new BstNode(30)
            };
            root_a.left.left = new BstNode (5);
            root_a.left.right = new BstNode (15);
            root_a.right.left = new BstNode (25);
            root_a.right.right = new BstNode (35);

            BstNode root_b = new(20)
            {
                left = new BstNode(10),
                right = new BstNode(30)
            };
            root_b.left.left = new BstNode (15);
            root_b.left.right = new BstNode (15);
            root_b.right.left = new BstNode (25);
            root_b.right.right = new BstNode (35);

            Console.WriteLine (IsBst_InOrder (root_a));
            Console.WriteLine (IsBst_InOrder (root_b));

            Console.WriteLine (IsBst_MinMax (root_a, Int32.MinValue, Int32.MaxValue));
            Console.WriteLine (IsBst_MinMax (root_b, Int32.MinValue, Int32.MaxValue));
        }

        public static bool IsBst_InOrder (BstNode node) {
            if (node != null) {
                if (!IsBst_InOrder (node.left)) {
                    return false;
                }

                if (previousNode != null && previousNode.data >= node.data) {
                    return false;
                }

                previousNode = node;
                return IsBst_InOrder (node.right);
            }

            return true;
        }

        public static bool IsBst_MinMax (BstNode node, int min, int max) {
            if (node != null) {
                if (node.data <= Int32.MinValue ||
                    node.data >= Int32.MaxValue) {
                    return false;
                }

                if (!IsBst_MinMax (node.left, Int32.MinValue, node.data) ||
                    !IsBst_MinMax (node.right, node.data, Int32.MaxValue)) {
                    return false;
                }
            }

            return true;
        }
    }

    public class BstNode {
        public int data { get; set; }
        public BstNode left { get; set; }
        public BstNode right { get; set; }

        public BstNode (int data) {
            this.data = data;
            this.left = null;
            this.right = null;
        }
    }
}