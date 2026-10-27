// Url:https://leetcode.com/problems/sales-analysis-ii

/*
1083. Sales Analysis II
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

Write an SQL query that reports the buyers who have bought S8 but not iPhone. Note that S8 and iPhone are products presented in the Product table.

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
| 2         | 1          | 3        | 2019-06-02 | 1        | 800   |
| 3         | 3          | 3        | 2019-05-13 | 2        | 2800  |
+-----------+------------+----------+------------+----------+-------+
Output: 
+-------------+
| buyer_id    |
+-------------+
| 1           |
+-------------+
Explanation: The buyer with id 1 bought an S8 but did not buy an iPhone. The buyer with id 3 bought both.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1083 {
    public class Product {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int UnitPrice { get; set; }
    }

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
            List<Product> products = [
                new() { ProductId = 1, ProductName = "S8", UnitPrice = 1000 },
                new() { ProductId = 2, ProductName = "G4", UnitPrice = 800 },
                new() { ProductId = 3, ProductName = "iPhone", UnitPrice = 1400 }
            ];

            List<SaleRecord> sales = [
                new() { SellerId = 1, ProductId = 1, BuyerId = 1, SaleDate = new DateTime(2019, 1, 21), Quantity = 2, Price = 2000 },
                new() { SellerId = 1, ProductId = 2, BuyerId = 2, SaleDate = new DateTime(2019, 2, 17), Quantity = 1, Price = 800 },
                new() { SellerId = 2, ProductId = 1, BuyerId = 3, SaleDate = new DateTime(2019, 6, 2), Quantity = 1, Price = 800 },
                new() { SellerId = 3, ProductId = 3, BuyerId = 3, SaleDate = new DateTime(2019, 5, 13), Quantity = 2, Price = 2800 }
            ];

            var result = BuyersOfS8NotIphone(products, sales);
            Console.WriteLine(string.Join(", ", result)); // 1
        }

        // Time: O(S + P) where S is sales count and P is products count
        // Space: O(S + P)
        public List<int> BuyersOfS8NotIphone(List<Product> products, List<SaleRecord> sales) {
            int s8Id = products.FirstOrDefault(p => p.ProductName == "S8")?.ProductId ?? -1;
            int iphoneId = products.FirstOrDefault(p => p.ProductName == "iPhone")?.ProductId ?? -1;

            var iphoneBuyers = sales
                .Where(s => s.ProductId == iphoneId)
                .Select(s => s.BuyerId)
                .ToHashSet();

            return sales
                .Where(s => s.ProductId == s8Id && !iphoneBuyers.Contains(s.BuyerId))
                .Select(s => s.BuyerId)
                .Distinct()
                .OrderBy(id => id)
                .ToList();
        }
    }
}