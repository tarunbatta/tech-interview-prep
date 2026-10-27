// Url:https://leetcode.com/problems/friend-requests-ii-who-has-the-most-friends

/*
602. Friend Requests II: Who Has the Most Friends
Medium

Table: RequestAccepted

+----------------+---------+
| Column Name    | Type    |
+----------------+---------+
| requester_id   | int     |
| accepter_id    | int     |
| accept_date    | date    |
+----------------+---------+
(requester_id, accepter_id) is the primary key (combination of columns with unique values) for this table.
This table contains the ID of the user who sent the request, the ID of the user who received the request, and the date when the request was accepted.

Write a solution to find the people who have the most friends and the most friends number.

The test cases are generated so that only one person has the most friends.

The result format is in the following example.

Example 1:

Input: 
RequestAccepted table:
+--------------+-------------+-------------+
| requester_id | accepter_id | accept_date |
+--------------+-------------+-------------+
| 1            | 2           | 2016/06/03  |
| 1            | 3           | 2016/06/08  |
| 2            | 3           | 2016/06/08  |
| 3            | 4           | 2016/06/09  |
+--------------+-------------+-------------+
Output: 
+----+-----+
| id | num |
+----+-----+
| 3  | 3   |
+----+-----+
Explanation: 
The person with id 3 is a friend of people 1, 2, and 4, so he has three friends in total, which is the most number than any others.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem602 {
    public class RequestAccepted {
        public int RequesterId { get; set; }
        public int AccepterId { get; set; }
        public DateTime AcceptDate { get; set; }
    }

    public class MostFriendsResult {
        public int Id { get; set; }
        public int Num { get; set; }
    }

    public class Solution {
        public void Init() {
            List<RequestAccepted> requests = [
                new() { RequesterId = 1, AccepterId = 2, AcceptDate = new DateTime(2016, 6, 3) },
                new() { RequesterId = 1, AccepterId = 3, AcceptDate = new DateTime(2016, 6, 8) },
                new() { RequesterId = 2, AccepterId = 3, AcceptDate = new DateTime(2016, 6, 8) },
                new() { RequesterId = 3, AccepterId = 4, AcceptDate = new DateTime(2016, 6, 9) }
            ];

            var result = FindMostFriends(requests);
            Console.WriteLine($"id: {result.Id}, num: {result.Num}"); // id: 3, num: 3
        }

        // Time: O(n log n) where n is the number of friendship records
        // Space: O(n) to store combined friend relations
        public MostFriendsResult FindMostFriends(List<RequestAccepted> requests) {
            var allFriends = requests.Select(r => r.RequesterId)
                .Concat(requests.Select(r => r.AccepterId));

            var top = allFriends
                .GroupBy(id => id)
                .Select(g => new MostFriendsResult {
                    Id = g.Key,
                    Num = g.Count()
                })
                .OrderByDescending(r => r.Num)
                .FirstOrDefault();

            return top ?? new MostFriendsResult();
        }
    }
}