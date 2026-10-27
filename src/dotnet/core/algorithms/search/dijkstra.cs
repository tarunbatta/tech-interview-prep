/*

*/

using System;

namespace InterviewPreperationGuide.Core.Algorithms.Search {
    public class Dijkstra {
        public static void Init () {
            int[, ] graph = { { 0, 6, 0, 0, 0, 0, 0, 9, 0 },
                { 6, 0, 9, 0, 0, 0, 0, 11, 0 },
                { 0, 9, 0, 5, 0, 6, 0, 0, 2 },
                { 0, 0, 5, 0, 9, 16, 0, 0, 0 },
                { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
                { 0, 0, 6, 0, 10, 0, 2, 0, 0 },
                { 0, 0, 0, 16, 0, 2, 0, 1, 6 },
                { 9, 11, 0, 0, 0, 0, 1, 0, 5 },
                { 0, 0, 2, 0, 0, 0, 6, 5, 0 }
            };

            DijkstraAlgo (graph, 0, 9);
        }

        public static void DijkstraAlgo (int[, ] graph, int source, int verticesCount) {
            int[] distance = new int[verticesCount];
            bool[] shortestPathTreeSet = new bool[verticesCount];

            for (int i = 0; i < verticesCount; ++i) {
                distance[i] = Int32.MaxValue;
                shortestPathTreeSet[i] = false;
            }

            distance[source] = 0;

            for (int i = 0; i < verticesCount - 1; i++) {
                // Pick the minimum distance vertex from the set of vertices not yet processed. u is always equal to source in first iteration
                int u = MinimumDistance (distance, shortestPathTreeSet, verticesCount);
                shortestPathTreeSet[u] = true;

                for (int v = 0; v < verticesCount; v++) {
                    // Update distance[v] only if is not in shortestPathTreeSet, there is an edge from u to v, 
                    // and total weight of path from source to v through u is smaller than current value of distance[v]
                    if (!shortestPathTreeSet[v] &&
                        Convert.ToBoolean (graph[u, v]) &&
                        distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v]) {
                        distance[v] = distance[u] + graph[u, v];
                    }
                }
            }

            Print (distance, verticesCount);
        }

        // A utility function to find the vertex with minimum distance value, from the set of vertices not yet included in shortest path tree
        private static int MinimumDistance (int[] distance, bool[] shortestPathTreeSet, int verticesCount) {
            int min = Int32.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < verticesCount; v++) {
                if (shortestPathTreeSet[v] == false && distance[v] <= min) {
                    min = distance[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        private static void Print (int[] distance, int verticesCount) {
            Console.WriteLine ("Vertex Distance from source");

            for (int i = 0; i < verticesCount; i++) {
                Console.WriteLine ("{0}\t  {1}", i, distance[i]);
            }
        }
    }
}