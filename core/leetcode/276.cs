// Url: https://leetcode.com/problems/paint-fence

/*
276. Paint Fence
Medium

You are painting a fence of n posts with k different colors. You must paint the posts following these rules:
- Every post has to be painted exactly one color.
- There cannot be three or more consecutive posts with the same color.

Given the two integers n and k, return the number of ways you can paint the fence.

Example 1:
Input: n = 3, k = 2
Output: 6
Explanation: All the possibilities are:
post 1  post 2  post 3      post 1  post 2  post 3
   c1      c1      c2          c2      c1      c1
   c1      c2      c1          c2      c1      c2
   c1      c2      c2          c2      c2      c1

Example 2:
Input: n = 1, k = 1
Output: 1

Example 3:
Input: n = 7, k = 2
Output: 42

Constraints:
1 <= n <= 50
1 <= k <= 10^5
The marked answer is guaranteed to fit within a 32-bit integer for the given input.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem276
{
    public class Solution
    {
        public void Init()
        {
            Console.WriteLine(NumWays(3, 2)); // 6
            Console.WriteLine(NumWays(1, 1)); // 1
            Console.WriteLine(NumWays(7, 2)); // 42
        }

        // Time: O(n)
        // Space: O(1) auxiliary space
        public static int NumWays(int n, int k)
        {
            if (n <= 0 || k <= 0)
            {
                return 0;
            }

            if (n == 1)
            {
                return k;
            }

            int same = k;
            int diff = k * (k - 1);
            int total = same + diff;

            for (int i = 3; i <= n; i++)
            {
                same = diff;
                diff = total * (k - 1);
                total = same + diff;
            }

            return total;
        }
    }
}