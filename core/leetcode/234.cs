// Url:https://leetcode.com/problems/palindrome-linked-list

/*
234. Palindrome Linked List
Easy

Given a singly linked list, determine if it is a palindrome.

Example 1:

Input: 1->2
Output: false

Example 2:

Input: 1->2->2->1
Output: true

Follow up:
Could you do it in O(n) time and O(1) space?
*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.LeetCode.problem234 {
    public class Solution {
        public static void Init () {
            ListNode l1 = new ListNode (1);
            l1.next = new ListNode (2);

            ListNode l2 = new ListNode (1);
            l2.next = new ListNode (2);
            l2.next.next = new ListNode (2);
            l2.next.next.next = new ListNode (1);

            Console.WriteLine (IsPalindrome (null));
            Console.WriteLine (IsPalindrome (new ListNode (0)));
            Console.WriteLine (IsPalindrome (l1));
            Console.WriteLine (IsPalindrome (l2));
        }

        // Time: O (n)
        // Space: O (1)
        public static bool IsPalindrome (ListNode head) {
            if (head == null) {
                return true;
            }

            bool result = true;
            int len = 0;
            ListNode node = head;

            while (node != null) {
                len++;
                node = node.next;
            }

            if (len == 1) {
                return true;
            }

            node = head;

            ListNode prev = null;

            for (int i = 0; i < len / 2; i++) {
                ListNode temp = node.next;
                node.next = prev;
                prev = node;
                node = temp;
            }

            ListNode reverseHead = prev;

            if (len % 2 != 0) {
                node = node.next;
            }

            while (node != null) {
                if (node.val != reverseHead.val) {
                    return false;
                }

                node = node.next;
                reverseHead = reverseHead.next;
            }

            return result;
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