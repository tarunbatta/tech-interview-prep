// Url: https://leetcode.com/problems/managers-with-at-least-5-direct-reports

/*
570. Managers with at Least 5 Direct Reports
Medium

Table: Employee
+-------------+---------+
| Column Name | Type    |
+-------------+---------+
| id          | int     |
| name        | varchar |
| department  | varchar |
| managerId   | int     |
+-------------+---------+
id is the primary key column for this table.
Each row of this table indicates the name of an employee, their department, and the id of their manager.
If managerId is null, then the employee does not have a manager.
No employee will be their own manager.

Write a solution to find managers with at least five direct reports.
Return the result table in any order.

SQL Solution:
SELECT m.name
FROM Employee m
JOIN Employee e ON m.id = e.managerId
GROUP BY m.id, m.name
HAVING COUNT(e.id) >= 5;
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem570
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int? ManagerId { get; set; }
    }

    public class Solution
    {
        public static void Init()
        {
            List<Employee> employees =
            [
                new() { Id = 101, Name = "John", Department = "A", ManagerId = null },
                new() { Id = 102, Name = "Dan", Department = "A", ManagerId = 101 },
                new() { Id = 103, Name = "James", Department = "A", ManagerId = 101 },
                new() { Id = 104, Name = "Amy", Department = "A", ManagerId = 101 },
                new() { Id = 105, Name = "Anne", Department = "A", ManagerId = 101 },
                new() { Id = 106, Name = "Ron", Department = "A", ManagerId = 101 }
            ];

            var managers = FindManagersWithAtLeast5DirectReports(employees);
            foreach (var m in managers)
            {
                Console.WriteLine(m); // John
            }
        }

        // Time: O(n) where n is employees.Count
        // Space: O(n) for manager frequency mapping
        public static IList<string> FindManagersWithAtLeast5DirectReports(IList<Employee> employees)
        {
            if (employees == null || employees.Count == 0)
            {
                return [];
            }

            var managerCounts = employees
                .Where(e => e.ManagerId.HasValue)
                .GroupBy(e => e.ManagerId.Value)
                .Where(g => g.Count() >= 5)
                .Select(g => g.Key)
                .ToHashSet();

            return [.. employees
                .Where(e => managerCounts.Contains(e.Id))
                .Select(e => e.Name)];
        }
    }
}