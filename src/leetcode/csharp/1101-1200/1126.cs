// Url:https://leetcode.com/problems/active-businesses

/*
1126. Active Businesses
Medium

Table: Events

+---------------+---------+
| Column Name   | Type    |
+---------------+---------+
| business_id   | int     |
| event_type    | varchar |
| occurences    | int     |
+---------------+---------+
(business_id, event_type) is the primary key (combination of columns with unique values) of this table.
Each row in the table logs the info that an event of some type occurred at some business for a number of times.

The average activity for a particular event_type is the average of occurences across all businesses that have this event.

An active business is a business that has more than one event_type where the occurences for that event_type is strictly greater than the average activity for that event.

Write a solution to find all active businesses.

Return the result table in any order.

The result format is in the following example.

Example 1:

Input: 
Events table:
+-------------+------------+------------+
| business_id | event_type | occurences |
+-------------+------------+------------+
| 1           | reviews    | 7          |
| 3           | reviews    | 3          |
| 1           | ads        | 11         |
| 2           | ads        | 8          |
| 3           | ads        | 6          |
| 1           | page views | 3          |
| 2           | page views | 12         |
+-------------+------------+------------+
Output: 
+-------------+
| business_id |
+-------------+
| 1           |
+-------------+
Explanation:  
The average occurence of each type of event is:
- "reviews": (7 + 3) / 2 = 5
- "ads": (11 + 8 + 6) / 3 = 8.33
- "page views": (3 + 12) / 2 = 7.5
The business with id=1 has 7 'reviews' events (more than 5) and 11 'ads' events (more than 8.33), so it is an active business.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1126 {
    public class EventRecord {
        public int BusinessId { get; set; }
        public string EventType { get; set; } = string.Empty;
        public int Occurences { get; set; }
    }

    public class Solution {
        public static void Init() {
            List<EventRecord> events = [
                new() { BusinessId = 1, EventType = "reviews", Occurences = 7 },
                new() { BusinessId = 3, EventType = "reviews", Occurences = 3 },
                new() { BusinessId = 1, EventType = "ads", Occurences = 11 },
                new() { BusinessId = 2, EventType = "ads", Occurences = 8 },
                new() { BusinessId = 3, EventType = "ads", Occurences = 6 },
                new() { BusinessId = 1, EventType = "page views", Occurences = 3 },
                new() { BusinessId = 2, EventType = "page views", Occurences = 12 }
            ];

            var result = FindActiveBusinesses(events);
            Console.WriteLine(string.Join(", ", result)); // 1
        }

        // Time: O(n) where n is the number of event records
        // Space: O(E) where E is distinct event types
        public static List<int> FindActiveBusinesses(List<EventRecord> events) {
            var eventAvg = events
                .GroupBy(e => e.EventType)
                .ToDictionary(g => g.Key, g => g.Average(e => e.Occurences));

            return [.. events
                .Where(e => e.Occurences > eventAvg[e.EventType])
                .GroupBy(e => e.BusinessId)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .OrderBy(id => id)];
        }
    }
}