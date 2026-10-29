/*
Return Kth to Last: 

Implement an algorithm to find the kth to last element of a singly linked list.
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.Ctci.c2q2 {
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

            Console.WriteLine ($"List: {a}");
            Node kth = ReturnKth (a, 2);
            Console.WriteLine ($"2nd to last: {kth?.val}"); // should be d
        }

        public static Node ReturnKth (Node node, int k) {
            if (node == null || k <= 0) return null;
            k--;
            Node first = node;
            Node last = node;
            for (int i = 0; i < k; i++) {
                if (last.next == null) return null;
                last = last.next;
            }
            while (last.next != null) {
                last = last.next;
                first = first.next;
            }
            return first;
        }
    }
}
