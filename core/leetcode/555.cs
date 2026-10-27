// Url: https://leetcode.com/problems/split-concatenated-strings

/*
555. Split Concatenated Strings
Medium

Given an array of strings strs, you could concatenate these strings into a loop, where for each string you could choose to reverse it or not. Among all the possible loops, you need to find the lexicographically largest string after cutting the loop, which will make the looped string into a regular one.

Specifically, to find the largest string, you need to experience two phases:
1. Concatenate all the strings into a loop, where you can reverse some strings or not and connect them in the same order as given.
2. Cut and make one breakpoint in any place of the loop, which will make the looped string into a regular one starting from the character at the cutpoint.

And your job is to find the lexicographically largest one among all the possible regular strings.

Example 1:
Input: strs = ["abc","xyz"]
Output: "zyxcba"
Explanation: You can get the looped string "-abcxyz-", "-abczyx-", "-cbaxyz-", "-cbazyx-", 
where '-' represents the looped status. 
The answer string came from the fourth looped string, where you could cut from the character 'y' and make it to "zyxcba".

Example 2:
Input: strs = ["abc"]
Output: "cba"

Constraints:
1 <= strs.length <= 1000
1 <= strs[i].length <= 1000
1 <= sum(strs[i].length) <= 1000
strs[i] consists of lowercase English letters.
*/

using System;
using System.Text;

namespace InterviewPreperationGuide.Core.LeetCode.problem555
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(SplitLoopedString(new string[] { "abc", "xyz" })); // zyxcba
            Console.WriteLine(SplitLoopedString(new string[] { "abc" }));        // cba
        }

        // Time: O(N * L^2) where N is strs.Length and L is the average string length
        // Space: O(TotalLen) for StringBuilder and intermediate strings
        public static string SplitLoopedString(string[] strs)
        {
            if (strs == null || strs.Length == 0)
            {
                return string.Empty;
            }

            int n = strs.Length;
            string[] best = new string[n];

            // Greedily pick the lexicographically larger orientation for each string
            for (int i = 0; i < n; i++)
            {
                string rev = Reverse(strs[i]);
                best[i] = string.CompareOrdinal(strs[i], rev) >= 0 ? strs[i] : rev;
            }

            string result = string.Empty;

            for (int i = 0; i < n; i++)
            {
                string original = strs[i];
                string reversed = Reverse(strs[i]);

                // Construct mid part of the remaining strings
                StringBuilder mid = new();
                for (int j = i + 1; j < n; j++)
                {
                    mid.Append(best[j]);
                }
                for (int j = 0; j < i; j++)
                {
                    mid.Append(best[j]);
                }
                string midStr = mid.ToString();

                // Try cutting at each index of original and reversed
                foreach (string candidate in new string[] { original, reversed })
                {
                    for (int k = 0; k < candidate.Length; k++)
                    {
                        string current = candidate.Substring(k) + midStr + candidate.Substring(0, k);

                        if (string.CompareOrdinal(current, result) > 0)
                        {
                            result = current;
                        }
                    }
                }
            }

            return result;
        }

        private static string Reverse(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}