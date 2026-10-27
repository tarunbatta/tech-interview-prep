// Url: https://leetcode.com/problems/flip-game-ii

/*
294. Flip Game II
Medium

You are playing a Flip Game with your friend.

You are given a string currentState that contains only '+' and '-'. You and your friend take turns to flip two consecutive "++" into "--". The game ends when a person can no longer make a move, and therefore the other person will be the winner.

Given a string currentState, return true if the starting player can guarantee a win, and false otherwise.

Example 1:
Input: currentState = "++++"
Output: true
Explanation: The starting player can guarantee a win by flipping the middle "++" to become "+--+".

Example 2:
Input: currentState = "+"
Output: false

Constraints:
1 <= currentState.length <= 60
currentState[i] is either '+' or '-'.

Follow up: Derive its runtime complexity.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem294
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(CanWin("++++")); // True
            Console.WriteLine(CanWin("+"));    // False
        }

        // Time: O(2^n) worst case without memoization, substantially pruned by memoization
        // Space: O(2^n) for the memoization cache and call stack depth O(n)
        public static bool CanWin(string currentState)
        {
            if (string.IsNullOrEmpty(currentState) || currentState.Length < 2)
            {
                return false;
            }

            Dictionary<string, bool> memo = [];
            return CanWinHelper(currentState, memo);
        }

        private static bool CanWinHelper(string s, Dictionary<string, bool> memo)
        {
            if (memo.TryGetValue(s, out bool result))
            {
                return result;
            }

            char[] chars = s.ToCharArray();

            for (int i = 0; i < chars.Length - 1; i++)
            {
                if (chars[i] == '+' && chars[i + 1] == '+')
                {
                    chars[i] = '-';
                    chars[i + 1] = '-';
                    string nextState = new(chars);

                    bool opponentWins = CanWinHelper(nextState, memo);

                    // Revert move
                    chars[i] = '+';
                    chars[i + 1] = '+';

                    // If the opponent cannot win from nextState, the current player wins!
                    if (!opponentWins)
                    {
                        memo[s] = true;
                        return true;
                    }
                }
            }

            memo[s] = false;
            return false;
        }
    }
}