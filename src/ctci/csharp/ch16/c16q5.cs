/*
LRU Cache: 

Design and build a "least recently used" cache, which evicts the least recently used
item. The cache should map from keys to values (allowing you to insert and retrieve a value associated
with a particular key) and have a max size. When it exceeds the max size, it should evict the least
recently used item.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c16q5 {
    public class Solution {
        public class LruCache<TKey, TValue> {
            public class Node {
                public TKey Key { get; }
                public TValue Value { get; set; }
                public Node Next { get; set; }
                public Node Prev { get; set; }

                public Node (TKey key, TValue value) {
                    Key = key;
                    Value = value;
                }
            }

            private readonly int _capacity;
            private readonly Dictionary<TKey, Node> _map = new Dictionary<TKey, Node> ();
            private Node _head;
            private Node _tail;

            public LruCache (int capacity) {
                _capacity = capacity;
            }

            public bool TryGet (TKey key, out TValue value) {
                if (_map.TryGetValue (key, out Node node)) {
                    Remove (node);
                    SetHead (node);
                    value = node.Value;
                    return true;
                }
                value = default;
                return false;
            }

            public void Set (TKey key, TValue value) {
                if (_map.TryGetValue (key, out Node oldNode)) {
                    oldNode.Value = value;
                    Remove (oldNode);
                    SetHead (oldNode);
                } else {
                    Node newNode = new Node (key, value);
                    if (_map.Count >= _capacity) {
                        _map.Remove (_tail.Key);
                        Remove (_tail);
                    }
                    SetHead (newNode);
                    _map[key] = newNode;
                }
            }

            private void Remove (Node node) {
                if (node.Prev != null) node.Prev.Next = node.Next;
                if (node.Next != null) node.Next.Prev = node.Prev;
                if (node == _head) _head = node.Next;
                if (node == _tail) _tail = node.Prev;
            }

            private void SetHead (Node node) {
                node.Prev = null;
                node.Next = _head;
                if (_head != null) _head.Prev = node;
                _head = node;
                if (_tail == null) _tail = _head;
            }
        }

        public static void Init (string[] args = null) {
            LruCache<int, string> cache = new LruCache<int, string> (2);
            cache.Set (1, "one");
            cache.Set (2, "two");
            cache.TryGet (1, out string v1);
            Console.WriteLine ($"Key 1: {v1}"); // one
            cache.Set (3, "three"); // evicts key 2
            Console.WriteLine ($"Key 2 exists: {cache.TryGet (2, out _)}"); // False
            Console.WriteLine ($"Key 3 exists: {cache.TryGet (3, out _)}"); // True
        }
    }
}
