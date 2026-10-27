// Url:https://leetcode.com/problems/copy-list-with-random-pointer

/*
138. Copy List with Random Pointer
Medium

A linked list is given such that each node contains an additional random pointer which could point to any node in the list or null.
Return a deep copy of the list.

Example 1:

Input:
{"$id":"1","next":{"$id":"2","next":null,"random":{"$ref":"2"},"val":2},"random":{"$ref":"2"},"val":1}

Explanation:
Node 1's value is 1, both of its next and random pointer points to Node 2.
Node 2's value is 2, its next pointer points to null and its random pointer points to itself.

Note:
	You must return the copy of the given head as a reference to the cloned list.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem138 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        Dictionary<Node, Node> visited = [];

        public Node CopyRandomList_DFS (Node head) {
            if (head == null) {
                return head;
            }

            if (visited.ContainsKey (head)) {
                return visited[head];
            }

            Node node = new(head.val, null, null);
            visited.Add (head, node);

            node.next = CopyRandomList_DFS (head.next);
            node.random = CopyRandomList_DFS (head.random);

            return node;
        }

        public static Node CopyRandomList_BFS (Node head) {
            if (head == null) {
                return head;
            }

            Queue<Node> q = new();
            Dictionary<Node, Node> visited = [];
            q.Enqueue (head);
            visited.Add (head, new Node (head.val, null, null));

            while (q.Count > 0) {
                Node n = q.Dequeue ();

                if (n.next != null && !visited.ContainsKey (n.next)) {
                    q.Enqueue (n.next);
                    visited.Add (n.next, new Node (n.next.val, null, null));
                }

                if (n.random != null && !visited.ContainsKey (n.random)) {
                    q.Enqueue (n.random);
                    visited.Add (n.random, new Node (n.random.val, null, null));
                }

                visited[n].next = n.next == null ? null : visited[n.next];
                visited[n].random = n.random == null ? null : visited[n.random];
            }

            return visited[head];
        }
    }

    public class Node {
        public int val;
        public Node next;
        public Node random;

        public Node () {

        }

        public Node (int _val, Node _next, Node _random) {
            val = _val;
            next = _next;
            random = _random;
        }
    }
}