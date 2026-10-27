// Url:https://leetcode.com/problems/students-report-by-geography

/*
618. Students Report By Geography
Hard

Table: Student

+-------------+---------+
| Column Name | Type    |
+-------------+---------+
| name        | varchar |
| continent   | varchar |
+-------------+---------+
This table may contain duplicate rows.
Each row of this table indicates the name of a student and the continent they came from.

A pupil tree is a way to organize students that come from different continents.

Write a solution to pivot the continent column in the Student table so that each name is sorted alphabetically and displayed underneath its corresponding continent. The output headers should be America, Asia, and Europe, respectively.

The test cases are generated such that the student number from America is not less than either Asia or Europe.

The result format is in the following example.

Example 1:

Input: 
Student table:
+--------+-----------+
| name   | continent |
+--------+-----------+
| Jane   | America   |
| Pascal | Europe    |
| Xi     | Asia      |
| Jack   | America   |
+--------+-----------+
Output: 
+---------+------+--------+
| America | Asia | Europe |
+---------+------+--------+
| Jack    | Xi   | Pascal |
| Jane    | null | null   |
+---------+------+--------+
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem618 {
    public class StudentGeography {
        public string Name { get; set; } = string.Empty;
        public string Continent { get; set; } = string.Empty;
    }

    public class GeographyPivotResult {
        public string America { get; set; } = string.Empty;
        public string Asia { get; set; } = string.Empty;
        public string Europe { get; set; } = string.Empty;
    }

    public class Solution {
        public static void Init() {
            List<StudentGeography> students = [
                new() { Name = "Jane", Continent = "America" },
                new() { Name = "Pascal", Continent = "Europe" },
                new() { Name = "Xi", Continent = "Asia" },
                new() { Name = "Jack", Continent = "America" }
            ];

            var result = PivotGeography(students);
            foreach (var r in result) {
                Console.WriteLine($"{r.America ?? "null"} | {r.Asia ?? "null"} | {r.Europe ?? "null"}");
            }
        }

        // Time: O(n log n) where n is the number of students
        // Space: O(n) to store sorted continent lists and results
        public static List<GeographyPivotResult> PivotGeography(List<StudentGeography> students) {
            var america = students.Where(s => s.Continent == "America").Select(s => s.Name).OrderBy(n => n, StringComparer.Ordinal).ToList();
            var asia = students.Where(s => s.Continent == "Asia").Select(s => s.Name).OrderBy(n => n, StringComparer.Ordinal).ToList();
            var europe = students.Where(s => s.Continent == "Europe").Select(s => s.Name).OrderBy(n => n, StringComparer.Ordinal).ToList();

            int maxRows = Math.Max(america.Count, Math.Max(asia.Count, europe.Count));
            var result = new List<GeographyPivotResult>(maxRows);

            for (int i = 0; i < maxRows; i++) {
                result.Add(new GeographyPivotResult {
                    America = i < america.Count ? america[i] : null,
                    Asia = i < asia.Count ? asia[i] : null,
                    Europe = i < europe.Count ? europe[i] : null
                });
            }

            return result;
        }
    }
}