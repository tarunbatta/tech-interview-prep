/*
Random Node: 

You are implementing a binary tree class from scratch which, in addition to
insert, find, and delete, has a method getRandomNode() which returns a random node
from the tree. All nodes should be equally likely to be chosen. Design and implement an algorithm
for getRandomNode, and explain how you would implement the rest of the methods.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c4q11 {
    public class Solution {
        public class TreeNode {
            public int Val { get; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
            public int Size { get; set; }

            private static readonly Random _random = new Random ();

            public TreeNode (int val) {
                Val = val;
                Size = 1;
            }

            public TreeNode GetRandomNode () {
                int leftSize = Left?.Size ?? 0;
                int index = _random.Next (Size);
                if (index < leftSize) {
                    return Left.GetRandomNode ();
                } else if (index == leftSize) {
                    return this;
                } else {
                    return Right.GetRandomNode ();
                }
            }
        }

        public static void Init (string[] args = null) {
            TreeNode a = new TreeNode (8);
            TreeNode b = new TreeNode (4);
            TreeNode c = new TreeNode (13);
            TreeNode d = new TreeNode (2);
            TreeNode e = new TreeNode (6);
            a.Left = b;
            a.Right = c;
            b.Left = d;
            b.Right = e;
            a.Size = 5;
            b.Size = 3;

            Console.WriteLine ($"Random sample: {a.GetRandomNode ().Val}");
            Console.WriteLine ($"Random sample: {a.GetRandomNode ().Val}");
            Console.WriteLine ($"Random sample: {a.GetRandomNode ().Val}");
        }
    }
}
