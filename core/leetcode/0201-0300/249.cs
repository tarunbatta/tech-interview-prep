// Url: https://leetcode.com/problems/group-shifted-strings

/*
249. Group Shifted Strings
Medium

Given an array of strings strings, group all strings[i] that belong to the same shifting sequence. You may return the answer in any order.

We can shift a string by shifting each of its letters to its successive letter.
For example, "abc" can be shifted to be "bcd".

Example 1:
Input: strings = ["abc","bcd","acef","xyz","az","ba","a","z"]
Output: [["acef"],["a","z"],["abc","bcd","xyz"],["az","ba"]]

Example 2:
Input: strings = ["a"]
Output: [["a"]]

Constraints:
1 <= strings.length <= 200
1 <= strings[i].length <= 50
strings[i] consists of lowercase English letters.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPreperationGuide.Core.LeetCode.problem249
{
    public class Solution
    {
        public static void Init()
        {
            string[] strings = ["abc", "bcd", "acef", "xyz", "az", "ba", "a", "z"];
            var grouped = GroupStrings(strings);

            foreach (var group in grouped)
            {
                Console.WriteLine($"[{string.Join(", ", group)}]");
            }
        }

        // Time: O(N * L) where N is strings.Length and L is maximum string length
        // Space: O(N * L) for storing the normalized keys and grouped strings
        public static IList<IList<string>> GroupStrings(string[] strings)
        {
            if (strings == null || strings.Length == 0)
            {
                return [];
            }

            Dictionary<string, List<string>> map = [];

            foreach (string s in strings)
            {
                string key = GetKey(s);

                if (!map.TryGetValue(key, out List<string> list))
                {
                    list = [];
                    map[key] = list;
                }

                list.Add(s);
            }

            return [.. map.Values.Cast<IList<string>>()];
        }

        private static string GetKey(string s)
        {
            char[] chars = new char[s.Length];
            int shift = s[0] - 'a';

            for (int i = 0; i < s.Length; i++)
            {
                int diff = (s[i] - 'a' - shift + 26) % 26;
                chars[i] = (char)('a' + diff);
            }

            return new string(chars);
        }
    }
}