// Url:https://leetcode.com/problems/range-sum-query-mutable

/*
307. Range Sum Query - Mutable
Medium

Given an integer array nums, find the sum of the elements between indices i and j (i ≤ j), inclusive.

The update(i, val) function modifies nums by updating the element at index i to val.

Example:

Given nums = [1, 3, 5]

sumRange(0, 2) -> 9
update(1, 2)
sumRange(0, 2) -> 8


Note:


	The array is only modifiable by the update function.
	You may assume the number of calls to update and sumRange function is distributed evenly.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem307 {
    public class Solution {
        public static void Init () {
            NumArray obj = new(null);
            NumArray.Update(0, 0);
            int param_2 = NumArray.SumRange(0, 0);
        }
    }

    public class NumArray(int[] nums)
    {
        public static void Update (int i, int val) {

        }

        public static int SumRange (int i, int j) {
            return 0;
        }
    }
}