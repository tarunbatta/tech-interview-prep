// Url:https://leetcode.com/problems/biggest-single-number

/*
619. Biggest Single Number
Easy

Table: MyNumbers

+-------------+------+
| Column Name | Type |
+-------------+------+
| num         | int  |
+-------------+------+
This table may contain duplicates (In SQL, it does not have a primary key).
Each row of this table contains an integer.

A single number is a number that appeared only once in the MyNumbers table.

Find the largest single number. If there is no single number, report null.

The result format is in the following example.

Example 1:

Input: 
MyNumbers table:
+-----+
| num |
+-----+
| 8   |
| 8   |
| 3   |
| 3   |
| 1   |
| 4   |
| 5   |
| 6   |
+-----+
Output: 
+-----+
| num |
+-----+
| 6   |
+-----+
Explanation: The single numbers are 1, 4, 5, and 6.
Since 6 is the largest single number, we return it.

Example 2:

Input: 
MyNumbers table:
+-----+
| num |
+-----+
| 8   |
| 8   |
| 7   |
| 7   |
| 3   |
| 3   |
| 3   |
+-----+
Output: 
+------+
| num  |
+------+
| null |
+------+
Explanation: There are no single numbers in the input table so we return null.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem619 {
    public class Solution {
        public static void Init() {
            List<int> numbers1 = [8, 8, 3, 3, 1, 4, 5, 6];
            Console.WriteLine(BiggestSingleNumber(numbers1)?.ToString() ?? "null"); // 6

            List<int> numbers2 = [8, 8, 7, 7, 3, 3, 3];
            Console.WriteLine(BiggestSingleNumber(numbers2)?.ToString() ?? "null"); // null
        }

        // Time: O(n) where n is the number of integers
        // Space: O(n) to store frequencies of numbers
        public static int? BiggestSingleNumber(List<int> numbers) {
            var singleNumbers = numbers
                .GroupBy(n => n)
                .Where(g => g.Count() == 1)
                .Select(g => (int?)g.Key);

            return singleNumbers.Max();
        }
    }
}