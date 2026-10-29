/*
Queue via Stacks: 

Implement a MyQueue class which implements a queue using two stacks.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c3q4 {
    public class Solution {
        public class MyQueue<T> {
            private readonly Stack<T> _stackNew = new Stack<T> ();
            private readonly Stack<T> _stackOld = new Stack<T> ();

            public int Size => _stackNew.Count + _stackOld.Count;

            public void Add (T value) {
                _stackNew.Push (value);
            }

            private void ShiftStacks () {
                if (_stackOld.Count == 0) {
                    while (_stackNew.Count > 0) {
                        _stackOld.Push (_stackNew.Pop ());
                    }
                }
            }

            public T Peek () {
                ShiftStacks ();
                if (_stackOld.Count == 0) {
                    throw new InvalidOperationException ("Queue is empty.");
                }
                return _stackOld.Peek ();
            }

            public T Remove () {
                ShiftStacks ();
                if (_stackOld.Count == 0) {
                    throw new InvalidOperationException ("Queue is empty.");
                }
                return _stackOld.Pop ();
            }
        }

        public static void Init (string[] args = null) {
            MyQueue<int> q = new MyQueue<int> ();
            q.Add (1);
            q.Add (2);
            q.Add (3);
            Console.WriteLine ($"Peek: {q.Peek ()}"); // 1
            Console.WriteLine ($"Remove: {q.Remove ()}"); // 1
            Console.WriteLine ($"Remove: {q.Remove ()}"); // 2
        }
    }
}
