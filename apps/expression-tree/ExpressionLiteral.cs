namespace ExpressionTree {
    public class ExpressionLiteral : ExpressionElement {
        public int data { get; set; }

        public ExpressionLiteral (int data) {
            this.data = data;
        }
    }
}