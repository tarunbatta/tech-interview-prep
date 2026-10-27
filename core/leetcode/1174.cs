namespace InterviewPreperationGuide.Core.LeetCode.problem1174
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // 1174. Immediate Food Delivery II
    // https://leetcode.com/problems/immediate-food-delivery-ii/
    //
    // Table: Delivery
    // +-----------------------------+---------+
    // | Column Name                 | Type    |
    // +-----------------------------+---------+
    // | delivery_id                 | int     |
    // | customer_id                 | int     |
    // | order_date                  | date    |
    // | customer_pref_delivery_date | date    |
    // +-----------------------------+---------+
    // delivery_id is the column of unique values of this table.
    // The table holds information about food delivery to customers that make orders at some date and
    // specify a preferred delivery date (on the same day as the order or after it).
    //
    // If the customer's preferred delivery date is the same as the order date, then the order is called immediate;
    // otherwise, it is called scheduled.
    //
    // The first order of a customer is the order with the earliest order date that the customer made.
    // It is guaranteed that a customer has precisely one first order.
    //
    // Write a solution to find the percentage of immediate orders in the first orders of all customers, rounded to 2 decimal places.
    //
    // SQL Schema & Query:
    // SELECT ROUND(100.0 * SUM(CASE WHEN order_date = customer_pref_delivery_date THEN 1 ELSE 0 END) / COUNT(*), 2) AS immediate_percentage
    // FROM Delivery
    // WHERE (customer_id, order_date) IN (
    //     SELECT customer_id, MIN(order_date)
    //     FROM Delivery
    //     GROUP BY customer_id
    // );
    public class Solution
    {
        public void Init()
        {
            var deliveries = new List<Delivery>
            {
                new Delivery { DeliveryId = 1, CustomerId = 1, OrderDate = new DateTime(2019, 8, 1), CustomerPrefDeliveryDate = new DateTime(2019, 8, 2) },
                new Delivery { DeliveryId = 2, CustomerId = 2, OrderDate = new DateTime(2019, 8, 2), CustomerPrefDeliveryDate = new DateTime(2019, 8, 2) },
                new Delivery { DeliveryId = 3, CustomerId = 1, OrderDate = new DateTime(2019, 8, 11), CustomerPrefDeliveryDate = new DateTime(2019, 8, 12) },
                new Delivery { DeliveryId = 4, CustomerId = 3, OrderDate = new DateTime(2019, 8, 24), CustomerPrefDeliveryDate = new DateTime(2019, 8, 24) },
                new Delivery { DeliveryId = 5, CustomerId = 3, OrderDate = new DateTime(2019, 8, 21), CustomerPrefDeliveryDate = new DateTime(2019, 8, 22) },
                new Delivery { DeliveryId = 6, CustomerId = 2, OrderDate = new DateTime(2019, 8, 11), CustomerPrefDeliveryDate = new DateTime(2019, 8, 13) },
                new Delivery { DeliveryId = 7, CustomerId = 4, OrderDate = new DateTime(2019, 8, 9), CustomerPrefDeliveryDate = new DateTime(2019, 8, 9) }
            };

            Console.WriteLine(ImmediateFirstOrdersPercentage(deliveries)); // 50.00
        }

        // Time: O(N log N) where N is the number of deliveries (grouping and ordering per customer)
        // Space: O(N) for groupings
        public double ImmediateFirstOrdersPercentage(List<Delivery> deliveries)
        {
            if (deliveries == null || deliveries.Count == 0)
            {
                return 0.0;
            }

            var firstOrders = deliveries
                .GroupBy(d => d.CustomerId)
                .Select(g => g.OrderBy(d => d.OrderDate).First())
                .ToList();

            int immediateCount = firstOrders.Count(d => d.OrderDate.Date == d.CustomerPrefDeliveryDate.Date);
            return Math.Round(100.0 * immediateCount / firstOrders.Count, 2);
        }

        public class Delivery
        {
            public int DeliveryId { get; set; }
            public int CustomerId { get; set; }
            public DateTime OrderDate { get; set; }
            public DateTime CustomerPrefDeliveryDate { get; set; }
        }
    }
}