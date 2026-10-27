// Url:https://leetcode.com/problems/sales-person

/*
607. Sales Person
Easy

Table: SalesPerson

+-----------------+---------+
| Column Name     | Type    |
+-----------------+---------+
| sales_id        | int     |
| name            | varchar |
| salary          | int     |
| commission_rate | int     |
| hire_date       | date    |
+-----------------+---------+
sales_id is the primary key column for this table.
Each row of this table indicates the name and the ID of a salesperson alongside their salary, commission rate, and hire date.

Table: Company

+-------------+---------+
| Column Name | Type    |
+-------------+---------+
| com_id      | int     |
| name        | varchar |
| city        | varchar |
+-------------+---------+
com_id is the primary key column for this table.
Each row of this table indicates the name and the ID of a company and the city in which the company is located.

Table: Orders

+-------------+------+
| Column Name | Type |
+-------------+------+
| order_id    | int  |
| order_date  | date |
| com_id      | int  |
| sales_id    | int  |
| amount      | int  |
+-------------+------+
order_id is the primary key column for this table.
com_id is a foreign key to com_id in the Company table.
sales_id is a foreign key to sales_id in the SalesPerson table.
Each row of this table contains information about one order. This includes the ID of the company, the ID of the salesperson, the date of the order, and the amount paid.

Write a solution to find the names of all the salespersons who did not have any orders related to the company with the name "RED".

Return the result table in any order.

The result format is in the following example.

Example 1:

Input: 
SalesPerson table:
+----------+------+--------+-----------------+------------+
| sales_id | name | salary | commission_rate | hire_date  |
+----------+------+--------+-----------------+------------+
| 1        | John | 100000 | 6               | 4/1/2006   |
| 2        | Amy  | 12000  | 5               | 5/1/2010   |
| 3        | Mark | 65000  | 12              | 12/25/2008 |
| 4        | Pam  | 25000  | 25              | 1/1/2005   |
| 5        | Alex | 50000  | 10              | 2/3/2007   |
+----------+------+--------+-----------------+------------+
Company table:
+--------+--------+----------+
| com_id | name   | city     |
+--------+--------+----------+
| 1      | RED    | Boston   |
| 2      | ORANGE | New York |
| 3      | YELLOW | Boston   |
| 4      | GREEN  | Austin   |
+--------+--------+----------+
Orders table:
+----------+------------+--------+----------+--------+
| order_id | order_date | com_id | sales_id | amount |
+----------+------------+--------+----------+--------+
| 1        | 1/1/2014   | 3      | 4        | 10000  |
| 2        | 2/1/2014   | 4      | 5        | 5000   |
| 3        | 3/1/2014   | 1      | 1        | 50000  |
| 4        | 4/1/2014   | 1      | 4        | 25000  |
+----------+------------+--------+----------+--------+
Output: 
+------+
| name |
+------+
| Amy  |
| Mark |
| Alex |
+------+
Explanation: 
According to orders 3 and 4 in the Orders table, easy to see that only salesperson 1 and 4 have orders with company 1.
So the result is all other persons in the SalesPerson table, including 2, 3, and 5.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem607 {
    public class SalesPerson {
        public int SalesId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Salary { get; set; }
        public int CommissionRate { get; set; }
        public DateTime HireDate { get; set; }
    }

    public class Company {
        public int ComId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }

    public class Order {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int ComId { get; set; }
        public int SalesId { get; set; }
        public int Amount { get; set; }
    }

    public class Solution {
        public static void Init() {
            List<SalesPerson> salesPersons = [
                new() { SalesId = 1, Name = "John", Salary = 100000, CommissionRate = 6, HireDate = new DateTime(2006, 4, 1) },
                new() { SalesId = 2, Name = "Amy", Salary = 12000, CommissionRate = 5, HireDate = new DateTime(2010, 5, 1) },
                new() { SalesId = 3, Name = "Mark", Salary = 65000, CommissionRate = 12, HireDate = new DateTime(2008, 12, 25) },
                new() { SalesId = 4, Name = "Pam", Salary = 25000, CommissionRate = 25, HireDate = new DateTime(2005, 1, 1) },
                new() { SalesId = 5, Name = "Alex", Salary = 50000, CommissionRate = 10, HireDate = new DateTime(2007, 2, 3) }
            ];

            List<Company> companies = [
                new() { ComId = 1, Name = "RED", City = "Boston" },
                new() { ComId = 2, Name = "ORANGE", City = "New York" },
                new() { ComId = 3, Name = "YELLOW", City = "Boston" },
                new() { ComId = 4, Name = "GREEN", City = "Austin" }
            ];

            List<Order> orders = [
                new() { OrderId = 1, OrderDate = new DateTime(2014, 1, 1), ComId = 3, SalesId = 4, Amount = 10000 },
                new() { OrderId = 2, OrderDate = new DateTime(2014, 2, 1), ComId = 4, SalesId = 5, Amount = 5000 },
                new() { OrderId = 3, OrderDate = new DateTime(2014, 3, 1), ComId = 1, SalesId = 1, Amount = 50000 },
                new() { OrderId = 4, OrderDate = new DateTime(2014, 4, 1), ComId = 1, SalesId = 4, Amount = 25000 }
            ];

            var result = SalesPersonsWithoutRedOrders(salesPersons, companies, orders);
            Console.WriteLine(string.Join(", ", result)); // Amy, Mark, Alex
        }

        // Time: O(S + C + O) where S, C, O are the counts of salespersons, companies, orders
        // Space: O(C + O) to store RED com IDs and sales IDs who sold to RED
        public static List<string> SalesPersonsWithoutRedOrders(List<SalesPerson> salesPersons, List<Company> companies, List<Order> orders) {
            var redComIds = companies
                .Where(c => c.Name == "RED")
                .Select(c => c.ComId)
                .ToHashSet();

            var redSalesIds = orders
                .Where(o => redComIds.Contains(o.ComId))
                .Select(o => o.SalesId)
                .ToHashSet();

            return [.. salesPersons
                .Where(s => !redSalesIds.Contains(s.SalesId))
                .Select(s => s.Name)];
        }
    }
}