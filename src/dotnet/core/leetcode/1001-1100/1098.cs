// Url:https://leetcode.com/problems/unpopular-books

/*
1098. Unpopular Books
Medium

Table: Books

+----------------+---------+
| Column Name    | Type    |
+----------------+---------+
| book_id        | int     |
| name           | varchar |
| available_from | date    |
+----------------+---------+
book_id is the primary key of this table.
Each row of this table indicates the name and the date when a book was available for purchase.

Table: Orders

+----------------+---------+
| Column Name    | Type    |
+----------------+---------+
| order_id       | int     |
| book_id        | int     |
| quantity       | int     |
| dispatch_date  | date    |
+----------------+---------+
order_id is the primary key of this table.
book_id is a foreign key to Books table.
Each row of this table indicates the quantity and dispatch date of one order.

Write an SQL query that reports the books that have sold less than 10 copies in the last year, excluding books that have been available for less than one month from today. Assume today is 2019-06-23.

Return the result table in any order.

The query result format is in the following example.

Example 1:

Input: 
Books table:
+---------+--------------------+----------------+
| book_id | name               | available_from |
+---------+--------------------+----------------+
| 1       | "Kalila And Demna" | 2010-01-01     |
| 2       | "28 Letters"       | 2012-05-12     |
| 3       | "The Hobbit"       | 2019-06-10     |
| 4       | "13 Reasons Why"   | 2019-05-01     |
| 5       | "The Hunger Games" | 2008-09-21     |
+---------+--------------------+----------------+
Orders table:
+----------+---------+----------+---------------+
| order_id | book_id | quantity | dispatch_date |
+----------+---------+----------+---------------+
| 1        | 1       | 2        | 2018-07-26    |
| 2        | 1       | 1        | 2018-11-05    |
| 3        | 3       | 8        | 2019-06-11    |
| 4        | 4       | 1        | 2019-06-05    |
| 5        | 4       | 5        | 2019-06-20    |
| 6        | 5       | 9        | 2009-02-02    |
| 7        | 5       | 8        | 2010-04-13    |
+----------+---------+----------+---------------+
Output: 
+-----------+--------------------+
| book_id   | name               |
+-----------+--------------------+
| 1         | "Kalila And Demna" |
| 2         | "28 Letters"       |
| 5         | "The Hunger Games" |
+-----------+--------------------+
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1098 {
    public class Book {
        public int BookId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime AvailableFrom { get; set; }
    }

    public class Order {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public DateTime DispatchDate { get; set; }
    }

    public class BookResult {
        public int BookId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class Solution {
        public static void Init() {
            List<Book> books = [
                new() { BookId = 1, Name = "Kalila And Demna", AvailableFrom = new DateTime(2010, 1, 1) },
                new() { BookId = 2, Name = "28 Letters", AvailableFrom = new DateTime(2012, 5, 12) },
                new() { BookId = 3, Name = "The Hobbit", AvailableFrom = new DateTime(2019, 6, 10) },
                new() { BookId = 4, Name = "13 Reasons Why", AvailableFrom = new DateTime(2019, 5, 1) },
                new() { BookId = 5, Name = "The Hunger Games", AvailableFrom = new DateTime(2008, 9, 21) }
            ];

            List<Order> orders = [
                new() { OrderId = 1, BookId = 1, Quantity = 2, DispatchDate = new DateTime(2018, 7, 26) },
                new() { OrderId = 2, BookId = 1, Quantity = 1, DispatchDate = new DateTime(2018, 11, 5) },
                new() { OrderId = 3, BookId = 3, Quantity = 8, DispatchDate = new DateTime(2019, 6, 11) },
                new() { OrderId = 4, BookId = 4, Quantity = 1, DispatchDate = new DateTime(2019, 6, 5) },
                new() { OrderId = 5, BookId = 4, Quantity = 5, DispatchDate = new DateTime(2019, 6, 20) },
                new() { OrderId = 6, BookId = 5, Quantity = 9, DispatchDate = new DateTime(2009, 2, 2) },
                new() { OrderId = 7, BookId = 5, Quantity = 8, DispatchDate = new DateTime(2010, 4, 13) }
            ];

            var result = FindUnpopularBooks(books, orders);
            foreach (var r in result) {
                Console.WriteLine($"{r.BookId} | {r.Name}"); // 1, 2, 5
            }
        }

        // Time: O(B + O) where B is count of books and O is count of orders
        // Space: O(B + O)
        public static List<BookResult> FindUnpopularBooks(List<Book> books, List<Order> orders) {
            DateTime today = new(2019, 6, 23);
            DateTime oneMonthBefore = today.AddDays(-30); // 2019-05-24 or AddMonths(-1)
            DateTime oneYearBefore = today.AddYears(-1);

            var recentSales = orders
                .Where(o => o.DispatchDate >= oneYearBefore && o.DispatchDate <= today)
                .GroupBy(o => o.BookId)
                .ToDictionary(g => g.Key, g => g.Sum(o => o.Quantity));

            return [.. books
                .Where(b => b.AvailableFrom < oneMonthBefore)
                .Where(b => recentSales.GetValueOrDefault(b.BookId, 0) < 10)
                .Select(b => new BookResult {
                    BookId = b.BookId,
                    Name = b.Name
                })
                .OrderBy(r => r.BookId)];
        }
    }
}