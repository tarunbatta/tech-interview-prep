// Url: https://leetcode.com/problems/encode-string-with-shortest-length

/*
471. Encode String with Shortest Length
Hard

Given a string s, encode the string such that its encoded length is the shortest.

The encoding rule is: k[encoded_string], where the encoded_string inside the square brackets is being repeated exactly k times. k should be a positive integer.

If an encoding process does not make the string shorter, then do not encode it. If there are several solutions, return any of them.

Example 1:
Input: s = "aaa"
Output: "aaa"
Explanation: There is no way to encode it such that it is shorter than 3 characters, so return it as it is.

Example 2:
Input: s = "aaaaa"
Output: "5[a]"
Explanation: "5[a]" is shorter than "aaaaa" by 1 character.

Example 3:
Input: s = "aaaaaaaaaa"
Output: "10[a]"
Explanation: "a9[a]" or "9[a]a" are also valid solutions, but "10[a]" is the shortest possible.

Example 4:
Input: s = "aabcaabcd"
Output: "2[aabc]d"

Constraints:
1 <= s.length <= 150
s consists of only lowercase English letters.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem471
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(Encode("aaa"));        // aaa
            Console.WriteLine(Encode("aaaaa"));      // 5[a]
            Console.WriteLine(Encode("aaaaaaaaaa")); // 10[a]
            Console.WriteLine(Encode("aabcaabcd"));  // 2[aabc]d
        }

        // Time: O(n^3) where n is s.Length using interval dynamic programming
        // Space: O(n^3) for the 2D DP memoization table of encoded substrings
        public static string Encode(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length <= 4)
            {
                return s;
            }

            int n = s.Length;
            string[,] dp = new string[n, n];

            for (int len = 1; len <= n; len++)
            {
                for (int i = 0; i <= n - len; i++)
                {
                    int j = i + len - 1;
                    string sub = s.Substring(i, len);
                    dp[i, j] = sub;

                    if (len >= 5)
                    {
                        // Check if sub consists of repeated smaller periods
                        int pos = (sub + sub).IndexOf(sub, 1, StringComparison.Ordinal);

                        if (pos < sub.Length)
                        {
                            int count = sub.Length / pos;
                            string encodedPeriod = $"{count}[{dp[i, i + pos - 1]}]";

                            if (encodedPeriod.Length < dp[i, j].Length)
                            {
                                dp[i, j] = encodedPeriod;
                            }
                        }

                        // Try splitting at every intermediate index k
                        for (int k = i; k < j; k++)
                        {
                            string combined = dp[i, k] + dp[k + 1, j];

                            if (combined.Length < dp[i, j].Length)
                            {
                                dp[i, j] = combined;
                            }
                        }
                    }
                }
            }

            return dp[0, n - 1];
        }
    }
}