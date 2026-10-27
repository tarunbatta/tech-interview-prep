/*

*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.Algorithms.Search.BFS {
    public class BreadthFirstSearch {
        public static void Init () {
            Graph aGraph = new(13);
            aGraph.AddVertex ("A");
            aGraph.AddVertex ("B");
            aGraph.AddVertex ("C");
            aGraph.AddVertex ("D");
            aGraph.AddVertex ("E");
            aGraph.AddVertex ("F");
            aGraph.AddVertex ("G");
            aGraph.AddVertex ("H");
            aGraph.AddVertex ("I");
            aGraph.AddVertex ("J");
            aGraph.AddVertex ("K");
            aGraph.AddVertex ("L");
            aGraph.AddVertex ("M");
            aGraph.AddEdge (0, 1);
            aGraph.AddEdge (1, 2);
            aGraph.AddEdge (2, 3);
            aGraph.AddEdge (0, 4);
            aGraph.AddEdge (4, 5);
            aGraph.AddEdge (5, 6);
            aGraph.AddEdge (0, 7);
            aGraph.AddEdge (7, 8);
            aGraph.AddEdge (8, 9);
            aGraph.AddEdge (0, 10);
            aGraph.AddEdge (10, 11);
            aGraph.AddEdge (11, 12);
            aGraph.BreadthFirstSearch ();
            Console.WriteLine ();
        }
    }

    public class Vertex {
        public bool isVisited;
        public string data;

        public Vertex (string item) {
            this.isVisited = false;
            this.data = item;
        }
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
            Console.Write (vertices[v].data + " ");
        }

        private int GetAdjUnvisitedVertex (int v) {
            for (int j = 0; j <= numberOfVertices - 1; j++) {
                if ((adjMatrix[v, j] == 1) && (vertices[j].isVisited == false)) {
                    return j;
                }
            }

            return -1;
        }

        public void BreadthFirstSearch () {
            Queue q = new();
            vertices[0].isVisited = true;
            ShowVertex (0);
            q.Enqueue (0);

            while (q.Count > 0) {
                int vert1 = (int) q.Dequeue ();
                int vert2 = GetAdjUnvisitedVertex (vert1);

                while (vert2 != -1) {
                    vertices[vert2].isVisited = true;
                    ShowVertex (vert2);
                    q.Enqueue (vert2);
                    vert2 = GetAdjUnvisitedVertex (vert1);
                }
            }

            for (int i = 0; i <= numberOfVertices - 1; i++) {
                vertices[i].isVisited = false;
            }
        }
    }
}