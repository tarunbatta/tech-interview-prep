// Url: https://leetcode.com/problems/split-array-into-fibonacci-sequence/

/*
842. Split Array into Fibonacci Sequence
Medium

Given a string S of digits, such as S = "123456579", we can split it into a Fibonacci-like sequence [123, 456, 579].

Formally, a Fibonacci-like sequence is a list F of non-negative integers such that:

0 <= F[i] <= 2^31 - 1, (that is, each integer fits a 32-bit signed integer type);
F.length >= 3;
and F[i] + F[i+1] = F[i+2] for all 0 <= i < F.length - 2.
Also, note that when splitting the string into pieces, each piece must not have extra leading zeroes, except if the piece is the number 0 itself.

Return any Fibonacci-like sequence split from S, or return [] if it cannot be done.

Example 1:

Input: "123456579"
Output: [123,456,579]
Example 2:

Input: "11235813"
Output: [1,1,2,3,5,8,13]
Example 3:

Input: "112358130"
Output: []
Explanation: The task is impossible.
Example 4:

Input: "0123"
Output: []
Explanation: Leading zeroes are not allowed, so "01", "2", "3" is not valid.
Example 5:

Input: "1101111"
Output: [110, 1, 111]
Explanation: The output [11, 0, 11, 11] would also be accepted.
Note:

1 <= S.length <= 200
S contains only digits.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem842 {
    public class Solution {
        public static void Init() {
            Console.WriteLine (SplitIntoFibonacci(null));
            Console.WriteLine (SplitIntoFibonacci("123456579"));
            Console.WriteLine (SplitIntoFibonacci("11235813"));
            Console.WriteLine (SplitIntoFibonacci("112358130"));
            Console.WriteLine (SplitIntoFibonacci("0123"));
            Console.WriteLine (SplitIntoFibonacci("1101111"));
        }

        public static IList<int> SplitIntoFibonacci (string S) {
            if (string.IsNullOrEmpty (S)) {
                return null;
            }

            List<int> result = [];
            helper(result, S, 0);

            return result;
        }

        public static bool helper (List<int> result, String S, int start) {
            // If we reached end of string & we have more than 2 elements in our sequence then return true
            if (start == S.Length && result.Count >= 3) {
                return true;
            }

            long num = 0;

            // Try getting a solution by forming a number with 'i' chars begginning with start
            for (int i = start; i < S.Length; i++) {
                if (S[start] == '0' && i > start) {
                    break;
                }

                num = num * 10 + (S[i] - '0');

                if (num > Int32.MaxValue) {
                    break;
                }

                // If fibonacci property is not satisfied then we cannot get a solution
                if (result.Count >= 2 && result[^1] + result[^2] < num) {
                    break;
                }

                if (result.Count <= 1 || result[^1] + result[^2] == num) {
                    result.Add ((int) num);

                    if (helper(result, S, i + 1)) {
                        return true;
                    }

                    result.RemoveAt (result.Count - 1);
                }
            }

            return false;
        }
    }
}