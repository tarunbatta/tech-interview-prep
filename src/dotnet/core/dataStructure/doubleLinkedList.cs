/*

*/

using System;

namespace InterviewPreperationGuide.Core.DataStructure.DoubleLinkedList {
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

    public class DoubleLinkedList {
        public Node head;

        public DoubleLinkedList () {
            this.head = null;
        }

        public void Display () {
            Node current = head;

            while (current.next != null) {
                Console.WriteLine (current.data.ToString ());
                current = current.next;
            }
        }

        public void DisplayReverse () {
            Node current = FindLast ();

            while (current.prev != null) {
                Console.WriteLine (current.data.ToString ());
                current = current.prev;
            }
        }

        public Node Find (object item) {
            Node current = head;

            while (current.next != null) {
                if (current.data == item) {
                    return current;
                }

                current = current.next;
            }

            return null;
        }

        public Node FindLast () {
            Node current = head;

            if (current.next != null) {
                current = current.next;
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
            Node current = Find (item);

            if (current != null) {
                current.prev.next = current.next;
                current.next.prev = current.prev;
                current.prev = null;
                current.next = null;
            }
        }
    }
}