// Url:https://leetcode.com/problems/sales-analysis-iii

/*
1084. Sales Analysis III
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

Write an SQL query that reports the products that were only sold in the first quarter of 2019. That is, between 2019-01-01 and 2019-03-31 inclusive.

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
+-------------+--------------+
| product_id  | product_name |
+-------------+--------------+
| 1           | S8           |
+-------------+--------------+
Explanation: 
The product with id 1 was only sold in the spring of 2019.
The product with id 2 was sold in the spring of 2019 but was also sold after the spring of 2019.
The product with id 3 was sold after spring 2019.
We return only product 1 as it is the only product that was only sold in the spring of 2019.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1084 {
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

    public class ProductResult {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
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
                new() { SellerId = 2, ProductId = 2, BuyerId = 3, SaleDate = new DateTime(2019, 6, 2), Quantity = 1, Price = 800 },
                new() { SellerId = 3, ProductId = 3, BuyerId = 4, SaleDate = new DateTime(2019, 5, 13), Quantity = 2, Price = 2800 }
            ];

            var result = ProductsOnlyInQ1_2019(products, sales);
            foreach (var r in result) {
                Console.WriteLine($"{r.ProductId} | {r.ProductName}"); // 1 | S8
            }
        }

        // Time: O(P + S) where P is products count and S is sales count
        // Space: O(P + S)
        public List<ProductResult> ProductsOnlyInQ1_2019(List<Product> products, List<SaleRecord> sales) {
            DateTime q1Start = new(2019, 1, 1);
            DateTime q1End = new(2019, 3, 31);

            var productMap = products.ToDictionary(p => p.ProductId, p => p.ProductName);

            return sales
                .Where(s => productMap.ContainsKey(s.ProductId))
                .GroupBy(s => s.ProductId)
                .Where(g => g.Min(s => s.SaleDate) >= q1Start && g.Max(s => s.SaleDate) <= q1End)
                .Select(g => new ProductResult {
                    ProductId = g.Key,
                    ProductName = productMap[g.Key]
                })
                .OrderBy(r => r.ProductId)
                .ToList();
        }
    }
}