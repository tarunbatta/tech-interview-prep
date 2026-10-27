using System.Linq;
// Url:https://leetcode.com/problems/minimum-cost-to-connect-sticks

/*
1167. Minimum Cost to Connect Sticks
Medium

Given n ropes of different lengths, we need to connect these ropes into one rope. We can connect only 2 ropes at a time. The cost required to connect 2 ropes is equal to sum of their lengths. The length of this connected rope is also equal to the sum of their lengths. This process is repeated until n ropes are connected into a single rope. Find the min possible cost required to connect all ropes.

Example 1:

Input: ropes = [8, 4, 6, 12]
Output: 58
Explanation: The optimal way to connect ropes is as follows
1. Connect the ropes of length 4 and 6 (cost is 10). Ropes after connecting: [8, 10, 12]
2. Connect the ropes of length 8 and 10 (cost is 18). Ropes after connecting: [18, 12]
3. Connect the ropes of length 18 and 12 (cost is 30).
Total cost to connect the ropes is 10 + 18 + 30 = 58
Example 2:

Input: ropes = [20, 4, 8, 2]
Output: 54
Example 3:

Input: ropes = [1, 2, 5, 10, 35, 89]
Output: 224
Example 4:

Input: ropes = [2, 2, 3, 3]
Output: 20
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1167 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (ConnectSticks(null));
            Console.WriteLine (ConnectSticks([]));
            Console.WriteLine (ConnectSticks([20, 4, 8, 2]));
            Console.WriteLine (ConnectSticks([1, 2, 5, 10, 35, 89]));
            Console.WriteLine (ConnectSticks([2, 2, 3, 3]));
        }

        // Time: O (n log (n))
        // Space: O (n)
        public static int ConnectSticks (int[] sticks) {
            if (sticks == null || sticks.Length == 0) {
                return 0;
            }

            MinHeap heap = new(sticks.Length);
            foreach (var item in sticks) {
                heap.Add (item);
            }

            int result = 0;

            while (heap.Count () > 1) {
                int item1 = heap.Pop ();
                int item2 = heap.Pop ();

                int newItem = item1 + item2;
                result += newItem;
                heap.Add (newItem);
            }

            return result;
        }
    }

    public class MinHeap(int size)
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
}