// Url:https://leetcode.com/problems/group-anagrams

/*
49. Group Anagrams
Medium

Given an array of strings, group anagrams together.

Example:

Input: ["eat", "tea", "tan", "ate", "nat", "bat"],
Output:
[
  ["ate","eat","tea"],
  ["nat","tan"],
  ["bat"]
]

Note:
	All inputs will be in lowercase.
	The order of your output does not matter.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem49 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (nk)
        // Space: O (nk)
        public static IList<IList<string>> GroupAnagrams (string[] strs) {
            IList<IList<string>> result = [];

            if (strs == null || strs.Length == 0) {
                return result;
            }

            Dictionary<string, List<string>> dict = [];

            foreach (var item in strs) {
                char[] arr = item.ToCharArray ();
                Array.Sort (arr);
                string key = new(arr);

                if (!dict.ContainsKey (key)) {
                    dict.Add (key, []);
                }

                dict[key].Add (item);
            }

            return [.. dict.Values];
        }
    }
}