// Url:https://leetcode.com/problems/product-sales-analysis-ii

/*
1069. Product Sales Analysis II
Easy

Table: Sales

+-------------+-------+
| Column Name | Type  |
+-------------+-------+
| sale_id     | int   |
| product_id  | int   |
| year        | int   |
| quantity    | int   |
| price       | int   |
+-------------+-------+
(sale_id, year) is the primary key (combination of columns with unique values) of this table.
product_id is a foreign key (reference column) to Product table.
Each row of this table shows a sale on the product product_id in a certain year.
Note that price is per unit.

Write a solution to report the total quantity sold for every product id.

Return the resulting table in any order.

The result format is in the following example.

Example 1:

Input: 
Sales table:
+---------+------------+------+----------+-------+
| sale_id | product_id | year | quantity | price |
+---------+------------+------+----------+-------+ 
| 1       | 100        | 2008 | 10       | 5000  |
| 2       | 100        | 2009 | 12       | 5000  |
| 7       | 200        | 2011 | 15       | 9000  |
+---------+------------+------+----------+-------+
Output: 
+------------+----------------+
| product_id | total_quantity |
+------------+----------------+
| 100        | 22             |
| 200        | 15             |
+------------+----------------+
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1069 {
    public class Sale {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Year { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }

    public class ProductQuantityResult {
        public int ProductId { get; set; }
        public int TotalQuantity { get; set; }
    }

    public class Solution {
        public static void Init() {
            List<Sale> sales = [
                new() { SaleId = 1, ProductId = 100, Year = 2008, Quantity = 10, Price = 5000 },
                new() { SaleId = 2, ProductId = 100, Year = 2009, Quantity = 12, Price = 5000 },
                new() { SaleId = 7, ProductId = 200, Year = 2011, Quantity = 15, Price = 9000 }
            ];

            var result = TotalQuantityPerProduct(sales);
            foreach (var r in result) {
                Console.WriteLine($"{r.ProductId} | {r.TotalQuantity}");
            }
        }

        // Time: O(n) where n is the number of sales rows
        // Space: O(k) where k is the number of distinct products
        public static List<ProductQuantityResult> TotalQuantityPerProduct(List<Sale> sales) {
            return [.. sales
                .GroupBy(s => s.ProductId)
                .Select(g => new ProductQuantityResult {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(s => s.Quantity)
                })];
        }
    }
}