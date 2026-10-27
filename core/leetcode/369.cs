// Url: https://leetcode.com/problems/plus-one-linked-list

/*
369. Plus One Linked List
Medium

Given a non-negative integer represented as a linked list of digits, plus one to the integer.

The digits are stored such that the most significant digit is at the head of the list.

Example 1:
Input: head = [1,2,3]
Output: [1,2,4]

Example 2:
Input: head = [0]
Output: [1]

Example 3:
Input: head = [9,9]
Output: [1,0,0]

Constraints:
The number of nodes in the linked list is in the range [1, 100].
0 <= Node.val <= 9
The number represented by the linked list does not contain leading zeros except for the zero itself.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem369
{
    public class ListNode(int val = 0, ListNode next = null)
    {
        public int val = val;
        public ListNode next = next;
    }

    public class Solution
    {
        public static void Init()
        {
            ListNode head1 = new(1, new(2, new(3)));
            PrintList(PlusOne(head1)); // 1 -> 2 -> 4

            ListNode head2 = new(9, new(9));
            PrintList(PlusOne(head2)); // 1 -> 0 -> 0
        }

        // Time: O(n) where n is the number of nodes in the list
        // Space: O(1) in-place modification
        public static ListNode PlusOne(ListNode head)
        {
            ListNode dummy = new(0, head);
            ListNode notNine = dummy;

            // Find the rightmost node that is not 9
            for (ListNode curr = head; curr != null; curr = curr.next)
            {
                if (curr.val != 9)
                {
                    notNine = curr;
                }
            }

            // Increment the rightmost non-9 node
            notNine.val++;

            // Set all trailing 9s to 0
            for (ListNode curr = notNine.next; curr != null; curr = curr.next)
            {
                curr.val = 0;
            }

            return dummy.val == 1 ? dummy : dummy.next;
        }

        private static void PrintList(ListNode head)
        {
            for (ListNode curr = head; curr != null; curr = curr.next)
            {
                Console.Write(curr.val + (curr.next != null ? " -> " : "\n"));
            }
        }
    }
}