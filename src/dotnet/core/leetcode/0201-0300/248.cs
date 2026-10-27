// Url: https://leetcode.com/problems/strobogrammatic-number-iii

/*
248. Strobogrammatic Number III
Hard

Given two strings low and high that represent two integers, return the number of strobogrammatic numbers in the range [low, high].

A strobogrammatic number is a number that looks the same when rotated 180 degrees (looked at upside down).

Example 1:
Input: low = "50", high = "100"
Output: 3
Explanation: The 3 strobogrammatic numbers in this range are "69", "88", and "96".

Example 2:
Input: low = "0", high = "0"
Output: 1

Constraints:
1 <= low.length <= high.length <= 15
low and high consist of only digits.
low <= high
low and high do not contain any leading zeros except for zero itself.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem248
{
    public class Solution
    {
        private static readonly char[][] Pairs =
        [
            ['0', '0'],
            ['1', '1'],
            ['6', '9'],
            ['8', '8'],
            ['9', '6']
        ];

        public static void Init()
        {
            Console.WriteLine(StrobogrammaticInRange("50", "100")); // 3
            Console.WriteLine(StrobogrammaticInRange("0", "0"));     // 1
        }

        // Time: O(5^(high.Length / 2))
        // Space: O(high.Length) for recursion depth and buffer
        public static int StrobogrammaticInRange(string low, string high)
        {
            int count = 0;

            for (int len = low.Length; len <= high.Length; len++)
            {
                char[] buffer = new char[len];
                Dfs(low, high, buffer, 0, len - 1, ref count);
            }

            return count;
        }

        private static void Dfs(string low, string high, char[] buffer, int left, int right, ref int count)
        {
            if (left > right)
            {
                string s = new(buffer);

                if ((s.Length == low.Length && Compare(s, low) < 0) ||
                    (s.Length == high.Length && Compare(s, high) > 0))
                {
                    return;
                }

                count++;
                return;
            }

            foreach (var pair in Pairs)
            {
                buffer[left] = pair[0];
                buffer[right] = pair[1];

                // Leading zero is not allowed for numbers with length > 1
                if (buffer.Length > 1 && buffer[0] == '0')
                {
                    continue;
                }

                // Middle digit for odd length cannot be 6 or 9
                if (left == right && pair[0] != pair[1])
                {
                    continue;
                }

                Dfs(low, high, buffer, left + 1, right - 1, ref count);
            }
        }

        private static int Compare(string a, string b)
        {
            if (a.Length != b.Length)
            {
                return a.Length.CompareTo(b.Length);
            }

            return string.CompareOrdinal(a, b);
        }
    }
}