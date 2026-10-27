// Url: https://leetcode.com/problems/guess-number-higher-or-lower

/*
374. Guess Number Higher or Lower
Easy

We are playing the Guess Game. The game is as follows:

I pick a number from 1 to n. You have to guess which number I picked.

Every time you guess wrong, I will tell you whether the number I picked is higher or lower than your guess.

You call a pre-defined API int guess(int num), which returns three possible results:

- -1: Your guess is higher than the number I picked (i.e. num > pick).
- 1: Your guess is lower than the number I picked (i.e. num < pick).
- 0: your guess is equal to the number I picked (i.e. num == pick).

Return the number that I picked.

Example 1:
Input: n = 10, pick = 6
Output: 6

Example 2:
Input: n = 1, pick = 1
Output: 1

Example 3:
Input: n = 2, pick = 1
Output: 1

Constraints:
1 <= n <= 2^31 - 1
1 <= pick <= n
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem374
{
    public class GuessGame
    {
        private static int _secret = 6;

        public static void SetPick(int pick)
        {
            _secret = pick;
        }

        public static int Guess(int num)
        {
            if (num > _secret)
            {
                return -1;
            }
            if (num < _secret)
            {
                return 1;
            }
            return 0;
        }
    }

    public class Solution : GuessGame
    {
        public void Init()
        {
            SetPick(6);
            Console.WriteLine(GuessNumber(10)); // 6

            SetPick(1);
            Console.WriteLine(GuessNumber(1));  // 1
        }

        // Time: O(log n) using binary search
        // Space: O(1) auxiliary space
        public static int GuessNumber(int n)
        {
            int low = 1;
            int high = n;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                int res = Guess(mid);

                if (res == 0)
                {
                    return mid;
                }
                else if (res < 0)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return -1;
        }
    }
}