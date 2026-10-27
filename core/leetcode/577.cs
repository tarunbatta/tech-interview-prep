// Url: https://leetcode.com/problems/employee-bonus

/*
577. Employee Bonus
Easy

Table: Employee
+-------------+---------+
| Column Name | Type    |
+-------------+---------+
| empId       | int     |
| name        | varchar |
| supervisor  | int     |
| salary      | int     |
+-------------+---------+
empId is the column with unique values for this table.
Each row of this table indicates the name and the ID of an employee in addition to their salary and the id of their manager.

Table: Bonus
+-------------+------+
| Column Name | Type |
+-------------+------+
| empId       | int  |
| bonus       | int  |
+-------------+------+
empId is the column of unique values for this table.
empId is a foreign key to empId from the Employee table.
Each row of this table contains the id of an employee and their respective bonus.

Write a solution to report the name and bonus amount of each employee with a bonus less than 1000.
Return the result table in any order.

SQL Solution:
SELECT e.name, b.bonus
FROM Employee e
LEFT JOIN Bonus b ON e.empId = b.empId
WHERE b.bonus < 1000 OR b.bonus IS NULL;
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem577
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string Name { get; set; }
        public int? Supervisor { get; set; }
        public int Salary { get; set; }
    }

    public class Bonus
    {
        public int EmpId { get; set; }
        public int Amount { get; set; }
    }

    public class EmployeeBonusResult
    {
        public string Name { get; set; }
        public int? Bonus { get; set; }
    }

    public class Solution
    {
        public void Init()
        {
            List<Employee> employees =
            [
                new Employee { EmpId = 3, Name = "Brad", Supervisor = null, Salary = 4000 },
                new Employee { EmpId = 1, Name = "John", Supervisor = 3, Salary = 1000 },
                new Employee { EmpId = 2, Name = "Dan", Supervisor = 3, Salary = 2000 },
                new Employee { EmpId = 4, Name = "Thomas", Supervisor = 3, Salary = 4000 }
            ];

            List<Bonus> bonuses =
            [
                new Bonus { EmpId = 2, Amount = 500 },
                new Bonus { EmpId = 4, Amount = 2000 }
            ];

            var results = GetEmployeeBonus(employees, bonuses);
            foreach (var r in results)
            {
                Console.WriteLine($"{r.Name}: {r.Bonus?.ToString() ?? "null"}");
            }
        }

        // Time: O(E + B) where E is employees.Count and B is bonuses.Count
        // Space: O(E) for dictionary mapping and left join output
        public static IList<EmployeeBonusResult> GetEmployeeBonus(IList<Employee> employees, IList<Bonus> bonuses)
        {
            if (employees == null)
            {
                return new List<EmployeeBonusResult>();
            }

            Dictionary<int, int> bonusMap = bonuses?.ToDictionary(b => b.EmpId, b => b.Amount) ?? new();

            return employees
                .Where(e => !bonusMap.ContainsKey(e.EmpId) || bonusMap[e.EmpId] < 1000)
                .Select(e => new EmployeeBonusResult
                {
                    Name = e.Name,
                    Bonus = bonusMap.TryGetValue(e.EmpId, out int b) ? b : null
                })
                .ToList();
        }
    }
}