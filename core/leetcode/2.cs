// Url: https://leetcode.com/problems/add-two-numbers/

/*
2. Add Two Numbers
Medium

You are given two non-empty linked lists representing two non-negative integers. The digits are stored in reverse order and each of their nodes contain a single digit. Add the two numbers and return it as a linked list.

You may assume the two numbers do not contain any leading zero, except the number 0 itself.

Example:

Input: (2 -> 4 -> 3) + (5 -> 6 -> 4)
Output: 7 -> 0 -> 8
Explanation: 342 + 465 = 807.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem2
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }

    public class Solution
    {
        public static void Init()
        {
            ListNode l1 = new ListNode(2);
            l1.next = new ListNode(4);
            l1.next.next = new ListNode(3);

            ListNode l2 = new ListNode(5);
            l2.next = new ListNode(6);
            l2.next.next = new ListNode(4);
            Console.WriteLine(AddTwoNumbers(l1, l2));

            ListNode l3 = new ListNode(1);
            l3.next = new ListNode(8);

            ListNode l4 = new ListNode(0);
            Console.WriteLine(AddTwoNumbers(l3, l4));
        }

        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode result = new ListNode(0);

            ListNode a = l1;
            ListNode b = l2;
            ListNode current = result;
            int carry = 0;

            while (a != null || b != null)
            {
                int x = a != null ? a.val : 0;
                int y = b != null ? b.val : 0;
                int sum = carry + x + y;
                carry = sum / 10;

                current.next = new ListNode(sum % 10);
                current = current.next;

                if (a != null)
                {
                    a = a.next;
                }

                if (b != null)
                {
                    b = b.next;
                }
            }

            if (carry > 0)
            {
                current.next = new ListNode(carry);
            }

            return result.next;
        }
    }
}