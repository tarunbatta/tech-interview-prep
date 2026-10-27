// Url:https://leetcode.com/problems/project-employees-iii

/*
1077. Project Employees III
Medium

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

Write an SQL query that reports the most experienced employees in each project. In case of a tie, report all employees with the maximum number of experience years.

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
| 3           | John   | 3                |
| 4           | Doe    | 2                |
+-------------+--------+------------------+
Output: 
+-------------+---------------+
| project_id  | employee_id   |
+-------------+---------------+
| 1           | 1             |
| 1           | 3             |
| 2           | 1             |
+-------------+---------------+
Explanation: Both employees with id 1 and 3 have the most experience among the employees of the first project. For the second project, the employee with id 1 has the most experience.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1077 {
    public class ProjectAssignment {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
    }

    public class Employee {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
    }

    public class MostExperiencedResult {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
    }

    public class Solution {
        public static void Init() {
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
                new() { EmployeeId = 3, Name = "John", ExperienceYears = 3 },
                new() { EmployeeId = 4, Name = "Doe", ExperienceYears = 2 }
            ];

            var result = MostExperiencedEmployees(projects, employees);
            foreach (var r in result) {
                Console.WriteLine($"{r.ProjectId} | {r.EmployeeId}");
            }
        }

        // Time: O(P + E) where P is assignments count and E is employees count
        // Space: O(E + P)
        public static List<MostExperiencedResult> MostExperiencedEmployees(List<ProjectAssignment> projects, List<Employee> employees) {
            var expMap = employees.ToDictionary(e => e.EmployeeId, e => e.ExperienceYears);

            var result = new List<MostExperiencedResult>();

            var grouped = projects
                .Where(p => expMap.ContainsKey(p.EmployeeId))
                .GroupBy(p => p.ProjectId);

            foreach (var g in grouped) {
                int maxExp = g.Max(p => expMap[p.EmployeeId]);
                foreach (var p in g.Where(p => expMap[p.EmployeeId] == maxExp)) {
                    result.Add(new MostExperiencedResult {
                        ProjectId = g.Key,
                        EmployeeId = p.EmployeeId
                    });
                }
            }

            return result;
        }
    }
}