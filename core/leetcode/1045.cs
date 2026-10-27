// Url:https://leetcode.com/problems/customers-who-bought-all-products

/*
1045. Customers Who Bought All Products
Medium

Table: Customer

+-------------+---------+
| Column Name | Type    |
+-------------+---------+
| customer_id | int     |
| product_key | int     |
+-------------+---------+
This table may contain duplicate rows. 
customer_id is not NULL.
product_key is a foreign key (reference column) to Product table.

Table: Product

+-------------+---------+
| Column Name | Type    |
+-------------+---------+
| product_key | int     |
+-------------+---------+
product_key is the primary key (column with unique values) for this table.

Write a solution to report the customer_ids from the Customer table who bought all the products in the Product table.

Return the result table in any order.

The result format is in the following example.

Example 1:

Input: 
Customer table:
+-------------+-------------+
| customer_id | product_key |
+-------------+-------------+
| 1           | 5           |
| 2           | 6           |
| 3           | 5           |
| 3           | 6           |
| 1           | 6           |
+-------------+-------------+
Product table:
+-------------+
| product_key |
+-------------+
| 5           |
| 6           |
+-------------+
Output: 
+-------------+
| customer_id |
+-------------+
| 1           |
| 3           |
+-------------+
Explanation: 
The customers who bought all the products (5 and 6) are customers with IDs 1 and 3.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1045 {
    public class CustomerPurchase {
        public int CustomerId { get; set; }
        public int ProductKey { get; set; }
    }

    public class ProductRecord {
        public int ProductKey { get; set; }
    }

    public class Solution {
        public void Init() {
            List<CustomerPurchase> purchases = [
                new() { CustomerId = 1, ProductKey = 5 },
                new() { CustomerId = 2, ProductKey = 6 },
                new() { CustomerId = 3, ProductKey = 5 },
                new() { CustomerId = 3, ProductKey = 6 },
                new() { CustomerId = 1, ProductKey = 6 }
            ];

            List<ProductRecord> products = [
                new() { ProductKey = 5 },
                new() { ProductKey = 6 }
            ];

            var result = FindCustomersWithAllProducts(purchases, products);
            Console.WriteLine(string.Join(", ", result)); // 1, 3
        }

        // Time: O(C + P) where C is customer purchases count and P is products count
        // Space: O(C + P)
        public List<int> FindCustomersWithAllProducts(List<CustomerPurchase> purchases, List<ProductRecord> products) {
            int totalProducts = products.Select(p => p.ProductKey).Distinct().Count();

            return purchases
                .GroupBy(p => p.CustomerId)
                .Where(g => g.Select(x => x.ProductKey).Distinct().Count() == totalProducts)
                .Select(g => g.Key)
                .OrderBy(id => id)
                .ToList();
        }
    }
}