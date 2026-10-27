// Url:https://leetcode.com/problems/russian-doll-envelopes

/*
354. Russian Doll Envelopes
Hard

You have a number of envelopes with widths and heights given as a pair of integers (w, h). One envelope can fit into another if and only if both the width and height of one envelope is greater than the width and height of the other envelope.

What is the maximum number of envelopes can you Russian doll? (put one inside other)

Note:
Rotation is not allowed.

Example:


Input: [[5,4],[6,4],[6,7],[2,3]]
Output: 3 
Explanation: The maximum number of envelopes you can Russian doll is 3 ([2,3] => [5,4] => [6,7]).
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem354 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (MaxEnvelopes(null));
            Console.WriteLine (MaxEnvelopes([
                [5, 4],
                    [6, 4],
                    [6, 7],
                    [2, 3]
            ]));
        }

        // Time: O (n log (n))
        // Space: O (n)
        public static int MaxEnvelopes (int[][] envelopes) {
            int result = 0;

            if (envelopes == null || envelopes.Length == 0 || envelopes[0].Length == 0) {
                return result;
            }

            int rows = envelopes.GetLength (0);
            Envelope[] lst = new Envelope[rows];

            for (int i = 0; i < rows; i++) {
                lst[i] = new Envelope (envelopes[i][0], envelopes[i][1]);
            }

            Array.Sort (lst);

            int[] dp = new int[rows];

            for (int i = 0; i < rows; i++) {
                int idx = Array.BinarySearch (dp, 0, result, lst[i].height);

                if (idx < 0) {
                    idx = -(idx + 1);
                }

                dp[idx] = lst[i].height;

                if (idx == result) {
                    result++;
                }
            }

            return result;
        }
    }

    public class Envelope(int width, int height) : IComparable<Envelope> {
        public int width { get; set; } = width;
        public int height { get; set; } = height;

        public int CompareTo (Envelope y) {
            if (this.width == y.width) {
                return y.height - this.height;
            } else {
                return this.width - y.width;
            }
        }
    }
}