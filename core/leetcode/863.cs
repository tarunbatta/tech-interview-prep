// Url:https://leetcode.com/problems/all-nodes-distance-k-in-binary-tree

/*
863. All Nodes Distance K in Binary Tree
Medium

We are given a binary tree (with root node root), a target node, and an integer value K.

Return a list of the values of all nodes that have a distance K from the target node.  The answer can be returned in any order.

 
Example 1:

Input: root = [3,5,1,6,2,0,8,null,null,7,4], target = 5, K = 2

Output: [7,4,1]

Explanation: 
The nodes that are a distance 2 from the target node (with value 5)
have values 7, 4, and 1.


Note that the inputs "root" and "target" are actually TreeNodes.
The descriptions of the inputs above are just serializations of these objects.

Note:
	The given tree is non-empty.
	Each node in the tree has unique values 0 <= node.val <= 500.
	The target node is a node in the tree.
	0 <= K <= 1000.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem863 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (n)
        // Space: O (n)
        public static IList<int> DistanceK (TreeNode root, TreeNode target, int K) {
            var stack = new Stack<TreeNode> ();
            var graph = new Dictionary<int, IList<int>> ();

            stack.Push (root);

            while (stack.Count > 0) {
                var node = stack.Pop ();

                if (!graph.ContainsKey (node.val)) { graph[node.val] = []; }

                if (node.left != null) {
                    if (!graph.ContainsKey (node.left.val)) { graph[node.left.val] = []; }

                    graph[node.left.val].Add (node.val);
                    graph[node.val].Add (node.left.val);

                    stack.Push (node.left);
                }

                if (node.right != null) {
                    if (!graph.ContainsKey (node.right.val)) { graph[node.right.val] = []; }

                    graph[node.right.val].Add (node.val);
                    graph[node.val].Add (node.right.val);

                    stack.Push (node.right);
                }
            }

            var queue = new Queue<int> ();
            var visited = new HashSet<int> ();
            queue.Enqueue (target.val);

            while (K-- > 0) {
                int size = queue.Count;

                for (int i = 0; i < size; i++) {
                    int node = queue.Dequeue ();

                    visited.Add (node);
                    var neighbors = graph[node];

                    foreach (var neighbor in neighbors) {
                        if (visited.Contains (neighbor)) { continue; }
                        queue.Enqueue (neighbor);
                    }
                }
            }

            return [.. queue];
        }
    }

    public class TreeNode(int x)
    {
        public int val = x;
        public TreeNode left;
        public TreeNode right;
    }
}