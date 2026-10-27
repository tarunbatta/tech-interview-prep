/*

*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.Algorithms.Others.Dijkstra {
    public class ShortestPath {
        public static void Init () {
            Graph theGraph = new(7);
            theGraph.AddVertex ("A");
            theGraph.AddVertex ("B");
            theGraph.AddVertex ("C");
            theGraph.AddVertex ("D");
            theGraph.AddVertex ("E");
            theGraph.AddVertex ("F");
            theGraph.AddVertex ("G");
            theGraph.AddEdge (0, 1, 2);
            theGraph.AddEdge (0, 3, 1);
            theGraph.AddEdge (1, 3, 3);
            theGraph.AddEdge (1, 4, 10);
            theGraph.AddEdge (2, 5, 5);
            theGraph.AddEdge (2, 0, 4);
            theGraph.AddEdge (3, 2, 2);
            theGraph.AddEdge (3, 5, 8);
            theGraph.AddEdge (3, 4, 2);
            theGraph.AddEdge (3, 6, 4);
            theGraph.AddEdge (4, 6, 6);
            theGraph.AddEdge (6, 5, 1);
            Console.WriteLine ();
            Console.WriteLine ("Shortest paths:");
            Console.WriteLine ();
            theGraph.Path ();
            Console.WriteLine ();
        }
    }

    public class DistOriginal(int pv, int d)
    {
        public int distance = d;
        public int parentVert = pv;
    }

    public class Vertex(string item)
    {
        public bool isInTree = false;
        public string data = item;
    }

    public class Graph {
        private int numberOfVertices;
        private Vertex[] vertices;
        private int[, ] adjMatrix;
        private int counter;
        private int infinity = 1000000;
        private int nTree;
        private DistOriginal[] sPath;
        private int currentVert;
        private int startToCurrent;

        public Graph (int numberOfVertices) {
            this.numberOfVertices = numberOfVertices;
            this.vertices = new Vertex[this.numberOfVertices];
            this.adjMatrix = new int[this.numberOfVertices, this.numberOfVertices];
            this.counter = 0;
            nTree = 0;

            for (int i = 0; i < this.numberOfVertices; i++) {
                for (int j = 0; j < this.numberOfVertices; j++) {
                    this.adjMatrix[i, j] = infinity;
                }
            }

            sPath = new DistOriginal[numberOfVertices];
        }

        public void AddVertex (string item) {
            if (counter < this.numberOfVertices) {
                vertices[counter] = new Vertex (item);
                counter++;
            }
        }

        public void AddEdge (int start, int theEnd, int weight) {
            adjMatrix[start, theEnd] = weight;
        }

        public void Path () {
            int startTree = 0;
            vertices[startTree].isInTree = true;
            nTree = 1;

            for (int j = 0; j <= numberOfVertices; j++) {
                int tempDist = adjMatrix[startTree, j];
                sPath[j] = new DistOriginal (startTree, tempDist);
            }

            while (nTree < numberOfVertices) {
                int indexMin = GetMin ();
                int minDist = sPath[indexMin].distance;
                currentVert = indexMin;
                startToCurrent = sPath[indexMin].distance;
                vertices[currentVert].isInTree = true;
                nTree++;

                AdjustShortPath ();
            }

            DisplayPaths ();
            nTree = 0;

            for (int j = 0; j <= numberOfVertices - 1; j++) {
                vertices[j].isInTree = false;
            }
        }

        public int GetMin () {
            int minDist = infinity;
            int indexMin = 0;

            for (int j = 1; j <= numberOfVertices - 1; j++) {
                if (!vertices[j].isInTree && sPath[j].distance < minDist) {
                    minDist = sPath[j].distance;
                    indexMin = j;
                }
            }

            return indexMin;
        }

        public void AdjustShortPath () {
            int column = 1;

            while (column < numberOfVertices) {
                if (vertices[column].isInTree) {
                    column++;
                } else {
                    int currentToFring = adjMatrix[currentVert, column];
                    int startToFringe = startToCurrent + currentToFring;
                    int sPathDist = sPath[column].distance;

                    if (startToFringe < sPathDist) {
                        sPath[column].parentVert = currentVert;
                        sPath[column].distance = startToFringe;
                    }

                    column++;
                }
            }
        }

        public void DisplayPaths () {
            for (int j = 0; j <= numberOfVertices - 1; j++) {
                Console.Write (vertices[j].data + "=");

                if (sPath[j].distance == infinity) {
                    Console.Write ("inf");
                } else {
                    Console.Write (sPath[j].distance);
                }

                string parent = vertices[sPath[j].parentVert].data;
                Console.Write ("(" + parent + ") ");
            }
        }
    }
}