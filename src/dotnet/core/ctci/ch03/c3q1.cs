/*
Three in One: 

Describe how you could use a single array to implement three stacks.
*/

using System;

namespace InterviewPreperationGuide.Core.Ctci.c3q1 {
    public class Solution {
        public class FixedMultiStack {
            private const int NumberOfStacks = 3;
            private readonly int _stackCapacity;
            private readonly int[] _values;
            private readonly int[] _sizes;

            public FixedMultiStack (int stackSize) {
                _stackCapacity = stackSize;
                _values = new int[stackSize * NumberOfStacks];
                _sizes = new int[NumberOfStacks];
            }

            public void Push (int stackNum, int value) {
                if (IsFull (stackNum)) {
                    throw new InvalidOperationException ($"Stack {stackNum} is full.");
                }
                _values[IndexOfTop (stackNum)] = value;
                _sizes[stackNum]++;
            }

            public int Pop (int stackNum) {
                if (IsEmpty (stackNum)) {
                    throw new InvalidOperationException ($"Stack {stackNum} is empty.");
                }
                _sizes[stackNum]--;
                return _values[IndexOfTop (stackNum)];
            }

            public int Peek (int stackNum) {
                if (IsEmpty (stackNum)) {
                    throw new InvalidOperationException ($"Stack {stackNum} is empty.");
                }
                return _values[IndexOfTop (stackNum) - 1];
            }

            public bool IsEmpty (int stackNum) {
                return _sizes[stackNum] == 0;
            }

            public bool IsFull (int stackNum) {
                return _sizes[stackNum] == _stackCapacity;
            }

            private int IndexOfTop (int stackNum) {
                int offset = stackNum * _stackCapacity;
                int size = _sizes[stackNum];
                return offset + size;
            }
        }

        public static void Init (string[] args = null) {
            FixedMultiStack stack = new FixedMultiStack (5);
            stack.Push (0, 10);
            stack.Push (0, 20);
            stack.Push (1, 100);
            stack.Push (2, 1000);

            Console.WriteLine ($"Stack 0 Pop: {stack.Pop (0)}"); // 20
            Console.WriteLine ($"Stack 1 Peek: {stack.Peek (1)}"); // 100
            Console.WriteLine ($"Stack 2 Pop: {stack.Pop (2)}"); // 1000
        }
    }
}
