// Url:https://leetcode.com/problems/average-salary-departments-vs-company

/*
615. Average Salary: Departments VS Company
Hard

Table: Salary

+-------------+------+
| Column Name | Type |
+-------------+------+
| id          | int  |
| employee_id | int  |
| amount      | int  |
| pay_date    | date |
+-------------+------+
id is the primary key column for this table.
Each row of this table indicates the salary of an employee in one month.
pay_date was the day that the salary was paid.

Table: Employee

+---------------+------+
| Column Name   | Type |
+---------------+------+
| employee_id   | int  |
| department_id | int  |
+---------------+------+
employee_id is the primary key column for this table.
Each row of this table indicates the department of an employee.

Write a solution to report the comparison result (higher/lower/same) of the average salary of employees in each department to the company's average salary in that month.

Return the result table in any order.

The result format is in the following example.

Example 1:

Input: 
Salary table:
+----+-------------+--------+------------+
| id | employee_id | amount | pay_date   |
+----+-------------+--------+------------+
| 1  | 1           | 9000   | 2017/03/31 |
| 2  | 2           | 6000   | 2017/03/31 |
| 3  | 3           | 10000  | 2017/03/31 |
| 4  | 1           | 7000   | 2017/02/28 |
| 5  | 2           | 6000   | 2017/02/28 |
| 6  | 3           | 8000   | 2017/02/28 |
+----+-------------+--------+------------+
Employee table:
+-------------+---------------+
| employee_id | department_id |
+-------------+---------------+
| 1           | 1             |
| 2           | 2             |
| 3           | 2             |
+-------------+---------------+
Output: 
+-----------+---------------+------------+
| pay_month | department_id | comparison |
+-----------+---------------+------------+
| 2017-03   | 1             | higher     |
| 2017-03   | 2             | lower      |
| 2017-02   | 1             | same       |
| 2017-02   | 2             | same       |
+-----------+---------------+------------+
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem615 {
    public class SalaryRecord {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int Amount { get; set; }
        public DateTime PayDate { get; set; }
    }

    public class EmployeeDepartment {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
    }

    public class SalaryComparisonResult {
        public string PayMonth { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public string Comparison { get; set; } = string.Empty;
    }

    public class Solution {
        public void Init() {
            List<SalaryRecord> salaries = [
                new() { Id = 1, EmployeeId = 1, Amount = 9000, PayDate = new DateTime(2017, 3, 31) },
                new() { Id = 2, EmployeeId = 2, Amount = 6000, PayDate = new DateTime(2017, 3, 31) },
                new() { Id = 3, EmployeeId = 3, Amount = 10000, PayDate = new DateTime(2017, 3, 31) },
                new() { Id = 4, EmployeeId = 1, Amount = 7000, PayDate = new DateTime(2017, 2, 28) },
                new() { Id = 5, EmployeeId = 2, Amount = 6000, PayDate = new DateTime(2017, 2, 28) },
                new() { Id = 6, EmployeeId = 3, Amount = 8000, PayDate = new DateTime(2017, 2, 28) }
            ];

            List<EmployeeDepartment> employees = [
                new() { EmployeeId = 1, DepartmentId = 1 },
                new() { EmployeeId = 2, DepartmentId = 2 },
                new() { EmployeeId = 3, DepartmentId = 2 }
            ];

            var result = CompareAverageSalaries(salaries, employees);
            foreach (var r in result) {
                Console.WriteLine($"{r.PayMonth} | Dept: {r.DepartmentId} | {r.Comparison}");
            }
        }

        // Time: O(S log S + E) where S is salaries count, E is employees count
        // Space: O(S + E)
        public List<SalaryComparisonResult> CompareAverageSalaries(List<SalaryRecord> salaries, List<EmployeeDepartment> employees) {
            var empDeptMap = employees.ToDictionary(e => e.EmployeeId, e => e.DepartmentId);

            var companyAvg = salaries
                .GroupBy(s => s.PayDate.ToString("yyyy-MM"))
                .ToDictionary(g => g.Key, g => g.Average(s => s.Amount));

            var deptAvg = salaries
                .Where(s => empDeptMap.ContainsKey(s.EmployeeId))
                .GroupBy(s => (Month: s.PayDate.ToString("yyyy-MM"), DeptId: empDeptMap[s.EmployeeId]))
                .Select(g => new {
                    g.Key.Month,
                    g.Key.DeptId,
                    Average = g.Average(s => s.Amount)
                });

            return deptAvg
                .Select(d => {
                    double comp = companyAvg[d.Month];
                    string comparison = Math.Abs(d.Average - comp) < 1e-6
                        ? "same"
                        : d.Average > comp ? "higher" : "lower";

                    return new SalaryComparisonResult {
                        PayMonth = d.Month,
                        DepartmentId = d.DeptId,
                        Comparison = comparison
                    };
                })
                .OrderByDescending(r => r.PayMonth)
                .ThenBy(r => r.DepartmentId)
                .ToList();
        }
    }
}