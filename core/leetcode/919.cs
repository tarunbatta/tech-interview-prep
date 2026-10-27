// Url:https://leetcode.com/problems/complete-binary-tree-inserter

/*
919. Complete Binary Tree Inserter
Medium

A complete binary tree is a binary tree in which every level, except possibly the last, is completely filled, and all nodes are as far left as possible.

Design an algorithm to insert a new node to a complete binary tree keeping it complete after the insertion.

Implement the CBTInserter class:
- CBTInserter(TreeNode root) Initializes the data structure with the root of the complete binary tree.
- int Insert(int val) Inserts a TreeNode into the tree with value Node.val == val so that the tree remains complete, and returns the value of the parent of the inserted TreeNode.
- TreeNode Get_root() Returns the head node of the tree.

Example 1:
Input
["CBTInserter", "insert", "insert", "get_root"]
[[[1, 2]], [3], [4], []]
Output
[null, 1, 2, [1, 2, 3, 4]]

Explanation
CBTInserter cBTInserter = new CBTInserter([1, 2]);
cBTInserter.insert(3);  // return 1
cBTInserter.insert(4);  // return 2
cBTInserter.get_root(); // return [1, 2, 3, 4]

Constraints:
- The number of nodes in the tree will be in the range [1, 1000].
- 0 <= Node.val <= 5000
- root is a complete binary tree.
- 0 <= val <= 5000
- At most 10^4 calls will be made to insert and get_root.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem919 {
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

    public class CBTInserter {
        private readonly List<TreeNode> _tree = [];

        // Time: O(n) where n is the number of nodes in tree
        // Space: O(n) to store tree nodes in array list
        public CBTInserter(TreeNode root) {
            if (root == null) {
                return;
            }

            Queue<TreeNode> queue = new();
            queue.Enqueue(root);

            while (queue.Count > 0) {
                TreeNode curr = queue.Dequeue();
                _tree.Add(curr);

                if (curr.left != null) {
                    queue.Enqueue(curr.left);
                }
                if (curr.right != null) {
                    queue.Enqueue(curr.right);
                }
            }
        }

        // Time: O(1)
        // Space: O(1)
        public int Insert(int val) {
            TreeNode newNode = new(val);
            int parentIdx = (_tree.Count - 1) / 2;
            TreeNode parent = _tree[parentIdx];

            if (_tree.Count % 2 == 1) {
                parent.left = newNode;
            } else {
                parent.right = newNode;
            }

            _tree.Add(newNode);
            return parent.val;
        }

        // Time: O(1)
        // Space: O(1)
        public TreeNode Get_root() {
            return _tree.Count > 0 ? _tree[0] : null;
        }
    }

    public class Solution {
        public static void Init() {
            TreeNode root = new(1, new TreeNode(2));
            CBTInserter inserter = new(root);
            Console.WriteLine(inserter.Insert(3)); // 1
            Console.WriteLine(inserter.Insert(4)); // 2
            Console.WriteLine(inserter.Get_root().val); // 1
        }
    }
}