// Url:https://leetcode.com/problems/user-activity-for-the-past-30-days-i

/*
1141. User Activity for the Past 30 Days I
Easy

Table: Activity

+---------------+---------+
| Column Name   | Type    |
+---------------+---------+
| user_id       | int     |
| session_id    | int     |
| activity_date | date    |
| activity_type | enum    |
+---------------+---------+
This table may have duplicate rows.
The activity_type column is an ENUM (category) of type ('open_session', 'end_session', 'scroll_down', 'send_message').
The table shows the user activities for a social media website. 
Note that each session belongs to exactly one user.

Write a solution to find the daily active user count for a period of 30 days ending 2019-07-27 inclusively. A user was active on someday if they made at least one activity on that day.

Return the result table in any order.

The result format is in the following example.

Example 1:

Input: 
Activity table:
+---------+------------+---------------+---------------+
| user_id | session_id | activity_date | activity_type |
+---------+------------+---------------+---------------+
| 1       | 1          | 2019-07-20    | open_session  |
| 1       | 1          | 2019-07-20    | scroll_down   |
| 1       | 1          | 2019-07-20    | end_session   |
| 2       | 4          | 2019-07-20    | open_session  |
| 2       | 4          | 2019-07-21    | send_message  |
| 2       | 4          | 2019-07-21    | end_session   |
| 3       | 2          | 2019-07-21    | open_session  |
| 3       | 2          | 2019-07-21    | send_message  |
| 3       | 2          | 2019-07-21    | end_session   |
| 4       | 3          | 2019-06-25    | open_session  |
| 4       | 3          | 2019-06-25    | end_session   |
+---------+------------+---------------+---------------+
Output: 
+------------+--------------+ 
| day        | active_users |
+------------+--------------+ 
| 2019-07-20 | 2            |
| 2019-07-21 | 2            |
+------------+--------------+ 
Explanation: Note that we do not care about days with zero active users.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1141 {
    public class ActivityRecord {
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityType { get; set; } = string.Empty;
    }

    public class DailyActiveUsersResult {
        public string Day { get; set; } = string.Empty;
        public int ActiveUsers { get; set; }
    }

    public class Solution {
        public void Init() {
            List<ActivityRecord> activities = [
                new() { UserId = 1, SessionId = 1, ActivityDate = new DateTime(2019, 7, 20), ActivityType = "open_session" },
                new() { UserId = 1, SessionId = 1, ActivityDate = new DateTime(2019, 7, 20), ActivityType = "scroll_down" },
                new() { UserId = 1, SessionId = 1, ActivityDate = new DateTime(2019, 7, 20), ActivityType = "end_session" },
                new() { UserId = 2, SessionId = 4, ActivityDate = new DateTime(2019, 7, 20), ActivityType = "open_session" },
                new() { UserId = 2, SessionId = 4, ActivityDate = new DateTime(2019, 7, 21), ActivityType = "send_message" },
                new() { UserId = 2, SessionId = 4, ActivityDate = new DateTime(2019, 7, 21), ActivityType = "end_session" },
                new() { UserId = 3, SessionId = 2, ActivityDate = new DateTime(2019, 7, 21), ActivityType = "open_session" },
                new() { UserId = 3, SessionId = 2, ActivityDate = new DateTime(2019, 7, 21), ActivityType = "send_message" },
                new() { UserId = 3, SessionId = 2, ActivityDate = new DateTime(2019, 7, 21), ActivityType = "end_session" },
                new() { UserId = 4, SessionId = 3, ActivityDate = new DateTime(2019, 6, 25), ActivityType = "open_session" },
                new() { UserId = 4, SessionId = 3, ActivityDate = new DateTime(2019, 6, 25), ActivityType = "end_session" }
            ];

            var result = DailyActiveUsers(activities);
            foreach (var r in result) {
                Console.WriteLine($"{r.Day} | {r.ActiveUsers}");
            }
        }

        // Time: O(n log n) where n is the number of activity records
        // Space: O(n) for grouping and distinct user counts
        public List<DailyActiveUsersResult> DailyActiveUsers(List<ActivityRecord> activities) {
            DateTime endDate = new(2019, 7, 27);
            DateTime startDate = endDate.AddDays(-29); // 30 days inclusive (2019-06-28)

            return activities
                .Where(a => a.ActivityDate >= startDate && a.ActivityDate <= endDate)
                .GroupBy(a => a.ActivityDate)
                .Select(g => new DailyActiveUsersResult {
                    Day = g.Key.ToString("yyyy-MM-dd"),
                    ActiveUsers = g.Select(a => a.UserId).Distinct().Count()
                })
                .OrderBy(r => r.Day)
                .ToList();
        }
    }
}