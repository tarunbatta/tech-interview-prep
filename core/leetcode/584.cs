// Url:https://leetcode.com/problems/find-customer-referee

/*
584. Find Customer Referee
Easy

Table: Customer

+-------------+---------+
| Column Name | Type    |
+-------------+---------+
| id          | int     |
| name        | varchar |
| referee_id  | int     |
+-------------+---------+
In SQL, id is the primary key column for this table.
Each row of this table indicates the id of a customer, their name, and the id of the customer who referred them.

Find the names of the customer that are not referred by the customer with id = 2.

Return the result table in any order.

The result format is in the following example.

Example 1:

Input: 
Customer table:
+----+------+------------+
| id | name | referee_id |
+----+------+------------+
| 1  | Will | null       |
| 2  | Jane | null       |
| 3  | Alex | 2          |
| 4  | Bill | null       |
| 5  | Zack | 1          |
| 6  | Mark | 2          |
+----+------+------------+
Output: 
+------+
| name |
+------+
| Will |
| Jane |
| Bill |
| Zack |
+------+
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem584 {
    public class Customer {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? RefereeId { get; set; }
    }

    public class Solution {
        public void Init() {
            List<Customer> customers = [
                new() { Id = 1, Name = "Will", RefereeId = null },
                new() { Id = 2, Name = "Jane", RefereeId = null },
                new() { Id = 3, Name = "Alex", RefereeId = 2 },
                new() { Id = 4, Name = "Bill", RefereeId = null },
                new() { Id = 5, Name = "Zack", RefereeId = 1 },
                new() { Id = 6, Name = "Mark", RefereeId = 2 }
            ];

            var result = FindCustomerReferee(customers);
            Console.WriteLine(string.Join(", ", result)); // Will, Jane, Bill, Zack
        }

        // Time: O(n) where n is the number of customers
        // Space: O(n) to store the result list
        public List<string> FindCustomerReferee(List<Customer> customers) {
            return customers
                .Where(c => c.RefereeId == null || c.RefereeId != 2)
                .Select(c => c.Name)
                .ToList();
        }
    }
}