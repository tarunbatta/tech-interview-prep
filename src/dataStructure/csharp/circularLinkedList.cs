/*

*/

using System;

namespace InterviewPreperationGuide.Core.DataStructure.CircularLinkedList {
    public class Node {
        public object data;
        public Node prev;
        public Node next;

        public Node () {
            this.data = null;
            this.prev = null;
            this.next = null;
        }

        public Node (object item) {
            this.data = item;
            this.prev = null;
            this.next = null;
        }
    }

    public class CircularLinkedList {
        public Node head;

        public CircularLinkedList () {
            this.head = null;
        }

        public bool IsEmpty () {
            return (head.next == null);
        }

        public void Display () {
            Node current = head.next;

            while (current != head) {
                Console.WriteLine (current.data.ToString ());
                current = current.next;
            }
        }

        public Node Find (object item) {
            Node current = head.next;

            while (current != head) {
                if (current.data == item) {
                    return current;
                }

                current = current.next;
            }

            return null;
        }

        public Node FindPrevious (object item) {
            Node current = head.next;

            while (current != head && current.next.data != item) {
                current = current.next;
            }

            if (current == head.next) {
                current = null;
            }

            return current;
        }

        public void Insert (object item, Node after) {
            Node newNode = new(item);
            Node current = Find (after.data);

            if (current != null) {
                newNode.prev = current;
                newNode.next = current.next;
                current.next = newNode;
            }
        }

        public Node Previous (object item) {
            Node current = head;

            while (current.next != null && current.next.data != item) {
                current = current.next;
            }

            if (current == head) {
                current = null;
            }

            return current;
        }

        public void Remove (object item) {
            Node current = FindPrevious (item);

            if (current.next != null) {
                current.next = current.next.next;
            }
        }
    }
}