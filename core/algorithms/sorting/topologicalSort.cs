/*

*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.Algorithms.Sorting {
    // Time: O ()
    // Space: O ()
    public class TopologicalSort {
        public static void Init () {
            Graph theGraph = new(6);
            theGraph.AddVertex ("CS1");
            theGraph.AddVertex ("CS2");
            theGraph.AddVertex ("DS");
            theGraph.AddVertex ("OS");
            theGraph.AddVertex ("ALG");
            theGraph.AddVertex ("AL");
            theGraph.AddEdge (0, 1);
            theGraph.AddEdge (1, 2);
            theGraph.AddEdge (1, 5);
            theGraph.AddEdge (2, 3);
            theGraph.AddEdge (2, 4);
            theGraph.TopSort ();
            Console.WriteLine ();
            Console.WriteLine ("Finished.");
        }
    }

    public class Vertex(string item)
    {
        public bool isVisited = false;
        public string data = item;
    }

    public class Graph {
        private int numberOfVertices;
        private Vertex[] vertices;
        private int[, ] adjMatrix;
        private int counter;

        public Graph (int numberOfVertices) {
            this.numberOfVertices = numberOfVertices;
            this.vertices = new Vertex[this.numberOfVertices];
            this.adjMatrix = new int[this.numberOfVertices, this.numberOfVertices];
            this.counter = 0;

            for (int i = 0; i < this.numberOfVertices; i++) {
                for (int j = 0; j < this.numberOfVertices; j++) {
                    this.adjMatrix[i, j] = 0;
                }
            }
        }

        public void AddVertex (string item) {
            if (counter < this.numberOfVertices) {
                vertices[counter] = new Vertex (item);
                counter++;
            }
        }

        public void AddEdge (int start, int end) {
            adjMatrix[start, end] = 1;
            adjMatrix[end, start] = 1;
        }

        public void ShowVertex (int v) {
            Console.WriteLine (vertices[v].data + " ");
        }

        public int NoSuccessors () {
            bool isEdge;

            for (int row = 0; row <= numberOfVertices - 1; row++) {
                isEdge = false;

                for (int col = 0; col <= numberOfVertices - 1; col++) {
                    if (adjMatrix[row, col] > 0) {
                        isEdge = true;
                        break;
                    }
                }

                if (!isEdge) {
                    return row;
                }
            }

            return -1;
        }

        public void DelVertex (int vert) {
            if (vert != numberOfVertices - 1) {
                for (int j = vert; j <= numberOfVertices - 1; j++) {
                    vertices[j] = vertices[j + 1];
                }

                for (int row = vert; row <= numberOfVertices - 1; row++) {
                    MoveRow (row, numberOfVertices);
                }

                for (int col = vert; col <= numberOfVertices - 1; col++) {
                    MoveCol (col, numberOfVertices);
                }
            }
        }

        private void MoveRow (int row, int length) {
            for (int col = 0; col <= length - 1; col++) {
                adjMatrix[row, col] = adjMatrix[row + 1, col];
            }
        }

        private void MoveCol (int col, int length) {
            for (int row = 0; row <= length - 1; row++) {
                adjMatrix[row, col] = adjMatrix[row, col + 1];
            }
        }

        public void TopSort () {
            int origVerts = numberOfVertices;
            Stack gStack = new();

            while (numberOfVertices > 0) {
                int currVertex = NoSuccessors ();

                if (currVertex == -1) {
                    Console.WriteLine ("Error: graph has cycles.");
                    return;
                }

                gStack.Push (vertices[currVertex].data);
                DelVertex (currVertex);
            }

            Console.Write ("Topological sorting order: ");

            while (gStack.Count > 0) {
                Console.Write (gStack.Pop () + " ");
            }
        }
    }
}