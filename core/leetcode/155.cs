// Url:https://leetcode.com/problems/min-stack

/*
155. Min Stack
Easy

Design a stack that supports push, pop, top, and retrieving the minimum element in constant time.
	push(x) -- Push element x onto stack.
	pop() -- Removes the element on top of the stack.
	top() -- Get the top element.
	getMin() -- Retrieve the minimum element in the stack.

Example:

MinStack minStack = new MinStack();
minStack.push(-2);
minStack.push(0);
minStack.push(-3);
minStack.getMin();   --> Returns -3.
minStack.pop();
minStack.top();      --> Returns 0.
minStack.getMin();   --> Returns -2. 
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem155 {
    public class Solution {
        public static void Init () {
            MinStack obj = new();
            obj.Push (1);
            obj.Pop ();
            int param_3 = obj.Top ();
            int param_4 = obj.GetMin ();
        }
    }

    public class MinStack {
        private Stack<int> stack;
        private Stack<int> min;

        public MinStack () {
            stack = new Stack<int> ();
            min = new Stack<int> ();
        }

        public void Push (int x) {
            stack.Push (x);

            if (min.Count == 0 || x <= min.Peek ()) {
                min.Push (x);
            }
        }

        public void Pop () {
            int peek = stack.Pop ();

            if (min.Count != 0 && peek == min.Peek ()) {
                min.Pop ();
            }
        }

        public int Top () {
            return stack.Peek ();
        }

        public int GetMin () {
            return min.Peek ();
        }
    }
}