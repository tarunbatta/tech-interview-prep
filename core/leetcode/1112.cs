// Url:https://leetcode.com/problems/highest-grade-for-each-student

/*
1112. Highest Grade For Each Student
Medium

Table: Enrollments

+---------------+---------+
| Column Name   | Type    |
+---------------+---------+
| student_id    | int     |
| course_id     | int     |
| grade         | int     |
+---------------+---------+
(student_id, course_id) is the primary key (combination of columns with unique values) of this table.
grade will never be NULL.

Write a solution to find the highest grade with its corresponding course for each student. In case of a tie, you should find the course with the smallest course_id.

Return the result table ordered by student_id in ascending order.

The result format is in the following example.

Example 1:

Input: 
Enrollments table:
+------------+-------------------+
| student_id | course_id | grade |
+------------+-----------+-------+
| 2          | 2         | 95    |
| 2          | 3         | 95    |
| 1          | 1         | 90    |
| 1          | 2         | 99    |
| 3          | 1         | 80    |
| 3          | 2         | 75    |
| 3          | 3         | 82    |
+------------+-----------+-------+
Output: 
+------------+-----------+-------+
| student_id | course_id | grade |
+------------+-----------+-------+
| 1          | 2         | 99    |
| 2          | 2         | 95    |
| 3          | 3         | 82    |
+------------+-----------+-------+
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1112 {
    public class Enrollment {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Grade { get; set; }
    }

    public class HighestGradeResult {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Grade { get; set; }
    }

    public class Solution {
        public static void Init() {
            List<Enrollment> enrollments = [
                new() { StudentId = 2, CourseId = 2, Grade = 95 },
                new() { StudentId = 2, CourseId = 3, Grade = 95 },
                new() { StudentId = 1, CourseId = 1, Grade = 90 },
                new() { StudentId = 1, CourseId = 2, Grade = 99 },
                new() { StudentId = 3, CourseId = 1, Grade = 80 },
                new() { StudentId = 3, CourseId = 2, Grade = 75 },
                new() { StudentId = 3, CourseId = 3, Grade = 82 }
            ];

            var result = HighestGradeForEachStudent(enrollments);
            foreach (var r in result) {
                Console.WriteLine($"{r.StudentId} | {r.CourseId} | {r.Grade}");
            }
        }

        // Time: O(n log n) where n is the number of enrollments
        // Space: O(n) for grouping and sorting
        public static List<HighestGradeResult> HighestGradeForEachStudent(List<Enrollment> enrollments) {
            return [.. enrollments
                .GroupBy(e => e.StudentId)
                .Select(g => g
                    .OrderByDescending(e => e.Grade)
                    .ThenBy(e => e.CourseId)
                    .First())
                .Select(e => new HighestGradeResult {
                    StudentId = e.StudentId,
                    CourseId = e.CourseId,
                    Grade = e.Grade
                })
                .OrderBy(r => r.StudentId)];
        }
    }
}