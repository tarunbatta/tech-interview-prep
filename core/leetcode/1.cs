// Url: https://leetcode.com/problems/two-sum/

/*
1. Two Sum
Easy

Given an array of integers, return indices of the two numbers such that they add up to a specific target.

You may assume that each input would have exactly one solution, and you may not use the same element twice.

Example:

Given nums = [2, 7, 11, 15], target = 9,

Because nums[0] + nums[1] = 2 + 7 = 9,
return [0, 1].
*/

using System;
using System.Collections;

namespace InterviewPreperationGuide.Core.LeetCode.problem1
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(TwoSum_a(null, 9));
            Console.WriteLine(TwoSum_a(new int[] { 2, 7, 11, 15 }, 9));
            Console.WriteLine(TwoSum_a(new int[] { 3, 3 }, 6));

            Console.WriteLine(TwoSum_b(null, 9));
            Console.WriteLine(TwoSum_b(new int[] { 2, 7, 11, 15 }, 9));
            Console.WriteLine(TwoSum_b(new int[] { 3, 3 }, 6));

            Console.WriteLine(TwoSum_c(null, 9));
            Console.WriteLine(TwoSum_c(new int[] { 2, 7, 11, 15 }, 9));
            Console.WriteLine(TwoSum_c(new int[] { 3, 3 }, 6));
        }

        public static int[] TwoSum_a(int[] nums, int target)
        {
            int[] result = null;

            if (nums == null || nums.Length == 0)
            {
                return null;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        return new int[2] { i, j };
                    }
                }
            }

            return result;
        }

        public static int[] TwoSum_b(int[] nums, int target)
        {
            int[] result = null;

            if (nums == null || nums.Length == 0)
            {
                return null;
            }

            Hashtable hash = new Hashtable();

            for (int i = 0; i < nums.Length; i++)
            {
                if (!hash.ContainsKey(nums[i]))
                {
                    hash.Add(nums[i], i);
                }
            }

            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];

                if (hash.ContainsKey(complement) && (int)hash[complement] != i)
                {
                    return new int[2] {
                    (int) hash[complement], i
                    };
                }
            }

            return result;
        }

        public static int[] TwoSum_c(int[] nums, int target)
        {
            int[] result = null;

            if (nums == null || nums.Length == 0)
            {
                return null;
            }

            Hashtable hash = new Hashtable();

            for (int i = 0; i < nums.Length; i++)
            {
                int complement = target - nums[i];

                if (!hash.ContainsKey(nums[i]))
                {
                    hash.Add(nums[i], i);
                }

                if (hash.ContainsKey(complement) && (int)hash[complement] != i)
                {
                    return new int[2] {
                    (int) hash[complement], i
                    };
                }
            }

            return result;
        }
    }
}