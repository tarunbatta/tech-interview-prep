// Url: https://leetcode.com/problems/special-binary-string

/*
761. Special Binary String
Hard

Special binary strings are binary strings with the following two properties:

The number of 0's is equal to the number of 1's.
Every prefix of the binary string has at least as many 1's as 0's.
Given a special string S, a move consists of choosing two consecutive, non-empty, special substrings of S, and swapping them. (Two strings are consecutive if the last character of the first string is exactly one index before the first character of the second string.)

At the end of any number of moves, what is the lexicographically largest resulting string possible?

Example 1:
Input: S = "11011000"
Output: "11100100"
Explanation:
The strings "10" [occuring at S[1]] and "1100" [at S[3]] are swapped.
This is the lexicographically largest string possible after some number of swaps.
Note:

S has length at most 50.
S is guaranteed to be a special binary string as defined above.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem761 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (MakeLargestSpecial (null));
            Console.WriteLine (MakeLargestSpecial ("11011000"));
        }

        public static string MakeLargestSpecial (string S) {
            string result = string.Empty;

            if (string.IsNullOrEmpty (S)) {
                return result;
            }

            Dictionary<string, string> visited = [];
            result = GetLargest(S, visited);

            return result;
        }

        private static string GetLargest (string s, Dictionary<string, string> visited) {
            if (visited.ContainsKey (s)) {
                return visited[s];
            }

            string result = s;

            for (int i = 0; i < s.Length; i++) {
                for (int j = i + 1; j < s.Length; j++) {
                    string first = s[i..j];

                    if (!CheckSpecial(first)) {
                        continue;
                    }

                    for (int k = j + 1; k < s.Length; k++) {
                        string second = s.Substring (j, k - j + 1);
                        string check1 = second + first, check2 = first + second;

                        if (!CheckSpecial(second) || check1.CompareTo (check2) <= 0) {
                            continue;
                        }

                        string answer = s[..i] + second + first + s.Substring (k + 1, s.Length - k - 1);
                        if (answer.CompareTo (result) > 0) {
                            result = answer;
                        }
                    }
                }
            }
            visited.Add (s, result);
            return GetLargest(result, visited);
        }

        private static bool CheckSpecial (string first) {
            int check = 0;

            for (int i = 0; i < first.Length; i++) {
                if (first[i] == '1') {
                    check++;
                } else {
                    check--;
                    if (check < 0) {
                        return false;
                    }
                }
            }

            return check == 0;
        }
    }
}