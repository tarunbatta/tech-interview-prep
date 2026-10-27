// Url:https://leetcode.com/problems/insert-into-a-sorted-circular-linked-list

/*
708. Insert into a Sorted Circular Linked List
Medium

Given a Circular Linked List node, which is sorted in non-descending order, write a function to insert a value insertVal into the list such that it remains a sorted circular list. The given node can be a reference to any single node in the list and may not necessarily be the smallest value in the circular list.

If there are multiple suitable places for insertion, you may choose any place for insertion in order to maintain the sorted order. If the list is empty (i.e., the given node is null), you should create a new single circular list and return the reference to that single node. Otherwise, you should return the originally given node.

Example 1:
Input: head = [3,4,1], insertVal = 2
Output: [3,4,1,2]
Explanation: In the figure the list is: 3 -> 4 -> 1 -> (3). After insertion: 3 -> 4 -> 1 -> 2 -> (3).

Example 2:
Input: head = [], insertVal = 1
Output: [1]
Explanation: The list is empty (given head is null). We create a new single circular list and return the reference to that single node.

Example 3:
Input: head = [1], insertVal = 0
Output: [1,0]

Constraints:
- The number of nodes in the list is in the range [0, 5 * 10^4].
- -10^6 <= Node.val, insertVal <= 10^6
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem708 {
    public class Node {
        public int val;
        public Node next;

        public Node() { }

        public Node(int _val) {
            val = _val;
        }

        public Node(int _val, Node _next) {
            val = _val;
            next = _next;
        }
    }

    public class Solution {
        public void Init() {
            Node n1 = new(3);
            Node n2 = new(4);
            Node n3 = new(1);
            n1.next = n2;
            n2.next = n3;
            n3.next = n1;

            Node res = Insert(n1, 2);
            Console.WriteLine($"{res.val} -> {res.next.val} -> {res.next.next.val} -> {res.next.next.next.val}"); // 3 -> 4 -> 1 -> 2
        }

        // Time: O(n) where n is the number of nodes in the circular list
        // Space: O(1)
        public Node Insert(Node head, int insertVal) {
            if (head == null) {
                Node single = new(insertVal);
                single.next = single;
                return single;
            }

            Node prev = head;
            Node curr = head.next;

            do {
                if (prev.val <= insertVal && insertVal <= curr.val) {
                    break;
                }

                if (prev.val > curr.val) {
                    if (insertVal >= prev.val || insertVal <= curr.val) {
                        break;
                    }
                }

                prev = curr;
                curr = curr.next;
            } while (prev != head);

            prev.next = new Node(insertVal, curr);
            return head;
        }
    }
}