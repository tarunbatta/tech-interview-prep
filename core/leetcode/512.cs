// Url: https://leetcode.com/problems/game-play-analysis-ii

/*
512. Game Play Analysis II
Easy

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

Write a solution to report the device that is first logged in for each player.
Return the result table in any order.

SQL Solution:
SELECT player_id, device_id
FROM Activity
WHERE (player_id, event_date) IN (
    SELECT player_id, MIN(event_date)
    FROM Activity
    GROUP BY player_id
);
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem512
{
    public class Activity
    {
        public int PlayerId { get; set; }
        public int DeviceId { get; set; }
        public DateTime EventDate { get; set; }
        public int GamesPlayed { get; set; }
    }

    public class PlayerDeviceResult
    {
        public int PlayerId { get; set; }
        public int DeviceId { get; set; }
    }

    public class Solution
    {
        public static void Init()
        {
            List<Activity> activities =
            [
                new() { PlayerId = 1, DeviceId = 2, EventDate = new(2016, 3, 1), GamesPlayed = 5 },
                new() { PlayerId = 1, DeviceId = 3, EventDate = new(2016, 5, 2), GamesPlayed = 6 },
                new() { PlayerId = 2, DeviceId = 3, EventDate = new(2017, 6, 25), GamesPlayed = 1 },
                new() { PlayerId = 3, DeviceId = 1, EventDate = new(2016, 3, 2), GamesPlayed = 0 },
                new() { PlayerId = 3, DeviceId = 4, EventDate = new(2018, 7, 3), GamesPlayed = 5 }
            ];

            var results = GetFirstLoginDevice(activities);
            foreach (var r in results)
            {
                Console.WriteLine($"{r.PlayerId}: {r.DeviceId}");
            }
        }

        // Time: O(n) where n is activities.Count
        // Space: O(k) where k is the number of unique players
        public static IList<PlayerDeviceResult> GetFirstLoginDevice(IList<Activity> activities)
        {
            if (activities == null || activities.Count == 0)
            {
                return [];
            }

            return [.. activities
                .GroupBy(a => a.PlayerId)
                .Select(g =>
                {
                    var earliest = g.OrderBy(a => a.EventDate).First();
                    return new PlayerDeviceResult
                    {
                        PlayerId = earliest.PlayerId,
                        DeviceId = earliest.DeviceId
                    };
                })];
        }
    }
}