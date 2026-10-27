// Url:https://leetcode.com/problems/k-empty-slots

/*
683. K Empty Slots
Hard

You have n bulbs labeled from 1 to n. You are given an array bulbs of length n where bulbs[i] = x means that on the (i+1)th day, bulb x will be turned on.

Find and return the earliest day that there exists two turned on bulbs that have exactly k bulbs between them that are all turned off.

If there is no such day, return -1.

Example 1:
Input: bulbs = [1,3,2], k = 1
Output: 2
Explanation:
On the first day: bulbs[0] = 1, first bulb is turned on: [1,0,0]
On the second day: bulbs[1] = 3, third bulb is turned on: [1,0,1]
On the third day: bulbs[2] = 2, second bulb is turned on: [1,1,1]
We return 2 because on the second day, there were two on bulbs with one off bulb between them.

Example 2:
Input: bulbs = [1,2,3], k = 1
Output: -1

Constraints:
- n == bulbs.length
- 1 <= n <= 2 * 10^4
- 1 <= bulbs[i] <= n
- bulbs is a permutation of numbers from 1 to n.
- 0 <= k <= 2 * 10^4
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem683 {
    public class Solution {
        public void Init() {
            int[] bulbs1 = [1, 3, 2];
            int k1 = 1;
            Console.WriteLine(KEmptySlots(bulbs1, k1)); // 2

            int[] bulbs2 = [1, 2, 3];
            int k2 = 1;
            Console.WriteLine(KEmptySlots(bulbs2, k2)); // -1
        }

        // Time: O(n) where n is the number of bulbs
        // Space: O(n) to store days array
        public int KEmptySlots(int[] bulbs, int k) {
            int n = bulbs.Length;
            int[] days = new int[n + 1];
            for (int i = 0; i < n; i++) {
                days[bulbs[i]] = i + 1;
            }

            int ans = int.MaxValue;
            int left = 1;
            int right = k + 2;

            for (int i = 1; right <= n; i++) {
                if (days[i] < days[left] || days[i] <= days[right]) {
                    if (i == right) {
                        ans = Math.Min(ans, Math.Max(days[left], days[right]));
                    }
                    left = i;
                    right = i + k + 1;
                }
            }

            return ans == int.MaxValue ? -1 : ans;
        }
    }
}