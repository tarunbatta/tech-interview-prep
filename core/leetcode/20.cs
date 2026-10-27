// Url:https://leetcode.com/problems/valid-parentheses

/*
20. Valid Parentheses
Easy

Given a string containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.

An input string is valid if:


	Open brackets must be closed by the same type of brackets.
	Open brackets must be closed in the correct order.


Note that an empty string is also considered valid.

Example 1:

Input: "()"
Output: true


Example 2:

Input: "()[]{}"
Output: true


Example 3:

Input: "(]"
Output: false


Example 4:

Input: "([)]"
Output: false


Example 5:

Input: "{[]}"
Output: true
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem20 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (IsValid(null));
            Console.WriteLine (IsValid(string.Empty));
            Console.WriteLine (IsValid("()"));
            Console.WriteLine (IsValid("()[]{}"));
            Console.WriteLine (IsValid("(]"));
            Console.WriteLine (IsValid("([)]"));
            Console.WriteLine (IsValid("{[]}"));
        }

        // Time: O (n)
        // Space: O (n)
        public static bool IsValid (string s) {
            if (string.IsNullOrEmpty (s)) {
                return true;
            }

            bool result = false;
            Stack<char> stack = new Stack<char> ();

            for (int i = 0; i < s.Length; i++) {
                if (stack.Count != 0 &&
                    ((stack.Peek () == '(' && s[i] == ')') ||
                        (stack.Peek () == '[' && s[i] == ']') ||
                        (stack.Peek () == '{' && s[i] == '}'))) {
                    stack.Pop ();
                } else {
                    stack.Push (s[i]);
                }
            }

            result = stack.Count == 0 ? true : false;

            return result;
        }
    }
}