// Url: https://leetcode.com/problems/implement-queue-using-stacks/

/*
232. Implement Queue using Stacks
Easy

Implement the following operations of a queue using stacks.

push(x) -- Push element x to the back of queue.
pop() -- Removes the element from in front of queue.
peek() -- Get the front element.
empty() -- Return whether the queue is empty.
Example:

MyQueue queue = new MyQueue();

queue.push(1);
queue.push(2);  
queue.peek();  // returns 1
queue.pop();   // returns 1
queue.empty(); // returns false
Notes:

You must use only standard operations of a stack -- which means only push to top, peek/pop from top, size, and is empty operations are valid.
Depending on your language, stack may not be supported natively. You may simulate a stack by using a list or deque (double-ended queue), as long as you use only standard operations of a stack.
You may assume that all operations are valid (for example, no pop or peek operations will be called on an empty queue).
*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.LeetCode.problem232 {
    public class MyQueue {
        private Stack inbox;
        private Stack outbox;

        /** Initialize your data structure here. */
        public MyQueue () {
            inbox = new Stack ();
            outbox = new Stack ();
        }

        /** Push element x to the back of queue. */
        public void Push (int x) {
            inbox.Push (x);
        }

        /** Removes the element from in front of queue and returns that element. */
        public int Pop () {
            if (outbox.Count == 0) {
                while (inbox.Count != 0) {
                    outbox.Push (inbox.Pop ());
                }
            }

            return (int) outbox.Pop ();
        }

        /** Get the front element. */
        public int Peek () {
            if (outbox.Count == 0) {
                while (inbox.Count != 0) {
                    outbox.Push (inbox.Pop ());
                }
            }

            return (int) outbox.Peek ();
        }

        /** Returns whether the queue is empty. */
        public bool IsEmpty () {
            return inbox.Count == 0 && outbox.Count == 0;
        }
    }
}