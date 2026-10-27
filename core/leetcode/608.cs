// Url:https://leetcode.com/problems/tree-node

/*
608. Tree Node
Medium

Table: Tree

+-------------+------+
| Column Name | Type |
+-------------+------+
| id          | int  |
| p_id        | int  |
+-------------+------+
id is the column with unique values for this table.
Each row of this table contains information about the id of a node and the id of its parent node in a tree.

Each node in the tree can be one of three types:
- "Leaf": if the node is a leaf node.
- "Root": if the node is the root of the tree.
- "Inner": If the node is neither a leaf node nor a root node.

Write a solution to report the type of each node in the tree.

Return the result table in any order.

The result format is in the following example.

Example 1:

Input: 
Tree table:
+----+------+
| id | p_id |
+----+------+
| 1  | null |
| 2  | 1    |
| 3  | 1    |
| 4  | 2    |
| 5  | 2    |
+----+------+
Output: 
+----+-------+
| id | type  |
+----+-------+
| 1  | Root  |
| 2  | Inner |
| 3  | Leaf  |
| 4  | Leaf  |
| 5  | Leaf  |
+----+-------+
Explanation: 
Node 1 is the root node because its parent node is null and it has child nodes 2 and 3.
Node 2 is an inner node because it has parent node 1 and child nodes 4 and 5.
Nodes 3, 4, and 5 are leaf nodes because they have parent nodes and they do not have child nodes.

Example 2:

Input: 
Tree table:
+----+------+
| id | p_id |
+----+------+
| 1  | null |
+----+------+
Output: 
+----+-------+
| id | type  |
+----+-------+
| 1  | Root  |
+----+-------+
Explanation: If there is only one node on the tree, you only need to output its root attributes.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem608 {
    public class TreeNodeRecord {
        public int Id { get; set; }
        public int? PId { get; set; }
    }

    public class TreeNodeTypeResult {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
    }

    public class Solution {
        public void Init() {
            List<TreeNodeRecord> tree = [
                new() { Id = 1, PId = null },
                new() { Id = 2, PId = 1 },
                new() { Id = 3, PId = 1 },
                new() { Id = 4, PId = 2 },
                new() { Id = 5, PId = 2 }
            ];

            var result = ClassifyTreeNodes(tree);
            foreach (var r in result) {
                Console.WriteLine($"{r.Id}: {r.Type}");
            }
        }

        // Time: O(n) where n is the number of nodes
        // Space: O(n) to store parent IDs and results
        public List<TreeNodeTypeResult> ClassifyTreeNodes(List<TreeNodeRecord> nodes) {
            var parentIds = nodes
                .Where(n => n.PId.HasValue)
                .Select(n => n.PId!.Value)
                .ToHashSet();

            return nodes.Select(n => new TreeNodeTypeResult {
                Id = n.Id,
                Type = !n.PId.HasValue
                    ? "Root"
                    : parentIds.Contains(n.Id)
                        ? "Inner"
                        : "Leaf"
            }).ToList();
        }
    }
}