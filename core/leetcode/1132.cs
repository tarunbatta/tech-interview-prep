// Url:https://leetcode.com/problems/reported-posts-ii

/*
1132. Reported Posts II
Medium

Table: Actions

+---------------+---------+
| Column Name   | Type    |
+---------------+---------+
| user_id       | int     |
| post_id       | int     |
| action_date   | date    |
| action        | enum    |
| extra         | varchar |
+---------------+---------+
There is no primary key for this table, it may have duplicate rows.
The action column is an ENUM of type ('view', 'like', 'reaction', 'comment', 'report', 'share').
The extra column has optional information about the action, such as a reason for report or a type of reaction.

Table: Removals

+---------------+---------+
| Column Name   | Type    |
+---------------+---------+
| post_id       | int     |
| remove_date   | date    |
+---------------+---------+
post_id is the primary key of this table.
Each row in this table indicates that some post was removed as a result of being evaluated by a team.

Write an SQL query to find the average daily percentage of posts that got removed after being reported as spam, rounded to 2 decimal places.

The query result format is in the following example.

Example 1:

Input: 
Actions table:
+---------+---------+-------------+--------+--------+
| user_id | post_id | action_date | action | extra  |
+---------+---------+-------------+--------+--------+
| 1       | 1       | 2019-07-01  | view   | null   |
| 1       | 1       | 2019-07-01  | like   | null   |
| 1       | 1       | 2019-07-01  | share  | null   |
| 2       | 2       | 2019-07-04  | view   | null   |
| 2       | 2       | 2019-07-04  | report | spam   |
| 3       | 4       | 2019-07-04  | view   | null   |
| 3       | 4       | 2019-07-04  | report | spam   |
| 4       | 3       | 2019-07-02  | view   | null   |
| 4       | 3       | 2019-07-02  | report | spam   |
| 5       | 2       | 2019-07-03  | view   | null   |
| 5       | 2       | 2019-07-03  | report | test   |
| 5       | 4       | 2019-07-03  | view   | null   |
| 5       | 4       | 2019-07-03  | report | spam   |
+---------+---------+-------------+--------+--------+
Removals table:
+---------+-------------+
| post_id | remove_date |
+---------+-------------+
| 2       | 2019-07-20  |
| 3       | 2019-07-18  |
+---------+-------------+
Output: 
+-----------------------+
| average_daily_percent |
+-----------------------+
| 75.00                 |
+-----------------------+
Explanation: 
The percentage for 2019-07-04 is 50% because only one of the two reported spam posts (post 2 and 4) was removed.
The percentage for 2019-07-02 is 100% because the only reported post (post 3) was removed.
The percentage for 2019-07-03 is 0% because only post 4 was reported as spam and wasn't removed.
The average rate is (50 + 100 + 0) / 3 = 75.00%
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1132 {
    public class ActionRecord {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime ActionDate { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Extra { get; set; } = string.Empty;
    }

    public class RemovalRecord {
        public int PostId { get; set; }
        public DateTime RemoveDate { get; set; }
    }

    public class Solution {
        public static void Init() {
            List<ActionRecord> actions = [
                new() { UserId = 1, PostId = 1, ActionDate = new DateTime(2019, 7, 1), Action = "view", Extra = "" },
                new() { UserId = 2, PostId = 2, ActionDate = new DateTime(2019, 7, 4), Action = "report", Extra = "spam" },
                new() { UserId = 3, PostId = 4, ActionDate = new DateTime(2019, 7, 4), Action = "report", Extra = "spam" },
                new() { UserId = 4, PostId = 3, ActionDate = new DateTime(2019, 7, 2), Action = "report", Extra = "spam" },
                new() { UserId = 5, PostId = 2, ActionDate = new DateTime(2019, 7, 3), Action = "report", Extra = "test" },
                new() { UserId = 5, PostId = 4, ActionDate = new DateTime(2019, 7, 3), Action = "report", Extra = "spam" }
            ];

            List<RemovalRecord> removals = [
                new() { PostId = 2, RemoveDate = new DateTime(2019, 7, 20) },
                new() { PostId = 3, RemoveDate = new DateTime(2019, 7, 18) }
            ];

            Console.WriteLine($"{AverageDailyRemovalPercent(actions, removals):F2}"); // 75.00
        }

        // Time: O(A + R) where A is actions count and R is removals count
        // Space: O(A + R)
        public static double AverageDailyRemovalPercent(List<ActionRecord> actions, List<RemovalRecord> removals) {
            var removedPostIds = removals.Select(r => r.PostId).ToHashSet();

            var spamReports = actions
                .Where(a => a.Action == "report" && a.Extra == "spam")
                .GroupBy(a => a.ActionDate);

            List<double> dailyPercentages = [];

            foreach (var group in spamReports) {
                var uniquePosts = group.Select(a => a.PostId).Distinct().ToList();
                int totalPosts = uniquePosts.Count;
                int removedPosts = uniquePosts.Count(id => removedPostIds.Contains(id));

                dailyPercentages.Add((double)removedPosts * 100.0 / totalPosts);
            }

            if (dailyPercentages.Count == 0) {
                return 0.00;
            }

            return Math.Round(dailyPercentages.Average(), 2);
        }
    }
}