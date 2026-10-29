/*
Build Order: 

You are given a list of projects and a list of dependencies (which is a list of pairs of
projects, where the second project is dependent on the first project). All of a project'sdependencies
must be built before the project is. Find a build order that will allow the projects to be built. If there
is no valid build order, return an error.

EXAMPLE
Input:
projects: a, b, c, d, e, f
dependencies: (a, d), (f, b), (b, d), (f, a), (d, c)
Output: f, e, a, b, d, c
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c4q7 {
    public class Solution {
        public static List<char> FindBuildOrder (char[] projects, (char, char)[] dependencies) {
            Dictionary<char, List<char>> adj = new Dictionary<char, List<char>> ();
            Dictionary<char, int> inDegree = new Dictionary<char, int> ();

            foreach (char p in projects) {
                adj[p] = new List<char> ();
                inDegree[p] = 0;
            }

            foreach (var (parent, child) in dependencies) {
                adj[parent].Add (child);
                inDegree[child]++;
            }

            Queue<char> queue = new Queue<char> ();
            foreach (char p in projects) {
                if (inDegree[p] == 0) {
                    queue.Enqueue (p);
                }
            }

            List<char> order = new List<char> ();
            while (queue.Count > 0) {
                char curr = queue.Dequeue ();
                order.Add (curr);

                foreach (char child in adj[curr]) {
                    inDegree[child]--;
                    if (inDegree[child] == 0) {
                        queue.Enqueue (child);
                    }
                }
            }

            return order.Count == projects.Length ? order : null;
        }

        public static void Init (string[] args = null) {
            char[] projects = { 'a', 'b', 'c', 'd', 'e', 'f' };
            (char, char)[] deps = { ('a', 'd'), ('f', 'b'), ('b', 'd'), ('f', 'a'), ('d', 'c') };

            List<char> order = FindBuildOrder (projects, deps);
            if (order != null) {
                Console.WriteLine ($"Build Order: {string.Join (", ", order)}");
            } else {
                Console.WriteLine ("No valid build order (cycle detected).");
            }
        }
    }
}
