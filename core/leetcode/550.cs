// Url: https://leetcode.com/problems/game-play-analysis-iv

/*
550. Game Play Analysis IV
Medium

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

Write a solution to report the fraction of players that logged in again on the day after the day they first logged in, rounded to 2 decimal places. In other words, you need to count the number of players that logged in for at least two consecutive days starting from their first login date, then divide that number by the total number of players.

SQL Solution:
SELECT ROUND(
    COUNT(DISTINCT a.player_id) / (SELECT COUNT(DISTINCT player_id) FROM Activity),
    2
) AS fraction
FROM Activity a
JOIN (
    SELECT player_id, MIN(event_date) AS first_login
    FROM Activity
    GROUP BY player_id
) f ON a.player_id = f.player_id AND a.event_date = DATE_ADD(f.first_login, INTERVAL 1 DAY);
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem550
{
    public class Activity
    {
        public int PlayerId { get; set; }
        public int DeviceId { get; set; }
        public DateTime EventDate { get; set; }
        public int GamesPlayed { get; set; }
    }

    public class Solution
    {
        public static void Init()
        {
            List<Activity> activities =
            [
                new Activity { PlayerId = 1, DeviceId = 2, EventDate = new DateTime(2016, 3, 1), GamesPlayed = 5 },
                new Activity { PlayerId = 1, DeviceId = 2, EventDate = new DateTime(2016, 3, 2), GamesPlayed = 6 },
                new Activity { PlayerId = 2, DeviceId = 3, EventDate = new DateTime(2017, 6, 25), GamesPlayed = 1 },
                new Activity { PlayerId = 3, DeviceId = 1, EventDate = new DateTime(2016, 3, 2), GamesPlayed = 0 },
                new Activity { PlayerId = 3, DeviceId = 4, EventDate = new DateTime(2018, 7, 3), GamesPlayed = 5 }
            ];

            Console.WriteLine(FractionConsecutiveLogins(activities)); // 0.33
        }

        // Time: O(n) where n is activities.Count
        // Space: O(k) where k is the number of unique players
        public static double FractionConsecutiveLogins(IList<Activity> activities)
        {
            if (activities == null || activities.Count == 0)
            {
                return 0.0;
            }

            var grouped = activities.GroupBy(a => a.PlayerId).ToList();
            int totalPlayers = grouped.Count;
            int consecutiveCount = 0;

            foreach (var group in grouped)
            {
                DateTime firstLogin = group.Min(a => a.EventDate);
                DateTime nextDay = firstLogin.AddDays(1);

                if (group.Any(a => a.EventDate.Date == nextDay.Date))
                {
                    consecutiveCount++;
                }
            }

            return Math.Round((double)consecutiveCount / totalPlayers, 2);
        }
    }
}