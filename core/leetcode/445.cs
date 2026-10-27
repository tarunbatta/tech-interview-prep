// Url:https://leetcode.com/problems/add-two-numbers-ii

/*
445. Add Two Numbers II
Medium

You are given two non-empty linked lists representing two non-negative integers. The most significant digit comes first and each of their nodes contain a single digit. Add the two numbers and return it as a linked list.

You may assume the two numbers do not contain any leading zero, except the number 0 itself.

Follow up:
What if you cannot modify the input lists? In other words, reversing the lists is not allowed.

Example:
Input: (7 -> 2 -> 4 -> 3) + (5 -> 6 -> 4)
Output: 7 -> 8 -> 0 -> 7
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem445 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static ListNode AddTwoNumbers_b (ListNode l1, ListNode l2) {
            int n1 = getListLength(l1);
            int n2 = getListLength(l2);

            ListNode curr1 = l1;
            ListNode curr2 = l2;
            ListNode result = null;

            while (n1 > 0 && n2 > 0) {
                int sum = 0;

                if (n1 >= n2) {
                    sum += curr1.val;
                    curr1 = curr1.next;
                    n1--;
                }

                if (n2 > n1) {
                    sum += curr2.val;
                    curr2 = curr2.next;
                    n2--;
                }

                result = addToFront(sum, result);
            }

            curr1 = result;
            result = null;
            int carry = 0;

            while (curr1 != null) {
                curr1.val += carry;
                carry = curr1.val / 10;
                result = addToFront(curr1.val % 10, result);
                curr1 = curr1.next;
            }

            if (carry > 0) {
                result = addToFront(1, result);
            }

            return result;
        }

        private static int getListLength (ListNode head) {
            int result = 0;

            ListNode current = head;
            while (current != null) {
                result++;
                current = current.next;
            }

            return result;
        }

        private static ListNode addToFront (int val, ListNode head) {
            ListNode temp = new ListNode (val);
            temp.next = head;
            return temp;
        }

        public static ListNode AddTwoNumbers_a (ListNode l1, ListNode l2) {
            ListNode a = ReverseList(l1);
            ListNode b = ReverseList(l2);
            ListNode n = new ListNode (0);
            ListNode current = n;
            int carry = 0;

            while (a != null || b != null) {
                int i = a != null ? a.val : 0;
                int j = b != null ? b.val : 0;

                int sum = i + j + carry;
                carry = sum / 10;
                sum = sum % 10;

                current.next = new ListNode (sum);
                current = current.next;

                if (a != null) {
                    a = a.next;
                }

                if (b != null) {
                    b = b.next;
                }
            }

            if (carry > 0) {
                current.next = new ListNode (carry);
            }

            return ReverseList(n.next);
        }

        private static ListNode ReverseList (ListNode head) {
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

    public class ListNode {
        public int val;
        public ListNode next;

        public ListNode (int x) {
            val = x;
        }
    }
}