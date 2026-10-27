// Url:https://leetcode.com/problems/max-stack

/*
716. Max Stack
Hard

Design a max stack data structure that supports the stack operations and supports finding the stack's maximum element and retrieving the maximum element.

Implement the MaxStack class:
- MaxStack() Initializes the stack object.
- void Push(int x) Pushes element x onto the stack.
- int Pop() Removes the element on top of the stack and returns it.
- int Top() Gets the element on the top of the stack without removing it.
- int PeekMax() Retrieves the maximum element in the stack without removing it.
- int PopMax() Retrieves the maximum element in the stack and removes it. If there is more than one maximum element, only remove the top-most one.

You must come up with an algorithm that supports push, pop, top, peekMax, and popMax with O(log n) time complexity or better for each operation.

Example 1:
Input
["MaxStack", "push", "push", "push", "top", "popMax", "top", "peekMax", "pop", "top"]
[[], [5], [1], [5], [], [], [], [], [], []]
Output
[null, null, null, null, 5, 5, 1, 5, 1, 5]

Explanation
MaxStack stk = new MaxStack();
stk.push(5);   // [5] the top of the stack and the maximum number is 5.
stk.push(1);   // [5, 1] the top of the stack is 1, but the maximum is 5.
stk.push(5);   // [5, 1, 5] the top of the stack is 5, which is also the maximum, because it is the top-most one.
stk.top();     // return 5, [5, 1, 5] the stack did not change.
stk.popMax();  // return 5, [5, 1] the stack is changed now, and the top is different from the max.
stk.top();     // return 1, [5, 1] the stack did not change.
stk.peekMax(); // return 5, [5, 1] the stack did not change.
stk.pop();     // return 1, [5] the top of the stack and the max number is now 5.
stk.top();     // return 5, [5] the stack did not change.

Constraints:
- -10^7 <= x <= 10^7
- At most 10^5 calls will be made to push, pop, top, peekMax, and popMax.
- There will be at least one element in the stack when pop, top, peekMax, or popMax is called.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem716 {
    public class MaxStack {
        private readonly LinkedList<int> _list = [];
        private readonly SortedDictionary<int, List<LinkedListNode<int>>> _map = [];

        // Time: O(log n)
        // Space: O(1)
        public void Push(int x) {
            var node = _list.AddLast(x);
            if (!_map.TryGetValue(x, out var nodes)) {
                nodes = [];
                _map[x] = nodes;
            }
            nodes.Add(node);
        }

        // Time: O(log n)
        // Space: O(1)
        public int Pop() {
            var node = _list.Last!;
            _list.RemoveLast();
            RemoveNodeFromMap(node.Value, node);
            return node.Value;
        }

        // Time: O(1)
        // Space: O(1)
        public int Top() {
            return _list.Last!.Value;
        }

        // Time: O(log n)
        // Space: O(1)
        public int PeekMax() {
            using var enumerator = _map.Keys.GetEnumerator();
            int maxVal = 0;
            // In SortedDictionary, the last element is the maximum
            foreach (var key in _map.Keys) {
                maxVal = key;
            }
            return maxVal;
        }

        // Time: O(log n)
        // Space: O(1)
        public int PopMax() {
            int maxVal = PeekMax();
            var nodes = _map[maxVal];
            var node = nodes[^1];
            nodes.RemoveAt(nodes.Count - 1);
            if (nodes.Count == 0) {
                _map.Remove(maxVal);
            }

            _list.Remove(node);
            return maxVal;
        }

        private void RemoveNodeFromMap(int val, LinkedListNode<int> node) {
            var nodes = _map[val];
            nodes.RemoveAt(nodes.Count - 1);
            if (nodes.Count == 0) {
                _map.Remove(val);
            }
        }
    }

    public class Solution {
        public void Init() {
            MaxStack stk = new();
            stk.Push(5);
            stk.Push(1);
            stk.Push(5);
            Console.WriteLine(stk.Top());     // 5
            Console.WriteLine(stk.PopMax());  // 5
            Console.WriteLine(stk.Top());     // 1
            Console.WriteLine(stk.PeekMax()); // 5
            Console.WriteLine(stk.Pop());     // 1
            Console.WriteLine(stk.Top());     // 5
        }
    }
}