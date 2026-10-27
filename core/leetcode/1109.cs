// Url:https://leetcode.com/problems/corporate-flight-bookings

/*
1109. Corporate Flight Bookings
Medium

There are n flights, and they are labeled from 1 to n.

We have a list of flight bookings.  The i-th booking bookings[i] = [i, j, k] means that we booked k seats from flights labeled i to j inclusive.

Return an array answer of length n, representing the number of seats booked on each flight in order of their label.

 
Example 1:

Input: bookings = [[1,2,10],[2,3,20],[2,5,25]], n = 5
Output: [10,55,45,25,25]


 
Constraints:


	1 <= bookings.length <= 20000
	1 <= bookings[i][0] <= bookings[i][1] <= n <= 20000
	1 <= bookings[i][2] <= 10000
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1109 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static int[] CorpFlightBookings (int[][] bookings, int n) {
            return null;
        }
    }
}