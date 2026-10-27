// Url:https://leetcode.com/problems/remove-9

/*
660. Remove 9
Hard

Start from integer 1, remove any integer that contains 9 such as 9, 19, 29...

Now, you will have a new integer sequence: 1, 2, 3, 4, 5, 6, 7, 8, 10, 11, ...

Given a positive integer n, return the n-th integer after removing 9.

Example 1:
Input: n = 9
Output: 10

Example 2:
Input: n = 8
Output: 8

Constraints:
- 1 <= n <= 8 * 10^8
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem660 {
    public class Solution {
        public void Init() {
            Console.WriteLine(NewInteger(9)); // 10
            Console.WriteLine(NewInteger(8)); // 8
            Console.WriteLine(NewInteger(100)); // 121
        }

        // Time: O(log_9 n)
        // Space: O(1)
        public int NewInteger(int n) {
            long result = 0;
            long multiplier = 1;

            while (n > 0) {
                result += (n % 9) * multiplier;
                n /= 9;
                multiplier *= 10;
            }

            return (int)result;
        }
    }
}