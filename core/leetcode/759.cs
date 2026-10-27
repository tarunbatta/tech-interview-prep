// Url:https://leetcode.com/problems/employee-free-time

/*
759. Employee Free Time
Hard

We are given a list schedule of employees, which represents the working time for each employee.

Each employee has a list of non-overlapping Intervals, and these intervals are in sorted order.

Return the list of finite intervals representing common, positive-length free time for all employees, also in sorted order.

(Even though we are representing Intervals in the form [x, y], the objects inside are Intervals, not lists or arrays. For example, schedule[0][0].start = 1, schedule[0][0].end = 2, and schedule[0][0][0] is not defined). Also, we wouldn't include intervals like [5, 5] in our answer, as they have zero length.

Example 1:
Input: schedule = [[[1,2],[5,6]],[[1,3]],[[4,10]]]
Output: [[3,4]]
Explanation: There are a total of three employees, and all common
free time intervals would be [-inf, 1], [3, 4], [10, inf].
We discard any intervals that contain inf as they aren't finite.

Example 2:
Input: schedule = [[[1,3],[6,7]],[[2,4]],[[2,5],[9,12]]]
Output: [[5,6],[7,9]]

Constraints:
- 1 <= schedule.length, schedule[i].length <= 50
- 0 <= schedule[i][j].start < schedule[i][j].end <= 10^8
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem759 {
    public class Interval {
        public int start;
        public int end;

        public Interval() { }

        public Interval(int _start, int _end) {
            start = _start;
            end = _end;
        }
    }

    public class Solution {
        public static void Init() {
            IList<IList<Interval>> schedule1 = [
                [new(1, 2), new(5, 6)],
                [new(1, 3)],
                [new(4, 10)]
            ];
            var res1 = EmployeeFreeTime(schedule1);
            Console.WriteLine(string.Join(", ", res1.Select(i => $"[{i.start},{i.end}]"))); // [3,4]

            IList<IList<Interval>> schedule2 = [
                [new(1, 3), new(6, 7)],
                [new(2, 4)],
                [new(2, 5), new(9, 12)]
            ];
            var res2 = EmployeeFreeTime(schedule2);
            Console.WriteLine(string.Join(", ", res2.Select(i => $"[{i.start},{i.end}]"))); // [5,6], [7,9]
        }

        // Time: O(N log N) where N is the total number of intervals
        // Space: O(N) to store flattened intervals and results
        public static IList<Interval> EmployeeFreeTime(IList<IList<Interval>> schedule) {
            List<Interval> allIntervals = [];
            foreach (var emp in schedule) {
                foreach (var interval in emp) {
                    allIntervals.Add(interval);
                }
            }

            allIntervals.Sort((a, b) => a.start != b.start ? a.start.CompareTo(b.start) : a.end.CompareTo(b.end));

            List<Interval> freeTime = [];
            int currEnd = allIntervals[0].end;

            for (int i = 1; i < allIntervals.Count; i++) {
                if (allIntervals[i].start > currEnd) {
                    freeTime.Add(new Interval(currEnd, allIntervals[i].start));
                    currEnd = allIntervals[i].end;
                } else {
                    currEnd = Math.Max(currEnd, allIntervals[i].end);
                }
            }

            return freeTime;
        }
    }
}