// Url:https://leetcode.com/problems/reorder-list

/*
143. Reorder List
Medium

Given a singly linked list L: L0→L1→…→Ln-1→Ln,
reorder it to: L0→Ln→L1→Ln-1→L2→Ln-2→…

You may not modify the values in the list's nodes, only nodes itself may be changed.

Example 1:

Given 1->2->3->4, reorder it to 1->4->2->3.

Example 2:

Given 1->2->3->4->5, reorder it to 1->5->2->4->3.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem143 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n)
        // Space: O (1)
        public static void ReorderList (ListNode head) {
            if (head == null) {
                return;
            }

            ListNode slow = head;
            ListNode fast = head.next;

            while (fast != null && fast.next != null) {
                slow = slow.next;
                fast = fast.next.next;
            }

            ListNode middle = slow;
            ListNode head2 = Reverse(slow.next);
            slow.next = null;

            while (head != null && head2 != null) {
                ListNode temp1 = head.next;
                ListNode temp2 = head2.next;

                head2.next = head.next;
                head.next = head2;

                head = temp1;
                head2 = temp2;
            }
        }

        private static ListNode Reverse (ListNode head) {
            if (head == null) {
                return null;
            }

            ListNode current = head;
            ListNode prev = null;

            while (current != null) {
                ListNode temp = current.next;
                current.next = prev;
                prev = current;
                current = temp;
            }

            return prev;
        }
    }

    public class ListNode(int x)
    {
        public int val = x;
        public ListNode next;
    }
}