// Url: https://leetcode.com/problems/strobogrammatic-number-ii

/*
247. Strobogrammatic Number II
Medium

Given an integer n, return all the strobogrammatic numbers that are of length n. You may return the answer in any order.

A strobogrammatic number is a number that looks the same when rotated 180 degrees (looked at upside down).

Example 1:
Input: n = 2
Output: ["11","69","88","96"]

Example 2:
Input: n = 1
Output: ["0","1","8"]

Constraints:
1 <= n <= 14
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem247
{
    public class Solution
    {
        public static void Init()
        {
            var res1 = FindStrobogrammatic(2);
            Console.WriteLine(string.Join(", ", res1)); // 11, 69, 88, 96

            var res2 = FindStrobogrammatic(1);
            Console.WriteLine(string.Join(", ", res2)); // 0, 1, 8
        }

        // Time: O(n * 5^(n/2))
        // Space: O(n * 5^(n/2))
        public static IList<string> FindStrobogrammatic(int n)
        {
            return Helper(n, n);
        }

        private static List<string> Helper(int n, int totalLength)
        {
            if (n == 0)
            {
                return [""];
            }

            if (n == 1)
            {
                return ["0", "1", "8"];
            }

            List<string> subList = Helper(n - 2, totalLength);
            List<string> result = [];

            foreach (string s in subList)
            {
                if (n != totalLength)
                {
                    result.Add("0" + s + "0");
                }

                result.Add("1" + s + "1");
                result.Add("6" + s + "9");
                result.Add("8" + s + "8");
                result.Add("9" + s + "6");
            }

            return result;
        }
    }
}