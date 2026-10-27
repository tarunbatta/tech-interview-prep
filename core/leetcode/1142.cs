// Url:https://leetcode.com/problems/user-activity-for-the-past-30-days-ii

/*
1142. User Activity for the Past 30 Days II
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
There is no primary key for this table, it may have duplicate rows.
The activity_type column is an ENUM (category) of type ('open_session', 'end_session', 'scroll_down', 'send_message').
The table shows the user activities for a social media website. 
Note that each session belongs to exactly one user.

Write a solution to find the average number of sessions per user for a period of 30 days ending 2019-07-27 inclusively, rounded to 2 decimal places. The sessions we want to count for a user are those only within that 30 days period.

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
| 3       | 5          | 2019-07-21    | open_session  |
| 3       | 5          | 2019-07-21    | scroll_down   |
| 3       | 5          | 2019-07-21    | end_session   |
| 4       | 3          | 2019-06-25    | open_session  |
| 4       | 3          | 2019-06-25    | end_session   |
+---------+------------+---------------+---------------+
Output: 
+---------------------------+ 
| average_sessions_per_user |
+---------------------------+ 
| 1.33                      |
+---------------------------+ 
Explanation: User 1 and 2 each had 1 session in the past 30 days, while user 3 had 2 sessions so the average is (1 + 1 + 2) / 3 = 1.33.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1142 {
    public class ActivityRecord {
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityType { get; set; } = string.Empty;
    }

    public class Solution {
        public static void Init() {
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
                new() { UserId = 3, SessionId = 5, ActivityDate = new DateTime(2019, 7, 21), ActivityType = "open_session" },
                new() { UserId = 3, SessionId = 5, ActivityDate = new DateTime(2019, 7, 21), ActivityType = "scroll_down" },
                new() { UserId = 3, SessionId = 5, ActivityDate = new DateTime(2019, 7, 21), ActivityType = "end_session" },
                new() { UserId = 4, SessionId = 3, ActivityDate = new DateTime(2019, 6, 25), ActivityType = "open_session" },
                new() { UserId = 4, SessionId = 3, ActivityDate = new DateTime(2019, 6, 25), ActivityType = "end_session" }
            ];

            Console.WriteLine($"{AverageSessionsPerUser(activities):F2}"); // 1.33
        }

        // Time: O(n) where n is the number of activity records
        // Space: O(u) where u is distinct users
        public static double AverageSessionsPerUser(List<ActivityRecord> activities) {
            DateTime endDate = new(2019, 7, 27);
            DateTime startDate = endDate.AddDays(-29);

            var validActivities = activities
                .Where(a => a.ActivityDate >= startDate && a.ActivityDate <= endDate)
                .ToList();

            int totalDistinctUsers = validActivities.Select(a => a.UserId).Distinct().Count();
            if (totalDistinctUsers == 0) {
                return 0.00;
            }

            int totalDistinctSessions = validActivities.Select(a => a.SessionId).Distinct().Count();
            return Math.Round((double)totalDistinctSessions / totalDistinctUsers, 2);
        }
    }
}