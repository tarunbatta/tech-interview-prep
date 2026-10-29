/*
Loop Detection: 

Given a circular linked list, implement an algorithm that returns the node at the
beginning of the loop.

DEFINITION
Circular linked list: A (corrupt) linked list in which a node's next pointer points to an earlier node, so
as to make a loop in the linked list.

EXAMPLE
Input: A -> B -> C -> D -> E -> C [the same C as earlier]
Output: C
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c2q8 {
    public class Solution {
        public class Node {
            public Node next;
            public int val;

            public Node (int val) {
                this.val = val;
            }

            public override string ToString () {
                return val.ToString ();
            }
        }

        public static void Init (string[] args = null) {
            Node a = new Node (1);
            Node b = new Node (2);
            Node c = new Node (3);
            Node d = new Node (4);
            Node e = new Node (5);
            a.next = b;
            b.next = c;
            c.next = d;
            d.next = e;
            e.next = c; // Loop at C (3)

            Node loopStart = FindBeginning (a);
            Console.WriteLine ($"Loop starts at: {loopStart?.val}"); // 3
        }

        public static Node FindBeginning (Node head) {
            Node slow = head;
            Node fast = head;

            while (fast != null && fast.next != null) {
                slow = slow.next;
                fast = fast.next.next;
                if (fast == slow) {
                    break;
                }
            }

            if (fast == null || fast.next == null) {
                return null;
            }

            slow = head;
            while (slow != fast) {
                slow = slow.next;
                fast = fast.next;
            }
            return fast;
        }
    }
}
