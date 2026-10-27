namespace InterviewPreperationGuide.Core.LeetCode.problem1165
{
    using System;

    // 1165. Single-Row Keyboard
    // https://leetcode.com/problems/single-row-keyboard/
    //
    // There is a special keyboard with all keys in a single row.
    //
    // Given a string keyboard of length 26 indicating the layout of the keyboard (indexed from 0 to 25).
    // Initially, your finger is at index 0. To type a character, you have to move your finger to the index
    // of the desired character. The time taken to move your finger from index i to index j is |i - j|.
    //
    // You want to type a string word. Write a function to calculate how much time it takes to type it with one finger.
    //
    // Example 1:
    // Input: keyboard = "abcdefghijklmnopqrstuvwxyz", word = "cba"
    // Output: 4
    // Explanation: The index moves from 0 to 2 to write 'c' then to 1 to write 'b' then to 0 to write 'a'.
    // Total time = 2 + 1 + 1 = 4.
    //
    // Example 2:
    // Input: keyboard = "pqrstuvwxyzabcdefghijklmno", word = "leetcode"
    // Output: 73
    //
    // Constraints:
    // keyboard.length == 26
    // keyboard contains each English lowercase letter exactly once in some order.
    // 1 <= word.length <= 10^4
    // word[i] is an English lowercase letter.
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(CalculateTime("abcdefghijklmnopqrstuvwxyz", "cba")); // 4
            Console.WriteLine(CalculateTime("pqrstuvwxyzabcdefghijklmno", "leetcode")); // 73
        }

        // Time: O(N) where N is the length of word (building keyboard index map is O(26) = O(1))
        // Space: O(1) for index array of size 26
        public int CalculateTime(string keyboard, string word)
        {
            int[] pos = new int[26];
            for (int i = 0; i < keyboard.Length; i++)
            {
                pos[keyboard[i] - 'a'] = i;
            }

            int totalTime = 0;
            int currentPos = 0;
            for (int i = 0; i < word.Length; i++)
            {
                int targetPos = pos[word[i] - 'a'];
                totalTime += Math.Abs(targetPos - currentPos);
                currentPos = targetPos;
            }

            return totalTime;
        }
    }
}