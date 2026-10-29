/*
Delete Middle Node: 

Implement an algorithm to delete a node in the middle (i.e., any node but
the first and last node, not necessarily the exact middle) of a singly linked list, given only access to
that node.

EXAMPLE
Input: the node c from the linked list a -> b -> c -> d -> e -> f
Result: nothing is returned, but the new linked list looks like a -> b -> d -> e -> f
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.Ctci.c2q3 {
    public class Solution {
        public class Node {
            public Node next;
            public char val;

            public Node (char val) {
                this.val = val;
            }

            public override string ToString () {
                StringBuilder sb = new StringBuilder ();
                Node temp = this;
                while (temp != null) {
                    sb.Append (temp.val);
                    temp = temp.next;
                }
                return sb.ToString ();
            }
        }

        public static void Init (string[] args = null) {
            Node a = new Node ('a');
            Node b = new Node ('b');
            Node c = new Node ('c');
            Node d = new Node ('d');
            Node e = new Node ('e');
            a.next = b;
            b.next = c;
            c.next = d;
            d.next = e;

            Console.WriteLine ($"Before: {a}");
            DeleteMiddle (c);
            Console.WriteLine ($"After deleting c: {a}");
        }

        public static bool DeleteMiddle (Node node) {
            if (node == null || node.next == null) {
                return false;
            }
            node.val = node.next.val;
            node.next = node.next.next;
            return true;
        }
    }
}
