// Url: https://leetcode.com/problems/median-employee-salary

/*
569. Median Employee Salary
Hard

Table: Employee
+--------------+---------+
| Column Name  | Type    |
+--------------+---------+
| id           | int     |
| company      | varchar |
| salary       | int     |
+--------------+---------+
id is the primary key column for this table.
Each row of this table indicates the company and the salary of one employee.

Write a solution to find the rows that contain the median salary of each company. While calculating the median, when you sort the salaries of the company, break the ties by id.

Return the result table in any order.

SQL Solution:
WITH Ranked AS (
    SELECT id, company, salary,
           ROW_NUMBER() OVER(PARTITION BY company ORDER BY salary, id) AS rk,
           COUNT(*) OVER(PARTITION BY company) AS total
    FROM Employee
)
SELECT id, company, salary
FROM Ranked
WHERE rk IN (FLOOR((total + 1) / 2.0), CEIL((total + 1) / 2.0));
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem569
{
    public class Employee
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public int Salary { get; set; }
    }

    public class Solution
    {
        public static void Init()
        {
            List<Employee> employees =
            [
                new Employee { Id = 1, Company = "A", Salary = 2341 },
                new Employee { Id = 2, Company = "A", Salary = 341 },
                new Employee { Id = 3, Company = "A", Salary = 15 },
                new Employee { Id = 4, Company = "A", Salary = 15314 },
                new Employee { Id = 5, Company = "A", Salary = 451 },
                new Employee { Id = 6, Company = "A", Salary = 513 }
            ];

            var medians = GetMedianEmployeeSalaries(employees);
            foreach (var e in medians)
            {
                Console.WriteLine($"{e.Id}, {e.Company}, {e.Salary}");
            }
        }

        // Time: O(N log N) where N is employees.Count (due to sorting by salary and id per company)
        // Space: O(N) for grouping and filtered median results
        public static IList<Employee> GetMedianEmployeeSalaries(IList<Employee> employees)
        {
            List<Employee> results = [];

            if (employees == null || employees.Count == 0)
            {
                return results;
            }

            var grouped = employees.GroupBy(e => e.Company);

            foreach (var group in grouped)
            {
                var sorted = group.OrderBy(e => e.Salary).ThenBy(e => e.Id).ToList();
                int total = sorted.Count;

                int m1 = (total - 1) / 2;
                int m2 = total / 2;

                results.Add(sorted[m1]);
                if (m1 != m2)
                {
                    results.Add(sorted[m2]);
                }
            }

            return results;
        }
    }
}