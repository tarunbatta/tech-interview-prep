// Url:https://leetcode.com/problems/remove-nth-node-from-end-of-list

/*
19. Remove Nth Node From End of List
Medium

Given a linked list, remove the n-th node from the end of list and return its head.

Example:

Given linked list: 1->2->3->4->5, and n = 2.

After removing the second node from the end, the linked list becomes 1->2->3->5.


Note:

Given n will always be valid.

Follow up:

Could you do this in one pass?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem19 {
    public class Solution {
        public static void Init () {
            ListNode node = new(1)
            {
                next = new(2)
                {
                    next = new(3)
                }
            };
            node.next.next.next = new(4)
            {
                next = new(5)
            };

            Console.WriteLine (RemoveNthFromEnd_a(node, 2));

            Console.WriteLine (RemoveNthFromEnd_b(node, 2));
        }

        public static ListNode RemoveNthFromEnd_a (ListNode head, int n) {
            if (n <= 0) {
                return head;
            }

            int len = 0;
            ListNode current = head;

            while (current.next != null) {
                len++;
                current = current.next;
            }

            ListNode dummy = new(0)
            {
                next = head
            };
            current = dummy;

            for (int i = 0; i < len - n + 1; i++) {
                current = current.next;
            }

            if (current.next != null) {
                current.next = current.next.next;
            }

            return dummy.next;
        }

        public static ListNode RemoveNthFromEnd_b (ListNode head, int n) {
            if (n <= 0) {
                return head;
            }

            ListNode dummy = new(0)
            {
                next = head
            };

            ListNode first = dummy;
            ListNode second = dummy;

            for (int i = 1; i <= n + 1; i++) {
                first = first.next;
            }

            while (first != null) {
                first = first.next;
                second = second.next;
            }

            second.next = second.next.next;

            return dummy.next;
        }
    }

    public class ListNode(int x)
    {
        public int val = x;
        public ListNode next;
    }
}