/*
Stack Min: 

How would you design a stack which, in addition to push and pop, has a function min
which returns the minimum element? Push, pop and min should all operate in 0(1) time.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c3q2 {
    public class Solution {
        public class StackMin {
            private readonly Stack<int> _stack = new Stack<int> ();
            private readonly Stack<int> _minStack = new Stack<int> ();

            public void Push (int value) {
                if (value <= Min ()) {
                    _minStack.Push (value);
                }
                _stack.Push (value);
            }

            public int Pop () {
                int value = _stack.Pop ();
                if (value == Min ()) {
                    _minStack.Pop ();
                }
                return value;
            }

            public int Min () {
                if (_minStack.Count == 0) {
                    return int.MaxValue;
                }
                return _minStack.Peek ();
            }
        }

        public static void Init (string[] args = null) {
            StackMin stack = new StackMin ();
            stack.Push (5);
            stack.Push (6);
            stack.Push (3);
            stack.Push (7);
            Console.WriteLine ($"Min: {stack.Min ()}"); // 3
            stack.Pop (); // 7
            stack.Pop (); // 3
            Console.WriteLine ($"Min after popping 7 and 3: {stack.Min ()}"); // 5
        }
    }
}
