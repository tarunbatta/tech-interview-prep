// Url:https://leetcode.com/problems/populating-next-right-pointers-in-each-node-ii

/*
117. Populating Next Right Pointers in Each Node II
Medium

Given a binary tree

struct Node {
  int val;
  Node *left;
  Node *right;
  Node *next;
}


Populate each next pointer to point to its next right node. If there is no next right node, the next pointer should be set to NULL.
Initially, all next pointers are set to NULL.

Example:

Input: {"$id":"1","left":{"$id":"2","left":{"$id":"3","left":null,"next":null,"right":null,"val":4},"next":null,"right":{"$id":"4","left":null,"next":null,"right":null,"val":5},"val":2},"next":null,"right":{"$id":"5","left":null,"next":null,"right":{"$id":"6","left":null,"next":null,"right":null,"val":7},"val":3},"val":1}

Output: {"$id":"1","left":{"$id":"2","left":{"$id":"3","left":null,"next":{"$id":"4","left":null,"next":{"$id":"5","left":null,"next":null,"right":null,"val":7},"right":null,"val":5},"right":null,"val":4},"next":{"$id":"6","left":null,"next":null,"right":{"$ref":"5"},"val":3},"right":{"$ref":"4"},"val":2},"next":null,"right":{"$ref":"6"},"val":1}

Explanation: Given the above binary tree (Figure A), your function should populate each next pointer to point to its next right node, just like in Figure B.
 
Note:
	You may only use constant extra space.
	Recursive approach is fine, implicit stack space does not count as extra space for this problem.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem117 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n)
        // Space: O (n)
        public static Node Connect_a (Node root) {
            if (root == null) {
                return root;
            }

            Queue<Node> q = new Queue<Node> ();
            q.Enqueue (root);

            while (q.Count > 0) {
                int count = q.Count;

                for (int i = 0; i < count; i++) {
                    Node node = q.Dequeue ();

                    if (i < count - 1) {
                        node.next = q.Peek ();
                    }

                    if (node.left != null) {
                        q.Enqueue (node.left);
                    }

                    if (node.right != null) {
                        q.Enqueue (node.right);
                    }
                }
            }

            return root;
        }

        // Time: O (n)
        // Space: O (1)
        public static Node Connect_b (Node root) {
            if (root == null) {
                return root;
            }

            Node head = root; //The left most node in the lower level
            Node prev = null; //The previous node in the lower level
            Node curr = null; //The current node in the upper level

            while (head != null) {
                curr = head;
                prev = null;
                head = null;

                while (curr != null) {
                    if (curr.left != null) {
                        if (prev != null)
                            prev.next = curr.left;
                        else
                            head = curr.left;
                        prev = curr.left;
                    }

                    if (curr.right != null) {
                        if (prev != null)
                            prev.next = curr.right;
                        else
                            head = curr.right;
                        prev = curr.right;
                    }

                    curr = curr.next;
                }
            }

            return root;
        }

        public class Node {
            public int val;
            public Node left;
            public Node right;
            public Node next;

            public Node () {

            }

            public Node (int _val, Node _left, Node _right, Node _next) {
                val = _val;
                left = _left;
                right = _right;
                next = _next;
            }
        }
    }
}