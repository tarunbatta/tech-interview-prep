// Url: https://leetcode.com/problems/find-median-given-frequency-of-numbers

/*
571. Find Median Given Frequency of Numbers
Hard

Table: Numbers
+-------------+------+
| Column Name | Type |
+-------------+------+
| num         | int  |
| frequency   | int  |
+-------------+------+
num is the primary key for this table.
Each row of this table shows the frequency of a number in the database.

The median is the value separating the higher half from the lower half of a data sample.

Write a solution to report the median of all the numbers in the database after decompressing the Numbers table. Round the median to one decimal place.

SQL Solution:
WITH Cumulative AS (
    SELECT num, frequency,
           SUM(frequency) OVER(ORDER BY num) AS cum_freq,
           SUM(frequency) OVER() AS total_freq
    FROM Numbers
)
SELECT ROUND(AVG(num * 1.0), 1) AS median
FROM Cumulative
WHERE total_freq <= 2 * cum_freq
  AND total_freq >= 2 * (cum_freq - frequency);
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem571
{
    public class NumberFreq
    {
        public int Num { get; set; }
        public int Frequency { get; set; }
    }

    public class Solution
    {
        public static void Init()
        {
            List<NumberFreq> numbers =
            [
                new NumberFreq { Num = 0, Frequency = 7 },
                new NumberFreq { Num = 1, Frequency = 1 },
                new NumberFreq { Num = 2, Frequency = 3 },
                new NumberFreq { Num = 3, Frequency = 1 }
            ];

            Console.WriteLine(FindMedian(numbers)); // 0.0
        }

        // Time: O(N log N) where N is numbers.Count (sorting unique numbers)
        // Space: O(N) for cumulative frequency calculation
        public static double FindMedian(IList<NumberFreq> numbers)
        {
            if (numbers == null || numbers.Count == 0)
            {
                return 0.0;
            }

            var sorted = numbers.OrderBy(n => n.Num).ToList();
            long totalFreq = sorted.Sum(n => (long)n.Frequency);

            if (totalFreq == 0)
            {
                return 0.0;
            }

            long target1 = (totalFreq + 1) / 2;
            long target2 = (totalFreq + 2) / 2;

            long cumFreq = 0;
            int? val1 = null;
            int? val2 = null;

            foreach (var item in sorted)
            {
                long prevCum = cumFreq;
                cumFreq += item.Frequency;

                if (!val1.HasValue && cumFreq >= target1)
                {
                    val1 = item.Num;
                }

                if (!val2.HasValue && cumFreq >= target2)
                {
                    val2 = item.Num;
                }

                if (val1.HasValue && val2.HasValue)
                {
                    break;
                }
            }

            return Math.Round((val1.Value + val2.Value) / 2.0, 1);
        }
    }
}