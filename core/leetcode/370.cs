// Url: https://leetcode.com/problems/range-addition

/*
370. Range Addition
Medium

You are given an integer length and an array updates where updates[i] = [startIdx, endIdx, inc].

You have an array arr of length length with all zeros, and you have some operations to apply on arr. In the ith operation, you should increment all the elements arr[startIdx], arr[startIdx + 1], ..., arr[endIdx] by inc.

Return arr after applying all the updates.

Example 1:
Input: length = 5, updates = [[1,3,2],[2,4,3],[0,2,-2]]
Output: [-2,0,3,5,3]

Example 2:
Input: length = 10, updates = [[2,4,6],[5,6,8],[1,9,-4]]
Output: [0,-4,2,2,2,4,4,-4,-4,-4]

Constraints:
1 <= length <= 10^5
0 <= updates.length <= 10^4
0 <= startIdx <= endIdx < length
-1000 <= inc <= 1000
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem370
{
    public class Solution
    {
        public static void Init()
        {
            int[][] updates =
            [
                [1, 3, 2],
                [2, 4, 3],
                [0, 2, -2]
            ];

            Console.WriteLine(string.Join(", ", GetModifiedArray(5, updates))); // -2, 0, 3, 5, 3
        }

        // Time: O(length + k) where k is updates.Length using difference array
        // Space: O(1) auxiliary space (excluding output array)
        public static int[] GetModifiedArray(int length, int[][] updates)
        {
            if (length <= 0)
            {
                return [];
            }

            int[] result = new int[length];

            if (updates != null)
            {
                foreach (var update in updates)
                {
                    int start = update[0];
                    int end = update[1];
                    int inc = update[2];

                    result[start] += inc;

                    if (end + 1 < length)
                    {
                        result[end + 1] -= inc;
                    }
                }
            }

            for (int i = 1; i < length; i++)
            {
                result[i] += result[i - 1];
            }

            return result;
        }
    }
}