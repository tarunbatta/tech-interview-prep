// Url:https://leetcode.com/problems/count-student-number-in-departments

/*
580. Count Student Number in Departments
Medium

Table: Student

+--------------+---------+
| Column Name  | Type    |
+--------------+---------+
| student_id   | int     |
| student_name | varchar |
| gender       | varchar |
| dept_id      | int     |
+--------------+---------+
student_id is the primary key column for this table.
dept_id is a foreign key to dept_id in the Department table.
Each row of this table contains the name and the gender of a student along with the id of the department they are in.

Table: Department

+-------------+---------+
| Column Name | Type    |
+-------------+---------+
| dept_id     | int     |
| dept_name   | varchar |
+-------------+---------+
dept_id is the primary key column for this table.
Each row of this table contains the id and the name of a department.

Write a solution to report the respective department name and number of students majoring in each department for all departments in the Department table (even ones with no current students).

Return the result table ordered by student_number in descending order. In case of a tie, order them by dept_name alphabetically.

The result format is in the following example.

Example 1:

Input: 
Student table:
+------------+--------------+--------+---------+
| student_id | student_name | gender | dept_id |
+------------+--------------+--------+---------+
| 1          | Jack         | M      | 1       |
| 2          | Jane         | F      | 1       |
| 3          | Mark         | M      | 2       |
+------------+--------------+--------+---------+
Department table:
+---------+-------------+
| dept_id | dept_name   |
+---------+-------------+
| 1       | Engineering |
| 2       | Science     |
| 3       | Law         |
+---------+-------------+
Output: 
+-------------+----------------+
| dept_name   | student_number |
+-------------+----------------+
| Engineering | 2              |
| Science     | 1              |
| Law         | 0              |
+-------------+----------------+
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem580 {
    public class Student {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int DeptId { get; set; }
    }

    public class Department {
        public int DeptId { get; set; }
        public string DeptName { get; set; } = string.Empty;
    }

    public class DepartmentStudentCount {
        public string DeptName { get; set; } = string.Empty;
        public int StudentNumber { get; set; }
    }

    public class Solution {
        public static void Init() {
            List<Student> students = [
                new() { StudentId = 1, StudentName = "Jack", Gender = "M", DeptId = 1 },
                new() { StudentId = 2, StudentName = "Jane", Gender = "F", DeptId = 1 },
                new() { StudentId = 3, StudentName = "Mark", Gender = "M", DeptId = 2 }
            ];

            List<Department> departments = [
                new() { DeptId = 1, DeptName = "Engineering" },
                new() { DeptId = 2, DeptName = "Science" },
                new() { DeptId = 3, DeptName = "Law" }
            ];

            var result = CountStudentNumber(students, departments);
            foreach (var r in result) {
                Console.WriteLine($"{r.DeptName}: {r.StudentNumber}");
            }
        }

        // Time: O(D log D + S) where D is number of departments, S is number of students
        // Space: O(D + S)
        public static List<DepartmentStudentCount> CountStudentNumber(List<Student> students, List<Department> departments) {
            var studentCounts = students
                .GroupBy(s => s.DeptId)
                .ToDictionary(g => g.Key, g => g.Count());

            return [.. departments
                .Select(d => new DepartmentStudentCount {
                    DeptName = d.DeptName,
                    StudentNumber = studentCounts.GetValueOrDefault(d.DeptId, 0)
                })
                .OrderByDescending(r => r.StudentNumber)
                .ThenBy(r => r.DeptName, StringComparer.Ordinal)];
        }
    }
}