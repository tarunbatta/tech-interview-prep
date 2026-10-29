/*
Intersection: 

Given two (singly) linked lists, determine if the two lists intersect. Return the intersecting
node. Note that the intersection is defined based on reference, not value. That is, if the kth
node of the first linked list is the exact same node (by reference) as the jth node of the second
linked list, then they are intersecting.
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.Ctci.c2q7 {
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
            Node shared = new Node (7) { next = new Node (2) { next = new Node (1) } };
            Node list1 = new Node (3) { next = new Node (1) { next = new Node (5) { next = new Node (9) { next = shared } } } };
            Node list2 = new Node (4) { next = new Node (6) { next = shared } };

            Node inter = Intersection (list1, list2);
            Console.WriteLine ($"Intersection node value: {inter?.val}"); // 7
        }

        public static Node Intersection (Node a, Node b) {
            if (a == null || b == null) return null;

            int aLength = 0;
            int bLength = 0;
            Node aCurr = a;
            Node bCurr = b;

            while (aCurr != null) {
                aLength++;
                aCurr = aCurr.next;
            }
            while (bCurr != null) {
                bLength++;
                bCurr = bCurr.next;
            }

            Node longer = aLength > bLength ? a : b;
            Node shorter = aLength > bLength ? b : a;
            int diff = Math.Abs (aLength - bLength);

            while (diff > 0) {
                longer = longer.next;
                diff--;
            }

            while (longer != null && shorter != null) {
                if (ReferenceEquals (longer, shorter)) {
                    return longer;
                }
                longer = longer.next;
                shorter = shorter.next;
            }

            return null;
        }
    }
}
