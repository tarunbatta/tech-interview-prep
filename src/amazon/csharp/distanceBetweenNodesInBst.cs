/*
Amazon OA: Distance Between Nodes in BST

Given a list of unique integers nums, construct a BST from it (inserting elements one by one in the given order)
and find the distance between two nodes node1 and node2. Distance is the number of edges between two nodes.
If either node does not exist in the BST, return -1.

Example 1:
Input: nums = [2, 1, 3], node1 = 1, node2 = 3
Output: 2
Explanation:
     2
   /   \
  1     3

Example 2:
Input: nums = [5, 6, 3, 1, 2, 4], node1 = 2, node2 = 4
Output: 3
Explanation:
        5
       / \
      3   6
     / \
    1   4
     \
      2
*/

using System;

namespace InterviewPreperationGuide.Core.Amazon {
    public class DistanceBetweenNodesInBst {
        public class Node {
            public int Val;
            public Node Left, Right;
            public Node (int val) { Val = val; }
        }

        public static void Init () {
            Console.WriteLine (Distance ([2, 1, 3], 1, 3)); // 2
            Console.WriteLine (Distance ([5, 6, 3, 1, 2, 4], 2, 4)); // 3
            Console.WriteLine (Distance ([5, 6, 3, 1, 2, 4], 5, 2)); // 3
            Console.WriteLine (Distance ([5, 6, 3, 1, 2, 4], 4, 4)); // 0
            Console.WriteLine (Distance ([5, 6, 3, 1, 2, 4], 2, 99)); // -1
        }

        // Build BST, find LCA using BST ordering, then sum depths from LCA.
        // Time: O (n * h) to build, O (h) to query — O (n^2) worst case for skewed input
        // Space: O (n)
        public static int Distance (int[] nums, int node1, int node2) {
            Node root = null;
            foreach (int n in nums) {
                root = Insert (root, n);
            }

            if (Depth (root, node1) == -1 || Depth (root, node2) == -1) {
                return -1;
            }

            Node lca = root;
            while (true) {
                if (node1 < lca.Val && node2 < lca.Val) {
                    lca = lca.Left;
                } else if (node1 > lca.Val && node2 > lca.Val) {
                    lca = lca.Right;
                } else {
                    break;
                }
            }

            return Depth (lca, node1) + Depth (lca, node2);
        }

        private static Node Insert (Node root, int val) {
            if (root == null) {
                return new Node (val);
            }

            Node cur = root;
            while (true) {
                if (val < cur.Val) {
                    if (cur.Left == null) { cur.Left = new Node (val); break; }
                    cur = cur.Left;
                } else {
                    if (cur.Right == null) { cur.Right = new Node (val); break; }
                    cur = cur.Right;
                }
            }

            return root;
        }

        // Number of edges from 'from' down to 'target', or -1 if not found.
        private static int Depth (Node from, int target) {
            int d = 0;
            while (from != null) {
                if (target == from.Val) {
                    return d;
                }
                from = target < from.Val ? from.Left : from.Right;
                d++;
            }
            return -1;
        }
    }
}
