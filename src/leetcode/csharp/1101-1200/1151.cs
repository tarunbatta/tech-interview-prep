// Url:https://leetcode.com/problems/minimum-swaps-to-group-all-1s-together

/*
1151. Minimum Swaps to Group All 1's Together
Medium

Given a binary array data, return the minimum number of swaps required to group all 1's present in the array together in any place in the array.

Example 1:
Input: data = [1,0,1,0,1]
Output: 1
Explanation: There are 3 ways to group all 1's together:
[1,1,1,0,0] using 1 swap.
[0,1,1,1,0] using 2 swaps.
[0,0,1,1,1] using 1 swap.
The minimum is 1.

Example 2:
Input: data = [0,0,0,10] (wait: binary array [0,0,0,1,0])
Output: 0
Explanation: Since there is only one 1 in the array, no swaps needed.

Example 3:
Input: data = [1,0,1,0,1,0,0,1,1,0,1]
Output: 3
Explanation: One possible solution that uses 3 swaps is [0,0,0,0,0,1,1,1,1,1,1].

Constraints:
- 1 <= data.length <= 10^5
- data[i] is 0 or 1.
*/

using System;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem1151 {
    public class Solution {
        public static void Init() {
            int[] d1 = [1, 0, 1, 0, 1];
            Console.WriteLine(MinSwaps(d1)); // 1

            int[] d2 = [0, 0, 0, 1, 0];
            Console.WriteLine(MinSwaps(d2)); // 0

            int[] d3 = [1, 0, 1, 0, 1, 0, 0, 1, 1, 0, 1];
            Console.WriteLine(MinSwaps(d3)); // 3
        }

        // Time: O(n) where n is the length of data
        // Space: O(1)
        public static int MinSwaps(int[] data) {
            int totalOnes = 0;
            foreach (int x in data) {
                if (x == 1) {
                    totalOnes++;
                }
            }

            if (totalOnes <= 1) {
                return 0;
            }

            int currentOnes = 0;
            for (int i = 0; i < totalOnes; i++) {
                currentOnes += data[i];
            }

            int maxOnes = currentOnes;
            for (int i = totalOnes; i < data.Length; i++) {
                currentOnes += data[i] - data[i - totalOnes];
                if (currentOnes > maxOnes) {
                    maxOnes = currentOnes;
                }
            }

            return totalOnes - maxOnes;
        }
    }
}