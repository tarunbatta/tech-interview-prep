// Url:https://leetcode.com/problems/consecutive-available-seats

/*
603. Consecutive Available Seats
Easy

Table: Cinema

+-------------+------+
| Column Name | Type |
+-------------+------+
| seat_id     | int  |
| free        | bool |
+-------------+------+
seat_id is an auto-increment column for this table.
Each row of this table indicates whether the i-th seat is free or not. 1 means free while 0 means occupied.

Find all the consecutive available seats in the cinema.

Consecutive available seats are more than 2(inclusive) seats next to each other that are free.

Return the result table ordered by seat_id in ascending order.

The result format is in the following example.

Example 1:

Input: 
Cinema table:
+---------+------+
| seat_id | free |
+---------+------+
| 1       | 1    |
| 2       | 0    |
| 3       | 1    |
| 4       | 1    |
| 5       | 1    |
+---------+------+
Output: 
+---------+
| seat_id |
+---------+
| 3       |
| 4       |
| 5       |
+---------+
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem603 {
    public class Cinema {
        public int SeatId { get; set; }
        public bool Free { get; set; }
    }

    public class Solution {
        public static void Init() {
            List<Cinema> seats = [
                new() { SeatId = 1, Free = true },
                new() { SeatId = 2, Free = false },
                new() { SeatId = 3, Free = true },
                new() { SeatId = 4, Free = true },
                new() { SeatId = 5, Free = true }
            ];

            var result = ConsecutiveAvailableSeats(seats);
            Console.WriteLine(string.Join(", ", result)); // 3, 4, 5
        }

        // Time: O(n log n) where n is the number of seats
        // Space: O(n) to store seat availability lookup and results
        public static List<int> ConsecutiveAvailableSeats(List<Cinema> seats) {
            var freeSet = seats
                .Where(s => s.Free)
                .Select(s => s.SeatId)
                .ToHashSet();

            return [.. freeSet
                .Where(id => freeSet.Contains(id - 1) || freeSet.Contains(id + 1))
                .OrderBy(id => id)];
        }
    }
}