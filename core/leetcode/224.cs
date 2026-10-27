// Url: https://leetcode.com/problems/basic-calculator/

/*
224. Basic Calculator
Hard

Implement a basic calculator to evaluate a simple expression string.

The expression string may contain open ( and closing parentheses ), the plus + or minus sign -, non-negative integers and empty spaces .

Example 1:
Input: "1 + 1"
Output: 2

Example 2:
Input: " 2-1 + 2 "
Output: 3

Example 3:
Input: "(1+(4+5+2)-3)+(6+8)"
Output: 23

Note:
You may assume that the given expression is always valid.
Do not use the eval built-in library function.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem224 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (Calculate(null));
            Console.WriteLine (Calculate("42"));
            Console.WriteLine (Calculate("3+2*2"));
            Console.WriteLine (Calculate("3/2 "));
            Console.WriteLine (Calculate(" 3+5 / 2 "));
            Console.WriteLine (Calculate("(1+(4+5+2)-3)+(6+8)"));
            Console.WriteLine (Calculate(" 2-1 + 2 "));
        }

        public static int Calculate (string s) {
            if (string.IsNullOrEmpty (s) || string.IsNullOrWhiteSpace (s)) {
                return 0;
            }

            int result = 0;
            Stack<int> nums = new Stack<int> ();
            Stack<char> op = new Stack<char> ();
            int i = 0;

            while (i < s.Length) {
                char c = s[i];
                i++;

                if (char.IsDigit (c)) {
                    int num = c - '0';

                    while (i < s.Length && char.IsDigit (s[i])) {
                        num = num * 10 + (s[i] - '0');
                        i++;
                    }

                    nums.Push (num);
                    num = 0;
                } else if (c == '(') {
                    op.Push (c);
                } else if (c == ')') {
                    while (op.Count != 0 && op.Peek () != '(' && GetPresidence(c) <= GetPresidence(op.Peek ())) {
                        nums.Push (Compute(nums, op));
                    }

                    op.Pop ();
                } else if (c != ' ') {
                    while (op.Count != 0 && GetPresidence(c) < GetPresidence(op.Peek ())) {
                        nums.Push (Compute(nums, op));
                    }

                    op.Push (c);
                }
            }

            while (op.Count != 0) {
                nums.Push (Compute(nums, op));
            }

            result = nums.Pop ();

            return result;
        }

        private static int Compute (Stack<int> nums, Stack<char> op) {
            int result = 0;

            int num2 = nums.Pop ();
            int num1 = nums.Pop ();

            switch (op.Pop ()) {
                case '/':
                    result = num1 / num2;
                    break;
                case '*':
                    result = num1 * num2;
                    break;
                case '+':
                    result = num1 + num2;
                    break;
                case '-':
                    result = num1 - num2;
                    break;
                default:
                    break;
            }

            return result;
        }

        private static int GetPresidence (char operand) {
            int result = 0;

            switch (operand) {
                case '(':
                    result = 0;
                    break;
                case ')':
                    result = 0;
                    break;
                case '/':
                    result = 2;
                    break;
                case '*':
                    result = 2;
                    break;
                case '+':
                    result = 1;
                    break;
                case '-':
                    result = 1;
                    break;
                default:
                    result = 0;
                    break;
            }

            return result;
        }
    }
}