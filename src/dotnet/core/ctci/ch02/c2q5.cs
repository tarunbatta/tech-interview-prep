/*
Sum Lists: 

You have two numbers represented by a linked list, where each node contains a single
digit. The digits are stored in reverse order, such that the 1 's digit is at the head of the list. Write a
function that adds the two numbers and returns the sum as a linked list.

EXAMPLE
Input: (7-> 1 -> 6) + (5 -> 9 -> 2).That is, 617 + 295.
Output: 2 -> 1 -> 9. That is, 912.
FOLLOW UP
Suppose the digits are stored in forward order. Repeat the above problem.
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.Ctci.c2q5 {
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
            Node a = new Node (7) { next = new Node (1) { next = new Node (6) } };
            Node d = new Node (5) { next = new Node (9) { next = new Node (2) } };

            Console.WriteLine ($"List A: {a}");
            Console.WriteLine ($"List B: {d}");
            Node sum = NodeSum (a, d, 0);
            Console.WriteLine ($"Sum: {sum}"); // 2 1 9 (912)
        }

        public static Node NodeSum (Node a, Node b, int carry) {
            if (a == null && b == null && carry == 0) {
                return null;
            }

            int value = carry;
            if (a != null) value += a.val;
            if (b != null) value += b.val;

            Node result = new Node (value % 10);
            if (a != null || b != null) {
                Node next = NodeSum (a?.next, b?.next, value / 10);
                result.next = next;
            }
            return result;
        }
    }
}
