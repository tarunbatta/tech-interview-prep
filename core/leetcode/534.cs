// Url: https://leetcode.com/problems/game-play-analysis-iii

/*
534. Game Play Analysis III
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

Write a solution to report for each player and date, how many games played so far by the player. That is, the total number of games played by the player until that date. Check the example for clarity.

Return the result table in any order.

SQL Solution:
SELECT player_id, event_date,
       SUM(games_played) OVER(PARTITION BY player_id ORDER BY event_date) AS games_played_so_far
FROM Activity;
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem534
{
    public class Activity
    {
        public int PlayerId { get; set; }
        public int DeviceId { get; set; }
        public DateTime EventDate { get; set; }
        public int GamesPlayed { get; set; }
    }

    public class RunningGamesResult
    {
        public int PlayerId { get; set; }
        public string EventDate { get; set; }
        public int GamesPlayedSoFar { get; set; }
    }

    public class Solution
    {
        public static void Init()
        {
            List<Activity> activities =
            [
                new Activity { PlayerId = 1, DeviceId = 2, EventDate = new DateTime(2016, 3, 1), GamesPlayed = 5 },
                new Activity { PlayerId = 1, DeviceId = 2, EventDate = new DateTime(2016, 5, 2), GamesPlayed = 6 },
                new Activity { PlayerId = 1, DeviceId = 3, EventDate = new DateTime(2017, 6, 25), GamesPlayed = 1 },
                new Activity { PlayerId = 3, DeviceId = 1, EventDate = new DateTime(2016, 3, 2), GamesPlayed = 0 },
                new Activity { PlayerId = 3, DeviceId = 4, EventDate = new DateTime(2018, 7, 3), GamesPlayed = 5 }
            ];

            var results = GetGamesPlayedSoFar(activities);
            foreach (var r in results)
            {
                Console.WriteLine($"{r.PlayerId}, {r.EventDate}, {r.GamesPlayedSoFar}");
            }
        }

        // Time: O(n log n) where n is activities.Count (due to sorting each player's activity dates)
        // Space: O(n) for result list and grouping
        public static IList<RunningGamesResult> GetGamesPlayedSoFar(IList<Activity> activities)
        {
            List<RunningGamesResult> results = [];

            if (activities == null || activities.Count == 0)
            {
                return results;
            }

            var grouped = activities.GroupBy(a => a.PlayerId);

            foreach (var group in grouped)
            {
                int runningSum = 0;
                var sorted = group.OrderBy(a => a.EventDate);

                foreach (var act in sorted)
                {
                    runningSum += act.GamesPlayed;
                    results.Add(new RunningGamesResult
                    {
                        PlayerId = act.PlayerId,
                        EventDate = act.EventDate.ToString("yyyy-MM-dd"),
                        GamesPlayedSoFar = runningSum
                    });
                }
            }

            return results;
        }
    }
}