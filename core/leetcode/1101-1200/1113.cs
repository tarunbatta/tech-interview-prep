// Url:https://leetcode.com/problems/reported-posts

/*
1113. Reported Posts
Easy

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
extra is never NULL.

Write an SQL query that reports the number of posts reported yesterday for each report reason. Assume today is 2019-07-05.

Return the result table in any order.

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
| 2       | 4       | 2019-07-04  | view   | null   |
| 2       | 4       | 2019-07-04  | report | spam   |
| 3       | 4       | 2019-07-04  | view   | null   |
| 3       | 4       | 2019-07-04  | report | spam   |
| 4       | 3       | 2019-07-02  | view   | null   |
| 4       | 3       | 2019-07-02  | report | spam   |
| 5       | 2       | 2019-07-04  | view   | null   |
| 5       | 2       | 2019-07-04  | report | racism |
| 5       | 5       | 2019-07-04  | view   | null   |
| 5       | 5       | 2019-07-04  | report | offensive |
+---------+---------+-------------+--------+--------+
Output: 
+---------------+--------------+
| report_reason | report_count |
+---------------+--------------+
| spam          | 1            |
| racism        | 1            |
| offensive     | 1            |
+---------------+--------------+
Explanation: Note in a day, one post can be reported multiple times with the same reason.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1113 {
    public class ActionRecord {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime ActionDate { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Extra { get; set; } = string.Empty;
    }

    public class ReportCountResult {
        public string ReportReason { get; set; } = string.Empty;
        public int ReportCount { get; set; }
    }

    public class Solution {
        public static void Init() {
            List<ActionRecord> actions = [
                new() { UserId = 1, PostId = 1, ActionDate = new DateTime(2019, 7, 1), Action = "view", Extra = "" },
                new() { UserId = 2, PostId = 4, ActionDate = new DateTime(2019, 7, 4), Action = "report", Extra = "spam" },
                new() { UserId = 3, PostId = 4, ActionDate = new DateTime(2019, 7, 4), Action = "report", Extra = "spam" },
                new() { UserId = 4, PostId = 3, ActionDate = new DateTime(2019, 7, 2), Action = "report", Extra = "spam" },
                new() { UserId = 5, PostId = 2, ActionDate = new DateTime(2019, 7, 4), Action = "report", Extra = "racism" },
                new() { UserId = 5, PostId = 5, ActionDate = new DateTime(2019, 7, 4), Action = "report", Extra = "offensive" }
            ];

            var result = ReportedPosts(actions);
            foreach (var r in result) {
                Console.WriteLine($"{r.ReportReason} | {r.ReportCount}");
            }
        }

        // Time: O(n) where n is the number of action records
        // Space: O(k) where k is the number of distinct report reasons
        public static List<ReportCountResult> ReportedPosts(List<ActionRecord> actions) {
            DateTime yesterday = new(2019, 7, 4);

            return [.. actions
                .Where(a => a.ActionDate == yesterday && a.Action == "report" && !string.IsNullOrEmpty(a.Extra))
                .GroupBy(a => a.Extra)
                .Select(g => new ReportCountResult {
                    ReportReason = g.Key,
                    ReportCount = g.Select(a => a.PostId).Distinct().Count()
                })];
        }
    }
}