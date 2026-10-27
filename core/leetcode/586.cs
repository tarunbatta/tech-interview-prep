// Url:https://leetcode.com/problems/customer-placing-the-largest-number-of-orders

/*
586. Customer Placing the Largest Number of Orders
Easy

Table: Orders

+-----------------+----------+
| Column Name     | Type     |
+-----------------+----------+
| order_number    | int      |
| customer_number | int      |
+-----------------+----------+
order_number is the primary key (column with unique values) for this table.
This table contains information about the order ID and the customer ID.

Write a solution to find the customer_number for the customer who has placed the largest number of orders.

The test cases are generated so that exactly one customer will have placed more orders than any other customer.

The result format is in the following example.

Example 1:

Input: 
Orders table:
+--------------+-----------------+
| order_number | customer_number |
+--------------+-----------------+
| 1            | 1               |
| 2            | 2               |
| 3            | 3               |
| 4            | 3               |
+--------------+-----------------+
Output: 
+-----------------+
| customer_number |
+-----------------+
| 3               |
+-----------------+
Explanation: 
The customer with number 3 has two orders, which is greater than either customer 1 or 2 because each of them only has one order. 
So the result is customer_number 3.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem586 {
    public class Order {
        public int OrderNumber { get; set; }
        public int CustomerNumber { get; set; }
    }

    public class Solution {
        public void Init() {
            List<Order> orders = [
                new() { OrderNumber = 1, CustomerNumber = 1 },
                new() { OrderNumber = 2, CustomerNumber = 2 },
                new() { OrderNumber = 3, CustomerNumber = 3 },
                new() { OrderNumber = 4, CustomerNumber = 3 }
            ];

            Console.WriteLine(LargestOrdersCustomer(orders)); // 3
        }

        // Time: O(n log n) where n is the number of orders
        // Space: O(k) where k is the number of distinct customers
        public int LargestOrdersCustomer(List<Order> orders) {
            return orders
                .GroupBy(o => o.CustomerNumber)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();
        }
    }
}