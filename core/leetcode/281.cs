// Url: https://leetcode.com/problems/zigzag-iterator

/*
281. Zigzag Iterator
Medium

Given two 1D vectors, implement an iterator to return their elements alternately.

Example 1:
Input: v1 = [1,2], v2 = [3,4,5,6]
Output: [1,3,2,4,5,6]
Explanation: By calling next repeatedly until hasNext returns false, the order of elements returned by next should be: [1,3,2,4,5,6].

Follow up: What if you are given k vectors? How well can your code be extended to such cases?
Clarification for the k-vector case:
The "Zigzag" order is not clearly defined for k > 2 cases. However, we should return the elements in order like [v1[0], v2[0], ..., vk[0], v1[1], v2[1], ..., vk[1], ...].
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem281
{
    public class Solution
    {
        public static void Init()
        {
            List<int> v1 = [1, 2];
            List<int> v2 = [3, 4, 5, 6];
            ZigzagIterator it = new(v1, v2);

            List<int> res = [];
            while (it.HasNext())
            {
                res.Add(it.Next());
            }

            Console.WriteLine(string.Join(", ", res)); // 1, 3, 2, 4, 5, 6
        }
    }

    // Time:
    //   Next: O(1)
    //   HasNext: O(1)
    // Space: O(k) where k is the number of input vectors (k = 2 here, scalable to any k)
    public class ZigzagIterator
    {
        private readonly Queue<IEnumerator<int>> _queue;

        public ZigzagIterator(IList<int> v1, IList<int> v2)
        {
            _queue = new Queue<IEnumerator<int>>();

            if (v1 != null)
            {
                var it1 = v1.GetEnumerator();
                if (it1.MoveNext())
                {
                    _queue.Enqueue(it1);
                }
            }

            if (v2 != null)
            {
                var it2 = v2.GetEnumerator();
                if (it2.MoveNext())
                {
                    _queue.Enqueue(it2);
                }
            }
        }

        public int Next()
        {
            var it = _queue.Dequeue();
            int current = it.Current;

            if (it.MoveNext())
            {
                _queue.Enqueue(it);
            }

            return current;
        }

        public bool HasNext()
        {
            return _queue.Count > 0;
        }
    }
}