using System;
using System.Collections;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Find the number of islands
    /// Given a boolean 2D matrix, find the number of islands.
    ///
    /// This is an variation of the standard problem: “Counting number of connected components in a undirected graph”.
    /// Before we go to the problem, let us understand what is a connected component.
    /// A connected component of an undirected graph is a subgraph in which every two vertices are connected to each other by a path(s),
    /// and which is connected to no other vertices outside the subgraph.
    ///
    /// For example, the graph shown below has three connected components.
    ///
    /// A graph where all vertices are connected with each other, has exactly one connected component, consisting of the whole graph.
    /// Such graph with only one connected component is called as Strongly Connected Graph.
    ///
    /// The problem can be easily solved by applying DFS() on each component.In each DFS() call, a component or a sub-graph is visited.
    /// We will call DFS on the next un-visited component.The number of calls to DFS() gives the number of connected components.BFS can also be used.
    ///
    /// What is an island?
    /// A group of connected 1s forms an island. For example, the below matrix contains 5 islands
    /// {1, 1, 0, 0, 0},
    /// {0, 1, 0, 0, 1},
    /// {1, 0, 0, 1, 1},
    /// {0, 0, 0, 0, 0},
    /// {1, 0, 1, 0, 1}
    /// </summary>
    public class Question40 {
        public static void Init (string[] args) {
            Console.WriteLine (GetIslandCount (new int[, ] { { 1, 0, 1 }, { 0, 1, 0 }, { 1, 0, 1 }
            }));
        }

        public static int GetIslandCount (int[, ] matrix) {
            int result = 0;

            if (matrix != null) {
                int rows = matrix.GetLength (0);
                int cols = matrix.GetLength (1);

                bool[, ] isVisited = new bool[rows, cols];

                if (rows > 0 && cols > 0) {
                    for (int i = 0; i < rows; i++) {
                        for (int j = 0; j < cols; j++) {
                            if (matrix[i, j] == 1 && !isVisited[i, j]) {
                                result++;
                                BFS (matrix, isVisited, i, j);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static void BFS (int[, ] matrix, bool[, ] isVisited, int i, int j) {
            int rows = matrix.GetLength (0);
            int cols = matrix.GetLength (1);

            Queue q = new Queue ();
            q.Enqueue (new IslandPoint (i, j));

            while (q.Count > 0) {
                IslandPoint d = (IslandPoint) q.Dequeue ();
                isVisited[i, j] = true;

                IslandPoint right = null;
                IslandPoint down = null;

                if (d.y < cols - 1) {
                    right = new IslandPoint (d.x, d.y + 1);

                    if (matrix[right.x, right.y] == 1) {
                        q.Enqueue (right);
                    }
                }

                if (d.x < rows - 1) {
                    down = new IslandPoint (d.x + 1, d.y);

                    if (matrix[down.x, down.y] == 1) {
                        q.Enqueue (down);
                    }
                }
            }
        }
    }

    public class IslandPoint {
        public int x { get; set; }
        public int y { get; set; }

        public IslandPoint (int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
}