// Url:https://leetcode.com/problems/merge-two-sorted-lists

/*
21. Merge Two Sorted Lists
Easy

Merge two sorted linked lists and return it as a new list. The new list should be made by splicing together the nodes of the first two lists.

Example:
Input: 1->2->4, 1->3->4
Output: 1->1->2->3->4->4
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem21 {
    public class Solution {
        public static void Init () {
            ListNode l1 = new(1)
            {
                next = new ListNode(2)
                {
                    next = new ListNode(4)
                }
            };

            ListNode l2 = new(1)
            {
                next = new ListNode(3)
                {
                    next = new ListNode(4)
                }
            };

            Console.WriteLine (MergeTwoLists_a(null, null));
            Console.WriteLine (MergeTwoLists_a(l1, l2));
            Console.WriteLine (MergeTwoLists_a(new ListNode (2), new ListNode (1)));
            Console.WriteLine (MergeTwoLists_a(null, new ListNode (0)));

            Console.WriteLine (MergeTwoLists_b(null, null));
            Console.WriteLine (MergeTwoLists_b(l1, l2));
            Console.WriteLine (MergeTwoLists_b(new ListNode (2), new ListNode (1)));
            Console.WriteLine (MergeTwoLists_b(null, new ListNode (0)));
        }

        public static ListNode MergeTwoLists_a (ListNode l1, ListNode l2) {
            if (l1 == null && l2 == null) {
                return null;
            }

            if (l1 == null) {
                return l2;
            }

            if (l2 == null) {
                return l1;
            }

            ListNode head = new(0);
            ListNode node = head;

            while (l1 != null && l2 != null) {
                if (l1.val < l2.val) {
                    node.next = l1;
                    l1 = l1.next;
                } else {
                    node.next = l2;
                    l2 = l2.next;
                }

                node = node.next;
            }

            if (l1 != null) {
                node.next = l1;
            }

            if (l2 != null) {
                node.next = l2;
            }

            return head.next;
        }

        public static ListNode MergeTwoLists_b (ListNode l1, ListNode l2) {
            if (l1 == null) {
                return l2;
            }

            if (l2 == null) {
                return l1;
            }

            if (l1.val < l2.val) {
                l1.next = MergeTwoLists_b(l1.next, l2);
                return l1;
            } else {
                l2.next = MergeTwoLists_b(l1, l2.next);
                return l2;
            }
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