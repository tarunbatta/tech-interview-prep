// Url:https://leetcode.com/problems/backspace-string-compare

/*
844. Backspace String Compare
Easy

Given two strings S and T, return if they are equal when both are typed into empty text editors. # means a backspace character.


Example 1:

Input: S = "ab#c", T = "ad#c"
Output: true
Explanation: Both S and T become "ac".



Example 2:

Input: S = "ab##", T = "c#d#"
Output: true
Explanation: Both S and T become "".



Example 3:

Input: S = "a##c", T = "#a#c"
Output: true
Explanation: Both S and T become "c".



Example 4:

Input: S = "a#c", T = "b"
Output: false
Explanation: S becomes "c" while T becomes "b".


Note:


	1 <= S.length <= 200
	1 <= T.length <= 200
	S and T only contain lowercase letters and '#' characters.


Follow up:


	Can you solve it in O(N) time and O(1) space?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem844 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O ()
        // Space: O ()
        public static bool BackspaceCompare (string S, string T) {
            return false;
        }
    }
}