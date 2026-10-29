/*
Check Subtree: 

Tl and T2 are two very large binary trees, with Tl much bigger than T2. Create an
algorithm to determine if T2 is a subtree of Tl.
A tree T2 is a subtree of Tl if there exists a node n in Tl such that the subtree of n is identical to T2.
That is, if you cut off the tree at node n, the two trees would be identical.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c4q10 {
    public class Solution {
        public class Node {
            public int Val { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node (int val) {
                Val = val;
            }
        }

        public static bool ContainsTree (Node t1, Node t2) {
            if (t2 == null) return true;
            return SubTree (t1, t2);
        }

        private static bool SubTree (Node r1, Node r2) {
            if (r1 == null) return false;
            if (r1.Val == r2.Val && MatchTree (r1, r2)) return true;
            return SubTree (r1.Left, r2) || SubTree (r1.Right, r2);
        }

        private static bool MatchTree (Node r1, Node r2) {
            if (r1 == null && r2 == null) return true;
            if (r1 == null || r2 == null) return false;
            if (r1.Val != r2.Val) return false;
            return MatchTree (r1.Left, r2.Left) && MatchTree (r1.Right, r2.Right);
        }

        public static void Init (string[] args = null) {
            Node t1 = new Node (1) {
                Left = new Node (2) {
                    Left = new Node (4),
                    Right = new Node (5)
                },
                Right = new Node (3)
            };

            Node t2 = new Node (2) {
                Left = new Node (4),
                Right = new Node (5)
            };

            Console.WriteLine ($"T2 is subtree of T1: {ContainsTree (t1, t2)}"); // True
        }
    }
}
