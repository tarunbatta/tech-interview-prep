// Url: https://leetcode.com/problems/strobogrammatic-number

/*
246. Strobogrammatic Number
Easy

Given a string num which represents an integer, return true if num is a strobogrammatic number.

A strobogrammatic number is a number that looks the same when rotated 180 degrees (looked at upside down).

Example 1:
Input: num = "69"
Output: true

Example 2:
Input: num = "88"
Output: true

Example 3:
Input: num = "962"
Output: false

Constraints:
1 <= num.length <= 50
num consists of only digits.
num does not contain any leading zeros except for zero itself.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem246
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(IsStrobogrammatic("69"));  // True
            Console.WriteLine(IsStrobogrammatic("88"));  // True
            Console.WriteLine(IsStrobogrammatic("962")); // False
        }

        // Time: O(n) where n is num.Length
        // Space: O(1) auxiliary space
        public static bool IsStrobogrammatic(string num)
        {
            if (string.IsNullOrEmpty(num))
            {
                return false;
            }

            int left = 0;
            int right = num.Length - 1;

            while (left <= right)
            {
                char c1 = num[left];
                char c2 = num[right];

                if (!IsMatchingPair(c1, c2))
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }

        private static bool IsMatchingPair(char c1, char c2)
        {
            return (c1 == '0' && c2 == '0') ||
                   (c1 == '1' && c2 == '1') ||
                   (c1 == '8' && c2 == '8') ||
                   (c1 == '6' && c2 == '9') ||
                   (c1 == '9' && c2 == '6');
        }
    }
}