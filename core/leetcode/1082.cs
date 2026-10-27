// Url:https://leetcode.com/problems/sales-analysis-i

/*
1082. Sales Analysis I
Easy

Table: Product

+--------------+---------+
| Column Name  | Type    |
+--------------+---------+
| product_id   | int     |
| product_name | varchar |
| unit_price   | int     |
+--------------+---------+
product_id is the primary key of this table.
Each row of this table indicates the name and the price of each product.

Table: Sales

+-------------+---------+
| Column Name | Type    |
+-------------+---------+
| seller_id   | int     |
| product_id  | int     |
| buyer_id    | int     |
| sale_date   | date    |
| quantity    | int     |
| price       | int     |
+-------------+---------+
This table can have repeated rows.
product_id is a foreign key to Product table.
Each row of this table contains some information about one sale.

Write an SQL query that reports the best sellers by total sales price, If there is a tie, report them all.

Return the result table in any order.

The query result format is in the following example.

Example 1:

Input: 
Product table:
+------------+--------------+------------+
| product_id | product_name | unit_price |
+------------+--------------+------------+
| 1          | S8           | 1000       |
| 2          | G4           | 800        |
| 3          | iPhone       | 1400       |
+------------+--------------+------------+
Sales table:
+-----------+------------+----------+------------+----------+-------+
| seller_id | product_id | buyer_id | sale_date  | quantity | price |
+-----------+------------+----------+------------+----------+-------+
| 1         | 1          | 1        | 2019-01-21 | 2        | 2000  |
| 1         | 2          | 2        | 2019-02-17 | 1        | 800   |
| 2         | 2          | 3        | 2019-06-02 | 1        | 800   |
| 3         | 3          | 4        | 2019-05-13 | 2        | 2800  |
+-----------+------------+----------+------------+----------+-------+
Output: 
+-------------+
| seller_id   |
+-------------+
| 1           |
| 3           |
+-------------+
Explanation: Both sellers with id 1 and 3 sold products with the most total price of 2800.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1082 {
    public class SaleRecord {
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }

    public class Solution {
        public void Init() {
            List<SaleRecord> sales = [
                new() { SellerId = 1, ProductId = 1, BuyerId = 1, SaleDate = new DateTime(2019, 1, 21), Quantity = 2, Price = 2000 },
                new() { SellerId = 1, ProductId = 2, BuyerId = 2, SaleDate = new DateTime(2019, 2, 17), Quantity = 1, Price = 800 },
                new() { SellerId = 2, ProductId = 2, BuyerId = 3, SaleDate = new DateTime(2019, 6, 2), Quantity = 1, Price = 800 },
                new() { SellerId = 3, ProductId = 3, BuyerId = 4, SaleDate = new DateTime(2019, 5, 13), Quantity = 2, Price = 2800 }
            ];

            var result = BestSellers(sales);
            Console.WriteLine(string.Join(", ", result)); // 1, 3
        }

        // Time: O(n) where n is the number of sales rows
        // Space: O(k) where k is the number of distinct sellers
        public List<int> BestSellers(List<SaleRecord> sales) {
            var sellerTotals = sales
                .GroupBy(s => s.SellerId)
                .Select(g => new { SellerId = g.Key, Total = g.Sum(s => s.Price) })
                .ToList();

            if (sellerTotals.Count == 0) {
                return [];
            }

            int maxTotal = sellerTotals.Max(s => s.Total);
            return sellerTotals
                .Where(s => s.Total == maxTotal)
                .Select(s => s.SellerId)
                .OrderBy(id => id)
                .ToList();
        }
    }
}