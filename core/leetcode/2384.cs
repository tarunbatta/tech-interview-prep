// Url: https://leetcode.com/problems/largest-palindromic-number/

/*
2384. Largest Palindromic Number
Medium
Topics
premium lock icon
Companies
Hint
You are given a string num consisting of digits only.

Return the largest palindromic integer (in the form of a string) that can be formed using digits taken from num. It should not contain leading zeroes.

Notes:

You do not need to use all the digits of num, but you must use at least one digit.
The digits can be reordered.
 

Example 1:

Input: num = "444947137"
Output: "7449447"
Explanation: 
Use the digits "4449477" from "444947137" to form the palindromic integer "7449447".
It can be shown that "7449447" is the largest palindromic integer that can be formed.
Example 2:

Input: num = "00009"
Output: "9"
Explanation: 
It can be shown that "9" is the largest palindromic integer that can be formed.
Note that the integer returned should not contain leading zeroes.
 

Constraints:

1 <= num.length <= 105
num consists of digits.
*/

using System;
using System.IO.Pipelines;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem2384
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(LargestPalindromic(null));
            Console.WriteLine(LargestPalindromic(string.Empty));
            Console.WriteLine(LargestPalindromic("444947137"));
            Console.WriteLine(LargestPalindromic("00009"));
        }

        // Time: O(n)
        // Space: O(n)
        public static string LargestPalindromic(string num)
        {
            if (string.IsNullOrEmpty(num))
            {
                return string.Empty;
            }

            int[] count = new int[10];
            foreach (char c in num)
            {
                count[c - '0']++;
            }

            StringBuilder left = new();

            // Build pairs from 9 down to 0
            for (int i = 9; i >= 0; i--)
            {
                if (i == 0 && left.Length == 0)
                {
                    continue;
                }

                int pairs = count[i] / 2;

                if (pairs > 0)
                {
                    left.Append((char)('0' + i), pairs);
                    count[i] %= 2;
                }
            }

            // Find the largest single digit for the center
            string middle = string.Empty;
            for (int i = 9; i >= 0; i--)
            {
                if (count[i] == 1)
                {
                    middle = i.ToString();
                    break;
                }
            }

            if (left.Length == 0 && string.IsNullOrEmpty(middle))
            {
                return "0";
            }

            char[] leftArr = left.ToString().ToCharArray();
            Array.Reverse(leftArr);
            return new string(left.ToString() + middle + new string(leftArr));
        }
    }
}