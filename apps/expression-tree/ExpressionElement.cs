namespace ExpressionTree {
    public abstract class ExpressionElement {
        public bool IsLiteral () {
            return this is ExpressionLiteral;
        }

        public bool IsOperator () {
            return this is ExpressionOperator;
        }

        public bool IsValidOperator (char op) {
            if (op == '+' || op == '-' || op == '*' || op == '/') {
                return true;
            }

            return false;
        }
    }
}