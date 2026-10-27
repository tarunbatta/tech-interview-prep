/*

*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.Algorithms.Others.MinimumSpanningTree {
    public class MinimumSpanningTree {
        public static void Init () {
            Graph aGraph = new(7);
            aGraph.AddVertex ("A");
            aGraph.AddVertex ("B");
            aGraph.AddVertex ("C");
            aGraph.AddVertex ("D");
            aGraph.AddVertex ("E");
            aGraph.AddVertex ("F");
            aGraph.AddVertex ("G");
            aGraph.AddEdge (0, 1);
            aGraph.AddEdge (0, 2);
            aGraph.AddEdge (0, 3);
            aGraph.AddEdge (1, 2);
            aGraph.AddEdge (1, 3);
            aGraph.AddEdge (1, 4);
            aGraph.AddEdge (2, 3);
            aGraph.AddEdge (2, 5);
            aGraph.AddEdge (3, 5);
            aGraph.AddEdge (3, 4);
            aGraph.AddEdge (3, 6);
            aGraph.AddEdge (4, 5);
            aGraph.AddEdge (4, 6);
            aGraph.AddEdge (5, 6);
            Console.WriteLine ();
            aGraph.MinimumSpanningTree ();
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

        public void MinimumSpanningTree () {
            Stack gStack = new();

            vertices[0].isVisited = true;
            gStack.Push (0);

            while (gStack.Count > 0) {
                int currentVer = (int) gStack.Peek ();
                int v = GetAdjUnvisitedVertex (currentVer);

                if (v == -1)
                    gStack.Pop ();
                else {
                    vertices[v].isVisited = true;
                    gStack.Push (v);
                    ShowVertex (currentVer);
                    ShowVertex (v);
                    Console.Write (" ");
                }
            }

            for (int j = 0; j <= numberOfVertices - 1; j++) {
                vertices[j].isVisited = false;
            }
        }
    }
}