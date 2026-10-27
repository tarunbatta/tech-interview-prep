// Url:https://leetcode.com/problems/reverse-linked-list

/*
206. Reverse Linked List
Easy

Reverse a singly linked list.

Example:

Input: 1->2->3->4->5->NULL
Output: 5->4->3->2->1->NULL


Follow up:

A linked list can be reversed either iteratively or recursively. Could you implement both?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem206 {
    public class Solution {
        public static void Init () {
            ListNode node = new(1)
            {
                next = new ListNode(2)
                {
                    next = new ListNode(3)
                }
            };
            node.next.next.next = new ListNode(4)
            {
                next = new ListNode(5)
            };

            Console.WriteLine (ReverseList_a(null));
            Console.WriteLine (ReverseList_a(node));

            Console.WriteLine (ReverseList_b(null));
            Console.WriteLine (ReverseList_b(node));
        }

        public static ListNode ReverseList_a (ListNode head) {
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

        public static ListNode ReverseList_b (ListNode head) {
            if (head == null || head.next == null) {
                return head;
            }

            ListNode p = ReverseList_b(head.next);
            head.next.next = head;
            head.next = null;

            return p;
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