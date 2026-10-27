// Url: https://leetcode.com/problems/convert-binary-search-tree-to-sorted-doubly-linked-list

/*
426. Convert Binary Search Tree to Sorted Doubly Linked List
Medium

Convert a Binary Search Tree to a sorted Circular Doubly-Linked List in place.

You can think of the left and right pointers as synonymous to the predecessor and successor pointers in a doubly-linked list. For a circular doubly linked list, the predecessor of the first element is the last element, and the successor of the last element is the first element.

We want to do the transformation in place. After the transformation, the left pointer of the tree node should point to its predecessor, and the right pointer should point to its successor. You should return the pointer to the smallest element of the linked list.

Example 1:
Input: root = [4,2,5,1,3]
Output: [1,2,3,4,5]
Explanation: The figure shows the transformed BST. The solid line indicates the successor relationship, while the dashed line indicates the predecessor relationship.

Example 2:
Input: root = [2,1,3]
Output: [1,2,3]

Constraints:
The number of nodes in the tree is in the range [0, 2000].
-1000 <= Node.val <= 1000
All the values of the tree are unique.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem426
{
    public class Node
    {
        public int val;
        public Node left;
        public Node right;

        public Node(int val = 0, Node left = null, Node right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class Solution
    {
        private Node _first;
        private Node _last;

        public void Init()
        {
            Node root = new(4)
            {
                left = new(2)
                {
                    left = new(1),
                    right = new(3)
                },
                right = new(5)
            };

            Node head = TreeToDoublyList(root);

            if (head != null)
            {
                Node curr = head;
                do
                {
                    Console.Write(curr.val + " ");
                    curr = curr.right;
                } while (curr != head);
                Console.WriteLine();
            }
        }

        // Time: O(n) where n is the number of nodes in the BST (standard inorder traversal)
        // Space: O(h) where h is tree height for the recursion call stack
        public Node TreeToDoublyList(Node root)
        {
            if (root == null)
            {
                return null;
            }

            _first = null;
            _last = null;

            Inorder(root);

            // Connect head and tail into a circular doubly linked list
            _last.right = _first;
            _first.left = _last;

            return _first;
        }

        private void Inorder(Node node)
        {
            if (node == null)
            {
                return;
            }

            Inorder(node.left);

            if (_last != null)
            {
                _last.right = node;
                node.left = _last;
            }
            else
            {
                _first = node;
            }

            _last = node;

            Inorder(node.right);
        }
    }
}