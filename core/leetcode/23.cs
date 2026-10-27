// Url:https://leetcode.com/problems/merge-k-sorted-lists

/*
23. Merge k Sorted Lists
Hard

Merge k sorted linked lists and return it as one sorted list. Analyze and describe its complexity.

Example:

Input:
[
  1->4->5,
  1->3->4,
  2->6
]
Output: 1->1->2->3->4->4->5->6
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem23 {
    public class Solution {
        public static void Init() {
            ListNode l1 = new(3)
            {
                next = new ListNode(9)
            };

            ListNode l2 = new(2)
            {
                next = new ListNode(5)
            };

            ListNode l3 = new(1)
            {
                next = new ListNode(8)
            };

            ListNode[] lists = [l1, l2, l3];
            ListNode result = MergeKLists (lists);
        }

        // Time: O (n log (k))
        // Space: O (1)
        public static ListNode MergeKLists (ListNode[] lists) {
            if (lists == null || lists.Length == 0) {
                return null;
            }

            return Helper(lists, 0, lists.Length - 1);
        }

        private static ListNode Helper (ListNode[] lists, int left, int right) {
            if (left == right) {
                return lists[left];
            }

            int mid = left + (right - left) / 2;

            var leftListNode = Helper(lists, left, mid);
            var rightListNode = Helper(lists, mid + 1, right);

            return MergeTwoLists(leftListNode, rightListNode);
        }

        private static ListNode MergeTwoLists (ListNode l1, ListNode l2) {
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
    }

    public class ListNode {
        public int val;
        public ListNode next;

        public ListNode (int x) {
            val = x;
        }
    }
}