// Url:https://leetcode.com/problems/split-array-with-same-average

/*
805. Split Array With Same Average
Hard

In a given integer array A, we must move every element of A to either list B or list C. (B and C initially start empty.)

Return true if and only if after such a move, it is possible that the average value of B is equal to the average value of C, and B and C are both non-empty.

Example :
Input: 
[1,2,3,4,5,6,7,8]
Output: true
Explanation: We can split the array into [1,4,5,8] and [2,3,6,7], and both of them have the average of 4.5.


Note:
	The length of A will be in the range [1, 30].
	A[i] will be in the range of [0, 10000]. 
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem805 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static bool SplitArraySameAverage (int[] A) {
            int N = A.Length;

            if (N < 2) {
                return false;
            }

            // sum will become the average after array values will 
            // be multiplied by N to avoid fractional arithmetic 
            int sum = 0;

            foreach (int x in A) {
                sum += x;
            }

            // subtract the average to make our array suitable
            // for finding a subset with sum == 0
            for (int i = 0; i < A.Length; ++i) {
                A[i] = A[i] * N - sum;
            }

            // first half
            HashSet<int> left = [A[0]];

            for (int i = 1; i < N / 2; ++i) {
                HashSet<int> left2 = [];
                int z = A[i];
                left2.Add (z);

                foreach (int p in left) {
                    left2.Add (p);
                    left2.Add (p + z);
                }

                left = left2;
            }

            // quick check, can be done while filling the set
            if (left.Contains (0)) {
                return true;
            }

            // second half
            HashSet<int> right = [A[N - 1]];

            for (int i = N / 2; i < N - 1; ++i) {
                HashSet<int> right2 = [];
                int z = A[i];
                right2.Add (z);

                foreach (int p in right) {
                    right2.Add (p);
                    right2.Add (p + z);
                }

                right = right2;
            }

            // quick check, can be done while filling the set
            if (right.Contains (0)) {
                return true;
            }

            int sleft = 0;
            for (int i = 0; i < N / 2; ++i) {
                sleft += A[i];
            }

            int sright = 0;
            for (int i = N / 2; i < N; ++i) {
                sright += A[i];
            }

            // check sums
            foreach (int lSum in left) {
                int rSum = -lSum;
                // if the left and right sums cancel out can find a subset with a sum=0 and 
                // the array can be split to non-empty sets
                if (right.Contains (rSum) && (lSum != sleft || rSum != sright))
                    return true;
            }
            return false;
        }
    }
}