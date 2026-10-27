// Url:https://leetcode.com/problems/digit-count-in-range

/*
1067. Digit Count in Range
Hard

Given an integer d between 0 and 9, and two positive integers low and high as lower and upper bounds, respectively, return the number of times that d occurs as a digit in all integers between low and high, inclusive.

Example 1:
Input: d = 1, low = 1, high = 13
Output: 6
Explanation: The function will count the digit 1 in 1, 10, 11, 12, 13. Note that the digit 1 occurred twice in 11.

Example 2:
Input: d = 3, low = 100, high = 250
Output: 35
Explanation: The numbers with digit 3 are 103, 113, 123, 130, 131, ..., 238, 239.

Constraints:
- 0 <= d <= 9
- 1 <= low <= high <= 2 * 10^8
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1067
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(DigitsCount(1, 1, 13)); // 6
            Console.WriteLine(DigitsCount(3, 100, 250)); // 35
        }

        // Time: O(log_10 high)
        // Space: O(1)
        public static int DigitsCount(int d, int low, int high)
        {
            return CountDigit(high, d) - CountDigit(low - 1, d);
        }

        private static int CountDigit(int n, int d)
        {
            if (n <= 0)
            {
                return 0;
            }

            int count = 0;
            long place = 1;

            while (place <= n)
            {
                long higher = n / (place * 10);
                long cur = (n / place) % 10;
                long lower = n % place;

                if (d > 0)
                {
                    if (cur < d)
                    {
                        count += (int)(higher * place);
                    }
                    else if (cur == d)
                    {
                        count += (int)(higher * place + lower + 1);
                    }
                    else
                    {
                        count += (int)((higher + 1) * place);
                    }
                }
                else
                {
                    // d == 0 (no leading zeros)
                    if (cur == 0)
                    {
                        count += (int)((higher - 1) * place + lower + 1);
                    }
                    else
                    {
                        count += (int)(higher * place);
                    }
                }

                place *= 10;
            }

            return count;
        }
    }
}