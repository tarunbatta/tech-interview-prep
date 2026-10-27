// Url:https://leetcode.com/problems/design-compressed-string-iterator

/*
604. Design Compressed String Iterator
Easy

Design and implement a data structure for a compressed string iterator. It should support the following operations: next and hasNext.

The given compressed string will be in the form of each letter followed by a positive integer representing the number of this letter being in the original uncompressed string.

next() - if the original string still has uncompressed characters, return the next letter; Otherwise return a white space.
hasNext() - Check if there is still a letter that needs to be uncompressed.

Example:

StringIterator iterator = new StringIterator("L1e2t1C1o1d1e1");

iterator.Next(); // return 'L'
iterator.Next(); // return 'e'
iterator.Next(); // return 'e'
iterator.Next(); // return 't'
iterator.Next(); // return 'C'
iterator.Next(); // return 'o'
iterator.Next(); // return 'd'
iterator.HasNext(); // return true
iterator.Next(); // return 'e'
iterator.HasNext(); // return false
iterator.Next(); // return ' '

Constraints:
- 1 <= compressedString.length <= 1000
- compressedString consists of lower-case and upper-case English letters and digits.
- The number of a single character will be in the range [1, 10^9]
- At most 100 calls will be made to next and hasNext.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem604 {
    public class StringIterator {
        private readonly string _compressed;
        private int _index;
        private char _currentChar;
        private long _currentCount;

        // Time: O(1)
        // Space: O(1)
        public StringIterator(string compressedString) {
            _compressed = compressedString;
            _index = 0;
            _currentChar = ' ';
            _currentCount = 0;
        }

        // Time: O(L) where L is the length of digits for one chunk (at most 10 chars)
        // Space: O(1)
        public char Next() {
            if (!HasNext()) {
                return ' ';
            }

            _currentCount--;
            return _currentChar;
        }

        // Time: O(L) where L is the length of digits for one chunk
        // Space: O(1)
        public bool HasNext() {
            if (_currentCount > 0) {
                return true;
            }

            if (_index >= _compressed.Length) {
                return false;
            }

            _currentChar = _compressed[_index++];
            long count = 0;
            while (_index < _compressed.Length && char.IsDigit(_compressed[_index])) {
                count = count * 10 + (_compressed[_index] - '0');
                _index++;
            }
            _currentCount = count;

            return true;
        }
    }

    public class Solution {
        public static void Init() {
            StringIterator iterator = new("L1e2t1C1o1d1e1");
            Console.WriteLine(iterator.Next()); // 'L'
            Console.WriteLine(iterator.Next()); // 'e'
            Console.WriteLine(iterator.Next()); // 'e'
            Console.WriteLine(iterator.Next()); // 't'
            Console.WriteLine(iterator.Next()); // 'C'
            Console.WriteLine(iterator.Next()); // 'o'
            Console.WriteLine(iterator.Next()); // 'd'
            Console.WriteLine(iterator.HasNext()); // true
            Console.WriteLine(iterator.Next()); // 'e'
            Console.WriteLine(iterator.HasNext()); // false
            Console.WriteLine($"'{iterator.Next()}'"); // ' '
        }
    }
}