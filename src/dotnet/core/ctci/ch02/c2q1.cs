/*
Remove Dups: 

Write code to remove duplicates from an unsorted linked list.
FOLLOW UP
How would you solve this problem if a temporary buffer is not allowed?
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.Ctci.c2q1 {
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
            Node a = new Node ('F');
            Node b = new Node ('O');
            Node c = new Node ('L');
            Node d = new Node ('L');
            Node e = new Node ('O');
            Node f = new Node ('W');
            Node g = new Node (' ');
            Node h = new Node ('U');
            Node i = new Node ('P');
            a.next = b;
            b.next = c;
            c.next = d;
            d.next = e;
            e.next = f;
            f.next = g;
            g.next = h;
            h.next = i;

            Console.WriteLine ($"Before: {a}");
            RemoveDupes (a);
            Console.WriteLine ($"After: {a}");
        }

        public static void RemoveDupes (Node node) {
            if (node == null) return;
            HashSet<char> set = new HashSet<char> ();
            set.Add (node.val);
            Node prev = node;
            Node temp = node.next;
            while (temp != null) {
                if (set.Contains (temp.val)) {
                    prev.next = temp.next;
                } else {
                    set.Add (temp.val);
                    prev = temp;
                }
                temp = temp.next;
            }
        }
    }
}
