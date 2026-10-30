// Url: https://leetcode.com/problems/minimum-string-length-after-removing-substrings/

/*
2696. Minimum String Length After Removing Substrings
Easy

You are given a string s consisting only of uppercase English letters.

You can apply some operations to this string where, in one operation, you can remove any occurrence of one of the substrings "AB" or "CD" from s.

Return the minimum possible length of the resulting string that you can obtain.

Note that the string concatenates after removing the substring and could produce new "AB" or "CD" substrings.

Example 1:
Input: s = "ABFCACDB"
Output: 2
Explanation:
- We remove the substring "AB" in "ABFCACDB", so s = "FCACDB".
- Then, we remove the substring "CD" in "FCACDB", so s = "FCAB".
- Finally, we remove the substring "AB" in "FCAB", so s = "FC".
So the resulting length of the string is 2.
It can be shown that it is the minimum length that we can obtain.

Example 2:
Input: s = "ACBBD"
Output: 5
Explanation: We cannot do any operations on the string so the length remains the same.

Constraints:
1 <= s.length <= 100
s consists only of uppercase English letters.
*/

using System;
using System.Collections.Generic;

namespace InterviewPreperationGuide.Core.LeetCode.problem2696
{
    public class Solution
    {
        public static void Init()
        {
            Console.WriteLine(MinLength(null));           // Expected: 0
            Console.WriteLine(MinLength("ABFCACDB"));     // Expected: 2
            Console.WriteLine(MinLength("ACBBD"));        // Expected: 5
            Console.WriteLine(MinLength("CCCCDDDD"));     // Expected: 0
            Console.WriteLine(MinLength("D"));            // Expected: 1
        }

        // Time: O(n) where n is the length of string s
        // Space: O(n) auxiliary space for the stack
        public static int MinLength(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            Stack<char> stack = new();

            foreach (char c in s)
            {
                if (stack.Count > 0 &&
                    ((c == 'B' && stack.Peek() == 'A') ||
                     (c == 'D' && stack.Peek() == 'C')))
                {
                    stack.Pop();
                }
                else
                {
                    stack.Push(c);
                }
            }

            return stack.Count;
        }

        // Time: O(n) where n is the length of string s
        // Space: O(n) using an array / pointer simulating a stack (avoids object overhead)
        public static int MinLengthOptimized(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            char[] stack = new char[s.Length];
            int top = 0;

            foreach (char c in s)
            {
                if (top > 0 &&
                    ((c == 'B' && stack[top - 1] == 'A') ||
                     (c == 'D' && stack[top - 1] == 'C')))
                {
                    top--;
                }
                else
                {
                    stack[top++] = c;
                }
            }

            return top;
        }
    }
}

