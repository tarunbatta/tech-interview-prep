using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.App.PracticeQuestions {
    /// <summary>
    /// Check for balanced parentheses in an expression
    /// Given an expression string exp , write a program to examine whether the pairs and the orders of “{“,”}”,”(“,”)”,”[“,”]” are correct in exp.
    /// For example, the program should print true for exp = “[()]{}{[()()]()}” and false for exp = “[(])”
    /// </summary>
    public class Question43 {
        public static void Init (string[] args) {
            CheckBalanced (null);
            
            CheckBalanced ("[{()}]");
            
            CheckBalanced ("[{{{{{");
            
            CheckBalanced ("[[({({})})]][{()}]");
            
            CheckBalanced ("[}}}}}}}}");
            
            CheckBalanced ("]))}}}}}}}}");
        }

        public static void CheckBalanced (string str) {
            bool result = true;

            if (!string.IsNullOrEmpty (str)) {
                int checkEven = str.Length % 2;

                if (checkEven == 0) {
                    char[] arr = str.ToCharArray ();
                    Stack<char> s = new Stack<char> ();

                    for (int i = 0; i < arr.Length; i++) {
                        if (arr[i] == '[' || arr[i] == '{' || arr[i] == '(') {
                            s.Push (arr[i]);
                        } else {
                            if (s.Count == 0) {
                                result = false;
                                break;
                            } else {
                                char p = s.Peek ();

                                if (p == '[' && arr[i] == ']' || p == '{' && arr[i] == '}' || p == '(' && arr[i] == ')') {
                                    s.Pop ();
                                }
                            }
                        }
                    }

                    if (s.Count != 0) {
                        result = false;
                    }
                } else {
                    result = false;
                }
            }

            Console.WriteLine (str + " : " + result);
        }
    }
}