// Url: https://leetcode.com/problems/find-permutation

/*
484. Find Permutation
Medium

A permutation perm of n + 1 integers of all the integers in the range [1, n + 1] can be represented as a string s of length n where:
- s[i] == 'I' if perm[i] < perm[i + 1], and
- s[i] == 'D' if perm[i] > perm[i + 1].

Given a string s, reconstruct the lexicographically smallest permutation perm and return it.

Example 1:
Input: s = "I"
Output: [1,2]
Explanation: [1,2] is the only unique permutation that can be represented by "I".

Example 2:
Input: s = "DI"
Output: [2,1,3]
Explanation: Both [2,1,3] and [3,1,2] can be represented as "DI", but since we want to find the smallest lexicographical permutation, you should return [2,1,3].

Constraints:
1 <= s.length <= 10^5
s[i] is either 'I' or 'D'.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem484
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(string.Join(", ", FindPermutation("I")));     // 1, 2
            Console.WriteLine(string.Join(", ", FindPermutation("DI")));    // 2, 1, 3
            Console.WriteLine(string.Join(", ", FindPermutation("DDIIDI"))); // 3, 2, 1, 4, 6, 5, 7
        }

        // Time: O(n) where n is s.Length + 1 (reversing contiguous 'D' segments)
        // Space: O(1) auxiliary space (excluding output array)
        public static int[] FindPermutation(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return new int[] { 1 };
            }

            int n = s.Length + 1;
            int[] result = new int[n];

            for (int i = 0; i < n; i++)
            {
                result[i] = i + 1;
            }

            int start = 0;

            while (start < s.Length)
            {
                if (s[start] == 'D')
                {
                    int end = start;

                    while (end < s.Length && s[end] == 'D')
                    {
                        end++;
                    }

                    // Reverse subarray from start to end
                    Reverse(result, start, end);
                    start = end;
                }
                else
                {
                    start++;
                }
            }

            return result;
        }

        private static void Reverse(int[] arr, int left, int right)
        {
            while (left < right)
            {
                (arr[left], arr[right]) = (arr[right], arr[left]);
                left++;
                right--;
            }
        }
    }
}