// Url:https://leetcode.com/problems/design-log-storage-system

/*
635. Design Log Storage System
Medium

You are given several logs, where each log contains a unique ID and a timestamp. Timestamp is a string with the format: Year:Month:Day:Hour:Minute:Second, for example, 2017:01:01:23:59:59. All domains are zero-padded decimal numbers.

Implement the LogSystem class:
- LogSystem() Initializes the LogSystem object.
- void Put(int id, string timestamp) Stores the given log id and timestamp.
- IList<int> Retrieve(string start, string end, string granularity) Returns the IDs of the logs whose timestamps are within the range from start to end inclusive. granularity means the consider level of the timestamp. For example, granularity = "Day" means you need to consider the timestamp to the level of Year:Month:Day.

Example 1:
Input
["LogSystem", "put", "put", "put", "retrieve", "retrieve"]
[[], [1, "2017:01:01:23:59:59"], [2, "2017:01:01:22:59:59"], [3, "2016:01:01:00:00:00"], ["2016:01:01:01:01:01", "2017:01:01:23:00:00", "Year"], ["2016:01:01:01:01:01", "2017:01:01:23:00:00", "Hour"]]
Output
[null, null, null, null, [3, 2, 1], [2, 1]]

Explanation
LogSystem logSystem = new LogSystem();
logSystem.Put(1, "2017:01:01:23:59:59");
logSystem.Put(2, "2017:01:01:22:59:59");
logSystem.Put(3, "2016:01:01:00:00:00");

// return [3,2,1], because you need to return all logs between 2016 and 2017.
logSystem.Retrieve("2016:01:01:01:01:01", "2017:01:01:23:00:00", "Year");

// return [2,1], because you need to return all logs between 2016:01:01:01 to 2017:01:01:23.
logSystem.Retrieve("2016:01:01:01:01:01", "2017:01:01:23:00:00", "Hour");

Constraints:
- 1 <= id <= 500
- 2000 <= Year <= 2017
- 1 <= Month <= 12
- 1 <= Day <= 31
- 0 <= Hour <= 23
- 0 <= Minute, Second <= 59
- granularity is one of ["Year", "Month", "Day", "Hour", "Minute", "Second"].
- At most 300 calls will be made to put and retrieve.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem635 {
    public class LogSystem {
        private readonly List<(int Id, string Timestamp)> _logs = [];

        private static readonly Dictionary<string, int> GranularityIndices = new() {
            ["Year"] = 4,
            ["Month"] = 7,
            ["Day"] = 10,
            ["Hour"] = 13,
            ["Minute"] = 16,
            ["Second"] = 19
        };

        // Time: O(1)
        // Space: O(1)
        public void Put(int id, string timestamp) {
            _logs.Add((id, timestamp));
        }

        // Time: O(N) where N is number of stored logs
        // Space: O(N) for returned list
        public IList<int> Retrieve(string start, string end, string granularity) {
            int len = GranularityIndices[granularity];
            string startPrefix = start[..len];
            string endPrefix = end[..len];

            List<int> result = [];
            foreach (var (id, timestamp) in _logs) {
                string logPrefix = timestamp[..len];
                if (string.CompareOrdinal(logPrefix, startPrefix) >= 0 &&
                    string.CompareOrdinal(logPrefix, endPrefix) <= 0) {
                    result.Add(id);
                }
            }

            return result;
        }
    }

    public class Solution {
        public static void Init() {
            LogSystem logSystem = new();
            logSystem.Put(1, "2017:01:01:23:59:59");
            logSystem.Put(2, "2017:01:01:22:59:59");
            logSystem.Put(3, "2016:01:01:00:00:00");

            var res1 = logSystem.Retrieve("2016:01:01:01:01:01", "2017:01:01:23:00:00", "Year");
            Console.WriteLine(string.Join(", ", res1)); // 1, 2, 3

            var res2 = logSystem.Retrieve("2016:01:01:01:01:01", "2017:01:01:23:00:00", "Hour");
            Console.WriteLine(string.Join(", ", res2)); // 1, 2
        }
    }
}