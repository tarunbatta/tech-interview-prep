// Url:https://leetcode.com/problems/user-purchase-platform

/*
1127. User Purchase Platform
Hard

Table: Spending

+-------------+---------+
| Column Name | Type    |
+-------------+---------+
| user_id     | int     |
| spend_date  | date    |
| platform    | enum    |
| amount      | int     |
+-------------+---------+
The table logs the spendings history of users that make purchases from an online shopping website which has a desktop and a mobile application.
(user_id, spend_date, platform) is the primary key (combination of columns with unique values) of this table.
The platform column is an ENUM (category) of type ('desktop', 'mobile').

Write a solution to find the total number of users and the total amount spent using mobile only, desktop only, and both mobile and desktop together for each date.

Return the result table in any order.

The result format is in the following example.

Example 1:

Input: 
Spending table:
+---------+------------+----------+--------+
| user_id | spend_date | platform | amount |
+---------+------------+----------+--------+
| 1       | 2019-07-01 | mobile   | 100    |
| 1       | 2019-07-01 | desktop  | 100    |
| 2       | 2019-07-01 | mobile   | 100    |
| 2       | 2019-07-02 | mobile   | 100    |
| 3       | 2019-07-01 | desktop  | 100    |
| 3       | 2019-07-02 | desktop  | 100    |
+---------+------------+----------+--------+
Output: 
+------------+----------+--------------+-------------+
| spend_date | platform | total_amount | total_users |
+------------+----------+--------------+-------------+
| 2019-07-01 | desktop  | 100          | 1           |
| 2019-07-01 | mobile   | 100          | 1           |
| 2019-07-01 | both     | 200          | 1           |
| 2019-07-02 | desktop  | 100          | 1           |
| 2019-07-02 | mobile   | 100          | 1           |
| 2019-07-02 | both     | 0            | 0           |
+------------+----------+--------------+-------------+
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1127 {
    public class SpendingRecord {
        public int UserId { get; set; }
        public DateTime SpendDate { get; set; }
        public string Platform { get; set; } = string.Empty;
        public int Amount { get; set; }
    }

    public class PlatformSpendingResult {
        public string SpendDate { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public int TotalAmount { get; set; }
        public int TotalUsers { get; set; }
    }

    public class Solution {
        public static void Init() {
            List<SpendingRecord> spending = [
                new() { UserId = 1, SpendDate = new DateTime(2019, 7, 1), Platform = "mobile", Amount = 100 },
                new() { UserId = 1, SpendDate = new DateTime(2019, 7, 1), Platform = "desktop", Amount = 100 },
                new() { UserId = 2, SpendDate = new DateTime(2019, 7, 1), Platform = "mobile", Amount = 100 },
                new() { UserId = 2, SpendDate = new DateTime(2019, 7, 2), Platform = "mobile", Amount = 100 },
                new() { UserId = 3, SpendDate = new DateTime(2019, 7, 1), Platform = "desktop", Amount = 100 },
                new() { UserId = 3, SpendDate = new DateTime(2019, 7, 2), Platform = "desktop", Amount = 100 }
            ];

            var result = UserPurchasePlatform(spending);
            foreach (var r in result) {
                Console.WriteLine($"{r.SpendDate} | {r.Platform} | {r.TotalAmount} | {r.TotalUsers}");
            }
        }

        // Time: O(n log n) where n is the number of spending records
        // Space: O(n)
        public static List<PlatformSpendingResult> UserPurchasePlatform(List<SpendingRecord> records) {
            var distinctDates = records.Select(r => r.SpendDate).Distinct().OrderBy(d => d).ToList();
            string[] platforms = ["desktop", "mobile", "both"];

            // For each (date, user), determine category: "both", "desktop", "mobile"
            var userDaySummary = records
                .GroupBy(r => (r.SpendDate, r.UserId))
                .Select(g => {
                    var userPlatforms = g.Select(x => x.Platform).Distinct().ToHashSet();
                    string cat = userPlatforms.Count > 1 ? "both" : userPlatforms.First();
                    return new {
                        g.Key.SpendDate,
                        Category = cat,
                        Amount = g.Sum(x => x.Amount)
                    };
                })
                .ToList();

            var result = new List<PlatformSpendingResult>();

            foreach (var date in distinctDates) {
                foreach (var plat in platforms) {
                    var matching = userDaySummary.Where(u => u.SpendDate == date && u.Category == plat).ToList();
                    result.Add(new PlatformSpendingResult {
                        SpendDate = date.ToString("yyyy-MM-dd"),
                        Platform = plat,
                        TotalAmount = matching.Sum(m => m.Amount),
                        TotalUsers = matching.Count
                    });
                }
            }

            return result;
        }
    }
}