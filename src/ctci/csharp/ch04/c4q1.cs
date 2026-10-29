/*
Route Between Nodes: 

Given a directed graph, design an algorithm to find out whether there is a
route between two nodes.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c4q1 {
    public class Solution {
        public enum State { Unvisited, Visited, Visiting }

        public class Node {
            public string Vertex { get; }
            public List<Node> Adjacent { get; } = new List<Node> ();
            public State NodeState { get; set; } = State.Unvisited;

            public Node (string vertex) {
                Vertex = vertex;
            }

            public void AddAdjacent (Node x) {
                Adjacent.Add (x);
            }
        }

        public class Graph {
            public List<Node> Vertices { get; } = new List<Node> ();

            public void Add (Node x) {
                Vertices.Add (x);
            }
        }

        public static bool Search (Graph g, Node start, Node end) {
            if (start == end) return true;

            Queue<Node> queue = new Queue<Node> ();
            foreach (Node u in g.Vertices) {
                u.NodeState = State.Unvisited;
            }

            start.NodeState = State.Visiting;
            queue.Enqueue (start);

            while (queue.Count > 0) {
                Node r = queue.Dequeue ();
                if (r != null) {
                    foreach (Node v in r.Adjacent) {
                        if (v.NodeState == State.Unvisited) {
                            if (v == end) {
                                return true;
                            } else {
                                v.NodeState = State.Visiting;
                                queue.Enqueue (v);
                            }
                        }
                    }
                    r.NodeState = State.Visited;
                }
            }
            return false;
        }

        public static void Init (string[] args = null) {
            Graph g = new Graph ();
            Node a = new Node ("A");
            Node b = new Node ("B");
            Node c = new Node ("C");
            Node d = new Node ("D");
            Node e = new Node ("E");
            a.AddAdjacent (b);
            b.AddAdjacent (c);
            c.AddAdjacent (d);
            g.Add (a);
            g.Add (b);
            g.Add (c);
            g.Add (d);
            g.Add (e);

            Console.WriteLine ($"Route A->D: {Search (g, a, d)}"); // True
            Console.WriteLine ($"Route A->E: {Search (g, a, e)}"); // False
        }
    }
}
