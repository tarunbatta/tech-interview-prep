namespace InterviewPreperationGuide.Core.LeetCode.problem1158
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // 1158. Market Analysis I
    // https://leetcode.com/problems/market-analysis-i/
    //
    // Table: Users
    // +----------------+---------+
    // | Column Name    | Type    |
    // +----------------+---------+
    // | user_id        | int     |
    // | join_date      | date    |
    // | favorite_brand | varchar |
    // +----------------+---------+
    // user_id is the primary key of this table.
    //
    // Table: Orders
    // +---------------+---------+
    // | Column Name   | Type    |
    // +---------------+---------+
    // | order_id      | int     |
    // | order_date    | date    |
    // | item_id       | int     |
    // | buyer_id      | int     |
    // | seller_id     | int     |
    // +---------------+---------+
    // order_id is the primary key of this table.
    // buyer_id and seller_id are foreign keys to the Users table.
    //
    // Table: Items
    // +---------------+---------+
    // | Column Name   | Type    |
    // +---------------+---------+
    // | item_id       | int     |
    // | item_brand    | varchar |
    // +---------------+---------+
    // item_id is the primary key of this table.
    //
    // Write a solution to find for each user, the join date and the number of orders they made as a buyer in 2019.
    // Return the result table in any order.
    //
    // SQL Schema & Query:
    // SELECT u.user_id AS buyer_id,
    //        u.join_date,
    //        COUNT(o.order_id) AS orders_in_2019
    // FROM Users u
    // LEFT JOIN Orders o ON u.user_id = o.buyer_id AND YEAR(o.order_date) = 2019
    // GROUP BY u.user_id, u.join_date;
    public class Solution
    {
        public static void Init()
        {
            var users = new List<User>
            {
                new User { UserId = 1, JoinDate = new DateTime(2018, 1, 1), FavoriteBrand = "Lenovo" },
                new User { UserId = 2, JoinDate = new DateTime(2018, 2, 9), FavoriteBrand = "Samsung" },
                new User { UserId = 3, JoinDate = new DateTime(2018, 1, 19), FavoriteBrand = "LG" },
                new User { UserId = 4, JoinDate = new DateTime(2018, 5, 21), FavoriteBrand = "HP" }
            };

            var orders = new List<Order>
            {
                new Order { OrderId = 1, OrderDate = new DateTime(2019, 8, 1), ItemId = 4, BuyerId = 1, SellerId = 2 },
                new Order { OrderId = 2, OrderDate = new DateTime(2018, 8, 2), ItemId = 2, BuyerId = 1, SellerId = 3 },
                new Order { OrderId = 3, OrderDate = new DateTime(2019, 8, 3), ItemId = 3, BuyerId = 2, SellerId = 3 },
                new Order { OrderId = 4, OrderDate = new DateTime(2018, 8, 4), ItemId = 1, BuyerId = 4, SellerId = 2 },
                new Order { OrderId = 5, OrderDate = new DateTime(2018, 8, 4), ItemId = 1, BuyerId = 3, SellerId = 4 },
                new Order { OrderId = 6, OrderDate = new DateTime(2019, 8, 5), ItemId = 2, BuyerId = 2, SellerId = 4 }
            };

            var results = MarketAnalysis(users, orders);
            foreach (var r in results)
            {
                Console.WriteLine($"Buyer: {r.BuyerId}, JoinDate: {r.JoinDate:yyyy-MM-dd}, Orders2019: {r.OrdersIn2019}");
            }
        }

        // Time: O(U + O) where U is number of users and O is number of orders
        // Space: O(U) for grouping and output records
        public static IList<MarketAnalysisResult> MarketAnalysis(List<User> users, List<Order> orders)
        {
            var orders2019Count = orders
                .Where(o => o.OrderDate.Year == 2019)
                .GroupBy(o => o.BuyerId)
                .ToDictionary(g => g.Key, g => g.Count());

            var result = new List<MarketAnalysisResult>();
            foreach (var user in users)
            {
                orders2019Count.TryGetValue(user.UserId, out int count);
                result.Add(new MarketAnalysisResult
                {
                    BuyerId = user.UserId,
                    JoinDate = user.JoinDate,
                    OrdersIn2019 = count
                });
            }

            return result;
        }

        public class User
        {
            public int UserId { get; set; }
            public DateTime JoinDate { get; set; }
            public string FavoriteBrand { get; set; }
        }

        public class Order
        {
            public int OrderId { get; set; }
            public DateTime OrderDate { get; set; }
            public int ItemId { get; set; }
            public int BuyerId { get; set; }
            public int SellerId { get; set; }
        }

        public class MarketAnalysisResult
        {
            public int BuyerId { get; set; }
            public DateTime JoinDate { get; set; }
            public int OrdersIn2019 { get; set; }
        }
    }
}