// Url:https://leetcode.com/problems/number-of-days-in-a-month

/*
1118. Number of Days in a Month
Easy

Given a year year and a month month, return the number of days of that month.

Example 1:
Input: year = 1992, month = 7
Output: 31

Example 2:
Input: year = 2000, month = 2
Output: 29

Example 3:
Input: year = 1900, month = 2
Output: 28

Constraints:
- 1583 <= year <= 2100
- 1 <= month <= 12
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1118 {
    public class Solution {
        public void Init() {
            Console.WriteLine(NumberOfDays(1992, 7)); // 31
            Console.WriteLine(NumberOfDays(2000, 2)); // 29
            Console.WriteLine(NumberOfDays(1900, 2)); // 28
        }

        // Time: O(1)
        // Space: O(1)
        public int NumberOfDays(int year, int month) {
            return DateTime.DaysInMonth(year, month);
        }
    }
}