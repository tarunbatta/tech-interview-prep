// Url:https://leetcode.com/problems/find-cumulative-salary-of-an-employee

/*
579. Find Cumulative Salary of an Employee
Hard

Table: Employee

+-------------+------+
| Column Name | Type |
+-------------+------+
| id          | int  |
| month       | int  |
| salary      | int  |
+-------------+------+
(id, month) is the primary key (combination of columns with unique values) for this table.
Each row in the table has the information of the salary of an employee in a month.

Write a solution to calculate the cumulative salary summary for every employee in a single year.

The cumulative salary summary for an employee can be calculated as follows:
- For each month that the employee worked, sum up the salary in that month and the previous two months. This is the 3-month sum for that month. If an employee did not work for the company in previous months, their effective salary for those months is 0.
- Do not include the most recent month that the employee worked in the summary.
- Do not include the summary for any month the employee did not work.

Return the result table ordered by id in ascending order. In case of a tie, order it by month in descending order.

The result format is in the following example.

Example 1:

Input: 
Employee table:
+----+-------+--------+
| id | month | salary |
+----+-------+--------+
| 1  | 1     | 20     |
| 1  | 2     | 30     |
| 1  | 3     | 40     |
| 1  | 4     | 60     |
| 2  | 1     | 20     |
| 3  | 2     | 40     |
| 3  | 3     | 60     |
| 3  | 4     | 70     |
+----+-------+--------+
Output: 
+----+-------+--------+
| id | month | Salary |
+----+-------+--------+
| 1  | 3     | 90     |
| 1  | 2     | 50     |
| 1  | 1     | 20     |
| 3  | 3     | 100    |
| 3  | 2     | 40     |
+----+-------+--------+
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem579 {
    public class EmployeeRecord {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Salary { get; set; }
    }

    public class CumulativeSalaryResult {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Salary { get; set; }
    }

    public class Solution {
        public static void Init() {
            List<EmployeeRecord> employees = [
                new() { Id = 1, Month = 1, Salary = 20 },
                new() { Id = 1, Month = 2, Salary = 30 },
                new() { Id = 1, Month = 3, Salary = 40 },
                new() { Id = 1, Month = 4, Salary = 60 },
                new() { Id = 2, Month = 1, Salary = 20 },
                new() { Id = 3, Month = 2, Salary = 40 },
                new() { Id = 3, Month = 3, Salary = 60 },
                new() { Id = 3, Month = 4, Salary = 70 }
            ];

            var result = FindCumulativeSalary(employees);
            foreach (var r in result) {
                Console.WriteLine($"id: {r.Id}, month: {r.Month}, salary: {r.Salary}");
            }
        }

        // Time: O(n log n) where n is the number of records
        // Space: O(n) to store results and groupings
        public static List<CumulativeSalaryResult> FindCumulativeSalary(List<EmployeeRecord> records) {
            var maxMonths = records
                .GroupBy(r => r.Id)
                .ToDictionary(g => g.Key, g => g.Max(r => r.Month));

            var salaryMap = records.ToDictionary(r => (r.Id, r.Month), r => r.Salary);

            var result = new List<CumulativeSalaryResult>();

            foreach (var r in records) {
                if (r.Month == maxMonths[r.Id]) {
                    continue; // Exclude the most recent month
                }

                int sum = 0;
                for (int m = r.Month; m >= r.Month - 2 && m >= 1; m--) {
                    if (salaryMap.TryGetValue((r.Id, m), out int salary)) {
                        sum += salary;
                    }
                }

                result.Add(new CumulativeSalaryResult {
                    Id = r.Id,
                    Month = r.Month,
                    Salary = sum
                });
            }

            return [.. result
                .OrderBy(r => r.Id)
                .ThenByDescending(r => r.Month)];
        }
    }
}