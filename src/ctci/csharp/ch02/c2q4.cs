/*
Partition: 

Write code to partition a linked list around a value x, such that all nodes less than x come
before all nodes greater than or equal to x. If x is contained within the list the values of x only need
to be after the elements less than x (see below). The partition element x can appear anywhere in the
"right partition"; it does not need to appear between the left and right partitions.

EXAMPLE
Input: 3 -> 5 -> 8 -> 5 -> 10 -> 2 -> 1 [partition= 5]
Output: 3 -> 1 -> 2 -> 10 -> 5 -> 5 -> 8
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.Ctci.c2q4 {
    public class Solution {
        public class Node {
            public Node next;
            public int val;

            public Node (int val) {
                this.val = val;
            }

            public override string ToString () {
                StringBuilder sb = new StringBuilder ();
                Node temp = this;
                while (temp != null) {
                    sb.Append (temp.val).Append (' ');
                    temp = temp.next;
                }
                return sb.ToString ().TrimEnd ();
            }
        }

        public static void Init (string[] args = null) {
            Node a = new Node (3);
            Node b = new Node (5);
            Node c = new Node (8);
            Node d = new Node (5);
            Node e = new Node (10);
            Node f = new Node (2);
            Node g = new Node (1);
            a.next = b;
            b.next = c;
            c.next = d;
            d.next = e;
            e.next = f;
            f.next = g;

            Console.WriteLine ($"Before: {a}");
            Node result = Partition (a, 5);
            Console.WriteLine ($"After partition (5): {result}");
        }

        public static Node Partition (Node node, int partition) {
            Node lessHead = null;
            Node lessTail = null;
            Node moreHead = null;
            Node moreTail = null;

            Node current = node;
            while (current != null) {
                Node next = current.next;
                current.next = null;

                if (current.val < partition) {
                    if (lessHead == null) {
                        lessHead = current;
                        lessTail = lessHead;
                    } else {
                        lessTail.next = current;
                        lessTail = current;
                    }
                } else {
                    if (moreHead == null) {
                        moreHead = current;
                        moreTail = moreHead;
                    } else {
                        moreTail.next = current;
                        moreTail = current;
                    }
                }
                current = next;
            }

            if (lessHead == null) {
                return moreHead;
            }

            lessTail.next = moreHead;
            return lessHead;
        }
    }
}
