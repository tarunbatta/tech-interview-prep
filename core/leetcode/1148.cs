// Url:https://leetcode.com/problems/article-views-i

/*
1148. Article Views I
Easy

Table: Views

+---------------+---------+
| Column Name   | Type    |
+---------------+---------+
| article_id    | int     |
| author_id     | int     |
| viewer_id     | int     |
| view_date     | date    |
+---------------+---------+
There is no primary key (column with unique values) for this table, the table may have duplicate rows.
Each row of this table indicates that some viewer viewed an article (written by some author) on some date. 
Note that equal author_id and viewer_id indicate the same person.

Write a solution to find all the authors that viewed at least one of their own articles.

Return the result table sorted by id in ascending order.

The result format is in the following example.

Example 1:

Input: 
Views table:
+------------+-----------+-----------+------------+
| article_id | author_id | viewer_id | view_date  |
+------------+-----------+-----------+------------+
| 1          | 3         | 5         | 2019-08-01 |
| 1          | 3         | 6         | 2019-08-02 |
| 2          | 7         | 7         | 2019-08-01 |
| 2          | 7         | 6         | 2019-08-02 |
| 4          | 7         | 1         | 2019-07-22 |
| 3          | 4         | 4         | 2019-07-21 |
| 3          | 4         | 4         | 2019-07-21 |
+------------+-----------+-----------+------------+
Output: 
+------+
| id   |
+------+
| 4    |
| 7    |
+------+
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1148 {
    public class ArticleView {
        public int ArticleId { get; set; }
        public int AuthorId { get; set; }
        public int ViewerId { get; set; }
        public DateTime ViewDate { get; set; }
    }

    public class Solution {
        public void Init() {
            List<ArticleView> views = [
                new() { ArticleId = 1, AuthorId = 3, ViewerId = 5, ViewDate = new DateTime(2019, 8, 1) },
                new() { ArticleId = 1, AuthorId = 3, ViewerId = 6, ViewDate = new DateTime(2019, 8, 2) },
                new() { ArticleId = 2, AuthorId = 7, ViewerId = 7, ViewDate = new DateTime(2019, 8, 1) },
                new() { ArticleId = 2, AuthorId = 7, ViewerId = 6, ViewDate = new DateTime(2019, 8, 2) },
                new() { ArticleId = 4, AuthorId = 7, ViewerId = 1, ViewDate = new DateTime(2019, 7, 22) },
                new() { ArticleId = 3, AuthorId = 4, ViewerId = 4, ViewDate = new DateTime(2019, 7, 21) },
                new() { ArticleId = 3, AuthorId = 4, ViewerId = 4, ViewDate = new DateTime(2019, 7, 21) }
            ];

            var result = AuthorsWhoViewedOwnArticles(views);
            Console.WriteLine(string.Join(", ", result)); // 4, 7
        }

        // Time: O(n log n) where n is the number of views
        // Space: O(n) for distinct authors
        public List<int> AuthorsWhoViewedOwnArticles(List<ArticleView> views) {
            return views
                .Where(v => v.AuthorId == v.ViewerId)
                .Select(v => v.AuthorId)
                .Distinct()
                .OrderBy(id => id)
                .ToList();
        }
    }
}