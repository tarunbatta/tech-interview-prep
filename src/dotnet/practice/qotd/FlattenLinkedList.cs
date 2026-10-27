using System;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Flatten a Linked List
    ///
    /// Given a linked list, in addition to the next pointer, each node has a child pointer that can point to a separate list.
    /// With the head node, flatten the list to a single-level linked list.
    ///
    /// 1 -> 2 -> 3 -> 4
    ///      |		   |
    ///      V         V
    ///      5 -> 6	   7
    ///      |		   |
    ///      V         V
    ///      8		   9
    ///
    /// For instanec, the above linked list should be flattened to 1 -> 2 -> 3 -> 4 -> 5 -> 6 -> 7 -> 8 -> 9.
    /// The idea is to flatten the linked list by level. User additional space if needed.
    ///
    /// Follow up : Find the solution that can do the operation in-place without external storage.
    /// </summary>
    public class FlattenLinkedList {
        public static void Init (string[] args) {
            ListChildNode root = new ListChildNode (1);
            root.next = new ListChildNode (2);
            root.next.child = new ListChildNode (5);
            root.next.child.next = new ListChildNode (6);
            root.next.child.child = new ListChildNode (8);
            root.next.next = new ListChildNode (3);
            root.next.next.next = new ListChildNode (4);
            root.next.next.next.child = new ListChildNode (7);
            root.next.next.next.child.child = new ListChildNode (9);

            Console.WriteLine (PrintListChildNode (FlattenALinkedList (root)));
        }

        public static ListChildNode FlattenALinkedList (ListChildNode node) {
            if (node == null) {
                return null;
            }

            ListChildNode head = node;
            ListChildNode tail = node;

            while (tail.next != null) {
                tail = tail.next;
            }

            while (node != tail) {
                if (node.child != null) {
                    tail.next = node.child;

                    ListChildNode temp = node.child;
                    while (temp.next != null) {
                        temp = temp.next;
                    }
                    tail = temp;
                }

                node = node.next;
            }

            return head;
        }

        private static string PrintListChildNode (ListChildNode node) {
            string result = string.Empty;

            while (node != null) {
                if (string.IsNullOrEmpty (result)) {
                    result = Convert.ToString (node.val);
                } else {
                    result = result + "," + node.val;
                }

                node = node.next;
            }

            return result;
        }
    }

    public class ListChildNode {
        public int val;
        public ListChildNode next;
        public ListChildNode child;

        public ListChildNode (int val = 0, ListChildNode next = null, ListChildNode child = null) {
            this.val = val;
            this.next = next;
            this.child = child;
        }
    }
}