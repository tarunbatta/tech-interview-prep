// Url:https://leetcode.com/problems/new-users-daily-count

/*
1107. New Users Daily Count
Medium

Table: Traffic

+---------------+---------+
| Column Name   | Type    |
+---------------+---------+
| user_id       | int     |
| activity      | enum    |
| activity_date | date    |
+---------------+---------+
There is no primary key for this table, it may have duplicate rows.
The activity column is an ENUM of type ('login', 'logout', 'jobs', 'groups', 'homepage').

Write an SQL query that reports for every date within at most 90 days from today, the number of users that logged in for the first time on that date. Assume today is 2019-06-30.

Return the result table in any order.

The query result format is in the following example.

Example 1:

Input: 
Traffic table:
+---------+----------+---------------+
| user_id | activity | activity_date |
+---------+----------+---------------+
| 1       | login    | 2019-05-01    |
| 1       | homepage | 2019-05-01    |
| 1       | logout   | 2019-05-01    |
| 2       | login    | 2019-06-21    |
| 2       | logout   | 2019-06-21    |
| 3       | login    | 2019-01-01    |
| 3       | jobs     | 2019-01-01    |
| 3       | logout   | 2019-01-01    |
| 4       | login    | 2019-06-21    |
| 4       | groups   | 2019-06-21    |
| 4       | logout   | 2019-06-21    |
| 5       | login    | 2019-03-01    |
| 5       | logout   | 2019-03-01    |
| 5       | login    | 2019-06-21    |
| 5       | logout   | 2019-06-21    |
+---------+----------+---------------+
Output: 
+------------+-------------+
| login_date | user_count  |
+------------+-------------+
| 2019-05-01 | 1           |
| 2019-06-21 | 2           |
+------------+-------------+
Explanation: 
Note that we only care about dates with user_count > 0.
The user with id 5 first logged in on 2019-03-01, so he's not counted on 2019-06-21.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1107 {
    public class TrafficRecord {
        public int UserId { get; set; }
        public string Activity { get; set; } = string.Empty;
        public DateTime ActivityDate { get; set; }
    }

    public class NewUserResult {
        public string LoginDate { get; set; } = string.Empty;
        public int UserCount { get; set; }
    }

    public class Solution {
        public void Init() {
            List<TrafficRecord> traffic = [
                new() { UserId = 1, Activity = "login", ActivityDate = new DateTime(2019, 5, 1) },
                new() { UserId = 1, Activity = "homepage", ActivityDate = new DateTime(2019, 5, 1) },
                new() { UserId = 1, Activity = "logout", ActivityDate = new DateTime(2019, 5, 1) },
                new() { UserId = 2, Activity = "login", ActivityDate = new DateTime(2019, 6, 21) },
                new() { UserId = 2, Activity = "logout", ActivityDate = new DateTime(2019, 6, 21) },
                new() { UserId = 3, Activity = "login", ActivityDate = new DateTime(2019, 1, 1) },
                new() { UserId = 3, Activity = "jobs", ActivityDate = new DateTime(2019, 1, 1) },
                new() { UserId = 3, Activity = "logout", ActivityDate = new DateTime(2019, 1, 1) },
                new() { UserId = 4, Activity = "login", ActivityDate = new DateTime(2019, 6, 21) },
                new() { UserId = 4, Activity = "groups", ActivityDate = new DateTime(2019, 6, 21) },
                new() { UserId = 4, Activity = "logout", ActivityDate = new DateTime(2019, 6, 21) },
                new() { UserId = 5, Activity = "login", ActivityDate = new DateTime(2019, 3, 1) },
                new() { UserId = 5, Activity = "logout", ActivityDate = new DateTime(2019, 3, 1) },
                new() { UserId = 5, Activity = "login", ActivityDate = new DateTime(2019, 6, 21) },
                new() { UserId = 5, Activity = "logout", ActivityDate = new DateTime(2019, 6, 21) }
            ];

            var result = NewUsersDailyCount(traffic);
            foreach (var r in result) {
                Console.WriteLine($"{r.LoginDate} | {r.UserCount}");
            }
        }

        // Time: O(n log n) where n is the number of traffic records
        // Space: O(n) to store first login dates and grouping
        public List<NewUserResult> NewUsersDailyCount(List<TrafficRecord> traffic) {
            DateTime today = new(2019, 6, 30);
            DateTime ninetyDaysAgo = today.AddDays(-90);

            var firstLogins = traffic
                .Where(t => t.Activity == "login")
                .GroupBy(t => t.UserId)
                .Select(g => g.Min(t => t.ActivityDate))
                .Where(d => d >= ninetyDaysAgo && d <= today);

            return firstLogins
                .GroupBy(d => d)
                .Select(g => new NewUserResult {
                    LoginDate = g.Key.ToString("yyyy-MM-dd"),
                    UserCount = g.Count()
                })
                .OrderBy(r => r.LoginDate)
                .ToList();
        }
    }
}