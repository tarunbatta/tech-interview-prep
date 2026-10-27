// Url:https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii

/*
82. Remove Duplicates from Sorted List II
Medium

Given a sorted linked list, delete all nodes that have duplicate numbers, leaving only distinct numbers from the original list.

Example 1:

Input: 1->2->3->3->4->4->5
Output: 1->2->5


Example 2:

Input: 1->1->1->2->3
Output: 2->3
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem82 {
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

    public class ListNode {
        public int val;
        public ListNode next;

        public ListNode (int x) {
            val = x;
        }
    }
}