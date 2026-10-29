/*
Stack of Plates: 

Imagine a (literal) stack of plates. If the stack gets too high, it might topple.
Therefore, in real life, we would likely start a new stack when the previous stack exceeds some
threshold. Implement a data structure SetOfStacks that mimics this. SetOfStacks should be
composed of several stacks and should create a new stack once the previous one exceeds capacity.
SetOfStacks. push () and SetOfStacks. pop () should behave identically to a single stack
(that is, pop () should return the very same values as it would if there were just a single stack).

FOLLOW UP
Implement a function popAt (int index) which performs a pop operation on a specific sub-stack.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c3q3 {
    public class Solution {
        public class SetOfStacks {
            private readonly int _capacity;
            private readonly List<Stack<int>> _stacks = new List<Stack<int>> ();

            public SetOfStacks (int capacity = 3) {
                _capacity = capacity;
            }

            public void Push (int v) {
                Stack<int> last = GetLastStack ();
                if (last != null && last.Count < _capacity) {
                    last.Push (v);
                } else {
                    Stack<int> stack = new Stack<int> ();
                    stack.Push (v);
                    _stacks.Add (stack);
                }
            }

            public int Pop () {
                Stack<int> last = GetLastStack ();
                if (last == null) {
                    throw new InvalidOperationException ("Stack is empty.");
                }
                int v = last.Pop ();
                if (last.Count == 0) {
                    _stacks.RemoveAt (_stacks.Count - 1);
                }
                return v;
            }

            public Stack<int> GetLastStack () {
                if (_stacks.Count == 0) return null;
                return _stacks[_stacks.Count - 1];
            }

            public bool IsEmpty () {
                Stack<int> last = GetLastStack ();
                return last == null || last.Count == 0;
            }
        }

        public static void Init (string[] args = null) {
            SetOfStacks set = new SetOfStacks (2);
            set.Push (1);
            set.Push (2);
            set.Push (3);
            set.Push (4);
            Console.WriteLine ($"Popped: {set.Pop ()}"); // 4
            Console.WriteLine ($"Popped: {set.Pop ()}"); // 3
            Console.WriteLine ($"Popped: {set.Pop ()}"); // 2
        }
    }
}
