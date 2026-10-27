// Url:https://leetcode.com/problems/investments-in-2016

/*
585. Investments in 2016
Medium

Table: Insurance

+-------------+-------+
| Column Name | Type  |
+-------------+-------+
| pid         | int   |
| tiv_2015    | float |
| tiv_2016    | float |
| lat         | float |
| lon         | float |
+-------------+-------+
pid is the primary key column for this table.
Each row of this table contains information about one policy where:
pid is the policyholder's policy ID.
tiv_2015 is the total investment value in 2015 and tiv_2016 is the total investment value in 2016.
lat is the latitude of the policy holder's city. It's guaranteed that lat is not NULL.
lon is the longitude of the policy holder's city. It's guaranteed that lon is not NULL.

Write a solution to report the sum of all total investment values in 2016 tiv_2016, for all policyholders who:
- have the same tiv_2015 value as one or more other policyholders, and
- are not located in the same city as any other policyholder (i.e., the (lat, lon) attribute pairs must be unique).

Round tiv_2016 to two decimal places.

The result format is in the following example.

Example 1:

Input: 
Insurance table:
+-----+----------+----------+-----+-----+
| pid | tiv_2015 | tiv_2016 | lat | lon |
+-----+----------+----------+-----+-----+
| 1   | 10       | 5        | 10  | 10  |
| 2   | 20       | 20       | 20  | 20  |
| 3   | 10       | 30       | 20  | 20  |
| 4   | 10       | 40       | 40  | 40  |
+-----+----------+----------+-----+-----+
Output: 
+----------+
| tiv_2016 |
+----------+
| 45.00    |
+----------+
Explanation: 
The first record in the table, like the last record, meets both of the two criteria.
The tiv_2015 value 10 is the same as the third and fourth records, and its location is unique.

The second record does not meet any of the two criteria. Its tiv_2015 is not like any other policyholders and its location is also the same as the third record, which makes the third record fail, too.
So, the result is the sum of tiv_2016 of the first and last record, which is 5 + 40 = 45.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem585 {
    public class Insurance {
        public int Pid { get; set; }
        public double Tiv2015 { get; set; }
        public double Tiv2016 { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class Solution {
        public static void Init() {
            List<Insurance> table = [
                new() { Pid = 1, Tiv2015 = 10, Tiv2016 = 5, Lat = 10, Lon = 10 },
                new() { Pid = 2, Tiv2015 = 20, Tiv2016 = 20, Lat = 20, Lon = 20 },
                new() { Pid = 3, Tiv2015 = 10, Tiv2016 = 30, Lat = 20, Lon = 20 },
                new() { Pid = 4, Tiv2015 = 10, Tiv2016 = 40, Lat = 40, Lon = 40 }
            ];

            Console.WriteLine(CalculateInvestments(table)); // 45.00
        }

        // Time: O(n) where n is the number of insurance policies
        // Space: O(n) to store frequencies of Tiv2015 and coordinates
        public static double CalculateInvestments(List<Insurance> policies) {
            var tiv2015Counts = policies
                .GroupBy(p => p.Tiv2015)
                .ToDictionary(g => g.Key, g => g.Count());

            var locationCounts = policies
                .GroupBy(p => (p.Lat, p.Lon))
                .ToDictionary(g => g.Key, g => g.Count());

            double sum = policies
                .Where(p => tiv2015Counts[p.Tiv2015] > 1 && locationCounts[(p.Lat, p.Lon)] == 1)
                .Sum(p => p.Tiv2016);

            return Math.Round(sum, 2);
        }
    }
}