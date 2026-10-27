// Url:https://leetcode.com/problems/positions-of-large-groups

/*
830. Positions of Large Groups
Easy

In a string S of lowercase letters, these letters form consecutive groups of the same character.

For example, a string like S = "abbxxxxzyy" has the groups "a", "bb", "xxxx", "z" and "yy".

Call a group large if it has 3 or more characters.  We would like the starting and ending positions of every large group.

The final answer should be in lexicographic order.

 

Example 1:

Input: "abbxxxxzzy"
Output: [[3,6]]
Explanation: "xxxx" is the single large group with starting  3 and ending positions 6.


Example 2:

Input: "abc"
Output: []
Explanation: We have "a","b" and "c" but no large group.


Example 3:

Input: "abcdddeeeeaabbbcd"
Output: [[3,5],[6,9],[12,14]]

 

Note:  1 <= S.length <= 1000
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem830 {
    public class Solution {
        public static void Init () {
            Console.WriteLine (LargeGroupPositions(null));
            Console.WriteLine (LargeGroupPositions(string.Empty));
            Console.WriteLine (LargeGroupPositions("abbxxxxzzy"));
            Console.WriteLine (LargeGroupPositions("abc"));
            Console.WriteLine (LargeGroupPositions("aaa"));
            Console.WriteLine (LargeGroupPositions("aaab"));
            Console.WriteLine (LargeGroupPositions("abcdddeeeeaabbbcd"));
        }

        public static IList<IList<int>> LargeGroupPositions (string S) {
            List<IList<int>> result = [];

            if (string.IsNullOrEmpty (S)) {
                return result;
            }

            int start = 0;
            int end = 1;

            while (end < S.Length) {
                if (S[end - 1] != S[end]) {
                    if (end - start >= 3) {
                        result.Add ([start, end - 1]);
                    }

                    start = end;
                }

                end++;
            }

            if (end - start >= 3) {
                result.Add ([start, end - 1]);
            }

            return result;
        }
    }
}