using System;

namespace ExpressionTree {
    public class ExpressionTree {
        public ExpressionTree () { }

        public void InOrder (ExpressionTreeNode root) {
            while (root != null) {
                InOrder (root.left);
                Console.WriteLine (root.val);
                InOrder (root.right);
            }
        }

        public void PreOrder (ExpressionTreeNode root) {
            while (root != null) {
                Console.WriteLine (root.val);
                InOrder (root.left);
                InOrder (root.right);
            }
        }

        public void PostOrder (ExpressionTreeNode root) {
            while (root != null) {
                InOrder (root.left);
                InOrder (root.right);
                Console.WriteLine (root.val);
            }
        }
    }
}