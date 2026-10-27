namespace ExpressionTree {
    public class ExpressionTreeNode {
        public ExpressionElement val { get; set; }
        public ExpressionTreeNode left { get; set; }
        public ExpressionTreeNode right { get; set; }

        public ExpressionTreeNode (int data) {
            this.val = new ExpressionLiteral (data);
            this.left = null;
            this.right = null;
        }

        public ExpressionTreeNode (char op) {
            this.val = new ExpressionOperator (op);
            this.left = null;
            this.right = null;
        }
    }
}