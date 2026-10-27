// Url: https://leetcode.com/problems/valid-word-square

/*
422. Valid Word Square
Easy

Given an array of strings words, return true if it forms a valid word square.

A sequence of strings forms a valid word square if the kth row and kth column read the exact same string, where 0 <= k < max(numRows, numColumns).

Example 1:
Input: words = ["abcd","bnrt","crmy","dtye"]
Output: true
Explanation:
The 1st row and 1st column both read "abcd".
The 2nd row and 2nd column both read "bnrt".
The 3rd row and 3rd column both read "crmy".
The 4th row and 4th column both read "dtye".
Therefore, it is a valid word square.

Example 2:
Input: words = ["abcd","bnrt","crm","dt"]
Output: true
Explanation:
The 1st row and 1st column both read "abcd".
The 2nd row and 2nd column both read "bnrt".
The 3rd row and 3rd column both read "crm".
The 4th row and 4th column both read "dt".
Therefore, it is a valid word square.

Example 3:
Input: words = ["ball","area","read","lady"]
Output: false
Explanation:
The 3rd row reads "read" while the 3rd column reads "lead".
Therefore, it is not a valid word square.

Constraints:
1 <= words.length <= 500
1 <= words[i].length <= 500
words[i] consists of only lowercase English letters.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem422
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(ValidWordSquare(new List<string> { "abcd", "bnrt", "crmy", "dtye" })); // True
            Console.WriteLine(ValidWordSquare(new List<string> { "abcd", "bnrt", "crm", "dt" }));     // True
            Console.WriteLine(ValidWordSquare(new List<string> { "ball", "area", "read", "lady" })); // False
        }

        // Time: O(N * L) where N is words.Count and L is average word length
        // Space: O(1) auxiliary space
        public static bool ValidWordSquare(IList<string> words)
        {
            if (words == null || words.Count == 0)
            {
                return true;
            }

            int n = words.Count;

            for (int r = 0; r < n; r++)
            {
                string rowWord = words[r];

                for (int c = 0; c < rowWord.Length; c++)
                {
                    // Column c must exist, have at least r + 1 characters, and match words[r][c]
                    if (c >= n || r >= words[c].Length || rowWord[c] != words[c][r])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}