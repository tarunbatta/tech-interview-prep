// Url:https://leetcode.com/problems/project-employees-ii

/*
1076. Project Employees II
Easy

Table: Project

+-------------+---------+
| Column Name | Type    |
+-------------+---------+
| project_id  | int     |
| employee_id | int     |
+-------------+---------+
(project_id, employee_id) is the primary key of this table.
employee_id is a foreign key to Employee table.
Each row of this table indicates that the employee with employee_id is working on the project with project_id.

Table: Employee

+------------------+---------+
| Column Name      | Type    |
+------------------+---------+
| employee_id      | int     |
| name             | varchar |
| experience_years | int     |
+------------------+---------+
employee_id is the primary key of this table.
Each row of this table contains information about one employee.

Write an SQL query that reports all the projects that have the most employees.

Return the result table in any order.

The query result format is in the following example.

Example 1:

Input: 
Project table:
+-------------+-------------+
| project_id  | employee_id |
+-------------+-------------+
| 1           | 1           |
| 1           | 2           |
| 1           | 3           |
| 2           | 1           |
| 2           | 4           |
+-------------+-------------+
Employee table:
+-------------+--------+------------------+
| employee_id | name   | experience_years |
+-------------+--------+------------------+
| 1           | Khaled | 3                |
| 2           | Ali    | 2                |
| 3           | John   | 1                |
| 4           | Doe    | 2                |
+-------------+--------+------------------+
Output: 
+-------------+
| project_id  |
+-------------+
| 1           |
+-------------+
Explanation: The first project has 3 employees while the second one has 2.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1076 {
    public class ProjectAssignment {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
    }

    public class Solution {
        public void Init() {
            List<ProjectAssignment> projects = [
                new() { ProjectId = 1, EmployeeId = 1 },
                new() { ProjectId = 1, EmployeeId = 2 },
                new() { ProjectId = 1, EmployeeId = 3 },
                new() { ProjectId = 2, EmployeeId = 1 },
                new() { ProjectId = 2, EmployeeId = 4 }
            ];

            var result = ProjectsWithMostEmployees(projects);
            Console.WriteLine(string.Join(", ", result)); // 1
        }

        // Time: O(n) where n is the number of project assignments
        // Space: O(k) where k is the number of distinct projects
        public List<int> ProjectsWithMostEmployees(List<ProjectAssignment> projects) {
            var counts = projects
                .GroupBy(p => p.ProjectId)
                .Select(g => new { ProjectId = g.Key, Count = g.Count() })
                .ToList();

            if (counts.Count == 0) {
                return [];
            }

            int maxCount = counts.Max(c => c.Count);
            return counts
                .Where(c => c.Count == maxCount)
                .Select(c => c.ProjectId)
                .ToList();
        }
    }
}