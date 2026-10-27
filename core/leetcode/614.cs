// Url:https://leetcode.com/problems/second-degree-follower

/*
614. Second Degree Follower
Medium

Table: Follow

+-------------+---------+
| Column Name | Type    |
+-------------+---------+
| followee    | varchar |
| follower    | varchar |
+-------------+---------+
(followee, follower) is the primary key (combination of columns with unique values) for this table.
Each row of this table indicates that the user follower follows the user followee on a social network.

A second-degree follower is a user who:
- follows at least one user, and
- is followed by at least one user.

Write a solution to report the second-degree users and the number of their followers.

Return the result table ordered by follower in alphabetical order.

The result format is in the following example.

Example 1:

Input: 
Follow table:
+----------+----------+
| followee | follower |
+----------+----------+
| Alice    | Bob      |
| Bob      | C        |
| Bob      | D        |
| D        | Donald   |
+----------+----------+
Output: 
+----------+-----+
| follower | num |
+----------+-----+
| Bob      | 2   |
| D        | 1   |
+----------+-----+
Explanation: 
User Bob has 2 followers (C and D), and Bob follows Alice. So Bob is a second-degree follower.
User D has 1 follower (Donald), and D follows Bob. So D is a second-degree follower.
User Alice has 1 follower (Bob), but does not follow anyone. So Alice is not a second-degree follower.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem614 {
    public class FollowRecord {
        public string Followee { get; set; } = string.Empty;
        public string Follower { get; set; } = string.Empty;
    }

    public class SecondDegreeResult {
        public string Follower { get; set; } = string.Empty;
        public int Num { get; set; }
    }

    public class Solution {
        public void Init() {
            List<FollowRecord> table = [
                new() { Followee = "Alice", Follower = "Bob" },
                new() { Followee = "Bob", Follower = "C" },
                new() { Followee = "Bob", Follower = "D" },
                new() { Followee = "D", Follower = "Donald" }
            ];

            var result = GetSecondDegreeFollowers(table);
            foreach (var r in result) {
                Console.WriteLine($"{r.Follower}: {r.Num}");
            }
        }

        // Time: O(n log n) where n is the number of records
        // Space: O(n) to store follow relationships and results
        public List<SecondDegreeResult> GetSecondDegreeFollowers(List<FollowRecord> follows) {
            var whoFollowsSomeone = follows.Select(f => f.Follower).ToHashSet();

            return follows
                .Where(f => whoFollowsSomeone.Contains(f.Followee))
                .GroupBy(f => f.Followee)
                .Select(g => new SecondDegreeResult {
                    Follower = g.Key,
                    Num = g.Select(x => x.Follower).Distinct().Count()
                })
                .OrderBy(r => r.Follower, StringComparer.Ordinal)
                .ToList();
        }
    }
}