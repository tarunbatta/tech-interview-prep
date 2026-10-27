using System.Reflection.Metadata.Ecma335;
// Url: https://leetcode.com/problems/meeting-scheduler/

/*
1229. Meeting Scheduler
Medium

Given the availability time slots arrays slots1 and slots2 of two people and a meeting duration duration, return the earliest time slot that works for both of them and is of duration duration.

If there is no common time slot that satisfies the requirements, return an empty array.

The format of a time slot is an array of two elements [start, end] representing an inclusive time range from start to end.  

It is guaranteed that no two availability slots of the same person intersect with each other. That is, for any two time slots [start1, end1] and [start2, end2] of the same person, either start1 > end2 or start2 > end1.

 

Example 1:

Input: slots1 = [[10,50],[60,120],[140,210]], slots2 = [[0,15],[60,70]], duration = 8
Output: [60,68]
Example 2:

Input: slots1 = [[10,50],[60,120],[140,210]], slots2 = [[0,15],[60,70]], duration = 12
Output: []
 

Constraints:

1 <= slots1.length, slots2.length <= 10^4
slots1[i].length, slots2[i].length == 2
slots1[i][0] < slots1[i][1]
slots2[i][0] < slots2[i][1]
0 <= slots1[i][j], slots2[i][j] <= 10^9
1 <= duration <= 10^6
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem1229 {
    public class Solution {
        public static void Init () {

        }

        // Time: O (n log n)
        // Space: O (n)
        public static IList<int> MinAvailableDuration (int[][] slots1, int[][] slots2, int duration) {
            List<int> result = [];

            if (slots1 == null || slots1.Length == 0 || slots1[0].Length == 0 ||
                slots2 == null || slots2.Length == 0 || slots2[0].Length == 0 ||
                duration < 1) {
                return result;
            }

            Array.Sort (slots1, (a, b) => a[0] - b[0]);
            Array.Sort (slots2, (a, b) => a[0] - b[0]);

            int i = 0, j = 0;
            int n1 = slots1.Length;
            int n2 = slots2.Length;

            while (i < n1 && j < n2) {
                // Find intersect between slots1[i] and slots2[j]
                int intersectStart = Math.Max (slots1[i][0], slots2[j][0]);
                int intersectEnd = Math.Min (slots1[i][1], slots2[j][1]);

                if (intersectStart + duration <= intersectEnd) // Found the result
                    return [intersectStart, intersectStart + duration];
                else if (slots1[i][1] < slots2[j][1])
                    i++;
                else
                    j++;
            }

            return [];
        }
    }
}