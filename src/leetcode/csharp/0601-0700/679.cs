// Url:https://leetcode.com/problems/24-game

/*
679. 24 Game
Hard

You have 4 cards each containing a number from 1 to 9.  You need to judge whether they could operated through *, /, +, -, (, ) to get the value of 24.


Example 1:
Input: [4, 1, 8, 7]
Output: True
Explanation: (8-4) * (7-1) = 24



Example 2:
Input: [1, 2, 1, 2]
Output: False



Note:

The division operator / represents real division, not integer division.  For example, 4 / (1 - 2/3) = 12.
Every operation done is between two numbers.  In particular, we cannot use - as a unary operator.  For example, with [1, 1, 1, 1] as input, the expression -1 - 1 - 1 - 1 is not allowed.
You cannot concatenate numbers together.  For example, if the input is [1, 2, 1, 2], we cannot write this as 12 + 12.
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem679 {
    public class Solution {
        public static void Init () {
            Console.WriteLine ();
        }

        // Time: O (1)
        // Space: O (1)
        public static bool JudgePoint24 (int[] nums) {
            double[] n = new double[4];
            for (int i = 0; i < 4; i++)
                n[i] = nums[i];
            return dfs(n);
        }

        private static bool dfs (double[] nums) {
            int n = nums.Length;
            if (n == 1)
                return Math.Abs (nums[0] - 24) < 0.001;

            double[] left = new double[n - 1];
            for (int i = 0; i < n; i++) {
                for (int j = i + 1; j < n; j++) {
                    double a = nums[i];
                    double b = nums[j];

                    for (int k = 0, l = 0; k < n; k++) {
                        if (k != i && k != j) {
                            left[l] = nums[k];
                            l++;
                        }
                    }

                    left[n - 2] = a + b;
                    if (dfs(left))
                        return true;

                    left[n - 2] = a - b;
                    if (dfs(left))
                        return true;

                    left[n - 2] = a * b;
                    if (dfs(left))
                        return true;

                    left[n - 2] = a / b;
                    if (dfs(left))
                        return true;

                    left[n - 2] = b - a;
                    if (dfs(left))
                        return true;

                    left[n - 2] = b / a;
                    if (dfs(left))
                        return true;
                }
            }

            return false;
        }
    }
}