namespace InterviewPreperationGuide.Core.LeetCode.problem1159
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // 1159. Market Analysis II
    // https://leetcode.com/problems/market-analysis-ii/
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
    // Write a solution to find for each user whether the brand of the second item (by date) they sold
    // is their favorite brand. If a user sold less than two items, report "no" for this user.
    // Return the result table in any order.
    //
    // SQL Schema & Query:
    // WITH RankedSales AS (
    //     SELECT seller_id, item_id,
    //            ROW_NUMBER() OVER (PARTITION BY seller_id ORDER BY order_date) AS rn
    //     FROM Orders
    // )
    // SELECT u.user_id AS seller_id,
    //        CASE WHEN i.item_brand = u.favorite_brand THEN 'yes' ELSE 'no' END AS 2nd_item_fav_brand
    // FROM Users u
    // LEFT JOIN RankedSales r ON u.user_id = r.seller_id AND r.rn = 2
    // LEFT JOIN Items i ON r.item_id = i.item_id;
    public class Solution
    {
        public void Init()
        {
            var users = new List<User>
            {
                new User { UserId = 1, JoinDate = new DateTime(2019, 1, 1), FavoriteBrand = "Lenovo" },
                new User { UserId = 2, JoinDate = new DateTime(2019, 2, 9), FavoriteBrand = "Samsung" },
                new User { UserId = 3, JoinDate = new DateTime(2019, 1, 19), FavoriteBrand = "LG" },
                new User { UserId = 4, JoinDate = new DateTime(2019, 5, 21), FavoriteBrand = "HP" }
            };

            var orders = new List<Order>
            {
                new Order { OrderId = 1, OrderDate = new DateTime(2019, 8, 1), ItemId = 4, BuyerId = 1, SellerId = 2 },
                new Order { OrderId = 2, OrderDate = new DateTime(2019, 8, 2), ItemId = 2, BuyerId = 1, SellerId = 3 },
                new Order { OrderId = 3, OrderDate = new DateTime(2019, 8, 3), ItemId = 3, BuyerId = 2, SellerId = 3 },
                new Order { OrderId = 4, OrderDate = new DateTime(2019, 8, 4), ItemId = 1, BuyerId = 4, SellerId = 2 },
                new Order { OrderId = 5, OrderDate = new DateTime(2019, 8, 4), ItemId = 1, BuyerId = 3, SellerId = 4 },
                new Order { OrderId = 6, OrderDate = new DateTime(2019, 8, 5), ItemId = 2, BuyerId = 2, SellerId = 4 }
            };

            var items = new List<Item>
            {
                new Item { ItemId = 1, ItemBrand = "Samsung" },
                new Item { ItemId = 2, ItemBrand = "Lenovo" },
                new Item { ItemId = 3, ItemBrand = "LG" },
                new Item { ItemId = 4, ItemBrand = "HP" }
            };

            var results = MarketAnalysisII(users, orders, items);
            foreach (var r in results)
            {
                Console.WriteLine($"Seller: {r.SellerId}, 2nd_item_fav_brand: {r.SecondItemFavBrand}");
            }
        }

        // Time: O(U + O log O) where U is users, O is orders (due to sorting sales per seller)
        // Space: O(U + O + I)
        public IList<MarketAnalysisIIResult> MarketAnalysisII(List<User> users, List<Order> orders, List<Item> items)
        {
            var itemBrands = items.ToDictionary(i => i.ItemId, i => i.ItemBrand);

            var secondItemPerSeller = orders
                .GroupBy(o => o.SellerId)
                .ToDictionary(
                    g => g.Key,
                    g =>
                    {
                        var sorted = g.OrderBy(x => x.OrderDate).ToList();
                        return sorted.Count >= 2 ? sorted[1].ItemId : -1;
                    });

            var result = new List<MarketAnalysisIIResult>();
            foreach (var user in users)
            {
                string fav = "no";
                if (secondItemPerSeller.TryGetValue(user.UserId, out int secondItemId) && secondItemId != -1)
                {
                    if (itemBrands.TryGetValue(secondItemId, out string brand) && brand == user.FavoriteBrand)
                    {
                        fav = "yes";
                    }
                }

                result.Add(new MarketAnalysisIIResult
                {
                    SellerId = user.UserId,
                    SecondItemFavBrand = fav
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

        public class Item
        {
            public int ItemId { get; set; }
            public string ItemBrand { get; set; }
        }

        public class MarketAnalysisIIResult
        {
            public int SellerId { get; set; }
            public string SecondItemFavBrand { get; set; }
        }
    }
}