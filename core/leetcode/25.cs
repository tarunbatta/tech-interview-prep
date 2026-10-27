// Url:https://leetcode.com/problems/reverse-nodes-in-k-group

/*
25. Reverse Nodes in k-Group
Hard

Given a linked list, reverse the nodes of a linked list k at a time and return its modified list.

k is a positive integer and is less than or equal to the length of the linked list. If the number of nodes is not a multiple of k then left-out nodes in the end should remain as it is.


Example:

Given this linked list: 1->2->3->4->5

For k = 2, you should return: 2->1->4->3->5

For k = 3, you should return: 3->2->1->4->5

Note:
	Only constant extra memory is allowed.
	You may not alter the values in the list's nodes, only nodes itself may be changed.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem25 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n)
        // Space: O (1)
        public static ListNode ReverseKGroup (ListNode head, int k) {
            if (head == null || head.next == null || k == 1) {
                return head;
            }

            ListNode begin = null;
            ListNode dummyhead = new(-1)
            {
                next = head
            };
            begin = dummyhead;
            int i = 0;

            while (head != null) {
                i++;

                if (i % k == 0) {
                    begin = reverse(begin, head.next);
                    head = begin.next;
                } else {
                    head = head.next;
                }
            }

            return dummyhead.next;
        }

        private static ListNode reverse (ListNode begin, ListNode end) {
            ListNode curr = begin.next;
            ListNode next, first;
            ListNode prev = begin;
            first = curr;

            while (curr != end) {
                next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }

            begin.next = prev;
            first.next = curr;

            return first;
        }
    }

    public class ListNode {
        public int val;
        public ListNode next;

        public ListNode (int x) {
            val = x;
        }
    }
}