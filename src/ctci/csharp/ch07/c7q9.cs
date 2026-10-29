/*
Circular Array: 

Implement a CircularArray class that supports an array-like data structure which
can be efficiently rotated. If possible, the class should use a generic type (also called a template), and
should support iteration via the standard for (each) loop.
*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c7q9 {
    public class Solution {
        public class CircularArray<T> : IEnumerable<T> {
            private readonly T[] _items;
            private int _head = 0;

            public int Length => _items.Length;

            public CircularArray (int size) {
                _items = new T[size];
            }

            private int Convert (int index) {
                if (index < 0) {
                    index += _items.Length;
                }
                return (_head + index) % _items.Length;
            }

            public void Rotate (int shiftRight) {
                _head = Convert (shiftRight);
            }

            public T this[int i] {
                get {
                    if (i < 0 || i >= _items.Length) {
                        throw new IndexOutOfRangeException ();
                    }
                    return _items[Convert (i)];
                }
                set {
                    if (i < 0 || i >= _items.Length) {
                        throw new IndexOutOfRangeException ();
                    }
                    _items[Convert (i)] = value;
                }
            }

            public IEnumerator<T> GetEnumerator () {
                for (int i = 0; i < _items.Length; i++) {
                    yield return this[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator () => GetEnumerator ();
        }

        public static void Init (string[] args = null) {
            CircularArray<string> array = new CircularArray<string> (4);
            array[0] = "A";
            array[1] = "B";
            array[2] = "C";
            array[3] = "D";

            Console.WriteLine ($"Before rotate: {string.Join (", ", array)}");
            array.Rotate (2);
            Console.WriteLine ($"After rotate by 2: {string.Join (", ", array)}");
        }
    }
}
