// Url: https://leetcode.com/problems/two-sum-iii-data-structure-design

/*
170. Two Sum III - Data structure design
Easy

Design a data structure that accepts a stream of integers and checks if it has a pair of integers that sum up to a specified value.

Implement the TwoSum class:

- TwoSum() Initializes the TwoSum object, with an empty array initially.
- void add(int number) Adds number to the data structure.
- boolean find(int value) Returns true if there exists any pair of numbers whose sum is equal to value, otherwise, it returns false.

Example 1:
Input
["TwoSum", "add", "add", "add", "find", "find"]
[[], [1], [3], [5], [4], [7]]
Output
[null, null, null, null, true, false]

Explanation
TwoSum twoSum = new TwoSum();
twoSum.add(1);   // [] --> [1]
twoSum.add(3);   // [1] --> [1,3]
twoSum.add(5);   // [1,3] --> [1,3,5]
twoSum.find(4);  // 1 + 3 = 4, return true
twoSum.find(7);  // No two integers sum up to 7, return false

Constraints:
-10^5 <= number <= 10^5
-2^31 <= value <= 2^31 - 1
At most 10^4 calls will be made to add and find.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem170
{
    public class Solution
    {
        public static void Init()
        {
            TwoSum twoSum = new();
            twoSum.Add(1);
            twoSum.Add(3);
            twoSum.Add(5);

            Console.WriteLine(twoSum.Find(4)); // True
            Console.WriteLine(twoSum.Find(7)); // False
        }
    }

    // Time:
    //   Add: O(1)
    //   Find: O(N) where N is the number of distinct elements added
    // Space: O(N) where N is the number of distinct elements stored
    public class TwoSum
    {
        private readonly Dictionary<int, int> _counts = [];

        public void Add(int number)
        {
            _counts[number] = _counts.GetValueOrDefault(number, 0) + 1;
        }

        public bool Find(int value)
        {
            foreach (var kvp in _counts)
            {
                long complement = (long)value - kvp.Key;

                if (complement < int.MinValue || complement > int.MaxValue)
                {
                    continue;
                }

                int compInt = (int)complement;

                if (compInt == kvp.Key)
                {
                    if (kvp.Value >= 2)
                    {
                        return true;
                    }
                }
                else if (_counts.ContainsKey(compInt))
                {
                    return true;
                }
            }

            return false;
        }
    }
}