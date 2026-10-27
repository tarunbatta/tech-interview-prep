// Url: https://leetcode.com/problems/flip-game

/*
293. Flip Game
Easy

You are playing a Flip Game with your friend.

You are given a string currentState that contains only '+' and '-'. You and your friend take turns to flip two consecutive "++" into "--". The game ends when a person can no longer make a move, and therefore the other person will be the winner.

Return all possible states of the string currentState after one valid move. You may return the answer in any order. If there is no valid move, return an empty list [].

Example 1:
Input: currentState = "++++"
Output: ["--++","+--+","++--"]

Example 2:
Input: currentState = "+"
Output: []

Constraints:
1 <= currentState.length <= 500
currentState[i] is either '+' or '-'.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem293
{
    public class Solution
    {
        public void Init()
        {
            var res1 = GeneratePossibleNextMoves("++++");
            Console.WriteLine(string.Join(", ", res1)); // --++, +--+, ++--

            var res2 = GeneratePossibleNextMoves("+");
            Console.WriteLine(res2.Count); // 0
        }

        // Time: O(n^2) where n is currentState.Length (n possible flips, each creates an O(n) string)
        // Space: O(n) auxiliary space per generated state
        public static IList<string> GeneratePossibleNextMoves(string currentState)
        {
            List<string> result = new();

            if (string.IsNullOrEmpty(currentState) || currentState.Length < 2)
            {
                return result;
            }

            for (int i = 0; i < currentState.Length - 1; i++)
            {
                if (currentState[i] == '+' && currentState[i + 1] == '+')
                {
                    char[] chars = currentState.ToCharArray();
                    chars[i] = '-';
                    chars[i + 1] = '-';
                    result.Add(new string(chars));
                }
            }

            return result;
        }
    }
}