// Url:https://leetcode.com/problems/project-employees-i

/*
1075. Project Employees I
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
employee_id is the primary key of this table. It's guaranteed that experience_years is not NULL.
Each row of this table contains information about one employee.

Write an SQL query that reports the average experience years of all the employees for each project, rounded to 2 digits.

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
+-------------+---------------+
| project_id  | average_years |
+-------------+---------------+
| 1           | 2.00          |
| 2           | 2.50          |
+-------------+---------------+
Explanation: The average experience years for the first project is (3 + 2 + 1) / 3 = 2.00 and for the second project is (3 + 2) / 2 = 2.50
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1075 {
    public class ProjectAssignment {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
    }

    public class Employee {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
    }

    public class ProjectAverageResult {
        public int ProjectId { get; set; }
        public double AverageYears { get; set; }
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

            List<Employee> employees = [
                new() { EmployeeId = 1, Name = "Khaled", ExperienceYears = 3 },
                new() { EmployeeId = 2, Name = "Ali", ExperienceYears = 2 },
                new() { EmployeeId = 3, Name = "John", ExperienceYears = 1 },
                new() { EmployeeId = 4, Name = "Doe", ExperienceYears = 2 }
            ];

            var result = CalculateAverageExperience(projects, employees);
            foreach (var r in result) {
                Console.WriteLine($"{r.ProjectId} | {r.AverageYears:F2}");
            }
        }

        // Time: O(P + E) where P is assignments count and E is employees count
        // Space: O(E) for employee dictionary lookup
        public List<ProjectAverageResult> CalculateAverageExperience(List<ProjectAssignment> projects, List<Employee> employees) {
            var expMap = employees.ToDictionary(e => e.EmployeeId, e => e.ExperienceYears);

            return projects
                .Where(p => expMap.ContainsKey(p.EmployeeId))
                .GroupBy(p => p.ProjectId)
                .Select(g => new ProjectAverageResult {
                    ProjectId = g.Key,
                    AverageYears = Math.Round(g.Average(p => expMap[p.EmployeeId]), 2)
                })
                .OrderBy(r => r.ProjectId)
                .ToList();
        }
    }
}