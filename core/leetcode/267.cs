// Url: https://leetcode.com/problems/palindrome-permutation-ii

/*
267. Palindrome Permutation II
Medium

Given a string s, return all the palindromic permutations (without duplicates) of it.

You may return the answer in any order. If no palindromic permutation can be formed, return an empty list.

Example 1:
Input: s = "aabb"
Output: ["abba","baab"]

Example 2:
Input: s = "abc"
Output: []

Constraints:
1 <= s.length <= 16
s consists of only lowercase English letters.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem267
{
    public class Solution
    {
        public void Init()
        {
            var res1 = GeneratePalindromes("aabb");
            Console.WriteLine(string.Join(", ", res1)); // abba, baab

            var res2 = GeneratePalindromes("abc");
            Console.WriteLine(res2.Count); // 0
        }

        // Time: O(((n/2)!) * n) where n is s.Length
        // Space: O(n) for recursion stack and character buffers
        public static IList<string> GeneratePalindromes(string s)
        {
            List<string> result = new();

            if (string.IsNullOrEmpty(s))
            {
                return result;
            }

            int[] map = new int[26];
            foreach (char c in s)
            {
                map[c - 'a']++;
            }

            string middle = string.Empty;
            List<char> half = new();

            for (int i = 0; i < 26; i++)
            {
                if (map[i] % 2 != 0)
                {
                    if (!string.IsNullOrEmpty(middle))
                    {
                        return result; // More than 1 odd frequency char
                    }

                    middle = ((char)('a' + i)).ToString();
                }

                for (int count = 0; count < map[i] / 2; count++)
                {
                    half.Add((char)('a' + i));
                }
            }

            char[] halfArr = half.ToArray();
            bool[] used = new bool[halfArr.Length];
            char[] current = new char[halfArr.Length];

            Permute(halfArr, used, current, 0, middle, result);

            return result;
        }

        private static void Permute(char[] half, bool[] used, char[] current, int depth, string middle, List<string> result)
        {
            if (depth == half.Length)
            {
                string left = new(current);
                char[] reversed = (char[])current.Clone();
                Array.Reverse(reversed);
                result.Add(left + middle + new string(reversed));
                return;
            }

            for (int i = 0; i < half.Length; i++)
            {
                if (used[i])
                {
                    continue;
                }

                if (i > 0 && half[i] == half[i - 1] && !used[i - 1])
                {
                    continue;
                }

                used[i] = true;
                current[depth] = half[i];

                Permute(half, used, current, depth + 1, middle, result);

                used[i] = false;
            }
        }
    }
}