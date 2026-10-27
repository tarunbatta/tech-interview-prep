// Url: https://leetcode.com/problems/ternary-expression-parser

/*
439. Ternary Expression Parser
Medium

Given a string expression representing arbitrarily nested ternary expressions, evaluate the expression, and return the result of it.

You can always assume that the given expression is valid and only consists of digits, '?', ':', 'T', and 'F' where 'T' is true and 'F' is false. All the numbers in the expression are one-digit numbers (i.e., in the range [0, 9]).

The conditional expressions group right-to-left (as usual in most languages), and the result of the expression will always evaluate to either a digit, 'T' or 'F'.

Example 1:
Input: expression = "T?2:3"
Output: "2"
Explanation: If true, then result is 2; otherwise result is 3.

Example 2:
Input: expression = "F?1:T?4:5"
Output: "4"
Explanation: The conditional expressions group right-to-left. Using parenthesis, it is read/evaluated as:
"(F ? 1 : (T ? 4 : 5))" --> "(F ? 1 : 4)" --> "4"

Example 3:
Input: expression = "T?T?F:5:3"
Output: "F"
Explanation: The conditional expressions group right-to-left. Using parenthesis, it is read/evaluated as:
"(T ? (T ? F : 5) : 3)" --> "(T ? F : 3)" --> "F"

Constraints:
5 <= expression.length <= 10^4
expression consists of digits, 'T', 'F', '?', and ':'.
It is guaranteed that expression is a valid ternary expression and each number is a one-digit number.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem439
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(ParseTernary("T?2:3"));       // 2
            Console.WriteLine(ParseTernary("F?1:T?4:5"));   // 4
            Console.WriteLine(ParseTernary("T?T?F:5:3"));   // F
        }

        // Time: O(n) where n is expression.Length (right-to-left single pass with stack)
        // Space: O(n) for the evaluation stack
        public static string ParseTernary(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return string.Empty;
            }

            Stack<char> stack = new();

            for (int i = expression.Length - 1; i >= 0; i--)
            {
                char c = expression[i];

                if (stack.Count > 0 && stack.Peek() == '?')
                {
                    stack.Pop(); // pop '?'
                    char onTrue = stack.Pop();
                    stack.Pop(); // pop ':'
                    char onFalse = stack.Pop();

                    char result = (c == 'T') ? onTrue : onFalse;
                    stack.Push(result);
                }
                else
                {
                    stack.Push(c);
                }
            }

            return stack.Peek().ToString();
        }
    }
}