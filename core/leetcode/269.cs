// Url:https://leetcode.com/problems/alien-dictionary

/*
269. Alien Dictionary
Hard

There is a new alien language which uses the latin alphabet. However, the order among letters are unknown to you. You receive a list of non-empty words from the dictionary, where words are sorted lexicographically by the rules of this new language. Derive the order of letters in this language.

Example 1:

Input:
[
  "wrt",
  "wrf",
  "er",
  "ett",
  "rftt"
]

Output: "wertf"
Example 2:

Input:
[
  "z",
  "x"
]

Output: "zx"
Example 3:

Input:
[
  "z",
  "x",
  "z"
] 

Output: "" 

Explanation: The order is invalid, so return "".


Note:
You may assume all letters are in lowercase.
You may assume that if a is a prefix of b, then a must appear before b in the given dictionary.
If the order is invalid, return an empty string.
There may be multiple valid order of letters, return any one of them is fine.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem269 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (AlienOrder(["wrt", "wrf", "er", "ett", "rftt"]));
            Console.WriteLine (AlienOrder(["z", "x"]));
            Console.WriteLine (AlienOrder(["z", "x", "z"]));
            Console.WriteLine (AlienOrder(["a", "b", "ca", "cc"]));
        }

        // Time: O (n + m), where n represents all vertices, m represents all edges
        // Space: O (n + m)
        public static string AlienOrder (string[] words) {
            string result = "";

            if (words == null || words.Length == 0) {
                return result;
            }

            Dictionary<char, HashSet<char>> graph = [];
            Dictionary<char, int> inDegreeMap = [];

            // MUST initialize the map, to avoid null exception for those character that will have zero inDegrees 
            // (i.e. starting characters)
            foreach (String s in words) {
                foreach (char c in s.ToCharArray ()) {
                    if (!inDegreeMap.ContainsKey (c)) {
                        inDegreeMap.Add (c, 0);
                    }
                }
            }

            // build the graph by comparing the adjacent words, the first character that is different 
            // between two adjacent words reflect the lexicographical order
            // as well as fill out inDegree map for every character
            for (int i = 0; i < words.Length - 1; i++) {
                String cur = words[i];
                String next = words[i + 1];
                int length = Math.Min (cur.Length, next.Length);

                /* according to given dictionary with specified order, traverse every pair of words,
                then put each pair into graph map to build the graph, and then update inDegree map
                for every "nextChar" (increase their inDegree by 1 every time) */
                for (int j = 0; j < length; j++) {
                    char c1 = cur[j];
                    char c2 = next[j];

                    if (c1 != c2) {
                        HashSet<char> set = [];

                        if (graph.ContainsKey (c1)) {
                            set = graph[c1];
                        }

                        if (!set.Contains (c2)) {
                            set.Add (c2);
                            graph[c1] = set;

                            // update inDegree map
                            inDegreeMap[c2] = inDegreeMap[c2] + 1;
                        }

                        // we can determine the order of characters ony by first different pair of characters 
                        // so we cannot add relationship by the rest of characters
                        break;
                    }
                }
            }

            Queue<char> q = new Queue<char> ();

            // put all starting node into queue, which means put all nodes that have inDegree = 0
            foreach (char c in inDegreeMap.Keys) {
                if (inDegreeMap[c] == 0) {
                    q.Enqueue (c);
                }
            }

            StringBuilder sb = new StringBuilder ();

            // BFS traversal to build result string
            while (q.Count != 0) {
                char c = q.Dequeue ();
                sb.Append (c);

                // traverse all next node of current node in graph, 
                // update inDegree value then put all nodes with zero inDegree into queue 
                if (graph.ContainsKey (c)) {
                    foreach (char c2 in graph[c]) {
                        inDegreeMap[c2] = inDegreeMap[c2] - 1;

                        if (inDegreeMap[c2] == 0) {
                            q.Enqueue (c2);
                        }
                    }
                }
            }

            // check if input order is valid
            if (sb.ToString ().Length != inDegreeMap.Count) {
                return "";
            } else {
                result = sb.ToString ();
            }

            return result;
        }
    }
}