// Url:https://leetcode.com/problems/4-keys-keyboard

/*
651. 4 Keys Keyboard
Medium

Imagine you have a special keyboard with the following four keys:
- Key 1: (A): Print one 'A' on the screen.
- Key 2: (Ctrl-A): Select the whole screen.
- Key 3: (Ctrl-C): Copy selection to buffer.
- Key 4: (Ctrl-V): Print buffer on screen appending it after what has already been printed.

Given an integer n, return the maximum number of 'A' you can print on the screen with at most n keypresses.

Example 1:
Input: n = 3
Output: 3
Explanation: We can at most get 3 A's on screen by pressing following key sequence:
A, A, A

Example 2:
Input: n = 7
Output: 9
Explanation: We can at most get 9 A's on screen by pressing following key sequence:
A, A, A, Ctrl A, Ctrl C, Ctrl V, Ctrl V

Constraints:
- 1 <= n <= 50
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem651
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(MaxA(3)); // 3
            Console.WriteLine(MaxA(7)); // 9
            Console.WriteLine(MaxA(11)); // 27
        }

        // Time: O(n^2)
        // Space: O(n)
        public static int MaxA(int n)
        {
            int[] dp = new int[n + 1];

            for (int i = 1; i <= n; i++)
            {
                dp[i] = dp[i - 1] + 1; // Press 'A'
                for (int j = 1; j <= i - 3; j++)
                {
                    // Ctrl-A, Ctrl-C at step j+1, j+2, then Ctrl-V (i - j - 1) times total factor (i - j - 1)
                    dp[i] = Math.Max(dp[i], dp[j] * (i - j - 1));
                }
            }

            return dp[n];
        }
    }
}