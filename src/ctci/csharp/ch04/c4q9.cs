/*
BST Sequences: 

A binary search tree was created by traversing through an array from left to right
and inserting each element. Given a binary search tree with distinct elements, print all possible
arrays that could have led to this tree.

EXAMPLE
Input:
    2
   / \
  1   3
Output: {2, 1, 3}, {2, 3, 1}
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c4q9 {
    public class Solution {
        public class Node {
            public int Val { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node (int val) {
                Val = val;
            }
        }

        public static List<List<int>> AllSequences (Node node) {
            List<List<int>> result = new List<List<int>> ();
            if (node == null) {
                result.Add (new List<int> ());
                return result;
            }

            List<int> prefix = new List<int> { node.Val };
            List<List<int>> leftSeq = AllSequences (node.Left);
            List<List<int>> rightSeq = AllSequences (node.Right);

            foreach (var left in leftSeq) {
                foreach (var right in rightSeq) {
                    List<List<int>> weaved = new List<List<int>> ();
                    WeaveLists (new LinkedList<int> (left), new LinkedList<int> (right), weaved, new LinkedList<int> (prefix));
                    result.AddRange (weaved);
                }
            }

            return result;
        }

        private static void WeaveLists (LinkedList<int> first, LinkedList<int> second, List<List<int>> results, LinkedList<int> prefix) {
            if (first.Count == 0 || second.Count == 0) {
                List<int> result = new List<int> (prefix);
                result.AddRange (first);
                result.AddRange (second);
                results.Add (result);
                return;
            }

            int headFirst = first.First.Value;
            first.RemoveFirst ();
            prefix.AddLast (headFirst);
            WeaveLists (first, second, results, prefix);
            prefix.RemoveLast ();
            first.AddFirst (headFirst);

            int headSecond = second.First.Value;
            second.RemoveFirst ();
            prefix.AddLast (headSecond);
            WeaveLists (first, second, results, prefix);
            prefix.RemoveLast ();
            second.AddFirst (headSecond);
        }

        public static void Init (string[] args = null) {
            Node root = new Node (2) {
                Left = new Node (1),
                Right = new Node (3)
            };

            List<List<int>> seqs = AllSequences (root);
            foreach (var seq in seqs) {
                Console.WriteLine ($"{{{string.Join (", ", seq)}}}");
            }
        }
    }
}
