// Url: https://leetcode.com/problems/nested-list-weight-sum-ii

/*
364. Nested List Weight Sum II
Medium

You are given a nested list of integers nestedList. Each element is either an integer or a list whose elements may also be integers or other lists.

The depth of an integer is the number of lists that it is inside of. For example, the nested list [1,[2,2],[[3],2],1] has each integer's value set to its depth. Let maxDepth be the maximum depth of any integer.

The weight of an integer is maxDepth - (the depth of the integer) + 1.

Return the sum of each integer in nestedList multiplied by its weight.

Example 1:
Input: nestedList = [[1,1],2,[1,1]]
Output: 8
Explanation: Four 1's with a weight of 1, one 2 with a weight of 2.
1*1 + 1*1 + 2*2 + 1*1 + 1*1 = 8.

Example 2:
Input: nestedList = [1,[4,[6]]]
Output: 17
Explanation: One 1 at depth 3, one 4 at depth 2, and one 6 at depth 1.
1*3 + 4*2 + 6*1 = 17.

Constraints:
1 <= nestedList.length <= 50
The values of the integers in the nested list are in the range [-100, 100].
The maximum depth of any integer is less than or equal to 50.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem364
{
    public interface NestedInteger
    {
        bool IsInteger();
        int GetInteger();
        void SetInteger(int value);
        void Add(NestedInteger ni);
        IList<NestedInteger> GetList();
    }

    public class NestedIntegerImpl : NestedInteger
    {
        private int? _value;
        private readonly List<NestedInteger> _list;

        public NestedIntegerImpl(int value)
        {
            _value = value;
            _list = null;
        }

        public NestedIntegerImpl()
        {
            _value = null;
            _list = [];
        }

        public bool IsInteger() => _value.HasValue;
        public int GetInteger() => _value ?? 0;
        public void SetInteger(int value) => _value = value;
        public void Add(NestedInteger ni) => _list?.Add(ni);
        public IList<NestedInteger> GetList() => _list;
    }

    public class Solution
    {
        public static void Init()
        {
            // [[1,1],2,[1,1]]
            NestedInteger l1 = new NestedIntegerImpl();
            l1.Add(new NestedIntegerImpl(1));
            l1.Add(new NestedIntegerImpl(1));

            NestedInteger n2 = new NestedIntegerImpl(2);

            NestedInteger l2 = new NestedIntegerImpl();
            l2.Add(new NestedIntegerImpl(1));
            l2.Add(new NestedIntegerImpl(1));

            List<NestedInteger> list = [l1, n2, l2];
            Console.WriteLine(DepthSumInverse(list)); // 8
        }

        // Time: O(N) where N is the total number of nested elements
        // Space: O(N) for BFS level queue
        public static int DepthSumInverse(IList<NestedInteger> nestedList)
        {
            if (nestedList == null || nestedList.Count == 0)
            {
                return 0;
            }

            Queue<NestedInteger> queue = new(nestedList);
            int unweightedSum = 0;
            int weightedSum = 0;

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;

                for (int i = 0; i < levelSize; i++)
                {
                    var item = queue.Dequeue();

                    if (item.IsInteger())
                    {
                        unweightedSum += item.GetInteger();
                    }
                    else if (item.GetList() != null)
                    {
                        foreach (var sub in item.GetList())
                        {
                            queue.Enqueue(sub);
                        }
                    }
                }

                weightedSum += unweightedSum;
            }

            return weightedSum;
        }
    }
}