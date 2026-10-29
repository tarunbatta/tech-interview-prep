/*
Pairs with Sum: 

Design an algorithm to find all pairs of integers within an array which sum to a
specified value.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Ctci.c16q24 {
    public class Solution {
        public static List<(int, int)> FindPairs (int[] nums, int sum) {
            List<(int, int)> result = new List<(int, int)> ();
            HashSet<int> seen = new HashSet<int> ();

            foreach (int num in nums) {
                int complement = sum - num;
                if (seen.Contains (complement)) {
                    result.Add ((complement, num));
                }
                seen.Add (num);
            }
            return result;
        }

        public static void Init (string[] args = null) {
            int[] nums = { 1, 4, 2, 5, 6, 7, 0, 3 };
            var pairs = FindPairs (nums, 7);
            Console.WriteLine ($"Pairs summing to 7: {string.Join (", ", pairs)}");
        }
    }
}
