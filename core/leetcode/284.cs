// Url: https://leetcode.com/problems/peeking-iterator

/*
284. Peeking Iterator
Medium

Design an iterator that supports the peek operation on an existing iterator in addition to the hasNext and the next operations.

Implement the PeekingIterator class:

- PeekingIterator(Iterator<int> nums) Initializes the object with the given integer iterator.
- int peek() Returns the next element in the array without moving the pointer.
- int next() Returns the next element in the array and moves the pointer to the next element.
- boolean hasNext() Returns true if there are still elements in the array.

Example:
Input
["PeekingIterator", "next", "peek", "next", "next", "hasNext"]
[[[1, 2, 3]], [], [], [], [], []]
Output
[null, 1, 2, 2, 3, false]

Explanation
PeekingIterator peekingIterator = new PeekingIterator([1, 2, 3]); // [1,2,3]
peekingIterator.next();    // return 1, the pointer moves to the next element [1,2,3].
peekingIterator.peek();    // return 2, the pointer does not move [1,2,3]
peekingIterator.next();    // return 2, the pointer moves to the next element [1,2,3]
peekingIterator.next();    // return 3, the pointer moves to the next element [1,2,3]
peekingIterator.hasNext(); // return False

Constraints:
1 <= nums.length <= 1000
1 <= nums[i] <= 1000
All the calls to next and peek are valid.
At most 1000 calls will be made to next, peek, and hasNext.

Follow up: How would you extend your design to be generic and work with all types, not just integer?
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem284
{
    public class Solution
    {
        public void Init()
        {
            List<int> list = new() { 1, 2, 3 };
            PeekingIterator peekingIterator = new(list.GetEnumerator());
            Console.WriteLine(peekingIterator.Next());    // 1
            Console.WriteLine(peekingIterator.Peek());    // 2
            Console.WriteLine(peekingIterator.Next());    // 2
            Console.WriteLine(peekingIterator.Next());    // 3
            Console.WriteLine(peekingIterator.HasNext()); // False
        }
    }

    // Time:
    //   Constructor: O(1)
    //   Peek: O(1)
    //   Next: O(1)
    //   HasNext: O(1)
    // Space: O(1) auxiliary space
    public class PeekingIterator
    {
        private readonly IEnumerator<int> _iterator;
        private bool _hasPeeked;
        private int _peekedValue;

        // iter is an IEnumerator<int>
        public PeekingIterator(IEnumerator<int> iterator)
        {
            _iterator = iterator;
            _hasPeeked = false;
        }

        // Returns the next element in the iteration without advancing the iterator.
        public int Peek()
        {
            if (!_hasPeeked)
            {
                _peekedValue = _iterator.MoveNext() ? _iterator.Current : default;
                _hasPeeked = true;
            }

            return _peekedValue;
        }

        // Returns the next element in the iteration and advances the iterator.
        public int Next()
        {
            if (_hasPeeked)
            {
                _hasPeeked = false;
                return _peekedValue;
            }

            if (_iterator.MoveNext())
            {
                return _iterator.Current;
            }

            return default;
        }

        // Returns true if the iteration has more elements.
        public bool HasNext()
        {
            if (_hasPeeked)
            {
                return true;
            }

            if (_iterator.MoveNext())
            {
                _peekedValue = _iterator.Current;
                _hasPeeked = true;
                return true;
            }

            return false;
        }
    }
}