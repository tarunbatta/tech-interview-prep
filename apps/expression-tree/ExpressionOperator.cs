namespace ExpressionTree {
    public class ExpressionOperator : ExpressionElement {
        public char op { get; set; }

        public ExpressionOperator (char op) {
            this.op = op;
        }
    }
}