// Url:https://leetcode.com/problems/triangle-judgement

/*
610. Triangle Judgement
Easy

Table: Triangle

+-------------+------+
| Column Name | Type |
+-------------+------+
| x           | int  |
| y           | int  |
| z           | int  |
+-------------+------+
In SQL, (x, y, z) is the primary key column for this table.
Each row of this table contains the lengths of three line segments x, y, and z.

Report for every three line segments whether they can form a triangle.

Return the result table in any order.

The result format is in the following example.

Example 1:

Input: 
Triangle table:
+----+----+----+
| x  | y  | z  |
+----+----+----+
| 13 | 15 | 30 |
| 10 | 20 | 15 |
+----+----+----+
Output: 
+----+----+----+----------+
| x  | y  | z  | triangle |
+----+----+----+----------+
| 13 | 15 | 30 | No       |
| 10 | 20 | 15 | Yes      |
+----+----+----+----------+
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem610 {
    public class TriangleRecord {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    public class TriangleJudgementResult {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public string Triangle { get; set; } = string.Empty;
    }

    public class Solution {
        public static void Init() {
            List<TriangleRecord> records = [
                new() { X = 13, Y = 15, Z = 30 },
                new() { X = 10, Y = 20, Z = 15 }
            ];

            var result = JudgeTriangle(records);
            foreach (var r in result) {
                Console.WriteLine($"{r.X}, {r.Y}, {r.Z} => {r.Triangle}");
            }
        }

        // Time: O(n) where n is the number of rows
        // Space: O(n) to store results
        public static List<TriangleJudgementResult> JudgeTriangle(List<TriangleRecord> records) {
            return [.. records.Select(r => new TriangleJudgementResult {
                X = r.X,
                Y = r.Y,
                Z = r.Z,
                Triangle = (r.X + r.Y > r.Z && r.X + r.Z > r.Y && r.Y + r.Z > r.X) ? "Yes" : "No"
            })];
        }
    }
}