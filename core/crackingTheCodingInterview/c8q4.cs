/*
Power Set: Write a method to return all subsets of a set.
Generate all subsets of a given array[1, 2, 3]
[[], [1], [2], [3], [1,2], [2,3], [1,3], [1,2,3]]
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.CrackingTheCodingInterview.c8q4 {
    public class Solution {
        public static void Init (string[] args) {
            PrintSubsets (Subsets (null));

            PrintSubsets (Subsets ([1, 2, 3]));
        }

        public static IList<IList<int>> Subsets (int[] nums) {
            IList<IList<int>> result = [];

            if (nums != null && nums.Length > 0) {
                result = GetSubsets (nums, 0);
            }

            return result;
        }

        public static IList<IList<int>> GetSubsets (int[] nums, int index) {
            IList<IList<int>> result;

            if (nums.Length == index) {
                result = [[]];
            } else {
                result = GetSubsets (nums, index + 1);
                int item = nums[index];
                IList<IList<int>> moresubsets = [];

                foreach (var subset in result) {
                    IList<int> newsubset = [.. subset, item];
                    moresubsets.Add (newsubset);
                }

                foreach (var moresub in moresubsets) {
                    result.Add (moresub);
                }
            }

            return result;
        }

        public static void PrintSubsets (IList<IList<int>> list) {
            Console.WriteLine ("[");

            if (list != null && list.Count > 0) {
                foreach (var item in list) {
                    Console.Write ("[");
                    Console.Write (string.Join (",", item));
                    Console.WriteLine ("]");
                }
            }

            Console.WriteLine ("]");
        }
    }
}