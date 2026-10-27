// Url: https://leetcode.com/problems/design-hit-counter

/*
362. Design Hit Counter
Medium

Design a hit counter which counts the number of hits received in the past 5 minutes (i.e., the past 300 seconds).

Your system should accept a timestamp parameter (in seconds granularity), and you may assume that calls are being made to the system in chronological order (i.e., timestamp is monotonically increasing). Several hits may arrive roughly at the same time.

Implement the HitCounter class:
- HitCounter() Initializes the object of the hit counter system.
- void hit(int timestamp) Records a hit that happened at timestamp (in seconds). Several hits may happen at the same timestamp.
- int getHits(int timestamp) Returns the number of hits in the past 5 minutes from timestamp (i.e., the past 300 seconds).

Example 1:
Input
["HitCounter", "hit", "hit", "hit", "getHits", "hit", "getHits", "getHits"]
[[], [1], [2], [3], [4], [300], [300], [301]]
Output
[null, null, null, null, 3, null, 4, 3]

Explanation
HitCounter hitCounter = new HitCounter();
hitCounter.hit(1);       // hit at timestamp 1.
hitCounter.hit(2);       // hit at timestamp 2.
hitCounter.hit(3);       // hit at timestamp 3.
hitCounter.getHits(4);   // get hits at timestamp 4, return 3.
hitCounter.hit(300);     // hit at timestamp 300.
hitCounter.getHits(300); // get hits at timestamp 300, return 4.
hitCounter.getHits(301); // get hits at timestamp 301, return 3.

Constraints:
1 <= timestamp <= 2 * 10^9
All the calls are being made to the system in chronological order (i.e., timestamp is monotonically increasing).
At most 300 calls will be made to hit and getHits.

Follow up: What if the number of hits per second could be huge? Does your design scale?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem362
{
    public class Solution
    {
        public static void Init()
        {
            HitCounter hitCounter = new();
            hitCounter.Hit(1);
            hitCounter.Hit(2);
            hitCounter.Hit(3);
            Console.WriteLine(hitCounter.GetHits(4));   // 3
            hitCounter.Hit(300);
            Console.WriteLine(hitCounter.GetHits(300)); // 4
            Console.WriteLine(hitCounter.GetHits(301)); // 3
        }
    }

    // Time:
    //   Hit: O(1)
    //   GetHits: O(300) = O(1)
    // Space: O(300) = O(1) constant fixed space circular buffer
    public class HitCounter
    {
        private const int WindowSeconds = 300;
        private readonly int[] _timestamps;
        private readonly int[] _hits;

        public HitCounter()
        {
            _timestamps = new int[WindowSeconds];
            _hits = new int[WindowSeconds];
        }

        public void Hit(int timestamp)
        {
            int idx = timestamp % WindowSeconds;

            if (_timestamps[idx] != timestamp)
            {
                _timestamps[idx] = timestamp;
                _hits[idx] = 1;
            }
            else
            {
                _hits[idx]++;
            }
        }

        public int GetHits(int timestamp)
        {
            int totalHits = 0;

            for (int i = 0; i < WindowSeconds; i++)
            {
                if (timestamp - _timestamps[i] < WindowSeconds)
                {
                    totalHits += _hits[i];
                }
            }

            return totalHits;
        }
    }
}