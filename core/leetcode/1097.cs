// Url:https://leetcode.com/problems/game-play-analysis-v

/*
1097. Game Play Analysis V
Hard

Table: Activity

+--------------+---------+
| Column Name  | Type    |
+--------------+---------+
| player_id    | int     |
| device_id    | int     |
| event_date   | date    |
| games_played | int     |
+--------------+---------+
(player_id, event_date) is the primary key of this table.
This table shows the activity of players of some games.
Each row is a record of a player who logged in and played a number of games (possibly 0) before logging out on someday using some device.

The install date of a player is the first login day of that player.

We define day 1 retention of some date x to be the number of players whose install date is x and they logged back in on the day right after x, divided by the number of players whose install date is x, rounded to 2 decimal places.

Write an SQL query to report for each install date, the number of players that installed the game on that day, and the day 1 retention.

Return the result table in any order.

The query result format is in the following example.

Example 1:

Input: 
Activity table:
+-----------+-----------+------------+--------------+
| player_id | device_id | event_date | games_played |
+-----------+-----------+------------+--------------+
| 1         | 2         | 2016-03-01 | 5            |
| 1         | 2         | 2016-03-02 | 6            |
| 2         | 3         | 2017-06-25 | 1            |
| 3         | 1         | 2016-03-01 | 0            |
| 3         | 4         | 2016-07-03 | 5            |
+-----------+-----------+------------+--------------+
Output: 
+------------+----------+----------------+
| install_dt | installs | Day1_retention |
+------------+----------+----------------+
| 2016-03-01 | 2        | 0.50           |
| 2017-06-25 | 1        | 0.00           |
+------------+----------+----------------+
Explanation: 
Player 1 and 3 installed the game on 2016-03-01, so 2 installs. Player 1 logged back in on 2016-03-02, but player 3 did not. So day 1 retention for 2016-03-01 is 1/2 = 0.50.
Player 2 installed the game on 2017-06-25, so 1 install. Player 2 did not log back in on 2017-06-26. So day 1 retention for 2017-06-25 is 0/1 = 0.00.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1097 {
    public class ActivityRecord {
        public int PlayerId { get; set; }
        public int DeviceId { get; set; }
        public DateTime EventDate { get; set; }
        public int GamesPlayed { get; set; }
    }

    public class RetentionResult {
        public string InstallDt { get; set; } = string.Empty;
        public int Installs { get; set; }
        public double Day1Retention { get; set; }
    }

    public class Solution {
        public void Init() {
            List<ActivityRecord> activity = [
                new() { PlayerId = 1, DeviceId = 2, EventDate = new DateTime(2016, 3, 1), GamesPlayed = 5 },
                new() { PlayerId = 1, DeviceId = 2, EventDate = new DateTime(2016, 3, 2), GamesPlayed = 6 },
                new() { PlayerId = 2, DeviceId = 3, EventDate = new DateTime(2017, 6, 25), GamesPlayed = 1 },
                new() { PlayerId = 3, DeviceId = 1, EventDate = new DateTime(2016, 3, 1), GamesPlayed = 0 },
                new() { PlayerId = 3, DeviceId = 4, EventDate = new DateTime(2016, 7, 3), GamesPlayed = 5 }
            ];

            var result = CalculateDay1Retention(activity);
            foreach (var r in result) {
                Console.WriteLine($"{r.InstallDt} | {r.Installs} | {r.Day1Retention:F2}");
            }
        }

        // Time: O(n log n) where n is the number of activity records
        // Space: O(n) to store login dates and groupings
        public List<RetentionResult> CalculateDay1Retention(List<ActivityRecord> activities) {
            var firstLogin = activities
                .GroupBy(a => a.PlayerId)
                .ToDictionary(g => g.Key, g => g.Min(a => a.EventDate));

            var allLogins = activities
                .Select(a => (a.PlayerId, a.EventDate))
                .ToHashSet();

            return firstLogin
                .GroupBy(kvp => kvp.Value)
                .Select(g => {
                    int totalInstalls = g.Count();
                    int retained = g.Count(kvp => allLogins.Contains((kvp.Key, kvp.Value.AddDays(1))));
                    return new RetentionResult {
                        InstallDt = g.Key.ToString("yyyy-MM-dd"),
                        Installs = totalInstalls,
                        Day1Retention = Math.Round((double)retained / totalInstalls, 2)
                    };
                })
                .OrderBy(r => r.InstallDt)
                .ToList();
        }
    }
}