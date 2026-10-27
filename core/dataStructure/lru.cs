/*

*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.DataStructure.LRU {
    public class LRU {
        public static void Init (string[] args) {
            LRUCache obj = new(5);
            obj.Set (1, 1);
            Console.WriteLine (obj.Get (1));
            obj.Set (2, 2);
            obj.Set (3, 3);
            obj.Set (4, 4);
            obj.Set (5, 5);
            obj.Set (6, 6);
            Console.WriteLine (obj.Get (1));
            Console.WriteLine (obj.Get (3));
            Console.WriteLine (obj.Get (6));
        }

        public class Node {
            public int key { get; set; }
            public int value { get; set; }
            public Node previous { get; set; }
            public Node next { get; set; }
        }

        public class LRUCache {
            private int _capacity;
            private Dictionary<int, Node> _cache = null;
            private Node head;
            private Node tail;

            public LRUCache (int capacity) {
                if (capacity <= 0) {
                    throw new ArgumentOutOfRangeException ("capacity", "Capacity should be greater than zero");
                }

                this._capacity = capacity;
                this._cache = new Dictionary<int, Node> (this._capacity);
                this.head = null;
                this.tail = null;
            }

            public int Get (int key) {
                Node item;

                if (_cache.TryGetValue (key, out item)) {
                    MoveToHead (item);
                    return item.value;
                }

                return -1;
            }

            public void Set (int key, int value) {
                Node item;

                if (!_cache.TryGetValue (key, out item)) {
                    item = new Node () { key = key, value = value };

                    if (_cache.Count == this._capacity) {
                        _cache.Remove (tail.key);
                        this.tail = tail.previous;

                        if (this.tail != null) {
                            this.tail.next = null;
                        }
                    }

                    _cache.Add (key, item);
                }

                item.value = value;
                MoveToHead (item);

                if (this.head == null) {
                    this.head = item;
                }

                if (this.tail == null) {
                    this.tail = this.head;
                }
            }

            private void MoveToHead (Node item) {
                if (item == null || item == this.head) {
                    return;
                }

                var next = item.next;
                var previous = item.previous;

                if (next != null) {
                    next.previous = item.previous;
                }

                if (previous != null) {
                    previous.next = item.next;
                }

                item.previous = null;
                item.next = this.head;

                if (this.head != null) {
                    this.head.previous = item;
                    this.head = item;
                }

                if (this.tail == item) {
                    this.tail = previous;
                }
            }
        }
    }
}