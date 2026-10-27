// Url: https://leetcode.com/problems/logger-rate-limiter

/*
359. Logger Rate Limiter
Easy

Design a logger system that receives a stream of messages along with their timestamps. Each unique message should only be printed at most once every 10 seconds (i.e. a message printed at timestamp t will prevent other identical messages from being printed until timestamp t + 10).

All timestamps are in seconds granularity.

Implement the Logger class:
- Logger() Initializes the logger object.
- bool shouldPrintMessage(int timestamp, string message) Returns true if the message should be printed in the given timestamp, otherwise returns false.

Example 1:
Input
["Logger", "shouldPrintMessage", "shouldPrintMessage", "shouldPrintMessage", "shouldPrintMessage", "shouldPrintMessage", "shouldPrintMessage"]
[[], [1, "foo"], [2, "bar"], [3, "foo"], [8, "bar"], [10, "foo"], [11, "foo"]]
Output
[null, true, true, false, false, false, true]

Constraints:
0 <= timestamp <= 10^9
Every timestamp will be passed in non-decreasing order (chronological order).
1 <= message.length <= 30
At most 10^4 calls will be made to shouldPrintMessage.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem359
{
    public class Solution
    {
        public void Init()
        {
            Logger logger = new();
            Console.WriteLine(logger.ShouldPrintMessage(1, "foo"));  // True
            Console.WriteLine(logger.ShouldPrintMessage(2, "bar"));  // True
            Console.WriteLine(logger.ShouldPrintMessage(3, "foo"));  // False
            Console.WriteLine(logger.ShouldPrintMessage(8, "bar"));  // False
            Console.WriteLine(logger.ShouldPrintMessage(10, "foo")); // False
            Console.WriteLine(logger.ShouldPrintMessage(11, "foo")); // True
        }
    }

    // Time: O(1) per message check
    // Space: O(M) where M is the number of distinct messages received
    public class Logger
    {
        private readonly Dictionary<string, int> _messageNextAllowedTime;

        public Logger()
        {
            _messageNextAllowedTime = new Dictionary<string, int>();
        }

        public bool ShouldPrintMessage(int timestamp, string message)
        {
            if (!_messageNextAllowedTime.TryGetValue(message, out int nextAllowedTime) || timestamp >= nextAllowedTime)
            {
                _messageNextAllowedTime[message] = timestamp + 10;
                return true;
            }

            return false;
        }
    }
}