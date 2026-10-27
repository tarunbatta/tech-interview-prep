// Url:https://leetcode.com/problems/actors-and-directors-who-cooperated-at-least-three-times

/*
1050. Actors and Directors Who Cooperated At Least Three Times
Easy

Table: ActorDirector

+-------------+---------+
| Column Name | Type    |
+-------------+---------+
| actor_id    | int     |
| director_id | int     |
| timestamp   | int     |
+-------------+---------+
timestamp is the primary key (column with unique values) for this table.

Write a solution to find all the pairs (actor_id, director_id) where the actor has cooperated with the director at least three times.

Return the result table in any order.

The result format is in the following example.

Example 1:

Input: 
ActorDirector table:
+-------------+-------------+-------------+
| actor_id    | director_id | timestamp   |
+-------------+-------------+-------------+
| 1           | 1           | 0           |
| 1           | 1           | 1           |
| 1           | 1           | 2           |
| 1           | 2           | 3           |
| 1           | 2           | 4           |
| 2           | 1           | 5           |
| 2           | 1           | 6           |
+-------------+-------------+-------------+
Output: 
+-------------+-------------+
| actor_id    | director_id |
+-------------+-------------+
| 1           | 1           |
+-------------+-------------+
Explanation: The only pair is (1, 1) where they cooperated exactly 3 times.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1050 {
    public class ActorDirectorRecord {
        public int ActorId { get; set; }
        public int DirectorId { get; set; }
        public int Timestamp { get; set; }
    }

    public class ActorDirectorResult {
        public int ActorId { get; set; }
        public int DirectorId { get; set; }
    }

    public class Solution {
        public void Init() {
            List<ActorDirectorRecord> records = [
                new() { ActorId = 1, DirectorId = 1, Timestamp = 0 },
                new() { ActorId = 1, DirectorId = 1, Timestamp = 1 },
                new() { ActorId = 1, DirectorId = 1, Timestamp = 2 },
                new() { ActorId = 1, DirectorId = 2, Timestamp = 3 },
                new() { ActorId = 1, DirectorId = 2, Timestamp = 4 },
                new() { ActorId = 2, DirectorId = 1, Timestamp = 5 },
                new() { ActorId = 2, DirectorId = 1, Timestamp = 6 }
            ];

            var result = FindCooperations(records);
            foreach (var r in result) {
                Console.WriteLine($"{r.ActorId}, {r.DirectorId}"); // 1, 1
            }
        }

        // Time: O(n) where n is the number of records
        // Space: O(n) for grouping and results
        public List<ActorDirectorResult> FindCooperations(List<ActorDirectorRecord> records) {
            return records
                .GroupBy(r => (r.ActorId, r.DirectorId))
                .Where(g => g.Count() >= 3)
                .Select(g => new ActorDirectorResult {
                    ActorId = g.Key.ActorId,
                    DirectorId = g.Key.DirectorId
                })
                .ToList();
        }
    }
}