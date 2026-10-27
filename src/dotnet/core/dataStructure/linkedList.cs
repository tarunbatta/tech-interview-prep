/*

*/

using System;

namespace InterviewPreperationGuide.Core.DataStructure.LinkedList {
    public class Node {
        public object data;
        public Node next;

        public Node () {
            this.data = null;
            this.next = null;
        }

        public Node (object item) {
            this.data = item;
            this.next = null;
        }
    }

    public class LinkedList {
        public Node head;

        public LinkedList () {
            this.head = null;
        }

        public void Display () {
            Node current = head;

            while (current.next != null) {
                Console.WriteLine (current.data.ToString ());
                current = current.next;
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

        public Node FindPrevious (object item) {
            Node current = head;

            while (current.next != null && current.next.data != item) {
                current = current.next;
            }

            if (current == head) {
                current = null;
            }

            return current;
        }

        public void Insert (object item, Node after) {
            Node newNode = new(item);
            Node current = Find (after.data);

            if (current != null) {
                newNode.next = current.next;
                current.next = newNode;
            }
        }

        public void Remove (object item) {
            Node prev = FindPrevious (item);

            if (prev != null) {
                prev.next = prev.next.next;
            }
        }

        public Node Reverse () {
            Node prev = null;
            Node next = null;
            Node current = head;

            while (current.next != null) {
                next = current.next;
                current.next = prev;
                prev = current;
                current = next;
            }

            head = prev;

            return head;
        }
    }
}