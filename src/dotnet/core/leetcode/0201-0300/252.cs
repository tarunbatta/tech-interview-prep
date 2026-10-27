// Url:https://leetcode.com/problems/meeting-rooms

/*
252. Meeting Rooms
Easy

Given an array of meeting time intervals consisting of start and end times [[s1,e1],[s2,e2],...] (si < ei), determine if a person could attend all meetings.

Example 1:

Input: [[0,30],[5,10],[15,20]]
Output: false
Example 2:

Input: [[7,10],[2,4]]
Output: true

NOTE: input types have been changed on April 15, 2019. Please reset to default code definition to get new method signature.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem252 {
    public class Solution {
        public static void Init () {

        }

        // Time: O (n log n)
        // Space: O (1)
        public static bool CanAttendMeetings (int[][] intervals) {
            if (intervals == null || intervals.Length == 0 || intervals[0].Length == 0) {
                return true;
            }

            Array.Sort (intervals, (a, b) => a[0] - b[0]);

            for (int i = 0; i < intervals.Length - 1; i++) {
                if (intervals[i][1] > intervals[i + 1][0]) {
                    return false;
                }
            }

            return true;
        }
    }
}