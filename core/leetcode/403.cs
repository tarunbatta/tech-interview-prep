// Url:https://leetcode.com/problems/frog-jump

/*
403. Frog Jump
Hard

A frog is crossing a river. The river is divided into x units and at each unit there may or may not exist a stone. The frog can jump on a stone, but it must not jump into the water.

Given a list of stones' positions (in units) in sorted ascending order, determine if the frog is able to cross the river by landing on the last stone. Initially, the frog is on the first stone and assume the first jump must be 1 unit.


If the frog's last jump was k units, then its next jump must be either k - 1, k, or k + 1 units. Note that the frog can only jump in the forward direction.

Note:

The number of stones is ≥ 2 and is < 1,100.
Each stone's position will be a non-negative integer < 231.
The first stone's position is always 0.



Example 1:
[0,1,3,5,6,8,12,17]

There are a total of 8 stones.
The first stone at the 0th unit, second stone at the 1st unit,
third stone at the 3rd unit, and so on...
The last stone at the 17th unit.

Return true. The frog can jump to the last stone by jumping 
1 unit to the 2nd stone, then 2 units to the 3rd stone, then 
2 units to the 4th stone, then 3 units to the 6th stone, 
4 units to the 7th stone, and 5 units to the 8th stone.



Example 2:
[0,1,2,3,4,8,9,11]

Return false. There is no way to jump to the last stone as 
the gap between the 5th and 6th stone is too large.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem403 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (CanCross(null));
            Console.WriteLine (CanCross([0, 1, 3, 5, 6, 8, 12, 17]));
            Console.WriteLine (CanCross([0, 1, 2, 3, 4, 8, 9, 11]));
            Console.WriteLine (CanCross([0, 1, 3, 4, 5, 7, 9, 10, 12]));
        }

        public static bool CanCross (int[] stones) {
            if (stones == null) {
                return false;
            }

            int n = stones.Length;

            if (n <= 1) {
                return true;
            }
            if (stones[1] != 1) {
                return false;
            }

            bool[, ] dp = new bool[n, n];

            return RecursiveCall(stones, dp, 1, 0);
        }

        public static bool RecursiveCall (int[] stones, bool[, ] dp, int index, int last) {
            if (index == stones.Length - 1) {
                return true;
            }

            if (dp[index, last]) {
                return false;
            }

            dp[index, last] = true;

            int dist = stones[index] - stones[last];
            int st = index + 1;
            int ed = stones.Length - 1;
            int re = -1;

            while (st <= ed) {
                int mid = (st + ed) >> 1;

                if (stones[mid] - stones[index] >= dist - 1 && stones[mid] - stones[index] <= dist + 1) {
                    re = mid;
                    ed = mid - 1;
                } else if (stones[mid] - stones[index] < dist - 1) {
                    st = mid + 1;
                } else {
                    ed = mid - 1;
                }
            }

            if (re == -1) {
                return false;
            }

            for (int i = re; i < stones.Length; i++) {
                if (stones[i] - stones[index] > dist + 1) {
                    break;
                }

                if (RecursiveCall(stones, dp, i, index)) {
                    return true;
                }
            }

            return false;
        }
    }
}