// Url:https://leetcode.com/problems/remove-duplicates-from-sorted-list

/*
83. Remove Duplicates from Sorted List
Easy

Given a sorted linked list, delete all duplicates such that each element appear only once.

Example 1:

Input: 1->1->2
Output: 1->2


Example 2:

Input: 1->1->2->3->3
Output: 1->2->3
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem83 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static ListNode DeleteDuplicates (ListNode head) {
            return null;
        }
    }

    public class ListNode(int x)
    {
        public int val = x;
        public ListNode next;
    }
}