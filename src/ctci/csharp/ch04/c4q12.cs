/*
Paths with Sum: 

You are given a binary tree in which each node contains an integer value (which
might be positive or negative). Design an algorithm to count the number of paths that sum to a
given value. The path does not need to start or end at the root or a leaf, but it must go downwards
(traveling only from parent nodes to child nodes).
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c4q12 {
    public class Solution {
        public class Node {
            public int Val { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node (int val) {
                Val = val;
            }
        }

        public static int CountPathsWithSum (Node root, int sum) {
            if (root == null) return 0;
            int pathsFromRoot = CountPathsWithSumFromNode (root, sum, 0);
            int pathsOnLeft = CountPathsWithSum (root.Left, sum);
            int pathsOnRight = CountPathsWithSum (root.Right, sum);
            return pathsFromRoot + pathsOnLeft + pathsOnRight;
        }

        private static int CountPathsWithSumFromNode (Node root, int target, int current) {
            if (root == null) return 0;
            current += root.Val;
            int total = 0;
            if (current == target) total++;
            total += CountPathsWithSumFromNode (root.Left, target, current);
            total += CountPathsWithSumFromNode (root.Right, target, current);
            return total;
        }

        public static void Init (string[] args = null) {
            Node a = new Node (10);
            Node b = new Node (5);
            Node c = new Node (-3);
            Node d = new Node (3);
            Node e = new Node (2);
            Node f = new Node (11);
            Node g = new Node (3);
            Node h = new Node (-2);
            Node i = new Node (1);

            a.Left = b; a.Right = c;
            b.Left = d; b.Right = e;
            c.Right = f;
            d.Left = g; d.Right = h;
            e.Right = i;

            Console.WriteLine ($"Paths with sum 8: {CountPathsWithSum (a, 8)}"); // 3
        }
    }
}
