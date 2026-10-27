// Url: https://leetcode.com/problems/number-of-steps-to-reduce-a-number-in-binary-representation-to-one/

/*
1404. Number of Steps to Reduce a Number in Binary Representation to One
Medium

Given the binary representation of an integer as a string s, return the number of steps to reduce it to 1 under the following rules:

If the current number is even, you have to divide it by 2.
If the current number is odd, you have to add 1 to it.

It is guaranteed that you can always reach 1 for all test cases.

Example 1:
Input: s = "1101"
Output: 6
Explanation: "1101" corressponds to number 13 in decimal.
Step 1) 13 is odd, add 1 and obtain 14. 
Step 2) 14 is even, divide by 2 and obtain 7.
Step 3) 7 is odd, add 1 and obtain 8.
Step 4) 8 is even, divide by 2 and obtain 4.  
Step 5) 4 is even, divide by 2 and obtain 2. 
Step 6) 2 is even, divide by 2 and obtain 1.  

Example 2:
Input: s = "10"
Output: 1
Explanation: "10" corresponds to number 2 in decimal.
Step 1) 2 is even, divide by 2 and obtain 1.  

Example 3:
Input: s = "1"
Output: 0
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem1404
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(NumSteps(null));     // Expected: 0
            Console.WriteLine(NumSteps("1101"));   // Expected: 6
            Console.WriteLine(NumSteps("10"));     // Expected: 1
            Console.WriteLine(NumSteps("1"));      // Expected: 0
        }

        // Time: O(n) where n is the length of binary string s
        // Space: O(1) auxiliary space (right-to-left single pass simulation)
        public static int NumSteps(string s)
        {
            if (string.IsNullOrEmpty(s) || s == "1")
            {
                return 0;
            }

            int steps = 0;
            int carry = 0;

            for (int i = s.Length - 1; i >= 1; i--)
            {
                int currentBit = (s[i] - '0') + carry;

                if (currentBit == 1)
                {
                    // Odd: add 1 (takes 1 step and produces a carry) + divide by 2 (takes 1 step)
                    steps += 2;
                    carry = 1;
                }
                else
                {
                    // Even (0 or 2): divide by 2 (takes 1 step)
                    steps += 1;
                }
            }

            return steps + carry;
        }
    }
}
