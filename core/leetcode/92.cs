// Url: https://leetcode.com/problems/reverse-linked-list-ii/

/*
92. Reverse Linked List II
Medium

Reverse a linked list from position m to n. Do it in one-pass.

Note: 1 ≤ m ≤ n ≤ length of list.

Example:

Input: 1->2->3->4->5->NULL, m = 2, n = 4
Output: 1->4->3->2->5->NULL
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem92 {
    public class Solution {
        public static void Init () {
            ListNode l = new(1)
            {
                next = new ListNode(2)
                {
                    next = new ListNode(3)
                }
            };
            l.next.next.next = new ListNode(4)
            {
                next = new ListNode(5)
            };

            Console.WriteLine (ReverseBetween(null, 2, 4));
            Console.WriteLine (ReverseBetween(null, 3, 3));
            Console.WriteLine (ReverseBetween(null, 4, 3));
            Console.WriteLine (ReverseBetween(l, 2, 4));
            Console.WriteLine (ReverseBetween(l, 3, 3));
        }

        // Time: O (n)
        // Space: O (1)
        public static ListNode ReverseBetween (ListNode head, int m, int n) {
            if (head == null || m >= n) {
                return head;
            }

            ListNode result = new(0)
            {
                next = head
            };
            ListNode node = result;

            while (m > 1) {
                node = node.next;
                m--;
                n--;
            }

            ListNode prev = node;
            ListNode current = node.next;
            ListNode tail = node.next;
            ListNode temp = null;

            while (n > 0) {
                temp = current.next;
                current.next = prev;
                prev = current;
                current = temp;
                n--;
            }

            if (node != null) {
                node.next = prev;
            } else {
                head = prev;
            }

            tail.next = current;

            return result.next;
        }
    }

    public class ListNode {
        public int val;
        public ListNode next;
        public ListNode (int x) { val = x; }
    }
}