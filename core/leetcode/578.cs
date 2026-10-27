// Url:https://leetcode.com/problems/get-highest-answer-rate-question

/*
578. Get Highest Answer Rate Question
Medium

Table: SurveyLog

+-------------+------+
| Column Name | Type |
+-------------+------+
| id          | int  |
| action      | ENUM |
| question_id | int  |
| answer_id   | int  |
| q_num       | int  |
| timestamp   | int  |
+-------------+------+
This table may contain duplicate rows.
action is an ENUM (category) of the type: "show", "answer", or "skip".
Each row of this table indicates an action taken by a user on a question with id question_id at timestamp.
answer_id will be null when action is "show" or "skip".

The answer rate for a question is the number of times it is answered divided by the number of times it is shown.

Write a solution to report the question that has the highest answer rate. If multiple questions have the same highest answer rate, report the question with the smallest question_id.

The result format is in the following example.

Example 1:

Input: 
SurveyLog table:
+----+--------+-------------+-----------+-------+-----------+
| id | action | question_id | answer_id | q_num | timestamp |
+----+--------+-------------+-----------+-------+-----------+
| 5  | show   | 285         | null      | 1     | 123       |
| 5  | answer | 285         | 124124    | 1     | 124       |
| 5  | show   | 369         | null      | 2     | 125       |
| 5  | skip   | 369         | null      | 2     | 126       |
+----+--------+-------------+-----------+-------+-----------+
Output: 
+------------+
| survey_log |
+------------+
| 285        |
+------------+
Explanation: 
Question 285 was showed 1 time and answered 1 time. The answer rate of question 285 is 1.0
Question 369 was showed 1 time and was not answered. The answer rate of question 369 is 0.0
Question 285 has the highest answer rate.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem578 {
    public class SurveyLog {
        public int Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public int QNum { get; set; }
        public int Timestamp { get; set; }
    }

    public class Solution {
        public static void Init() {
            List<SurveyLog> logs = [
                new() { Id = 5, Action = "show", QuestionId = 285, AnswerId = null, QNum = 1, Timestamp = 123 },
                new() { Id = 5, Action = "answer", QuestionId = 285, AnswerId = 124124, QNum = 1, Timestamp = 124 },
                new() { Id = 5, Action = "show", QuestionId = 369, AnswerId = null, QNum = 2, Timestamp = 125 },
                new() { Id = 5, Action = "skip", QuestionId = 369, AnswerId = null, QNum = 2, Timestamp = 126 }
            ];

            Console.WriteLine(GetHighestAnswerRateQuestion(logs)); // 285
        }

        // Time: O(n log n) where n is the number of survey log rows
        // Space: O(k) where k is the number of distinct questions
        public static int GetHighestAnswerRateQuestion(List<SurveyLog> logs) {
            return logs
                .GroupBy(l => l.QuestionId)
                .Select(g => new {
                    QuestionId = g.Key,
                    AnswerCount = g.Count(l => l.Action == "answer"),
                    ShowCount = g.Count(l => l.Action == "show")
                })
                .Where(x => x.ShowCount > 0)
                .OrderByDescending(x => (double)x.AnswerCount / x.ShowCount)
                .ThenBy(x => x.QuestionId)
                .Select(x => x.QuestionId)
                .FirstOrDefault();
        }
    }
}