/*
Parens: Implement an algorithm to print all valid (e.g., properly opened and closed) combinations
of n pairs of parentheses.
EXAMPLE
Input: 3
Output: ((())), (()()), (())(), ()(()), ()()()
*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.Ctci.c8q9 {
    public class Solution {
        public static void Init (string[] args = null) {
            GenerateParens (0);

            GenerateParens (1);

            GenerateParens (2);

            GenerateParens (3);
        }

        public static void GenerateParens (int count) {
            if (count > 0) {
                ArrayList result = [];
                char[] parens = new char[count * 2];

                GenerateParens (result, count, count, parens, 0);

                foreach (var item in result) {
                    Console.WriteLine (item);
                }
            }
        }

        public static void GenerateParens (ArrayList list, int left, int right, char[] parens, int index) {
            if (left < 0 || right < left) {
                return;
            }

            if (left == 0 && right == 0) {
                list.Add (new string (parens));
            } else {
                parens[index] = '(';
                GenerateParens (list, left - 1, right, parens, index + 1);

                parens[index] = ')';
                GenerateParens (list, left, right - 1, parens, index + 1);
            }
        }
    }
}