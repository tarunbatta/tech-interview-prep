// Url:https://leetcode.com/problems/find-the-derangement-of-an-array

/*
634. Find the Derangement of An Array
Medium

In combinatorial mathematics, a derangement is a permutation of the elements of a set, such that no element appears in its original position.

You are given an integer n, return the number of derangements of an array consisting of n consecutive integers from 1 to n. Since the answer may be large, return it modulo 10^9 + 7.

Example 1:
Input: n = 3
Output: 2
Explanation: The original array is [1,2,3]. The two derangements are [2,3,1] and [3,1,2].

Example 2:
Input: n = 2
Output: 1

Constraints:
- 1 <= n <= 10^6
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem634 {
    public class Solution {
        public void Init() {
            Console.WriteLine(FindDerangement(3)); // 2
            Console.WriteLine(FindDerangement(2)); // 1
            Console.WriteLine(FindDerangement(1)); // 0
        }

        // Time: O(n)
        // Space: O(1)
        public int FindDerangement(int n) {
            if (n <= 1) {
                return 0;
            }

            const int mod = 1_000_000_007;
            long prev2 = 0; // D(1) = 0
            long prev1 = 1; // D(2) = 1

            for (int i = 3; i <= n; i++) {
                long current = ((i - 1L) * (prev1 + prev2)) % mod;
                prev2 = prev1;
                prev1 = current;
            }

            return (int)prev1;
        }
    }
}