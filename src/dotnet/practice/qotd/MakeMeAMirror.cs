using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.App.Qotd {
    /// <summary>
    /// Make Me A Mirror
    /// Given a fully formed (perfect) binary tree, you may exchange the values of any two nodes on the same level.
    /// Find the minimum number of swaps needed to make the tree a mirror of itself (symmetric).
    /// If no number of swaps can do it, return -1.
    ///
    /// The tree is given in level order: level k occupies indices [2^k - 1, 2^(k+1) - 2].
    /// </summary>
    public class MakeMeAMirror {
        public static void Init () {
            Console.WriteLine (MinSwaps ([1, 2, 2, 3, 4, 4, 3])); // 0 (already symmetric)
            Console.WriteLine (MinSwaps ([1, 2, 2, 3, 3, 4, 4])); // 1 (swap positions 4 and 6)
            Console.WriteLine (MinSwaps ([1, 2, 3, 4, 4, 4, 4])); // -1 (level 1 can't be a palindrome)
            // Level 2 [1,2,1,2] is a 2-cycle (1 swap); level 3 [1,2,3,4,4,1,3,2] is a 3-cycle (2 swaps).
            Console.WriteLine (MinSwaps ([1, 5, 5, 1, 2, 1, 2, 1, 2, 3, 4, 4, 1, 3, 2])); // 3
        }

        /// A level is symmetric iff it reads the same forwards and backwards, so each level is solved
        /// independently as "min arbitrary swaps to make an array a palindrome".
        ///
        /// Model: every mirrored position pair (i, n-1-i) holding different values (a, b) is an edge a–b in a
        /// multigraph over values. Every vertex then has even degree, so the edges split into cycles.
        /// Fixing a cycle of k edges takes k - 1 swaps, so swaps = edges - cycles, and we want as many cycles
        /// as possible.
        ///
        /// 2-cycles (two pairs (a,b) and (b,a)) are always worth taking first. After that, greedy cycle peeling
        /// is used. It is optimal for typical inputs, but maximizing cycle count in general multigraphs is NP-hard,
        /// so for adversarial levels it is a heuristic upper bound.
        ///
        /// Time: O (n) per level for counting plus O (E^2) worst case for cycle peeling
        /// Space: O (n)
        public static int MinSwaps (int[] levelOrder) {
            int total = 0;

            for (int start = 0, size = 1; start < levelOrder.Length; start += size, size *= 2) {
                int len = Math.Min (size, levelOrder.Length - start);
                int swaps = MinSwapsToPalindrome (levelOrder.Skip (start).Take (len).ToArray ());

                if (swaps == -1) {
                    return -1;
                }

                total += swaps;
            }

            return total;
        }

        private static int MinSwapsToPalindrome (int[] level) {
            int n = level.Length;

            // Feasibility: at most one value may have odd count (only possible when n is odd).
            int oddCounts = level.GroupBy (v => v).Count (g => g.Count () % 2 == 1);
            if (oddCounts > n % 2) {
                return -1;
            }

            // Build edges from mismatched mirror pairs.
            Dictionary<(int, int), int> edgeCounts = new Dictionary<(int, int), int> ();
            int edges = 0;

            for (int i = 0; i < n / 2; i++) {
                int a = level[i], b = level[n - 1 - i];
                if (a != b) {
                    (int, int) key = a < b ? (a, b) : (b, a);
                    edgeCounts[key] = edgeCounts.GetValueOrDefault (key) + 1;
                    edges++;
                }
            }

            int cycles = 0;

            // Take all 2-cycles (parallel edges).
            Dictionary<int, List<int>> adj = new Dictionary<int, List<int>> ();
            foreach (var kv in edgeCounts) {
                cycles += kv.Value / 2;

                if (kv.Value % 2 == 1) {
                    (int u, int v) = kv.Key;
                    if (!adj.ContainsKey (u)) adj[u] = new List<int> ();
                    if (!adj.ContainsKey (v)) adj[v] = new List<int> ();
                    adj[u].Add (v);
                    adj[v].Add (u);
                }
            }

            // Greedily peel cycles from the remaining simple graph (every vertex has even degree).
            // Walk from a vertex until a vertex repeats, then remove that cycle.
            while (true) {
                int startVertex = adj.FirstOrDefault (kv => kv.Value.Count > 0).Key;
                if (!adj.TryGetValue (startVertex, out List<int> list) || list.Count == 0) {
                    break;
                }

                List<int> path = new List<int> { startVertex };
                Dictionary<int, int> positionInPath = new Dictionary<int, int> { [startVertex] = 0 };

                while (true) {
                    int u = path[^1];
                    int v = adj[u][0];
                    adj[u].Remove (v);
                    adj[v].Remove (u);

                    if (positionInPath.TryGetValue (v, out int pos)) {
                        // Found a cycle path[pos..] -> v. Remove its vertices from the path, keep walking from v.
                        cycles++;
                        for (int k = path.Count - 1; k > pos; k--) {
                            positionInPath.Remove (path[k]);
                            path.RemoveAt (k);
                        }

                        if (adj[v].Count == 0) {
                            break;
                        }
                    } else {
                        positionInPath[v] = path.Count;
                        path.Add (v);
                    }
                }
            }

            return edges - cycles;
        }
    }
}
