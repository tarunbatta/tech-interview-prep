/*
Minimal Tree: 

Given a sorted (increasing order) array with unique integer elements, write an
algorithm to create a binary search tree with minimal height.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c4q2 {
    public class Solution {
        public class Node {
            public int Key { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node (int key) {
                Key = key;
            }
        }

        public static Node CreateBst (int[] array) {
            if (array == null || array.Length == 0) return null;
            return CreateBst (array, 0, array.Length - 1);
        }

        public static Node CreateBst (int[] array, int start, int end) {
            if (end < start) return null;
            int mid = (start + end) / 2;
            Node n = new Node (array[mid]);
            n.Left = CreateBst (array, start, mid - 1);
            n.Right = CreateBst (array, mid + 1, end);
            return n;
        }

        public static void Init (string[] args = null) {
            int[] arr = { 1, 2, 3, 4, 5, 6, 7 };
            Node root = CreateBst (arr);
            Console.WriteLine ($"Root: {root.Key}, Left: {root.Left.Key}, Right: {root.Right.Key}");
        }
    }
}
