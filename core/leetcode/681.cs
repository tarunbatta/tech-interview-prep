// Url:https://leetcode.com/problems/next-closest-time

/*
681. Next Closest Time
Medium

Given a time represented in the format "HH:MM", form the next closest time by reusing the current digits. There is no limit on how many times a digit can be reused.

You may assume the given input string is always valid. For example, "01:34", "23:59" are all valid. "23:60" is not valid.

Example 1:
Input: time = "19:34"
Output: "19:39"
Explanation: The next closest time choosing from digits 1, 9, 3, 4, is 19:39, which occurs 5 minutes later.
It is not 19:33, because this occurs 23 hours and 59 minutes later.

Example 2:
Input: time = "23:59"
Output: "22:22"
Explanation: The next closest time choosing from digits 2, 3, 5, 9, is 22:22.
It may be assumed that the returned time is next day's time since it has smaller value than the input time numerically.

Constraints:
- time.length == 5
- time is a valid time in the form "HH:MM".
- 0 <= HH < 24
- 0 <= MM < 60
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem681 {
    public class Solution {
        public static void Init() {
            Console.WriteLine(NextClosestTime("19:34")); // "19:39"
            Console.WriteLine(NextClosestTime("23:59")); // "22:22"
        }

        // Time: O(1) at most 1440 iterations
        // Space: O(1)
        public static string NextClosestTime(string time) {
            int currentMinutes = int.Parse(time[..2]) * 60 + int.Parse(time[3..]);
            HashSet<char> allowed = [time[0], time[1], time[3], time[4]];

            for (int step = 1; step <= 1440; step++) {
                int nextMinutes = (currentMinutes + step) % 1440;
                string candidate = $"{nextMinutes / 60:D2}:{nextMinutes % 60:D2}";

                if (allowed.Contains(candidate[0]) &&
                    allowed.Contains(candidate[1]) &&
                    allowed.Contains(candidate[3]) &&
                    allowed.Contains(candidate[4])) {
                    return candidate;
                }
            }

            return time;
        }
    }
}