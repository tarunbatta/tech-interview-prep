// Url:https://leetcode.com/problems/find-median-from-data-stream

/*
295. Find Median from Data Stream
Hard

Median is the middle value in an ordered integer list. If the size of the list is even, there is no middle value. So the median is the mean of the two middle value.
For example,

[2,3,4], the median is 3

[2,3], the median is (2 + 3) / 2 = 2.5

Design a data structure that supports the following two operations:
	void addNum(int num) - Add a integer number from the data stream to the data structure.
	double findMedian() - Return the median of all elements so far.

Example:
addNum(1)
addNum(2)
findMedian() -> 1.5
addNum(3) 
findMedian() -> 2


Follow up:
	If all integer numbers from the stream are between 0 and 100, how would you optimize it?
	If 99% of all integer numbers from the stream are between 0 and 100, how would you optimize it?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem295 {
    public class Solution {
        public static void Init () {
            MedianFinder obj = new MedianFinder ();
            obj.AddNum (1);
            double param_2 = obj.FindMedian ();
        }
    }

    // Time: O (log n)
    // Space: O (n)
    public class MedianFinder {
        private MinHeap _min;
        private MaxHeap _max;
        private int _capacity = 10000;

        public MedianFinder () {
            _min = new MinHeap (_capacity);
            _max = new MaxHeap (_capacity);
        }

        public void AddNum (int num) {
            _min.Add (num); // Add to max heap

            _max.Add (_min.Peek ()); // balancing step
            _min.Pop ();

            if (_min.Count () < _max.Count ()) { // maintain size property
                _min.Add (_max.Peek ());
                _max.Pop ();
            }
        }

        public double FindMedian () {
            return _min.Count () > _max.Count () ? (double) _min.Peek () : (_min.Peek () + _max.Peek ()) * 0.5;
        }
    }

    public class MinHeap {
        private readonly int[] _elements;
        private int _size;

        public MinHeap (int size) {
            _elements = new int[size];
        }

        private static int GetLeftChildIndex (int elementIndex) => 2 * elementIndex + 1;
        private static int GetRightChildIndex (int elementIndex) => 2 * elementIndex + 2;
        private static int GetParentIndex (int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild (int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
        private bool HasRightChild (int elementIndex) => GetRightChildIndex(elementIndex) < _size;
        private static bool IsRoot (int elementIndex) => elementIndex == 0;

        private int GetLeftChild (int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
        private int GetRightChild (int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
        private int GetParent (int elementIndex) => _elements[GetParentIndex(elementIndex)];

        private void Swap (int firstIndex, int secondIndex) {
            var temp = _elements[firstIndex];
            _elements[firstIndex] = _elements[secondIndex];
            _elements[secondIndex] = temp;
        }

        public bool IsEmpty () {
            return _size == 0;
        }

        public int Count () {
            return _size;
        }

        public int Peek () {
            if (_size == 0)
                throw new IndexOutOfRangeException ();

            return _elements[0];
        }

        public int Pop () {
            if (_size == 0)
                throw new IndexOutOfRangeException ();

            var result = _elements[0];
            _elements[0] = _elements[_size - 1];
            _size--;

            ReCalculateDown ();

            return result;
        }

        public void Add (int element) {
            if (_size == _elements.Length)
                throw new IndexOutOfRangeException ();

            _elements[_size] = element;
            _size++;

            ReCalculateUp ();
        }

        private void ReCalculateDown () {
            int index = 0;

            while (HasLeftChild (index)) {
                var smallerIndex = GetLeftChildIndex(index);

                if (HasRightChild (index) && GetRightChild (index) < GetLeftChild (index)) {
                    smallerIndex = GetRightChildIndex(index);
                }

                if (_elements[smallerIndex] >= _elements[index]) {
                    break;
                }

                Swap (smallerIndex, index);
                index = smallerIndex;
            }
        }

        private void ReCalculateUp () {
            var index = _size - 1;

            while (!IsRoot(index) && _elements[index] < GetParent (index)) {
                var parentIndex = GetParentIndex(index);
                Swap (parentIndex, index);
                index = parentIndex;
            }
        }
    }

    public class MaxHeap {
        private readonly int[] _elements;
        private int _size;

        public MaxHeap (int size) {
            _elements = new int[size];
        }

        private static int GetLeftChildIndex (int elementIndex) => 2 * elementIndex + 1;
        private static int GetRightChildIndex (int elementIndex) => 2 * elementIndex + 2;
        private static int GetParentIndex (int elementIndex) => (elementIndex - 1) / 2;

        private bool HasLeftChild (int elementIndex) => GetLeftChildIndex(elementIndex) < _size;
        private bool HasRightChild (int elementIndex) => GetRightChildIndex(elementIndex) < _size;
        private static bool IsRoot (int elementIndex) => elementIndex == 0;

        private int GetLeftChild (int elementIndex) => _elements[GetLeftChildIndex(elementIndex)];
        private int GetRightChild (int elementIndex) => _elements[GetRightChildIndex(elementIndex)];
        private int GetParent (int elementIndex) => _elements[GetParentIndex(elementIndex)];

        private void Swap (int firstIndex, int secondIndex) {
            var temp = _elements[firstIndex];
            _elements[firstIndex] = _elements[secondIndex];
            _elements[secondIndex] = temp;
        }

        public bool IsEmpty () {
            return _size == 0;
        }

        public int Count () {
            return _size;
        }

        public int Peek () {
            if (_size == 0)
                throw new IndexOutOfRangeException ();

            return _elements[0];
        }

        public int Pop () {
            if (_size == 0)
                throw new IndexOutOfRangeException ();

            var result = _elements[0];
            _elements[0] = _elements[_size - 1];
            _size--;

            ReCalculateDown ();

            return result;
        }

        public void Add (int element) {
            if (_size == _elements.Length)
                throw new IndexOutOfRangeException ();

            _elements[_size] = element;
            _size++;

            ReCalculateUp ();
        }

        private void ReCalculateDown () {
            int index = 0;
            while (HasLeftChild (index)) {
                var biggerIndex = GetLeftChildIndex(index);
                if (HasRightChild (index) && GetRightChild (index) > GetLeftChild (index)) {
                    biggerIndex = GetRightChildIndex(index);
                }

                if (_elements[biggerIndex] < _elements[index]) {
                    break;
                }

                Swap (biggerIndex, index);
                index = biggerIndex;
            }
        }

        private void ReCalculateUp () {
            var index = _size - 1;
            while (!IsRoot(index) && _elements[index] > GetParent (index)) {
                var parentIndex = GetParentIndex(index);
                Swap (parentIndex, index);
                index = parentIndex;
            }
        }
    }
}