using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Given a Postfix expression, convert it to a prefix expression.
    /// Assume you are given the input as a List<PFElement>.
    /// Where PFElement is the abstract parent class with two implementations PFLiteral and PFOperator.
    ///
    /// input: 5 4 +
    /// output: + 5 4
    ///
    /// input: 7 6 + 5 -
    /// output - + 7 6 5
    ///
    /// input: 7 log 2 +
    /// output: + log 7 2
    /// </summary>
    public class Question49 {
        public static void Init (string[] args) {
            PostfixToPrefix (new List<PFElement> () { new PFLiteral (5), new PFLiteral (4), new PFOperator ("+") });
            
            PostfixToPrefix (new List<PFElement> () { new PFLiteral (7), new PFLiteral (6), new PFOperator ("+"), new PFLiteral (5), new PFOperator ("-") });
            
            PostfixToPrefix (new List<PFElement> () { new PFLiteral (7), new PFOperator ("log"), new PFLiteral (2), new PFOperator ("+") });
        }

        public static string PostfixToPrefix (List<PFElement> exp) {
            StringBuilder expression = new StringBuilder ();
            StringBuilder sb = new StringBuilder ();

            if (exp != null && exp.Count > 0) {
                StringBuilder literals = new StringBuilder ();
                Stack<string> operators = new Stack<string> ();

                foreach (var item in exp) {
                    if (item is PFLiteral) {
                        expression.Append (((PFLiteral) item).data);
                        literals.Append (((PFLiteral) item).data);
                    } else if (item is PFOperator) {
                        expression.Append (((PFOperator) item).optr);
                        operators.Push (((PFOperator) item).optr);
                    }
                }

                while (operators.Count > 0) {
                    sb.Append (operators.Pop ());
                }

                sb.Append (literals.ToString ());
            }

            Console.WriteLine (expression.ToString () + " : " + sb.ToString ());

            return sb.ToString ();
        }
    }

    public interface PFElement { }

    public class PFLiteral : PFElement {
        public int data;

        public PFLiteral (int data) {
            this.data = data;
        }
    }

    public class PFOperator : PFElement {
        public string optr;

        public PFOperator (string optr) {
            this.optr = optr;
        }
    }
}