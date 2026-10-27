// Url:https://leetcode.com/problems/moving-average-from-data-stream

/*
346. Moving Average from Data Stream
Easy

Given a stream of integers and a window size, calculate the moving average of all integers in the sliding window.

Example:

MovingAverage m = new MovingAverage(3);
m.next(1) = 1
m.next(10) = (1 + 10) / 2
m.next(3) = (1 + 10 + 3) / 3
m.next(5) = (10 + 3 + 5) / 3
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem346 {
    public class Solution {
        public static void Init () {
            MovingAverage obj = new(3);
            double param_1 = obj.Next (1);
        }
    }

    public class MovingAverage(int size)
    {
        Queue<int> _queue = new(size);
        int _size = size;

        public double Next (int val) {
            if (_queue.Count >= _size) {
                _queue.Dequeue ();
            }
            _queue.Enqueue (val);

            double result = 0;
            foreach (var item in _queue) {
                result += item;
            }

            return result / _queue.Count;
        }
    }
}