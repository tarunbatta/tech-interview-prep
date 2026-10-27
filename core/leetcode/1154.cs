// Url: https://leetcode.com/problems/day-of-the-year/

/*
1154. Day of the Year
Easy

Given a string date representing a Gregorian calendar date formatted as YYYY-MM-DD, return the day number of the year.

 

Example 1:

Input: date = "2019-01-09"
Output: 9
Explanation: Given date is the 9th day of the year in 2019.
Example 2:

Input: date = "2019-02-10"
Output: 41
Example 3:

Input: date = "2003-03-01"
Output: 60
Example 4:

Input: date = "2004-03-01"
Output: 61
 

Constraints:

date.length == 10
date[4] == date[7] == '-', and all other date[i]'s are digits
date represents a calendar date between Jan 1st, 1900 and Dec 31, 2019.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1154 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (DayOfYear(null));
            Console.WriteLine (DayOfYear("2019-01-09"));
            Console.WriteLine (DayOfYear("2003-03-01"));
            Console.WriteLine (DayOfYear("2004-03-01"));
        }

        public static int DayOfYear (string date) {
            int result = 0;

            if (string.IsNullOrEmpty (date)) {
                return result;

            }

            string[] strs = date.Split ("-");
            int year = Convert.ToInt32 (strs[0]);
            int month = Convert.ToInt32 (strs[1]);
            int day = Convert.ToInt32 (strs[2]);
            bool isLeapYear = IsLeapYear(year);
            int[] daysInMonth = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

            result += day;

            for (int i = 0; i < month - 1; i++) {
                if (isLeapYear && i == 1) {
                    result += daysInMonth[i] + 1;
                } else {
                    result += daysInMonth[i];
                }
            }

            return result;
        }

        public static bool IsLeapYear (int year) {
            bool result = false;

            if (year % 400 == 0)
                return true;
            if (year % 100 == 0)
                return false;
            if (year % 4 == 0)
                return true;

            return result;
        }
    }
}