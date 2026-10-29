// Url:https://leetcode.com/problems/clone-graph

/*
133. Clone Graph
Medium

Given a reference of a node in a connected undirected graph, return a deep copy (clone) of the graph. Each node in the graph contains a val (int) and a list (List[Node]) of its neighbors.


Example:

Input:
{
	"$id": "1",
	"neighbors": [{
		"$id": "2",
		"neighbors": [{
				"$ref": "1"
			},
			{
				"$id": "3",
				"neighbors": [{
					"$ref": "2"
				}, {
					"$id": "4",
					"neighbors": [{
						"$ref": "3"
					}, {
						"$ref": "1"
					}],
					"val": 4
				}],
				"val": 3
			}
		],
		"val": 2
	}, {
		"$ref": "4"
	}],
	"val": 1
}

Explanation:
Node 1's value is 1, and it has two neighbors: Node 2 and 4.
Node 2's value is 2, and it has two neighbors: Node 1 and 3.
Node 3's value is 3, and it has two neighbors: Node 2 and 4.
Node 4's value is 4, and it has two neighbors: Node 1 and 3.

Note:
	The number of nodes will be between 1 and 100.
	The undirected graph is a simple graph, which means no repeated edges and no self-loops in the graph.
	Since the graph is undirected, if node p has node q as neighbor, then node q must have node p as neighbor too.
	You must return the copy of the given node as a reference to the cloned graph.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem133 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (CloneGraph_DFS (null));

            Console.WriteLine (CloneGraph_BFS(null));
        }

        static Dictionary<Node, Node> visited = [];

        public static Node CloneGraph_DFS (Node node) {
            if (node == null) {
                return node;
            }

            if (visited.ContainsKey (node)) {
                return visited[node];
            }

            Node clone = new(node.val, []);
            visited.Add (node, clone);

            foreach (var neighbour in node.neighbors) {
                clone.neighbors.Add (CloneGraph_DFS (neighbour));
            }

            return clone;
        }

        public static Node CloneGraph_BFS (Node node) {
            if (node == null) {
                return node;
            }

            Queue<Node> q = new();
            Dictionary<Node, Node> visited = [];

            q.Enqueue (node);
            visited.Add (node, new Node (node.val, []));

            while (q.Count > 0) {
                Node t = q.Dequeue ();

                foreach (var item in t.neighbors) {
                    if (!visited.ContainsKey (item)) {
                        q.Enqueue (item);
                        visited.Add (item, new Node (item.val, []));
                    }

                    visited[t].neighbors.Add (visited[item]);
                }
            }

            return visited[node];
        }
    }

    public class Node {
        public int val;
        public IList<Node> neighbors;

        public Node () {

        }

        public Node (int _val, IList<Node> _neighbors) {
            val = _val;
            neighbors = _neighbors;
        }
    }
}