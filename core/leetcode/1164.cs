namespace InterviewPreperationGuide.Core.LeetCode.problem1164
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // 1164. Product Price at a Given Date
    // https://leetcode.com/problems/product-price-at-a-given-date/
    //
    // Table: Products
    // +---------------+---------+
    // | Column Name   | Type    |
    // +---------------+---------+
    // | product_id    | int     |
    // | new_price     | int     |
    // | change_date   | date    |
    // +---------------+---------+
    // (product_id, change_date) is the primary key of this table.
    // Each row of this table indicates that the price of some product was changed to a new_price at some date.
    //
    // Write a solution to find the prices of all products on 2019-08-16.
    // Assume the price of all products before any change is 10.
    // Return the result table in any order.
    //
    // SQL Schema & Query:
    // SELECT product_id, 10 AS price
    // FROM Products
    // GROUP BY product_id
    // HAVING MIN(change_date) > '2019-08-16'
    // UNION ALL
    // SELECT product_id, new_price AS price
    // FROM Products
    // WHERE (product_id, change_date) IN (
    //     SELECT product_id, MAX(change_date)
    //     FROM Products
    //     WHERE change_date <= '2019-08-16'
    //     GROUP BY product_id
    // );
    public class Solution
    {
        public void Init()
        {
            var products = new List<ProductPriceChange>
            {
                new ProductPriceChange { ProductId = 1, NewPrice = 20, ChangeDate = new DateTime(2019, 8, 14) },
                new ProductPriceChange { ProductId = 2, NewPrice = 50, ChangeDate = new DateTime(2019, 8, 14) },
                new ProductPriceChange { ProductId = 1, NewPrice = 30, ChangeDate = new DateTime(2019, 8, 15) },
                new ProductPriceChange { ProductId = 1, NewPrice = 35, ChangeDate = new DateTime(2019, 8, 16) },
                new ProductPriceChange { ProductId = 2, NewPrice = 65, ChangeDate = new DateTime(2019, 8, 17) },
                new ProductPriceChange { ProductId = 3, NewPrice = 20, ChangeDate = new DateTime(2019, 8, 18) }
            };

            var targetDate = new DateTime(2019, 8, 16);
            var results = GetProductPricesOnDate(products, targetDate);
            foreach (var r in results)
            {
                Console.WriteLine($"Product: {r.ProductId}, Price: {r.Price}");
            }
        }

        // Time: O(N log N) where N is number of price change entries (grouping and ordering)
        // Space: O(N)
        public IList<ProductPriceResult> GetProductPricesOnDate(List<ProductPriceChange> products, DateTime targetDate)
        {
            var result = new List<ProductPriceResult>();
            var grouped = products.GroupBy(p => p.ProductId);

            foreach (var g in grouped)
            {
                var latestValidChange = g
                    .Where(p => p.ChangeDate <= targetDate)
                    .OrderByDescending(p => p.ChangeDate)
                    .FirstOrDefault();

                int price = latestValidChange != null ? latestValidChange.NewPrice : 10;
                result.Add(new ProductPriceResult
                {
                    ProductId = g.Key,
                    Price = price
                });
            }

            return result;
        }

        public class ProductPriceChange
        {
            public int ProductId { get; set; }
            public int NewPrice { get; set; }
            public DateTime ChangeDate { get; set; }
        }

        public class ProductPriceResult
        {
            public int ProductId { get; set; }
            public int Price { get; set; }
        }
    }
}