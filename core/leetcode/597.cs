// Url:https://leetcode.com/problems/friend-requests-i-overall-acceptance-rate

/*
597. Friend Requests I: Overall Acceptance Rate
Easy

Table: FriendRequest

+----------------+---------+
| Column Name    | Type    |
+----------------+---------+
| sender_id      | int     |
| send_to_id     | int     |
| request_date   | date    |
+----------------+---------+
This table may contain duplicate rows.
This table contains the ID of the user who sent the request, the ID of the user who received the request, and the date when the request was sent.

Table: RequestAccepted

+----------------+---------+
| Column Name    | Type    |
+----------------+---------+
| requester_id   | int     |
| accepter_id    | int     |
| accept_date    | date    |
+----------------+---------+
This table may contain duplicate rows.
This table contains the ID of the user who sent the request, the ID of the user who received the request, and the date when the request was accepted.

Find the overall acceptance rate of requests, which is the number of acceptance divided by the number of requests. The rate should be rounded to 2 decimal places.

Note:
- The accepted requests are not necessarily from the table FriendRequest. In this case, Count the total accepted requests (no matter whether they are in the table FriendRequest), and divide it by the number of requests.
- Count the distinct requests (sender_id, send_to_id) and distinct acceptances (requester_id, accepter_id).
- If there are no requests at all, you should return 0.00 as the accept_rate.

The result format is in the following example.

Example 1:

Input: 
FriendRequest table:
+-----------+------------+--------------+
| sender_id | send_to_id | request_date |
+-----------+------------+--------------+
| 1         | 2          | 2016/06/01   |
| 1         | 3          | 2016/06/01   |
| 1         | 4          | 2016/06/01   |
| 2         | 3          | 2016/06/02   |
| 3         | 4          | 2016/06/09   |
+-----------+------------+--------------+
RequestAccepted table:
+--------------+-------------+-------------+
| requester_id | accepter_id | accept_date |
+--------------+-------------+-------------+
| 1            | 2           | 2016/06/03  |
| 1            | 3           | 2016/06/08  |
| 2            | 3           | 2016/06/08  |
| 3            | 4           | 2016/06/09  |
| 3            | 4           | 2016/06/10  |
+--------------+-------------+-------------+
Output: 
+-------------+
| accept_rate |
+-------------+
| 0.80        |
+-------------+
Explanation: 
There are 4 unique accepted requests, and there are 5 unique requests. So the rate is 4/5 = 0.80.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem597 {
    public class FriendRequest {
        public int SenderId { get; set; }
        public int SendToId { get; set; }
        public DateTime RequestDate { get; set; }
    }

    public class RequestAccepted {
        public int RequesterId { get; set; }
        public int AccepterId { get; set; }
        public DateTime AcceptDate { get; set; }
    }

    public class Solution {
        public static void Init() {
            List<FriendRequest> requests = [
                new() { SenderId = 1, SendToId = 2, RequestDate = new DateTime(2016, 6, 1) },
                new() { SenderId = 1, SendToId = 3, RequestDate = new DateTime(2016, 6, 1) },
                new() { SenderId = 1, SendToId = 4, RequestDate = new DateTime(2016, 6, 1) },
                new() { SenderId = 2, SendToId = 3, RequestDate = new DateTime(2016, 6, 2) },
                new() { SenderId = 3, SendToId = 4, RequestDate = new DateTime(2016, 6, 9) }
            ];

            List<RequestAccepted> accepts = [
                new() { RequesterId = 1, AccepterId = 2, AcceptDate = new DateTime(2016, 6, 3) },
                new() { RequesterId = 1, AccepterId = 3, AcceptDate = new DateTime(2016, 6, 8) },
                new() { RequesterId = 2, AccepterId = 3, AcceptDate = new DateTime(2016, 6, 8) },
                new() { RequesterId = 3, AccepterId = 4, AcceptDate = new DateTime(2016, 6, 9) },
                new() { RequesterId = 3, AccepterId = 4, AcceptDate = new DateTime(2016, 6, 10) }
            ];

            Console.WriteLine($"{FindAcceptanceRate(requests, accepts):F2}"); // 0.80
        }

        // Time: O(R + A) where R is number of requests and A is number of accepts
        // Space: O(R + A) to store distinct pairs
        public static double FindAcceptanceRate(List<FriendRequest> requests, List<RequestAccepted> accepts) {
            int totalRequests = requests
                .Select(r => (r.SenderId, r.SendToId))
                .Distinct()
                .Count();

            if (totalRequests == 0) {
                return 0.00;
            }

            int totalAccepts = accepts
                .Select(a => (a.RequesterId, a.AccepterId))
                .Distinct()
                .Count();

            return Math.Round((double)totalAccepts / totalRequests, 2);
        }
    }
}