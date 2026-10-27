// Url:https://leetcode.com/problems/implement-stack-using-queues

/*
225. Implement Stack using Queues
Easy

Implement the following operations of a stack using queues.


	push(x) -- Push element x onto stack.
	pop() -- Removes the element on top of the stack.
	top() -- Get the top element.
	empty() -- Return whether the stack is empty.


Example:

MyStack stack = new MyStack();

stack.push(1);
stack.push(2);  
stack.top();   // returns 2
stack.pop();   // returns 2
stack.empty(); // returns false

Notes:


	You must use only standard operations of a queue -- which means only push to back, peek/pop from front, size, and is empty operations are valid.
	Depending on your language, queue may not be supported natively. You may simulate a queue by using a list or deque (double-ended queue), as long as you use only standard operations of a queue.
	You may assume that all operations are valid (for example, no pop or top operations will be called on an empty stack).
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem225 {
    public class Solution {
        public static void Init () {
            MyStack obj = new MyStack ();
            obj.Push (1);
            int param_2 = obj.Pop ();
            int param_3 = obj.Top ();
            bool param_4 = obj.Empty ();
        }
    }

    // Time: O (n)
    // Space: O (1)
    public class MyStack {
        Queue<int> _q;

        /** Initialize your data structure here. */
        public MyStack () {
            _q = new Queue<int> ();
        }

        // Push element x onto stack.
        public void Push (int x) {
            _q.Enqueue (x);
            int count = _q.Count;

            while (count > 1) {
                _q.Enqueue (_q.Dequeue ());
                count--;
            }
        }

        // Removes the element on top of the stack and returns that element. 
        public int Pop () {
            return _q.Dequeue ();
        }

        // Get the top element.
        public int Top () {
            return _q.Peek ();
        }

        // Returns whether the stack is empty.
        public bool Empty () {
            return _q.Count == 0;
        }
    }
}