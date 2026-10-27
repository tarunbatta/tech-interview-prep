// Url:https://leetcode.com/problems/meeting-rooms-ii

/*
253. Meeting Rooms II
Medium

Given an array of meeting time intervals consisting of start and end times [[s1,e1],[s2,e2],...] (si < ei), find the minimum number of conference rooms required.

Example 1:

Input: [[0, 30],[5, 10],[15, 20]]
Output: 2
Example 2:

Input: [[7,10],[2,4]]
Output: 1
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem253 {
    public class Solution {
        public static void Init () {

        }

        // Time: O (n log(n))
        // Space: O (n)
        public static int MinMeetingRooms_a (int[][] intervals) {
            if (intervals == null || intervals.Length == 0 || intervals[0].Length == 0) {
                return 0;
            }

            int n = intervals.Length;
            int[] startTimes = new int[n];
            int[] endTimes = new int[n];

            for (int i = 0; i < n; i++) {
                startTimes[i] = intervals[i][0];
                endTimes[i] = intervals[i][1];
            }

            Array.Sort (startTimes);
            Array.Sort (endTimes);

            int minMeetingRooms = 0;
            int endTimesIterator = 0;

            for (int i = 0; i < n; i++) {
                // Increment the room for the current meeting that is starting
                minMeetingRooms++;

                // Check if startTime of current meeting is after endTime of meeting that is suppose to end first
                if (startTimes[i] >= endTimes[endTimesIterator]) {
                    minMeetingRooms--;
                    endTimesIterator++;
                }
            }

            return minMeetingRooms;
        }

        // Time: O (n)
        // Space: O (n)
        public static int MinMeetingRooms_b (int[][] intervals) {
            if (intervals == null || intervals.Length == 0 || intervals[0].Length == 0) {
                return 0;
            }

            Array.Sort (intervals, (a, b) => a[0] - b[0]);

            MinHeap heap = new MinHeap (intervals.Length);
            heap.Add (intervals[0][1]);
            int result = 1;

            for (int i = 1; i < intervals.Length; i++) {
                var currentInteval = intervals[i];

                if (heap.Peek () <= currentInteval[0]) {
                    heap.Pop ();
                } else {
                    result++;
                }

                heap.Add (currentInteval[1]);
            }

            return result;
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