// Url:https://leetcode.com/problems/linked-list-random-node

/*
382. Linked List Random Node
Medium

Given a singly linked list, return a random node's value from the linked list. Each node must have the same probability of being chosen.

Follow up:
What if the linked list is extremely large and its length is unknown to you? Could you solve this efficiently without using extra space?


Example:
// Init a singly linked list [1,2,3].
ListNode head = new ListNode(1);
head.next = new(2);
head.next.next = new(3);
Solution solution = new Solution(head);

// getRandom() should return either 1, 2, or 3 randomly. Each element should have equal probability of returning.
solution.getRandom();
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem382 {
    public class Solution(ListNode head)
    {
        public static void Init () {
            Solution obj = new(null);
            int param_1 = GetRandom();
        }

        // Returns a random node's value.
        public static int GetRandom () {
            return 0;
        }
    }

    public class ListNode(int x)
    {
        public int val = x;
        public ListNode next;
    }
}