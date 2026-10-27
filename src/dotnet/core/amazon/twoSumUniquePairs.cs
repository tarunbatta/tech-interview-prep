/*
Amazon OA: Two Sum - Unique Pairs

Given an int array nums and an int target, find how many unique pairs in the array such that their sum is equal
to target. Return the number of pairs.

Example 1:
Input: nums = [1, 1, 2, 45, 46, 46], target = 47
Output: 2
Explanation: 1 + 46 = 47, 2 + 45 = 47

Example 2:
Input: nums = [1, 1], target = 2
Output: 1

Example 3:
Input: nums = [1, 5, 1, 5], target = 6
Output: 1
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.Amazon {
    public class TwoSumUniquePairs {
        public static void Init () {
            Console.WriteLine (CountPairs ([1, 1, 2, 45, 46, 46], 47)); // 2
            Console.WriteLine (CountPairs ([1, 1], 2)); // 1
            Console.WriteLine (CountPairs ([1, 5, 1, 5], 6)); // 1
            Console.WriteLine (CountPairs ([3], 6)); // 0
        }

        // Track values seen; record each pair once by its smaller element.
        // Time: O (n)
        // Space: O (n)
        public static int CountPairs (int[] nums, int target) {
            HashSet<int> seen = new HashSet<int> ();
            HashSet<int> pairs = new HashSet<int> ();

            foreach (int num in nums) {
                int complement = target - num;

                if (seen.Contains (complement)) {
                    pairs.Add (Math.Min (num, complement));
                }

                seen.Add (num);
            }

            return pairs.Count;
        }
    }
}
