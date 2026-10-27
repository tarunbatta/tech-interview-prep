// Url: https://leetcode.com/problems/nested-list-weight-sum

/*
339. Nested List Weight Sum
Medium

You are given a nested list of integers nestedList. Each element is either an integer or a list whose elements may also be integers or other lists.

The depth of an integer is the number of lists that it is inside of. For example, the nested list [1,[2,2],[[3],2],1] has each integer's value set to its depth.

Return the sum of each integer in nestedList multiplied by its depth.

Example 1:
Input: nestedList = [[1,1],2,[1,1]]
Output: 10
Explanation: Four 1's at depth 2, one 2 at depth 1. 1*2 + 1*2 + 2*1 + 1*2 + 1*2 = 10.

Example 2:
Input: nestedList = [1,[4,[6]]]
Output: 27
Explanation: One 1 at depth 1, one 4 at depth 2, and one 6 at depth 3. 1*1 + 4*2 + 6*3 = 27.

Example 3:
Input: nestedList = [0]
Output: 0

Constraints:
1 <= nestedList.length <= 50
The values of the integers in the nested list are in the range [-100, 100].
The maximum depth of any integer is less than or equal to 50.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem339
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
            _list = new List<NestedInteger>();
        }

        public bool IsInteger() => _value.HasValue;
        public int GetInteger() => _value ?? 0;
        public void SetInteger(int value) => _value = value;
        public void Add(NestedInteger ni) => _list?.Add(ni);
        public IList<NestedInteger> GetList() => _list;
    }

    public class Solution
    {
        public void Init()
        {
            // [[1,1],2,[1,1]]
            NestedInteger l1 = new NestedIntegerImpl();
            l1.Add(new NestedIntegerImpl(1));
            l1.Add(new NestedIntegerImpl(1));

            NestedInteger n2 = new NestedIntegerImpl(2);

            NestedInteger l2 = new NestedIntegerImpl();
            l2.Add(new NestedIntegerImpl(1));
            l2.Add(new NestedIntegerImpl(1));

            List<NestedInteger> list = new() { l1, n2, l2 };
            Console.WriteLine(DepthSum(list)); // 10
        }

        // Time: O(N) where N is the total number of nested elements
        // Space: O(D) where D is the maximum nesting depth (recursion stack)
        public static int DepthSum(IList<NestedInteger> nestedList)
        {
            return Dfs(nestedList, 1);
        }

        private static int Dfs(IList<NestedInteger> list, int depth)
        {
            if (list == null)
            {
                return 0;
            }

            int sum = 0;

            foreach (var item in list)
            {
                if (item.IsInteger())
                {
                    sum += item.GetInteger() * depth;
                }
                else
                {
                    sum += Dfs(item.GetList(), depth + 1);
                }
            }

            return sum;
        }
    }
}