// Url:https://leetcode.com/problems/lru-cache

/*
146. LRU Cache
Medium

Design and implement a data structure for Least Recently Used (LRU) cache. It should support the following operations: get and put.

get(key) - Get the value (will always be positive) of the key if the key exists in the cache, otherwise return -1.
put(key, value) - Set or insert the value if the key is not already present. When the cache reached its capacity, it should invalidate the least recently used item before inserting a new item.

The cache is initialized with a positive capacity.

Follow up:
Could you do both operations in O(1) time complexity?

Example:

LRUCache cache = new LRUCache(2);

cache.put(1, 1);
cache.put(2, 2);
cache.get(1);       // returns 1
cache.put(3, 3);    // evicts key 2
cache.get(2);       // returns -1 (not found)
cache.put(4, 4);    // evicts key 1
cache.get(1);       // returns -1 (not found)
cache.get(3);       // returns 3
cache.get(4);       // returns 4
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem146 {
    public class Solution {
        public static void Init () {
            // LRUCache obj = new LRUCache(capacity);
            // int param_1 = obj.Get(key);
            // obj.Put(key, value);
        }
    }
}
public class Node {
    public int key;
    public int val;
    public Node prev;
    public Node next;

    public Node (int key, int val) {
        this.key = key;
        this.val = val;
    }
}

public class LRUCache {
    int _capacity;
    Dictionary<int, Node> _cache;
    Node head;
    Node tail;

    public LRUCache (int capacity) {
        if (capacity <= 0) {
            throw new Exception ();
        }

        this._capacity = capacity;
        this._cache = new Dictionary<int, Node> (this._capacity);
        this.head = null;
        this.tail = null;
    }

    public int Get (int key) {
        Node item;
        if (_cache.TryGetValue (key, out item)) {
            moveToHead (item);
            return item.val;
        }

        return -1;
    }

    public void Put (int key, int value) {
        Node item;

        if (!_cache.TryGetValue (key, out item)) {
            item = new Node (key, value);

            if (_cache.Count == _capacity) {
                _cache.Remove (tail.key);
                tail = tail.prev;

                if (tail != null) {
                    tail.next = null;
                }
            }

            _cache.Add (key, item);
        }

        item.val = value;
        moveToHead (item);

        if (head == null) {
            head = item;
        }

        if (tail == null) {
            tail = head;
        }
    }

    private void moveToHead (Node item) {
        if (item == null || item == head) {
            return;
        }

        var next = item.next;
        var prev = item.prev;

        if (next != null) {
            next.prev = item.prev;
        }

        if (prev != null) {
            prev.next = item.next;
        }

        item.prev = null;
        item.next = head;

        if (head != null) {
            head.prev = item;
            head = item;
        }

        if (tail == item) {
            tail = prev;
        }
    }
}