// Url:https://leetcode.com/problems/triples-with-bitwise-and-equal-to-zero

/*
982. Triples with Bitwise AND Equal To Zero
Hard

Given an array of integers A, find the number of triples of indices (i, j, k) such that:


	0 <= i < A.length
	0 <= j < A.length
	0 <= k < A.length
	A[i] &amp; A[j] &amp; A[k] == 0, where &amp; represents the bitwise-AND operator.


 

Example 1:

Input: [2,1,3]
Output: 12
Explanation: We could choose the following i, j, k triples:
(i=0, j=0, k=1) : 2 &amp; 2 &amp; 1
(i=0, j=1, k=0) : 2 &amp; 1 &amp; 2
(i=0, j=1, k=1) : 2 &amp; 1 &amp; 1
(i=0, j=1, k=2) : 2 &amp; 1 &amp; 3
(i=0, j=2, k=1) : 2 &amp; 3 &amp; 1
(i=1, j=0, k=0) : 1 &amp; 2 &amp; 2
(i=1, j=0, k=1) : 1 &amp; 2 &amp; 1
(i=1, j=0, k=2) : 1 &amp; 2 &amp; 3
(i=1, j=1, k=0) : 1 &amp; 1 &amp; 2
(i=1, j=2, k=0) : 1 &amp; 3 &amp; 2
(i=2, j=0, k=1) : 3 &amp; 2 &amp; 1
(i=2, j=1, k=0) : 3 &amp; 1 &amp; 2


 

Note:


	1 <= A.length <= 1000
	0 <= A[i] < 2^16
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem982 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static int CountTriplets (int[] A) {
            return 0;
        }
    }
}