// Url:https://leetcode.com/problems/basic-calculator-iii

/*
772. Basic Calculator III
Hard

Implement a basic calculator to evaluate a simple expression string.

The expression string contains only non-negative integers, '+', '-', '*', '/' operators, and open '(' and closing ')' parentheses. All intermediate results will be in the range of [-2^31, 2^31 - 1].

Note: You are not allowed to use any built-in library function which evaluates strings as mathematical expressions, such as eval().

Example 1:
Input: s = "1+1"
Output: 2

Example 2:
Input: s = "6-4/2"
Output: 4

Example 3:
Input: s = "2*(5+5*2)/3+(6/2+8)"
Output: 21

Constraints:
- 1 <= s <= 10^4
- s consists of digits, '+', '-', '*', '/', '(', ')', and ' '.
- s is a valid expression.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem772 {
    public class Solution {
        public void Init() {
            Console.WriteLine(Calculate("1+1")); // 2
            Console.WriteLine(Calculate("6-4/2")); // 4
            Console.WriteLine(Calculate("2*(5+5*2)/3+(6/2+8)")); // 21
        }

        // Time: O(n) where n is the length of expression s
        // Space: O(n) for stack and recursion call frames
        public int Calculate(string s) {
            int i = 0;
            return (int)Evaluate(s, ref i);
        }

        private static long Evaluate(string s, ref int i) {
            Stack<long> stack = [];
            char op = '+';
            long num = 0;

            while (i < s.Length) {
                char c = s[i++];

                if (char.IsDigit(c)) {
                    num = num * 10 + (c - '0');
                } else if (c == '(') {
                    num = Evaluate(s, ref i);
                }

                if ((!char.IsDigit(c) && c != ' ') || i == s.Length) {
                    switch (op) {
                        case '+':
                            stack.Push(num);
                            break;
                        case '-':
                            stack.Push(-num);
                            break;
                        case '*':
                            stack.Push(stack.Pop() * num);
                            break;
                        case '/':
                            stack.Push(stack.Pop() / num);
                            break;
                    }

                    op = c;
                    num = 0;
                }

                if (c == ')') {
                    break;
                }
            }

            long sum = 0;
            while (stack.Count > 0) {
                sum += stack.Pop();
            }

            return sum;
        }
    }
}