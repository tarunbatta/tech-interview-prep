// Url: https://leetcode.com/problems/design-hashmap/

/*
706. Design HashMap
Easy

Design a HashMap without using any built-in hash table libraries.

To be specific, your design should include these functions:

put(key, value) : Insert a (key, value) pair into the HashMap. If the value already exists in the HashMap, update the value.
get(key): Returns the value to which the specified key is mapped, or -1 if this map contains no mapping for the key.
remove(key) : Remove the mapping for the value key if this map contains the mapping for the key.

Example:

MyHashMap hashMap = new MyHashMap();
hashMap.put(1, 1);          
hashMap.put(2, 2);         
hashMap.get(1);            // returns 1
hashMap.get(3);            // returns -1 (not found)
hashMap.put(2, 1);          // update the existing value
hashMap.get(2);            // returns 1 
hashMap.remove(2);          // remove the mapping for 2
hashMap.get(2);            // returns -1 (not found) 

Note:

All keys and values will be in the range of [0, 1000000].
The number of operations will be in the range of [1, 10000].
Please do not use the built-in HashMap library.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem706 {
    public class Solution {
        public static void Init () {
            MyHashMap hashMap = new();
            hashMap.Put (1, 1);
            hashMap.Put (2, 2);
            hashMap.Get (1); // returns 1
            hashMap.Get (3); // returns -1 (not found)
            hashMap.Put (2, 1); // update the existing value
            hashMap.Get (2); // returns 1 
            hashMap.Remove (2); // remove the mapping for 2
            hashMap.Get (2); // returns -1 (not found) 
        }
    }

    public class MyHashMap {
        private int capacity = 1000001;
        public ListNode[] nodes;

        /** Initialize your data structure here. */
        public MyHashMap () {
            nodes = new ListNode[capacity];
        }

        /** value will always be non-negative. */
        public void Put (int key, int value) {
            int index = getIndex (key);
            ListNode prev = FindElement (index, key);

            if (prev.next == null) {
                prev.next = new ListNode (key, value);
            } else {
                prev.next.value = value;
            }
        }

        /** Returns the value to which the specified key is mapped, or -1 if this map contains no mapping for the key */
        public int Get (int key) {
            int index = getIndex (key);
            ListNode prev = FindElement (index, key);

            return prev.next == null ? -1 : prev.next.value;
        }

        /** Removes the mapping of the specified value key if this map contains a mapping for the key */
        public void Remove (int key) {
            int index = getIndex (key);
            ListNode prev = FindElement (index, key);

            if (prev.next != null) {
                prev.next = prev.next.next;
            }
        }

        private int getIndex (int key) {
            return key.GetHashCode () % nodes.Length;
        }

        private ListNode FindElement (int index, int key) {
            if (nodes[index] == null) {
                return null;
            }

            ListNode prev = nodes[index];

            while (prev.next != null && prev.next.key != key) {
                prev = prev.next;
            }

            return prev;
        }
    }

    public class ListNode {
        public int key;
        public int value;
        public ListNode next;

        public ListNode (int key, int value) {
            this.key = key;
            this.value = value;
            this.next = null;
        }
    }
}