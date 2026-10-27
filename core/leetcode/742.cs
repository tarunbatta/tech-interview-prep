// Url:https://leetcode.com/problems/closest-leaf-in-a-binary-tree

/*
742. Closest Leaf in a Binary Tree
Medium

Given the root of a binary tree where every node has a unique value and a target integer k, return the value of the nearest leaf node to the target k in the tree.

Nearest to a leaf means the least number of edges traveled on the binary tree to reach any leaf of the tree. Also, a leaf is a node with no children.

Example 1:
Input: root = [1,3,2], k = 1
Output: 2 (or 3)
Explanation: Either 2 or 3 is the nearest leaf node to 1.

Example 2:
Input: root = [1], k = 1
Output: 1
Explanation: The nearest leaf node is the root itself.

Example 3:
Input: root = [1,2,3,4,null,null,null,5,null,6], k = 2
Output: 3
Explanation: The leaf node with value 3 (and not 4) is nearest to node 2.

Constraints:
- The number of nodes in the tree is in the range [1, 1000].
- 1 <= Node.val <= 1000
- All the values of the tree are unique.
- There will exist a node in the tree where Node.val == k.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem742 {
    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null) {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class Solution {
        public static void Init() {
            TreeNode root1 = new(1, new TreeNode(3), new TreeNode(2));
            Console.WriteLine(FindClosestLeaf(root1, 1)); // 2 or 3

            TreeNode root2 = new(1);
            Console.WriteLine(FindClosestLeaf(root2, 1)); // 1
        }

        // Time: O(n) where n is the number of nodes
        // Space: O(n) to store the undirected graph and visited set
        public static int FindClosestLeaf(TreeNode root, int k) {
            Dictionary<int, List<int>> graph = [];
            HashSet<int> leaves = [];

            BuildGraph(root, null, graph, leaves);

            Queue<int> queue = new();
            HashSet<int> visited = [];

            queue.Enqueue(k);
            visited.Add(k);

            while (queue.Count > 0) {
                int curr = queue.Dequeue();

                if (leaves.Contains(curr)) {
                    return curr;
                }

                if (graph.TryGetValue(curr, out var neighbors)) {
                    foreach (int neighbor in neighbors) {
                        if (visited.Add(neighbor)) {
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }

            return -1;
        }

        private static void BuildGraph(TreeNode node, TreeNode parent, Dictionary<int, List<int>> graph, HashSet<int> leaves) {
            if (node == null) {
                return;
            }

            if (!graph.ContainsKey(node.val)) {
                graph[node.val] = [];
            }

            if (parent != null) {
                graph[node.val].Add(parent.val);
                if (!graph.ContainsKey(parent.val)) {
                    graph[parent.val] = [];
                }
                graph[parent.val].Add(node.val);
            }

            if (node.left == null && node.right == null) {
                leaves.Add(node.val);
            }

            BuildGraph(node.left, node, graph, leaves);
            BuildGraph(node.right, node, graph, leaves);
        }
    }
}