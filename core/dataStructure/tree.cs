/*

*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.DataStructure.Tree {
    public class Node {
        public object data;
        public List<Node> childrens;

        public Node () {
            this.data = null;
            this.childrens = null;
        }

        public Node (object item) {
            this.data = item;
            this.childrens = [];
        }

        public void DisplayNode () {
            Console.WriteLine (data);
        }
    }

    public class Tree {
        public Node root;

        public Tree () {
            this.root = null;
        }
    }
}