// Url:https://leetcode.com/problems/implement-rand10-using-rand7

/*
470. Implement Rand10() Using Rand7()
Medium

Given a function rand7 which generates a uniform random integer in the range 1 to 7, write a function rand10 which generates a uniform random integer in the range 1 to 10.

Do NOT use system's Math.random().


Example 1:

Input: 1
Output: [7]


Example 2:

Input: 2
Output: [8,4]


Example 3:

Input: 3
Output: [8,1,10]


Note:
	rand7 is predefined.
	Each testcase has one argument: n, the number of times that rand10 is called.

Follow up:
	What is the expected value for the number of calls to rand7() function?
	Could you minimize the number of calls to rand7()?
*/

using System;

namespace InterviewPreperationGuide.Core.LeetCode.problem470 {
    // The Rand7() API is already defined in the parent class SolBase. 
    // public int Rand7(); 
    // @return a random integer in the range 1 to 7
    public class Solution : SolBase {
        public static int Rand10 () {
            int result = 40;

            while (result >= 40) {
                result = (7 * (Rand7() - 1)) + (Rand7() - 1);
            }

            return (result % 10) + 1;
        }
    }

    public class SolBase {
        public static int Rand7 () {
            Random rand = new Random ();
            return rand.Next (1, 7);
        }
    }
}