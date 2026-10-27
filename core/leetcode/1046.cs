// Url: https://leetcode.com/problems/last-stone-weight/

/*
1046. Last Stone Weight
Easy

We have a collection of rocks, each rock has a positive integer weight.

Each turn, we choose the two heaviest rocks and smash them together.  Suppose the stones have weights x and y with x <= y.  The result of this smash is:

If x == y, both stones are totally destroyed;
If x != y, the stone of weight x is totally destroyed, and the stone of weight y has new weight y-x.
At the end, there is at most 1 stone left.  Return the weight of this stone (or 0 if there are no stones left.)

 

Example 1:

Input: [2,7,4,1,8,1]
Output: 1
Explanation: 
We combine 7 and 8 to get 1 so the array converts to [2,4,1,1,1] then,
we combine 2 and 4 to get 2 so the array converts to [2,1,1,1] then,
we combine 2 and 1 to get 1 so the array converts to [1,1,1] then,
we combine 1 and 1 to get 0 so the array converts to [1] then that's the value of last stone.
 

Note:

1 <= stones.length <= 30
1 <= stones[i] <= 1000
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem1046 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (LastStoneWeight(null));
            Console.WriteLine (LastStoneWeight([2, 7, 4, 1, 8, 1]));
        }

        // Time: O (n logn (n))
        // Space: O (n)
        public static int LastStoneWeight (int[] stones) {
            if (stones == null || stones.Length == 0) {
                return 0;
            }

            MaxHeap heap = new(stones.Length);
            for (int i = 0; i < stones.Length; i++) {
                heap.Add (stones[i]);
            }

            while (heap.Count () > 1) {
                int item1 = heap.Pop ();
                int item2 = heap.Pop ();

                int temp = item1 - item2;
                if (temp > 0) {
                    heap.Add (temp);
                }
            }

            return heap.Count () > 0 ? heap.Peek () : 0;
        }
    }

    public class MaxHeap(int size)
    {
        private readonly int[] _elements = new int[size];
        private int _size;

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