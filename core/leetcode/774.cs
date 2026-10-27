// Url:https://leetcode.com/problems/minimize-max-distance-to-gas-station

/*
774. Minimize Max Distance to Gas Station
Hard

You are given an integer array stations that represents the positions of the gas stations on the x-axis. You are also given an integer k.

You should add k new gas stations. You can add the stations anywhere on the x-axis, and not necessarily on an integer position.

Let penalty() be the maximum distance between adjacent gas stations after adding the k new stations.

Return the smallest possible value of penalty(). Answers within 10^-6 of the actual answer will be accepted.

Example 1:
Input: stations = [1,2,3,4,5,6,7,8,9,10], k = 9
Output: 0.50000

Example 2:
Input: stations = [23,24,36,39,46,56,57,65,84,98], k = 1
Output: 14.00000

Constraints:
- 10 <= stations.length <= 10^4
- 0 <= stations[i] <= 10^8
- stations is sorted in a strictly increasing order.
- 1 <= k <= 10^6
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem774 {
    public class Solution {
        public static void Init() {
            int[] stations1 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            int k1 = 9;
            Console.WriteLine($"{MinmaxGasDist(stations1, k1):F5}"); // 0.50000

            int[] stations2 = [23, 24, 36, 39, 46, 56, 57, 65, 84, 98];
            int k2 = 1;
            Console.WriteLine($"{MinmaxGasDist(stations2, k2):F5}"); // 14.00000
        }

        // Time: O(n * log(maxDist / 1e-6)) where n is the number of stations
        // Space: O(1)
        public static double MinmaxGasDist(int[] stations, int k) {
            double low = 0;
            double high = stations[^1] - stations[0];

            while (high - low > 1e-6) {
                double mid = low + (high - low) / 2.0;

                if (Possible(stations, k, mid)) {
                    high = mid;
                } else {
                    low = mid;
                }
            }

            return low;
        }

        private static bool Possible(int[] stations, int k, double maxDist) {
            int needed = 0;
            for (int i = 0; i < stations.Length - 1; i++) {
                needed += (int)((stations[i + 1] - stations[i]) / maxDist);
                if (needed > k) {
                    return false;
                }
            }
            return needed <= k;
        }
    }
}