/*

*/

using System;

namespace InterviewPreperationGuide.Core.DataStructure.BinarySearchTree {
    public class Node {
        public int? data;
        public Node left;
        public Node right;

        public Node () {
            this.data = null;
            this.left = null;
            this.right = null;
        }

        public Node (int item) {
            this.data = item;
            this.left = null;
            this.right = null;
        }

        public void DisplayNode () {
            Console.WriteLine (data);
        }
    }

    public class BinarySearchTree {
        public Node root;

        public BinarySearchTree () {
            this.root = null;
        }

        public void Insert (int item) {
            Node newNode = new Node (item);

            if (root == null) {
                root = newNode;
            } else {
                Node current = root;
                Node parent;

                while (true) {
                    parent = current;

                    if (item < current.data) {
                        current = current.left;

                        if (current == null) {
                            parent.left = newNode;
                            break;
                        }
                    } else {
                        current = current.right;

                        if (current == null) {
                            parent.right = newNode;
                            break;
                        }
                    }
                }
            }
        }

        public static void InOrderTraversal (Node r) {
            if (r != null) {
                InOrderTraversal(r.left);
                r.DisplayNode ();
                InOrderTraversal(r.right);
            }
        }

        public static void PreOrderTraversal (Node r) {
            if (r != null) {
                r.DisplayNode ();
                PreOrderTraversal(r.left);
                PreOrderTraversal(r.right);
            }
        }

        public static void PostOrderTraversal (Node r) {
            if (r != null) {
                PostOrderTraversal(r.left);
                PostOrderTraversal(r.right);
                r.DisplayNode ();
            }
        }

        public int FindMin () {
            Node current = root;

            while (current.left != null) {
                current = current.left;
            }

            return (int) current.data;
        }

        public int FindMax () {
            Node current = root;

            while (current.right != null) {
                current = current.right;
            }

            return (int) current.data;
        }

        public Node Find (int item) {
            Node current = root;

            while (current.data != item) {
                if (item < current.data) {
                    current = current.left;
                } else {
                    current = current.right;
                }

                if (current == null) {
                    return null;
                }
            }

            return current;
        }

        public static Node GetSuccessor (Node delNode) {
            Node successorParent = delNode;
            Node successor = delNode;
            Node current = delNode.right;

            while (current != null) {
                successorParent = current;
                successor = current;
                current = current.left;
            }

            if (successor != delNode.right) {
                successorParent.left = successor.right;
                successor.right = delNode.right;
            }

            return successor;
        }

        public bool Delete (int item) {
            Node current = root;
            Node parent = root;
            bool isLeftChild = true;

            while (current.data != item) {
                parent = current;

                if (item < current.data) {
                    isLeftChild = true;
                    current = current.left;
                } else {
                    isLeftChild = false;
                    current = current.right;
                }

                if (current == null) {
                    return false;
                }
            }

            if (current.left == null && current.right == null) {
                if (current == root) {
                    root = null;
                } else if (isLeftChild) {
                    parent.left = null;
                } else {
                    parent.right = null;
                }
            } else if (current.right == null) {
                if (current == root) {
                    root = current.left;
                } else if (isLeftChild) {
                    parent.left = current.left;
                } else {
                    parent.right = current.right;
                }
            } else if (current.left == null) {
                if (current == root) {
                    root = current.right;
                } else if (isLeftChild) {
                    parent.left = parent.right;
                } else {
                    parent.right = current.right;
                }
            } else {
                Node successor = GetSuccessor(current);

                if (current == root) {
                    root = successor;
                } else if (isLeftChild) {
                    parent.left = successor;
                } else {
                    parent.right = successor;
                }

                successor.left = current.left;
            }

            return true;
        }
    }
}